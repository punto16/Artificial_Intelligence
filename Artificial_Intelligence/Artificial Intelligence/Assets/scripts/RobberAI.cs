using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberAI : MonoBehaviour
{
    //[Header('Robber Wander options')]
    private Vector3 localTarget;
    private Vector3 worldTarget;
    private int amountCalculations = 10;
    //private float wanderTimer = 4f;
    private float radius = 5.0f;
    private float offset = 3.0f;

    public float aproachDistance = 5f;
    public float stealDistance = 2f;
    public GameObject toStealObject;

    private GameObject robber;
    private GameObject policeman;
    private GameObject[] hidingSpots;

    private NavMeshAgent robberAgent;

    private float timer = 0.05f; // 20 times per second

    private bool haveRobbed = false;
    private bool policeClose = false;

    private enum RobberState
    {
        Wander,
        Approach,
        Hide
    }
    private RobberState currentState = RobberState.Wander;

    // Start is called before the first frame update
    void Start()
    {
        robber = this.gameObject;
        policeman = GameObject.FindGameObjectWithTag("policeman");
        //toStealObject = GameObject.FindGameObjectWithTag("toSteal");
        hidingSpots = GameObject.FindGameObjectsWithTag("hideSpot");

        robberAgent = this.GetComponent<NavMeshAgent>();

        StartCoroutine(RobberFSM());
    }


    IEnumerator RobberFSM()
    {
        while (true)
        {
            switch (currentState)
            {
                case RobberState.Wander:
                    //Debug.Log("WANDER STATE");
                    currentState = inRange(policeman, toStealObject, aproachDistance) ? RobberState.Wander : RobberState.Approach;
                    policeClose = true;
                    Wander();
                    //yield return 4;
                    break;

                case RobberState.Approach:
                    //Debug.Log("APPROACH STATE");
                    currentState = inRange(policeman, toStealObject, aproachDistance) ? (RobberState.Wander) : (RobberState.Approach);

                    if (inRange(robber, toStealObject, stealDistance))
                    {
                        //Debug.Log("ROBBER TOOK ITEM!");
                        if (toStealObject != null)
                            Destroy(toStealObject);
                        haveRobbed = true;
                        currentState = RobberState.Hide;
                    }
                    robberAgent.destination = toStealObject.transform.position;

                    break;

                case RobberState.Hide:
                    //Debug.Log("HIDE STATE");
                    while (true)
                    {
                        //Here we will find the farest hiding spot from police
                        float maxDistance = 0.0f;
                        GameObject bestHidingSpot = null;
                        foreach (GameObject hideSpot in hidingSpots)
                        {
                            //calculating the distance bitween each hiding spot and the average position between policeman and it's target
                            float distance = Vector3.Distance((policeman.transform.position + policeman.GetComponent<NavMeshAgent>().destination)/2f, hideSpot.transform.position);
                            if (distance > maxDistance)
                            {
                                maxDistance = distance;
                                bestHidingSpot = hideSpot;
                            }
                        }
                        robberAgent.destination = bestHidingSpot.transform.position;
                        yield return timer;
                    }
            }

            yield return new WaitForSeconds(timer);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool inRange(GameObject a, GameObject b, float range)
    {
        //Debug.Log(Vector3.Distance(a.transform.position, b.transform.position));
        return Vector3.Distance(a.transform.position, b.transform.position) <= range ? true : false;
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

        robberAgent.destination = worldTarget;
    }

}
