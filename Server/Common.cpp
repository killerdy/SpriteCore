#include "Common.h"
json inputDataToJson(const  InputData& data){

    json j;
    j["horizontal"]=data.horizontal;
    j["vertical"]=data.vertical;
    return j;
}
json gameObjectStateToJson(const GameObjectState& data){
    json j;
    j["playerName"]=data.playerName;
    j["x"]=data.x;
    j["y"]=data.y;
    
    return j;
}
bool parseInputData(const json& j, InputData& input){
    try{
        input.horizontal=j["horizontal"].get<float>();
        input.vertical=j["vertical"].get<float>();
    }
    catch(const  json::type_error& e){
        return false;
    }
    return  true;
}

