using UnityEngine;
using System.Collections;
using System.Linq;

public class Army : ArmedForce {

	public ArrayList units = new ArrayList();

	public override void FoundCity ()
	{
		LandProvince landProvince = (LandProvince)provinceOwner;
		//provinceData.isFound = true;
		GameObject c = Instantiate(Resources.Load ("GameObjects/City", typeof(Object))) as GameObject;
		c.name = "New City " + Random.Range(1, 1000);
		//c.transform.position = cityPosition;
		
		City ci = c.GetComponent<City> ();
		ci.SetInitialData (c.name, landProvince, GlobalData.currentPlayer, landProvince.foodBonus, landProvince.productionBonus, 1, 1, 5, 5, 1);
		ci.GetComponent<Renderer>().materials [0].color = GlobalData.currentPlayer.playerColor;

		GameObject cities = GameObject.Find ("Cities");
		cities.GetComponent<Cities> ().AddCity (c);
		
		//city = ci;
		c.transform.position = landProvince.cityPosition;
		landProvince.city = ci;
	}
	
	public void SetInitialData (string armedForceName, Province provinceOwner, Player playerOwner)
	{
		this.armedForceName = armedForceName;
		this.provinceOwner = provinceOwner;
		this.playerOwner = playerOwner;
		this.armedForceAtack = 5;
		this.armedForceDefence = 10;
	}

	public override void MoveArmedForce (GameObject destination)
	{
		provinceOwner.RemoveArmedForce ();
		destination.GetComponent<Province> ().PlaceArmedForce (this.gameObject);

		Province[] provinceNeighbors = destination.GetComponent<Province> ().GetNeighbors ();
		foreach (Province obj in provinceNeighbors)
		{
			obj.Show();
		}

		provinceOwner = destination.GetComponent<Province> ();

		GameObject.Find ("GameGUI").GetComponent<GameGUI> ().UpdateArmedForcePanel ();
		//GameObject.Find ("Main Camera").GetComponent<CameraMove> ().TranslateCamera (destination.transform.position);
	}

	public int GetAccurateArmyAttack ()
	{
		int armyAttack = 0;
		foreach (Unit unit in units)
		{
			armyAttack += unit.GetAccurateAttack();
		}
		return armyAttack;
	}
	
	public int GetRandomArmyAttack()
	{
		int armyAttack = 0;
		foreach (Unit unit in units)
		{
			armyAttack += unit.GetRandomAttack();
		}
		return armyAttack;
	}
	
	public int GetAccurateArmyDefence()
	{
		int armyDefence = 0;
		foreach (Unit unit in units)
		{
			armyDefence += unit.GetAccurateDefence();
		}
		return armyDefence;
	}
	
	public int GetRandomArmyDefence()
	{
		int armyDefence = 0;
		foreach (Unit unit in units)
		{
			armyDefence += unit.GetRandomDefence();
		}
		return (int)(armyDefence / units.Count);
	}
	
	public void AttackEnemyArmy (Army enemyArmy)
	{
		ArmedForceAttackController armedForceAttackController = new ArmedForceAttackController(this, enemyArmy);
		armedForceAttackController.CalculateAttackEnemyArmy();
	}
	
	public void TakeDamage (int damage)
	{
		var orderedRegions = units.Cast<Unit>().OrderBy(r => r.unitCurrentHealthPoint).ToList();
		
		int unitsCount = orderedRegions.Count;
		int remainingDamage = damage;
		
		for (int i = 0; i < orderedRegions.Count; i++)
		{
			int damageForUnit = remainingDamage / (unitsCount - i);
			Unit unit = (Unit)orderedRegions[i];
			
			if (damageForUnit >= unit.unitCurrentHealthPoint)
			{
				//unit is dead
				remainingDamage -= unit.unitCurrentHealthPoint;
				unitsCount -= 1;
				units.Remove(unit);
				orderedRegions.Remove(unit);
				i -= 1;
			}
			else
			{
				unit.unitCurrentHealthPoint -= damageForUnit;
				remainingDamage -= damageForUnit;
			}
		}
	}


}
