using UnityEngine;
using System.Collections;

public class SpawnBehaviour : Interactible
{
    [SerializeField]
    private bool active = false;
    private Animator anim;

    protected override void Start()
    {
        promptPath = "Prefabs/Interface/ButtonPromptMask";
        anim = gameObject.GetComponent<Animator>();
        if (active)
        {
            Activate();
        }
        base.Start();
    }

    public override void Activate()
    {
        base.Activate();
        active = true;
        anim.SetBool("isActive", active);
    }

    public override void Deactivate()
    {
        active = false;
        anim.SetBool("isActive", active);
    }

    public bool isActive()
    {
        return active;
    }

    public override void DestroyButtonPrompt()
    {
        if (!active)
        {
            base.DestroyButtonPrompt();
        }
    }

}
