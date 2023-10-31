using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit Data")]
public class UnitData : ScriptableObject
{
    public int Health;
    public float Armor;
}