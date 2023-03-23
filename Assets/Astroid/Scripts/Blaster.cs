using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField]  Rigidbody2D rb;


    public SpriteRenderer spriteRenderer;
    public Sprite _bulletSprite;
    public float movementSpeed = 30f;
    public float maxLifetime = 40f;
    public float amountOfBullet=2, angletOfBullet=20, delaytOfBullet=0.01f;


    public float size = 1f, minSize = 0.35f, maxSize = 1.65f,_timer;


    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

       
    }

    private void Start()
    {

        
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);


        transform.localScale = Vector3.one * size;
        rb.mass = size;


        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {

        rb.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            gameController.SetNewWeapon(amountOfBullet, angletOfBullet,delaytOfBullet,_timer, _bulletSprite);

             
            Destroy(gameObject);
        }
    }

   
}


