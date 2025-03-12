using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Referencia al ScriptableObject con la configuración")]
    public FallingObjectData objectData;

    [Header("Lista de objetos a spawnear (GoodThings, BadThings, SpecialThings)")]
    public List<SpawnableItem> spawnableItems;

    [Header("Área de Spawn (BoxCollider)")]
    public BoxCollider spawnArea;

    void Awake()
    {
        objectData.spawnInterval = 2.0f;
        objectData.fallSpeed = 2.0f;
          
    }

    void Start()
    {
        if (spawnArea == null)
        {
            Debug.LogError("No se ha asignado un BoxCollider para el área de spawn.");
            return;
        }
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(objectData.spawnInterval);

            // Obtiene una posición aleatoria dentro del área: solo se randomiza el eje X
            Vector3 spawnPosition = GetRandomPositionInBox();

            // Selecciona un objeto basado en el spawn rate
            GameObject selectedPrefab = GetRandomSpawnable();
            if (selectedPrefab != null)
            {
                GameObject obj = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                // Asigna la velocidad de caída desde el ScriptableObject
                FallingObject fallingObj = obj.GetComponent<FallingObject>();
                if (fallingObj != null)
                {
                    fallingObj.fallSpeed = objectData.fallSpeed;
                }
            }
        }
    }

    Vector3 GetRandomPositionInBox()
    {
        Bounds bounds = spawnArea.bounds;
        // Solo randomizamos el eje X
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        // Los ejes Y y Z se fijan en el centro del BoxCollider
        float fixedY = bounds.center.y;
        float fixedZ = bounds.center.z;
        return new Vector3(randomX, fixedY, fixedZ);
    }

    // Selecciona un prefab basado en el spawn rate asignado a cada objeto
    GameObject GetRandomSpawnable()
    {
        float totalRate = 0f;
        foreach (var item in spawnableItems)
        {
            totalRate += item.spawnRate;
        }

        float randomValue = Random.Range(0f, totalRate);
        float cumulative = 0f;
        foreach (var item in spawnableItems)
        {
            cumulative += item.spawnRate;
            if (randomValue <= cumulative)
            {
                return item.prefab;
            }
        }
        return null;
    }

    // Método público para incrementar la velocidad de caída (por ejemplo, al recolectar "GoodThings")
    public void IncreaseFallSpeed(float increment)
    {
        objectData.fallSpeed += increment;
        Debug.Log("Nueva velocidad de caída: " + objectData.fallSpeed);
    }
}
