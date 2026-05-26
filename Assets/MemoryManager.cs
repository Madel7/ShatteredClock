using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    public Image[] clockParts;

    int currentMemory = 0;

    public AudioSource audioSource;
    public AudioClip fiveKillsSound;
    public void UnlockMemory()
    {
        if (currentMemory < clockParts.Length)
        {
            Color c = clockParts[currentMemory].color;
            c.a = 1f; 
            clockParts[currentMemory].color = c;

            currentMemory++;

            audioSource.Play();
            audioSource.PlayOneShot(fiveKillsSound);

            Debug.Log("Memory Restored: " + currentMemory);
        }
    }
}