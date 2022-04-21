using UnityEngine;
using System;

using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Net;

[RequireComponent(typeof(TCPDAO))]
[RequireComponent(typeof(TCPMessageHelper))]
[RequireComponent(typeof(TCPMessageController))]
public class TCPConnection : MonoBehaviour
{
	private TCPMessageController m_TCPMessageController;

	#region private members 	
	private UdpClient socketConnection;
	private Thread clientReceiveThread;
	[SerializeField] private string serverURL;
	[SerializeField] private int serverPort;
	private string rsaKeyPub;

    #endregion
    // Use this for initialization 	

    private void Awake()
    {
		m_TCPMessageController = GetComponent<TCPMessageController>();

	}

    public void Start()
	{
		ConnectToTcpServer();
	}

	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	public void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData()
	{
		try
		{
			socketConnection = new UdpClient(serverPort);
			socketConnection.Connect(serverURL, serverPort);

			//IPEndPoint object will allow us to read datagrams sent from any source.
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

			Byte[] bytes = new Byte[1024];
			while (true)
			{
				// Blocks until a message returns on this socket from a remote host.
				Byte[] receiveBytes = socketConnection.Receive(ref RemoteIpEndPoint);
				string returnData = Encoding.ASCII.GetString(receiveBytes);

				Debug.Log($"Server message: {returnData}");
				m_TCPMessageController.ReceiveFromServer(returnData);			
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	public void SendData(TCPEvent mEvent)
	{
		if (socketConnection == null) return;

		try
		{
			byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(mEvent));
			socketConnection.Send(clientMessageAsByteArray, clientMessageAsByteArray.Length);
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	private void OnDestroy()
	{
		clientReceiveThread.Abort();
		socketConnection.Close();
	}
}


