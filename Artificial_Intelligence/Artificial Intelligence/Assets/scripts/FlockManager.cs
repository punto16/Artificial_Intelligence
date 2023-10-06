using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using UnityEngine;

public class FlockManager: MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish;
    public GameObject[] allFish;
    public Vector3 swimLimits;
    public bool bounded;
    public bool randomize;
    public bool followLider;
    public GameObject lider;
    [Range (0, 5)] public float minSpeed;
    [Range (0,5)] public float maxSpeed;
    [Range (0,10)] public float distance;
    [Range (0,5)] public float rotationSpeed;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), 0, UnityEngine.Random.Range(-5.0f, 5.0f));

        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 randVec = new Vector3(UnityEngine.Random.Range(-swimLimits.x, swimLimits.x), 0, UnityEngine.Random.Range(-swimLimits.z, swimLimits.z));
            Vector3 pos = this.transform.position + randVec; // random position
            Vector3 randomize = new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), 0, UnityEngine.Random.Range(-5.0f, 5.0f)); // random vector direction
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos,
                                Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<Flock>().fManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
