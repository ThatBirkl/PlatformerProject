using UnityEngine;
using System.Collections;

public class ForcefieldUnlock : CollectibleBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        int i = Random.Range(1, 3);
        anim.SetInteger("animation", i);
        switch (i)
        {
            case 1:
                for (int v = 0; v < 11; v++)
                {
                    yield return new WaitForEndOfFrame();
                }
                break;
            case 2:
                for (int v = 0; v < 12; v++)
                {
                    yield return new WaitForEndOfFrame();
                }
                break;
        }
        StartCoroutine(Animate());
    }

    public override void DoYourThing()
    {
        //First: Unlock the rewind ability
        GameManager.ActivateAbility("Forcefield");
        //after that it has to be equipped
        //check if there is already a primary; if not, then it will be the new primary
        if (GameManager.GivePrimary(true) == 0)
        {
            GameManager.SetAbilities("Forcefield", true, true);
        }
        else
        {
            if (GameManager.GiveSecundary(true) == 0)
            {
                GameManager.SetAbilities("Forcefield", false, true);
            }
            else
            {
                print("Both abilities already assigned; should not be the case in the beginning");
            }
        }
        HUD_Management.SetIcons();
        base.DoYourThing();
    }
}
