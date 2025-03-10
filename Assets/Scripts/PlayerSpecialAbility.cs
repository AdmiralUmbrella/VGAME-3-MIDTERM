using UnityEngine;

public class PlayerSpecialAbility : MonoBehaviour
{
    // Asigna la misma instancia de PlayerData
    public PlayerData playerData;
    // Cantidad que se aumenta el medidor al recoger cada GoodThing
    public float specialMeterIncreaseAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoodThings"))
        {
            playerData.IncreaseSpecialMeter(specialMeterIncreaseAmount);
            playerData.IncreaseGoodThingsCount();
            Debug.Log("GoodThings recogidos: " + playerData.goodThingsCollected);

            if (playerData.ShouldIncreaseSpeed())
            {
                IncreaseFallSpeedForAll();
            }

            Destroy(other.gameObject);
        }
    }

    void IncreaseFallSpeedForAll()
    {
        // Encuentra todos los spawners en la escena y actualiza la velocidad de caída global
        Spawner[] spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        foreach (Spawner spawner in spawners)
        {
            spawner.spawnInterval -= 1f;
            spawner.IncreaseGlobalFallSpeed(1.5f); // Aumenta en 50%
        }

        // También afecta a los objetos que ya están cayendo
        FallingObject[] fallingObjects = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);
        foreach (FallingObject obj in fallingObjects)
        {
            obj.fallSpeed *= 1.5f;
        }

        Debug.Log("¡Todos los Spawners y objetos en la escena han aumentado la velocidad de caída!");
    }


    void ActivateSpecialAbility()
    {
        GameObject[] badThings = GameObject.FindGameObjectsWithTag("BadThings");
        foreach (GameObject bad in badThings)
        {
            Destroy(bad);
        }
        Debug.Log("¡Habilidad especial activada! Se eliminaron " + badThings.Length + " objetos malos.");
        playerData.ResetSpecialMeter();
    }
}
