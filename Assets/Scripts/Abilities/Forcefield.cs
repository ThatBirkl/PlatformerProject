using UnityEngine;
using System.Collections;

public class Forcefield : MonoBehaviour
{
    private Vector2 MaxScale;
    private Vector2 scale;
    private GameObject player;
    private Vector3 offset;
    private enum State {grow, dead, alive }
    private State state;

	void Start ()
    {
        player = GameManager.player;
        offset = transform.position - player.transform.position;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        MaxScale = gameObject.transform.localScale;
        scale.x = gameObject.transform.localScale.x * 0.001f;
        scale.y = gameObject.transform.localScale.y * 0.001f;
        StartCoroutine(Grow());
    }

    void LateUpdate()
    {
        Follow();
    }

	void Update ()
    {
        if (state == State.alive || state == State.grow)
        {
            //First: Reduce Energy by 15
            GameManager.addEnergy(-15, GameManager.currentlyPlaying);
            //Second: Check if Key released oder Energie aus, etc
            CheckIfCanceled();
        }
	}

    IEnumerator Grow()
    {
        if (scale.x != MaxScale.x && scale.y != MaxScale.y && state == State.grow)
        {
            if (scale.x * 1.001f <= MaxScale.x && scale.y * 1.001f <= MaxScale.y)
            {
                scale.x *= 2f;
                scale.y *= 2f;
                gameObject.transform.localScale = scale;
                yield return new WaitForEndOfFrame();
                StartCoroutine(Grow());
            }
            else
            {
                scale.x = MaxScale.x;
                scale.y = MaxScale.y;
                gameObject.transform.localScale = scale;
                state = State.alive;
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator Shrink()
    {
        if (scale.x > MaxScale.x * 0.001f || scale.y > MaxScale.y * 0.001f)
        {
            scale.x *= 0.5f;
            scale.y *= 0.5f;
            gameObject.transform.localScale = scale;
            yield return new WaitForEndOfFrame();
            StartCoroutine(Shrink());
        }
        else
        {
            Object.Destroy(gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckIfCanceled()
    {
        //Energy has run out. Forcefield has to be canceled
        //GiveEnergy only needs true, bc only White can cast Forcefields
        if (GameManager.GiveEnergy(true) <= 0)
        {
            state = State.dead;
            StartCoroutine(Shrink());
            player.GetComponent<WhiteBehaviour>().protect = false;
        }
        else
        {
            //Forcefield is Primary
            if (GameManager.GivePrimary(true) == 2 && Input.GetButtonUp("Primary"))
            {
                state = State.dead;
                StartCoroutine(Shrink());
                player.GetComponent<WhiteBehaviour>().protect = false;
            }
            //Forcefield is Secundary
            if (GameManager.GiveSecundary(true) == 2 && Input.GetButtonUp("Secundary"))
            {
                state = State.dead;
                StartCoroutine(Shrink());
                player.GetComponent<WhiteBehaviour>().protect = false;
            }
        }
    }

    private void Follow()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
}
