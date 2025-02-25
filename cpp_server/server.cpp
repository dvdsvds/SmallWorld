#include <asm-generic/socket.h>
#include <iostream>
#include <sstream>
#include <string>
#include <curl/curl.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <unistd.h>
#include <cstring>

#define PORT 8080

const char* env_ip = std::getenv("IP");
std::string ip = (env_ip != nullptr) ? env_ip : "0.0.0.0"; 

size_t wcb(void *contents, size_t size, size_t nmemb, std::string *output) {
    size_t ts = size * nmemb;
    output->append((char*)contents, ts);
    return ts;
}

void logError(const std::string& err) {
    std::cerr << "[ERROR] " << err << std::endl; 
}

void logInfo(const std::string& info) {
    std::cout << "[INFO] " << info << std::endl; 
}

bool hReq(const std::string& endpoint, const std::string& jd, std::string& response) {
    CURL *curl;
    CURLcode res;
    std::string url = "http://";
    if(ip.c_str() != nullptr) {
        url += ip;
    }
    else {
        logError("IP 환경 변수가 설정되지 않았습니다.");
        return false;
    }
    url += ":5000" + endpoint;

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();
    if(curl) {
        struct curl_slist *h = NULL;
        h = curl_slist_append(h, "Content-Type: application/json");

        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_POSTFIELDS, jd.c_str());
        curl_easy_setopt(curl, CURLOPT_HTTPHEADER, h);


        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, wcb);

        res = curl_easy_perform(curl);

        if(res != CURLE_OK) {
            logError("Request failed : " + std::string(curl_easy_strerror(res)));
            curl_easy_cleanup(curl);
            curl_slist_free_all(h);
            curl_global_cleanup();
            return false;
        }

        std::cout << "Res : " << response << std::endl;

        curl_easy_cleanup(curl);
        curl_slist_free_all(h);
    }
    curl_global_cleanup();
    return true;
}

void startSocket() {
    int sSocket, cSocket;
    struct sockaddr_in sAddr, cAddr;
    socklen_t addrSize = sizeof(cAddr);
    
    sSocket = socket(AF_INET, SOCK_STREAM, 0);
    if(sSocket < 0) {
        logError("Socket 생성 실패");
        return;
    }

    memset(&sAddr, 0, sizeof(sAddr));
    sAddr.sin_family = AF_INET;

    if(ip.c_str() != nullptr) {
        inet_pton(AF_INET, ip.c_str(), &sAddr.sin_addr);
    }
    else {
        logError("IP 환경 변수가 설정되지 않았습니다.");
        return;
    }

    sAddr.sin_port = htons(PORT);

    int opt = 1;
    setsockopt(sSocket, SOL_SOCKET, SO_REUSEADDR, &opt, sizeof(opt));
    if(bind(sSocket, (struct sockaddr*)&sAddr, sizeof(sAddr)) < 0) {
        logError("바인드 실패");
        close(sSocket);
        return;
    }


    if(listen(sSocket, 5) < 0) {
        logError("연결 대기 실패");
        close(sSocket);
        return;
    }

    logInfo("소켓이 " + std::string(ip) + ":" + std::to_string(PORT) + "에서 실행 중");

    while(true) {
        cSocket = accept(sSocket, (struct sockaddr*)&cAddr, &addrSize);
        if(cSocket < 0) {
            logError("수락 실패");
            continue;
        }

        char buffer[1024] = {0};
        ssize_t bytesReceived = recv(cSocket, buffer, sizeof(buffer) - 1, 0);
        if(bytesReceived <= 0) {
            logError("데이터 수신 실패");
            close(cSocket);
            continue;
        }

        buffer[bytesReceived] = '\0';
        std::string req(buffer);
        logInfo("[REQ] " + req);

        std::string command, username, email, password, code;
        std::istringstream iss(req);
        iss >> command;

        std::string endpoint;
        std::string flaskRes;
        std::string jd;

        if(command == "LOGIN") {
            iss >> email >> password;
            endpoint = "/login/attempt";
            jd = "{\"email\":\"" + email + "\", \"password\":\"" + password + "\"}";
        }
        else if(command == "DONE") {
            std::string confirm_password;
            iss >> username >> email >> password >> confirm_password;
            endpoint = "/register/done";
            jd = "{\"username\":\"" + username + "\", \"email\":\"" + email + "\" ,\"password\":\"" + password + "\",\"confirm_password\":\"" + confirm_password + "\"}";
        }
        else if(command == "CHECK-USERNAME") {
            iss >> username;
            endpoint = "/register/check-username";
            jd = "{\"username\":\"" + username + "\"}";
        } 
        else if(command == "SEND-EMAIL") {
            iss >> email;
            endpoint = "/register/send-email";
            jd = "{\"email\":\"" + email + "\"}";
        } 
        else if(command == "CHECK-CODE") {
            iss >> email >> code;
            endpoint = "/register/check-code";
            jd = "{\"email\":\"" + email + "\", \"code\":\"" + code + "\"}";
        }

        else {
            flaskRes = "INVALID COMMAND";
            send(cSocket, flaskRes.c_str(), flaskRes.size(), 0);
            close(cSocket);
            continue;
        }

        if(hReq(endpoint, jd, flaskRes)) {
            size_t totalSent = 0;
            while(totalSent < flaskRes.size()) {
                ssize_t sent = send(cSocket, flaskRes.c_str() + totalSent, flaskRes.size() - totalSent, 0);
                if(sent <= 0) {
                    logError("데이터 전송 실패");
                    break;
                }
                totalSent += sent;
            }
        }
        else {
            flaskRes = "ERROR";
            send(cSocket, flaskRes.c_str(), flaskRes.size(), 0);
        }

        close(cSocket);
    }
    close(sSocket);
}


int main() {
    startSocket();
    return 0;

}
