using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateGameSettings", order = 52)]
public class GameSettings : ScriptableObject
{
    public float sfx;
    public float music;
    public int resolution;
    public bool fullScreen;
}
