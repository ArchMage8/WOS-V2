using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock_Complete : MonoBehaviour
{
    [Header("Padlock Main Reference")]
    public Padlock_Main padlock_Main;

    private void OnMouseDown()
    {
        padlock_Main.OpenPadlock();
    }
}
