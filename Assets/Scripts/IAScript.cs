using UnityEngine;
using System.Collections;

public class IAScript : MonoBehaviour {
	// Use this for initialization
	float time = 1.5f;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(time>=1.5)
		{
			var a = Random.Range(0,3);
			switch(a)
			{
			case 0:
				transform.position += Vector3.forward;
				break;
			case 1:
				transform.position += Vector3.back;
				break;
			case 2:
				transform.position += Vector3.left;
				break;
			case 3:
				transform.position += Vector3.right;
				break;
			}
			time = 0;
		}
		else
		{
			time += Time.deltaTime;
		}
	}
}
