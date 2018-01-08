using UnityEngine;
using System.Collections;

public class OutputManager : MonoBehaviour
{
	//GameManager hängt am Player => der GM kann zu beginn der Szene eine AudioSource-Referenz an den OM übergeben
	private static AudioSource audioSource;
	private static AudioClip addedMaxLives;
	private static AudioClip addedLives;
	private static AudioClip addedEnergy;
    private static AudioClip addedMaxEnergy;
	private static AudioClip addedBones;
	private static AudioClip death;
    private static AudioClip addedAbility;
	private static AudioClip damageTaken;
    private static AudioClip respawn;
	
	public static void Prepare(AudioSource _audioSource)
	{
		//Load all AudioClips
		//Called from GM
		
		audioSource = _audioSource;
	}
	
	private static void Vibrate(int times)
	{
        print("called");
        if (Application.platform == RuntimePlatform.Android)
        {
            print("Android");
            for(int i = 0; i < times; i++)
                Handheld.Vibrate();
        }
	}
	
	private static void PlaySound(string Type)
	{
        //until there are actual clips
        return;
        //end


        switch (Type)
        {
            case "death":
                audioSource.clip = death;
                break;
            case "addedMaxLives":
                audioSource.clip = addedMaxLives;
                break;
            case "addedLives":
                audioSource.clip = addedLives;
                break;
            case "addedMaxEnergy":
                audioSource.clip = addedMaxEnergy;
                break;
            case "addedEnergy":
                audioSource.clip = addedEnergy;
                break;
            case "addedBones":
                audioSource.clip = addedBones;
                break;
            case "damageTaken":
                audioSource.clip = damageTaken;
                break;
            case "addedAbility":
                audioSource.clip = addedAbility;
                break;
            case "respawn":
                audioSource.clip = respawn;
                break;
            default:
                break;
        }

        audioSource.Play();
	}
	
	public static void DamageTaken()
	{
        PlaySound("damageTaken");
        Vibrate(1);
    }
	
	public static void Death()
	{
        PlaySound("death");
        Vibrate(3);
    }
	
	public static void AddedMaxLives()
	{
        PlaySound("addedMaxLives");
        Vibrate(0);
    }
	
	public static void AddedLives()
	{
        PlaySound("addedLives");
        Vibrate(0);
    }

    public static void AddedMaxEnergy()
    {
        PlaySound("addedMaxEnergy");
        Vibrate(0);
    }

    public static void AddedEnergy()
	{
        PlaySound("addedEnergy");
        Vibrate(0);
    }
	
	public static void AddedBones()
	{
        PlaySound("addedBones");
        Vibrate(0);
    }
	
	public static void AddedAbility()
	{
        PlaySound("addedAbility");
        Vibrate(1);
    }
}