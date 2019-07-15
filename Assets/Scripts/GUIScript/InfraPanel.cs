using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfraPanel : MonoBehaviour {

	private GameGUI gui;

	private Text infraFoodValue;
	private Text infraProdValue;

	private Button infraFoodLevelUpButton;
	private Button infraProdLevelUpButton;

	private InfraData infraData;
	
	private void Awake ()
	{

		gui = GameObject.Find ("GameGUI").GetComponent<GameGUI> ();

		infraFoodValue = GameObject.Find ("InfraFoodValue").GetComponent<Text> ();
		infraProdValue = GameObject.Find ("InfraProdValue").GetComponent<Text> ();

		infraFoodLevelUpButton = GameObject.Find ("InfraFoodLevelUpButton").GetComponent<Button> ();
		infraProdLevelUpButton = GameObject.Find ("InfraProdLevelUpButton").GetComponent<Button> ();
	}
	
	public void ShowInfraData (InfraData infraData)
	{

		this.infraData = infraData;

		if (infraData.foodLvl != -1)
		{
			infraFoodValue.text = "" + infraData.foodLvl + " lvl";
			infraProdValue.text = "" + infraData.prodLvl + " lvl";
		}
		else
		{
			infraFoodValue.text = "???";
			infraProdValue.text = "???";
		}

		infraFoodLevelUpButton.interactable = infraData.isFoodUpAvailable();
		infraProdLevelUpButton.interactable = infraData.isProdUpAvailable();
	}

	public void FoodUp ()
	{
		infraData.FoodUp ();
		gui.UpdateCityAndInfraPanel ();
	}

	public void ProdUp ()
	{
		infraData.ProdUp ();
		gui.UpdateCityAndInfraPanel ();
	}
}
