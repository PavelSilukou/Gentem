using UnityEngine;
using System.Collections;

public class Cities : MonoBehaviour {

	public void GenerateCitiesGroup (int maxPlayers)
	{
		for (int i = 1; i <= maxPlayers; i++)
		{
			GameObject playerGroup = new GameObject();
			playerGroup.name = "citiesGroupPlayer" + i;
			playerGroup.transform.parent = gameObject.transform;
		}
	}

	public void NextTurn (int player)
	{
		GameObject playerGroup = GameObject.Find ("citiesGroupPlayer" + player);
		foreach (Transform child in playerGroup.transform)
		{
			child.SendMessage("NextTurn");
		}
	}

	public void AddCity (GameObject city)
	{
		GameObject playerGroup = GameObject.Find ("citiesGroupPlayer" + GlobalData.currentPlayerNumber);
		city.transform.parent = playerGroup.transform;
	}
}