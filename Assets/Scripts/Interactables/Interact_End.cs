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
        GameStateHandler.Instance.dialogueActive = true;
        DialogueVFX.SetActive(true);

        if (!isTyping)
        {
            if (dialogueIndex < dialogueArray.Length)
            {
                StartCoroutine(TypeDialogue(dialogueArray[dialogueIndex], colorHexArray[dialogueIndex]));
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueArray[dialogueIndex];
            isTyping = false;
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

    private void EndDialogue()
    {
        DialogueVFX.SetActive(false);
        properties.HasBeenInteracted = true;

        SceneManager.LoadScene(DestinationScene);

        GameStateHandler.Instance.dialogueActive = false;

    }
}
