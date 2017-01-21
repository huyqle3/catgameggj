using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    /// <summary>
    /// All Callbacks in here
    /// </summary>
    public partial class Animal 
    {

        /// CallBack from the EnterWater.cs
        public void EnterWater(bool water)
        {
            isInWater = water;
        }

        ///Call Back from an activation Zone
        public void ActionEmotion(int ID)
        {
            actionID = ID;
        }

        /// Find the direction hit vector and send it to the Damage Behavior;
        public void getDamaged(DamageValues DV)
        {
            life = life - (DV.Amount - defense);
            if (life > 0)
            {
                if (!_currentState.IsTag("Damage"))
                    damaged = true;
                hitDirection = (-DV.Mycenter + DV.Theircenter).normalized;
            }
            else
            {
                if (!death)
                {
                    _anim.SetTrigger(HashIDsAnimal.deathHash);
                    death = true;
                }
            }
        }

        public void Attacking(bool attack)
        {
            isAttacking = attack;
        }

        

        public void SetIntID(int value)
        {
            idInt = value;
        }

        public void SetFloatID(float value)
        {
            idfloat = value;
        }

        public void SetIntIDRandom(int range)
        {
            idInt = Random.Range(1, range + 1);
        }

        /// This will return false if is not in the Jumping state or if is not jumping in the desired half.
        protected bool isJumping(float normalizedtime, bool half)
        {
            if (half)  //if is jumping the first half
            {

                if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
                {
                    if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= normalizedtime)
                        return true;
                }

                if (_anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))  //if is transitioning to jump
                {
                    if (_anim.GetNextAnimatorStateInfo(0).normalizedTime <= normalizedtime)
                        return true;
                }
            }
            else //if is jumping the second half
            {
                if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
                {
                    if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= normalizedtime)
                        return true;
                }

                if (_anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))  //if is transitioning to jump
                {
                    if (_anim.GetNextAnimatorStateInfo(0).normalizedTime > normalizedtime)
                        return true;
                }
            }
            return false;
        }

        protected bool isJumping()
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
            {
                return true;
            }
            if (_anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))
            {
                return true;
            }
            return false;
        }

      
    }
}
