using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rewind : MonoBehaviour
{
    private CircularBuffer objectsOnStack = new CircularBuffer(500);
    private RInterface otherScript;
    private bool active; //true while game is playing; false when rewinding; basically if objects are active

    void Start()
    {
        active = true;
        otherScript = (RInterface)gameObject.GetComponent(typeof(RInterface));
    }

    void FixedUpdate()
    {
        ///Requirements:
        ///Rewind has to be unlocked
        ///Rewind has to be equipped as primary or secundary
        ///White's energy has to be >0
            if (GameManager.rewind && GameManager.GiveEnergy(true) > 0 && GameManager.GivePrimary(true) == 1)
            {
                if (Input.GetButton("Primary"))
                {
                    if (objectsOnStack.Count > 0)
                    {
                        active = false;
                        GameManager.rewinding = true;
                        otherScript.LoadTRObject(objectsOnStack.Pop());
                    }
                    else
                    {
                        otherScript.Stop();
                        GameManager.rewinding = true;
                    }
                }
                else
                {
                    otherScript.SaveTRObject();
                    GameManager.rewinding = false;
                }
            }
            else
            {
                //first check for primary, then secundary
                if (GameManager.rewind && GameManager.GiveEnergy(true) > 0 && GameManager.GiveSecundary(true) == 1)
                {
                    if (Input.GetButton("Secundary"))
                    {
                        if (objectsOnStack.Count > 0)
                        {
                            active = false;
                            otherScript.LoadTRObject(objectsOnStack.Pop());
                            GameManager.rewinding = true;
                        }
                        else
                        {
                            otherScript.Stop();
                            GameManager.rewinding = true;
                        }
                    }
                    else
                    {
                        GameManager.rewinding = false;
                        otherScript.SaveTRObject();
                    }
                }
                else
                {
                    GameManager.rewinding = false;
                }
            }
        Reactivate();
    }

    public void Reactivate()
    {
        if (!active && ((GameManager.GiveEnergy(true) <= 0)
            ||(GameManager.GivePrimary(true) == 1 && Input.GetButtonUp("Primary"))
            ||(GameManager.GiveSecundary(true) == 1 && Input.GetButtonUp("Secundary"))))
        {
            otherScript.Reactivate();
            active = true;
        }
    }

    public void PushTRObject(RObject robject)
    {
        objectsOnStack.Push(robject);
    }
}
