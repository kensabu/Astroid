 
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    PlayerScriptableObject _playerScriptableObject;
    [SerializeField]
    Bullet _bullet;
    [SerializeField]
    Player _player;
    [SerializeField]
    Asteroid _asteroidPrefab,  temp_austeroid;
    [SerializeField]
    Timer _timer;
    [SerializeField]
    Vector3 _asteroidDefaultPos;
    [SerializeField]
    ParticleSystem _explosionEffect;
    [SerializeField]
    int _score, _life;
    [SerializeField]
    TMPro.TextMeshProUGUI _scoreText, _lifeText;
    [SerializeField]
    GameObject _gameOverUI,_spawner,_playerbarrier ;
    [SerializeField]
    float _respawnDelay= 3;
    [SerializeField]
    float _amountBullet = 3, _angleBullet = 0,_bulletDelay=0;
    [SerializeField]
    bool _barrierON;
   

    [SerializeField]
    Sprite[] _bulletSprite;

    bool _timeLinetrigger,_gameStart;

    public float get_amountBullet { get { return _amountBullet; }set { _amountBullet = value; } }
    public float get_angleBullet { get { return _angleBullet; } set { _angleBullet = value; } }
    public float get_bulletDelay { get { return _bulletDelay; } set { _bulletDelay = value; } }
    public bool get_barrierON { get { return _barrierON; } set { _barrierON = value; } }
    public bool get_gameStart { get { return _gameStart; } set { _gameStart = value; } }
 

    private void Start()
    {
        StartGame();
    }


    public void StartGame()
    {
        _player.gameObject.SetActive(true);
        _spawner.SetActive(false);
        _gameOverUI.SetActive(false);
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        if (temp_austeroid != null) {
            Destroy(temp_austeroid.gameObject);
        }

        foreach (Asteroid a in asteroids)
        {
            Destroy(a.gameObject);
        }
        SetScore(0);
        SetLives(_playerScriptableObject._amountHealth);
        SetBarrierActive(false);
        BaseWeapon();
        _player.GetComponent<PlayableDirector>().Play();
         temp_austeroid = Instantiate(_asteroidPrefab, _asteroidDefaultPos, Quaternion.identity);
    }


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        _explosionEffect.transform.position = asteroid.transform.position;
        _explosionEffect.Play();

        if (asteroid.size < 0.7f)
        {
            SetScore(_score + 50);
        }
        
    }

    public void PlayerDeath(Vector3 _currentPos)
    {
        _explosionEffect.transform.position = _currentPos;
        _explosionEffect.Play();

        SetLives(_life - 1);

        if (_life <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke("Respawn",  _respawnDelay);
        }
    }

    public void Respawn()
    {
        _player.transform.position = Vector3.zero;
        _player.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        _gameOverUI.SetActive(true);
        _gameStart = false;
        _spawner.SetActive(false);

        DestroyAllObjectinSpace();
    }

    private static void DestroyAllObjectinSpace()
    {
        Asteroid[] _tempastroid = FindObjectsOfType<Asteroid>();
        foreach (Asteroid a in _tempastroid)
        {
            Destroy(a.gameObject);
        }
        Barrier[] _tempbarrier = FindObjectsOfType<Barrier>();
        foreach (Barrier a in _tempbarrier)
        {
            Destroy(a.gameObject);
        }
        Blaster[] _tempBlaster = FindObjectsOfType<Blaster>();
        foreach (Blaster a in _tempBlaster)
        {
            Destroy(a.gameObject);
        }
        Bullet[] _tempBullet = FindObjectsOfType<Bullet>();
        foreach (Bullet a in _tempBullet)
        {
            Destroy(a.gameObject);
        }
    }

    private void SetScore(int score)
    {
        this._score = score;
        _scoreText.text = score.ToString();


        if (_score % (8000/ _playerScriptableObject._leveldifficulty) ==0) { IncreaseLevel(); }
    }

    private void SetLives(int life)
    {
        this._life = life;
        _lifeText.text = life.ToString();
    }
    private void Update()
    {
        if (Input.anyKeyDown && _timeLinetrigger)
        {

            if (temp_austeroid != null)
            {
                Quaternion rotation = Quaternion.AngleAxis(-15, Vector3.forward);
                temp_austeroid.GetComponent<Rigidbody2D>().AddForce(rotation * -Random.insideUnitCircle.normalized * 50f);
                _spawner.SetActive(true);
                _gameStart = true;
                _timeLinetrigger = !_timeLinetrigger;
            }

        }
        else if (Input.anyKeyDown && !_gameStart) {
            StartGame();
        }
    }

    public void TriggerTimeLineEnd() {
        _timeLinetrigger = true;
    }

    public void BaseWeapon() {
        _amountBullet = 3;
        _angleBullet = 0;
        _bulletDelay = 0.03f;
        _bullet.spriteRenderer.sprite = _bulletSprite[0];
    }

    public void SetNewWeapon(float amount,float angle,float delay,float timer,Sprite sprite)
    {
        _amountBullet = amount;
        _angleBullet = angle;
        _bulletDelay = delay;
        _bullet.spriteRenderer.sprite = sprite;
        _timer.StartTimer(timer);

    }

    internal void StopedTimer()
    {
        BaseWeapon();
    }

    public void SetBarrierActive(bool value) {
        _barrierON = value;
        _playerbarrier.SetActive(value);
    }


    public void IncreaseLevel() {
        if (_spawner.GetComponent<Spawner>().get_amountPerSpawnAstroid <= 5) {
            _spawner.GetComponent<Spawner>().get_amountPerSpawnAstroid += 1;
        }
        if (_spawner.GetComponent<Spawner>().get_amountPerSpawnIteams <= 2) {
            _spawner.GetComponent<Spawner>().get_amountPerSpawnIteams += 1;
        }
       
    }
}

 
