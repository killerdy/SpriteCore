#pragma once 
#include<string>
#include<unordered_map>
#include<vector>
#include "json.hpp"
using json=nlohmann::json;
struct InputData{
    float horizontal;
    float vertical;
};
struct GameObjectState{
    std::string playerName;
    float x;
    float y;
};

json inputDataToJson(const  InputData& data);
json gameObjectStateToJson(const GameObjectState& data);
bool parseInputData(const json& j, InputData& input);
