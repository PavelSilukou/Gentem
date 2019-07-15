using UnityEngine;
using System.Collections;

public class Armies : MonoBehaviour {

	public void GenerateArmiesGroup (int maxPlayers)
	{
		for (int i = 1; i <= maxPlayers; i++)
		{
			GameObject playerGroup = new GameObject();
			playerGroup.name = "armiesGroupPlayer" + i;
			playerGroup.transform.parent = gameObject.transform;
		}
	}

	public void AddArmy (GameObject army)
	{
		GameObject playerGroup = GameObject.Find ("armiesGroupPlayer" + GlobalData.currentPlayerNumber);
		army.transform.parent = playerGroup.transform;
	}
}
