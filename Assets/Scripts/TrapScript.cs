//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {


	private string name;
	private GameObject go;
	public bool isMultiplayer;

	// Use this for initialization
	[RPC]
	void Start () 
    {
		name = this.gameObject.transform.parent.name;
	}

	[RPC]
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (name)
            {
                case "Colle": go = other.gameObject;
                    		  go.GetComponent<PlayerScript>().Vitesse = 2f;
                    		  ;break;

                case "Pique": 	if(!isMultiplayer)
				{
									Destroy(other.gameObject); break;
				}
								else
				{
									Network.Destroy(other.gameObject); break;
				}

                case "Cube": go = other.gameObject;
                    		 go.GetComponent<PlayerScript>().Poser = false;
                    		 ;break;

                case "default": ; break;
            }
        }
	}

}
