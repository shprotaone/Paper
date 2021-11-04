using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateGameStats", order = 53)]
public class GameStats : ScriptableObject
{
    public float spawnTime = 2;
    public float score = 0;
    public bool playerIsDeath = false;
    public bool gameInPause = false;
    public bool firstBlood;

    public void Restart()
    {       
        spawnTime = 2;
        score = 0;
        playerIsDeath = false;
        gameInPause = false;
        firstBlood = false;
    }

    public void AddScore(float score)
    {
        this.score += score;
    }
}
