using UnityEngine;
using TMPro;

public class EscapeButton : MonoBehaviour
{
    public EscapeManager manager;

    public GameObject currentDoor;
    public GameObject previousDoor;

    public AudioSource alarmSound;

    public Transform player;

    public TMP_Text interactText;

    public float interactDistance = 4f;

    private bool pressed = false;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (pressed)
            return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactDistance)
        {
            interactText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ActivateButton();
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void ActivateButton()
    {
        pressed = true;

        objectRenderer.material.color = Color.green;

        currentDoor.SetActive(false);

        previousDoor.SetActive(true);

        interactText.gameObject.SetActive(false);

        alarmSound.Play();

        manager.StartEscape();

        
    }
}