using UnityEngine;
using System.Collections;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool active = false;
    private Animator anim;
    private GameObject ButtonPrompt;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (active)
        {
            Activate();
        }
        ButtonPrompt = Resources.Load<GameObject>("Prefabs/Interface/ButtonPromptMask");
    }

    public void Activate()
    {
        DestroyButtonPrompt();
        active = true;
        anim.SetBool("isActive", active);
    }

    public void Deactivate()
    {
        active = false;
        anim.SetBool("isActive", active);
    }

    public bool isActive()
    {
        return active;
    }

    public void SpawnButtonPrompt()
    {
        Vector3 pos = transform.position;
        pos.y += 2;
        GameObject prompt = Instantiate(ButtonPrompt, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        prompt.transform.SetParent(gameObject.transform);
    }

    public void DestroyButtonPrompt()
    {
        if (!active)
        {
            GameObject prompt = gameObject.transform.GetChild(0).gameObject;
            Object.Destroy(prompt);
        }
    }
}
