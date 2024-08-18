using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraptionBehaviour : MonoBehaviour
{
    [SerializeField] Contraption c_Contraption;
    [SerializeField] float c_Interval;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float radius;
    [SerializeField] Transform shooter;
    [SerializeField] Transform rangeCenter;
    [SerializeField] float bulletForce = 3;
    GameObject bulletPrefab;
    int damage;
    int weight;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        InvokeRepeating("Shoot", 0.1f, c_Interval);
    }

    private void Initialize()
    {
        bulletPrefab = c_Contraption.c_bulletPrefab;
        damage = c_Contraption.c_dmg;
        weight = c_Contraption.c_weight;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot() 
    {
        //find the enemy closest (inside range)
        //rotate the head towards it
        //shoot at that direction
        Collider2D enemy = Physics2D.OverlapCircle(rangeCenter.position, radius, enemyMask);
        if (enemy != null) 
        {
            // shooter.LookAt(enemy.gameObject.transform, Vector3.forward);
            var dist = (shooter.position - enemy.transform.position).normalized;
            float rot_z = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            var bullet = Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(-shooter.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rangeCenter.position, radius);

    }
}
