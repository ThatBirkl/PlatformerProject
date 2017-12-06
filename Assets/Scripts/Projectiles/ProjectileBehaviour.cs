using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour, RInterface
{
    protected new Rigidbody2D rigidbody;
    protected Vector2 velocity;
    protected float velX;
    protected float velY;
    protected int damage;
    [SerializeField]
    protected Animator anim;
    protected Rewind trscript;

    protected virtual void Start()
    {
        //In child: First it's own specified stuff, then base.Start()
        trscript = GetComponent<Rewind>();
        //assigning values to velX and velY individually in each sub-class
        rigidbody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(velX, velY);
        rigidbody.velocity = velocity;
        StartCoroutine(Die());
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        UnityEngine.Object.Destroy(gameObject);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(20);
        UnityEngine.Object.Destroy(gameObject);
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
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Reactivate()
    {
        velocity = new Vector2(velX, velY);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    protected class MyStatus : RObject
    {
        public Vector3 myPosition;
        public Vector3 velocity;
    }
    #endregion
}
