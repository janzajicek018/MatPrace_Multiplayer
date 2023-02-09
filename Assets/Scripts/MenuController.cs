using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    NetworkManager manager;
    GameObject inpAddress;
    TMP_InputField inpField;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<NetworkManager>();
        inpAddress = GameObject.FindGameObjectWithTag("InpAddress");
        inpField = inpAddress.GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGame()
    {
         Application.Quit();
    }
    public void HostPlay()
    {
        manager.StartHost();
        SceneManager.LoadScene("OnlineScene");
    }
    public void Join()
    {
        manager.networkAddress = inpField.text;   
        manager.StartClient();
        SceneManager.LoadScene("OnlineScene");
    }
}
