using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerScriptableObject _playerScriptableObject;
    [SerializeField]
    GameController _gameController;
    [SerializeField]Rigidbody2D _rb;
    [SerializeField] Bullet _bulletPrefab;

    [SerializeField] float _accelerateSpeed = 1f;
    [SerializeField] float _turnDirection   = 0f;
    [SerializeField] float _rotationSpeed = 0.1f;

    [SerializeField] bool _accelerate;

    Renderer[] _renderers;
    bool _isWarpingX, _isWarpingY;

    private void OnEnable()
    {
        

        Invoke("TurnColliderOn",3);
    }

    private void OnDisable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();

        _rb = GetComponent<Rigidbody2D>();
        _renderers = GetComponents<Renderer>();

        _accelerateSpeed = _playerScriptableObject._acclerationSpeed;
        _rotationSpeed = _playerScriptableObject._roatationSpeed;
    }

  

    private void Update()
    {
        _accelerate = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1f;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1f;
        } else {
            _turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)& _gameController.get_gameStart) {
           StartCoroutine("Shoot");
        }

       
    }

    IEnumerator Shoot() {

        if (_gameController != null)
        {

            for (int i = 0; i < _gameController.get_amountBullet; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
                bullet.Project(Quaternion.Euler(0,0, _gameController.get_angleBullet)* transform.up);
                yield return new WaitForSeconds(_gameController.get_bulletDelay);
            }
        }
       
    }

    private void FixedUpdate()
    {
        if (_accelerate) {
            _rb.AddForce(transform.up * _accelerateSpeed);
        }

        if (_turnDirection != 0f) {
            _rb.AddTorque(_rotationSpeed * _turnDirection);
        }

        ScreenWarp();
    }
    void ScreenWarp() {
        bool _isvisible = CheckRenders();
        if (_isvisible) {
            _isWarpingX = false;
            _isWarpingY = false;
            return;
        }
        if (_isWarpingY && _isWarpingX) {
            return;
        }

        Vector3 temp = transform.position;
        if (temp.x > 1 || temp.x < 0) {
            temp.x = -temp.x;
            _isWarpingX = true;
        }
        if (temp.y > 1 || temp.y < 0)
        {
            temp.y = -temp.y;
            _isWarpingY = true;
        }
        transform.position = temp;
    }
    bool CheckRenders() {
        foreach (Renderer a in _renderers) {
            if (a.isVisible) {
                return true;
            }
        }
        return false;
    }
   

    private void TurnColliderOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (_gameController.get_barrierON)
            {
                _gameController.SetBarrierActive(false);
            }
            else {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = 0f;
                gameObject.SetActive(false);

                _gameController.PlayerDeath(transform.position);
            }
           
        }
    }



}
