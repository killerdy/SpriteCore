#include "GameLogic.h"
void updateGameState(std::unordered_map<std::string,InputData>& gameState,std::unordered_map<std::string,GameObjectState>& playerState,std::string address){
    if(!gameState.count(address)||!playerState.count(address))
    return;
    playerState[address].x+=gameState[address].horizontal;
    playerState[address].y+=gameState[address].vertical;

    if(abs(playerState[address].x)>9.0)
    playerState[address].x*=9.0/abs(playerState[address].x);
    if(abs(playerState[address].y)>5.0)
    playerState[address].y*=5.0/abs(playerState[address].y);
    gameState[address].horizontal=0;
    gameState[address].vertical=0;
}
json createGameStateJson(const std::unordered_map<std::string,GameObjectState>& playerState){
    // json gameStateJson;
    json clientState;
    for(const auto& pair :playerState){
        
        clientState[pair.first]=gameObjectStateToJson(pair.second);
        // gameStateJson.push_back(clientState);
    }
    return clientState;
}