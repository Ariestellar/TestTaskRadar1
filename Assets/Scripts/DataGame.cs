using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataGame", order = 1)]
public class DataGame : ScriptableObject
{
    public LevelSpeedGame[] levelsSpeedGames;
    public int countStartLife;    
    public int startSpeed;    
}

[System.Serializable]
public class LevelSpeedGame
{
    public string Name;
    public int countScoreForActivation;
    public float increaseSpeed;
}
