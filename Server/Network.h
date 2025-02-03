#pragma once
#include<iostream>
#include<string>
#include<winsock2.h>
#include<ws2tcpip.h>
#include<vector>

#pragma comment(lib,"ws2_32.lib")

#define SERVER_PORT 8888
#define MAX_BUFFER_SIZE 1024

bool initWinsock(){
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);
    return true;
}
void closeWinsock(){
    WSACleanup();
}
bool sendTcpData(SOCKET sock,const std::string& data){
    send(sock,data.c_str(),data.size(),0);
    return true;
}
int receiveTcpData(SOCKET sock, std::string& data) {
    char buffer[MAX_BUFFER_SIZE];
    int bytesReceived = recv(sock, buffer, MAX_BUFFER_SIZE, 0);
    if (bytesReceived <= 0) return -1;
    data = std::string(buffer, bytesReceived);
    return bytesReceived;
}

SOCKET createTcpSocket(){
    return socket(AF_INET, SOCK_STREAM, 0);
}
void closeSocket(SOCKET sock) {
    closesocket(sock);
}