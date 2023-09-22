using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wanderLogic : MonoBehaviour
{
    // Update is called once per frame
    public float radius;
    public float offset;

    public NavMeshAgent agent;

    void Update()
    {
        //agent.destination = target.position;

        // parameters: float radius, offset;
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);

        agent.destination = worldTarget;

        worldTarget.y = 0f;
    }
}
