using UnityEngine;
using System.Collections;


namespace MalbersAnimations
{
    public class SoundBehavior : StateMachineBehaviour
    {

        public AudioClip[] sounds;


        public bool playOnEnter;
        public bool playOnExit;
        public bool playOnTime;
        [Range(0, 1)]
        public float NormalizedTime = 0.5f;


        [Space]
        [Space]
        [Range(-0.5f, 3)]
        public float pitch = 1;
        // public bool loop;



        bool played;
        AudioSource _audio;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            played = false;

            if (playOnEnter)
            {
                PlaySound(animator);
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (playOnTime)
            {
                if (!played && stateInfo.normalizedTime > NormalizedTime)
                {
                    PlaySound(animator);
                    played = true;
                }

            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (playOnExit)
            {
                PlaySound(animator);
            }
        }

        void PlaySound(Animator animator)
        {
            _audio = animator.transform.root.GetComponent<AudioSource>();

            if (_audio)

                if (sounds.Length > 0 && _audio.enabled)
                {
                    _audio.clip = sounds[Random.Range(0, sounds.Length)];
                    _audio.pitch = pitch;
                    _audio.Play();
                }
        }
    }
}