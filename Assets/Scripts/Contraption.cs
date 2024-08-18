using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContraptionType { Turret, Trap }
[CreateAssetMenu(fileName ="Contraption", menuName ="GameItems")]
public class Contraption : ScriptableObject
{
    public string c_name;
    public int c_weight;
    public GameObject c_bulletPrefab;
    public int c_dmg;
    public Sprite c_sprite;
}
