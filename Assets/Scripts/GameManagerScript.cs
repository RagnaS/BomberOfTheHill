//Braga Mickaël, Thomas Etcheberry - 2013
using UnityEngine;
using System.Collections;



public class GameManagerScript : MonoBehaviour
{
	
	
	public GameObject[] Player;
	public GameObject[] Territoires;
	public int[] score;
	public int nbteam;
	public int nbjoueurs; //nombres de joueur
	public int nbterr;
	public int team;
	public bool isMultiplayer;
	public string typePartie;
	public GenerateArena Terre;
	private bool gameFinished = false;
	public GameObject modelPlayer;
	public GameObject modelEnnemy;
	public GenerateArena arena;
	public AudioSource audioSource;
	private string typeMap;
	void Awake()
	{
		
	}
	[RPC]
	void Start()
	{
		
		nbterr = 5;
		Terre = GetComponent<GenerateArena>();
		nbteam = PlayerPrefs.GetInt("nbteam");
		nbjoueurs = PlayerPrefs.GetInt("nbjoueurs");
		score = new int[nbteam];
		Territoires = GameObject.FindGameObjectsWithTag("Territoire");
		Player = new GameObject[nbjoueurs];
		
		if (PlayerPrefs.GetInt("Type") == 1)
		{
			typePartie = "normal";
		}
		else if (PlayerPrefs.GetInt("Type") == 2)
		{
			typePartie = "territoire";
		}
		
		CreationJoueur();
		typeMap = PlayerPrefs.GetString("Level");
		
		switch(typeMap)
		{
		case "normal":
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("industryMusic") as AudioClip;
			audioSource.Play();
			break;
		case "fire":
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("fireMusic") as AudioClip;
			audioSource.Play();
			break;
		case "nature":
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("Nature") as AudioClip;
			audioSource.Play();
			break;
		case "ice":
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("iceMusic") as AudioClip;
			audioSource.Play();
			break;
		case "space":
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("spaceMusic") as AudioClip;
			audioSource.Play();
			break;
		}
	}
	
	
	[RPC]
	void Update()
	{
		
		Player = GameObject.FindGameObjectsWithTag("Player");
		CheckGameState();
		
		if(gameFinished)
		{
			Application.LoadLevel(10);
		}
		
		
	}
	
	void CheckGameState()
	{
		
		if (typePartie == "normal")
		{
			if (Player.Length < 2)
				gameFinished = true;
		}
		else
		{
			Checkscore();
		}
		
		
	}
	
	[RPC]
	void PlayerDead()
	{
		nbjoueurs -= 1;
	}
	
	
	[RPC]
	void CreationJoueur()
	{
		for (int i = 0; i < nbjoueurs; i++)
		{
			if(i == 0)
			{
				if(!isMultiplayer)
					Instantiate(modelPlayer, new Vector3(Random.Range(-Terre.sizeMap / 4, Terre.sizeMap / 4), 3, Random.Range(-Terre.sizeMap / 4, Terre.sizeMap / 4)), modelPlayer.transform.rotation);
			}
			else
			{
				if(!isMultiplayer)
					Instantiate(modelEnnemy, new Vector3(Random.Range(-Terre.sizeMap / 4, Terre.sizeMap / 4), 3, Random.Range(-Terre.sizeMap / 4, Terre.sizeMap / 4)), modelPlayer.transform.rotation);
			}
		}
	}
	[RPC]
	void Checkscore()
	{
		
		
		for(int i = 0; i < score.Length; i++)
		{
			score[i] = 0;
		}
		
		foreach( GameObject territ in Territoires)
		{
			team = territ.transform.GetChild(0).GetComponent<TerritoireScript>().Capturedteam;
			score[team] += 1;
		}
		
		for(int j = 0; j < score.Length; j++)
		{
			if(score[j] > Territoires.Length /2)
			{
				Application.LoadLevel(10);
			}
		}
	}
}
