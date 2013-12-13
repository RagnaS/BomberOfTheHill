//Thomas Etchberry - 2013
using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {
	
	private string[] ligneChat = new string[4];
	private string texteChat = string.Empty;
	private string pseudoJoueur = string.Empty;
	private string texteModifie = string.Empty;
	
	void Start()
	{
		ligneChat[3] = string.Empty;
		ligneChat[2] = string.Empty;
		ligneChat[1] = string.Empty;
		ligneChat[0] = string.Empty;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && texteChat.Length!=0)
		{
			texteModifie = pseudoJoueur + ": "+ texteChat;
			networkView.RPC("RafraichirChat", RPCMode.All, texteModifie);
			texteChat = string.Empty;
		}

	}
	void OnGUI()
	{	
		//Si on est client ou serveur on pourra afficher le chat
		if(Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server)
		{
		
			texteChat = GUI.TextField(new Rect(0, Screen.height-20, 200, 20), texteChat);
			
			/* CHAT */
			GUI.Label(new Rect(0, Screen.height-100,400,20), ligneChat[3]);
			GUI.Label(new Rect(0, Screen.height-80,400,20), ligneChat[2]);
			GUI.Label(new Rect(0, Screen.height-60,400,20), ligneChat[1]);
			GUI.Label(new Rect(0, Screen.height-40,400,20), ligneChat[0]);
			
			/* FIN DU CHAT */
			
			if(GUI.Button(new Rect(200, Screen.height-20, 100, 20), "Envoyer") && texteChat.Length!=0)
			{
				texteModifie = pseudoJoueur + ": "+ texteChat;
				//Va appeler RafraichirChat pour tous joueurs connectés
				networkView.RPC("RafraichirChat", RPCMode.All, texteModifie);
				texteChat = string.Empty;
			}
		}
	}
	
	//Met a jour le texte du chat pour ne pas depasser le nombre de ligne max
	[RPC]
	void RafraichirChat(string texte)
	{
		ligneChat[3] = ligneChat[2];
		ligneChat[2] = ligneChat[1];
		ligneChat[1] = ligneChat[0];
		ligneChat[0] = texte;
	}
	
	void Connected(string nom)
	{
		pseudoJoueur = nom;
		nom+= " s'est connecté";
		networkView.RPC("RafraichirChat", RPCMode.All, nom);
	}
	
	void Equipe(string color)
	{
		var texteEquipe = pseudoJoueur + " a rejoint l'equipe "+color;
		networkView.RPC("RafraichirChat", RPCMode.All, texteEquipe);
	}
}
