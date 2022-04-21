using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPDAO : MonoBehaviour
{
    private Dictionary<string, TCPPlayerView> m_PlayerList;

    public Transform PlayerPrefab { get; private set; }
    public Transform Spawn { get; private set; }

    public Dictionary<string, TCPPlayerView> PlayerList {
        get {
            return m_PlayerList;
        }

        set {
            m_PlayerList = value;
        }
    }



}
