using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour, RInterface
{
    [SerializeField]
    private Vector2 velocity;
    private Vector2 velocityBackup;
    private new Rigidbody2D rigidbody;
    protected Rewind trscript;
    [SerializeField]
    protected float speed = 0.5f;

    private void Start()
    {
        trscript = GetComponent<Rewind>();
        rigidbody = GetComponent<Rigidbody2D>();
        int i = 0;
        while (i != 1 && i != -1)
        {
            i = UnityEngine.Random.Range(-1, 2);
        }
        velocity = new Vector2(speed * i, 0);
        velocityBackup = velocity;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            col.transform.SetParent(gameObject.transform);
        }
        else
        {
            velocity.x *= -1;
            if (col.gameObject.tag == "Projectile")
            {
                velocity.x *= -1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "End")
        {
            velocity.x *= -1;
        }
    }

    #region Rewind
    public void SaveTRObject()
    {
        MyStatus status = new MyStatus();
        status.myPosition = transform.position;
        trscript.PushTRObject(status);
    }

    public void LoadTRObject(RObject robject)
    {
        MyStatus newStatus = (MyStatus)robject;
        velocity = Vector2.zero;
        transform.position = newStatus.myPosition;
    }

    public void Stop()
    {
        velocity = Vector2.zero;
    }

    public void Reactivate()
    {
        velocity = velocityBackup;
    }

    protected class MyStatus : RObject
    {
        public Vector3 myPosition;
        public Vector3 velocity;
    }
    #endregion
}
