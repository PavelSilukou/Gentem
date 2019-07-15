using UnityEngine;
using System.Collections;

public abstract class ArmedForce : MapObject {

	public Province provinceOwner;
	public Player playerOwner;
	public string armedForceName;
	public int armedForceHealth = 100;
	public int armedForceCurrentHealth = 100;
	public int armedForceAtack = 0;
	public int armedForceDefence = 0;

	public static Color[] colors = {Color.white, 
									new Color (0.16f, 0.352f, 0.796f), 
									new Color (0.035f, 0.0588f, 0.372f)};

	public abstract void FoundCity ();

	public Province[] GetNeighborsOfProvince ()
	{
		return provinceOwner.GetNeighbors ();
	}

	/*public ArmyData GetArmedForceData ()
	{
		if (GlobalData.currentPlayer == armyData.playerOwner)
		{
			return armyData;
		}
		else {
			ArmyData newArmyData = new ArmyData();
			newArmyData.armyName = armyData.armyName;
			newArmyData.currentHealth = 0;
			return newArmyData;
		}
	}*/

	public abstract void MoveArmedForce (GameObject destination);

	public int GetAtack ()
	{
		return armedForceAtack;
	}
	
	public int TakeDamage (int damage)
	{
		armedForceCurrentHealth -= Mathf.Clamp(damage, 0, armedForceCurrentHealth);
		
		if (armedForceCurrentHealth == 0)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}
	
	public int GetDefence ()
	{
		return armedForceDefence;
	}
	
	public void Death ()
	{
		provinceOwner.RemoveArmedForce ();
		if (isSelect)
		{
			Unselect();
		}
		Destroy (gameObject);
	}

	protected override void OnMouseEnter ()
	{
		if (!isSelect)
			gameObject.GetComponent<Renderer>().materials[1].color = colors[1];
	}
	
	protected override void OnMouseExit ()
	{
		if (!isSelect)
			gameObject.GetComponent<Renderer>().materials[1].color = colors[0];
	}
	
	public override void Select ()
	{
		gameObject.GetComponent<Renderer>().materials[1].color = colors[2];
		isSelect = true;
		
		gameGUI.SelectedObject (2, this);
	}

	public override void Unselect ()
	{
		gameObject.GetComponent<Renderer>().materials[1].color = colors[0];
		isSelect = false;
		
		gameGUI.UnselectedObject ();
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
	
	public override void FogOfWar ()
	{

	}

	private void Awake ()
	{
		gameGUI = GameObject.Find ("GameGUI").GetComponent<GameGUI>();
	}
}
