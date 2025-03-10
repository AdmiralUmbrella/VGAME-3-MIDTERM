using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5.0f;
    // Límites horizontales del movimiento (ajusta según tu escena)
    public float xMin = -8.0f;
    public float xMax = 8.0f;

    void Update()
    {
        // Obtiene la entrada horizontal (teclas flecha o A/D)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcula el movimiento basado en la entrada, la velocidad y el tiempo transcurrido
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Limita la posición del jugador para que no salga de los límites definidos
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        transform.position = clampedPosition;
    }
}
