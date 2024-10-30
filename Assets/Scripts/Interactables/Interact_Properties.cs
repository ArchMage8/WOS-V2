using UnityEngine;

public class Interact_Properties : MonoBehaviour
{
    public bool HasBeenInteracted;
    [Space(20)]

    [Header("Interaction Sprites")]
    public GameObject NotInteractedSprite;
    public GameObject InteractedSprite;
    public GameObject Default;

    private Interact_Prerequisites prerequisiteScript;
    private Collider2D InteractCollider;

    private void Start()
    {
        InteractCollider = GetComponent<Collider2D>();
        InitializeSprites();
    }

    private void InitializeSprites()
    {
        // Check for Interact_Prerequisite script on the GameObject
        prerequisiteScript = GetComponent<Interact_Prerequisites>();

        if (prerequisiteScript == null)
        {
            // If no prerequisite, show NotInteractedSprite
            EnableSprite(NotInteractedSprite);
        }
        else
        {
            // If prerequisite exists, show Default sprite initially
            EnableSprite(Default);
        }
    }

    private void Update()
    {
        // Check if prerequisites are met and update sprite
        if (prerequisiteScript != null && prerequisiteScript.PrerequisitesMet)
        {
            EnableSprite(NotInteractedSprite);
        }

        if (HasBeenInteracted == true)
        {
            EnableSprite(InteractedSprite);
        }

        if(Default.activeSelf == true)
        {
            InteractCollider.enabled = false;
        }
        else if(NotInteractedSprite.activeSelf == true || InteractedSprite.activeSelf == true)
        {
            InteractCollider.enabled = true;
        }
    }

    private void EnableSprite(GameObject spriteToEnable)
    {
        // Disable all sprites
        NotInteractedSprite.SetActive(false);
        InteractedSprite.SetActive(false);
        Default.SetActive(false);

        // Enable the specified sprite
        if (spriteToEnable != null)
        {
            spriteToEnable.SetActive(true);
        }
    }
}
