using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    CapsuleCollider2D Weapon;

    void Start()
    {
        Weapon = GetComponent<CapsuleCollider2D>();
        Weapon.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Weapon.enabled = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision, int enemyLayer)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            float attackRange = 0;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                object damage = null;
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }


}