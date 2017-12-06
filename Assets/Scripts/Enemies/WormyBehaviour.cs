using UnityEngine;
using System.Collections;

public class WormyBehaviour : EnemyBehaviour
{
    new void Start()
    {
        base.Start();
        lookingRight = true;
        type = "Wormy";
        lives = 1;
        damage = 20;
    }

    public override void Drop()
    {
        Vector2 dropposition = transform.position;
        dropposition.y = dropposition.y - (transform.localScale.y * 0.2f);

        int drop = Random.Range(1, 4);
        switch (drop)
        {
            case 1:
                Instantiate(DropManager.energy, dropposition, Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                Instantiate(DropManager.health, dropposition, Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                Instantiate(DropManager.bone, dropposition, Quaternion.Euler(0, 0, 0));
                break;
            default:
                Instantiate(DropManager.bone, dropposition, Quaternion.Euler(0, 0, 0));
                break;
        }

    }

    protected override void Die()
    {
        moveSettings.runVelocity = 0;
        state = State.dead;
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        anim.SetBool("alive", false);
        anim.SetBool("dying", true);
        yield return new WaitForEndOfFrame();
        anim.SetBool("dead", true);
        anim.SetBool("dying", false);
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Drop();
        anim.SetBool("dead", false);
        anim.SetBool("alive", true);
    }

    [System.Serializable]
    public new class MoveSettings
    {
        public float runVelocity = 0.5f;
        public float jumpVelocity = 0;
        public float distanceToGround = 1;
        public LayerMask ground;
    }
}
