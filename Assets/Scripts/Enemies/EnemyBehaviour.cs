using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour, RInterface
{
    #region Fields
    public Vector2 velocity;
    protected Rigidbody2D enemyRigidbody;
    public MoveSettings moveSettings;
    public Velocity vel;
    public enum State { alive, dead }
    public State state;
    [SerializeField]
    public float distance;
    public int damage;
    public int lives;
    protected Rewind trscript;
    private bool damageIntake;
    protected Animator anim;
    public bool lookingRight;
    public string type;
    #endregion

    protected virtual void Start()
    {
        trscript = GetComponent<Rewind>();
        anim = gameObject.GetComponent<Animator>();
        lookingRight = true;
    }

    void Awake () 
    {
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        state = State.alive;
    }

    void FixedUpdate() //Physics
    {
        if (GameManager.Paused && gameObject.GetComponent<Rewind>() != null)
        {
            return;
        }

        if (state == State.alive && lives > 0)
        {
            Move();
        }
    }

    #region Movement
    protected virtual void Move()
    {
        if (state == State.alive && lives > 0)
        {
            velocity.x = moveSettings.runVelocity;
            velocity.y = enemyRigidbody.velocity.y;
            if (!lookingRight)
            {
                velocity.x *= -1;
            }
            if (gameObject.transform.parent != null)
            {
                if (transform.parent.tag == "Moving") //<reason?>
                {
                    velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
                }
            }
            enemyRigidbody.velocity = transform.TransformDirection(velocity);
        }
    }

    public bool Grounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, moveSettings.distanceToGround, moveSettings.ground);
    }

    [System.Serializable]
    public class MoveSettings
    {
        public float runVelocity = 12;
        public float jumpVelocity = 0;
        public float distanceToGround = 1;
        public LayerMask ground;
    }
    #endregion

    public virtual void subtractLives(bool character)
    {
        GameManager.addLives(-damage, character);
    }

    /** Chooses item from droptable
     * The item is picked by the percentage that is noted in the droptable spreadsheet
     * This method is called from the Die() method of the enemy
     * The enemy will not be deleted; just set invicible, collider off and rigidbody kinematic
     * */
    public virtual void Drop()
    {
        print("wrong");
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Forcefield")
        {
            //On collision with a block to the enemy's side it will change it's direction and mirror it's sprite
            if (col.gameObject.transform.position.x != transform.position.x)
            {
                if (col.collider.bounds.center.y + col.collider.bounds.extents.y > transform.position.y - distance * transform.localScale.y && Grounded())
                {
                    //Flip sprite, collider and direction. All set! Bam!
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                    lookingRight = !lookingRight;
                }
            }
        }

        if (col.gameObject.tag == "Enemy")
        {
            EnemyCollision(col);
        }
    }

    protected virtual void EnemyCollision(Collision2D col)
    {
        if (col.gameObject.GetComponent<EnemyBehaviour>().type.Equals(type))
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "End") //EndCabs
        {
            //Flip sprite, collider and direction. All set! Bam!
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            lookingRight = !lookingRight;
        }

        //Hazards
        if (col.tag == "Hazard")
        {
            damageIntake = true;
            StartCoroutine(DamageOverTime(col));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Hazard")
        {
            damageIntake = false;
        }
    }

    IEnumerator DamageOverTime(Collider2D col)
    {
        if (damageIntake && state == State.alive)
        {
            col.GetComponent<Hazard>().DoYourThing(gameObject, -1);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DamageOverTime(col));
        }
    }

    public void ManipulateLives(int dmg)
    {
        lives += dmg;
        if (lives <= 0 && state == State.alive)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        state = State.dead;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Drop();
    }

    #region Rewind
    public virtual void SaveTRObject()
    {
        MyStatus status = new MyStatus();
        status.myPosition = transform.position;
        status.myScale = transform.localScale;
        enemyRigidbody.isKinematic = false;
        status.s = state;
        status.l = lives;
        status.ren = gameObject.GetComponent<SpriteRenderer>().enabled;
        status.col = gameObject.GetComponent<Collider2D>().enabled;
        status.runvelocity = moveSettings.runVelocity;
        status.velocity = velocity;
        status.LookingRight = lookingRight;
        trscript.PushTRObject(status);
    }

    public  virtual void LoadTRObject(RObject robject)
    {
        MyStatus newStatus = (MyStatus)robject;
		enemyRigidbody.isKinematic = true;
        transform.position = newStatus.myPosition;
        transform.localScale = newStatus.myScale;
        state = newStatus.s;
        lives = newStatus.l;
        gameObject.GetComponent<SpriteRenderer>().enabled = newStatus.ren;
        gameObject.GetComponent<Collider2D>().enabled = newStatus.col;
        moveSettings.runVelocity = newStatus.runvelocity;
        vel.velocity = newStatus.velocity;
        vel.pos = newStatus.myPosition;
        lookingRight = newStatus.LookingRight;
    }

    public void Stop()
    {
        velocity = Vector2.zero;
        gameObject.transform.position = vel.pos;
    }

    public void Reactivate()
    {
        velocity = vel.velocity;
        vel.velocity = Vector2.zero;
        vel.pos = Vector3.zero;
    }

    protected class MyStatus : RObject
    {
        public Vector3 myPosition;
        public Vector3 myScale;
        public State s;
        public int l;
        public bool col;
        public bool ren;
        public float runvelocity;
        public Vector2 velocity;
        public bool LookingRight;
    }

    [System.Serializable]
    public class Velocity
    {
        public Vector2 velocity = Vector2.zero;
        public Vector3 pos = Vector3.zero;
    }
    #endregion
}
