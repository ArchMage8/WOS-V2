using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Close : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite defaultSprite;
    public Sprite hoverSprite;

    public GameObject TargetMinigame;
    private Animator TargetAnimator;
    private bool canClose = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set to default sprite initially
        if (defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    private void OnMouseEnter()
    {
        if (hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }

    private void OnMouseExit()
    {
        if (defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    private void OnMouseDown()
    {
        if (canClose)
        {
            StartCoroutine(CloseMiniGame());
        }
    }

    private IEnumerator CloseMiniGame()
    {
        canClose = false;
        TargetAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(1);

        canClose = true;
        TargetMinigame.SetActive(false);

        GameStateHandler.Instance.minigameActive = false;

    }
}
