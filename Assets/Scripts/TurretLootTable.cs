using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretLootTable", menuName = "ScriptableObjects/TurretLootTable", order = 1)]
public class TurretLootTable : ScriptableObject
{
    public GameObject[] TurretPrefabs;
}
