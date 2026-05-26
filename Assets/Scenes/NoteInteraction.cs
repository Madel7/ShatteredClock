using UnityEngine;
using TMPro;

public class NoteInteraction : MonoBehaviour
{
    public GameObject noteUI;
    public GameObject helperUI;
    public AudioSource audioSource;
    public AudioClip noteSound;
    public float interactDistance = 4f;

    Camera cam;

    bool reading = false;

    void Start()
    {
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        helperUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        bool isLooking = Physics.Raycast(ray, out RaycastHit hit, interactDistance)
                         && hit.collider.gameObject == gameObject;

        // show prompt
        if (!reading)
        {
            helperUI.SetActive(isLooking);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (reading)
            {
                CloseNote();
                return;
            }

            if (isLooking)
            {
                OpenNote();
            }
        }
    }

    void OpenNote()
    {
        reading = true;
        audioSource.PlayOneShot(noteSound);
        noteUI.SetActive(true);
        helperUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void CloseNote()
    {
        reading = false;
        audioSource.PlayOneShot(noteSound);
        noteUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }
}