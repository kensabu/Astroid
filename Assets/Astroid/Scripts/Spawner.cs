using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] PowerUpsScriptableObject _powerUpsScriptableObject;
    [SerializeField] Asteroid _asteroidPrefab;
    [SerializeField]
    Blaster blaster;
    [SerializeField] Barrier _barrier;
    [SerializeField] float _spawnDistance = 12f;
    [SerializeField] float _spawnRate = 1f;
    [SerializeField] int _amountPerSpawnAstroid = 1, _amountPerSpawnIteams=1;
    [SerializeField] float _otherSpawnRate = 1f;
    [Range(0f, 45f)]
    float _anglevarient = 15f;


    public int get_amountPerSpawnAstroid { get { return _amountPerSpawnAstroid; } set { _amountPerSpawnAstroid = value; }}
    public int get_amountPerSpawnIteams { get { return _amountPerSpawnIteams; } set { _amountPerSpawnIteams = value; } }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
        StartCoroutine(SpawnOther());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(_spawnRate);
            SpawnAstroid();
          
        }
    }
    IEnumerator SpawnOther()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(_otherSpawnRate);
            SpawnBullet_Barrier();
        }
    }
    public void SpawnAstroid()
    {
        for (int i = 0; i < _amountPerSpawnAstroid; i++)
        {

            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * _spawnDistance;

            spawnPoint += transform.position;


            float variance = Random.Range(-_anglevarient, _anglevarient);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);


            Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);


            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }
    }


    public void SpawnBullet_Barrier()
    {
        for (int i = 0; i < _amountPerSpawnIteams; i++)
        {

            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * _spawnDistance;

            spawnPoint += transform.position;


            float variance = Random.Range(-_anglevarient, _anglevarient);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            int temp = Random.Range(0, 2);
            Debug.Log(temp);
            switch (temp) {
                case 0:
                    if (_powerUpsScriptableObject._powerUPRandom)
                    {
                        int temp_randomPowerUps = Random.Range(0, _powerUpsScriptableObject.poerups.Length);

                        GameObject bla = Instantiate(_powerUpsScriptableObject.poerups[temp_randomPowerUps].prefab, spawnPoint, rotation);
                        bla.gameObject.name = _powerUpsScriptableObject.poerups[temp_randomPowerUps].name;
                        bla.GetComponent<Blaster>().spriteRenderer.sprite = _powerUpsScriptableObject.poerups[temp_randomPowerUps].sprite;
                        bla.GetComponent<Blaster>().movementSpeed = _powerUpsScriptableObject.poerups[temp_randomPowerUps].movementSpeed;
                        bla.GetComponent<Blaster>().maxLifetime = _powerUpsScriptableObject.poerups[temp_randomPowerUps].maxLifetime;
                        bla.GetComponent<Blaster>().amountOfBullet = _powerUpsScriptableObject.poerups[temp_randomPowerUps].amountOfBullet;
                        bla.GetComponent<Blaster>().angletOfBullet = _powerUpsScriptableObject.poerups[temp_randomPowerUps].angletOfBullet;
                        bla.GetComponent<Blaster>().delaytOfBullet = _powerUpsScriptableObject.poerups[temp_randomPowerUps].delaytOfBullet;


                        bla.GetComponent<Blaster>()._bulletSprite = _powerUpsScriptableObject.poerups[temp_randomPowerUps]._bulletSprite;
                        bla.GetComponent<Blaster>()._timer = _powerUpsScriptableObject.poerups[temp_randomPowerUps]._bulletTimmer;

                        Vector2 trajectory = rotation * -spawnDirection;
                        bla.GetComponent<Blaster>().SetTrajectory(trajectory);
                    }
                    else {
                        for (int j = 0; j < _powerUpsScriptableObject.poerups.Length; j++) {


                            GameObject bla = Instantiate(_powerUpsScriptableObject.poerups[j].prefab, spawnPoint, rotation);
                            bla.gameObject.name = _powerUpsScriptableObject.poerups[j].name;
                            bla.GetComponent<Blaster>().spriteRenderer.sprite = _powerUpsScriptableObject.poerups[j].sprite;
                            bla.GetComponent<Blaster>().movementSpeed = _powerUpsScriptableObject.poerups[j].movementSpeed;
                            bla.GetComponent<Blaster>().maxLifetime = _powerUpsScriptableObject.poerups[j].maxLifetime;
                            bla.GetComponent<Blaster>().amountOfBullet = _powerUpsScriptableObject.poerups[j].amountOfBullet;
                            bla.GetComponent<Blaster>().angletOfBullet = _powerUpsScriptableObject.poerups[j].angletOfBullet;
                            bla.GetComponent<Blaster>().delaytOfBullet = _powerUpsScriptableObject.poerups[j].delaytOfBullet;

                            bla.GetComponent<Blaster>()._bulletSprite = _powerUpsScriptableObject.poerups[j]._bulletSprite;
                            bla.GetComponent<Blaster>()._timer = _powerUpsScriptableObject.poerups[j]._bulletTimmer;

                            Vector2 trajectory = rotation * -spawnDirection;
                            bla.GetComponent<Blaster>().SetTrajectory(trajectory);
                        }
                    }
                  

                    break;
                case 1:
                    Barrier barr = Instantiate(_barrier, spawnPoint, rotation);
                    barr.SetTrajectory(rotation * -spawnDirection);
                    break;
            }
           
        }
    }
}
