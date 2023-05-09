using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] Transform checkpoint;
    [SerializeField] TextMeshProUGUI distText;
    [SerializeField] float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist =(checkpoint.transform.position-transform.position).magnitude;
        dist*=3.28f;
        distText.SetText("You are "+ dist.ToString("F1") + " feet away from the bus stop");
    }
}
