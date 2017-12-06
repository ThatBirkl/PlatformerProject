using UnityEngine;
using System.Collections;

public class RewindUnlock : CollectibleBehaviour
{
    private Animator anim;
    private float border1, border2;
    private GameObject player;
    private GameObject socket;
    [SerializeField]
    private LayerMask p;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("active 1", false);
        anim.SetBool("active 2", false);
        border1 = 5.5f;
        border2 = 2f;
        socket = Resources.Load<GameObject>("Prefabs/Collectible/Rewind_Unlock_Socket");
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Animate1();
        }

        if (FoundPlayer())
        {
            player = GameManager.player;
        }
    }

    public override void DoYourThing()
    {
        //First: Unlock the rewind ability
        GameManager.ActivateAbility("Rewind");
        //after that it has to be equipped
        //check if there is already a primary; if not, then it will be the new primary
        if (GameManager.GivePrimary(true) == 0)
        {
            GameManager.SetAbilities("Rewind", true, true);
        }
        else
        {
            if (GameManager.GiveSecundary(true) == 0)
            {
                GameManager.SetAbilities("Rewind", false, true);
            }
            else
            {
                print("Both abilities already assigned; should not be the case in the beginning");
            }
        }
        HUD_Management.SetIcons();
        SetSocket();
        base.DoYourThing();
    }

    private bool FoundPlayer()
    {
        return Physics2D.Raycast(transform.position, (GameManager.player.transform.position - transform.position), border1, p);
    }

    private void Animate2()
    {
        //checks each frame how far the player is away; if he passes a certain border active 2 will be true or false
        if (player.transform.position.x - gameObject.transform.position.x <= border2 &&
            player.transform.position.x - gameObject.transform.position.x >= (border2 * -1) &&
            player.transform.position.y - gameObject.transform.position.y <= border2 &&
            player.transform.position.y - gameObject.transform.position.y >= (border2 * -1))
        {
            anim.SetBool("active 2", true);
        }
        else
        {
            anim.SetBool("active 2", false);
        }
    }

    private void Animate1()
    {
        if (player.transform.position.x - gameObject.transform.position.x <= border1 &&
            player.transform.position.x - gameObject.transform.position.x >= (border1 * -1) &&
            player.transform.position.y - gameObject.transform.position.y <= border1 &&
            player.transform.position.y - gameObject.transform.position.y >= (border1 * -1))
        {
            anim.SetBool("active 1", true);
            Animate2();
        }
        else
        {
            player = null;
            anim.SetBool("active 1", false);
        }
    }

    private void SetSocket()
    {
        Instantiate(socket, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
    }
}

