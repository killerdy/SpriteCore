using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//public class Fish
public class Fish : MonoBehaviour 
{
    //Start is called before the first frame update
    //public float speed = 5f;
    public float cohesionRadius = 5f;
    public float alignmentRadius = 3f;
    public float separationRadius = 2f;

    public float cohesionWeight = 1f;
    public float alignmentWeight = 1f;
    public float separationWeight = 1f;
    public float randomWeight = 0.5f;
    public float maxSpeed = 1f;
    public Vector2 velocity;
    private List<Slime> nearbyFish = new List<Slime>();

    private Vector2 cohesionForce, alignmentForce, separationForce;

    private float updateTime=0.5f;
    private float nowTime;
    private void Update()
    {
        nowTime += Time.deltaTime;
        if(nowTime > updateTime) { nowTime = 0; GetVelocity(); }
    }
    private void GetVelocity()
    {
        FindNearbyFish();
        CalculateForce();
        CalculateVelocity();
    }
    void FindNearbyFish()
    {
        nearbyFish.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Max(cohesionRadius, Mathf.Max(alignmentRadius, separationRadius)));
        foreach (Collider2D collider in colliders)
        {
            Slime fish = collider.GetComponent<Slime>();
            if (fish != null)
            {
                nearbyFish.Add(fish);
            }
        }

    }
    void CalculateVelocity()
    {
        velocity += (cohesionForce * cohesionWeight + alignmentForce * alignmentWeight + separationForce
            * separationWeight);
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }
    #region CalculateForce
    int c1, c2, c3;
    Vector2 center, averageVelocity, separation;
    void CalculateForce()
    {
        if (nearbyFish.Count == 0)
        {
            cohesionForce = alignmentForce = separationForce = Vector2.zero;
            return;
        }
        c1 = c2 = c3 = 0;
        center = averageVelocity = separation = Vector2.zero;
        foreach (Slime fish in nearbyFish)
        {
            float distance = Vector2.Distance(transform.position, fish.transform.position);
            if (distance < cohesionRadius)
            {
                center += (Vector2)fish.transform.position;
                c1++;
            }
            if (distance < alignmentRadius)
            {
                averageVelocity += fish.rb.velocity;
                c2++;
            }
            if (distance < separationRadius)
            {
                separation += (Vector2)(transform.position - fish.transform.position).normalized / Mathf.Max(1f, distance);
                c3++;
            }

        }
        if (c1 != 0)
        {
            center /= c1;
            cohesionForce = (center - (Vector2)transform.position).normalized;
        }
        if (c2 != 0)
        {
            averageVelocity /= c2;
            alignmentForce = averageVelocity.normalized;
        }
        if (c3 != 0)
        {
            separation /= c3;
            separationForce = separation.normalized;
        }

    }
    #endregion
}
