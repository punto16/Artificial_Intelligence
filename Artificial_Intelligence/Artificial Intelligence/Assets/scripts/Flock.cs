using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager fManager;
    public float speed;

    float timeMin = 0.3f;
    float timeMax = 0.8f;
    float waitTime = 0.5f;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        speed = UnityEngine.Random.Range(fManager.minSpeed, fManager.maxSpeed);
    }

    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);

        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            Flocking();
            waitTime = UnityEngine.Random.Range(timeMin, timeMax);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), fManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    void Flocking()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in fManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= fManager.distance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;


        Vector3 align = Vector3.zero;
        num = 0;
        foreach (GameObject go in fManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= fManager.distance)
                {
                    align += go.GetComponent<Flock>().direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, fManager.minSpeed, fManager.maxSpeed);
        }
        Vector3 separation = Vector3.zero;
        foreach (GameObject go in fManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= fManager.distance)
                    separation -= (transform.position - go.transform.position) / (distance * distance);
            }
        }

        direction = (cohesion + align + separation).normalized * speed;
        if (direction == Vector3.zero)
        {
            direction.x = 1;
            direction.y = 1;
            direction.z = 1;

        }
    }
}