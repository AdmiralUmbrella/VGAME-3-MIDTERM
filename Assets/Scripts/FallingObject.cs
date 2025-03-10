using UnityEngine;

public class FallingObject : MonoBehaviour
{
    // Velocidad de caída del objeto (puedes modificarla desde el Inspector)
    public float fallSpeed = 2.0f;

    void Update()
    {
        // Mueve el objeto hacia abajo de forma constante
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    // Se ejecuta al entrar en colisión con otro collider (asegúrate de que los colliders estén configurados como Trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DestroyBarrier"))
        {
            // Destruye este objeto
            Destroy(gameObject);
        }
    }
}
