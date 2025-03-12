using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5.0f;
    // Límites horizontales del movimiento (ajusta según tu escena)
    public float xMin = -8.0f;
    public float xMax = 8.0f;

    // Vidas del jugador
    public int lives = 5;

    [Header("Referencias")]
    public Spawner spawner;

    private List<GameObject> goodThingsCollected = new List<GameObject>();

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

    // Detecta colisiones con objetos que tengan un componente FallingObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BadThings"))
        {
            lives--;
            Debug.Log("¡Has perdido una vida! Vidas restantes: " + lives);

            // Si aún no se destruye el objeto por su propio script, se puede forzar su destrucción
            // Destroy(other.gameObject);

            if (lives <= 0)
            {
                Debug.Log("¡Game Over!");
                // Aquí puedes implementar la lógica para finalizar el juego, reiniciar la escena, etc.
            }
        }
        else if (other.gameObject.CompareTag("GoodThings"))
        {
            Debug.Log("Recogiste un objeto bueno");

            //Añade a la lista
            goodThingsCollected.Add(other.gameObject);

            //Cada 4, se aumenta la dificultad
            if(goodThingsCollected.Count % 4 == 0)
            {
                //Incrementamos la velocidad de caída
                spawner.objectData.fallSpeed += 0.5f;

                //Disminuimos el intervalo de spawn (con clamp para evitar valores negativos)
                spawner.objectData.spawnInterval = Mathf.Max(spawner.objectData.spawnInterval - 0.2f, 0.2f);

                Debug.Log("¡Aumenta la dificultad! Velocidad de caída: " + spawner.objectData.fallSpeed +
                          ", Intervalo de spawn: " + spawner.objectData.spawnInterval);
            }
        }
        else if (other.gameObject.CompareTag("SpecialThings"))
        {
            Debug.Log("SPECIAL");
        }
    }
}

