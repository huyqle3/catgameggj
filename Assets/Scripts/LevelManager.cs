using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
