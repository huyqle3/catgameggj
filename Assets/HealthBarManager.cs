using System.Collections;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;

public class HealthBarManager : MonoBehaviour {
	public int livesLeft;
	private Transform[] positions;
    public GameObject[] hearts;
	public int numLivesPossible;
	public GameObject heartPositionPrefab;
    public GameObject tiger;

	// Use this for initialization
	void Start () {
		setupPositionList();
		setNumLives(livesLeft); // just for debugging purposes.

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setNumLives (int livesLeft)
	{
		if (livesLeft < 0 || livesLeft > numLivesPossible) {
			Debug.LogWarning ("Invalid number of lives left");
			return;
		}
		for(int i = 0; i < livesLeft; i++) {
			positions[i].gameObject.SetActive (true);
		}
		
	}

    public void destroyHeart(int heartNum)
    {
        if (hearts != null || hearts.Length != 0) {
            Destroy(hearts[heartNum]);
        }
    }

	private void setupPositionList ()
	{
		positions = new Transform[numLivesPossible];
        hearts = new GameObject[numLivesPossible];
		for (int i = 0; i < numLivesPossible; i++) {
            Vector3 nextLocation = new Vector3 ((0.35f * i) + 1.2f, tiger.transform.position.y + 0.5f, tiger.transform.position.z + 1.7f);
			GameObject heart = (GameObject) Instantiate (heartPositionPrefab, nextLocation, Quaternion.identity);
            heart.transform.parent = tiger.transform;

            hearts[i] = heart;
			positions[i] = heart.transform;
		}
	}

}
