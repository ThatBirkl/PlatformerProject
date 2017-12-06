using UnityEngine;
using System.Collections;

public class CollectibleBehaviour : MonoBehaviour
{
    public virtual void DoYourThing()
    {
        Object.Destroy(gameObject);
    }
}
