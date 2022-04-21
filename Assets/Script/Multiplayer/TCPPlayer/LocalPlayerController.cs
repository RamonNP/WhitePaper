using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerController : MonoBehaviour
{
    private TCPPlayerView m_TcpPlayerView;
    private Transform m_Body;

    private void Start()
    {
        m_Body = transform;
        m_TcpPlayerView = GetComponent<TCPPlayerView>();
    }

    void Update()
    {
        Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        if (pos != Vector3.zero)
            PlayerMovement(pos);
    }

    private void PlayerMovement(Vector3 dir)
    {
        m_Body.position += dir.normalized * 3 * Time.deltaTime;
        m_TcpPlayerView.LocalPlayerSendPosition();
    }

}
