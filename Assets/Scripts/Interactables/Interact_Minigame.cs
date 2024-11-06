using System.Collections;
using UnityEngine;
using TMPro;

public class Interact_Minigame : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea] public string[] dialogueArray;
    public string[] colorHexArray;
    public bool YesDialogue = true;
    public float typingSpeed = 0.05f;

    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueVFX;
    [Space(16)]
    public GameObject minigame;

    private int dialogueIndex = 0;
    private bool isTyping = false;
    private bool isDialogueActive = false;

    private Interact_Prerequisites prerequisites;
    private Interact_Properties properties;

    private void Start()
    {
        prerequisites = GetComponent<Interact_Prerequisites>();
        properties = GetComponent<Interact_Properties>();
        DialogueVFX.SetActive(false);
        minigame.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!GameStateHandler.Instance.dialogueActive && !GameStateHandler.Instance.minigameActive)
        {
            if (prerequisites == null)
            {
                SystemLogicStart();
            }
            else
            {
                if (prerequisites.PrerequisitesMet == true)
                {
                    SystemLogicStart();
                }
            }
        }
    }

    private void SystemLogicStart()
    {
        // Enable minigame
        minigame.SetActive(true);
        DialogueVFX.SetActive(true);

        GameStateHandler.Instance.dialogueActive = true;


        // Show dialogue if YesDialogue is true
        if (YesDialogue)
        {
            StartDialogue();
        }
        else
        {
            // Skip dialogue and directly enable minigame
            EnableMinigameLogic();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        ShowNextDialogueLine();
    }

    private void ShowNextDialogueLine()
    {
        if (dialogueIndex < dialogueArray.Length)
        {
            StartCoroutine(TypeDialogue(dialogueArray[dialogueIndex], colorHexArray[dialogueIndex]));
        }
        else
        {
            // Dialogue finished
            EndDialogue();
        }
    }

    private IEnumerator TypeDialogue(string dialogue, string colorHex)
    {
        isTyping = true;
        dialogueText.text = "";
        dialogueText.color = HexToColor(colorHex);

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        dialogueIndex++;
    }

    private Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }

    private void Update()
    {
        Debug.LogWarning("You need to turn off minigame state in the actual minigame");

        // Allow skipping text or advancing dialogue on click
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Skip to the full line if still typing
                StopAllCoroutines();
                dialogueText.text = dialogueArray[dialogueIndex];
                isTyping = false;
            }
            else
            {
                // Show next line if not typing
                ShowNextDialogueLine();
            }
        }
    }

    private void EndDialogue()
    {
        DialogueVFX.SetActive(false);
        isDialogueActive = false;
        GameStateHandler.Instance.dialogueActive = false;
        EnableMinigameLogic();
    }

    private void EnableMinigameLogic()
    {
        GameStateHandler.Instance.minigameActive = true;
    }
}
