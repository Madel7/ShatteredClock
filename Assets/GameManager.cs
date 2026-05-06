using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int zombieKills = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddKill()
    {
        zombieKills++;
        Debug.Log("Kills: " + zombieKills);

        if (zombieKills == 50)
        {
            TriggerMemory();
        }
    }

    void TriggerMemory()
    {
        Debug.Log("Memory Fragment Unlocked!");

        // ??? ????? ????:
        // UI + ??? + ??????
    }
}