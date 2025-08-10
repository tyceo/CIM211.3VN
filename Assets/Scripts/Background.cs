using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Animator transitionAnimator;
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

    public void FadeIn()
    {
        CycleBackground();
        transitionAnimator.Play("Fade in");
    }

    public void FadeOut()
    {
        transitionAnimator.Play("Fade out");
    }

    IEnumerator FadeToBackground()
    {
        FadeOut();
        yield return new WaitForSeconds(1f);
        FadeIn();
    }

    private void DialogueEventCalled(string input)
    {
        if (input == "change background")
        {
            CycleBackground();
            return;
        }
        if (input == "fade in")
        {
            FadeIn();
            return;
        }
        if (input == "fade out")
        {
            FadeOut();
            return;
        }
        if (input == "fade to background")
        {
            StartCoroutine(FadeToBackground());
            return;
        }
    }
}
