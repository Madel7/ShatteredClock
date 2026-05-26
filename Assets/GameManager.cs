using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Zombie System")]
    public int zombieKills = 0;
    public TextMeshProUGUI killText;

    [Header("Memory")]
    public MemoryManager memoryManager;
    public bool memoryUnlocked = false;

    [Header("Puzzle Progress")]
    public bool puzzleSolved = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip puzzleSolvedSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKill()
    {
        zombieKills++;

        Debug.Log("Kills: " + zombieKills);

        if (killText != null)
            killText.text = "Kill: " + zombieKills;

        if (zombieKills >= 20 && !memoryUnlocked)
        {
            memoryUnlocked = true;

            if (memoryManager != null)
                memoryManager.UnlockMemory();
        }
    }

    public void PuzzleSolved()
    {
        if (puzzleSolved) return;

        puzzleSolved = true;

        Debug.Log("Puzzle Solved!");

        if (audioSource != null && puzzleSolvedSound != null)
        {
            audioSource.PlayOneShot(puzzleSolvedSound);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        killText = GameObject.Find("KillText")
            ?.GetComponent<TextMeshProUGUI>();

        if (killText != null)
            killText.text = "Kill: " + zombieKills;
    }
}