using UnityEngine;

public class ScoutEnemy : EnemyBase
{
    [Header("Orbit Settings")]
    public float orbitDistance = 4f;
    private float currentOrbitSpeed;
    private int orbitDirection;

    [Header("Advanced Avoidance")]
    public float sensorLength = 2f;     
    public float avoidanceForce = 5f;   
    public LayerMask obstacleLayer;      

    protected override void Start()
    {
        base.Start();
        currentOrbitSpeed = speed * Random.Range(0.8f, 1.5f);
        orbitDirection = (Random.value > 0.5f) ? 1 : -1;
        orbitDistance += Random.Range(-0.5f, 0.5f);
    }

    protected override void Move()
    {
        if (player == null) return;

        Vector3 vectorToPlayer = player.position - transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;
        Vector3 moveStep = Vector3.zero;

        //OBLICZANIE GŁÓWNEGO RUCHU
        if (distanceToPlayer > orbitDistance + 0.5f)
        {
            moveStep = vectorToPlayer.normalized * speed;
        }
        else
        {
            Vector3 tangent = new Vector3(vectorToPlayer.y, -vectorToPlayer.x, 0).normalized;
            moveStep = tangent * orbitDirection * currentOrbitSpeed;

            // Korekta promienia orbity
            Vector3 radialCorrection = vectorToPlayer.normalized * (distanceToPlayer - orbitDistance);
            moveStep += radialCorrection * speed;
        }

        //LOGIKA RADARU (Korekta kursu kolizyjnego)
        moveStep += CalculateSensorAvoidance(moveStep.normalized);

        // Zastosowanie ruchu
        transform.position += moveStep * Time.deltaTime;

        LookAtPlayer();
    }

    private Vector3 CalculateSensorAvoidance(Vector3 currentDirection)
    {
        Vector3 avoidance = Vector3.zero;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, sensorLength, obstacleLayer);

        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            // Jeśli wykryto kolizję na kursie, tworzymy wektor przeciwny do kierunku lotu (hamowanie/cofanie)
            avoidance = -currentDirection * avoidanceForce;

            // Dodatkowo odpychamy statek lekko na zewnątrz orbity, żeby się wyminęły
            Vector3 pushAwayFromObstacle = (transform.position - (Vector3)hit.point).normalized;
            avoidance += pushAwayFromObstacle * (avoidanceForce * 0.5f);
            
            Debug.DrawRay(transform.position, currentDirection * sensorLength, Color.red); 
        }
        else
        {
            Debug.DrawRay(transform.position, currentDirection * sensorLength, Color.green);
        }

        return avoidance;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }
}