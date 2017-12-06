using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationBossFight : MonoBehaviour
{
    [SerializeField]
    private GameObject wormy; //spawns alle 10 seconds or something like that
    private GameObject hopper;
    public GameObject hopper1;
    public GameObject hopper2;
    public GameObject hopper3;
    public GameObject hopper4;
    public static bool active;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    public LayerMask p;

    private void Awake()
    {
        active = false;
        pos1 = hopper1.transform.position;
        pos2 = hopper2.transform.position;
        pos3 = hopper3.transform.position;
        pos4 = hopper4.transform.position;
        Object.Destroy(hopper1);
        Object.Destroy(hopper2);
        Object.Destroy(hopper3);
        Object.Destroy(hopper4);
        wormy = Resources.Load<GameObject>("Prefabs/Enemies/Wormy");
        hopper = Resources.Load<GameObject>("Prefabs/Enemies/Hopper");
    }

    private void FixedUpdate()
    {
        if (FoundPlayer() && !active)
        {
            active = true;
            StartCoroutine(Spawn());
            Instan();
        }

        if (active && Check())
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Interface/Box"),
                GameManager.player.transform.position, Quaternion.Euler(0,0,0));
            Object.Destroy(gameObject);
        }

        if (!active)
        {
            Object.Destroy(hopper1);
            hopper1 = null;
            Object.Destroy(hopper2);
            hopper2 = null;
            Object.Destroy(hopper3);
            hopper3 = null;
            Object.Destroy(hopper4);
            hopper4 = null;
        }
        if (active)
        {
            destroying();
        }
    }

    private bool FoundPlayer()
    {
        return Physics2D.Raycast(transform.position, (GameManager.player.transform.position - transform.position), 5, p);
    }

    IEnumerator Spawn()
    {
        Instantiate(wormy, transform.position, Quaternion.Euler(0,0,0));
        yield return new WaitForSeconds(10);
        if (active)
        {
            StartCoroutine(Spawn());
        }
    }

    private void Instan()
    {
        hopper1 = Instantiate(hopper, pos1, Quaternion.Euler(0,0,0));
        hopper2 = Instantiate(hopper, pos2, Quaternion.Euler(0, 0, 0));
        hopper3 = Instantiate(hopper, pos3, Quaternion.Euler(0, 0, 0));
        hopper4 = Instantiate(hopper, pos4, Quaternion.Euler(0, 0, 0));
    }

    private bool Check()
    {
        if (hopper1 == null &&
            hopper2 == null &&
            hopper3 == null &&
            hopper4 == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void destroying()
    {
        if (hopper1 != null)
        {
            if (hopper1.GetComponent<Collider2D>().enabled == false)
            {
                Object.Destroy(hopper1);
                hopper1 = null;
            }
        }
        if (hopper2 != null)
        {
            if (hopper2.GetComponent<Collider2D>().enabled == false)
            {
                Object.Destroy(hopper2);
                hopper2 = null;
            }
        }
        if (hopper3 != null)
        {
            if (hopper3.GetComponent<Collider2D>().enabled == false)
            {
                Object.Destroy(hopper3);
                hopper3 = null;
            }
        }
        if (hopper4 != null)
        {
            if (hopper4.GetComponent<Collider2D>().enabled == false)
            {
                Object.Destroy(hopper4);
                hopper4 = null;
            }
        }
    }
}
