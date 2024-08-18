using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    [SerializeField] float scaleFactor;
    float hDirection;
    Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        hDirection = Input.GetAxisRaw("Horizontal");
        //Debug.Log(hDirection);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.localScale.sqrMagnitude > (originalScale * 2).sqrMagnitude) transform.localScale = originalScale * 2;
            else if (transform.localScale.sqrMagnitude < (originalScale * 0.5f).sqrMagnitude) transform.localScale = originalScale * 0.5f;
            //Debug.Log("lol");
            if (hDirection > 0) transform.localScale += transform.localScale * scaleFactor;
            else if (hDirection < 0) transform.localScale -= transform.localScale * scaleFactor;


        }
    }

   
}
