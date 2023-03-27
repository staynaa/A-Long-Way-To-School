
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

  // Reference to object to be followed
  public Transform target;

  // Camera Offset
  public Vector3 offset;

  //Camera Follow Smoothness
  [Range(1,10)]
  public float smoothFactor;




    
    // FixedUpdate is called once every time step is settled
    void FixedUpdate() 
    {
      Follow();
    }

    //Update the camera coordinates based on the object being followed coordinates 
    void Follow( )
    {  
      Vector3 targetPosition = target.position + offset;
      Vector3 smoothPosition = Vector3.Lerp(transform.position,targetPosition,smoothFactor * Time.fixedDeltaTime);
      transform.position = smoothPosition;
   }
   
}
