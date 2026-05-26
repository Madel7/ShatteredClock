using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleButton button1;
    public PuzzleButton button2;
    public PuzzleButton button3;

    public GameObject door;

    public int correct1 = 3;
    public int correct2 = 7;
    public int correct3 = 1;

    bool opened = false;

    public void CheckCode()
    {
        if (opened) return;

        if (button1.currentNumber == correct1 &&
            button2.currentNumber == correct2 &&
            button3.currentNumber == correct3)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        opened = true;

       
        door.SetActive(false);


        Debug.Log("Door Opened!");
    }
}