using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIVision : MonoBehaviour
{
    public Camera frustum;
    public LayerMask mask;

    public NavMeshAgent agent;
    private bool targetFound = false;
    private GameObject target;

    public Vector3 localTarget;
    public Vector3 worldTarget;
    public int amountCalculations = 5;
    public float radius = 5.0f;
    public float offset = 3.0f;

    void goStupid()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("Villager"))
                    {
                        //Debug.Log("One Zombie saw Villager");
                        targetFound = true;
                        target = col.gameObject;
                        gameObject.SendMessageUpwards("GoForTarget", target);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (targetFound)
        {
            if (!agent.pathPending)
            {
                agent.destination = target.transform.position;
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.3f)
            {
                Wander();
            }
        }
    }

    void SetTarget(GameObject newtarget)
    {
        target = newtarget;
        targetFound = true;
    }

    private void Wander()
    {
        NavMeshHit hit;

        int count = 0;
        do
        {
            localTarget = Random.insideUnitSphere * radius;
            localTarget += new Vector3(0, 0, offset);

            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
            count++;
            if (count >= amountCalculations) break;

        } while (!NavMesh.SamplePosition(worldTarget, out hit, 1.0f, NavMesh.AllAreas));

        agent.destination = worldTarget;
    }

}