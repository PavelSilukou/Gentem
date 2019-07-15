using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class ScriptForEditor : MonoBehaviour {

	private struct RegData {
		public GameObject owner;
		public HashSet<GameObject> neighbors;
	};

	private static RegData[] provinces;
	
	private static void FindNeighbors (GameObject map) {

		int countOfProvinces = map.transform.childCount;
		provinces = new RegData[countOfProvinces];
		for (int currChild = 0; currChild < countOfProvinces; currChild++) {
			
			Transform child = map.transform.GetChild(currChild);
			provinces[currChild].owner = child.gameObject;
			provinces[currChild].neighbors = new HashSet<GameObject>();

			Vector3[] arrVect1 = child.GetComponent<MeshFilter>().sharedMesh.vertices;
			foreach (Transform child2 in map.transform) {
				if (child != child2) {
					Vector3[] arrVect2 = child2.GetComponent<MeshFilter>().sharedMesh.vertices;
					foreach (Vector3 v1 in arrVect1) {
						foreach (Vector3 v2 in arrVect2) {
							if (Vector3.Distance(child.transform.TransformPoint(v1), child2.transform.TransformPoint(v2)) < 0.01f) {
								provinces[currChild].neighbors.Add(child2.gameObject);
							}
						}
					}
				}
			}
		}
	}

	[MenuItem ("GameMenu/Create Texture")]
	private static void CreateFileWithNeighbors ()
	{
		GameObject map = Instantiate (Resources.Load ("GameObjects/Map", typeof(GameObject))) as GameObject;

		FindNeighbors (map);

		using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/TextFiles/neighbors.txt", false))
		{
			foreach (RegData set in provinces) 
			{
				writer.Write (set.owner.name + " ");
				foreach (GameObject neighbor in set.neighbors)
				{
					writer.Write (neighbor.gameObject.name + " ");
				}
				writer.WriteLine();
			}
		}

		DestroyImmediate (map);
	}
}
