using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Group options")]
    public uint numZombies = 5;
    public float spawnSpread = 2.0f;
    public GameObject[] zombies;
    public GameObject zombiePrefab;


    private AIVision aivision;

    void Start()
    {
        zombies = new GameObject[numZombies];

        for (int i = 0; i < numZombies; ++i)
        {

            Vector3 position = this.transform.position + Random.insideUnitSphere * spawnSpread;
            position.y = this.transform.position.y;
            Vector3 direction = Random.insideUnitSphere; // random vector direction
            direction.y = 0;
            zombies[i] = (GameObject)Instantiate(zombiePrefab, position, Quaternion.LookRotation(direction), this.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}