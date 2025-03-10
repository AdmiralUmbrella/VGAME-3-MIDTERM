using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public float spawnInterval = 2.0f;
    public Transform spawnPoint;
    private float currentFallSpeed = 2.0f; // Velocidad base inicial

    void Start()
    {
        if (spawnPoint == null)
            spawnPoint = this.transform;

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, spawnObjects.Length);
            GameObject newObj = Instantiate(spawnObjects[randomIndex], transform.position, Quaternion.identity);

            // Asigna la velocidad actualizada a los nuevos objetos
            FallingObject fallingObj = newObj.GetComponent<FallingObject>();
            if (fallingObj != null)
            {
                fallingObj.fallSpeed = currentFallSpeed;
            }
        }
    }

    // Método para actualizar la velocidad de caída globalmente
    public void IncreaseGlobalFallSpeed(float multiplier)
    {
        currentFallSpeed *= multiplier;
    }
}
