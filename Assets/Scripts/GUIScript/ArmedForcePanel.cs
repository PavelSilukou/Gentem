using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArmedForcePanel : MonoBehaviour {
	
	private GameGUI gui;

	private Text armedForceNameText;
	private Text armedForceHealthValue;
	private Text armedForceAtackValue;
	private Text armedForceDefenceValue;

	private Button armedForceFoundCityButton;

	private ArmedForce armedForce;
	
	private void Awake ()
	{
		gui = GameObject.Find ("GameGUI").GetComponent<GameGUI> ();

		armedForceNameText = GameObject.Find ("ArmedForceNameText").GetComponent<Text> ();
		armedForceHealthValue = GameObject.Find ("ArmedForceHealthValue").GetComponent<Text> ();
		armedForceAtackValue = GameObject.Find ("ArmedForceAtackValue").GetComponent<Text> ();
		armedForceDefenceValue = GameObject.Find ("ArmedForceDefenceValue").GetComponent<Text> ();

		armedForceFoundCityButton = GameObject.Find ("ArmedForceFoundCityButton").GetComponent<Button> ();
	}
	
	public void ShowArmyData (ArmedForce armedForce)
	{

		this.armedForce = armedForce;

		armedForceNameText.text = armedForce.armedForceName;

		//if (armedForce.playerOwner == GlobalData.currentPlayer) 
		//{
			armedForceHealthValue.text = "" + armedForce.armedForceCurrentHealth;
			armedForceAtackValue.text = "" + armedForce.GetAtack();
			armedForceDefenceValue.text = "" + armedForce.GetDefence();
			if (armedForce.provinceOwner is LandProvince)
			{
				LandProvince landProvince = (LandProvince)armedForce.provinceOwner;
				armedForceFoundCityButton.interactable = !landProvince.city;
			}
		//}
		//else
		//{
		//	armedForceHealthValue.text = "???";
		//	armedForceAtackValue.text = "???";
		//	armedForceDefenceValue.text = "???";
		//	armedForceFoundCityButton.enabled = false;
		//}
	
	}

	public void FoundCity ()
	{
		armedForce.FoundCity ();
		gui.UpdateArmedForcePanel ();
	}

}
