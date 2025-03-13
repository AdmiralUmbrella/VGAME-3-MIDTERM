using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        ActualizarTextoScore();
    }

    // Método público para aumentar el score (se incrementa de 100 en 100)
    public void AgregarScore(int cantidad)
    {
        score += cantidad;
        ActualizarTextoScore();
    }

    void ActualizarTextoScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
