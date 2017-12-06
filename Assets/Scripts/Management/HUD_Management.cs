using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(HUD_Elements))]
public class HUD_Management : MonoBehaviour
{
    #region Values
    private static int currentHealth;
    private static int currentEnergy;
    private static int primary;
    private static int secundary;
    private static float healthFill;
    private static float energyFill;
    private static Image health;
    private static Image energy;
    private static Text healthValue;
    private static Text energyValue;
    private static Image primaryIcon;
    private static Image secundaryIcon;
    private static Image abilities;
    private HUD_Elements elements;
    private static Vector3 abilitiesOffset;

    #endregion

    void Start ()
    {
        calculateValues();
        elements = gameObject.GetComponent<HUD_Elements>();
        AssignUI();
        abilitiesOffset = abilities.transform.position;
        SetIcons();
	}

    void Update ()
    {
        calculateValues();
        HandleBar();
        Colorint();
    }

    public static void calculateValues()
    {
        currentHealth = GameManager.GiveLives(GameManager.currentlyPlaying);
        currentEnergy = GameManager.GiveEnergy(GameManager.currentlyPlaying);
        primary = GameManager.GivePrimary(GameManager.currentlyPlaying);
        secundary = GameManager.GiveSecundary(GameManager.currentlyPlaying);
        healthFill = returnPercentage(currentHealth, GameManager.GiveMaxLives(GameManager.currentlyPlaying));
        energyFill = returnPercentage(currentEnergy, GameManager.GiveMaxEnergy(GameManager.currentlyPlaying));
    }

    private void AssignUI()
    {
        //Assigns in HUD_Elements Images and Texts to the fields in this class so they can be static
        //Images first;
        Image[] i = elements.GiveImages();
        health = i[0];
        energy = i[1];
        primaryIcon = i[2];
        secundaryIcon = i[3];
        abilities = i[4];

        //Then the texts
        Text[] t = elements.GiveTexts();
        healthValue = t[0];
        energyValue = t[1];
    }

    #region Abilities
    public static void SetIcons()
    {
        calculateValues();
        if (primary == 0 && secundary == 0)
        {
            abilities.transform.position = new Vector3(999999, 9999999);
        }
        else
        {
            abilities.transform.position = abilitiesOffset; //Landet, bei der x-Koordinate -80, weil das komischerweise immer 800 abzieht
            Sprite[] mask = Resources.LoadAll<Sprite>("Sprites/Interface/HUD_abilities");
            switch (primary)
            {
                case 0:
                    primaryIcon.sprite = mask[2];
                    primaryIcon.color = Color.black;
                    break;
                case 1:
                    primaryIcon.sprite = Resources.Load<Sprite>("Sprites/Interface/Rewind_Icon");
                    primaryIcon.color = Color.white;
                    break;
                case 2:
                    primaryIcon.sprite = Resources.Load<Sprite>("Sprites/Interface/Forcefield_Icon");
                    primaryIcon.color = Color.white;
                    break;
                default:
                    primaryIcon.sprite = mask[2];
                    primaryIcon.color = Color.black;
                    break;
            }
            //Scaling
            primaryIcon.SetNativeSize();
            primaryIcon.transform.localScale = new Vector3(0.45f, 0.45f, 0);
            

            switch (secundary)
            {
                case 0:
                    secundaryIcon.sprite = mask[0];
                    secundaryIcon.color = Color.black;
                    break;
                case 1:
                    secundaryIcon.sprite = Resources.Load<Sprite>("Sprites/Interface/Rewind_Icon");
                    secundaryIcon.color = Color.white;
                    break;
                case 2:
                    secundaryIcon.sprite = Resources.Load<Sprite>("Sprites/Interface/Forcefield_Icon");
                    secundaryIcon.color = Color.white;
                    break;
                default:
                    secundaryIcon.sprite = mask[0];
                    secundaryIcon.color = Color.black;
                    break;
            }
            //Scaling
            secundaryIcon.SetNativeSize();
            secundaryIcon.transform.localScale = new Vector3(0.45f, 0.45f, 0);
        }
    }

    private void Colorint()
    {
        if (Input.GetButtonDown("Primary") && GameManager.GivePrimary(GameManager.currentlyPlaying) != 0)
        {
            primaryIcon.color = Color.red;
        }
        if (Input.GetButtonUp("Primary") && GameManager.GivePrimary(GameManager.currentlyPlaying) != 0)
        {
            primaryIcon.color = Color.white;
        }
        if (Input.GetButtonDown("Secundary") && GameManager.GiveSecundary(GameManager.currentlyPlaying) != 0)
        {
            secundaryIcon.color = Color.red;
        }
        if (Input.GetButtonUp("Secundary") && GameManager.GiveSecundary(GameManager.currentlyPlaying) != 0)
        {
            secundaryIcon.color = Color.white;
        }
    }
    #endregion

    #region Health & Energy
    private void HandleBar()
    {
        health.fillAmount = healthFill;
        energy.fillAmount = energyFill;
        healthValue.text = "" + currentHealth;
        energyValue.text = "" + (currentEnergy / 100); 
    }

    public static float returnPercentage(float current, float max)
    {
        //Gibt den Prozentwert der Verhältnisses Current - Max zurück
        float f = (current / max);
        return f;
    }
    #endregion
}
