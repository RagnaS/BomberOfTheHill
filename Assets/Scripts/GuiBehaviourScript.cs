//Braga Mickael - 2013
using UnityEngine;
using System.Collections;

public class GuiBehaviourScript : MonoBehaviour {


	private Color EntryColor;
	private Color ExitColor;
	private int EntrySize;
	private int ExitSize;

	void Start () {

		EntryColor = Color.red;
		ExitColor = Color.black;
		EntrySize = 22;
		ExitSize = 20;
	}

	void OnMouseEnter()
	{
		this.guiText.color = EntryColor;
		this.guiText.fontSize = EntrySize;
	}

	void OnMouseExit()
	{
		this.guiText.color = ExitColor;
		this.guiText.fontSize = ExitSize;
	}
}
