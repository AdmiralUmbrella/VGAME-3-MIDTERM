using UnityEngine;

[CreateAssetMenu(fileName = "FallingObjectData", menuName = "FallingObjects/Settings", order = 1)]
public class FallingObjectData : ScriptableObject
{
    [Header("Configuración de Spawn")]
    public float spawnInterval = 2.0f;

    [Header("Configuración de Caída")]
    public float fallSpeed = 2.0f;
}
