using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitElementPanel : MonoBehaviour {

	public void ShowUnitData (Unit unit)
	{
		transform.GetChild (0).GetComponent<Text> ().text = unit.unitName;
		transform.GetChild (1).GetComponent<Text> ().text = "" + unit.unitCurrentHealthPoint;
		transform.GetChild (2).GetComponent<Text> ().text = "" + unit.unitAttack;
		transform.GetChild (3).GetComponent<Text> ().text = "" + unit.unitDefence;
	}
}
