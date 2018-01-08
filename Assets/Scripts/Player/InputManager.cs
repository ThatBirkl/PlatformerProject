using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    #region Helper Constructs
    public class InputSettings
    {
        public static string FORWARD_AXIS = "Vertical";
        public static string SIDEWAYS_AXIS = "Horizontal";
        public static string JUMP_AXIS = "Jump";
    }

    public struct ButtonInput
    {
        public bool up;
        public bool down;
        public bool hold;
    }
    #endregion
    private static ButtonInput primary;
    private static ButtonInput secundary;
    public LayerMask touchInputMask;
    private static float jumpInput;
    private static LinkedList<GameObject> prompts = new LinkedList<GameObject>();
    private static LinkedList<string> hitPrompts = new LinkedList<string>(); //only the names; easier to compare
    private static LinkedList<string> hitPromptsPrev = new LinkedList<string>(); //only the names

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

    private void Start()
    {
        primary = new ButtonInput();
        primary.up = false;
        primary.down = false;
        primary.hold = false;
        secundary = new ButtonInput();
        secundary.up = false;
        secundary.down = false;
        secundary.hold = false;
        jumpInput = 0f;
    }

    private void Update()
    {
        DetectTouches();
    }

    private void LateUpdate()
    {
        primary.up = false;
        primary.down = false;
        secundary.up = false;
        secundary.down = false;
        jumpInput = 0f;
    }

    private void DetectTouches()
    {
        if (!(Application.platform == RuntimePlatform.Android))
            return;

        hitPromptsPrev = hitPrompts;
        hitPrompts.Clear();
        GameObject[] _prompts = new GameObject[prompts.Count];
        prompts.CopyTo(_prompts, 0);
        GameObject recipient;
        foreach (Touch t in Input.touches)
        {
            RaycastHit2D hit = Physics2D.Raycast((t.position), Vector2.zero);
            if (hit.collider != null)
            {
                recipient = hit.transform.gameObject;

                if (t.phase == TouchPhase.Began)
                {
                    if (recipient.name.Equals(jmp.name))
                    { jumpInput = 1f; }
                    else if (recipient.name.Equals(Ability_p.name))
                    {
                        primary.down = true;
                        primary.hold = true;
                    }
                    else if (recipient.name.Equals(Ability_s.name))
                    {
                        secundary.down = true;
                        secundary.hold = true;
                    }
                }
                else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                {
                    if (recipient.name.Equals(jmp.name))
                    { jumpInput = 0f; }
                    else if (recipient.name.Equals(Ability_p.name))
                    {
                        primary.up = true;
                        primary.hold = false;
                    }
                    else if (recipient.name.Equals(Ability_s.name))
                    {
                        secundary.up = true;
                        secundary.hold = false;
                    }
                }
            }
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(t.position), Vector2.zero);
            if (hit.collider != null)
            {
                for (int i = 0; i < _prompts.Length; i++)
                {
                    recipient = hit.transform.gameObject;
                    if (recipient.name.Equals(_prompts[i].name))
                    {
                        hitPrompts.AddLast(_prompts[i].name);
                    }
                }
            }

        }
    }

    public static void AssignAllUIButtons(params Button[] buttons)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jmp = buttons[0];
            Ability_p = buttons[1];
            Ability_s = buttons[2];
            //menu = buttons[3];
        }
    }

	public static float GetHorizontalInput()
	{
        //Via tilt of the phone || (A, D)
        //Only calculate when called; NOT EVERY FRAME
        float sidewaysInput = 0f;
        if (Application.platform == RuntimePlatform.Android)
        {
            sidewaysInput = Input.acceleration.x;
            if (sidewaysInput > -0.15 && sidewaysInput < 0.15)
                sidewaysInput = 0;

            sidewaysInput *= 3;

            if (sidewaysInput > 1)
                sidewaysInput = 1;
            else if (sidewaysInput < -1)
                sidewaysInput = -1;

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
        if (Application.platform == RuntimePlatform.Android)
        {
        }
        else
        {
            jumpInput = Input.GetAxis(InputSettings.JUMP_AXIS);
        }
        return jumpInput;
	}

    //Werden beide verumtlich eher in den Skripten speziell zu den Fähigkeiten aufgerufen.
    #region Primary
    public static bool GetPrimaryAbilityDown()
	{
        //Button on screen || (Mouse0)

        if (Application.platform == RuntimePlatform.Android)
        {
            return primary.down;
        }
        else
        {
            return Input.GetButtonDown("Primary");
        }
	}

   
    public static bool GetPrimaryAbilityUp()
    {
        //Button on screen || (Mouse0)

        if (Application.platform == RuntimePlatform.Android)
        {
            return primary.up;
        }
        else
        {
            return Input.GetButtonUp("Primary");
        }
    }

    public static bool GetPrimaryAbility()
    {
        //Button on screen || (Mouse0)

        if (Application.platform == RuntimePlatform.Android)
        {
            return primary.hold;
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
        if (Application.platform == RuntimePlatform.Android)
        {
            return secundary.down;
        }
        else
        {
            return Input.GetButtonDown("Secundary");
        }
	}

    public static bool GetSecundaryAbilityUp()
    {
        //Button on screen || (Mouse1)
        if (Application.platform == RuntimePlatform.Android)
        {
            return secundary.up;
        }
        else
        {
            return Input.GetButtonUp("Secundary");
        }
    }

    public static bool GetSecundaryAbility()
    {
        //Button on screen || (Mouse1)
        if (Application.platform == RuntimePlatform.Android)
        {
            return secundary.hold;
        }
        else
        {
            return Input.GetButton("Secundary");
        }
    }
    #endregion

    #region Interaction
    public static void AddPrompt(GameObject _prompt)
    {
        prompts.AddLast(_prompt);
    }
    public static void RemovePrompt(GameObject _prompt)
    {
        prompts.Remove(_prompt);    
    }

    public static bool GetInteractDown(params GameObject[] obj)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            print(hitPrompts + " || " + obj[0].GetComponent<Interactible>().ButtonPrompt);
            print(hitPrompts.Contains(obj[0].GetComponent<Interactible>().ButtonPrompt.name));
            if (hitPrompts.Contains(obj[0].GetComponent<Interactible>().ButtonPrompt.name))
            {
                hitPrompts.Remove(obj[0].GetComponent<Interactible>().ButtonPrompt.name);
                print("interactig with prompt " + obj[0].GetComponent<Interactible>().ButtonPrompt.name);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetButtonDown("Interact");
        }
    }

    public static bool GetInteractUp(params GameObject[] obj)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!hitPrompts.Contains(obj[0].GetComponent<Interactible>().ButtonPrompt.name)
                && hitPromptsPrev.Contains(obj[0].GetComponent<Interactible>().ButtonPrompt.name))
            {
                hitPromptsPrev.Remove(obj[0].GetComponent<Interactible>().ButtonPrompt.name);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetButtonUp("Interact");
        }
    }

    public static bool GetInteract(params GameObject[] obj)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (hitPrompts.Contains(obj[0].GetComponent<Interactible>().ButtonPrompt.name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetButton("Interact");
        }
    }
    #endregion

}
