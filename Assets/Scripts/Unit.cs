using UnityEngine;
using System.Collections;

public class Unit
{
	public int unitMaxHealthPoint;
	public int unitCurrentHealthPoint;
	public int unitAttack;
	public int unitDefence;
	
	public string unitName;
	public string unitClass;
	public int unitLevel;
	
	private int dispersion = 10;
	
	public Unit (string unitName, string unitClass, int unitLevel,
	             int unitMaxHealthPoint, int unitAttack, int unitDefence)
	{
		this.unitName = unitName;
		this.unitClass = unitClass;
		this.unitLevel = unitLevel;
		this.unitMaxHealthPoint = unitMaxHealthPoint;
		this.unitCurrentHealthPoint = unitMaxHealthPoint;
		this.unitAttack = unitAttack;
		this.unitDefence = unitDefence;
	}
	
	public int GetAccurateAttack ()
	{
		return (int)((float)unitAttack / unitMaxHealthPoint * unitCurrentHealthPoint);
	}
	
	public int GetRandomAttack ()
	{
		int attack = unitAttack + Random.Range (-unitAttack / 100 * dispersion, unitAttack / 100 * dispersion);
		return (int)((float)attack / unitMaxHealthPoint * unitCurrentHealthPoint);
	}
	
	public int GetAccurateDefence ()
	{
		return (int)((float)unitDefence / unitMaxHealthPoint * unitCurrentHealthPoint);
	}
	
	public int GetRandomDefence ()
	{
		int defence = unitDefence + Random.Range (-unitDefence / 100 * dispersion, unitDefence / 100 * dispersion);
		return (int)((float)defence / unitMaxHealthPoint * unitCurrentHealthPoint);
	}
}

