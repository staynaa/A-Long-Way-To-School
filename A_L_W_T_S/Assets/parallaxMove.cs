using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxMove : MonoBehaviour
{
    [SerializeField] private float length, startPos;
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        cam= GameObject.Find("Main Camera");
        startPos= transform.position.x;
        length= GetComponent<SpriteRenderer>().bounds.size.x;
        //length=19.63427F;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp= (cam.transform.position.x * (1-parallaxEffect));
        float distance= (cam.transform.position.x * parallaxEffect);
        transform.position= new Vector3(startPos+distance,transform.position.y,transform.position.z);
        // if(temp > startPos + length) startPos+=length;
        // else if (temp < startPos -length) startPos -= length;
    }
}
/*
private float startPos;
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        cam= GameObject.Find("Main Camera");
        startPos= transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float distance= (cam.transform.position.x * parallaxEffect);
        transform.position= new Vector3(startPos+distance,transform.position.y,transform.position.z);
    }
*/