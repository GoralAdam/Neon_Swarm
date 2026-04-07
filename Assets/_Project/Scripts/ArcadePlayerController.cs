using UnityEngine;
using UnityEngine.UI;

public class ArcadePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 11f;
    
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    
    [Header("Experience System")]
    public int currentExp = 0;
    public int maxExp = 10;
    public Slider expBar;
    
    private float nextFireTime = 0f;
    private Vector2 currentMoveInput=Vector2.zero;
    public void SetMoveInput(Vector2 input)
    {
        currentMoveInput = input;
    }

    void Start()
    {
        if (expBar != null)
        {
            expBar.maxValue = maxExp;
            expBar.value = currentExp;
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exp"))
        {
            Destroy(other.gameObject);
            
            currentExp++;

            if (expBar != null)
            {
                expBar.value = currentExp;
            }
            if (currentExp >= maxExp)
            {
                currentExp = 0;
                maxExp += 5;
                
                expBar.maxValue = maxExp;
                expBar.value = currentExp;
                
                Debug.Log("Level Up!");
            }
        }
    }
}
