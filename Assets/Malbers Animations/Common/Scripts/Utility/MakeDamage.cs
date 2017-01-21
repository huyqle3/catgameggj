using UnityEngine;
using System.Collections;


namespace MalbersAnimations
{
   
    public class MakeDamage : MonoBehaviour
    {
        public float damageMultiplier;

        void OnTriggerEnter(Collider other)
        {
            Animal myAnimal = transform.root.GetComponent<Animal>();

            Animal.DamageValues DV = new Animal.DamageValues(other.bounds.center, GetComponent<Collider>().bounds.center, damageMultiplier* ( myAnimal? myAnimal.attackStrength : 1));

            if (myAnimal)
            {
                if (myAnimal.IsAttacking)
                {
                    other.transform.root.SendMessage("getDamaged", DV, SendMessageOptions.DontRequireReceiver);
                    myAnimal.IsAttacking = false;
                }
            }
            else
            {
                other.transform.root.SendMessage("getDamaged", DV, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
