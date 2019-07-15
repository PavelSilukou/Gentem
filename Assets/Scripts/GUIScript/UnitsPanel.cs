using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitsPanel : MonoBehaviour {

	//private GameGUI gui;

	//private GameObject unitElementPanel;

	private UnitElementPanel[] unitElementPanels;

	private void Awake ()
	{
		unitElementPanels = new UnitElementPanel[transform.childCount];
		//unitElementPanel = GameObject.Find ("UnitElementPanel");
		for (int i = 0; i < transform.childCount; i++)
		{
			UnitElementPanel unitElementPanel = transform.GetChild(i).GetComponent<UnitElementPanel>();
			unitElementPanels[i] = unitElementPanel;
			unitElementPanel.gameObject.SetActive(false);
		}
	}

	public void ShowUnitsData (Army army)
	{

		foreach (UnitElementPanel panel in unitElementPanels)
		{
			panel.gameObject.SetActive (false);
		}

		int unitsCount = army.units.Count;

		//float anchMinY = 0.7f;
		//float anchMaxY = 0.8f;

		for (int i = 0; i < unitsCount; i++)
		{
			unitElementPanels[i].gameObject.SetActive (true);
			unitElementPanels[i].ShowUnitData ((Unit)army.units[i]);
			/*GameObject panel = Instantiate(unitElementPanel) as GameObject;
			panel.transform.parent = this.transform;
			panel.name = "UnitElementPanel" + (i+1);
			panel.GetComponent<RectTransform>().anchorMin = new Vector2 (0.0f, anchMinY);
			panel.GetComponent<RectTransform>().anchorMax = new Vector2 (1.0f, anchMaxY);
			panel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
			panel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			panel.GetComponent<UnitElementPanel>().ShowUnitData((Unit)army.units[i]);
			anchMinY -= 0.15f;
			anchMaxY -= 0.15f;*/
		}
	}
}
