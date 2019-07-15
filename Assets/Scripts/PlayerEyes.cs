using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class PlayerEyes : MonoBehaviour {

	private GameObject selectedObject;

	private bool isArmedForce = false;
	private Province[] neighborsOfProvince;
	
	private void Update ()
	{

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (!Physics.Raycast(ray, out hit, 100))
			{
				return;
			}

			if (selectedObject)
				selectedObject.SendMessage("Unselect");
			selectedObject = hit.collider.gameObject;
			selectedObject.SendMessage("Select");

			if (selectedObject.GetComponent<ArmedForce>())
			{
				isArmedForce = true;
				neighborsOfProvince = selectedObject.GetComponent<ArmedForce>().GetNeighborsOfProvince();
			}
			else
			{
				isArmedForce = false;
				neighborsOfProvince = null;
			}

		}

		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject() && isArmedForce)
		{
			if (selectedObject.GetComponent<ArmedForce>().playerOwner == GlobalData.currentPlayer)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					//if (hit.collider.gameObject.GetComponent<Province>().GetProvinceData().provinceType == 0)
					//{
						if (neighborsOfProvince.Contains(hit.collider.gameObject.GetComponent<Province>()))
						{
							if (!hit.collider.gameObject.GetComponent<Province>().armedForce)
							{
								var armedForce = Utilities.GetInheritedComponent<ArmedForce>(selectedObject);
								armedForce.MoveArmedForce(hit.collider.gameObject);
								//selectedObject.GetComponent<ArmedForce>().MoveArmedForce (hit.collider.gameObject);
								//neighborsOfProvince = selectedObject.GetComponent<ArmedForce>().GetNeighborsOfProvince();
								neighborsOfProvince = armedForce.GetNeighborsOfProvince();
							}
						}
						else if (hit.collider.gameObject.GetComponent<ArmedForce>() && neighborsOfProvince.Contains(hit.collider.gameObject.GetComponent<ArmedForce>().provinceOwner))
						{
							if (hit.collider.gameObject.GetComponent<ArmedForce>().playerOwner != GlobalData.currentPlayer)
							{
								/*int atack = selectedObject.GetComponent<ArmedForce>().GetAtack();
								int defence = hit.collider.gameObject.GetComponent<ArmedForce>().GetDefence();

								int deathArmedForce2 = hit.collider.gameObject.GetComponent<ArmedForce>().TakeDamage (atack);
								int deathArmedForce1 = selectedObject.GetComponent<ArmedForce>().TakeDamage (defence);

								if (deathArmedForce1 == 1)
								{
									selectedObject.GetComponent<ArmedForce>().Death();
									selectedObject = null;
									isArmedForce = false;
									neighborsOfProvince = null;
								}

								if (deathArmedForce2 == 1)
									hit.collider.gameObject.GetComponent<ArmedForce>().Death();*/
								selectedObject.GetComponent<Army>().AttackEnemyArmy (hit.collider.gameObject.GetComponent<Army>());

								GameObject.Find("GameGUI").GetComponent<GameGUI>().UpdateArmedForcePanel();
							}
						}
						else if (hit.collider.gameObject.GetComponent<City>() && neighborsOfProvince.Contains(hit.collider.gameObject.GetComponent<City>().cityData.provinceOwner))
						{
							Debug.Log("no");
						}
					//}
				}
			}
		}
	}


}
