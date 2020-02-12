using UnityEngine;
using System.Collections;

public class PlayerTankController : MonoBehaviour
{
    public GameObject Bullet;

    private Transform Turret;
    private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;
    private float turretRotSpeed = 10.0f;
    private float maxForwardSpeed = 300.0f;
    private float maxBackwardSpeed = -300.0f;

    // Bullet shotting rate
    protected float shootRate = 0.5f;
    protected float elapsedTime;

    private void Start()
    {
        // Tank Settings
        rotSpeed = 150.0f;

        // Get the turret
        Turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = Turret.GetChild(0).transform;
    }

    private void Update()
    {
        UpdateWeapon();
        UpdateControl();
    }

    void UpdateWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= shootRate)
            {
                // Reset time
                elapsedTime = 0.0f;

                // Instantiate Bullet
                Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    void UpdateControl()
    {
        // Mouse Aim ---------------------
        // Generate a plane that intersects the transform's position with an upwards normal
        Plane playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0, 0));

        // Generate a ray from the cursor
        Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the sursor ray intersects the plane
        float hitDistance = 0;

        // If the ray is parallel to the plane, Raycast will return false
        if (playerPlane.Raycast(RayCast, out hitDistance))
        {
            // Get the point along the ray that hits the calculated distance
            Vector3 rayHitPoint = RayCast.GetPoint(hitDistance);

            Quaternion targetRotation = Quaternion.LookRotation(rayHitPoint - transform.position);

            Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, targetRotation, Time.deltaTime * turretRotSpeed);
        }
    }
}