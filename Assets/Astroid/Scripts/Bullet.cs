using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rb;
    [SerializeField] float speed = 500f;
    [SerializeField] float maxLifetime = 5f;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        
        rb.AddForce(direction * speed);
 
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);
    }

}
