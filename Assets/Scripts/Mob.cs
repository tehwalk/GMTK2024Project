using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "GameItems/Mob")]
public class Mob : ScriptableObject
{
    public string mob_Name;
    public GameObject GFX;
    public int health;
    public int dmg;
    public int expGain;
}
