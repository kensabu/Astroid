using UnityEngine;

 
public class Asteroid : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] GameController _gameController;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

      
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

       
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
       
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
           
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

         _gameController.AsteroidDestroyed(this);

            Destroy(gameObject);
        }
    }

    private void CreateSplit()
    {
       
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

         Asteroid half = Instantiate(this, position, transform.rotation);
        half.GetComponent<BoxCollider2D>().enabled = true;
        half.GetComponent<Asteroid>().enabled = true;
        half.size = size * 0.5f;

         half.SetTrajectory(Random.insideUnitCircle.normalized);

      
    }

}
