using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Padlock_Main : MonoBehaviour
{
    
    public Padlock_Child[] padlockChildren;

    
    [HideInInspector]public bool CanContinue = false;
    public Interact_Properties interactProperties;

    private void Update()
    {
        CheckCombination();
    }

    private void CheckCombination()
    {
        CanContinue = true;

        foreach (Padlock_Child child in padlockChildren)
        {
            if (!child.KeyMatch)
            {
                CanContinue = false;
                break;
            }
        }
    }

    public void OpenPadlock()
    {
        if (CanContinue)
        {
            interactProperties.HasBeenInteracted = true;
            
            //Enter the event whats supposed to happen here:
            // Play Sound
            // Animate Down
        }
    }
}
