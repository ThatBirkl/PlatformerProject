using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.GiveEnergy(true) >= 0 && (GameManager.GivePrimary(true) == 1 && Input.GetButtonDown("Primary") || GameManager.GiveSecundary(true) == 1 && Input.GetButtonDown("Secundary")))
        {
            GameManager.Paused = true;
        }


        if ((Input.GetButtonUp("Primary") && GameManager.GivePrimary(true) == 1) || (Input.GetButtonUp("Secundary") && GameManager.GiveSecundary(true) == 1) || GameManager.GiveEnergy(true) <= 0)
        {
            GameManager.Paused = false;
        }
    }
}
