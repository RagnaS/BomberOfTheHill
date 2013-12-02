//Thomas Etcheberry - 2013
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	public Transform joueur;
	public string equipe = string.Empty;
	public bool estMort = true;
	public GameObject spawn;
	public GameObject chat;
	
	void OnGUI() {
		if(Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server)
		{
			if(equipe == string.Empty)
			{
				GUI.Box(new Rect(Screen.width/2-125,Screen.width/2-50,250,75),"Choisissez votre equipe");
				
				if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-15, 75,30), "Rouge"))
				{
					equipe = "rouge";
					chat.SendMessage("Equipe", equipe);
				}
				if(GUI.Button(new Rect(Screen.width/2+25, Screen.height/2-15, 75,30), "Bleu"))
				{
					equipe = "bleu";
					chat.SendMessage("Equipe", equipe);
				}
			}
		}
		if(equipe != string.Empty && estMort == true)
		{
			if(GUI.Button(new Rect(Screen.width/2-50, Screen.height/2-15, 100,30), "Apparaitre"))
			{
				var joueurSpawn = Network.Instantiate(joueur, spawn.transform.position, spawn.transform.rotation, 0);
				estMort = false;
			}
		}
	}
}
