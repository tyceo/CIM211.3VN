using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public SpriteRenderer background;
    public Sprite[] backgroundImages;
    private int currentBackground;

    // Start is called before the first frame update
    void Start()
    {
        DialogueDisplay.DialogueEvent += DialogueEventCalled;
        currentBackground = 0;
        background.sprite = backgroundImages[currentBackground];
    }

    public void CycleBackground()
    {
        currentBackground++;
        if (currentBackground == backgroundImages.Length)
        {
            currentBackground = 0;
        }
        background.sprite = backgroundImages[currentBackground];
    }

    private void DialogueEventCalled(string input)
    {
        if (input == "change background")
        {
            CycleBackground();
        }
    }
}
