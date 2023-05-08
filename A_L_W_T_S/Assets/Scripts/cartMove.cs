using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartMove : MonoBehaviour
{
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        speed = 3;
    }
    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }

    //called potentially multiple times per frame
    //used for physics & movement
    void FixedUpdate()
    { 
        rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
    }
}
