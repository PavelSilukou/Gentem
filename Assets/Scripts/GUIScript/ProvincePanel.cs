using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProvincePanel : MonoBehaviour {

	private GameGUI gui;

	private Text provinceNameText;
	private Text foodBonusValue;
	private Text productionBonusValue;

	private Button createArmyButton;

	private LandProvince landProvince;

	private void Awake ()
	{

		gui = GameObject.Find ("GameGUI").GetComponent<GameGUI> ();

		provinceNameText = GameObject.Find ("ProvinceNameText").GetComponent<Text> ();
		foodBonusValue = GameObject.Find ("ProvinceFoodBonusValue").GetComponent<Text> ();
		productionBonusValue = GameObject.Find ("ProvinceProductionBonusValue").GetComponent<Text> ();

		createArmyButton = GameObject.Find ("ProvinceCreateArmyButton").GetComponent<Button> ();

	}

	public void ShowProvinceData (LandProvince landProvince)
	{
		this.landProvince = landProvince;

		provinceNameText.text = landProvince.provinceName;
		foodBonusValue.text = "" + landProvince.foodBonus;
		productionBonusValue.text = "" + landProvince.productionBonus;

		createArmyButton.interactable = !landProvince.armedForce;
	}

	public void OnCreateArmy ()
	{
		landProvince.CreateArmy ();
		gui.UpdateProvincePanel ();
	}
}
