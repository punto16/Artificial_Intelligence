using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Group options")]
    public int numZombies = 5;
    public Vector3 spawnLimits = new Vector3(5, 0, 5);
    public GameObject[] zombies;
    public GameObject zombiePrefab;

    //private AIVision aivision;

    //True if zombie found target
    //False if zombie is wandering
    private bool zombieState = false; 


    void Start()
    {
        zombies = new GameObject[numZombies];
        for (int i = 0; i < numZombies; ++i)
        {
            Vector3 randVec = new Vector3(UnityEngine.Random.Range(-spawnLimits.x, spawnLimits.x),0, UnityEngine.Random.Range(-spawnLimits.z, spawnLimits.z));
            Vector3 pos = this.transform.position + randVec; // random position

            zombies[i] = (GameObject)Instantiate(zombiePrefab, pos, Quaternion.identity,this.transform);
            //zombies[i].GetComponent<Flock>().fManager = this;
        }
    }


    void GoForTarget(GameObject target)
    {
        //Debug.Log("Zombie telling other zombies to target villager");
        gameObject.BroadcastMessage("SetTarget", target);
    }

    // Update is called once per frame
    void Update()
    {
        if(!zombieState)
        {
            gameObject.BroadcastMessage("goStupid");
        }
    }
}