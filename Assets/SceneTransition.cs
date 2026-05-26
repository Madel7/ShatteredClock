using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject pressEText;
    public Image fadeImage;

    public string sceneToLoad;

    bool playerInside = false;

    void Update()
    {
        if (GameManager.instance.memoryUnlocked&&playerInside && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;

            Color c = fadeImage.color;
            c.a = time;
            fadeImage.color = c;

            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.memoryUnlocked)
        {
            playerInside = true;

            pressEText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            pressEText.SetActive(false);
        }
    }
}