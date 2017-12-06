using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper_Projectile : ProjectileBehaviour
{
    protected override void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        damage = 5;
        velX = 8;
        velY = 0;
        CalculateDirection();
        base.Start();
    }

    private void CalculateDirection()
    {
        Vector2 dir = GameManager.player.transform.position - transform.position;
        if (dir.x < 0)
        {
            velX *= -1;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<EnemyBehaviour>().type.Equals("Hopper"))
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            }
            else
            {
                StartCoroutine(Splat());
            }
        }
        else
        {
            if (col.gameObject.tag == "Player")
            {
                GameManager.addLives(-damage, GameManager.currentlyPlaying);
            }
            StartCoroutine(Splat());
        }
    }

    IEnumerator Splat()
    {
        GetComponent<Collider2D>().isTrigger = true;
        anim.SetBool("splat", true);
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        rigidbody.velocity = Vector2.zero;
        yield return new WaitForFixedUpdate();
        velocity = Vector2.zero;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Object.Destroy(gameObject);
    }
}
