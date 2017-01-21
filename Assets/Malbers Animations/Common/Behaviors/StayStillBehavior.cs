using UnityEngine;
using System.Collections;
namespace MalbersAnimations
{

    /// <summary>
    /// This is when sometimes the characters is still rolling on a slope..
    /// </summary>
    public class StayStillBehavior : StateMachineBehaviour
    {
        public bool enter, exit;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (enter)
                animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (exit)
                animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

    }
}