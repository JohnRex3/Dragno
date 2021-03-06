using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyMoveSpeed = 2f;
    [SerializeField] public float enemyHealth = 1;


    Rigidbody2D myRigidBody;
    CircleCollider2D myCircleCollider2D;

    Vector3 enemyScale;
    float enemyScaleX;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        enemyScale = transform.localScale;
        enemyScaleX = enemyScale.x;
    }

    void Update()
    {
        if (isFacingRight())
        {
            myRigidBody.velocity = new Vector2(-enemyMoveSpeed, 0);
        }
        else
        {
            myRigidBody.velocity = new Vector2(enemyMoveSpeed, 0);
        }

    }

    bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerExit2D(Collider2D collision)
    
    {
        if (isFacingRight())
        {
            enemyScale.x = -enemyScaleX;
        }
        else
        {
            enemyScale.x = enemyScaleX;
        }
        transform.localScale = enemyScale;
    }

    private void Die()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player", "Place Holder For Player Weapon Mask"))) // add mask for player weapons later
        {
            Destroy(gameObject);
        }
    }
}
