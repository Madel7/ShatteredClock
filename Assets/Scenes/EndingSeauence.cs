using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingSeauence : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("End Trigger")]
    public Transform endPoint;
    public float triggerDistance = 3f;

    [Header("UI")]
    public Image fadeImage;
    public GameObject logo;

    [Header("Settings")]
    public float fadeSpeed = 2f;
    public float logoTime = 3f;

    private bool started;

    void Update()
    {
        if (started)
            return;

        float distance = Vector3.Distance(
            player.position,
            endPoint.position
        );

        if (distance <= triggerDistance)
        {
            started = true;
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        Color color = fadeImage.color;

        while (color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = color;

            yield return null;
        }

        logo.SetActive(true);

        yield return new WaitForSeconds(logoTime);

        SceneManager.LoadScene("MainMenu");
    }
}