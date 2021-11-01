using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateGameStats", order = 53)]
public class GameStats : ScriptableObject
{
    public float SpawnTime = 2;
    public float Score = 0;
    public bool PlayerIsDeath = false;
    public bool GameInPause = false;
    public bool firstBlood;

    public void Restart()
    {       
        SpawnTime = 2;
        Score = 0;
        PlayerIsDeath = false;
        GameInPause = false;
        firstBlood = false;
    }

    public void AddScore(float score)
    {
        Score += score;
    }
}
