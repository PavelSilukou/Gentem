using UnityEngine;
using System.Collections;

public class CityData
{
	//1 гражданин потребляет 1 единицу еды. Для роста необходимо 2 хода, если есть прирост еды
	//и 2 хода на уменьшение граждан, если еда кончилась
	public string cityName = "";
	public Province provinceOwner;
	public Player playerOwner;
	public int citizens = -1;
	public int requiredTurnsForUp = 2;
	public int currentTurnsForUp = 1;
	public int requiredTurnsForDown = 2;
	public int currentTurnsForDown = 1;
	public float foodBonus = 0.0f;
	public float productionBonus = 0.0f;
	public float currentFood = 0.0f;
	public float currentProd = 0.0f;
	public float surplusesFood = 0.0f;
	public float surplusesProd = 0.0f;
	public InfraData infraData = new InfraData();

	public void CalculateAddResources ()
	{
		surplusesFood = infraData.foodLvl * citizens * foodBonus - citizens;
		surplusesProd = infraData.prodLvl * citizens * productionBonus - citizens;
	}

	public void NextTurn ()
	{
		infraData.NextTurn ();

		CalculateAddResources ();

		if (currentFood > 0)
			currentTurnsForDown = 1;

		if (currentFood <= 0)
		{
			if (currentTurnsForDown == requiredTurnsForDown)
			{
				citizens -= 1;
				currentTurnsForDown = 1;
			}
			else
			{
				currentTurnsForDown += 1;
			}
		}

		if (surplusesFood > 0)
		{
			if (currentTurnsForUp == requiredTurnsForUp)
			{
				citizens += 1;
				currentTurnsForUp = 1;
			}
			else
			{
				currentTurnsForUp += 1;
			}

			currentFood += surplusesFood / 2;
		}
		else
		{
			currentFood += surplusesFood;
		}

		currentProd += surplusesProd;
	}
}

public class InfraData
{
	public int foodLvl = -1;
	public int prodLvl = -1;
	public int requiredTurnsForFoodUp = 2;
	public int requiredTurnsForProdUp = 2;
	public int currentTurnsForFoodUp = 1;
	public int currentTurnsForProdUp = 1;

	public bool isFoodUp = false;
	public bool isProdUp = false;

	public void NextTurn ()
	{
		if (isFoodUp)
		{
			if (currentTurnsForFoodUp == requiredTurnsForFoodUp)
			{
				foodLvl += 1;
				currentTurnsForFoodUp = 1;
				isFoodUp = false;
			}
			else
			{
				currentTurnsForFoodUp += 1;
			}
		}

		if (isProdUp)
		{
			if (currentTurnsForProdUp == requiredTurnsForProdUp)
			{
				prodLvl += 1;
				currentTurnsForProdUp = 1;
				isProdUp = false;
			}
			else
			{
				currentTurnsForProdUp += 1;
			}
		}
	}

	public void FoodUp ()
	{
		isFoodUp = true;
	}

	public void ProdUp ()
	{
		isProdUp = true;
	}

	public bool isFoodUpAvailable ()
	{
		return !isFoodUp;
	}

	public bool isProdUpAvailable ()
	{
		return !isProdUp;
	}
}