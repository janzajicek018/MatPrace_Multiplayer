using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    /*public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }*/
}
