using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour {

    public Transform fakeHand;
	
	void Update ()
    {
        transform.position = fakeHand.position + ((Camera.main.transform.forward * 0.75f + Vector3.forward * 0.25f) * 2f);
    }
}
