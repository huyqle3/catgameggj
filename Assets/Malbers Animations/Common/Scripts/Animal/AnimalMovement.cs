using UnityEngine;
using System.Collections;

namespace MalbersAnimations
{
    public partial class Animal
    {
        protected Vector3 SurfaceNormal;
        protected int SpeedCount;
        protected bool c;

        void Awake()
        {
            //modify the skeleton to fit the animal 
            GetComponent<Animator>().SetInteger("Type", animalTypeID); //Adjust the layer for the curret animal Type
        }

        void Start()
        {
            SetStart();
        }

        protected void SetStart()
        {
            _anim = GetComponent<Animator>();
            _transform = transform;
            //_collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            pivots = GetComponentsInChildren<Pivots>();     //Pivots are Strategically Transform objects use to cast rays used by the animal
            scaleFactor = _transform.localScale.y;          //TOTALLY SCALABE animal
            groundSpeed = (int)StartSpeed;
            SpeedCount = (int)StartSpeed - 1;

        }

        protected void LinkingAnimator(Animator anim_)
        {
            if (!death)
            {
                anim_.SetFloat(HashIDsAnimal.verticalHash, speed);
                anim_.SetFloat(HashIDsAnimal.horizontalHash, direction);

                anim_.SetFloat(HashIDsAnimal.slopeHash, slope);
                anim_.SetBool(HashIDsAnimal.shiftHash, shift);
                anim_.SetBool(HashIDsAnimal.standHash, stand);
                anim_.SetBool(HashIDsAnimal.jumpHash, jump);
                anim_.SetBool(HashIDsAnimal.attack1Hash, attack1);
                anim_.SetBool(HashIDsAnimal.damagedHash, damaged);
                anim_.SetBool(HashIDsAnimal.fallHash, fall);
                anim_.SetBool(HashIDsAnimal.stunnedHash, stun);
                anim_.SetBool(HashIDsAnimal.action, action);
                anim_.SetBool(HashIDsAnimal.stunnedHash, stun);
                anim_.SetBool(HashIDsAnimal.swimHash, swim);
                anim_.SetInteger(HashIDsAnimal.actionID, actionID);
                anim_.SetInteger(HashIDsAnimal.IDIntHash, idInt);//////

            }
            else  //Triggers the Death
            {
                if (!_currentState.IsTag("Death")) anim_.SetTrigger(HashIDsAnimal.deathHash);
            }
        }

        //Add more Rotations to the current Turn animations -------------------------------------------
        protected void AdditionalTurn()
        {
            float Turn;

            Turn = TurnSpeed;

            if (!_currentState.IsTag("Locomotion")) Turn = 0;

            if (swim) Turn = swimSpeed;

            if (movementAxis.z >= 0)
            {
                _transform.Rotate(_transform.up, Turn * 3 * movementAxis.x * Time.deltaTime);
            }
            else
            {
                _transform.Rotate(_transform.up, Turn * 3 * -movementAxis.x * Time.deltaTime);
            }

            //More Rotation when jumping and falling... in air rotation------------------
            if (isJumping() || fall && !fly && !swim && !stun)
            {
                if (movementAxis.z >= 0)
                    _transform.Rotate(_transform.up, 100 * movementAxis.x * Time.deltaTime);
                else
                    _transform.Rotate(_transform.up, 100 * -movementAxis.x * Time.deltaTime);
            }
        }

        //Add more Speed to the current Move animations------------------------------------------------
        protected void AdditionalSpeed()
        {
            float amount = 0;
            float axis = movementAxis.z;
            Vector3 direction = _transform.forward;

            if (groundSpeed == 1) amount = WalkSpeed;
            if (groundSpeed == 2) amount = TrotSpeed;
            if (groundSpeed == 3) amount = RunSpeed;

            if (!_currentState.IsTag("Locomotion")) amount = 0;

            if (swim) amount = swimSpeed;

            _transform.position = Vector3.Lerp(_transform.position, _transform.position + direction * amount * axis / 5f, Time.deltaTime);
        }

