using UnityEngine;
using System.Collections;

public class ArmedForceAttackController
{
	public int agressorAttack;
	public int targetAttack;
	public int agressorDefence;
	public int targetDefence;
	
	public Army agressor;
	public Army target;
	
	private int dispersion = 20;
	
	public ArmedForceAttackController (Army agressor, Army target)
	{
		this.agressor = agressor;
		this.target = target;
		
		agressorAttack = agressor.GetRandomArmyAttack();
		agressorDefence = agressor.GetRandomArmyDefence();
		System.Threading.Thread.Sleep(20);
		targetAttack = target.GetRandomArmyAttack();
		targetDefence = target.GetRandomArmyDefence();
	}
	
	public void CalculateAttackEnemyArmy ()
	{
		int calculatedAgressorAttack = agressorAttack + ((agressorAttack - targetDefence) / 100 * dispersion);
		int calculatedTargetAttack = targetAttack + ((targetAttack - agressorDefence) / 100 * dispersion);
		
		agressor.TakeDamage(calculatedTargetAttack);
		target.TakeDamage(calculatedAgressorAttack);
	}
}
	