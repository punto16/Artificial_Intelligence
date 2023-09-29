   // Patrol.cs
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
    using System.Collections.Generic;


    public class patrolLogic : MonoBehaviour 
    {

    double abs(double a){
        if (a >= 0)
            a = a * -1;
        return a;
    }


        public Transform[] points;
        public NavMeshAgent target;
        private int destPoint = 0;
        private NavMeshAgent agent;
        private double originalTargetSpeed;

        void Start () {
            agent = GetComponent<NavMeshAgent>();
            target = GetComponent<NavMeshAgent>();
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            target.autoBraking = false;
            agent.autoBraking = false;
            originalTargetSpeed = target.speed;
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
            if (!target.pathPending && target.remainingDistance < 0.5f)
                GotoNextPoint();

            if (abs(target.position - agent.position) >= 4)
            {
                target.speed = agent.speed;
            }
            else 
            {
                target.speed = originalTargetSpeed;
            }
            agent.destination = target.position;
        }
    }
