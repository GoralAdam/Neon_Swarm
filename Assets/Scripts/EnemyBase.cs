using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Settings")]
    public float health = 50f;
    public float speed = 2f;
    public int scoreValue = 100;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    protected float nextFireTime;

    protected Transform player;

    protected virtual void Start()
    {
        // Znajdź obiekt z tagiem player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        Move();
        
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    protected abstract void Move(); // Randomowy ruch przeciwnika

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected virtual void Shoot()
    {
        if (projectilePrefab && firePoint)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    protected virtual void Die()
    {
        // Efek wybuchu
        Destroy(gameObject);
    }
}