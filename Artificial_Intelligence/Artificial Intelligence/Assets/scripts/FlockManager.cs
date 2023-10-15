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
    public Vector3 swimLimits = new Vector3(5, 5, 5);
    public bool bounded;
    public bool randomize;
    public bool followLider;
    public GameObject lider;
    [Header("")]
    [Range (1.0f, 5.0f)] public float minSpeed;
    [Range (1.0f, 5.0f)] public float maxSpeed;
    [Range (1.0f,10.0f)] public float distance;
    [Range (0.0f, 5.0f)] public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 randVec = new Vector3(UnityEngine.Random.Range(-swimLimits.x, swimLimits.x), UnityEngine.Random.Range(-swimLimits.y, swimLimits.y), UnityEngine.Random.Range(-swimLimits.z, swimLimits.z));
            Vector3 pos = this.transform.position + randVec; // random position

            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().fManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
