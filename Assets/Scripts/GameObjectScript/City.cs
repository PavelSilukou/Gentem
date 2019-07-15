using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

	private GameGUI gameGUI;

	public static Color[] colors = {Color.white, 
									new Color(0.16f, 0.352f, 0.796f), 
									new Color(0.035f, 0.0588f, 0.372f)};
	
	private bool isSelect = false;
	
	public CityData cityData = new CityData();

	public CityData GetCityData ()
	{
		if (GlobalData.currentPlayer == cityData.playerOwner)
		{
			return cityData;
		}
		else {
			CityData newCityData = new CityData();
			newCityData.cityName = cityData.cityName;
			return newCityData;
		}
	}

	public void SetInitialData (string cityName, Province provinceOwner,
	                            Player playerOwner, float foodBonus,
	                            float productionBonus, int foodLvl, int prodLvl,
	                            float currentFood, float currentProd, int citizens)
	{
		cityData.cityName = cityName;
		cityData.provinceOwner = provinceOwner;
		cityData.playerOwner = playerOwner;
		cityData.foodBonus = foodBonus;
		cityData.productionBonus = productionBonus;
		cityData.citizens = citizens;
		cityData.currentFood = currentFood;
		cityData.currentProd = currentProd;
		cityData.infraData.foodLvl = foodLvl;
		cityData.infraData.prodLvl = prodLvl;
		cityData.CalculateAddResources ();
	}
	
	private void Awake ()
	{
		gameGUI = GameObject.Find ("GameGUI").GetComponent<GameGUI>();
	}
	
	private void OnMouseEnter ()
	{
		if (!isSelect)
			gameObject.GetComponent<Renderer>().materials[1].color = colors[1];
	}
	
	private void OnMouseExit ()
	{
		if (!isSelect)
			gameObject.GetComponent<Renderer>().materials[1].color = colors[0];
	}
	
	public void Select ()
	{
		gameObject.GetComponent<Renderer>().materials[1].color = colors[2];
		isSelect = true;
		
		gameGUI.SelectedObject (1, this);
	}
	
	public void Unselect ()
	{
		gameObject.GetComponent<Renderer>().materials[1].color = colors[0];
		isSelect = false;
		
		gameGUI.UnselectedObject ();
	}
	
	public void Hide ()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
	}
	
	public void Show ()
	{
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
	}
	
	public void FogOfWar ()
	{
		
	}

	public void NextTurn ()
	{
		cityData.NextTurn ();
	}

}
