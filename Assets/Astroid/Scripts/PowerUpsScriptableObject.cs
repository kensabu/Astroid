using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PowerUpsScriptableObject", order = 1)]
public class PowerUpsScriptableObject : ScriptableObject
{
    [SerializeField]
    public Powerups[] poerups;
    public bool _powerUPRandom=true;
}

[Serializable]
public class Powerups {
    [Header("spawn in space Config")]
    public string name;
    public GameObject prefab;
    public Sprite sprite;

    [Header("spawn power object  Config")]
    public float movementSpeed = 30f;
    public float maxLifetime = 40f;
    public float amountOfBullet = 2, angletOfBullet = 20, delaytOfBullet = 0.01f;

    [Header("Corresponding bullet congif spawn from player")]
    public Sprite  _bulletSprite;
    public float _bulletTimmer;

}


