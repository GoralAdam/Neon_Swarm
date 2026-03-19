using UnityEngine;

public class ArcadeBullet : MonoBehaviour
{
    public float speed = 10f;
    public float destroyYLimit = 6f;
    
    void Update()
    {
        transform.Translate(0,speed*Time.deltaTime,0);
        
        if(transform.position.y>destroyYLimit)
        {
            Destroy(gameObject);
        }
    }
}
