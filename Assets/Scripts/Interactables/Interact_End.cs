using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Interact_End : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea] public string[] dialogueArray;
    public string[] colorHexArray;

    [Header("UI/UX Reference")]
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueVFX;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    public int DestinationScene;

    private int dialogueIndex = 0;
    private bool isTyping = false;
    private bool lineFullyDisplayed = false;
    private bool dialogueInitialized = false;

    private Interact_Prerequisites prerequisites;
    private Interact_Properties properties;

    public Animator FadeAnimator;

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
            if (prerequisites == null || prerequisites.PrerequisitesMet)
            {
                
                SystemLogicStart();
            }
        }
    }

    private void Update()
    {
        if (dialogueInitialized && Input.GetMouseButtonDown(0))
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
        GameStateHandler.Instance.dialogueActive = true;
        DialogueVFX.SetActive(true);

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

    private void EndDialogue()
    {
        DialogueVFX.SetActive(false);
        properties.HasBeenInteracted = true;
        GameStateHandler.Instance.dialogueActive = false;

        StartCoroutine(EndSceneLogic());
    }

    private IEnumerator EndSceneLogic()
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(DestinationScene);
    }
}
