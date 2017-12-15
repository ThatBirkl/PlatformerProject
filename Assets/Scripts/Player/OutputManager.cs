using UnityEngine;
using System.Collections;

public class OutputManager : MonoBehaviour
{
	//GameManager hängt am Player => der GM kann zu beginn der Szene eine AudioSource-Referenz an den OM übergeben
	private static AudioSource audioSource;
	private static AudioClip addedMaxLives;
	private static AudioClip addedLives;
	private static AudioClip addedEnergy;
	private static AudioClip addedBones;
	private static AudioClip death;
	private static AudioClip damageTaken;
	
	public static void Prepare(AudioSource _audioSource)
	{
		//Load all AudioClips
		//Called from GM
		
		audioSource = _audioSource;
	}
	
	private static void Vibrate()
	{
		
	}
	
	private static void PlaySound(string Type)
	{
		
	}
	
	public static void DamageTaken()
	{
		
	}
	
	public static void Death()
	{
		
	}
	
	public static void AddedMaxLives()
	{
		
	}
	
	public static void AddedLives()
	{
		
	}
	
	public static void AddedEnergy()
	{
		
	}
	
	public static void AddedBones()
	{
		
	}
	
	public static void AddedAbility()
	{
		
	}
}