using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Padlock_Main : MonoBehaviour
{
    
    public Padlock_Child[] padlockChildren;

    
    [HideInInspector]public bool CanContinue = false;
    public Interact_Properties interactProperties;
    private bool completed = false;

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
        if (CanContinue && !completed)
        {
            GameStateHandler.Instance.minigameActive = false;
            interactProperties.HasBeenInteracted = true;
            completed = true;
            
            //Enter the event whats supposed to happen here:
            // Play Sound
            
        }
    }
}
