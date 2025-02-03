#include <unordered_map>
#include "Common.h"
#include "Network.h"
#include "json.hpp"
#include "GameLogic.h"
#include <iostream>
#include <vector>
#include <thread>
#include <chrono>
#include <sstream>
#include <mutex>
using json = nlohmann::json;

#define FRAME_RATE 30
#define FRAME_TIEM (1.0f / FRAME_RATE)

// namespace std {
// template <>
// struct hash<sockaddr_in> {
//   size_t operator()(const sockaddr_in& addr) const {
//     size_t h = 0;
//     h ^= std::hash<short>()(addr.sin_family);
//     h ^= std::hash<unsigned short>()(addr.sin_port);
//     h ^= std::hash<unsigned long>()(addr.sin_addr.S_un.S_addr);
//     return h;
//   }
// };

// template <>
// struct equal_to<sockaddr_in> {
//   bool operator()(const sockaddr_in& a, const sockaddr_in& b) const {
//     return a.sin_family == b.sin_family && a.sin_port == b.sin_port &&
//            a.sin_addr.S_un.S_addr == b.sin_addr.S_un.S_addr;
//   }
// };
// }

std::unordered_map<std::string, SOCKET> clients;
std::unordered_map<std::string, InputData> gameState;
std::unordered_map<std::string,GameObjectState> playerState;
std::mutex gameStateMutex;



void handleClient(SOCKET clientSocket, sockaddr_in clientAddr)
{
    std::string id=std::string(inet_ntoa(clientAddr.sin_addr))+":"+std::to_string(ntohs(clientAddr.sin_port));
    std::cout<<"Client connected: "<<inet_ntoa(clientAddr.sin_addr)<<":"<<ntohs(clientAddr.sin_port)<<std::endl;
    clients[id]=clientSocket;
    {
        std::lock_guard<std::mutex> lock(gameStateMutex);
        playerState[id]={"dy",0,0};
    }
    while(true){
        std::string data;
        int bytesReceived=receiveTcpData(clientSocket,data);
        if(bytesReceived<=0)break;
        try{
            json j=json::parse(data);
            std::cout<<j<<std::endl;
            InputData inputData;
            if(j.contains("horizontal")&&j.contains("vertical")&&parseInputData(j,inputData)){
                std::lock_guard<std::mutex> lock(gameStateMutex);
                gameState[id]=inputData;
            }
            if(j.contains("playerName")){
                playerState[id].playerName=j["playerName"].get<std::string>();
            }
            // if(j.contains(""))
        }
        catch(const json::parse_error& e){
            std::cerr<<" Error parsing json: "<<e.what()<<std::endl;
        }
    }
    std::cout<<"Client disconnected: "<<inet_ntoa(clientAddr.sin_addr) << ":" << ntohs(clientAddr.sin_port) << std::endl;
    {
        std::lock_guard<std::mutex> lock(gameStateMutex);
        gameState.erase(id);
        playerState.erase(id);

    }
    closesocket(clientSocket);
    clients.erase(id);
}

void gameLoop()
{
    while(true){
        auto startTime=std::chrono::high_resolution_clock::now();
        {
            std::lock_guard<std::mutex> lock(gameStateMutex);
            if(gameState.empty()) continue;
            for(const  auto& pair:gameState){
                updateGameState(gameState,playerState,pair.first);
                // json clientState;
                // clientState["address"]=pair.first;
                // clientState["input"]=inputDataToJson(pair.second);
                // gameStateJson.push_back(clientState);
            }
        }
        json gameStateJson=createGameStateJson(playerState);
        std::string jsonString =gameStateJson.dump();
        // std::cout<<"Send data :"<<jsonString<<std::endl;
        for(const auto&pair:clients){
            sendTcpData(pair.second,jsonString);
        }
        auto endTime=std::chrono::high_resolution_clock::now();
        auto elapsedTime=std::chrono::duration_cast<std::chrono::duration<float>>(endTime-startTime).count();
        auto sleepTime=std::max<float>(0.0f,FRAME_TIEM-elapsedTime);
        std::this_thread::sleep_for(std::chrono::duration<float>(sleepTime));
    }
}
int main()
{
    initWinsock();
    SOCKET serverSocket = createTcpSocket();
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(SERVER_PORT);
    serverAddr.sin_addr.s_addr = INADDR_ANY;
    bind(serverSocket, (sockaddr *)&serverAddr, sizeof(serverAddr));
    listen(serverSocket, SOMAXCONN);
    std::cout << "server listening on port " << SERVER_PORT << std::endl;
    std::thread gameThread(gameLoop);
    while (true)
    {

        sockaddr_in clientAddr;
        int clientAddrSize = sizeof(clientAddr);
        SOCKET clientSocket = accept(serverSocket, (sockaddr *)&clientAddr, &clientAddrSize);
        if (clientSocket != INVALID_SOCKET)
        {
            std::thread clientThread(handleClient,clientSocket,clientAddr);
            clientThread.detach();
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(1));

    }
    closeSocket(serverSocket);
    closeWinsock();
}
/*
g++ main.cpp GameLogic.cpp Common.cpp -o server -lws2_32
*/