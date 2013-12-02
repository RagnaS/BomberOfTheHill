//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class FindePartieScript : MonoBehaviour {

	public float timer;

	void Start () {

		timer = 4f;
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer < 0 )
			{
				Application.LoadLevel(0);
			}
	
	}
}
