using UnityEngine;
using System.Collections;

public abstract class MapObject : MonoBehaviour {

	protected GameGUI gameGUI;

	protected bool isSelect = false;

	protected abstract void OnMouseEnter ();
	protected abstract void OnMouseExit ();
	public abstract void Select ();
	public abstract void Unselect ();	

	public abstract void Hide ();
	public abstract void Show ();
	public abstract void FogOfWar ();

}
