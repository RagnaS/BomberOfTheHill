using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float timeOut = 1.5f;
	public bool isMultiplayer;
	// Use this for initialization
	[RPC]
	void Awake()
	{
		Invoke("DestroyNow", timeOut);
	}

	[RPC]
	void DestroyNow()
	{
		if(!isMultiplayer)
			Destroy(gameObject);
		else
			Network.Destroy(gameObject);
	}
}
