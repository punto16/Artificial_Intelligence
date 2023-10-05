using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class myManager : MonoBehaviour
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

    private int counter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
