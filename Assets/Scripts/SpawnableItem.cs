using UnityEngine;

[System.Serializable]
public class SpawnableItem
{
    public GameObject prefab;
    [Tooltip("Valor relativo del spawn rate. La suma de todos determina la probabilidad")]
    public float spawnRate;
}