//Thomas Etcheberry - 2013
using UnityEngine;
using System.Collections;

public class GenerateArena : MonoBehaviour {

	public int size; //1 = très petite, 2 = petite , 3 = moyenne, 4 = grande
	public int sizeMap; // Taille effective de la map dans la scene
	public string typeMap; //"normal", "fire", "ice", "nature"
	public GameObject ground;
	public GameObject indestructibleNormalCube;
	public GameObject destructibleNormalCube;
	public GameObject indestructibleFireCube;
	public GameObject destructibleFireCube;
	public GameObject indestructibleIceCube;
	public GameObject destructibleIceCube;
	public GameObject destructibleNatureCube;
	public GameObject indestructibleNatureCube;
	public GameObject destructibleSpaceCube;
	public GameObject indestructibleSpaceCube;
	public Texture2D groundNature;
	public Texture2D groundNormal;
	public Texture2D groundFire;
	public Texture2D groundIce;
	public Texture2D groundSpace;
	private GameObject actualDestructibleCube;
	private GameObject actualIndestructibleCube;
    public GameObject territory;

	private float demiCube = 0.5f;
	private int random;
	public int destructibleChance = 30; // Chance qu'une case comporte un cube destructible
	public bool isMultiplayer; // Si la partie en multijoueurs en réseau
    public bool isTerritory; // Si la partie est en mode conquete de territoire
    public bool territoryExist=false;
    private float timerZone;

	[RPC]
	void Start(){

		typeMap = PlayerPrefs.GetString("Level");

		switch(typeMap)
		{
		case "normal":
			actualDestructibleCube = destructibleNormalCube;
			actualIndestructibleCube = indestructibleNormalCube;
			ground.renderer.material.SetTexture("_MainTex", groundNormal);
			break;
		case "fire":
			actualDestructibleCube = destructibleFireCube;
			actualIndestructibleCube = indestructibleFireCube;
			ground.renderer.material.SetTexture("_MainTex", groundFire);
			break;
		case "nature":
			actualDestructibleCube = destructibleNatureCube;
			actualIndestructibleCube = indestructibleNatureCube;
			ground.renderer.material.SetTexture("_MainTex", groundNature);
			break;
		case "ice":
			actualDestructibleCube = destructibleIceCube;
			actualIndestructibleCube = indestructibleIceCube;
			ground.renderer.material.SetTexture("_MainTex", groundIce);
			break;
		case "space":
			actualDestructibleCube = destructibleSpaceCube;
			actualIndestructibleCube = indestructibleSpaceCube;
			ground.renderer.material.SetTexture("_MainTex", groundSpace);
			break;
		}

		ground.renderer.material.SetTextureScale("_MainTex", new Vector2(5, 5));
		ground.transform.localScale = new Vector3(size, size, size);
		if(!isMultiplayer)
			Instantiate(ground, Vector3.zero, ground.transform.rotation);
		else
			Network.Instantiate(ground, Vector3.zero, ground.transform.rotation,0);

		switch(size)
		{
		case 1:
			sizeMap = 10;
			break;
		case 2:
			sizeMap = 20;
			break;
		case 3:
			sizeMap = 30;
			break;
		case 4:
			sizeMap = 40;
			break;
		}

		for(int i = -sizeMap/2; i<=sizeMap/2; i++)
		{
			if(!isMultiplayer)
			{
				Instantiate(actualIndestructibleCube,new Vector3(i,demiCube,-sizeMap/2), actualIndestructibleCube.transform.rotation);
				Instantiate(actualIndestructibleCube,new Vector3(i,demiCube,sizeMap/2), actualIndestructibleCube.transform.rotation);
				Instantiate(actualIndestructibleCube,new Vector3(-sizeMap/2,demiCube,i), actualIndestructibleCube.transform.rotation);
				Instantiate(actualIndestructibleCube,new Vector3(sizeMap/2,demiCube,i), actualIndestructibleCube.transform.rotation);
			}
			else
			{
				Network.Instantiate(actualIndestructibleCube,new Vector3(i,demiCube,-sizeMap/2), actualIndestructibleCube.transform.rotation,0);
				Network.Instantiate(actualIndestructibleCube,new Vector3(i,demiCube,sizeMap/2), actualIndestructibleCube.transform.rotation,0);
				Network.Instantiate(actualIndestructibleCube,new Vector3(-sizeMap/2,demiCube,i), actualIndestructibleCube.transform.rotation,0);
				Network.Instantiate(actualIndestructibleCube,new Vector3(sizeMap/2,demiCube,i), actualIndestructibleCube.transform.rotation,0);
			}
		}

		for(int j = -sizeMap/2; j <= sizeMap/2 ; j++)
		{
			for(int k = -sizeMap/2; k <= sizeMap/2 ; k++)
			{
				if(j!=-sizeMap/2 && j!=sizeMap/2 && k!=-sizeMap/2 && k!=sizeMap/2 && j%2 == 0 && k%2 == 0)
				{
					if(!isMultiplayer)
						Instantiate(actualIndestructibleCube,new Vector3(j, demiCube, k), actualIndestructibleCube.transform.rotation);
					else
						Network.Instantiate(actualIndestructibleCube,new Vector3(j, demiCube, k), actualIndestructibleCube.transform.rotation,0);
				}
				else if(j!=-sizeMap/2 && j!=sizeMap/2 && k!=-sizeMap/2 && k!=sizeMap/2)
				{
					random = Random.Range(0,100);
					if(random<destructibleChance)
					{
						if(!isMultiplayer)
							Instantiate(actualDestructibleCube,new Vector3(j, demiCube, k), actualDestructibleCube.transform.rotation);
						else
							Network.Instantiate(actualDestructibleCube,new Vector3(j, demiCube, k), actualDestructibleCube.transform.rotation,0);
					}
				}
			}
		}
	}

    [RPC]
    void Update()
    {
        if (isTerritory)
        {
            if (!isMultiplayer)
            {
                if (!territoryExist)
                {
                    Instantiate(territory, new Vector3(Random.Range(-sizeMap / 2, sizeMap / 2), 0, Random.Range(-sizeMap / 2, sizeMap / 2)), territory.transform.rotation);
                    timerZone = 0;
                }
                else
                {
                    if (timerZone < 20)
                    {
                        timerZone += Time.deltaTime;
                    }
                    else
                    {
                        Destroy(territory);
                        territoryExist = false;
                    }
                }
            }
            else
            {
                if (!territoryExist)
                {
                    Network.Instantiate(territory, new Vector3(Random.Range(-sizeMap / 2, sizeMap / 2), 0, Random.Range(-sizeMap / 2, sizeMap / 2)), territory.transform.rotation,0);
                    timerZone = 0;
                }
                else
                {
                    if (timerZone < 20)
                    {
                        timerZone += Time.deltaTime;
                    }
                    else
                    {
                        Network.Destroy(territory);
                        territoryExist = false;
                    }
                }
            }
        }
    }
}
