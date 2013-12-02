//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class MenuPrincipalScript : MonoBehaviour {

	public int level;

	void OnMouseDown()
	{
		if(level < 10)
		{
			Application.LoadLevel(level);
		}
		else
		{
			Application.Quit();
		}
	}
}
