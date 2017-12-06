using UnityEngine;
using System.Collections;

public class CheatManager : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.SetAbilities("Rewind", true, true);
            HUD_Management.SetIcons();
        }
    }
}
