using UnityEngine;
using System.Collections;

public class WaterProvince : Province {

	public override void SetInitialObjectPosition ()
	{
		Transform child = transform.GetChild (0);
		armedForcePosition = child.transform.position;
		Destroy (child.gameObject);
		
		//provinceData.provinceType = 1;
	}
	
	public override void FoundCity ()
	{
		/*provinceData.isFound = true;
		GameObject c = Instantiate(Resources.Load ("GameObjects/City", typeof(Object))) as GameObject;
		c.name = "New City " + Random.Range(1, 1000);
		c.transform.position = cityPosition;
		
		City ci = c.GetComponent<City> ();
		ci.SetInitialData (c.name, this, GlobalData.currentPlayer, provinceData.foodBonus, provinceData.productionBonus, 1, 1, 5, 5, 1);
		
		//gameGUI.SelectedObject (0, this);
		
		GameObject cities = GameObject.Find ("Cities");
		cities.GetComponent<Cities> ().AddCity (c);

		city = ci;*/
	}
	
	public override void CreateArmy ()
	{
		GameObject a = Instantiate(Resources.Load ("GameObjects/Army", typeof(Object))) as GameObject;
		a.name = "New Army " + Random.Range(1, 1000);
		a.transform.position = armedForcePosition;
		
		Army ar = a.GetComponent<Army> ();
		ar.SetInitialData (a.name, this, GlobalData.currentPlayer);
		
		GameObject armies = GameObject.Find ("Armies");
		armies.GetComponent<Armies> ().AddArmy (a);
		armedForce = ar;
	}

	public override void Select ()
	{
		gameObject.GetComponent<Renderer>().materials[0].color = colors[2];
		isSelect = true;			
	}
	
	public override void Unselect ()
	{
		gameObject.GetComponent<Renderer>().materials[0].color = colors[0];
		isSelect = false;
	}

	public override void FogOfWar ()
	{
		if (armedForce)
			armedForce.FogOfWar();
	}

}
