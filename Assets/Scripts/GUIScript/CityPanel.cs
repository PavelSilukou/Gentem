using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CityPanel : MonoBehaviour {

	private Text cityNameText;
	private Text cityFoodValue;
	private Text cityProdValue;
	private Text cityCitizensValue;
	
	private void Awake ()
	{
		cityNameText = GameObject.Find ("CityNameText").GetComponent<Text> ();
		cityFoodValue = GameObject.Find ("CityFoodValue").GetComponent<Text> ();
		cityProdValue = GameObject.Find ("CityProductionValue").GetComponent<Text> ();
		cityCitizensValue = GameObject.Find ("CityCitizensValue").GetComponent<Text> ();
	}
	
	public void ShowCityData (CityData cityData)
	{
		cityNameText.text = cityData.cityName;

		if (cityData.playerOwner == GlobalData.currentPlayer) 
		{
			cityData.CalculateAddResources ();

			cityFoodValue.text = String.Format("{0}({1}{2})", cityData.currentFood, Sign (Mathf.Sign(cityData.surplusesFood)), cityData.surplusesFood);
			cityProdValue.text = String.Format("{0}({1}{2})", cityData.currentProd, Sign (Mathf.Sign(cityData.surplusesProd)), cityData.surplusesProd);
			cityCitizensValue.text = "" + cityData.citizens;
		}
		else
		{
			cityFoodValue.text = "???";
			cityProdValue.text = "???";
			cityCitizensValue.text = "???";
		}


	}

	private string Sign (float sign)
	{
		if (sign == 1.0f)
			return "+";
		else return "-";
	}
}