        //Terrain Logic
        protected void FixPosition()
        {
            scaleFactor = _transform.localScale.y;
            _Height = height * scaleFactor;
            _Hip = pivots[0];
            _Chest = pivots[1];

            //Calculate the Slope Factor
            slope = ((_Chest.Y - _Hip.Y) / 2) / scaleFactor / maxSlope;


            float distanceHip, distanceChest = 0;

            backray = frontray = false;

            //Ray From Hip to the ground
            if (Physics.Raycast(_Hip.GetPivot, -_transform.up, out hit_Hip, scaleFactor * _Hip.multiplier, GroundLayer))
            {
                if (debug) Debug.DrawRay(hit_Hip.point, hit_Hip.normal * 0.2f, Color.blue);
                distanceHip = hit_Hip.distance;

                // if the hip ray has a Big angle ignore it
                if (hit_Hip.normal.y > 0.7)
                    backray = true;
            }
            else distanceHip = _Height;

            //Ray From Chest to the ground
            if (Physics.Raycast(_Chest.GetPivot, -_transform.up, out hit_Chest, scaleFactor * _Chest.multiplier, GroundLayer))
            {
                if (debug) Debug.DrawRay(hit_Chest.point, hit_Chest.normal * 0.2f, Color.red);
                distanceChest = hit_Chest.distance;

                // if the hip ray if in Big Angle ignore it
                if (hit_Chest.normal.y > 0.7)
                    frontray = true;
            }
            else distanceChest = _Height;


            //------------------------------------------------Terrain Adjusment--------------------------------------------

            //---------------------------------Calculate the Align vector of the terrain-----------------------------------
            Vector3 direction = (hit_Chest.point - hit_Hip.point).normalized;
            Vector3 Side = Vector3.Cross(Vector3.up, direction).normalized;
            SurfaceNormal = Vector3.Cross(direction, Side).normalized;

            // ------------------------------------------Orient To Terrain--------------------------------------------------  

            Quaternion finalRot = Quaternion.FromToRotation(_transform.up, SurfaceNormal) * _rigidbody.rotation;

            // If the character is falling, jumping or swimming smoothly aling with the horizontal

            //FallFromEdge
            if (isInAir || isJumping())
            {
                //Don't Align whe is UpHill
                if (slope < 0 || (fall && !isJumping()))
                {
                    finalRot = Quaternion.FromToRotation(_transform.up, Vector3.up) * _rigidbody.rotation;
                    _transform.rotation = Quaternion.Lerp(_transform.rotation, finalRot, Time.deltaTime * 5f);
                }
            }
            else
            {
                if (!swim && slope <= 1 && backray && frontray)
                {
                    _transform.rotation = Quaternion.Lerp(_transform.rotation, finalRot, Time.deltaTime * 10f);
                }
            }

            float distance = distanceHip;

            // if is landing on the front feets
            if (!backray)
            {
                distance = distanceChest;
            }

            float realsnap = SnapToGround; // change in the inspector the  adjusting speed for the terrain


            //-----------------------------------------Snap To Terrain-------------------------------------------
            if (distance > _Height && !isInAir && !swim)
            {
                float diference = _Height - distance;
                _transform.position = Vector3.Lerp(_transform.position, _transform.position + new Vector3(0, diference, 0), Time.deltaTime * realsnap);
            }
            //-----------------------------------------Snap To Terrain-------------------------------------------
          else if (distance < _Height)
            {
                if (!fall && !isInAir)
                {
                    float diference = _Height - distance;
                    _transform.position = Vector3.Lerp(_transform.position, _transform.position + new Vector3(0, diference, 0), Time.deltaTime * realsnap);
                    _rigidbody.constraints = StillConstraints;
                }
               
            }
        }

        /// Fall Logic
        protected void Falling()
        {
            RaycastHit hitpos;

            _fallVector = _Chest.GetPivot + (_transform.forward.normalized * groundSpeed * 0.15f * ScaleFactor);
            if (debug) Debug.DrawRay(_fallVector, -transform.up * _Chest.multiplier * scaleFactor, Color.magenta);

            if (Physics.Raycast(_fallVector, -_transform.up, out hitpos, _Chest.multiplier * scaleFactor, GroundLayer))
            {
                fall = false;
            }
            else
            {
                fall = true;
            }
        }
        /// Swim Logic
        protected void Swimming()
        {
            RaycastHit WaterHitCenter;
            Pivots waterPivot = pivots[2];

            //Front RayWater Cast
            if (Physics.Raycast(waterPivot.transform.position, -_transform.up, out WaterHitCenter, _Height * pivots[2].multiplier, LayerMask.GetMask("Water")))
            {
                waterlevel = WaterHitCenter.point.y; //get the water level when find water
                isInWater = true;
            }
            else
            {
                isInWater = false;
            }

            if (isInWater) //if we hit water
            {
                if ((Pivot_Chest.GetPivot.y < waterlevel && !swim) || (fall && !isJumping(0.5f, true)))
                {
                    swim = true;
                    _rigidbody.constraints = StillConstraints;
                }

                //Stop swimming when he is coming out of the water              //Just come out the water when hit the back ray
                if (hit_Chest.distance < _Height && !isJumping() && !fall && (backray && swim))
                {
                    swim = false;
                }
            }

            if (swim)
            {
                fall = false;
                isInAir = false;

                float angleWater = Vector3.Angle(_transform.up, WaterHitCenter.normal);

                Quaternion finalRot = Quaternion.FromToRotation(_transform.up, WaterHitCenter.normal) * _rigidbody.rotation;

                //Smoothy rotate until is Aling with the Water
                if (angleWater > 0.5f)
                    _transform.rotation = Quaternion.Lerp(_transform.rotation, finalRot, Time.deltaTime * 10);
                else
                    _transform.rotation = finalRot;

                //Smoothy Aling position with the Water
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x, waterlevel - _Height + waterLine, _transform.position.z), Time.deltaTime * 5f);

