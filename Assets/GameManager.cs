using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int zombieKills = 0;

    public TextMeshProUGUI killText;

    public MemoryManager memoryManager;
    void Awake()
    {
        instance = this;
    }

    public void AddKill()
    {
        zombieKills++;
        Debug.Log("Kills: " + zombieKills);

        killText.text = "Kill: " + zombieKills;

        if (zombieKills == 10)
        {
            memoryManager.UnlockMemory();
        }
    }

    void TriggerMemory()
    {
        Debug.Log("Memory unlocked");
    }
}