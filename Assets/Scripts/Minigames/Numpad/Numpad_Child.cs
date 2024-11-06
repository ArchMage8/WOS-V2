using UnityEngine;

public class Numpad_Child : MonoBehaviour
{
    [Header("Numpad Reference")]
    public Numpad_Main numpadMain;

    [Header("Digit Value")]
    public string digitValue;

    private void OnMouseDown()
    {
        if (numpadMain != null)
        {
            numpadMain.AddDigit(digitValue);
        }
    }
}
