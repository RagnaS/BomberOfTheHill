//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class MenuMultiplayerScript : MonoBehaviour {

	public int type;
	public string level;
	private int nbjoueurs;
	public int niv;
	[RPC]
	void Start()
	{
		PlayerPrefs.SetInt("nbjoueurs", 2);
		PlayerPrefs.SetString("Level", "");

		if(type == 1)
		{
			nbjoueurs = 2;
			this.guiText.text = "Nombre de joueurs : " + nbjoueurs;
		}
	}
	[RPC]
	void Update()
	{
		if(type == 1)
		{
			this.guiText.text = "Nombre de joueurs : " + nbjoueurs;
			PlayerPrefs.SetInt("nbteam",nbjoueurs);
		}
	}

	void OnMouseDown()
	{
		if(type==0)
		{
			PlayerPrefs.SetString("Level", level);

		}
		else if(type == 1)
		{
			if(nbjoueurs < 8)
			{
				nbjoueurs ++;
				PlayerPrefs.SetInt("nbjoueurs", nbjoueurs);
			}
			else if(nbjoueurs == 8)
			{
				nbjoueurs = 2;
				PlayerPrefs.SetInt("nbjoueurs", nbjoueurs);
			}
		
		}
		else if(type == 3)
		{
			if(PlayerPrefs.GetString("Level") != "")
			   {
					Application.LoadLevel(niv);
				}
		}

	}
}
