using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHand : MonoBehaviour {

    public float speed = 1f;
	void Update ()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
	}
}
