//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class MenuSoloScript : MonoBehaviour {
	private int nbjoueurs;
	private string partie;

	public int type;
	public string level;
	public int niv;


	void Start()
	{
		PlayerPrefs.SetInt("nbjoueurs", 2);
		PlayerPrefs.SetString("Level", "");
		PlayerPrefs.SetInt("Type", 1);
		partie = "Classique";

		if(type == 1)
		{
			nbjoueurs = 2;
			this.guiText.text = "Nombre de joueurs : " + nbjoueurs;
		}
	}

	void Update()
	{
		if(type == 1)
		{
			this.guiText.text = "Nombre de joueurs : " + nbjoueurs;
			PlayerPrefs.SetInt("nbteam",nbjoueurs);
		}
		if(type == 4)
		{
			this.guiText.text = "Type de partie : " + partie;
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
		else if(type == 4)
		{
			if(partie == "Classique")
			{
				partie = "Prise de territoire";
				PlayerPrefs.SetInt("Type", 2);
			}
			else if(partie == "Prise de territoire")
			{
				partie = "Classique";
				PlayerPrefs.SetInt("Type", 1);
			}
		}

	}
}
