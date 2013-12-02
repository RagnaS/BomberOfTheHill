//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class CreationPartieScript : MonoBehaviour {
	
	private string mode;
	public int type;
	private int nbteam;
	private int choix;
	public GameObject nb;
	
	[RPC]
	void Start () {
		if(type == 1)
		{
			PlayerPrefs.SetInt("Mode", 1);
			mode = "Battle Royale";
			choix = 1;
			this.guiText.text = "Mode : " +mode;
		}
		if(type == 2)
		{
			PlayerPrefs.SetInt("nbteam", 2);
			nbteam = 2;
			this.gameObject.SetActive(false);
		}
		
	}
	
	[RPC]
	void Update () {
		
		
		if(type == 1)
		{
			this.guiText.text = "Mode : " +mode;
		}
		
		if(type ==2)
		{
			this.guiText.text = "Nombre d'équipes : " + nbteam;
			
			if(this.gameObject.activeSelf == false)
			{
				PlayerPrefs.SetInt("nbteam", 2);
				nbteam = 2;
			}
		}
		
	}
	
	[RPC]
	void OnMouseDown()
	{
		if(type == 1)
		{
			if(mode == "Battle Royale")
			{
				mode = "Team Battle";
				choix = 2;
				nb.SetActive(true);
			}
			else
			{
				mode = "Battle Royale";
				choix = 1;
				nb.SetActive(false);
			}
			
			PlayerPrefs.SetInt("Mode", choix);
		}
		
		if(type == 2)
		{
			if(nbteam == 2)
			{
				nbteam = 3;
				PlayerPrefs.SetInt("nbteam", nbteam);
			}
			else if(nbteam == 3)
			{
				nbteam = 2;
				PlayerPrefs.SetInt("nbteam", nbteam);
			}
		}
	}
}
