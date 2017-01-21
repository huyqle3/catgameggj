using UnityEngine;
using System.Collections.Generic;
using System;
using UnityStandardAssets.CrossPlatformInput;


namespace MalbersAnimations
{
    public enum InputType
    {
        Input, Key
    }

    public enum InputButton
    {
        Press, Down, Up
    }

    [Serializable]
    public class InputRow
    {
        [SerializeField]
        public bool active = true;
        [SerializeField]
        public string name = "Variable";
        [SerializeField]
        public InputType type;
        [SerializeField]
        public string input = "Value";
        [SerializeField]
        public KeyCode key;
        [SerializeField]
        public InputButton GetPressed;
    }

    public class MalbersInput : MonoBehaviour
    {
        public Animal _animal;
        private Vector3 m_CamForward;
        private Vector3 m_Move;
        private Transform m_Cam;
        public List<InputRow> inputs = new List<InputRow>();
        public bool cameraBaseInput;
        private float h;
        private float v;

        void Awake()
        {
            _animal = GetComponent<Animal>();
        }

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
             h = CrossPlatformInputManager.GetAxis("Horizontal");
             v = CrossPlatformInputManager.GetAxis("Vertical");
            SetInput();
        }

        Vector3 CameraInputBased()
        {
            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 1, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
            return m_Move;
        }


        void SetInput()
        {
            if (cameraBaseInput)
            {
                _animal.Move(CameraInputBased(),true);
            }
            else
            {
                _animal.Move(_animal.MovementAxis = new Vector3(h, 0, v),false);
            }

            if (isActive("Attack1")) _animal.Attack1 = GetInput("Attack1");         //Get the Attack1 button

            if (isActive("Action")) _animal.Action = GetInput("Action");  //Get the Action/Emotion button
           
            if (isActive("Jump"))  _animal.Jump = GetInput("Jump");

            if (isActive("Shift")) _animal.Shift = GetInput("Shift");           //Get the Shift button

            if (isActive("Fly")) _animal.Fly = GetInput("Fly");
            if (isActive("Down")) _animal.Down = GetInput("Down");             //Get the Down button

            if (isActive("Stun")) _animal.Stun = GetInput("Stun");             //Get the Stun button change the variable entry to manipulate how the stun works
            if (isActive("Death")) _animal.Death = GetInput("Death");            //Get the Death button change the variable entry to manipulate how the death works
            if (isActive("Damaged")) _animal.Damaged = GetInput("Damaged");


            if (isActive("Speed1")) _animal.Speed1 = GetInput("Speed1");                //Walk
            if (isActive("Speed2")) _animal.Speed2 = GetInput("Speed2");                //Trot
            if (isActive("Speed3")) _animal.Speed3 = GetInput("Speed3");                //Run


            //Get the Death button change the variable entry to manipulate how the death works
        }

        /// <summary>
        /// Thit will set the correct Input, from the Unity Input Manager or Keyboard.. you can always modify this code
        /// </summary>
        /// <param name="name">The name set on the list</param>
        /// <param name="down">True if is (GetKeyDown || GetButtonDown)... False is (GetKey || GetButton) </param>
        /// <returns></returns>
        bool GetInput(string name)
        {
            foreach (InputRow item in inputs)
            {
                if (item.name.ToUpper() == name.ToUpper() && item.active)
                {
                    switch (item.type)
                    {
                        case InputType.Input:
                            if (item.GetPressed == InputButton.Down)
                            {
                                return CrossPlatformInputManager.GetButtonDown(item.input);
                            }
                            else if (item.GetPressed == InputButton.Up)
                            {
                                return CrossPlatformInputManager.GetButtonUp(item.input);
                            }
                            return CrossPlatformInputManager.GetButton(item.input);
                            
                        case InputType.Key:
                            if (item.GetPressed == InputButton.Down)
                            {
                                return Input.GetKeyDown(item.key);
                            }
                            else if (item.GetPressed == InputButton.Up)
                            {
                                return Input.GetKeyUp(item.key);
                            }
                                return Input.GetKey(item.key);
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the input is active
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool isActive(string name)
        {
            foreach (InputRow item in inputs)
            {
                if (item.name.ToUpper() == name.ToUpper())  return item.active;
            }
            return false;
        }
    }
}