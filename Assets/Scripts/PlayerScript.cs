//Braga Mickaël - 2013
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	
	private bool up;
	private bool down;
	private bool left;
	private bool right;
	private string drction;
	private float vitesse = 4f;
	[SerializeField]
	private int teammulti;
	private GameObject bombe;
	private bool poser;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 direction;
	private bool isWalking;
	[SerializeField]
	private float Timer;
	[SerializeField]
	private Animation playerAnimation;
	[SerializeField]
	private Transform player;
	public int score;

	public bool invincible;
	public bool isMultiplayer;

	
	
	
	#region getter/setter
	
	public int Teammulti
	{
		get { return teammulti; }
		set { teammulti = value; }
	}
	public bool Poser
	{
		get { return poser; }
		set { poser = value; }
	}
	public RaycastHit Hit
	{
		get { return hit; }
		set { hit = value; }
	}
	
	public float Vitesse {
		get {
			return vitesse;
		}
		set{
			vitesse = value;
		}
	}
	
	public Ray Ray
	{
		get { return ray; }
		set { ray = value; }
	}
	public GameObject Bombe
	{
		get { return bombe; }
		set { bombe = value; }
	}
	public bool Up
	{
		get { return up; }
		set { up = value; }
	}
	public bool Down
	{
		get { return down; }
		set { down = value; }
	}
	public bool Left
	{
		get { return left; }
		set { left = value; }
	}
	public bool Right
	{
		get { return right; }
		set { right = value; }
	}
	
	#endregion
	
	
	
	[RPC]
	void Start ()
	{
		Up = false;
		Down = false;
		Left = false;
		Right = false;
		Ray = new Ray();
		Timer = 4f;
		Poser = true;
		isWalking = false;
		player = this.transform.GetChild(0);
		playerAnimation = this.transform.GetChild(0).animation;
		invincible = false;
		drction = "";
		score = 0;
		teammulti =1;
		
	}
	
	[RPC]
	void Update () 
	{
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Up = true;
			Down = false;
			Left = false;
			Right = false;
			isWalking = true;
			drction = "up";
		}
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			Up = false;
			isWalking = false;
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Up = false;
			Down = true;
			Left = false;
			Right = false;
			isWalking = true;
			drction = "down";
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			Down =false;
			isWalking = false;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Up = false;
			Down = false;
			Left = true;
			Right = false;
			isWalking = true;
			drction = "left";
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Left = false;
			isWalking = false;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Up = false;
			Down = false;
			Left = false;
			Right = true;
			isWalking = true;
			drction = "right";
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Right = false;
			isWalking = false;
		}
		
		
		if (Poser == true)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if(drction != "")
				{
					if (Detection(drction))
					{
						MettreBombe(drction);
					}
				}
			}
			
		}
		else
		{
			Bloque();
		}
		
		if(Vitesse !=4f)
		{
			timervitesse();
		}
		
		if(isWalking == true)
		{
			playerAnimation.Play("Marcher");
			playerAnimation.animation["Marcher"].speed = 2.0f;
		}
		
		if(isWalking == false)
		{
			playerAnimation.Play("EnAttente");
			player.animation["EnAttente"].speed = 0.5f;
		}
		
		if(invincible == true)
		{
			Invincible();
		}
		
	}

	[RPC]
	void FixedUpdate()
	{
		if (Left == true)
		{
			player.transform.localEulerAngles = new Vector3(0,90,0);
			this.transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
			
		}
		
		if (Right == true)
		{
			player.transform.localEulerAngles = new Vector3(0,270,0);
			this.transform.Translate(Vector3.back * vitesse * Time.deltaTime);
			
		}
		
		if (Down == true)
		{
			player.transform.localEulerAngles = new Vector3(0,0,0);
			this.transform.Translate(Vector3.left * vitesse * Time.deltaTime);
			
		}
		
		if (Up == true)
		{
			player.transform.localEulerAngles = new Vector3(0,180,0);
			this.transform.Translate(Vector3.right * vitesse * Time.deltaTime);
			
		}
		
		
	}

	[RPC]
	bool Detection(string Direction)
	{
		ray.origin = this.transform.position;
		
		if (Direction == "up")
		{ ray.direction = Vector3.forward; }
		
		if (Direction == "down")
		{ ray.direction = Vector3.back; }
		
		if (Direction == "left")
		{ ray.direction = Vector3.left; }
		
		if (Direction == "right")
		{ ray.direction = Vector3.right; }
		
		if (Physics.Raycast(ray, out hit, 2f))
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	[RPC]
	void MettreBombe(string Direction)
	{
		Vector3 position = new Vector3();

		if(!isMultiplayer)
		{
			Bombe = GameObject.Instantiate(Resources.Load("Bombe", typeof(GameObject))) as GameObject;
			if (Direction == "left")
			{ 
				
				bombe.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z + 1); 
			}
			
			if (Direction == "right")
			{ 
				
				bombe.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z - 1); 
			}
			
			if (Direction == "down")
			{
				
				bombe.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y - 0.5f, player.transform.position.z); 
			}
			
			if (Direction == "up")
			{ 
				
				bombe.transform.position = new Vector3(player.transform.position.x + 1 , player.transform.position.y - 0.5f, player.transform.position.z); 
			}
			
			bombe.SetActive(true);
		}
			
		else
		{

			
			if (Direction == "left")
			{ 
				
				position = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z + 1); 
			}
			
			if (Direction == "right")
			{ 
				
				position = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z - 1); 
			}
			
			if (Direction == "down")
			{
				
				position = new Vector3(player.transform.position.x - 1, player.transform.position.y - 0.5f, player.transform.position.z); 
			}
			
			if (Direction == "up")
			{ 
				
				position = new Vector3(player.transform.position.x + 1 , player.transform.position.y - 0.5f, player.transform.position.z); 
			}
			Network.Instantiate(Resources.Load("Bombe", typeof(GameObject)),position, gameObject.transform.rotation,0);
			bombe.SetActive(true);

		}
			
		
		
	}

	[RPC]
	void timervitesse()
	{	
		Timer -= Time.deltaTime;
		
		if(Timer <= 0)
		{
			Vitesse = 4f;
			Timer = 4f;
		}
		
	}
	[RPC]
	void Bloque()
	{
		Timer -= Time.deltaTime;
		
		
		if (Timer <= 0)
		{
			Poser = true;
			Timer = 4f;
		}
		
	}
	[RPC]
	void Invincible()
	{
		Timer -= Time.deltaTime;
		
		
		if (Timer <= 0)
		{
			invincible = false;
			Timer = 4f;
		}
		
	}
	
}
