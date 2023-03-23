using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerScriptableObject", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    public float _acclerationSpeed = 1.8f;
    public float _roatationSpeed = 1.8f;
    public int _amountHealth = 3;
    [Range(1, 2)]
    public float _leveldifficulty=1;
 
}
