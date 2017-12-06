using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class PlayerBehaviour : MonoBehaviour
{
    #region Stats
    public enum State { playing, dead, respawning}
    public State state;
    public GameObject Spawnpoint;
    public Camera cam;
    public bool character;

    #region Movement Fields
    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    private Rigidbody2D playerRigidbody;
    protected Vector2 velocity;
    private float sidewaysInput;
    private float jumpInput;
    private bool facingRight; //needed to mirror the character when moving in the opposite direction
    #endregion

    #region Damage Fields
    public bool protect = false; //will be set to true, while abilities like forcefield etc are active
    private enum Damage {damageOverTime, hit, nothing }
    private Damage damage;
    #endregion

    #endregion

    protected virtual void SetCharacter()
    {

    }

    protected void Awake()
    {
        velocity = Vector2.zero;
        sidewaysInput = 0;
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        state = State.playing;
        facingRight = true;
        moveSettings.distanceToGround = transform.localScale.y / 1.5f;
    }

    void Update()//Input
    {
        if (state == State.playing)
        {
            GetInput();
            Abilities();
        }
    }

    void FixedUpdate()//Physics
    {
        if (state == State.playing)
        {
            Move();
            Jump();
        }

    }

    void GetInput()
    {
        //Rework this so: 
        //sidewaysInput = InputManager.GetHorizontalInput();
        //jumpInput = InputManager.GetJumpInput();
        //Look into inputSettings!!
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
        {
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
            jumpInput = Input.GetAxis(inputSettings.JUMP_AXIS);
        }
    }

    #region Movement Methods
    protected virtual void Move()
    {
        if (damage != Damage.hit)
        {
            velocity.x = sidewaysInput * moveSettings.runVelocity;
            velocity.y = playerRigidbody.velocity.y;
            //first I need to set the velocity in my cache; then adjust the sprite and then translate it to the rigidbody

            //first: flipping sprite
            if (sidewaysInput < 0 && facingRight) //facing right but input left
            {
                facingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            if (sidewaysInput > 0 && !facingRight)//facing left but input right
            {
                facingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            //then adjusting x-velocity to move to the left when facing left
            //Since the last update I don't have to mirror the velocity.x when facing left; it's done automatically
        }
        if (transform.parent != null)
        {
            if (transform.parent.tag == "Moving")
            {
                velocity += transform.parent.GetComponent<Rigidbody2D>().velocity;
            }
        }
        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Jump()
    {
        if (damage != Damage.hit)
        {
            if (/*(Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) &&*/ Grounded())
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, moveSettings.jumpVelocity * jumpInput);
            }
        }
    }

    public bool Grounded()
    {
        //middle raycast
        bool m = Physics2D.Raycast(transform.position, Vector2.down, moveSettings.distanceToGround, moveSettings.ground);
        //left raycast
        Vector2 pos = transform.position;
        pos.x = pos.x - transform.localScale.x/2;
        bool l = Physics2D.Raycast(pos, Vector2.down, moveSettings.distanceToGround, moveSettings.ground);
        //right raycast
        pos = transform.position;
        pos.x = pos.x + transform.localScale.x / 2;
        bool r = Physics2D.Raycast(pos, Vector2.down, moveSettings.distanceToGround, moveSettings.ground);

        if (l || m || r)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [System.Serializable]
    public class MoveSettings
    {
        public float runVelocity = 6;
        public float jumpVelocity = 6;
        public float distanceToGround = 1;
        public LayerMask ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }
    #endregion

    public void Respawn()
    {
        PresentationBossFight.active = false;
        StartCoroutine(Respawn(1));
    }

    IEnumerator Respawn(int i)
    {
        velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        transform.position = Spawnpoint.transform.position;
        cam.GetComponent<CameraBehaviour>().Respawn();
        GameManager.ResetEnergy(character);
        GameManager.ResetLives(character);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Player respawns when he hits the Deathzone
        if (col.gameObject.tag == "Deathzone")
        {
            Respawn();
        }

        //When player touches collectible it does it's thing
        if (col.tag == "Collectible")
        {
            col.gameObject.GetComponent<CollectibleBehaviour>().DoYourThing();
        }
        //Hazards
        if (col.tag == "Hazard")
        {
            damage = Damage.damageOverTime;
            StartCoroutine(DamageOverTime(col));
        }
        //Spawnpoint
        if (col.tag == "Spawnpoint" && !col.GetComponent<SpawnBehaviour>().isActive())
        {
            col.GetComponent<SpawnBehaviour>().SpawnButtonPrompt();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Hazard")
        {
            damage = Damage.nothing;
        }
        if (col.tag == "Spawnpoint" && !col.GetComponent<SpawnBehaviour>().isActive())
        {
            col.GetComponent<SpawnBehaviour>().DestroyButtonPrompt();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //When the player touches a Spawnpoints collider and presses E then it is set as the player's new Spawnpoint
        if (col.gameObject.tag == "Spawnpoint")
        {
            //Still need to add some kind of dialogue option that shows that you can interact
            //Got problems with key-detection for the E; Need to look into it again
            if (!col.gameObject.GetComponent<SpawnBehaviour>().isActive()) //Stuff only happens if the spawnpoint isn't already the player's
            {
                if (Input.GetButtonDown("Interact"))
                {
                    Spawnpoint.GetComponent<SpawnBehaviour>().Deactivate();
                    Spawnpoint = col.gameObject;
                    Spawnpoint.GetComponent<SpawnBehaviour>().Activate();
                }
            }
            
        }
    }

    IEnumerator DamageOverTime(Collider2D col)
    {
        if (damage == Damage.damageOverTime)
        {
            if (!protect)
            {
                col.GetComponent<Hazard>().DoYourThing(gameObject, -1);
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DamageOverTime(col));
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Projectile")
        {
            StartCoroutine(EnemyCollision(col));
        }
    }

    IEnumerator EnemyCollision(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyBehaviour>().subtractLives(character);
        }
        damage = Damage.hit;
        velocity = gameObject.transform.position - col.gameObject.transform.position;
        velocity *= 2;
        yield return new WaitForSeconds(0.5f);
        velocity = Vector2.zero;
        damage = Damage.nothing;
    }

    /** This Method will be called every Update
     * In this method all ability methods will be called if the player has to use them directly
     * In each of the ability methods there will be checked if all requirements are met
     **/
    public virtual void Abilities()
    {

    }
}
