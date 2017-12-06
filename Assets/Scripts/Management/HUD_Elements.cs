using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Elements : MonoBehaviour
{
    [SerializeField]
    private Image health;
    [SerializeField]
    private Image energy;
    [SerializeField]
    private Text healthValue;
    [SerializeField]
    private Text energyValue;
    [SerializeField]
    private Image primaryIcon;
    [SerializeField]
    private Image secundaryIcon;
    [SerializeField]
    private Image abilities;

    public Image[] GiveImages()
    {
        Image[] i = new Image[5];
        i[0] = health;
        i[1] = energy;
        i[2] = primaryIcon;
        i[3] = secundaryIcon;
        i[4] = abilities;
        return i;
    }

    public Text[] GiveTexts()
    {
        Text[] t = new Text[2];
        t[0] = healthValue;
        t[1] = energyValue;
        return t;
    }
}
