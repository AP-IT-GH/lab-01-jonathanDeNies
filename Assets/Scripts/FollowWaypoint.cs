using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    // Waypoints Array: Stores all the waypoints the tank will navigate through
    public Transform[] waypoints;
    public int currentWaypoint = 0;

    public float speed = 5f;
    public float rotSpeed = 5f;
    public float reachDistance = 2f;


    void Update()
    {
        // Safety check
        if (waypoints.Length == 0) return;

        // Direction to the current waypoint
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;

        // Smooth Rotation: gradual turning toward the waypoint
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * rotSpeed
        );

        // Forward Movement: move along the local Z-axis
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Distance Check: switch to next waypoint when close enough
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distance < reachDistance)
        {
            currentWaypoint++;

            // Loop back to the first waypoint
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
