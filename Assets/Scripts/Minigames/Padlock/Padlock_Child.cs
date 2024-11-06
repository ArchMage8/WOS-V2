using UnityEngine;
using TMPro;

public class Padlock_Child : MonoBehaviour
{
    [Header("Padlock Settings")]
    public int CorrectValue;
    public int CurrValue;

    [Header("UI Reference")]
    public TextMeshProUGUI valueDisplay;

    [Header("Match Status")]
    public bool KeyMatch = false;

    private void Start()
    {
        UpdateDisplay();
        CheckMatch();
    }

    public void IncrementValue()
    {
        CurrValue = (CurrValue + 1) % 10; // Wrap around to 0 if above 9
        UpdateDisplay();
        CheckMatch();
    }

    public void DecrementValue()
    {
        CurrValue = (CurrValue - 1 + 10) % 10; // Wrap around to 9 if below 0
        UpdateDisplay();
        CheckMatch();
    }

    private void UpdateDisplay()
    {
        if (valueDisplay != null)
        {
            valueDisplay.text = CurrValue.ToString();
        }
    }

    private void CheckMatch()
    {
        KeyMatch = (CurrValue == CorrectValue);
    }
}
