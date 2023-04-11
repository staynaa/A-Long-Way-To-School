using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMove : MonoBehaviour
{
    public float spd; //speed of object

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * spd * Vector3.right);

    }
}
