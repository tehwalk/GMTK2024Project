using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContraptionState { PlacePending, PlaceFound, Placed }
public class ContraptionBehaviour : MonoBehaviour
{
    ContraptionState state;
    public ContraptionState State { get { return state; } }
    [SerializeField] Contraption c_Contraption;
    public Contraption Contraption { get { return c_Contraption; } set { c_Contraption = value; } }
    ContraptionManager contraptionManager;
    [SerializeField] float c_Interval;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float radius;
    [SerializeField] Transform shooter;
    [SerializeField] Transform rangeCenter;
    [SerializeField] Transform standPoint;
    [SerializeField] float bulletForce = 3;
    GameObject bulletPrefab;
    GameObject hole = null;
    int damage;
    public int Damage { get { return damage; } }
    int weight;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        state = ContraptionState.PlacePending;
        Initialize();
        //InvokeRepeating("Shoot", 0.1f, c_Interval);
    }

    private void Initialize()
    {
        bulletPrefab = c_Contraption.c_bulletPrefab;
        damage = c_Contraption.c_dmg;
        weight = c_Contraption.c_weight;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        switch (state) 
        {
            case ContraptionState.PlacePending:
                if (Input.GetMouseButtonDown(1))
                {
                    ContraptionManager.Instance.RemoveContraption(this);
                    Destroy(gameObject);
                }
                transform.position = mousePos;
                break;
            case ContraptionState.PlaceFound:
                
                if (Input.GetMouseButtonDown(0))
                {
                    transform.position = hole.transform.position + new Vector3(0,Mathf.Abs(standPoint.localPosition.y),0);
                    ContraptionManager.Instance.SetContraption();
                    state = ContraptionState.Placed; 
                }
                else if (Input.GetMouseButtonDown(1)) 
                {
                    ContraptionManager.Instance.RemoveContraption(this);
                    Destroy(gameObject);
                }
                else transform.position = mousePos;
                break;
            case ContraptionState.Placed:
                Shoot();
                break;
        }
    }

    void Shoot()
    {
        
        if (timer >= c_Interval)
        {
            //find the enemy closest (inside range)
            //rotate the head towards it
            //shoot at that direction
            // if (isHeld == true) return;
            Collider2D enemy = Physics2D.OverlapCircle(rangeCenter.position, radius, enemyMask);
            if (enemy != null)
            {
                // shooter.LookAt(enemy.gameObject.transform, Vector3.forward);
                var dist = (shooter.position - enemy.transform.position).normalized;
                float rot_z = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
                shooter.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                var bullet = Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
                bullet.transform.SetParent(transform);
                bullet.GetComponent<Rigidbody2D>().AddForce(-shooter.transform.up * bulletForce, ForceMode2D.Impulse);
                Destroy(bullet, 0.8f);
            }
            timer = 0;
        }
        else timer += Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rangeCenter.position, radius);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.T_Hole)) 
        {
            hole = collision.gameObject;
            state = ContraptionState.PlaceFound;
           
            //Debug.Log("can be placed");
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(Tags.T_Hole) && state==ContraptionState.PlaceFound)
        {
            state = ContraptionState.PlacePending;
           
            //Debug.Log("can't be placed");
        }
    }
}
