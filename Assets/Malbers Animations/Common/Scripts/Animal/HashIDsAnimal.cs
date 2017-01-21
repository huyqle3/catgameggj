using UnityEngine;
namespace MalbersAnimations
{
        public class HashIDsAnimal : MonoBehaviour
    {

        [HideInInspector]    public static int verticalHash =   Animator.StringToHash("Vertical");
        [HideInInspector]    public static int horizontalHash = Animator.StringToHash("Horizontal");
     // [HideInInspector]    public static int updownHash = Animator.StringToHash("UpDown");

        [HideInInspector]    public static int standHash =      Animator.StringToHash("Stand");

        [HideInInspector]    public static int jumpHash =       Animator.StringToHash("_Jump");
 
     // [HideInInspector]    public static int dodgeHash =      Animator.StringToHash("Dodge");
        [HideInInspector]    public static int fallHash =       Animator.StringToHash("Fall");
     // [HideInInspector]    public static int groundedHash =   Animator.StringToHash("Grounded");
        [HideInInspector]    public static int slopeHash = Animator.StringToHash("Slope");

        [HideInInspector]    public static int shiftHash =      Animator.StringToHash("Shift");

     // [HideInInspector]    public static int flyHash = Animator.StringToHash("Fly");
     

        [HideInInspector]    public static int attack1Hash =    Animator.StringToHash("Attack1");
     // [HideInInspector]    public static int attack2Hash = Animator.StringToHash("Attack2");


        [HideInInspector]    public static int deathHash =      Animator.StringToHash("Death");
    
        [HideInInspector]    public static int damagedHash = Animator.StringToHash("Damaged");
        [HideInInspector]    public static int stunnedHash = Animator.StringToHash("Stunned");

        [HideInInspector]    public static int IDIntHash = Animator.StringToHash("IDInt");
        [HideInInspector]    public static int IDFloatHash = Animator.StringToHash("IDFloat");

        [HideInInspector]    public static int swimHash = Animator.StringToHash("Swim");
     // [HideInInspector]    public static int underWaterHash = Animator.StringToHash("Underwater");

        [HideInInspector]    public static int action = Animator.StringToHash("Action");
        [HideInInspector]    public static int actionID = Animator.StringToHash("IDAction");
    }
}