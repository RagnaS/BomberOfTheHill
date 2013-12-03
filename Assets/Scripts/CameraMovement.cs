//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smooth = 1.5f;

	public Transform player;// variable de récupération du transform du joueur pour obtenir sa position
	private Vector3 newPos; // nouvelle position de la caméra
	
	void Start()
	{
		player =  this.transform.parent.transform;

	}



	void FixedUpdate()
	{
		newPos = new Vector3(player.position.x  , 21,  player.position.z);
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime); // change la position de la caméra pour voir le joueur
	}

	void LateUpdate()
	{
		transform.eulerAngles = new Vector3(80, 114, 23);
	}

}
