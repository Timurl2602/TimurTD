using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int CurrentMoney;
    public int CurrentHealth;

    public int DefaultMoney;
    public int DefaultHealth;
    
    
}
