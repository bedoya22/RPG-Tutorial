using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Transports;
using MLAPI.ServerList.Client;
public class ServerInformation : NetworkBehaviour
{
    public Text serverName, ServerStatus;
    public ServerModel serverModel;
    private void Start()
    {
        ServerConnection advertConnection = new ServerConnection();

        // Connect
        advertConnection.Connect("127.0.0.1", 7777).AsyncWaitHandle.WaitOne();

        // Create server data
        Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "Players", 50 },
                        { "Name", "This is the name" }
                    };

        // Register server
    }

}