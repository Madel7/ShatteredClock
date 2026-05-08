using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    public Image[] clockParts;

    int currentMemory = 0;

    public AudioSource audioSource;

    public void UnlockMemory()
    {
        if (currentMemory < clockParts.Length)
        {
            Color c = clockParts[currentMemory].color;
            c.a = 1f; 
            clockParts[currentMemory].color = c;

            currentMemory++;
            audioSource.Play();

            Debug.Log("Memory Restored: " + currentMemory);
        }
    }
}