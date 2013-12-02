//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class BonusScript : MonoBehaviour {

    private string name;
    private GameObject go;

    // Use this for initialization
    void Start()
    {
        name = this.gameObject.transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

	[RPC]
    void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.CompareTag("Player"))
        {
            switch (name)
            {
                case "Cube": go = other.gameObject;
                             go.GetComponent<PlayerScript>().Vitesse = 8f;
                             ;break;

                case "armure": go = other.gameObject;
								go.GetComponent<PlayerScript>().invincible = true; break;


                case "default": ; break;
            }
        }
 

    }
}
