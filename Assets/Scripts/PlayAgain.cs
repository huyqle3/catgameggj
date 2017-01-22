using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {

    public float speed;
    public Transform Pos1;
    public Transform Pos2;
    public GameOver gameOver;

	// Use this for initialization
	void Start () {

        gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();

        transform.position = Vector3.MoveTowards(transform.position, Pos1.position, speed);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameOver.gameOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos2.position, speed);
        }
		
	}
}
