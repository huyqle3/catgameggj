using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public bool YouWin = false;
    public bool YouLose = false;
    public bool gameOver = false;

    public FishScore fishScore;
    public FishScore lives;
    public Text myText;

    void Awake()
    {
        Time.timeScale = 1f;
    }

	// Use this for initialization
	void Start () {

        YouWin = false;
        YouLose = false;
        gameOver = false;
        fishScore = GameObject.Find("FishScore").GetComponent<FishScore>();
        lives = GameObject.Find("LivesScore").GetComponent<FishScore>();
        myText = GetComponent<Text>();
        myText.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

        /*if (fishScore.score >= 10 && !YouLose){
            gameOver = true;
            YouWin = true;
            myText.text = "You Win!";
            myText.enabled = true;
        }*/

        if (lives.score <= 0 && !YouWin){
            gameOver = true;
            YouLose = true;
            myText.text = "You Lose!";
            myText.enabled = true;
            Time.timeScale = 0f;
        }
		
	}
}
