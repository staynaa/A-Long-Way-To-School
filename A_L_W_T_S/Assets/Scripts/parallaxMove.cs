using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxMove : MonoBehaviour
{//effect on environment background
    private float startPos;
    [SerializeField] private GameObject cam; //main camera
    [SerializeField] private float parallaxEffect; //the effect, set in editor for each layer
    // Start is called before the first frame update
    void Start()
    {
        startPos=transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        float dist=(cam.transform.position.x*parallaxEffect);
        transform.position=new Vector3(startPos+dist,transform.position.y,transform.position.z);
    }
}
