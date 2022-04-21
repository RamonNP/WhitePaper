using System.Security.Cryptography; 
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LocalPlayerController))]
[RequireComponent(typeof(ServerPlayerController))]
[System.Serializable]
public class TCPPlayerView : MonoBehaviour
{
    [Header("Client attributes")]
    [SerializeField] private string m_clientID;
    [SerializeField] private bool m_isLocal;

    public Transform Body { get; set; }

    private LocalPlayerController m_LocalPlayerController;
    private ServerPlayerController m_ServerPlayerController;

    private Movimentation m_Movimentation;

    #region Encapsulation
    public bool IsLocal {
        get {
            return m_isLocal;
        }

        set {
            m_isLocal = value;
        }
    }
    public string ClientID {
        get {
            return m_clientID;
        }

        set {
            m_clientID = value;
        }
    }
    public Movimentation Movimentation {
        get {
            return m_Movimentation;
        }

        set {
            m_Movimentation = value;
        }
    }
    #endregion

    private void Awake()
    {
        Body = transform;

        m_LocalPlayerController = GetComponent<LocalPlayerController>();
        m_ServerPlayerController = GetComponent<ServerPlayerController>();
        m_LocalPlayerController.enabled = false;
        m_ServerPlayerController.enabled = false;

        if (m_isLocal) Initialize("-1");
    }

    public void Initialize(string id)
    {
        m_clientID = id;

        Movimentation = new Movimentation { clientID = id ,position = new Position() };


        if (m_isLocal) m_LocalPlayerController.enabled = true;
        else m_ServerPlayerController.enabled = true;
    }

    public void SendDataToMessagController(TCPEvent m_Event)
    {
        TCPMessageController.OnRequestSendData(m_Event);
    }

    public void LocalPlayerSendPosition()
    {
        Movimentation.position.SetPosition(Body.position.x, Body.position.y);

        TCPEvent newEvent = new TCPEvent
        {
            meta = new Meta { mEvent = Events.movimentation.ToString() },
            resource = Movimentation
        };

        SendDataToMessagController(newEvent);
    }

    public void ReceiveData(JObject data)
    {
        Dictionary<string, string> dictObj = data.ToObject<Dictionary<string, string>>();
  
        if (!dictObj["position"].Equals(""))
        {
            string[] pos = dictObj["position"].Split(':');
            Movimentation.position.x = pos[0];
            Movimentation.position.y = pos[1];
        }
    }
}
