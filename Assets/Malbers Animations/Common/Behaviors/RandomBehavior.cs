using UnityEngine;
using System.Collections;


namespace MalbersAnimations
{
    /// <summary>
    /// Is Used to execute random animations in a State Machine 
    /// </summary>
    /// 
   
    public class RandomBehavior : StateMachineBehaviour
    {
        public int Range;

        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            //animator.GetComponent<Animal>().SetIntIDRandom(Range);
            animator.SendMessage("SetIntIDRandom", Range,SendMessageOptions.DontRequireReceiver);
        }
    }
}