using UnityEngine;
using TMPro;
using System.Collections;

public class SpecialEffectUI : MonoBehaviour
{
    public TextMeshProUGUI specialEffectText;

    void Start()
    {
        // Oculta el mensaje al iniciar
        specialEffectText.gameObject.SetActive(false);
    }

    // Método público para mostrar el mensaje durante 'duracion' segundos
    public void MostrarMensajeEfecto(string mensaje, float duracion)
    {
        specialEffectText.text = mensaje;
        specialEffectText.gameObject.SetActive(true);
        StartCoroutine(OcultarMensajeDespues(duracion));
    }

    IEnumerator OcultarMensajeDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        specialEffectText.gameObject.SetActive(false);
    }
}