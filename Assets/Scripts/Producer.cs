using UnityEngine;
using System.Collections;

public class Producer : MonoBehaviour
{
	private Map map;
	private Cities cities;
	private Armies armies;
	
	public void Awake ()
	{
		GlobalData.Init (2);

		map = Instantiate (Resources.Load ("GameObjects/Map", typeof(Map))) as Map;
		map.name = "Map";
		map.GenerateMap (GlobalData.maxPlayers);
		
		cities = GameObject.Find ("Cities").GetComponent<Cities>();
		cities.SendMessage ("GenerateCitiesGroup", GlobalData.maxPlayers);

		armies = GameObject.Find ("Armies").GetComponent<Armies>();
		armies.SendMessage ("GenerateArmiesGroup", GlobalData.maxPlayers);

		Province province = map.GetRandomLandProvince ();
		Province[] provinceNeighbors = province.GetNeighbors ();
		foreach (Province reg in provinceNeighbors)
		{
			reg.Show();
		}
		province.Show ();

		GameObject.Find ("Main Camera").GetComponent<CameraMove> ().TranslateCamera (province.transform.position);
	}
	
	public void NextTurn ()
	{
		cities.NextTurn (GlobalData.currentPlayerNumber);
		GlobalData.NextTurn ();
	}
	
}
