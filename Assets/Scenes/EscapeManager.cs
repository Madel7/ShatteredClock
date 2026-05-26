using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class EscapeManager : MonoBehaviour
{
    [Header("Timer")]
    public float escapeTime = 90f;

    private float currentTime;

    private bool timerRunning = false;

    [Header("UI")]
    public TMP_Text timerText;

    public Slider healthBar;

    public GameObject deathScreen;


    [Header("Player")]
    public Transform player;


    void Start()
    {
        currentTime = escapeTime;

        UpdateTimerUI();

        deathScreen.SetActive(false);

        timerText.gameObject.SetActive(false);

        healthBar.maxValue = 100;
        healthBar.value = 100;
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            Die();
        }

        UpdateTimerUI();
        SyncHealthWithTimer();
    }

    

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SyncHealthWithTimer()
    {
        float percent = currentTime / escapeTime;

        float health = percent * 100f;

        healthBar.value = health;
    }

    public void StartEscape()
    {
        timerRunning = true;
        timerText.gameObject.SetActive(true);
        Debug.Log("Escape Started");
    }

    public void Die()
    {
        timerRunning = false;

        deathScreen.SetActive(true);

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = false;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}