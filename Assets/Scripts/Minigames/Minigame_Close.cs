using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Close : MonoBehaviour
{
    public GameObject TargetMinigame;
    private Animator TargetAnimator;
    private bool canClose = true;

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
