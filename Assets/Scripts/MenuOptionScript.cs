//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class MenuOptionScript : MonoBehaviour {
	
	private int[] hauteur;
	private int[] largeur;
	private string fullscreen;
	private int choix;
	public int type;

	// Use this for initialization
	void Start () {

		hauteur = new int[5];
		largeur = new int[5];
		InitialiseTableau();
		choix = PlayerPrefs.GetInt("Resolution");
		fullscreen = PlayerPrefs.GetString("Window");
		if(fullscreen != "Oui" && fullscreen != "Non")
		{
			fullscreen = "Oui";
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if(type == 1)
		{
			this.guiText.text = "Resolution: " +largeur[PlayerPrefs.GetInt("Resolution")]+ "X" + hauteur[PlayerPrefs.GetInt("Resolution")];
		}
		else if(type == 2)
		{
			this.guiText.text = "Fullscreen :" + fullscreen;
		}



	}
	void OnMouseDown()
	{
		if(type == 1)
		{
			choix ++;
			if(choix == 5)
			{
				choix = 0;
			}
			PlayerPrefs.SetInt("Resolution", choix);
		}
		if(type == 2)
		{
			if(fullscreen == "Oui")
			{
				fullscreen = "Non";
				PlayerPrefs.SetString("Window", fullscreen);
			}
			else
			{
				fullscreen = "Oui";
				PlayerPrefs.SetString("Window", fullscreen);
			}
		}
		if(type == 3)
		{
			if(PlayerPrefs.GetString("Window") == "Oui")
			{
				Screen.SetResolution(largeur[PlayerPrefs.GetInt("Resolution")],hauteur[PlayerPrefs.GetInt("Resolution")],true);
			}
			else if (PlayerPrefs.GetString("Window") == "Non")
			{
				Screen.SetResolution(largeur[PlayerPrefs.GetInt("Resolution")],hauteur[PlayerPrefs.GetInt("Resolution")],false);
			}

		}
	}


	void InitialiseTableau()
	{
		largeur[0] = 640;
		hauteur[0] = 480;
		largeur[1] = 1024;
		hauteur[1] = 768;
		largeur[2] = 1280;
		hauteur[2] = 720;
		largeur[3] = 1600;
		hauteur[3] = 900;
		largeur[4] = 1920;
		hauteur[4] = 1080;
	}
}
