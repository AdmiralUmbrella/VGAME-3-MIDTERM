using UnityEngine;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public TextMeshProUGUI lifeText;

    // M�todo p�blico para actualizar la vida mostrada
    public void ActualizarVidas(int vidas)
    {
        lifeText.text = "Vidas: " + vidas.ToString();
    }

    void Start()
    {
        // Opcional: inicializar con un valor por defecto (por ejemplo, 5 vidas)
        ActualizarVidas(5);
    }
}
