using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    NetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<NetworkManager>();
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
        SceneManager.LoadScene("OnlineScene");
        manager.StartHost();
    }
}
