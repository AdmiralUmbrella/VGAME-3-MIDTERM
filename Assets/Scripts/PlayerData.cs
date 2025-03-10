using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "FallingObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    // Datos de salud
    public int maxLives = 5;
    [HideInInspector]
    public int currentLives;

    // Datos del medidor especial
    public float maxSpecialMeter = 100f;
    [HideInInspector]
    public float currentSpecialMeter = 0f;

    public int goodThingsCollected = 0;
    private int nextThreshold = 25; // La primera meta es 25

    private void OnEnable()
    {
        currentLives = maxLives;
        currentSpecialMeter = 0f;
    }

    // Métodos para la salud
    public void TakeDamage(int damage = 1)
    {
        currentLives -= damage;
        if (currentLives < 0)
            currentLives = 0;
    }

    // Métodos para el medidor especial
    public void IncreaseSpecialMeter(float amount)
    {
        currentSpecialMeter += amount;
        if (currentSpecialMeter > maxSpecialMeter)
            currentSpecialMeter = maxSpecialMeter;
    }

    public bool IsSpecialMeterFull()
    {
        return currentSpecialMeter >= maxSpecialMeter;
    }

    public void ResetSpecialMeter()
    {
        currentSpecialMeter = 0f;
    }

    public bool ShouldIncreaseSpeed()
    {
        if (goodThingsCollected >= nextThreshold)
        {
            nextThreshold += 25; // Incrementamos la meta al siguiente múltiplo de 25
            return true;
        }
        return false;
    }

    public void IncreaseGoodThingsCount()
    {
        goodThingsCollected++;
    }
}
