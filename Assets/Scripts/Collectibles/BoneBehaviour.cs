using UnityEngine;
using System.Collections;

public class BoneBehaviour : CollectibleBehaviour
{
    private int type; //determines the type/appereance of bone that will be dropped


    public override void DoYourThing()
    {
        GameManager.addBones(1, GameManager.currentlyPlaying);
        base.DoYourThing();
    }
}
