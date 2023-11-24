using UnityEngine;

public class TowerBase : MonoBehaviour
{
   private Color _hoverColor;
   private Color _errorColor;
   private Color _originalColor;
   private Renderer _renderer;
   [SerializeField] private GameObject _towerPlacementArea;
   [SerializeField] private GameObject _existingTower;
   private TowerBlueprint _towerBlueprint;
   private BuildManager _buildManager;

   void Start()
   {
      _renderer = GetComponent<Renderer>();
      _originalColor = _renderer.material.color;
      _hoverColor = Color.green;
      _errorColor = Color.red;
      _buildManager = GameObject.FindWithTag("BuildManager").GetComponent<BuildManager>();
      if (_buildManager == null)
      {
         Debug.LogError("Build Manager is null");
      }
   }

   void Update()
   {
      if (Input.GetKey(KeyCode.Mouse1))
      {
         _buildManager.ClearTowerToBuld();
      }
   }

   void OnMouseDown()
   {
      if (!_buildManager.HaveATowerToBuild())
      {
         Debug.Log("Have no tower to build");
         return;
      }
      
      if (_existingTower != null)
      {
         Debug.Log("There is an existing tower here");
         return;
      }
      BuildTower(_buildManager.GetTowerToBuild());
   }

   void BuildTower(TowerBlueprint towerBlueprint)
   {
      GameObject tower = Instantiate(towerBlueprint.GetPrefabLv1(), _towerPlacementArea.transform.position, Quaternion.identity);
      _existingTower = tower;
      _buildManager.ClearTowerToBuld();
   }

   void OnMouseEnter()
   {
      if (_buildManager.HaveATowerToBuild() && _existingTower != null)
      {
         _renderer.material.color = _errorColor;
      }
      else if(_buildManager.HaveATowerToBuild() && _existingTower == null)
      {
         _renderer.material.color = _hoverColor;
      }
   }

   void OnMouseExit()
   {
      _renderer.material.color = _originalColor;
   }
   
}
