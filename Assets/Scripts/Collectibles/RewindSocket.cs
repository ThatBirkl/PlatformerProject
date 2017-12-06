using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindSocket : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private float border;
    [SerializeField]
    private LayerMask p;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("active", true);
        border = 3.5f;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Animate();
        }

        if (FoundPlayer())
        {
            player = GameManager.player;
            anim.SetBool("active", true);
        }
    }

    private bool FoundPlayer()
    {
        return Physics2D.Raycast(transform.position, (GameManager.player.transform.position - transform.position), border, p);
    }

    private void Animate()
    {
        if (player.transform.position.x - gameObject.transform.position.x <= border &&
            player.transform.position.x - gameObject.transform.position.x >= (border * -1) &&
            player.transform.position.y - gameObject.transform.position.y <= border &&
            player.transform.position.y - gameObject.transform.position.y >= (border * -1))
        {
            ;
        }
        else
        {
            player = null;
            anim.SetBool("active", false);
        }
    }
}
