using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    private Dictionary<int, GameObject> enemiesInScene;
    public GameObject defaultEnemyPrefab;
    public PlayerController player;
    public Text scoreText; // text field for the scoreboard
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        // set initial score on scoreboard
        scoreText.text = "SCORE: " + score.ToString("000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreAmt) 
    {
        score += scoreAmt;
        scoreText.text = "SCORE: " + score.ToString("000");
    }
}
