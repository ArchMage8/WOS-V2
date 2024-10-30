using UnityEngine;

public class Interact_Prerequisites : MonoBehaviour
{
    [Header("Prerequisite Settings")]
    public bool PrerequisitesMet = false;

    [Header("Interactable GameObjects")]
    public GameObject[] prerequisiteObjects;

    private void Update()
    {
        CheckPrerequisites();
    }

    private void CheckPrerequisites()
    {
        foreach (GameObject obj in prerequisiteObjects)
        {
            Interact_Properties interactProperties = obj.GetComponent<Interact_Properties>();
            if (interactProperties == null || !interactProperties.HasBeenInteracted)
            {
                PrerequisitesMet = false;
                return; // Exit early if any prerequisite is not met
            }
        }

        // If all objects have been interacted with, set PrerequisitesMet to true
        PrerequisitesMet = true;
    }
}
