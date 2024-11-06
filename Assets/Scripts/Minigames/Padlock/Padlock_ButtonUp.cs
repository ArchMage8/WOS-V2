using UnityEngine;

public class Padlock_ButtonUp : MonoBehaviour
{
    [Header("Target Padlock Child")]
    public Padlock_Child targetChild;

    private void OnMouseDown()
    {
        if (targetChild != null)
        {
            targetChild.IncrementValue();
        }
    }
}
