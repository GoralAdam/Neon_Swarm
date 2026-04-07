using UnityEngine;

public class ArcadeBullet : MonoBehaviour
{
    public float speed = 10f;
    public float destroyYLimit = 10f;
    
    void Update()
    {
        transform.Translate(0,speed*Time.deltaTime,0);
        
        if(transform.position.y>destroyYLimit || transform.position.y < -destroyYLimit)
        {
            Destroy(gameObject);
        }
        if(transform.position.x>destroyYLimit || transform.position.x < -destroyYLimit)
        {
            Destroy(gameObject);
        }
    }
}
