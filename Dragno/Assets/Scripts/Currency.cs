using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Currency : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] int Value = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            GameObject.Find("GameSession").GetComponent<GameSession>().AddToScore(Value);
            Destroy(gameObject);
        }
    }
}
