using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Asigna la instancia de PlayerData desde el Inspector
    public PlayerData playerData;

    private void Awake()
    {
        playerData.goodThingsCollected = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta colisión con objetos que tengan el tag "FallingObject"
        if (other.CompareTag("BadThings"))
        {
            playerData.TakeDamage();
            Debug.Log("Has perdido una vida. Vidas restantes: " + playerData.currentLives);

            if (playerData.currentLives <= 0)
            {
                Debug.Log("¡Game Over!");
                // Aquí puedes agregar la lógica para reiniciar el juego o mostrar un menú de Game Over
            }
        }
    }
}
