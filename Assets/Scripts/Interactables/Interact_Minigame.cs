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
    private bool lineFullyDisplayed = false;
    private bool dialogueInitialized = false;
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
        if (!GameStateHandler.Instance.dialogueActive && !GameStateHandler.Instance.minigameActive && !dialogueInitialized)
        {
            if (prerequisites == null || prerequisites.PrerequisitesMet)
            {
                SystemLogicStart();
            }
        }
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            if (lineFullyDisplayed)
            {
                ShowNextDialogueLine();
            }
            else if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueArray[dialogueIndex];
                lineFullyDisplayed = true;
                isTyping = false;
            }
        }
    }

    private void SystemLogicStart()
    {
        minigame.SetActive(true);
        DialogueVFX.SetActive(true);
        GameStateHandler.Instance.dialogueActive = true;

        if (YesDialogue)
        {
            StartDialogue();
        }
        else
        {
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
        isDialogueActive = false;
        GameStateHandler.Instance.dialogueActive = false;
        EnableMinigameLogic();
    }

    private void EnableMinigameLogic()
    {
        GameStateHandler.Instance.minigameActive = true;
    }
}
