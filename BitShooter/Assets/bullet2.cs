using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public Vector2 moveDirection;
    public float speed = 15f;
    public float deathTime = 2.5f;
    public int damage = 1;

    private void Start()
    {
        Destroy(gameObject, deathTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerMovement>() != null)
        {
            collider.GetComponent<PlayerMovement>().health -= damage;
        }
        if (collider.GetComponent<PlayerMovement2>() == null)
        {
            Destroy(gameObject);
        }
    }
}
