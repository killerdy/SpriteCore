using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{

    
    public GameObject onlineConfig;
    public void Local()
    {
        SceneManager.LoadScene("Local");
    }
    public void Online()
    {
        string name = onlineConfig.transform.GetChild(0).GetComponent<TMP_InputField>().text;
        string ip= onlineConfig.transform.GetChild(1).GetComponent<TMP_InputField>().text;
        int port = int.Parse(onlineConfig.transform.GetChild(2).GetComponent<TMP_InputField>().text);
        PersistObject.instance.UpdateConfig(name, ip, port);
        SceneManager.LoadScene("Online");
    }
    public void OnlineConfig()
    {
        onlineConfig.transform.GetChild(1).GetComponent<TMP_InputField>().text=PersistObject.instance.IP;
        onlineConfig.transform.GetChild(2).GetComponent<TMP_InputField>().text = PersistObject.instance.Port.ToString();
        onlineConfig.SetActive(true);
    }
}
