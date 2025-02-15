#include <iostream>
#include <string>
#include <curl/curl.h>

size_t wcb(void *contents, size_t size, size_t nmemb, std::string *output) {
    size_t ts = size * nmemb;
    output->append((char*)contents, ts);
    return ts;
}

bool slreq(const std::string& username, const std::string& password) {
    CURL *curl;
    CURLcode res;

    std::string url = "http://127.0.0.1:5000/login";
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

bool srreq(const std::string& username, const std::string& password) {
    CURL *curl;
    CURLcode res;

    std::string url = "http://127.0.0.1:5000/register";
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

int main() {
    while(true) {
        std::string command;
        std::string username, password;
        std::cout << "What do you want to do?" << std::endl;
        std::cout << "]> ";
        std::getline(std::cin, command);

        if(command == "l") {
            std::cout << "Username > ";
            std::getline(std::cin, username);

            std::cout << "Password > ";
            std::getline(std::cin, password);

            std::cout << "로그인 요청 : ";
            slreq(username, password);
        }
        else if(command == "r") {
            std::cout << "Username > ";
            std::getline(std::cin, username);

            std::cout << "Password > ";
            std::getline(std::cin, password);

            std::cout << "회원가입 요청 : ";
            srreq(username, password);
        }



    }
    return 0;
}
