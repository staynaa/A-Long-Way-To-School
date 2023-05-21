using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartMove : MonoBehaviour
{
    // [SerializeField] float movement;
    // [SerializeField] Rigidbody2D rigid;
    // [SerializeField] int speed;
    [SerializeField] List<GameObject> btn=new List<GameObject>();
    [SerializeField] GameObject imgObj;
    // Start is called before the first frame update
    void Start()
    {
        // if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        // speed = 3;
    }
    // Update is called once per frame
    void Update()
    {
        // movement = Input.GetAxis("Horizontal");
        transform.position += Time.deltaTime*Vector3.right * 3.0f;
    }

    //called potentially multiple times per frame
    //used for physics & movement
    // void FixedUpdate()
    // { 
    //     rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
    // }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "notif"){ 
            Time.timeScale = 0;
            imgObj.SetActive(false);
            btn[0].SetActive(false);
            btn[1].SetActive(false);
            btn[2].SetActive(false);
            btn[3].SetActive(false);
        }
    }
}
