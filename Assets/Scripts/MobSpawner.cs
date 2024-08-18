using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab;
    [SerializeField] float m_SpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.1f, m_SpawnTime);
    }

    void Spawn()
    {
        Instantiate(m_Prefab, transform.position, Quaternion.identity);
    }

}
