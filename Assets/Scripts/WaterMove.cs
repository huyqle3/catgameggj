using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour {

    public float speed;
    public float timerMax;
    public bool canKill = false;
    public bool canKillFish = false;
    public bool waveGo = false;
    public bool pos1Go = false;
    public bool pos2Go = false;
    public bool goDown = false;
    public Transform pos1;
    public Transform pos2;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= timerMax && !waveGo) {
            canKill = true;
            canKillFish = true;
            pos1Go = true;
            waveGo = true;
        }

        if (pos1Go)
        {
            goDown = false;
            transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed);
            if (this.transform.position.z == pos1.transform.position.z)
            {
                print("wave reached");
                pos1Go = false;
                pos2Go = true;
                canKillFish = false;
                goDown = true;
            }
        }

        if (pos2Go)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed);
            if (this.transform.position.z == pos2.transform.position.z)
            {
                timer = 0;
                waveGo = false;
                pos2Go = false;
            }
        }
	}
}
