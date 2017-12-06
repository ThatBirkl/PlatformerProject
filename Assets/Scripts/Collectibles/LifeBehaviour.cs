using UnityEngine;
using System.Collections;

public class LifeBehaviour : CollectibleBehaviour
{
    public int lifes = 10;

    public override void DoYourThing()
    {
        GameManager.addLives(lifes, GameManager.currentlyPlaying);
        base.DoYourThing();
    }

}