                if (!isInWater)
                {
                    swim = false;
                }
            }
        }

        /// Check for a behind Cliff so it will stop going backwards.
        protected bool IsFallingBackwards(float ammount = 0.5f)
        {
            RaycastHit BackRay;
            Vector3 FallingVectorBack = pivots[0].transform.position + _transform.forward * -1 * ammount;

            if (debug) Debug.DrawRay(FallingVectorBack, -_transform.up * pivots[0].multiplier);

            if (Physics.Raycast(FallingVectorBack, -_transform.up, out BackRay, scaleFactor * pivots[0].multiplier, GroundLayer))
            {
                return false;
            }
            else
            {
                if (!swim && movementAxis.z < 0) return true;
            }

            return false;
        }

        // call to prevent activatation a button after a time
        protected IEnumerator SpeeedCount(float seconds)
        {
            if (!c)
            {
                SpeedCount++;
                c = true;
            }
            yield return new WaitForSeconds(seconds);
            c = false;
        }

        protected virtual void SpeedChange()
        {
            int shiftSpeed = 0;

            int directionMult = 1;

            if (swapSpeed)
            {
                if (shift)
                {
                    StartCoroutine(SpeeedCount(0.45f));
                }

                if ((SpeedCount % 3) == 0)
                {
                    Speed1 = true;
                    Speed3 = Speed2 = false;

                }
                if ((SpeedCount % 3) == 1)
                {
                    Speed2 = true;
                    Speed1 = Speed3 = false;
                }
                if ((SpeedCount % 3) == 2)
                {
                    Speed3 = true;
                    Speed1 = Speed2 = false;
                }
            }
            else
            {
                //Shift Key multiplier
                if (shift)
                {
                    shiftSpeed = 1;
                    directionMult = 2; //Turn 180 when pressing shift
                }
            }

            int smooth = 2;


            //Change velocity on ground
            if (speed1) groundSpeed = 1f;
            if (speed2) groundSpeed = 2f;
            if (speed3) groundSpeed = 3f;

            if (groundSpeed == 1) smooth = 10;
            if (groundSpeed == 2) smooth = 5;
            if (groundSpeed == 3) smooth = 2;

            float maxspeed = groundSpeed;
            if (swim) maxspeed = 1;


            //SlowDown When going UpHill
            if (slope >= 0.5)
            {
                if (maxspeed > 1) maxspeed--;
            }

            //prevent to go uphill
            if (slope >= 1)
            {
                maxspeed = 0;
                smooth = 10;
            }

            if (movementAxis.z < 0)
            {
                if (IsFallingBackwards())
                {
                    maxspeed = 0;
                    smooth = 10;
                }
            }
            speed = Mathf.Lerp(speed, movementAxis.z * (maxspeed + shiftSpeed), Time.deltaTime * smooth);  //smoothly transitions bettwen velocities
            direction = Mathf.Lerp(direction, movementAxis.x * directionMult, Time.deltaTime * 8f);    //smoothly transitions bettwen directions
        }

        public virtual void Move(Vector3 move, bool Direction)
        {
            if (Direction)
            {
                // convert the world relative moveInput vector into a local-relative
                // turn amount and forward amount required to head in the desired
                // direction.
                if (move.magnitude > 1f) move.Normalize();
                move = transform.InverseTransformDirection(move);

                move = Vector3.ProjectOnPlane(move, SurfaceNormal);
                turnAmount = Mathf.Atan2(move.x, move.z);
                forwardAmount = move.z;

                movementAxis = new Vector3(turnAmount, 0,Mathf.Abs(forwardAmount));

                if (!stand)
                    _transform.Rotate(Vector3.up, movementAxis.x * Time.deltaTime * 40f);
            }
            else
            {
                movementAxis = move;
            }
        }

        protected void MovementSystem()
        {
            AdditionalTurn();
            AdditionalSpeed();


            SpeedChange();

            //Check if the Character is Standing
                                            //Speed
            if ((movementAxis.x != 0) || (Mathf.Abs(speed) > 0.2f))
                stand = false;
            else stand = true;

            if (jump || damaged || stun || fall || swim || fly || isInAir || tired >= GotoSleep)
                stand = false; //Stand False when doing some action

        
            if (tired >= GotoSleep) tired = 0;
        }

        void FixedUpdate()
        {
            //All Raycast Stuff Here
            _currentState = _anim.GetCurrentAnimatorStateInfo(0);
            FixPosition();
            Falling();
            Swimming();
        }

        void Update()
        {
            MovementSystem();
        }

        void LateUpdate()
        {
            //Set all Animator Parameters
            LinkingAnimator(_anim);
        }


#if UNITY_EDITOR
        /// <summary>
        /// DebugOptions
        /// </summary>
        void OnDrawGizmos()
        {
            if (debug)
            {
                pivots = GetComponentsInChildren<Pivots>();
                Gizmos.color = Color.magenta;
                float sc = transform.localScale.y;

                if (backray)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(hit_Hip.point, 0.05f * sc);
                }
                if (frontray)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(hit_Chest.point, 0.05f * sc);
                }


            }
        }
#endif
    }
}