using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {

	private Producer producer;

	private ProvincePanel provincePanel;
	private CityPanel cityPanel;
	private InfraPanel infraPanel;
	private ArmedForcePanel armedForcePanel;
	private UnitsPanel unitsPanel;

	private Text turnValue;
	private Text playerValue;

	private void Awake () 
	{
		producer = GameObject.Find ("Producer").GetComponent<Producer> ();

		turnValue = GameObject.Find ("TurnValue").GetComponent<Text> ();
		playerValue = GameObject.Find ("PlayerValue").GetComponent<Text> ();

		provincePanel = GameObject.Find ("ProvincePanel").GetComponent<ProvincePanel> ();
		cityPanel = GameObject.Find ("CityPanel").GetComponent<CityPanel> ();
		infraPanel = GameObject.Find ("InfraPanel").GetComponent<InfraPanel> ();
		armedForcePanel = GameObject.Find ("ArmedForcePanel").GetComponent<ArmedForcePanel> ();
		unitsPanel = GameObject.Find ("UnitsPanel").GetComponent<UnitsPanel> ();

		provincePanel.gameObject.SetActive (false);
		cityPanel.gameObject.SetActive (false);
		infraPanel.gameObject.SetActive (false);
		armedForcePanel.gameObject.SetActive (false);
		unitsPanel.gameObject.SetActive (false);
	}

	private void Start ()
	{
		UpdateTurnPanel ();
	}
	
	//0 - province; 1 - city
	private int typeObject;
	private Province province;
	private City city;
	private ArmedForce armedForce;
	
	public void SelectedObject (int type, Object obj)
	{
		if (type == 0)
		{
			province = obj as LandProvince;
			provincePanel.gameObject.SetActive (true);
			provincePanel.ShowProvinceData ((LandProvince)province);
		}
		else if (type == 1)
		{
			city = obj as City;
			cityPanel.gameObject.SetActive (true);
			cityPanel.ShowCityData (city.GetCityData());
			infraPanel.gameObject.SetActive (true);
			infraPanel.ShowInfraData (city.GetCityData().infraData);
		}
		else if (type == 2)
		{
			armedForce = obj as ArmedForce;
			armedForcePanel.gameObject.SetActive (true);
			armedForcePanel.ShowArmyData(armedForce);
			unitsPanel.gameObject.SetActive (true);
			unitsPanel.ShowUnitsData ((Army)armedForce);
		}
		
		typeObject = type;
	}
	
	public void UnselectedObject ()
	{
		if (typeObject == 0)
		{
			province = null;
			provincePanel.gameObject.SetActive (false);
		}
		else if (typeObject == 1)
		{
			city = null;
			cityPanel.gameObject.SetActive (false);
			infraPanel.gameObject.SetActive (false);
		}
		else if (typeObject == 2)
		{
			armedForce = null;
			armedForcePanel.gameObject.SetActive(false);
			unitsPanel.gameObject.SetActive (false);
		}
	}

	public void NextTurn()
	{
		producer.NextTurn ();
		UpdateTurnPanel ();
		UpdateProvincePanel ();
		UpdateCityAndInfraPanel ();
		UpdateArmedForcePanel ();
	}

	public void UpdateTurnPanel ()
	{
		turnValue.text = "" + GlobalData.currentTurn;
		playerValue.text = "" + GlobalData.currentPlayerNumber;
	}

	public void UpdateProvincePanel ()
	{
		if (province != null)
		{
			provincePanel.ShowProvinceData ((LandProvince)province);
		}
	}

	public void UpdateCityAndInfraPanel ()
	{
		if (city != null)
		{
			cityPanel.ShowCityData (city.GetCityData());
			infraPanel.ShowInfraData (city.GetCityData().infraData);
		}
	}

	public void UpdateArmedForcePanel ()
	{
		if (armedForce != null)
		{
			armedForcePanel.ShowArmyData(armedForce);
			unitsPanel.ShowUnitsData ((Army)armedForce);
		}
	}
}
