using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public string lvl; //Is entered to determine which music should be played. Makes it usable in all types of lvls
    private static AudioClip currentClip;
    #region Grassland
    //Grassland has 3 short tracks
    private static AudioClip grassland1;
    private static AudioClip grassland2;
    private static AudioClip grassland3;
    private static AudioClip grasslandBoss;
    #endregion

    private void Awake()
    {
        LoadClips();
        //Here is checked which lvl we currently got going
        if (lvl.Equals("Grassland"))
        {
            ChooseGrasslandTrack();
        }
    }

    private void LoadClips()
    {
        grassland1 = Resources.Load<AudioClip>("Sounds/Grassland Theme 1");
        grassland2 = Resources.Load<AudioClip>("Sounds/Grassland Theme 2");
        grassland3 = Resources.Load<AudioClip>("Sounds/Grassland Theme 3");
    }

    IEnumerator Wait(float duration)
    {
        //First: Playing current track

        //This function waits until the track is over;
        //The duration of the track has to be entered manually
        yield return new WaitForSeconds(duration);
        //after that the next track will be chosen randomly
        if (lvl.Equals("Grassland"))
        {
            ChooseGrasslandTrack();
        }
    }

    private void ChooseGrasslandTrack()
    {
        int i = Random.Range(0, 3);
        while (i < 0 && i > 2)
        {
            i = Random.Range(0, 3);
        }
        switch (i)
        {
            case 0:
                currentClip = grassland1;
                StartCoroutine(Wait(0));
                break;
            case 1:
                currentClip = grassland2;
                StartCoroutine(Wait(0));
                break;
            case 2:
                currentClip = grassland3;
                StartCoroutine(Wait(0));
                break;
        }
    }
}
