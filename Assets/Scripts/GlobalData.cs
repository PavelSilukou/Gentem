using UnityEngine;
using System.Collections;

public class GlobalData
{
	//public static int thisPlayer = 0;
	public static Player[] players;
	public static int currentPlayerNumber = 1;
	public static Player currentPlayer;
	public static int currentTurn = 1;
	public static int maxPlayers;

	public static Color[] playersColors = {new Color(0.615f, 0.215f, 0.894f), 
											new Color(0.929f, 0.937f, 0.247f)};

	public static void Init (int maxPl)
	{
		maxPlayers = maxPl;
		players = new Player[maxPlayers];
		players [0] = new HumanPlayer ();
		players [0].playerName = "player 1";
		players [0].playerColor = playersColors [0];
		players [1] = new HumanPlayer ();
		players [1].playerName = "player 2";
		players [1].playerColor = playersColors [1];

		currentPlayer = players [0];
	}

	public static void NextTurn ()
	{
		if (currentPlayerNumber < maxPlayers)
		{
			currentPlayerNumber += 1;
			currentPlayer = players [currentPlayerNumber - 1];
		}
		else
		{
			currentPlayerNumber = 1;
			currentPlayer = players [0];
			currentTurn += 1;
		}
	}

	/*public static bool isMyTurn ()
	{
		return thisPlayer == currentPlayer;
	}*/
}