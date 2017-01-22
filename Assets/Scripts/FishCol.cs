using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCol : MonoBehaviour
{
    WaterMove water;

	void Awake ()
    {
        water = GameObject.Find("Water").GetComponent<WaterMove>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Water") {
            if(water.canKillFish){
                print("water hit fish");
                Destroy(gameObject);
            }
        }
    }

    
}
