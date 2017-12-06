using UnityEngine;
using System.Collections;

public class DropManager : MonoBehaviour
{
    public static GameObject health = null;
    public static GameObject energy = null;
    public static GameObject bone = null;

    void Start()
    {
        health = (GameObject)Resources.Load("Prefabs/Collectible/Life");
        energy = (GameObject) Resources.Load("Prefabs/Collectible/EnergyBallPrefab");
        bone = (GameObject) Resources.Load("Prefabs/Collectible/Bone");
    }
}
