using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	private ArrayList provinces = new ArrayList();

	public void GenerateMap (int maxPlayers)
	{
		Instantiate(Resources.Load ("GameObjects/ExternalBorder", typeof(Object)));

		foreach (Transform child in transform)
		{
			if (child.transform.childCount == 2)
			{
				LandProvince landProvince = child.gameObject.AddComponent<LandProvince>();
				landProvince.SetInitialBonuses(2.0F, 3.0F);
			}
			else
			{
				child.gameObject.AddComponent<WaterProvince>();
			}

			Province province = child.GetComponent<Province>();

			province.SetInitialObjectPosition();

			province.Hide();

			provinces.Add(province);
		}

		Province[] neighbors;
		string line;
		using (StreamReader reader = new StreamReader(Application.dataPath + "/Resources/TextFiles/neighbors.txt"))
		{
			while ((line = reader.ReadLine()) != null)
			{
				string[] words = line.Split(' ');
				neighbors = new Province[words.Length - 2];

				Province owner = GameObject.Find(words[0]).GetComponent<Province>();
				for (int i = 0; i < words.Length - 2; i++)
				{
					neighbors[i] = GameObject.Find(words[i+1]).GetComponent<Province>();
				}
				owner.SetNeighbors (neighbors);
			}
		}

	}

	public Province GetRandomLandProvince ()
	{
		while (true)
		{
			Province province = (Province)provinces [Random.Range (0, provinces.Count)];
			if (province is LandProvince)
				return province;
			else
				continue;
		}
	}

}
