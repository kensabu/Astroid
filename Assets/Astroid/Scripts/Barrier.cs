using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rb;
    [SerializeField] GameController gameController;
    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float maxLifetime = 30f;


  


    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

        
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

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


            gameController.SetBarrierActive(true);


            Destroy(gameObject);
        }
    }
}
