using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Velocidad de Caída (se asigna al instanciar desde el Spawner)")]
    public float fallSpeed = 2.0f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DestroyBarrier"))
        {
            Destroy(gameObject);
        }
    }
}
