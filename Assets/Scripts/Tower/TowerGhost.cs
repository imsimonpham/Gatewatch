using UnityEngine;

public class TowerGhost : MonoBehaviour
{
   private Camera _mainCam;
   [SerializeField] private LayerMask _layerMask;
   [SerializeField] private Material _blueMat;
   [SerializeField] private Material _redMat;
   [SerializeField] 
   private GameObject[] _meshObjects;
   private bool _towerGhostIsInRange = false;
   private BuildManager _buildManager;
   

   void Start()
   {
      _meshObjects = GetChildrenWithTag("Mesh");
      _mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
      _buildManager = GameObject.FindWithTag("BuildManager").GetComponent<BuildManager>();
      if (_buildManager == null)
      {
         Debug.LogError("Build Manager is null");
      }
      TurnColorToRed();
   }
   void Update()
   {
      Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
      {
         transform.position = raycastHit.point;
         int hitLayer = raycastHit.collider.gameObject.layer;
         if (LayerMask.LayerToName(hitLayer) == "Base")
         {
            TowerBase towerBase = raycastHit.collider.gameObject.GetComponent<TowerBase>();
            float distance = Vector3.Distance(transform.position, towerBase.transform.position);
            if (distance <= 4f && towerBase.CanBuildHere())
            {
               TurnColorToBlue();
               if (Input.GetKey(KeyCode.Mouse0))
               {
                  towerBase.BuildTower(_buildManager.GetTowerToBuild());
               }
            }
            else
            {
               TurnColorToRed();
            }
         }
         else
         {
            TurnColorToRed();
         }
      }
      else
      {
         TurnColorToRed();
      }
   }

   
   void TurnColorToBlue()
   {
      foreach (GameObject meshObject in _meshObjects)
      {
         Renderer renderer = meshObject.GetComponent<Renderer>();
         if (!renderer.enabled)
         {
            renderer.enabled = true;
         }
         if (renderer != null)
         {
            renderer.material = _blueMat;
         }
      }
   }
   
   void TurnColorToRed()
   {
      foreach (GameObject meshObject in _meshObjects)
      {
         Renderer renderer = meshObject.GetComponent<Renderer>();
         if (!renderer.enabled)
         {
            renderer.enabled = true;
         }
         if (renderer != null)
         {
            renderer.material = _redMat;
         }
      }
   }

   void TurnInvisible()
   {
      foreach (GameObject meshObject in _meshObjects)
      {
         Renderer renderer = meshObject.GetComponent<Renderer>();
         if (renderer != null)
         {
            renderer.enabled = false;
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

