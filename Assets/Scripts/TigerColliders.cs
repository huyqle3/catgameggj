using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerColliders : MonoBehaviour {

    public FishScore fishScore;
    public FishScore lives;
    public WaterMove water;

	// Use this for initialization
	void Start () {

        fishScore = GameObject.Find("FishScore").GetComponent<FishScore>();
        lives = GameObject.Find("LivesScore").GetComponent<FishScore>();
        water = GameObject.Find("Water").GetComponent<WaterMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fish")
        {
            fishScore.score += 1;
            //print("fish touched");
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Water") {
            if (water.canKill)
            {
                water.canKill = false;
                lives.score -= 1;
            }
        }
    }
}
