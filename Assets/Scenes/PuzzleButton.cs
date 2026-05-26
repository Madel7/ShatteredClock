using UnityEngine;
using TMPro;

public class PuzzleButton : MonoBehaviour
{
    public int currentNumber = 0;

    public TextMeshProUGUI numberText;

    public PuzzleManager puzzleManager;

    void Start()
    {
        UpdateText();
    }

    public void PressButton()
    {
        currentNumber++;

        if (currentNumber > 9)
            currentNumber = 0;

        UpdateText();

        puzzleManager.CheckCode();
    }

    void UpdateText()
    {
        numberText.text = currentNumber.ToString();
    }
}