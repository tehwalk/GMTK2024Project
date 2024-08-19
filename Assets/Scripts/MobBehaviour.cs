using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public enum MobState { Moving, Hitting, Avoiding }
public class MobBehaviour : MonoBehaviour
{
    MobState state;
    GameObject wayPointsList;
    NavMeshAgent agent;
    [Header("General Settings")]
    [SerializeField] Mob m_Mob;
    [SerializeField] float speed;
    [SerializeField] float minDist;
    [SerializeField] float minMobDist;
    [SerializeField] float minAttackDist;
    [SerializeField] float attackRate;
    [SerializeField] LayerMask mask;
    [Header("UI Settings")]
    [SerializeField] Slider healthSlider;
    Transform[] targetList;
    Transform currentTarget;
    Transform avoidThing;
    Vector3 avoidanceTarget;
    int targetIndex = 0;
    float attackTimer = 0;
    int health;
    int dmg;
    int exp;
    // Start is called before the first frame update
    void Start()
    {
        health = m_Mob.health;
        healthSlider.minValue = 0;
        healthSlider.maxValue = m_Mob.health;
        healthSlider.value = health;
        dmg = m_Mob.dmg;
        exp = m_Mob.expGain;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        wayPointsList = GameObject.FindGameObjectWithTag(Tags.T_Path);
        targetList = wayPointsList.GetComponentsInChildren<Transform>();
        nextTarget();
        state = MobState.Moving;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case MobState.Moving:
                if (agent.remainingDistance<=minDist) nextTarget();
               // transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
                
                break;
            case MobState.Hitting:
                GiveDamage();
                break;
          
        }
    }

    void nextTarget()
    {
        targetIndex++;
        if (targetIndex >= targetList.Length)
        {
            state = MobState.Hitting;
        }
        else agent.SetDestination(targetList[targetIndex].position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.T_Bullet)) 
        {
            Debug.Log("ouch");
            LoseHealth(collision.gameObject.GetComponentInParent<ContraptionBehaviour>().Damage);
        } 
    }

    public void LoseHealth(int dmg) 
    { 
        health -= dmg;
        healthSlider.value = health;
        if (health <= 0)
        {
            LevelManager.Instance.GainExp(exp);
            Destroy(gameObject); 
        }
    }

    void GiveDamage() 
    {
        //Debug.Log("arrived");
        if (attackTimer >= attackRate)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, minAttackDist);
            foreach (var collider in colliders) 
            {
                if (collider.gameObject.CompareTag(Tags.T_Cake))
                {
                    Debug.Log("hir");
                    GameManager.Instance.LoseCakeHealth(dmg);
                }
                attackTimer = 0;
            }
        }
        else attackTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minAttackDist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minMobDist);
    }

  
}
