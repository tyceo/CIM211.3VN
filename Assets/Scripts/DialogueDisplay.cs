using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI speakerText;
    public float displaySpeed = 30f; // The speed at which the text will appear, measured in characters per second
    public DialogueSegment[] segments;
    public static event Action<string> DialogueEvent; // An event that can allow dialogue to trigger things
    public bool isPrinting = false; // If text is being printed to the text display right now

    private bool isAwaitingInput = false; // If this script is currently waiting for an input
    private int currentSegment = 0; // Index for the segment currently being displayed

    //portraits change with text
    public GameObject LeftCharacter;
    public GameObject RightCharacter;

    public Sprite Paul;
    public Sprite Bob;
    public Sprite Dan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintText(segments[0]));
    }

    // Update is called once per frame
    void Update()
    {
        if (isAwaitingInput && Input.GetMouseButtonDown(0))
        {
            isAwaitingInput = false;
            if (currentSegment < segments.Length - 1)
            {
                // Start displaying next segment
                currentSegment++;
                StartCoroutine(PrintText(segments[currentSegment]));
            }
            else
            {
                // There are no more segments to display
                Debug.Log("DialogueDisplay has reached the end of dialogue segments");
            }
        }
    }

    IEnumerator PrintText(DialogueSegment segment)
    {
        // Call a dialogue event if needed
        if (segment.callEvent != string.Empty)
        {
            DialogueEvent?.Invoke(segment.callEvent);
        }
        // Clear any previous text and wait if needed
        textDisplay.text = string.Empty;
        speakerText.text = string.Empty;
        yield return new WaitForSeconds(segment.waitTime);
        isPrinting = true;
        speakerText.text = segment.speakerName;
        // Display the text one character at a time
        foreach (char c in segment.text.ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(1 / displaySpeed);
        }
        Debug.Log("Finished printing text to display");

        SpriteRenderer targetRenderer = LeftCharacter.GetComponent<SpriteRenderer>();
        
        targetRenderer.sprite = Paul;

        isPrinting = false;
        isAwaitingInput = true;
    }
}
