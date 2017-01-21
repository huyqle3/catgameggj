using UnityEngine;
using System.Collections;
using System;

namespace MalbersAnimations
{
    public class ActionZone : MonoBehaviour
    {

        public Actions actionsToUse;
        public int ID;
        public int index;
        
   
        public bool HeadOnly;

        void OnTriggerEnter(Collider other)
        {
            if (HeadOnly)
            {
                if (other.name.Contains("Head"))
                {
                    other.transform.root.SendMessage("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                other.transform.root.SendMessage("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
            }
        }

       void OnTriggerStay(Collider other)
        {
            if (HeadOnly)
            {
                if (other.name.Contains("Head"))
                {
                    other.transform.root.SendMessage("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                other.transform.root.SendMessage("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
            }
        }

        void OnTriggerExit(Collider other)
        {
             other.transform.root.SendMessage("ActionEmotion", -1, SendMessageOptions.DontRequireReceiver);
        }
    }
}