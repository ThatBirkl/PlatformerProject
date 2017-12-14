using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Elements : MonoBehaviour
{
    [SerializeField]
    private Image health_d;
    [SerializeField]
    private Image energy_d;
    [SerializeField]
    private Image health_m;
    [SerializeField]
    private Image energy_m;
    [SerializeField]
    private Text healthValue_d;
    [SerializeField]
    private Text energyValue_d;
    [SerializeField]
    private Text healthValue_m;
    [SerializeField]
    private Text energyValue_m;
    [SerializeField]
    private Image primaryIcon;
    [SerializeField]
    private Image secundaryIcon;
    [SerializeField]
    private Button primaryButton;
    [SerializeField]
    private Button secundaryButton;
    [SerializeField]
    private Image abilities;
    [SerializeField]
    private Button jump;

    public Image[] GiveImages()
    {
        Image[] i = new Image[7];
        i[0] = health_d;
        i[1] = energy_d;
        i[2] = primaryIcon;
        i[3] = secundaryIcon;
        i[4] = abilities;
        i[5] = health_m;
        i[6] = energy_m;
        return i;
    }

    public Text[] GiveTexts()
    {
        Text[] t = new Text[4];
        t[0] = healthValue_d;
        t[1] = energyValue_d;
        t[2] = healthValue_m;
        t[3] = energyValue_m;
        return t;
    }

    public Button[] GiveButtons()
    {
        Button[] b = new Button[3];
        b[0] = jump;
        b[1] = primaryButton;
        b[2] = secundaryButton;
        return b;
    }
}
