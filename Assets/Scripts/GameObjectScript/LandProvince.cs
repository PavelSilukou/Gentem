using UnityEngine;
using System.Collections;

public class LandProvince : Province {

	public float foodBonus = 0.0f;
	public float productionBonus = 0.0f;

	public City city = null;
	public Vector3 cityPosition;

	public override void SetInitialObjectPosition ()
	{
		Transform child = transform.GetChild (1);
		cityPosition = child.transform.position;
		Destroy (child.gameObject);
		child = transform.GetChild (0);
		armedForcePosition = child.transform.position;
		Destroy (child.gameObject);
	}

	public void SetInitialBonuses (float foodBonus, float productionBonus)
	{
		this.foodBonus = foodBonus;
		this.productionBonus = productionBonus;
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
		ar.GetComponent<Renderer>().materials [0].color = GlobalData.currentPlayer.playerColor;

		ar.units.Add(new Unit("unit1", "", 1, 200, 30, 40));
		ar.units.Add(new Unit("unit2", "", 2, 500, 60, 80));
		ar.units.Add(new Unit("unit3", "", 3, 1000, 120, 260));
		
		GameObject armies = GameObject.Find ("Armies");
		armies.GetComponent<Armies> ().AddArmy (a);
		armedForce = ar;
	}

	public override void Select ()
	{
		gameObject.GetComponent<Renderer>().materials[0].color = colors[2];
		isSelect = true;
		
		gameGUI.SelectedObject (0, this);
	}
	
	public override void Unselect ()
	{
		gameObject.GetComponent<Renderer>().materials[0].color = colors[0];
		isSelect = false;
		
		gameGUI.UnselectedObject ();
	}

	public override void FogOfWar ()
	{
		if (city)
			city.FogOfWar();
		if (armedForce)
			armedForce.FogOfWar();
	}

}
