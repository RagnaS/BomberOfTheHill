//Braga Mickael - 2013

using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	private SphereCollider coll;
	public bool isMultiplayer;

	[RPC]
	void Start()
	{
		coll = this.gameObject.GetComponent<SphereCollider>();
	}

	[RPC]
	void Update()
	{
		coll.radius += Time.deltaTime /2;
	}

	[RPC]
	void OnTriggerEnter(Collider other)
	{
			if (other.gameObject.CompareTag("Player") || other.gameObject.name == "Cube_destructibleFire(Clone)" || other.gameObject.name == "Cube_destructibleGlace(Clone)" || other.gameObject.name == "Cube_destructibleNature(Clone)" || other.gameObject.name == "Cube_destructibleNormal(Clone)" || other.gameObject.name == "Cube_destructibleSpace(Clone)"  )
			{

				if(other.gameObject.CompareTag("Player")  )
				{
					if(!isMultiplayer)
						Destroy(other.gameObject);
					else
						Network.Destroy(other.gameObject);
				}
				else
				{
					if(!isMultiplayer)
						Destroy(other.gameObject);
					else
						Network.Destroy(other.gameObject);
				}
				
			}

		Debug.Log("Test");

	}
}
