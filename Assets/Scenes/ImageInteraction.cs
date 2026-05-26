using UnityEngine;
using TMPro;

public class ImageRaycastInteraction : MonoBehaviour
{
    public Camera playerCamera;

    public float interactDistance = 3f;

    public TMP_Text interactText;

    public AudioSource audioSource;
    public AudioClip soundEffect;

    void Update()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                interactText.gameObject.SetActive(true);
                

                if (Input.GetKeyDown(KeyCode.E))
                {
                    audioSource.PlayOneShot(soundEffect);
                }

                return;
            }
        }

        interactText.gameObject.SetActive(false);
    }
}