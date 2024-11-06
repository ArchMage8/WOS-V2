using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numpad_Check : MonoBehaviour
{
    public Numpad_Main numpad;

    private void OnMouseDown()
    {
        numpad.CheckCode();
    }
}
