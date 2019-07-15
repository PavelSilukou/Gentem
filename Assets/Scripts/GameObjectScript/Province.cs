using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Province : MapObject
{
	public string provinceName = "";

	public ArmedForce armedForce;
	public Vector3 armedForcePosition;

	protected Province[] neighbors;
	
	protected static Color[] colors = {Color.white, 
										new Color(0.16f, 0.352f, 0.796f), 
										new Color(0.035f, 0.0588f, 0.372f)};

	public abstract void SetInitialObjectPosition();
	public abstract void FoundCity ();
	public abstract void CreateArmy ();

	public void PlaceArmedForce (GameObject targetArmy)
	{
		armedForce = targetArmy.GetComponent<Army>();
		targetArmy.transform.position = armedForcePosition;
	}
	
	public void RemoveArmedForce ()
	{
		armedForce = null;
	}

	public void SetNeighbors (Province[] neighbors)
	{
		this.neighbors = neighbors;
	}
	
	//public void SetInitialBonuses (float foodBonus, float productionBonus)
	//{
	//	provinceData.foodBonus = foodBonus;
	//	provinceData.productionBonus = productionBonus;
	//}
	
	//public ProvinceData GetProvinceData ()
	//{
	//	return provinceData;
	//}
	
	public Province[] GetNeighbors ()
	{
		return neighbors;
	}

	protected override void OnMouseEnter ()
	{
		if (!isSelect)
		{
			gameObject.GetComponent<Renderer>().materials[0].color = colors[1];
		}
	}
	
	protected override void OnMouseExit ()
	{
		if (!isSelect)
		{
			gameObject.GetComponent<Renderer>().materials[0].color = colors[0];
		}
	}
	
	public override void Hide ()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
	}
	
	public override void Show ()
	{
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
	}
	
	/*public override void FogOfWar ()
	{
		if (provinceData.city)
			provinceData.city.FogOfWar();
		if (provinceData.armedForce)
			provinceData.armedForce.FogOfWar();
	}*/

	private void Awake ()
	{
		provinceName = gameObject.name;
		gameGUI = GameObject.Find ("GameGUI").GetComponent<GameGUI>();
	}
}