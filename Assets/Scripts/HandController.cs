using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
	{
        ps.Emit(15);
		Destroy (other.gameObject);
	}
}