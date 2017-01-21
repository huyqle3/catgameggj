using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    public partial class Animal
    {
        public class DamageValues
        {
            public Vector3 Mycenter;
            public Vector3 Theircenter;
            public float Amount = 0;

            public DamageValues(Vector3 mycenter, Vector3 theircenter, float amount = 0)
            {
                Mycenter = mycenter;
                Theircenter = theircenter;
                Amount  = amount;
            }
        }

        public enum Ground
        {
            walk = 1, trot = 2, run = 3
        }

        #region Animal Components 
        protected Animator _anim;
        protected AnimatorStateInfo _currentState;
        protected Transform _transform;
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected Transform Cam;
        #endregion

        #region Animator Parameters Variables

        protected Vector3 movementAxis;  // In this variable will store vertical (Z) and horizontal Y

        protected bool
            speed1, //Walk
            speed2, //Trot
            speed3, //Run
            jump,   
            shift,
            down,
            damaged,
            fly,
            fall, fallback,
            death,
            isInWater, isInAir, isAttacking, isHit,
            swim, 
            grounded,
            attack1, attack2,
            stun,
            action,
            stand = true;

        bool backray, frontray = false;

        protected float
            upDown,
            anim_Float,
            speed,
            direction,
            groundSpeed = 1f,
            waterlevel,
            slope,
            idfloat,
            _Height;

        private int
            idInt = 1,
            actionID = -1,
            tired;
        #endregion

        #region Inspector Entries
        
        [Tooltip("Activate the correct additive Animation for offset the Bones")]
        public int  animalTypeID;

        [Header("Ground  ─────────────────────────────────")]
        [Space]
        [Tooltip("Specify wich layer are ground")]
        public LayerMask GroundLayer;
        public Ground StartSpeed = Ground.walk;
        [Tooltip("Distance from the ground")]
        public float height;
        [Space]
        [Tooltip("Add Walk Speed greater than 1 the animal will Slide")]
        public float WalkSpeed = 1f;
        [Tooltip("Add Trot Speed greater than 1 the animal will Slide")]
        public float TrotSpeed = 1f;
        [Tooltip("Add Run Speed greater than 1 the animal will Slide")]
        public float RunSpeed = 1f;
        [Space]
        [Tooltip("Add Turn Speed .... Zero will rotate with the default animation rotation")]
        public float TurnSpeed = 0f;
        [Space]
        [Space]
        [Range(0, 1)]
        public float maxSlope;
        [Range(0, 100)]
        public int GotoSleep;
        public float SnapToGround = 20f;
        public bool swapSpeed;

        [Space]

        [Header("Water  ──────────────────────────────────")]
        [Space]
        [Tooltip("Water Level for the dragon to Swim on the water")]
        public float waterLine = 0f;
        public float swimSpeed = 1f;
        public float swimTurn = 0f;

       

        #endregion

        [Header("Atributes  ──────────────────────────────")]
        [Space]
        [SerializeField] public float life = 3;
        [SerializeField] public float defense = 0;
        [SerializeField] public float attackStrength = 0;

        [Space]
        [Space]
        public bool debug;

        //------------------------------------------------------------------------------
        #region Modify_the_Position_Variables

        RaycastHit hit_Hip, hit_Chest;
        Vector3 _fallVector, hitDirection;

        protected float
             turnAmount,
             forwardAmount,
             scaleFactor = 1,
             maxHeight;

        Pivots[] pivots;
        Pivots _Chest, _Hip;
        #endregion

        #region Properties

        public float GroundSpeed
        {
            get { return groundSpeed; }
        }

        public float MaxHeight
        {
            set { maxHeight = value; }
            get { return maxHeight; }
        }

        public int Tired
        {
            set { tired = value; }
            get { return tired; }
        }

        public float AnimalFloat
        {
            set { anim_Float = value; }
            get { return anim_Float; }
        }

        public bool IsInWater
        {
            get { return isInWater; }
        }
        public bool Speed1
        {
            get { return speed1; }
            set { speed1 = value; }
        }

        public bool Speed2
        {
            get { return speed2; }
            set { speed2 = value; }
        }

        public bool Speed3
        {
            get { return speed3; }
            set { speed3 = value; }
        }

        public bool Jump
        {
            get { return jump; }
            set { jump = value; }
        }
        public bool Shift
        {
            get { return shift; }
            set { shift = value; }
        }
        public bool Down
        {
            get { return down; }
            set { down = value; }
        }

        public bool Damaged
        {
            get { return damaged; }
            set { damaged = value; }
        }
        public bool Fly
        {
            get { return fly; }
            set { fly = value; }
        }

        public bool Death
        {
            get { return death; }
            set { death = value; }
        }

        public bool Attack1
        {
            get { return attack1; }
            set { attack1 = value; }
        }

        public bool Stun
        {
            get { return stun; }
            set { stun = value; }
        }

        public bool Action
        {
            get { return action; }
            set { action = value; }
        }

        public int ActionID
        {
            get { return actionID; }
            set { actionID = value; }
        }

        public bool IsInAir
        {
            get { return isInAir; }
            set { isInAir = value; }
        }

        public bool Stand
        {
            get { return stand; }
        }

        public Vector3 HitDirection
        {
            get { return hitDirection; }
            set { hitDirection = value; }
        }

        public float ScaleFactor
        {
            get { return scaleFactor; }
        }

        public Pivots Pivot_Hip
        {
            get { return _Hip; }
        }
        public Pivots Pivot_Chest
        {
            get { return _Chest; }
        }

        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }


        public Vector3 Pivot_fall
        {
            get { return _fallVector; }
        }

        public RigidbodyConstraints StillConstraints
        {
            get { return RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; }
        }

        public Vector3 MovementAxis
        {
            get { return movementAxis; }
            set { movementAxis = value; }
        }

        #endregion
    }
}
