using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerController : MonoBehaviour
{
    private Transform m_Body = null;

    private Vector2 targetPosition;

    private void Start()
    {
        m_Body = transform;
    }

    public void UpdatePosition(string pos)
    {
        string[] posList = pos.Split(':');
        targetPosition = new Vector3(float.Parse(posList[0]), float.Parse(posList[1]), 0);
    }

    private void Update()
    {
        m_Body.position = Vector3.MoveTowards(m_Body.position, targetPosition, 5 * Time.deltaTime);
    }
}
