using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public class InputSettings
    {
        public static string FORWARD_AXIS = "Vertical";
        public static string SIDEWAYS_AXIS = "Horizontal";
        public static string JUMP_AXIS = "Jump";
    }

    #region Buttons
    private static Button jmp;
    private static Button menu;
    private static Button Ability_p;
    private static Button Ability_s;
    #endregion

    //rework GUI to fit PC and Android:
    //Energy and health upper left corner
    //Abilities stay where they are but I need to rework the sprite
    //Jump button that's only visible when on Android in lower left corner
    //Button to open Menu in upper right corner if I ever get that far.
    private void Update()
	{
		
	}


    public static void AssignAllUIButtons(params Button[] buttons)
    {
        if (true/*Application.isMobilePlatform*/)
        {
            jmp = buttons[0];
            Ability_p = buttons[1];
            Ability_s = buttons[2];
            menu = buttons[3];
        }
    }

	public static float GetHorizontalInput()
	{
        //Via tilt of the phone || (A, D)
        //Only calculate when called; NOT EVERY FRAME
        float sidewaysInput = 0f;
        if (Application.isMobilePlatform)
        {

        }
        else
        {
            if (InputSettings.SIDEWAYS_AXIS.Length != 0)
                sidewaysInput = Input.GetAxis(InputSettings.SIDEWAYS_AXIS);
        }

        return sidewaysInput;
	}
	
	//Jump and Abilities' values will be set by the buttons that are pressed
	public static float GetJumpInput()
	{
        //Button on screen || (Space)
        float jmpInput = 0f;
        if (Application.isMobilePlatform)
        {

        }
        else
        {
            jmpInput = Input.GetAxis(InputSettings.JUMP_AXIS);
        }
        return jmpInput;
	}

    //Werden beide verumtlich eher in den Skripten speziell zu den Fähigkeiten aufgerufen.
    #region Primary
    public static bool GetPrimaryAbilityDown()
	{
        //Button on screen || (Mouse0)

        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButtonDown("Primary");
        }
	}

   
    public static bool GetPrimaryAbilityUp()
    {
        //Button on screen || (Mouse0)

        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButtonUp("Primary");
        }
    }

    public static bool GetPrimaryAbility()
    {
        //Button on screen || (Mouse0)

        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButton("Primary");
        }
    }
    #endregion
    #region Secundary
    public static bool GetSecundaryAbilityDown()
	{
        //Button on screen || (Mouse1)
        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButtonDown("Secundary");
        }
	}

    public static bool GetSecundaryAbilityUp()
    {
        //Button on screen || (Mouse1)
        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButtonUp("Secundary");
        }
    }

    public static bool GetSecundaryAbility()
    {
        //Button on screen || (Mouse1)
        if (Application.isMobilePlatform)
        {
            return false;
        }
        else
        {
            return Input.GetButton("Secundary");
        }
    }
    #endregion

}
