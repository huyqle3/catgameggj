using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    ParticleSystem ps;
    FishScore fishScore;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        fishScore = GameObject.Find("FishScore").GetComponent<FishScore>();
    }

    void OnTriggerEnter(Collider other)
	{
        ps.Emit(15);
        fishScore.score += 1;
        Destroy (other.gameObject);
	}
}