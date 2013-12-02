//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class TerritoireScript : MonoBehaviour {


    private float Timer;
    private GameObject go;
	private GameObject player;
    private Transform light;
	private bool booltimer;
	private int capturedteam;

	public int Capturedteam {
		get {
			return capturedteam;
		}
		set
		{
			capturedteam = value;
		}
	}

    // Use this for initialization
	[RPC]
    void Start()
    {
        go = this.gameObject.transform.parent.gameObject;
        Timer = 2.0f;
        light = go.transform.GetChild(1);
		booltimer =false;
		Capturedteam = 99;

    }

	[RPC]
    void Update()
    {
		if(booltimer == true)
		{
			Temps ();
		}
    }

	[RPC]
    void OnTriggerEnter(Collider other)
    {
    
       
            if (other.gameObject.CompareTag("Player"))
            {
					if(capturedteam != other.GetComponent<PlayerScript>().Teammulti)
					{				
						player = other.gameObject;
						booltimer = true;
					}
			}
	}
	[RPC]
	void Temps()
	{
		Timer -= Time.deltaTime;
		if (Timer <= 0)
		{
			capturedteam = player.GetComponent<PlayerScript>().Teammulti;
			light.light.color = Color.green;
			Timer = 2.0f;
			booltimer = false;
		}
	}
  
        
}
