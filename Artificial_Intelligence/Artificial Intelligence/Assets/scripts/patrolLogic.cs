   // Patrol.cs
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
    using System.Collections.Generic;


    public class patrolLogic : MonoBehaviour 
    {
        public Transform agentTransform;
        public Transform[] points;
        public NavMeshAgent target;
        public Transform targetTransform;
        private int destPoint = 0;
        private NavMeshAgent agent;

        private int randomDirection;
        private int initialPivot;

        void Start () {

            int randomDirection = Random.Range(0,2);
            int initialPivot = Random.Range(0,4);

            agent = GetComponent<NavMeshAgent>();
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            target.autoBraking = false;
            agent.autoBraking = false;

            if (randomDirection == 0) System.Array.Reverse(points);
            destPoint = initialPivot;
            GotoNextPoint();
        }


        void GotoNextPoint() {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            target.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }


        void Update () {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!target.pathPending && target.remainingDistance < 2f)
                GotoNextPoint();

            if (Vector3.Distance(targetTransform.position, agentTransform.position) >= 3)
            {
                target.isStopped = true;
            }
            else 
            {
                target.isStopped = false;
            }
            agent.destination = targetTransform.position;
        }
    }

