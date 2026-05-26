using UnityEngine;
using TMPro;

public class WallPuzzle : MonoBehaviour
{
    [System.Serializable]
    public class NumberSlot
    {
        public GameObject cube;
        public TextMeshPro numberText;

        [HideInInspector]
        public int currentNumber = 0;
    }

    public MemoryImage memoryImages;

    public NumberSlot[] slots;


    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip remember;

    [Header("UI")]
    public GameObject pressEText;

    public float interactDistance = 5f;

    [Header("Correct Code")]
    public int first = 5;
    public int second = 5;
    public int third = 2;

    [Header("Door")]
    public GameObject door;

    [Header("Lights")]
    public Renderer redQuad;
    public Renderer greenQuad;

    public Material redMaterial;
    public Material greenMaterial;

    public Color redOn;
    public Color redOff;

    public Color greenOn;
    public Color greenOff;

    Camera cam;

    bool opened = false;
    bool imagetriggered = false;

    void Start()
    {
        cam = Camera.main;
        pressEText.SetActive(false);
        UpdateAllTexts();
        SetWrongState();
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        bool found = false;

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (hit.collider.gameObject == slots[i].cube)
                {
                    found = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioSource.PlayOneShot(clickSound);

                        IncreaseNumber(i);
                    }

                    break;
                }
            }
        }

        if (found)
            pressEText.SetActive(true);
        else
            pressEText.SetActive(false);
    }

    void IncreaseNumber(int index)
    {
        slots[index].currentNumber++;

        if (slots[index].currentNumber > 9)
            slots[index].currentNumber = 0;

        slots[index].numberText.text =
            slots[index].currentNumber.ToString();

        CheckCode();
    }

    void CheckCode()
    {
        bool correct =
            slots[0].currentNumber == first &&
            slots[1].currentNumber == second &&
            slots[2].currentNumber == third;

        if (correct && !opened)
        {
            opened = true;

            door.SetActive(false);
            audioSource.PlayOneShot(remember);
            SetCorrectState();
            if (imagetriggered == false) { memoryImages.NextImage(); imagetriggered = true; }


        }
        else if (!correct)
        {
            opened = false;

            door.SetActive(true);

            SetWrongState();
        }
    }

    void UpdateAllTexts()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].numberText.text =
                slots[i].currentNumber.ToString();
        }
    }

    void SetCorrectState()
    {
        redMaterial.SetColor("_EmissionColor", redOff);
        greenMaterial.SetColor("_EmissionColor", greenOn);
    }

    void SetWrongState()
    {
        redMaterial.SetColor("_EmissionColor", redOn);
        greenMaterial.SetColor("_EmissionColor", greenOff);
    }
}