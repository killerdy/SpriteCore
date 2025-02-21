using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Playables;
using TMPro; // 需要安装 Newtonsoft.Json 包

public class NetWorkClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;
    private string serverIP = "10.11.10.11";
    private int serverPort = 4000;
    private bool isConnected = false;

    public float moveInterval = 0.2f;
    public float moveTime;
    public struct GameState
    {
        public string playerName;
        public float x;
        public float y;
    }
    public struct PlayerState
    {
        public string playerName;
    }
    public struct InputState
    {
        public int horizontal;
        public int vertical;
    }
    public Dictionary<string, GameState> gameState = new Dictionary<string, GameState>();
    //public  List<string> playerState = new List<string>();


    public GameObject playerPrefab;
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> playerNames = new Dictionary<string, GameObject>();
    public bool flag;
    private void Awake()
    {
        if (PersistObject.instance != null)
        {
            serverIP = PersistObject.instance.IP;
            serverPort = PersistObject.instance.Port;
        }

        //instance = this;
    }
    void Start()
    {
        playerPrefab = Resources.Load<GameObject>("Online/Slime");
        ConnectToServer();
        SendNewName();
    }

    void Update()
    {
        if (isConnected)
        {
            HandleInput();
            UpdateGameState();
        }
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            isConnected = true;
            Debug.Log("Connected to server!");
            //HandleInput();
            SendInput(new InputState { horizontal = 0, vertical = 0 });
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("Socket Exception: " + e.Message);
            isConnected = false;
        }
    }

    void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;
        while (isConnected)
        {
            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string receivedJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    UpdateGameState(receivedJson);
                    flag = true;
                    //MainThreadDispatcher.Enqueue(() =>
                    //{
                    //    UpdateGameState();
                    //});
                    //UpdateGameState();
                }
            }
            catch (System.IO.IOException)
            {
                isConnected = false;
            }
        }
    }
    void UpdateGameState(string json)
    {
        //Debug.Log(json);
        try
        {
            //Debug.Log(json);
            //List<string> jsonObjects = new List<string>();
            int braceCount = 0;
            int startIndex = 0;
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == '{')
                    braceCount++;
                else if (json[i] == '}')
                    braceCount--;
                if (braceCount == 0)
                {
                    string jsonObject = json.Substring(startIndex, i - startIndex + 1);
                    gameState = JsonConvert.DeserializeObject<Dictionary<string, GameState>>(jsonObject);
                    startIndex = i + 1;
                }
            }
            //gameState = JsonConvert.DeserializeObject<Dictionary<string, GameState>>(json);
        }
        catch (JsonSerializationException e)
        {
            Debug.Log("Error parsing json: " + e.Message);
        }
    }
    public void UpdateGameState()
    {
        if (!flag) return;
        foreach (var entry in gameState)
        {
            string playerName = entry.Value.playerName;
            int x = int.Parse(entry.Value.x.ToString());
            int y = int.Parse(entry.Value.y.ToString());
            string address = entry.Key;
            if (!players.ContainsKey(address))
            {
                GameObject newPlayer = Instantiate(playerPrefab);
                newPlayer.transform.position = new Vector3(x, y, 0);
                players[address] = newPlayer;
                Debug.Log("Create new player" + address);
                GameObject newName = Instantiate(Resources.Load<GameObject>("Online/PlayerName"));
                newName.GetComponent<PlayerName>().SetPlayer(newPlayer.transform);
                //newName.GetComponent<PlayerName>().playerName = playerName;
                //newName.GetComponent<TextMeshProUGUI>().text = playerName;
                playerNames[address] = newName;

            }
            //更新玩家的位置和状态
            GameObject player = players[address];
            GameObject playerNameObject = playerNames[address];
            if (playerNameObject != null && playerNameObject.GetComponent<TextMeshProUGUI>().text != playerName)
                playerNameObject.GetComponent<TextMeshProUGUI>().text = playerName;
            if (player != null)
            {

                OnlinePlayerController controller = player.GetComponent<OnlinePlayerController>();

                //if (controller.isMove) return;
                controller.startPosition = player.transform.position;
                controller.endPosition = new Vector3(x, y, 0);
                controller.AddMove();
                //Vector3 startPosition= player.transform.position;
                //Vector3 endPosition= player.transform.position+new Vector3(x,y,0); 
                //player.transform.position=Vector3.Lerp(startPosition,)
            }
        }

        if (gameState.Count != players.Count)
        {
            List<string> keyToRemove = new List<string>();
            foreach (var entry in players)
            {
                if (!gameState.ContainsKey(entry.Key))
                    keyToRemove.Add(entry.Key);
            }
            foreach (string key in keyToRemove)
            {
                if (players[key] != null)
                    Destroy(players[key]);
                if (playerNames[key] != null)
                    Destroy(playerNames[key]);
                players.Remove(key);
                playerNames.Remove(key);
            }
            //foreach(var entry in gameState)
            //{
            //    if(!players.ContainsKey(entry.Key)) {
            //}
        }
        flag = false;

    }

    void HandleInput()
    {
        //Dictionary<string, object> inputData = new Dictionary<string, object>();
        // 获取 WASD 输入
        int horizontal = 0;
        int vertical = 0;
        moveTime += Time.deltaTime;
        if (moveTime < moveInterval)
            return;
        if (Input.GetKey(KeyCode.W))
        {

            moveTime = 0;
            vertical = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveTime = 0;
            vertical = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveTime = 0;
            horizontal = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveTime = 0;
            horizontal = 1;
        }
        if (vertical == 0 && horizontal == 0) return;
        SendInput(new InputState { horizontal = horizontal, vertical = vertical });
        //inputData["horizontal"] = horizontal;
        //inputData["vertical"] = vertical;
        //string jsonInput = JsonConvert.SerializeObject(inputData);
        ////jsonInput = ("{" +$"\"{}\"").ToString();
        //byte[] data = Encoding.UTF8.GetBytes(jsonInput);
        //try
        //{
        //    stream.Write(data, 0, data.Length);
        //}
        //catch (System.IO.IOException)
        //{
        //    isConnected = false;
        //}
    }
    void SendNewName()
    {
        if (PersistObject.instance != null)
        {
            string jsonInput = JsonConvert.SerializeObject(new PlayerState { playerName = PersistObject.instance.Name });
            byte[] data = Encoding.UTF8.GetBytes(jsonInput);
            stream.Write(data, 0, data.Length);
        }
    }
    void SendInput(InputState inputState)
    {
        string jsonInput = JsonConvert.SerializeObject(inputState);
        byte[] data = Encoding.UTF8.GetBytes(jsonInput);
        try
        {
            stream.Write(data, 0, data.Length);
        }
        catch (System.IO.IOException)
        {
            isConnected = false;
        }
    }

    void OnApplicationQuit()
    {
        isConnected = false;
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
    }
}