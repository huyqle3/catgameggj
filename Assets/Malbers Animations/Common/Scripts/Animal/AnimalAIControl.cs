using UnityEngine;
using System.Collections;


namespace MalbersAnimations
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AnimalAIControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }
        public Animal animal { get; private set; } // the character we are controlling
        public Transform target;


        // Use this for initialization
        void Start()
        {
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            animal = GetComponent<Animal>();
            agent.updateRotation = false;
            agent.updatePosition = true;

        }

        // Update is called once per frame
        void Update()
        {

            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                animal.Move(agent.desiredVelocity,true);
            else
                animal.Move(Vector3.zero,true);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
