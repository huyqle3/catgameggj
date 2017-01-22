using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject Fish;

    public float timerMax = 0;
    private float timer = 0;
    private float posx;
    private float posz;

    public bool spawn = false;
    public bool spawn1 = false;
    public bool spawn2 = false;
    public bool spawn3 = false;

    public bool isSpawn1 = false;
    public bool isSpawn2 = false;
    public bool isSpawn3 = false;

    public int fishMax;
    public int Count = 0;
    private int fishCount;

    public WaterMove water;

    // Use this for initialization
    void Start()
    {
        if (this.name == "FishSpawner1") { spawn1 = true; }
        if (this.name == "FishSpawner2") { spawn2 = true; }
        if (this.name == "FishSpawner3") { spawn3 = true; }

        water = GameObject.Find("Water").GetComponent<WaterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (water.goDown) {
            Count += 1;
            if (Count == 1)
            {
                FishOnBeach();
            }
        }

        if (water.goDown == false) {
            Count = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)) { FishOnBeach(); }
    }

    void FishOnBeach(){

        fishCount = Random.Range(1, fishMax);

        if(spawn1)
        {
            for (int i = 0; i < fishCount; i++)
            {
                SpawnFish();
            }
        }

        if(spawn2)
        {
            for (int i = 0; i < fishCount; i++)
            {
                SpawnFish();
            }
        }

        if(spawn3)
        {
            for (int i = 0; i < fishCount; i++)
            {
                SpawnFish();
            }
        }
    }

    public void SpawnFish()
    {
        posx = Random.RandomRange(-6f, 6f);
        posz = Random.RandomRange(-1f, 1f);
        GameObject FishName = null;
        FishName = (GameObject)Instantiate(Fish, transform.position, Quaternion.identity);
        FishName.transform.Translate(posx, 0.2f, posz);
    }
}
