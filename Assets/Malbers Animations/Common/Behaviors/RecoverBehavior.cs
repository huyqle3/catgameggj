using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    /// <summary>
    /// This Behavior Updates and resets all parameters to their original state
    /// </summary>
    public class RecoverBehavior : StateMachineBehaviour
    {
        Animal animal;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animal = animator.GetComponent<Animal>();
            animator.GetComponent<Rigidbody>().constraints = animal.StillConstraints;
            animal.IsInAir = false;
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            animal.IsInAir = false;
            animator.applyRootMotion = false;

            //Smooth Stop when RecoverFalls
            if (stateInfo.normalizedTime < 0.9f)
            {
                animator.GetComponent<Rigidbody>().drag = Mathf.Lerp(animator.GetComponent<Rigidbody>().drag, 3, Time.deltaTime * 10f);
            }
            else
            {
                animator.applyRootMotion = true;
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.applyRootMotion != true) animator.applyRootMotion = true;
            //Reset the Drag
            animator.GetComponent<Rigidbody>().drag = 0;
        }
    }
}