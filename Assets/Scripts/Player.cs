using UnityEngine;
using System.Collections;

public class Player {

	public bool isActive = false;

	public string playerName = "";
	public Color playerColor;
	
	public virtual void SetActive (bool state)
	{
		isActive = state;
	}

}
