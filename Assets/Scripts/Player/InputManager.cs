using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	//rework GUI to fit PC and Android:
	//Energy and health upper left corner
	//Abilities stay where they are but I need to rework the sprite
	//Jump button that's only visible when on Android in lower left corner
	//Button to open Menu in upper right corner if I ever get that far.
	private void Update()
	{
		
	}
	
	public Vector2 GetVerticalInput()
	{
		//Via tilt of the phone || (A, D)
		//Only calculate when called; NOT EVERY FRAME
		return new Vector2(0, 0);
	}
	
	//Jump and Abilities' values will be set by the buttons that are pressed
	public bool GetJumpInput()
	{
		//Button on screen || (Space)
		return false;
	}
	
	//Werden beide verumtlich eher in den Skripten speziell zu den Fähigkeiten aufgerufen.
	public bool GetPrimaryAbility()
	{
		//Button on screen || (Mouse0)
		return false;
	}
	
	public bool GetSecundaryAbility()
	{
		//Button on screen || (Mouse1)
		return false;
	}
	
}