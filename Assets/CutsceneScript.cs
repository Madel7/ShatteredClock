using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject panelDefault;
    public GameObject panelBlack;
    private CanvasGroup panelBlackGroup;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public int currentPanelIndex;
    public int[] audioStartIndex = { 0, 3, 6, 7, 8, 9 };

    public bool canSwitch = true;
    public bool canFadeOut = true;
    public bool canPlay = true;

    void Start()
    {
        panelBlackGroup = panelBlack.GetComponent<CanvasGroup>();
        currentPanelIndex = 0;
        canPlay = false;
        StartCoroutine(FadeBlack(false));
        StartCoroutine(PlayAudioSequentially());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (panelBlackGroup.alpha < 0.9)
            {
                canSwitch = false;
                canFadeOut = false;
                canPlay = false;
                StartCoroutine(FadeBlack(true));
            }

            currentPanelIndex++;
            if (currentPanelIndex >= panels.Length)
                StartCoroutine(StartGame());
            StartCoroutine(ShowPanel(currentPanelIndex));

            StartCoroutine(FadeBlack(false));
        }
    }

    IEnumerator StartGame()
    {
        while (!canSwitch)
            yield return null;
        
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator ShowPanel(int panelIndex)
    {
        while (!canSwitch)
            yield return null;

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == panelIndex);
        }

        canFadeOut = true;
    }

    IEnumerator PlayAudioSequentially()
    {
        int previousPanelIndex = 0;

        for (int i = 0; i < audioClips.Length; i++)
        {
            while (!canPlay || i == audioStartIndex[currentPanelIndex + 1])
                yield return null;

            audioSource.clip = audioClips[i];
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                if (previousPanelIndex != currentPanelIndex)
                {
                    audioSource.Stop();
                    i = audioStartIndex[currentPanelIndex] - 1;
                    previousPanelIndex = currentPanelIndex;
                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeBlack(bool fadeIn)
    {
        float start = fadeIn ? 0 : 1;
        float end = fadeIn ? 1 : 0;
        float duration = 0.5f;

        if (!fadeIn)
        {
            while (!canFadeOut)
                yield return null;
        }

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(start, end, t / duration);
            panelBlackGroup.alpha = alpha;
            yield return null;
        }

        panelBlackGroup.alpha = end;
        if (fadeIn)
            canSwitch = true; 
        else
            canPlay = true;
    }
}
