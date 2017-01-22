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
        Vector3 offset = Vector3.zero; //new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f);
        Vector3 dir = ((target.position - fish.position) * 2) + offset;
        float h = dir.y;
        dir.y = 0;

        float distance = dir.magnitude;
        float radAngle = angle * Mathf.Deg2Rad;
        dir.y = distance * Mathf.Tan(radAngle);
        float vel = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radAngle));

        rb.velocity = vel * fish.InverseTransformDirection(dir.normalized);

        Destroy(fish.gameObject, 3f);
    }
}
