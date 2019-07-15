using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	//расстояние от границы экрана, после которой начинается движение
	public int boundary = 1;
	//скорость движения
	public int speed = 10;
	
	//private int screenWidth;
	//private int screenHeight;
	
	void Start () 
	{
		//screenWidth = Screen.width;
		//screenHeight = Screen.height;
	}

	public void TranslateCamera (Vector3 target)
	{
		transform.position = new Vector3 (target.x, transform.position.y, target.z + 20.0f);
	}
	
	void Update () 
	{
		//движение камеры с помощью стрелок
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}
		
		/*if (Input.mousePosition.x > theScreenWidth - Boundary)
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		
		if (Input.mousePosition.x < 0 + Boundary)
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
		}
		
		if (Input.mousePosition.y > theScreenHeight - Boundary)
		{
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}
		
		if (Input.mousePosition.y < 0 + Boundary)
		{
			transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
		}*/
	}
}
