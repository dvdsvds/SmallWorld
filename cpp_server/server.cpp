#include <iostream>
#include <sstream>
#include <string>
#include <curl/curl.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <unistd.h>
#include <cstring>

#define PORT 8080

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

bool slreq(const std::string& username, const std::string& password, std::string& response) {
    CURL *curl;
    CURLcode res;
    std::string url = "http://192.168.50.246:5000/login";
    std::string jd = "{\"username\":\"" + username + "\", \"password\":\"" + password + "\"}";

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();
    if(curl) {
        struct curl_slist *h = NULL;
        h = curl_slist_append(h, "Content-Type: application/json");

        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_POSTFIELDS, jd.c_str());
        curl_easy_setopt(curl, CURLOPT_HTTPHEADER, h);

        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, wcb);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response);

        res = curl_easy_perform(curl);

        if(res != CURLE_OK) {
            std::cerr << "Request failed : " << curl_easy_strerror(res) << std::endl;
            curl_easy_cleanup(curl);
            return false;
        }

        std::cout << "Res : " << response << std::endl;

        curl_easy_cleanup(curl);
        curl_slist_free_all(h);
    }
    curl_global_cleanup();

    return true;
}

bool srreq(const std::string& username, const std::string& password) {
    CURL *curl;
    CURLcode res;

    std::string url = "http://192.168.50.246:5000/register";
    std::string jd = "{\"username\":\"" + username + "\", \"password\":\"" + password + "\"}";

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();
    if(curl) {
        struct curl_slist *h = NULL;
        h = curl_slist_append(h, "Content-Type: application/json");

        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_POSTFIELDS, jd.c_str());
        curl_easy_setopt(curl, CURLOPT_HTTPHEADER, h);

        std::string response;
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, wcb);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response);

        res = curl_easy_perform(curl);

        if(res != CURLE_OK) {
            std::cerr << "Request failed : " << curl_easy_strerror(res) << std::endl;
            curl_easy_cleanup(curl);
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
    inet_pton(AF_INET, "192.168.50.246", &sAddr.sin_addr);
    sAddr.sin_port = htons(PORT);

    if(bind(sSocket, (struct sockaddr*)&sAddr, sizeof(sAddr)) < 0) {
        logError("바인드 실패");
        return;
    }


    if(listen(sSocket, 5) < 0) {
        logError("연결 대기 실패");
        return;
    }

    logInfo("c++ 소켓이 192.168.50.246:" + std::to_string(PORT) + "에서 실행 중");

    while(true) {
        cSocket = accept(sSocket, (struct sockaddr*)&cAddr, &addrSize);
        if(cSocket < 0) {
            logError("수락 실패");
            continue;
        }

        char buffer[1024] = {0};
        recv(cSocket, buffer, 1024, 0);
        std::string req(buffer);
        logInfo("[LOGIN] " + req);

        std::string command, username, password;
        std::istringstream iss(req);
        iss >> command >> username >> password;

        std::string flaskRes;
        if(command == "LOGIN") {
            slreq(username, password, flaskRes);
        }
        else if(command == "REGISTER") {
            // todo
        }

        send(cSocket, flaskRes.c_str(), flaskRes.size(), 0);
        close(cSocket);
    }
    close(sSocket);
}


int main() {
    startSocket();
    return 0;
}
