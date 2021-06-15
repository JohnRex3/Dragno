using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerRunSpeed = 5f;
    [SerializeField] float playerJumpSpeed = 10f;
    [SerializeField] int Damage = 1;
    [SerializeField] public int armor = 1;
    [SerializeField] int health = 5;

    public bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myFeet;
    CircleCollider2D Weapon;

    Vector3 characterScale;
    float characterScaleX;

    public void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        Weapon = GetComponent<CircleCollider2D>();
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    public void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        Attack();
        TakeDamage();
        TurnAround();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * playerRunSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Foreground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, playerJumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackWithWeapon();
        }

    }

    public void TakeDamage()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {

            if (armor >= Damage)
            {
                Damage = 1;
            }
            health -= Damage;
            if(health >= 0)
            {
                Die();
            }
            
            
        }
    }

    public void Die()
    {
        isAlive = false;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    }

    private void TurnAround()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;
    }


    private void AttackWithWeapon()
    {
        /*Weapon.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        Weapon.enabled = false;
        */
        float distance = 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), distance);
        if (hit.collider != null)
        {
            //do stuff
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Armor")
        {
            armor++;
            Destroy(collision.gameObject);
        }

    }


}

