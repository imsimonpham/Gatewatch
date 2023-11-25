using UnityEngine;

[System.Serializable]
public class MinMaxRange
{
   public float min;
   public float max;
}

public class CameraController : MonoBehaviour
{
   [SerializeField] private float _panSpeed;
   [SerializeField] private float _panBorderThickness;
   [SerializeField] private float _scrollSpeed; 
   [SerializeField] private MinMaxRange _panLimitX;
   [SerializeField] private MinMaxRange _panLimitZ;
   [SerializeField] private MinMaxRange _zoomLimit;
   [SerializeField] private MinMaxRange _rotationLimit;
   
   
   //mouse's origin position is at the bottom of the screen 
   void Update()
   {
      Vector3 camPos = transform.position; // transform.position is Vector3
      Vector3 camRot = transform.rotation.eulerAngles; //convert transform.rotation (Quaternion) to Vector3
      
      //camera movement
      if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - _panBorderThickness && Input.GetKey(KeyCode.Mouse2)))
      {
         camPos.z += _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("s") || (Input.mousePosition.y <=  _panBorderThickness && Input.GetKey(KeyCode.Mouse2)))
      {
         camPos.z -= _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("a") || (Input.mousePosition.x <= _panBorderThickness && Input.GetKey(KeyCode.Mouse2)))
      {
         camPos.x -= _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - _panBorderThickness && Input.GetKey(KeyCode.Mouse2)))
      {
         camPos.x += _panSpeed * Time.deltaTime;
      }
      
      //zoom in and out
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      camPos.y -= scroll * _scrollSpeed * Time.deltaTime;
      camRot.x -= scroll * _scrollSpeed * Time.deltaTime;
      
      //limit camera's movement range and zoom and rotation
      camPos.x = Mathf.Clamp(camPos.x, _panLimitX.min, _panLimitX.max);
      camPos.z = Mathf.Clamp(camPos.z, _panLimitZ.min, _panLimitZ.max);
      camPos.y = Mathf.Clamp(camPos.y, _zoomLimit.min, _zoomLimit.max);
      camRot.x = Mathf.Clamp(camRot.x, _rotationLimit.min, _rotationLimit.max);
      
      //update camera's position and rotation
      transform.position = camPos;
      transform.rotation = Quaternion.Euler(camRot);
   }
}
