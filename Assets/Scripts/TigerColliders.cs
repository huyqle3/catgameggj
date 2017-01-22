using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerColliders : MonoBehaviour {

    public float angle = 30f;

    FishScore fishScore;
    FishScore lives;
    WaterMove water;
    public Transform player;

    void Awake () {

        fishScore = GameObject.Find("FishScore").GetComponent<FishScore>();
        lives = GameObject.Find("LivesScore").GetComponent<FishScore>();
        water = GameObject.Find("Water").GetComponent<WaterMove>();
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fish")
        {
            fishScore.score += 1;
            //print("fish touched");
            Throw(col.gameObject.transform, player);
        }

        else if (col.gameObject.tag == "Water") {
            if (water.canKill)
            {
                water.canKill = false;
                lives.score -= 1;
            }
        }
    }

    void Throw(Transform fish, Transform target)
    {
        fish.gameObject.layer = LayerMask.NameToLayer("Catching");
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.1f, 0.1f), 0f);
        Vector3 dif = (target.position + offset) - fish.position;
        float vx = dif.x;
        float vy = dif.y + 0.5f * Physics.gravity.magnitude;
        float vz = dif.z;

        rb.velocity = new Vector3(vx, vy, vz);


        Destroy(fish.gameObject, 3f);
    }
}
