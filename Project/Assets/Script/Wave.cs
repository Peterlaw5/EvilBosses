using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spawn
{
    public GameObject bullet;
    public Vector3 position;
    public float delaysSpawn;
}

[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public Spawn[] spawns;
    public float durationWave;
}
