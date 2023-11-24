using UnityEngine;
using System.Collections;

public class TowerGhost : MonoBehaviour
{
   [SerializeField] private Camera _mainCam;
   [SerializeField] private LayerMask _layerMask;
   [SerializeField] private Material _blueMat;
   [SerializeField] private Material _redMat;
   private GameObject[] _meshObjects;

   void Start()
   {
      _meshObjects = GetChildrenWithTag("Mesh");
   }
   void Update()
   {
      Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
      {
         transform.position = raycastHit.point;
         int hitLayer = raycastHit.collider.gameObject.layer;
         // Check if the hit layer is the "Ground" layer
         if (LayerMask.LayerToName(hitLayer) == "Ground")
         {
            TurnColorToRed();
         } else if (LayerMask.LayerToName(hitLayer) == "Base")
         {
            TurnColorToBlue();
         }
      }
   }

   void TurnColorToBlue()
   {
      foreach (GameObject meshObject in _meshObjects)
      {
         Renderer renderer = meshObject.GetComponent<Renderer>();
         if (renderer != null)
         {
            // Replace the material with the newMaterial
            renderer.material = _blueMat;
         }
      }
   }
   
   void TurnColorToRed()
   {
      foreach (GameObject meshObject in _meshObjects)
      {
         Renderer renderer = meshObject.GetComponent<Renderer>();
         if (renderer != null)
         {
            // Replace the material with the newMaterial
            renderer.material = _redMat;
         }
      }
   }

   GameObject[] GetChildrenWithTag(string tag)
   {
      Transform[] childTransforms = transform.GetComponentsInChildren<Transform>(true);
      var meshObjects = new System.Collections.Generic.List<GameObject>();
      foreach (var childTransform in childTransforms)
      {
         if (childTransform.CompareTag(tag))
         {
            meshObjects.Add(childTransform.gameObject);
         }
      }
      return meshObjects.ToArray();
   }
}

