using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private string type; //Set type of hazard like fire etc
    private Animator anim;

    private void Awake()
    {
        if (type.Equals("Fire"))
        {
            anim = GetComponent<Animator>();
            StartCoroutine(RandomAnimation(true));
        }
    }

    public void DoYourThing(GameObject obj , int i)
    {
        if (type.Equals("Fire"))
        {
            Fire(obj, i);
        }
    }

    #region Fire
    private void Fire(GameObject obj, int i)
    {
        if (obj.tag == "Player" && i < 0)
        {
            GameManager.addLives( - 5, GameManager.currentlyPlaying);
        }
        if (obj.tag == "Player" && i > 0)
        {
            GameManager.addLives(5, GameManager.currentlyPlaying);
        }
        if (obj.tag == "Enemy" && i < 0)
        {
            obj.GetComponent<EnemyBehaviour>().ManipulateLives(-5);
        }
        if (obj.tag == "Enemy" && i > 0)
        {
            obj.GetComponent<EnemyBehaviour>().ManipulateLives(5);
        }
    }

    IEnumerator RandomAnimation(bool weiter)
    {
        if (type.Equals("Fire"))
        {
            int i = Random.Range(1, 1000);
            if (i == 1)
            {
                anim.SetBool("weiter", true);
                weiter = false;
            }
            else
            {
                if (weiter)
                {
                    yield return new WaitForFixedUpdate();
                    StartCoroutine(RandomAnimation(weiter));
                }
            }
        }
    }
    #endregion
}
