using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
    /// <summary>
    /// The name of the person currently speaking to display (this can be blank if needed)
    /// </summary>
    public string speakerName;
    /// <summary>
    /// The contents of this text segment
    /// </summary>
    public string text;
    /// <summary>
    /// The amount of time between this segment starting and the text being displayed (use this for transitions)
    /// </summary>
    public float waitTime;
    /// <summary>
    /// Call an event with this text as the input when this segment starts. If this is empty, no event is called.
    /// </summary>
    public string callEvent;
}
