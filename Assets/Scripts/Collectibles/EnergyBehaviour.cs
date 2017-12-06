using UnityEngine;
using System.Collections;

public class EnergyBehaviour : CollectibleBehaviour
{
    [SerializeField]
    private int energy = 500;

    public override void DoYourThing()
    {
        GameManager.addEnergy(energy, GameManager.currentlyPlaying);
        base.DoYourThing();
    }
}
