using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreEntry
{
    public float Score;
    public float Time;
    public string Name;
    public bool newEntry;

    public HighscoreEntry(float score, float time, string name,bool newEntry)
    {
        Score = score;
        Time = time;
        Name = name;
        this.newEntry = newEntry;
    }
}
