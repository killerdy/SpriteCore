#pragma once 
#include<string>
#include<unordered_map>
#include<winsock2.h>
#include<ws2tcpip.h>
#include "Common.h"
#include "json.hpp"

void updateGameState(std::unordered_map<std::string,InputData>& gameState,std::unordered_map<std::string,GameObjectState>& playerState,std::string address);
json createGameStateJson(const std::unordered_map<std::string,GameObjectState>& playerState);