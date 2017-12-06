using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBehaviour : EnemyBehaviour
{
    protected enum Action {jump, shoot, nothing }
    protected Action action;
    private GameObject player;
    private Vector2 playerPosition;
    private GameObject projectile;
    private float border;
    [SerializeField]
    private LayerMask p;
    [SerializeField]
    private LayerMask enemy;

    protected override void Start()
    {
        base.Start();
        lookingRight = false;
        type = "Hopper";
        lives = 20;
        damage = 20;
        projectile = Resources.Load<GameObject>("Prefabs/Projectiles/Hopper_Projectile");//nachher noch Name einfügen
        border = 6;
        action = Action.nothing;
        moveSettings.runVelocity = 2;
        moveSettings.jumpVelocity = 6;
    }

    protected void Update()
    {
        playerPosition = GameManager.player.transform.position;
        if (player == null)
        {
            if (FoundPlayer())
            {
                player = GameManager.player;
            }
            else
            {
                player = null;
            }
        }
    }

    private void LateUpdate()
    {
        CheckPlayer();
    }

    #region Movement

    protected override void Move()
    {
        if (player == null)
        {
            if (action == Action.nothing && (Grounded() || OnEnemy()))
            {
                action = Action.jump;
                int direction = Random.Range(-1, 2); // random left or right
                while (direction != 1 && direction != -1)
                {
                    direction = Random.Range(-1, 2);
                }
                if (((direction == -1 && lookingRight) || (direction == 1 && !lookingRight)) && (Grounded() || OnEnemy())) //flipping sprite
                {
                    lookingRight = !lookingRight;
                    Vector2 scale = gameObject.transform.localScale;
                    scale.x *= -1;
                    gameObject.transform.localScale = scale;
                }
                velocity.x = direction * moveSettings.runVelocity;
                velocity.y = moveSettings.jumpVelocity;
                StartCoroutine(Jump());
            }
            else
            {
                velocity.x = enemyRigidbody.velocity.x;
                velocity.y = enemyRigidbody.velocity.y;
            }
            enemyRigidbody.velocity = velocity;
        }
        else
        {
            int beh = Random.Range(1, 3);
            switch (beh)
            {
                case 1:
                    MoveTowards();
                    break;
                case 2:
                    Shoot();
                    break;
                default:
                    print("Error");
                    break;
            }
        }
    }

    private void CheckPlayer()
    {
        if (player != null)
        {
            if (player.transform.position.x - gameObject.transform.position.x <= border &&
            player.transform.position.x - gameObject.transform.position.x >= (border * -1) &&
            player.transform.position.y - gameObject.transform.position.y <= border &&
            player.transform.position.y - gameObject.transform.position.y >= (border * -1)) //player inside borders
            {
                ;
            }
            else //player outside borders
            {
                player = null;
            }
        }
    }

    private void MoveTowards()
    {
        if (action == Action.nothing && (Grounded()  || OnEnemy()))
        {
            action = Action.jump;
            int direction;
            if (player.transform.position.x > gameObject.transform.position.x) //player on the right side of the enemy
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            if (((direction == -1 && lookingRight) || (direction == 1 && !lookingRight)) && (Grounded() || OnEnemy())) //flipping sprite
            {
                lookingRight = !lookingRight;
                Vector2 scale = gameObject.transform.localScale;
                scale.x *= -1;
                gameObject.transform.localScale = scale;
            }//applying force
            velocity.x = direction * moveSettings.runVelocity;
            velocity.y = moveSettings.jumpVelocity;
            StartCoroutine(Jump());
        }
        else
        {
            velocity.x = enemyRigidbody.velocity.x;
            velocity.y = enemyRigidbody.velocity.y;
        }
        enemyRigidbody.velocity = velocity;
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(2);
        action = Action.nothing;
    }

    private bool OnEnemy()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 2, enemy);
    }

    [System.Serializable]
    public new class MoveSettings
    {
        public float runVelocity = 2;
        public float jumpVelocity = 4;
        public float distanceToGround = 1;
        public LayerMask ground;
    }

    #endregion

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        //Nothing in here
        //Detection of player in Raycast method
    }

    private bool FoundPlayer()
    {
        Vector2 dir = ((Vector2)gameObject.transform.position - playerPosition) * -1;
        return Physics2D.Raycast(gameObject.transform.position, dir, border, p);
    }

    protected override void EnemyCollision(Collision2D col)
    {
        //had to include so collision between two hoppers won't be ignored
    }

    public override void Drop()
    {
        Vector2 dropposition = transform.position;
        dropposition.y = dropposition.y - (transform.localScale.y * 0.2f);

        int drop = Random.Range(1, 4);
        switch (drop)
        {
            case 1:
                if (Random.Range(1, 3) == 1) //2 Energy drops
                {
                    dropposition.x += 0.5f;
                    Instantiate(DropManager.energy, dropposition, Quaternion.Euler(0, 0, 0));
                    dropposition.x -= 1;
                    Instantiate(DropManager.energy, dropposition, Quaternion.Euler(0, 0, 0));
                }
                else //only 1
                {
                    Instantiate(DropManager.energy, dropposition, Quaternion.Euler(0, 0, 0));
                }
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
        base.Die();
    }

    private void Shoot()
    {
        if (action == Action.nothing && Grounded())
        {
            action = Action.shoot;
            float dir = player.transform.position.x - gameObject.transform.position.x;
            if (dir < 0)//player left
            {
                if (lookingRight && (Grounded() || OnEnemy()))
                {
                    lookingRight = false;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                Vector3 pos = gameObject.transform.position;
                pos.x -= gameObject.transform.localScale.x / 2;
                Instantiate(projectile, pos, Quaternion.Euler(0, 0, 0));
            }
            else //player right
            {
                if (!lookingRight && (Grounded() || OnEnemy()))
                {
                    lookingRight = true;
                    Vector3 s = transform.localScale;
                    s.x *= -1;
                    transform.localScale = s;
                }
                Vector3 pos = gameObject.transform.position;
                pos.x -= gameObject.transform.localScale.x * 0.7f;
                Vector2 scale = projectile.transform.localScale;
                scale.x *= -1;
                projectile.transform.localScale = scale;
                Instantiate(projectile, pos, Quaternion.Euler(0, 0, 0));
                scale.x *= -1;
                projectile.transform.localScale = scale;

            }
            StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(2);
        action = Action.nothing;
    }

    #region Rewind
    #endregion
}
