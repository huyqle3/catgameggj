using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerCatchTest : MonoBehaviour {
    public float angle = 30f;
    public Transform player;
    public GameObject fishPrefab;
    public float speed = 0.5f;
    public float vel = 1f;

    Vector3 target;

    void Start()
    {
        target = transform.position;

        StartCoroutine(WhenToThrow());
        StartCoroutine(NewTarget());
    }

    void Update()
    {
        if ((transform.position - target).sqrMagnitude > 0.025f)
        {
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }

    IEnumerator WhenToThrow()
    {
        while (true)
        {
            GameObject fish = Instantiate(fishPrefab, transform.position, Quaternion.identity);
            Throw(fish.transform, player, angle);
            yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        }
    }

    IEnumerator NewTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.7f, 1f));
            target = new Vector3(Random.Range(-4.5f, 4.5f), 0.45f, Random.Range(5f, 10f));
        }
    }

    void Throw(Transform fish, Transform target, float a)
    {
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Vector3 offset = Vector3.zero; //new Vector3(Random.Range(-0.5f, 0.5f), 0f, 0f);
		Vector3 dir = ((target.position - transform.position) * 2) + offset;
        float h = dir.y;
        dir.y = 0;

        float distance = dir.magnitude;
        float radAngle = a * Mathf.Deg2Rad;
        dir.y = distance * Mathf.Tan(radAngle);
        float vel = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radAngle));

        rb.velocity = vel * fish.InverseTransformDirection(dir.normalized);

        Destroy(fish.gameObject, 3f);
    }
}
