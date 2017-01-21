using UnityEngine;
using System.Collections;


namespace MalbersAnimations
{
    public class DamagedBehavior : StateMachineBehaviour
    {
        public string parameter = "IDInt";
        int Side = 0;

        //Calculate the Direction from where is coming the hit and plays hits respective animation.
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            Animal animal = animator.GetComponent<Animal>();
            Vector3 hitdirection = animal.HitDirection;
            Vector3 forward = animator.transform.forward;
            bool left = true;

            hitdirection.y = 0;
            forward.y = 0;

            //Get The angle
            float angle = Vector3.Angle(forward, hitdirection);

            //Calculate witch directions comes the hit
            if (Vector3.Dot(animal.transform.right, animal.HitDirection) > 0)  left = false;
           

            if (left)
            {
                     if (angle > 0 && angle <= 60)    Side = 3;
                else if (angle > 60 && angle <= 120)  Side = 2;
                else if (angle > 120 && angle <= 180) Side = 1;
            }
            else
            {
                     if (angle > 0 && angle <= 60)    Side = -3;
                else if (angle > 60 && angle <= 120)  Side = -2;
                else if (angle > 120 && angle <= 180) Side = -1;
            }

            animal.SetIntID(Side);
            animator.SetInteger(parameter, Side);

            //Reset Hit Direction and Damage bool
            animal.HitDirection = Vector3.zero;
            animal.Damaged = false;
        }
    }
}