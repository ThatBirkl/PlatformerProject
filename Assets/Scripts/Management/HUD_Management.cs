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
    //Desktop
    private static Image health_d;
    private static Image energy_d;
    private static Text healthValue_d;
    private static Text energyValue_d;
    //Mobile
    private static Image health_m;
    private static Image energy_m;
    private static Text healthValue_m;
    private static Text energyValue_m;
    private static Button jumpButton;
    private static Button primaryButton;
    private static Button secundaryButton;
    //Both
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
        SetUIInvisibility();
        abilitiesOffset = abilities.transform.position;
        SetIcons();
        InputManager.AssignAllUIButtons(jumpButton, primaryButton, secundaryButton);
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

        health_d = i[0];
        energy_d = i[1];
        primaryIcon = i[2];
        secundaryIcon = i[3];
        abilities = i[4];
        health_m = i[5];
        energy_m = i[6];

        //Then the texts
        Text[] t = elements.GiveTexts();
        healthValue_d = t[0];
        energyValue_d = t[1];
        healthValue_m = t[2];
        energyValue_m = t[3];

        //Buttons
        Button[] b = elements.GiveButtons();
        jumpButton = b[0];
        primaryButton = b[1];
        secundaryButton = b[2];
        
    }

    private void SetUIInvisibility()
    {
        if (Application.isMobilePlatform)
        {
            health_d.transform.parent.transform.parent.GetComponent<Image>().enabled = false;
            health_d.transform.parent.GetComponent<Image>().enabled = false;
            energy_d.transform.parent.GetComponent<Image>().enabled = false;
            health_d.enabled = false;
            energy_d.enabled = false;
            health_d.gameObject.transform.parent.transform.GetChild(1).GetComponent<Text>().enabled = false;
            energy_d.gameObject.transform.parent.transform.GetChild(1).GetComponent<Text>().enabled = false;
        }
        else
        {
            health_m.transform.parent.transform.parent.GetComponent<Image>().enabled = false;
            health_m.transform.parent.GetComponent<Image>().enabled = false;
            energy_m.transform.parent.GetComponent<Image>().enabled = false;
            health_m.enabled = false;
            energy_m.enabled = false;
            health_m.gameObject.transform.parent.transform.GetChild(1).GetComponent<Text>().enabled = false;
            energy_m.gameObject.transform.parent.transform.GetChild(1).GetComponent<Text>().enabled = false;
            jumpButton.transform.parent.GetComponent<Image>().enabled = false;
            jumpButton.GetComponent<Image>().enabled = false;
            jumpButton.transform.GetChild(0).GetComponent<Text>().enabled = false;
            jumpButton.enabled = false;
            primaryButton.enabled = false;
            secundaryButton.enabled = false;
        }
    }

    #region Abilities
    public static void SetIcons()
    {
        print(primary + "||" + secundary);
        calculateValues();
        if (primary == 0 && secundary == 0)
        {
            abilities.enabled = false;
        }
        else
        {
            abilities.enabled = true;
            //abilities.transform.position = abilitiesOffset; //Landet, bei der x-Koordinate -80, weil das komischerweise immer 800 abzieht
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
        //Falls Desktop
        health_d.fillAmount = healthFill;
        energy_d.fillAmount = energyFill;
        healthValue_d.text = "" + currentHealth;
        energyValue_d.text = "" + (currentEnergy / 100);

        //Falls Android
        health_m.fillAmount = healthFill;
        energy_m.fillAmount = energyFill;
        healthValue_m.text = "" + currentHealth;
        energyValue_m.text = "" + (currentEnergy / 100);
    }

    public static float returnPercentage(float current, float max)
    {
        //Gibt den Prozentwert der Verhältnisses Current - Max zurück
        float f = (current / max);
        return f;
    }
    #endregion
}
