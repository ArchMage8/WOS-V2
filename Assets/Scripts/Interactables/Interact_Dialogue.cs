using System.Collections;
using UnityEngine;
using TMPro;

public class Interact_Dialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea] public string[] dialogueArray;
    public string[] colorHexArray;

    [Header("UI/UX Reference")]
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueVFX;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    private int dialogueIndex = 0;
    private bool isTyping = false;
    private bool lineFullyDisplayed = false;
    private bool dialogueInitialized = false;

    private Interact_Prerequisites prerequisites;
    private Interact_Properties properties;

    private void Start()
    {
        prerequisites = GetComponent<Interact_Prerequisites>();
        properties = GetComponent<Interact_Properties>();
        DialogueVFX.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!GameStateHandler.Instance.dialogueActive && !GameStateHandler.Instance.minigameActive && !dialogueInitialized)
        {
            // Initialize dialogue on the first click of the trigger object
            if (prerequisites == null || prerequisites.PrerequisitesMet)
            {
                
                SystemLogicStart();
            }
        }
    }

    private void Update()
    {
        // Listen for any click on the screen once the dialogue is active
        if (dialogueInitialized && Input.GetMouseButtonDown(0))
        {
            if (lineFullyDisplayed)
            {
                DisplayNextDialogueLine();
            }
            else if (isTyping)
            {
                // Skip typing effect and display full line immediately
                StopAllCoroutines();
                dialogueText.text = dialogueArray[dialogueIndex];
                lineFullyDisplayed = true;
                isTyping = false;
            }
        }
    }

    private void SystemLogicStart()
    {
        GameStateHandler.Instance.dialogueActive = true;
        DialogueVFX.SetActive(true);

        if (!isTyping)
        {
            if (dialogueIndex < dialogueArray.Length)
            {
                DisplayNextDialogueLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void DisplayNextDialogueLine()
    {
        if (dialogueIndex < dialogueArray.Length)
        {
            StartCoroutine(TypeDialogue(dialogueArray[dialogueIndex], colorHexArray[dialogueIndex]));
            dialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeDialogue(string dialogue, string colorHex)
    {
        isTyping = true;
        lineFullyDisplayed = false;
        dialogueText.text = "";
        dialogueText.color = HexToColor(colorHex);

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogueInitialized = true;
        isTyping = false;
        lineFullyDisplayed = true;
    }

    private Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }

    public void EndDialogue()
    {
        DialogueVFX.SetActive(false);
        properties.HasBeenInteracted = true;
        dialogueIndex = 0;
        GameStateHandler.Instance.dialogueActive = false;
        dialogueInitialized = false;
    }
}
