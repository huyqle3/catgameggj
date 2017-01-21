using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    public class JumpBehavior : StateMachineBehaviour
    {
        [Header("Jump Range to deactivate rigid body constraints while in the air")]
        public float startJump;
        public float finishJump;

        public float FallRay = 1;
        public float treshold = 0.5f;
        
        [Header("Jump Range to activate Jump Over a Cliff Transition")]
        public float startEdge = 0.5f;
        public float finishEdge = 0.6f;
        public float GroundRay = 0.5f;

        float jumpPoint;

        RaycastHit JumpRay;
        Animal animal;


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animal = animator.GetComponent<Animal>();
          //  animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            if (Physics.Raycast(animal.Pivot_Hip.GetPivot, -animal.transform.up, out JumpRay, animal.Pivot_Chest.multiplier * animal.ScaleFactor, animal.GroundLayer))
            {
                jumpPoint = JumpRay.point.y;
                animal.SetIntID(0);
                //animator.SetInteger("IDInt", 0); // IDInt=0 Means that the animal is starting a Jump.
            }
        }



        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //This code is execute when the animal can change to fall state if there's no future ground to land on
            if (Physics.Raycast(animal.Pivot_fall, -animal.transform.up, out JumpRay, animal.Pivot_Chest.multiplier * animal.ScaleFactor * FallRay, animal.GroundLayer))
            {
                if (animal.debug)
                {
                    Debug.DrawRay(animal.Pivot_fall, -animal.transform.up * animal.Pivot_Chest.multiplier * animal.ScaleFactor * FallRay, Color.magenta);
                }
                // Debug.Log("Ray point diference: " + (jumpPoint - JumpRay.point.y));
                //If if finding a lower jump point;
                if ((jumpPoint - JumpRay.point.y) <= treshold * animal.ScaleFactor)
                {
                    animal.SetIntID(0);
                }
                else
                {
                    animal.SetIntID(111);
                }
            }
            else
            {
                animal.SetIntID(111);// this IDInt value+fall will make go to fall state
            }

            //-----------------------------------------Get jumping on a cliff -------------------------------------------------------------------------------

            if (Physics.Raycast(animal.Pivot_Chest.GetPivot, -animal.transform.up, out JumpRay, GroundRay * animal.ScaleFactor, animal.GroundLayer))
            {
                if (stateInfo.normalizedTime >= startEdge && stateInfo.normalizedTime <= finishEdge)
                {
                 // Debug.DrawRay(animal.Pivot_Chest.GetPivot, -animal.transform.up * GroundRay, Color.yellow);
                    animal.SetIntID(110);
                }
            }


            //Modify  Jump RiggidBody constraints.. when the animal is jumping Unfreeze all movement axis ... until he reaches the ground

            if (stateInfo.normalizedTime > startJump)
            {
                if (!animal.IsInAir)
                {
                    animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    animator.GetComponent<Animal>().IsInAir = true;
                }
            }


            if (stateInfo.normalizedTime >= finishJump && !animator.GetNextAnimatorStateInfo(0).IsTag("Fall"))
            {
                if (animal.IsInAir)
                {
                    //animator.GetComponent<Rigidbody>().constraints = animal.StillConstraints;
                    animal.IsInAir = false;
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Make sure the constraints go back to normal when exit animator to the locomotion state
            if (animator.GetNextAnimatorStateInfo(0).IsTag("Locomotion"))
            {
                animator.GetComponent<Rigidbody>().constraints = animal.StillConstraints;
            }

            //Resets the Jump Point
            jumpPoint = 0;
        }
    }
}