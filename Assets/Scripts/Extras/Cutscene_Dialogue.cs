using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cutscene_Dialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea] public string[] dialogueArray;
    public string[] colorHexArray;
    public int TargetScene;

    [Header("UI/UX Reference")]
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueVFX;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    private int dialogueIndex = 0;
    private bool isTyping = false;

    private void Awake()
    {
        DialogueVFX.SetActive(false);
        StartCutsceneDialogue();
    }

    private void StartCutsceneDialogue()
    {
      
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

        // Start next line if there are more lines to show
        if (dialogueIndex < dialogueArray.Length)
        {
            StartCoroutine(TypeDialogue(dialogueArray[dialogueIndex], colorHexArray[dialogueIndex]));
        }
        else
        {
            EndDialogue();
        }
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
        StartCoroutine(EndSceneLogic());
        dialogueIndex = 0;
    }

    public Animator FadeAnimator;

    private IEnumerator EndSceneLogic()
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(TargetScene);
    }
}
