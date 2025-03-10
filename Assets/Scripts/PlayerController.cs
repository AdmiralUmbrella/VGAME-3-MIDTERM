using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5.0f;
    // L�mites horizontales del movimiento (ajusta seg�n tu escena)
    public float xMin = -8.0f;
    public float xMax = 8.0f;

    // Vidas del jugador
    public int lives = 5;

    void Update()
    {
        // Obtiene la entrada horizontal (teclas flecha o A/D)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcula el movimiento basado en la entrada, la velocidad y el tiempo transcurrido
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Limita la posici�n del jugador para que no salga de los l�mites definidos
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        transform.position = clampedPosition;
    }

    // Detecta colisiones con objetos que tengan un componente FallingObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BadThings"))
        {
            lives--;
            Debug.Log("�Has perdido una vida! Vidas restantes: " + lives);

            // Si a�n no se destruye el objeto por su propio script, se puede forzar su destrucci�n
            // Destroy(other.gameObject);

            if (lives <= 0)
            {
                Debug.Log("�Game Over!");
                // Aqu� puedes implementar la l�gica para finalizar el juego, reiniciar la escena, etc.
            }
        }
        else if (other.gameObject.CompareTag("GoodThings"))
        {
            Debug.Log("Nice");
        }
    }
}

