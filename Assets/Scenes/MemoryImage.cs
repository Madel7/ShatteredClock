using UnityEngine;
using UnityEngine.UI;

public class MemoryImage : MonoBehaviour
{
    public Image[] images;

    int currentIndex = 0;

    void Start()
    {
        ShowOnlyCurrent();
    }

    public void NextImage()
    {
        Debug.Log("NextImage Called");
        if (currentIndex < images.Length - 1)
        {
            currentIndex++;
            ShowOnlyCurrent();
        }
    }

    void ShowOnlyCurrent()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Color c = images[i].color;

            if (i == currentIndex)
                c.a = 1f;  
            else
                c.a = 0f;  

            images[i].color = c;
        }
    }
}