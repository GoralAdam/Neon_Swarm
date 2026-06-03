using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    private Vector3 targetDirection;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // Oblicz kierunek
            targetDirection = (player.transform.position - transform.position).normalized;

            //Oblicz kąt obrotu
            // Atan2 zwraca radianach, więc zamieniamy na stopnie (Rad2Deg)
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // Zastosuj obrót
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        }
        else
        {
            targetDirection = Vector3.down;
        }
        
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Poruszamy pocisk w wyliczonym wcześniej kierunku
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // logika dla obrażeń dla gracza
            Destroy(gameObject);
        }
    }
}