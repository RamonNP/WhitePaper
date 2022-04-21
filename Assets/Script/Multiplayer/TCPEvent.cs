using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Events { movimentation }

public class TCPEvent
{
    public Meta meta { get; set; }
    public Resource resource { get; set; }
}


/*[System.Serializable]
public class TCPMessageData
{
    [SerializeField] private string m_PlayerID;

    public TCPMessageData(string id)
    {
        PlayerID = id;
    }

    public string PlayerID {
        get {
            return m_PlayerID;
        }

        set {
            m_PlayerID = value;
        }
    }
}*/



