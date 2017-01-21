using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    public class SleepBehavior : StateMachineBehaviour
    {

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state 
        [Header("and move to the next animation Clip ")]
        [Header("This will count every Cycle ")]


        public bool CyclesFromController;

        public int Cycles, transitionID;
        int currentCycle;

        void CyclesToSleep(Animator animator, AnimatorStateInfo stateInfo)
        {
            if (CyclesFromController)
            {
                Cycles = animator.GetComponent<Animal>().GotoSleep;
            }
            currentCycle++;

            if (currentCycle >= Cycles)
            {
                animator.GetComponent<Animal>().SetIntID(transitionID);
                currentCycle = 0;
            }
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!stateInfo.IsTag("Idle"))
            {
                CyclesToSleep(animator, stateInfo);
            }
        }


        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
           if (animator.GetComponent<Animal>().Tired == 0)
                animator.GetComponent<Animal>().SetIntID(0);

            //If is in idle, start to count , to get to sleep
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                animator.GetComponent<Animal>().Tired++;
                if (animator.GetComponent<Animal>().Tired >= animator.GetComponent<Animal>().GotoSleep - 1)
                {
                    //Get to the Sleep Mode
                    animator.GetComponent<Animal>().SetIntID(-100);


                    animator.GetComponent<Animal>().Tired = 0;
                }
            }
            else
            {
                CyclesToSleep(animator, animator.GetCurrentAnimatorStateInfo(0));
            }
        }
    }
}

