using UnityEngine;
using TMPro;

public class Numpad_Main : MonoBehaviour
{
    [Header("Numpad Settings")]
    public string correctCode = "1234";
    private string currentCode = "";

    [Header("UI Reference")]
    public TextMeshProUGUI displayText;

    [Space(16)]
    public Interact_Properties interact_Properties;
    

    private void Start()
    {
        UpdateDisplay();
    }

    public void AddDigit(string digit)
    {
        if (currentCode.Length < 4)
        {
            currentCode += digit;
            UpdateDisplay();
        }
    }

    public void CheckCode()
    {
        if (currentCode == correctCode)
        {
            CorrectCodeEntered();
        }
        else
        {
            IncorrectCodeEntered();
        }
    }

    private void UpdateDisplay()
    {
        displayText.text = currentCode;
    }

    private void IncorrectCodeEntered()
    {
        currentCode = "";
        UpdateDisplay();
    }

    private void CorrectCodeEntered()
    {

        interact_Properties.HasBeenInteracted = true;
            
        // Logic for correct code entry (keep empty for now)
    }
}
