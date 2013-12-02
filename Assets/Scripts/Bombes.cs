//Braga Mickaël- 2013
using UnityEngine;
using System.Collections;

public class Bombes : MonoBehaviour {
	
	private bool valid;
    private GameObject bombe;
    private float timer;
    private Ray ray;
	public GameObject particules;
	public bool isMultiplayer;

    public GameObject Bombe
    {
        get { return bombe; }
        set { bombe = value; }
 
    }

	// Use this for initialization
	void Start () 
    {

		valid = true;
        timer = 2f;
        ray = new Ray();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if(valid == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                Explosion();
            }
        }
        
	

       
	}

	[RPC]
	void Explosion()
	{
		if(!isMultiplayer)
		{
			Instantiate(particules, gameObject.transform.position, particules.transform.rotation);
        	ApplyRaycast();
			animation.Play("Explosion");
			Destroy(this.gameObject);
		}
		else
		{
			Network.Instantiate(particules, gameObject.transform.position, particules.transform.rotation,0);
			ApplyRaycast();
			animation.Play("Explosion");
			Network.Destroy(this.gameObject);
		}
		
		
	}

	[RPC]
    void ApplyRaycast()
    {
        ApplyRayhit(this.transform, Vector3.right);
        ApplyRayhit(this.transform, Vector3.forward);
        ApplyRayhit(this.transform, Vector3.back);
        ApplyRayhit(this.transform, Vector3.left);
        

    }

	[RPC]
    void ApplyRayhit(Transform origin, Vector3 direction)
    {
        ray.origin = origin.position;
        ray.direction = direction;

        RaycastHit hit;

        if (Physics.Raycast( ray, out hit, 3f))
        {
			if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.name == "Cube_destructibleFire(Clone)" || hit.collider.gameObject.name == "Cube_destructibleGlace(Clone)" || hit.collider.gameObject.name == "Cube_destructibleNature(Clone)" || hit.collider.gameObject.name == "Cube_destructibleNormal(Clone)" || hit.collider.gameObject.name == "Cube_destructibleSpace(Clone)"  )
            {


				if(hit.collider.gameObject.CompareTag("Player") && hit.collider.gameObject.transform.parent.gameObject.GetComponent<PlayerScript>().invincible == false )
				{
					if(!isMultiplayer)
						Destroy(hit.collider.gameObject.transform.parent.gameObject);
					else
						Network.Destroy(hit.collider.gameObject.transform.parent.gameObject);
				}
				else
				{
					if(!isMultiplayer)
                		Destroy(hit.collider.gameObject);
					else
						Network.Destroy(hit.collider.gameObject);
				}

            }

        }
     


    }
}
