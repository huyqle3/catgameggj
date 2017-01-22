using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishScore : MonoBehaviour
{
    public bool life = false;
    public int score = 0;
    public Text myText;
    public int lives;
    // public GameObject[] Hearts;

    void Start()
    {
        if (life)
        {
            Reset(lives);
        }
        else
            Reset(0);

        myText = GetComponent<Text>();

    }

    void Update()
    {
        myText.text = score.ToString();
    }

    public void Reset(int value)
    {
        score = value;
    }


}
