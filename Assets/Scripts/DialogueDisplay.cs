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

    public GameObject Leftportrate;
    public GameObject Rightportrate;

    public Sprite Paul;
    public Sprite Bob;
    public Sprite Dan;

    public TMP_Text Leftname;    //portraite names
    public TMP_Text Rightname;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintText(segments[0]));
        Leftname.text = "Main Character"; // Set the initial speaker name
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
        foreach (string callEvent in segment.callEvents)
        {
            DialogueEvent?.Invoke(callEvent);
        }
        // Clear any previous text and wait if needed
        textDisplay.text = string.Empty;
        speakerText.text = string.Empty;
        yield return new WaitForSeconds(segment.waitTime);
        isPrinting = true;
        speakerText.text = segment.speakerName;

        SpriteRenderer sr = Rightportrate.GetComponent<SpriteRenderer>();
        sr.sprite = Paul; 
        if (currentSegment == 0)
        {
            Leftportrate.SetActive(false);
            Rightportrate.SetActive(true);
            Rightname.text = "2nd person"; 
        }
        else if (currentSegment == 1)
        {
            Leftportrate.SetActive(true);
            Rightportrate.SetActive(false);
            sr.sprite = Bob;
        }
        else if (currentSegment == 2)
        {
            Leftportrate.SetActive(true);
            Rightportrate.SetActive(false);
            sr.sprite = Dan;
        }
        else if (currentSegment == 3)
        {
            Leftportrate.SetActive(false);
            Rightportrate.SetActive(true);
            sr.sprite = Paul;
        }
        else if (currentSegment == 4)
        {
            Leftportrate.SetActive(true);
            Rightportrate.SetActive(false);
            sr.sprite = Bob;
        }
        else if (currentSegment == 5)
        {
            Leftportrate.SetActive(true);
            Rightportrate.SetActive(false);
            sr.sprite = Dan;
        }

        // Display the text one character at a time
        foreach (char c in segment.text.ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(1 / displaySpeed);
        }
        Debug.Log("Finished printing text to display");
        isPrinting = false;
        isAwaitingInput = true;
    }
}
