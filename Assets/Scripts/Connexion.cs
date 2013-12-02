/*using UnityEngine;
using System.Collections;

public class Connexion : MonoBehaviour {
	
	public string matchIP;
	public string matchPort;
	public int maxPlayer = 10;
	public int port = 2310;
	
	void OnGUI ()
	{
		GUI.Label(new Rect(5,10,200,20),"Adresse IP du serveur");
		matchIP = GUI.TextField(new Rect(150,10,100,20), matchIP);
		
		GUI.Label(new Rect(5,30,200,20),"Port du serveur");
		matchPort = GUI.TextField(new Rect(150,30,100,20), matchPort);
		
		if(GUI.Button(new Rect(50,50,100,25),"Se connecter") && matchIP.Length !=0 && matchPort.Length!=0)
		{
			Network.Connect(matchIP, matchPort);
		}
		
		if(GUI.Button(new Rect(300,50,150,25),"Creer un serveur"))
		{
			bool useNat = !Network.HavePublicAddress();
			Network.InitializeServer(maxPlayer, port,useNat);
		}
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Serveur créer");
	}
	
	void OnFailedToConnect()
	{
		Debug.Log("Impossible de se connecter a l'adresse IP:"+matchIP);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Connexion au serveur réussie!");
	}
	
	void OnPlayerConnected()
	{
		Debug.Log("Un joueur a rejoint le serveur");
	}
}*/
//Thomas Etcheberry - 2013
using UnityEngine;
using System.Collections;

public class Connexion : MonoBehaviour {
	
	public string matchIP = "127.0.0.1"; //127.0.0.1 est l'adresse locale universelle.
	public string matchPort="8000";
	public int maxClients=32;
	//GUI
	public int labelW=80;
	public int fieldW=100;
	public int elementsH=35;
	public int fieldX=80;
	private string pseudo = string.Empty;
	public GameObject chat;
	public GameObject arena;
	
	void Awake()
	{
		// Evite de supprimer le gameobject -> pour le chargement du niveau
		DontDestroyOnLoad(gameObject);	
	}
	
	void OnGUI(){
		//Regarde si on est déconnecté, le serveur, un client connecté, ou bien en train de se connecter
		switch(Network.peerType){
			//ce qui est affiché lorsqu'on démarre le jeu, ou lorsqu'on est déconnecté
			case NetworkPeerType.Disconnected :
				Disconnected_GUI();
			break;
			//ce qui est affiché lorsqu'on est connecté au serveur en tant que Client		
			case NetworkPeerType.Client :
				Client_GUI();
			break;
			//ce qui est affiché lorsqu'on est celui qui a initialisé le serveur		
			case NetworkPeerType.Server :
				Server_GUI();
			break;
			//ce qui est affiché lorsqu'on est entrain de se connecter au serveur.		
			case NetworkPeerType.Connecting :
				Connecting_GUI();
			break;
		}
	}
	
	
	void Disconnected_GUI(){
		//IP
		GUI.Label(new Rect(10,10,labelW,elementsH),"Server IP");
		matchIP=GUI.TextField(new Rect(fieldX,10,fieldW,elementsH),matchIP);
		
		//Port
		GUI.Label(new Rect(10,50,labelW,elementsH),"Server Port");		
		matchPort=GUI.TextField(new Rect(fieldX,50,fieldW,elementsH),matchPort);
		
		//Pseudo
		GUI.Label(new Rect(10,90,labelW,elementsH),"Pseudo");		
		pseudo=GUI.TextField(new Rect(fieldX,90,fieldW,elementsH),pseudo);
		
		//conversion de string en int pour les éléments nécessaires
		int connectPort = int.Parse(matchPort);
		
		//Bouton de connexion
		if(GUI.Button(new Rect(10,130,150,30),"Connect to server") && matchIP.Length != 0 && pseudo.Length != 0)
		{
			Network.Connect(matchIP,connectPort);
			Debug.Log("Port: "+connectPort);
			Debug.Log("IP: "+matchIP);
			/******chargement du niveau*****/
			//Application.LoadLevel("Jeu");
		}
		
		//Bouton d'initialisation du serveur
		if(GUI.Button(new Rect(10,160,150,30),"Start a server") && pseudo.Length != 0)
		{
			bool useNat = !Network.HavePublicAddress();
			Network.InitializeServer(maxClients, connectPort, useNat);
			Debug.Log("Port: "+connectPort);
			arena.SendMessage("Generate");
			/******chargement du niveau*****/
			//Application.LoadLevel("Jeu");

		}
	}
	
	void Client_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Connecte au serveur :"+matchIP);
	}
	
	
	void Server_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Nombre de clients : "+Network.connections.Length);
	}
	
	void Connecting_GUI(){
		GUI.Label(new Rect(10,10,500,100),"Connexion au serveur...");
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Serveur créer");
		chat.SendMessage("Connected",pseudo);
	}
	
	void OnFailedToConnect()
	{
		Debug.Log("Impossible de se connecter a l'adresse IP:"+matchIP);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Connexion au serveur réussie!");
		chat.SendMessage("Connected",pseudo);
	}
	
	void OnPlayerConnected()
	{
		Debug.Log("Un joueur a rejoint le serveur");
	}
}
