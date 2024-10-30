using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
   
    public static GameStateHandler Instance { get; private set; }

    public bool dialogueActive = false;
    public bool minigameActive = false;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }
}
