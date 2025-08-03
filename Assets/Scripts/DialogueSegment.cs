using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
    // The name of the person currently speaking to display (this can be blank if needed)
    public string speakerName;
    // The contents of this text segment
    public string text;
    // The amount of time between this segment starting and the text being displayed (use this for transitions)
    public float waitTime;
    // Call an event with this text as the input when this segment starts. If this is empty, no event is called.
    public string callEvent;
}
