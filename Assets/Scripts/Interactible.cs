using UnityEngine;
using System.Collections;

public class Interactible : MonoBehaviour
{
    protected GameObject buttonPrompt; //The Prefab
    protected GameObject promptInstance;
    protected string promptPath;

    //private void Update()
    //{
    //    print("+++++++++++++++++++++++++++ " + promptInstance + "+++++++++++++++++++++++++");
    //}

    public GameObject ButtonPrompt
    {
        get { return promptInstance; }
    }

    protected virtual void Start()
    {
        buttonPrompt = Resources.Load<GameObject>(promptPath);
    }

    public virtual void Activate()
    {
        DestroyButtonPrompt();
    }

    public virtual void Deactivate()
    {

    }

    public virtual void SpawnButtonPrompt()
    {
        Vector3 pos = transform.position;
        pos.y += 2;
        promptInstance = Instantiate(buttonPrompt, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        promptInstance.transform.SetParent(gameObject.transform);
        promptInstance.name = promptInstance.name + GameManager.NewID();
        InputManager.AddPrompt(promptInstance);
    }

    public virtual void DestroyButtonPrompt()
    {
        GameObject prompt = gameObject.transform.GetChild(0).gameObject;
        InputManager.RemovePrompt(prompt);
        Object.Destroy(prompt);
    }

}
