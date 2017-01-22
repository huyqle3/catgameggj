using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCol : MonoBehaviour {

    public WaterMove water;

	// Use this for initialization
	void Start () {

        water = GameObject.Find("Water").GetComponent<WaterMove>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Water") {
            if(water.canKillFish){
                print("water hit fish");
                Destroy(this.gameObject);
            }
        }
    }
}
