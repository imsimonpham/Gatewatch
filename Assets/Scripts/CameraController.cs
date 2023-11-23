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
   
   //mouse's origin position is at the bottom of the screen 
   void Update()
   {
      Vector3 camPos = transform.position;
      
      //camera movement
      if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - _panBorderThickness && Input.GetKey(KeyCode.Mouse1)))
      {
         camPos.z += _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("s") || (Input.mousePosition.y <=  _panBorderThickness && Input.GetKey(KeyCode.Mouse1)))
      {
         camPos.z -= _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("a") || (Input.mousePosition.x <= _panBorderThickness && Input.GetKey(KeyCode.Mouse1)))
      {
         camPos.x -= _panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - _panBorderThickness && Input.GetKey(KeyCode.Mouse1)))
      {
         camPos.x += _panSpeed * Time.deltaTime;
      }
      
      //zoom in and out
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      camPos.y -= scroll * _scrollSpeed * Time.deltaTime;
      
      //limit camera's movement range and zoom
      camPos.x = Mathf.Clamp(camPos.x, _panLimitX.min, _panLimitX.max);
      camPos.z = Mathf.Clamp(camPos.z, _panLimitZ.min, _panLimitZ.max);
      camPos.y = Mathf.Clamp(camPos.y, _zoomLimit.min, _zoomLimit.max);
      
      //update camera's position
      transform.position = camPos;
   }
}
