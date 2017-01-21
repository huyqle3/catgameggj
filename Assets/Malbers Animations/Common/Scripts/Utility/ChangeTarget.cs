using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
  public class ChangeTarget : MonoBehaviour
    {
        public Transform[] targets;
        public KeyCode key = KeyCode.T;
        int current;
  

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                current++;
                SendMessage("SetTarget", targets[(current % targets.Length)]);
            }
        }
    }
}
