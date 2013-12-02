using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smooth = 1.5f;
	
	private Transform player;// variable de récupération du transform du joueur pour obtenir sa position
	private Vector3 relCameraPos; //variable de position de la caméra par rapport au joueur
	private float relCameraPosMag; // variable de magnitude de la caméra
	private Vector3 newPos; // nouvelle position de la caméra
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform; 
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
		
	}
	
	void FixedUpdate()
	{
		Vector3 standardPos = player.position + relCameraPos; // variable de position standard 
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag; // Variable positionnant la caméra au dessus du joueur
		Vector3[] checkpoints = new Vector3[5]; // tableau permettant stockant les positions possibles de la caméra ou peut se trouver le joueur
		checkpoints[0] = standardPos;
		checkpoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkpoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkpoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
		checkpoints[4] = abovePos;
		
		for(int i = 0; i < checkpoints.Length; i++)
		{
			if(ViewingPosCheck(checkpoints[i])) //Verifie si le joueur est trouver dans les positions du tableau grace a un raycast
			{ 
				break;
			}
		}
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime); // change la position de la caméra pour voir le joueur
		SmoothLookAt();
	}
	
	bool ViewingPosCheck(Vector3 checkPos)
	{
		RaycastHit hit;
		
		if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
		{
			if(hit.transform != player)
			{
				return false;
			}
		}
		
		newPos = checkPos; // si le joueur a été trouver la nouvelle position est égale a la position vérifiée
		return true;
	}
	
	void SmoothLookAt()
	{
		Vector3 relPlayerPosition = player.position - transform.position;
		Quaternion lookatRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, lookatRotation, smooth * Time.deltaTime);
	}
}
