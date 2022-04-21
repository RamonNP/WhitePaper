using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TCPMessageController : MonoBehaviour
{
    private TCPConnection m_TCPConnection;
    private TCPMessageHelper m_TCPMessageHelper;

    private float m_fixedTickDelay = 0.3f;
    private DateTime m_curentTickTimer;

    public static UnityAction<TCPEvent> OnRequestSendData;

    private void OnEnable()
    {
        OnRequestSendData += RequestSendMessage;
    }
    private void OnDisable()
    {
        OnRequestSendData -= RequestSendMessage;
    }
    private void Awake()
    {
        m_curentTickTimer = DateTime.UtcNow; ;

        m_TCPConnection = GetComponent<TCPConnection>();
        m_TCPMessageHelper = GetComponent<TCPMessageHelper>();
    }

    public void RequestSendMessage(TCPEvent data)
    {
        TimeSpan interval = DateTime.UtcNow - m_curentTickTimer;

        if (interval.Milliseconds > m_fixedTickDelay) return;
        Debug.Log(interval.Seconds);

        m_curentTickTimer = DateTime.UtcNow;
        //Debug.Log(JsonConvert.SerializeObject(data));

        m_TCPConnection.SendData(data);

        /*var ob = JObject.Parse(json);
        string[] pos = ((string)ob["m_PlayerPosition"]).Split(':');
        Debug.Log($"{pos[0]} eee {pos[1]}");*/
    }

    public void ReceiveFromServer(string data)
    {
        Debug.Log(data);
        string[] msgList = data.Split(',');
        m_TCPMessageHelper.UpdatePlayerData(msgList);

        //TCPMessageData[] message = JsonConvert.DeserializeObject<TCPMessageData[]>(data);
    }
}




