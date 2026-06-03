using UnityEngine;

public class BossEnemy : EnemyBase
{
    [Header("Boss Stats & Phases")]
    public float phaseTwoHealthThreshold = 200f; 
    public Transform[] multipleFirePoints;    

    [Header("Boss Movement")]
    public float orbitDistance = 7f;
    public float horizontalRange = 8f;
    
    [Header("Boss Rotation")]
    public float rotationSpeed = 2f; 

    protected override void Move()
    {
        if (player == null) return;

        // Boss porusza się lewo-prawo, utrzymując dystans od gracza
        float targetX = Mathf.PingPong(Time.time * speed, horizontalRange * 2) - horizontalRange;
        float targetY = player.position.y + orbitDistance;

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

        // Płynne obracanie
        LookAtPlayerSmooth();
    }

    private void LookAtPlayerSmooth()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Obrót
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    protected override void Shoot()
    {
        if (projectilePrefab == null) return;

        // strzał z wielu punktów naraz
        if (health <= phaseTwoHealthThreshold)
        {
            if (multipleFirePoints != null && multipleFirePoints.Length > 0)
            {
                foreach (Transform pt in multipleFirePoints)
                {
                    Instantiate(projectilePrefab, pt.position, pt.rotation);
                }
            }
        }
        else 
        {
            //standardowy strzał z klasy bazowej
            base.Shoot();
        }
    }
}