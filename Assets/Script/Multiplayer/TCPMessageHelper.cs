using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPMessageHelper : MonoBehaviour
{
    private TCPDAO m_TCPDAO;

    private void Awake()
    {
        m_TCPDAO = GetComponent<TCPDAO>();
    }

    public void UpdatePlayerData(string[] msgList)
    {
        foreach(string m in msgList)
        {
            JObject jData = JObject.Parse(m);
            string playerID = (string)jData["clientID"];

            if (jData.ContainsKey("clientID"))
                m_TCPDAO.PlayerList[playerID].ReceiveData(jData);
            else AddNewPlayer(false);
        }        
    }

    public void AddNewPlayer(bool isLocal)
    {
        Transform m_PlayerInGame = Instantiate(m_TCPDAO.PlayerPrefab, m_TCPDAO.Spawn.position, Quaternion.identity);

        TCPPlayerView view = m_PlayerInGame.GetComponent<TCPPlayerView>();
        view.IsLocal = isLocal;
        m_TCPDAO.PlayerList.Add(view.ClientID, view);
    }

    public void RemovePlayerData(TCPEvent data)
    {
        if (m_TCPDAO.PlayerList.ContainsKey(data.resource.clientID))
        {
            m_TCPDAO.PlayerList.Remove(data.resource.clientID);
            Destroy(m_TCPDAO.PlayerList[data.resource.clientID].gameObject);

        }
    }

}
