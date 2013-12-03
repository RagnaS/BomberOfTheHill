using UnityEngine;
using System.Collections;

public class JoinServerScript : MonoBehaviour {

	public int port = 10123;
	public string ip = "192.168.1.42";
	private string pseudo = string.Empty;

	void OnMouseDown()
	{
		Debug.Log("clic detecté");
		Network.Connect(ip,port);
		Application.LoadLevel("Scene_Multi_Server");
	}

	void OnFailedToConnect()
	{
		Debug.Log("Impossible de se connecter a l'adresse IP:");
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Connexion au serveur réussie!");
	}
}
