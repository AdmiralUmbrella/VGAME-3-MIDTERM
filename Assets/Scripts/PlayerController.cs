using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("UI")]
    public ScoreUI scoreUI;
    public LifeUI lifeUI;
    public SpecialEffectUI specialEffectUI;
    public GameObject gameOverScreen;

    private List<GameObject> goodThingsCollected = new List<GameObject>();

    public float slowMotionDuration = 3f;

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

            if (lifeUI != null)
                lifeUI.ActualizarVidas(lives);

            if (lives <= 0)
            {
                Debug.Log("¡Game Over!");
                Destroy(gameObject);
                gameOverScreen.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("GoodThings"))
        {
            Debug.Log("Recogiste un objeto bueno");

            // Aumenta el score en 100
            if (scoreUI != null)
                scoreUI.AgregarScore(100);

            // Agrega a la lista de GoodThings recogidos
            goodThingsCollected.Add(other.gameObject);

            // Cada 4 objetos, aumenta la dificultad
            if (goodThingsCollected.Count % 4 == 0)
            {
                spawner.objectData.fallSpeed += 0.5f;
                spawner.objectData.spawnInterval = Mathf.Max(spawner.objectData.spawnInterval - 0.2f, 0.2f);
                Debug.Log("¡Aumenta la dificultad! Velocidad de caída: " + spawner.objectData.fallSpeed +
                          ", Intervalo de spawn: " + spawner.objectData.spawnInterval);
            }
        }
        else if (other.gameObject.CompareTag("SpecialThings"))
        {
            Debug.Log("Recogiste un objeto ESPECIAL");

            ActivateRandomSpecialEffect();
        }


    }

    void ActivateRandomSpecialEffect()
    {
        int randomEffect = Random.Range(0, 3);
        switch (randomEffect)
        {
            case 0:
                DestroyAllBadThings();
                if (specialEffectUI != null)
                    specialEffectUI.MostrarMensajeEfecto("Purificacion", 2f);
                break;

            case 1:
                if (specialEffectUI != null)
                    specialEffectUI.MostrarMensajeEfecto("Slow Motion", slowMotionDuration);
                StartCoroutine(SlowMoCoroutine());
                break;

            case 2:
                GainExtraLife();
                if (specialEffectUI != null)
                    specialEffectUI.MostrarMensajeEfecto("Vida Extra", 2f);
                break;
        }
    }

    void DestroyAllBadThings()
    {
        GameObject[] badThings = GameObject.FindGameObjectsWithTag("BadThings");

        foreach (var bad in badThings)
        {
            Destroy(bad);
        }
        Debug.Log("Se han destruido todos los BadThings en la escena.");
    }

    IEnumerator SlowMoCoroutine()
    {
        Debug.Log("Slow Motion activado");

        float originalSpeed = spawner.objectData.fallSpeed;
        spawner.objectData.fallSpeed = originalSpeed / 2.0f;

        yield return new WaitForSeconds(slowMotionDuration);

        spawner.objectData.fallSpeed = originalSpeed;
        Debug.Log("Fin del slow motion");
    }

    void GainExtraLife()
    {
        lives++;
        Debug.Log("¡Has obtenido una vida extra! Vidas totales: " + lives);

        if (lifeUI != null)
            lifeUI.ActualizarVidas(lives);
    }
}

