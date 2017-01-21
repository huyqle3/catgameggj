using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    public class FallBehavior : StateMachineBehaviour
    {
        RaycastHit JumpRay; 

        [Tooltip("Lower Distance to Stretch the feet")]
        public float LowerDistance;
        Animal animal;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animal = animator.GetComponent<Animal>();
            animal.SetIntID(1);
          //animator.SetFloat("IDFloat", 1);
            animal.IsInAir = true;

            //Resets MaxHeight
            animal.MaxHeight = 0;

            animator.applyRootMotion = false;
            animator.GetComponent<Rigidbody>().drag = 0;
            animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Physics.Raycast(animator.transform.position, -animal.transform.up, out JumpRay, 100, animal.GroundLayer))
            {
                if (animal.MaxHeight < JumpRay.distance)
                {
                    //get the lower Distance 
                    animal.MaxHeight = JumpRay.distance;
                }

                //Fall Blend between fall animations ... Higher Execute one animation
                float animalFloat = Mathf.Lerp(animator.GetFloat("IDFloat"), (JumpRay.distance - LowerDistance) / animal.MaxHeight, Time.deltaTime * 5f);
                animator.SetFloat("IDFloat", animalFloat);
            }
        }
    }
}