using UnityEngine;
using TMPro;
using System.Collections;

public class FinalChoiceButton : MonoBehaviour
{
    public GameObject[] buttons;
    public int correctButtonIndex;

    public Transform player;
    public float interactDistance = 3f;

    public TMP_Text interactText;

    public Transform finalDoor;
    public float openHeight = 6f;
    public float openSpeed = 2f;

    public AudioSource audioSource;

    public AudioClip explosionClip;
    public AudioClip clownClip;
    public AudioClip playerVoiceClip;

    public Transform clownTrigger;
    public float clownDistance = 5f;

    public EscapeManager manager;

    private bool pressed;
    private bool clownSequencePlayed;

    private Vector3 closedPos;
    private Vector3 openPos;

    void Start()
    {
        interactText.gameObject.SetActive(false);

        closedPos = finalDoor.position;
        openPos = closedPos + Vector3.up * openHeight;
    }

    void Update()
    {
        CheckClownEvent();

        if (pressed)
            return;

        Ray ray = Camera.main.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );

        bool found = false;

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (hit.collider.gameObject == buttons[i])
                {
                    found = true;

                    interactText.gameObject.SetActive(true);
                    interactText.text = "Press E";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PressButton(i);
                    }

                    break;
                }
            }
        }

        if (!found)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void CheckClownEvent()
    {
        if (clownSequencePlayed)
            return;

        float distance = Vector3.Distance(
            player.position,
            clownTrigger.position
        );

        if (distance <= clownDistance)
        {
            clownSequencePlayed = true;

            StartCoroutine(PlayClownSequence());
        }
    }

    IEnumerator PlayClownSequence()
    {
        audioSource.PlayOneShot(clownClip);

        yield return new WaitForSeconds(clownClip.length);

        audioSource.PlayOneShot(playerVoiceClip);
    }

    void PressButton(int index)
    {
        pressed = true;

        interactText.gameObject.SetActive(false);

        if (index == correctButtonIndex)
        {
            StartCoroutine(OpenDoor());
        }
        else
        {
            audioSource.PlayOneShot(explosionClip);

            manager.Die();
        }
    }

    IEnumerator OpenDoor()
    {
        while (Vector3.Distance(finalDoor.position, openPos) > 0.01f)
        {
            finalDoor.position = Vector3.Lerp(
                finalDoor.position,
                openPos,
                Time.deltaTime * openSpeed
            );

            yield return null;
        }

        finalDoor.position = openPos;
    }
}