using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyManager : NetworkManager
{

    //When this function is called, it calls the starthost function from the networkmanager
    public void StartGame()
    {
        NetworkManager.singleton.StartHost();
    }

    //When called, it joins any hosted game on the local network
    public void JoinGame()
    {
        NetworkManager.singleton.StartClient();
    }
}
