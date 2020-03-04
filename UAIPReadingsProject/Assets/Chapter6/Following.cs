using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public Path path;
    public float speed = 20.0f;
    public float mass = 5.0f;
    public bool isLooping = true;

    // Actual Speed
    private float curSpeed;

    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;

    Vector3 velocity;

    private void Start()
    {
        pathLength = path.Length;
        curPathIndex = 0;

        // gets current velocity
        velocity = transform.forward;
    }

    private void Update()
    {
        // Unify the Speed
        curSpeed = speed * Time.deltaTime;

        targetPoint = path.GetPoint(curPathIndex);

        // If point is reached, move to the next one on the path
        if (Vector3.Distance(transform.position, targetPoint) < path.Radius)
        {
            // Stops movement if path is finished
            if (curPathIndex < pathLength - 1)
            {
                curPathIndex++;
            }
            else if (isLooping)
            {
                curPathIndex = 0;
            }
            else
            {
                return;
            }
        }

        // Move until the end of the path is reached
        if (curPathIndex >= pathLength)
        {
            return;
        }

        // Get velocity towards the path
        if (curPathIndex >= pathLength - 1 && !isLooping)
        {
            velocity += Steer(targetPoint, true);
        }
        else
        {
            velocity += Steer(targetPoint);
        }

        // Move according to the velocity
        transform.position += velocity;

        // Rotate towards the desired velocity
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    //Steering algorithm to steer the vector towards the target
    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;

        //Normalise the desired Velocity
        desiredVelocity.Normalize();

        //Calculate the velocity according to the speed
        if (bFinalPoint && dist < 10.0f)
            desiredVelocity *= (curSpeed * (dist / 10.0f));
        else
            desiredVelocity *= curSpeed;

        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }
}
