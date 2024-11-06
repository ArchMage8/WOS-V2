using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_DialogueSkip : MonoBehaviour
{
    public GameObject ObjectWithDialogue;

    private Interact_Dialogue normaldialogue;
    private Interact_Minigame minigameDialogue;

    private void Start()
    {
        normaldialogue = ObjectWithDialogue.GetComponent<Interact_Dialogue>();
        minigameDialogue = ObjectWithDialogue.GetComponent<Interact_Minigame>();
    }

    private void OnMouseDown()
    {
        if (normaldialogue != null && minigameDialogue == null)
        {
            normaldialogue.EndDialogue();
        }

        else if (minigameDialogue != null && normaldialogue == null)
        {
            minigameDialogue.EndDialogue();
        }
    }
}
