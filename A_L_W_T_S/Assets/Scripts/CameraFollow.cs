
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

  [Tooltip("Reference to object to be followed by camera")]
  // Reference to object to be followed
  public Transform target;

  [Tooltip("Camera offset: Keep Z at -10 ")]
  // Camera Offset
  public Vector3 offset;

  [Tooltip("Camera Follow Smoothness Range [1:(slower) - 10:(faster)] ")]
  //Camera Follow Smoothness
  [Range(1,10)]
  public float smoothFactor;

  [Tooltip("Min/Max bound values for camera")]
  // Bound values for camera 
  public Vector3 minValues, maxValues;




    
  // FixedUpdate is called once every time step is settled
  void FixedUpdate() 
  {
    Follow();
  }



  /* 
  Method Name: Follow()
  Description: Update the camera coordinates based on the object being followed coordinates 
  */    
    
  void Follow( )
  {  
  Vector3 targetPosition = target.position + offset;
  //Verify if the targetPosition is out of bounds or not
  //Limit it to the min an max values

  //Bounded position of camera 
   Vector3 boundPosition = new Vector3(
    Mathf.Clamp(targetPosition.x,minValues.x,maxValues.x),
    Mathf.Clamp(targetPosition.y,minValues.x,maxValues.y),
    Mathf.Clamp(targetPosition.z,minValues.x,maxValues.z));


  Vector3 smoothPosition = Vector3.Lerp(transform.position,targetPosition,smoothFactor * Time.fixedDeltaTime);
  transform.position = smoothPosition;
  }
   
}
