using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobState { Moving, Hitting, Avoiding }
public class MobBehaviour : MonoBehaviour
{
    MobState state;
    GameObject wayPointsList;
    [SerializeField] float speed;
    [SerializeField] float minDist;
    [SerializeField] float minMobDist;
    [SerializeField] LayerMask mask;
    Transform[] targetList;
    Transform currentTarget;
    Transform avoidThing;
    int targetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
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
                if (Vector2.Distance(transform.position, currentTarget.position) < minDist) nextTarget();
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
                Collider2D collider = Physics2D.OverlapCircle(transform.position, minMobDist, mask);
                if (collider != null && collider !=this.GetComponent<Collider2D>())
                {
                    avoidThing = collider.gameObject.transform;
                    state = MobState.Avoiding;
                }
                break;
            case MobState.Hitting:
                break;
            case MobState.Avoiding:
                if (Vector2.Distance(transform.position, avoidThing.position) > minMobDist) state = MobState.Moving;
                transform.position = Vector2.MoveTowards(transform.position, avoidThing.position, -1* Time.deltaTime * speed);
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
        else currentTarget = targetList[targetIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.T_Bullet)) 
        {
            Debug.Log("ouch");
        } 
    }
}
