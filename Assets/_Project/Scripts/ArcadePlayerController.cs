using UnityEngine;

public class ArcadePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 11f;
    
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    
    private float nextFireTime = 0f;
    private Vector2 currentMoveInput=Vector2.zero;
    public void SetMoveInput(Vector2 input)
    {
        currentMoveInput = input;
    }

    void Update()
    {
        transform.position += (Vector3)currentMoveInput * moveSpeed * Time.deltaTime;

        if (currentMoveInput != Vector2.zero)
        {
            transform.up = Vector3.Slerp(transform.up, currentMoveInput, rotationSpeed * Time.deltaTime);
        }
        
        if(Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
