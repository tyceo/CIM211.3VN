using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicTracks;
    private int trackIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        if (musicTracks.Length == 0 || musicTracks[0] == null)
        {
            Debug.Log("No music tracks present. Music player will be disabled.");
            this.enabled = false;
        }
        else
        {
            PlayTrack(0);
            DialogueDisplay.DialogueEvent += DialogueEventCalled;
        }
    }

    public void PlayTrack(int track)
    {
        if (track < musicTracks.Length && musicTracks[track] != null)
        {
            trackIndex = track;
            musicSource.clip = musicTracks[track];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music track " + track + " is empty or invalid.");
        }
    }

    public void CycleTracks()
    {
        if (trackIndex++ == musicTracks.Length)
        {
            PlayTrack(0);
        }
        else
        {
            PlayTrack(trackIndex++);
        }
    }

    private void DialogueEventCalled(string input)
    {
        if (input == "change music")
        {
            CycleTracks();
            return;
        }
        if (input == "stop music")
        {
            musicSource.Stop();
            return;
        }
    }
}
