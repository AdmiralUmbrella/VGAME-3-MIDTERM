using UnityEngine;

[CreateAssetMenu(fileName = "FallingObjectData", menuName = "FallingObjects/Settings", order = 1)]
public class FallingObjectData : ScriptableObject
{
    [Header("Configuraci�n de Spawn")]
    public float spawnInterval = 2.0f;

    [Header("Configuraci�n de Ca�da")]
    public float fallSpeed = 2.0f;
}
