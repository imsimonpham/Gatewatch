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
   private PlayerStats _playerStats;
   private GamePlayUI _gamePlayUI;
   private BoxCollider _towerBaseCollider;
   private Vector3 _originalSize;
   private Vector3 _originalCenter;
   private TowerBaseUI _towerBaseUI;
   
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
      _playerStats = GameObject.FindWithTag("PlayerStats").GetComponent<PlayerStats>();
      if (_playerStats == null)
      {
         Debug.LogError("Player Stats is null");
      }
      _gamePlayUI = GameObject.FindWithTag("GamePlayUI").GetComponent<GamePlayUI>();
      if (_gamePlayUI == null)
      {
         Debug.LogError("Gameplay UI is null");
      }
      _towerBaseCollider = GetComponent<BoxCollider>();
      if (_towerBaseCollider == null)
      {
         Debug.LogError("Tower Base Box Collider is null");
      }
      else
      {
         _originalCenter = _towerBaseCollider.center;
         _originalSize = _towerBaseCollider.size;
      }
      _towerBaseUI = GameObject.FindWithTag("TowerBaseUI").GetComponent<TowerBaseUI>();
      if (_towerBaseUI == null)
      {
         Debug.LogError("Tower Base UI is null");
      }
   }

   void Update()
   {
      if (!_buildManager.HaveATowerToBuild())
      {
         RevertColliderSizeY();
      }
      if (_existingTower != null)
      {
         ResizeColliderY();
      }
   }

   /*void OnMouseDown()
   {
      Debug.Log("Base is clicked");
      if (!_buildManager.HaveATowerToBuild())
      {
         if (_existingTower != null)
         {
            //Toggle Canvas when clicking on tower base
            //_towerBaseUI.GetComponent<Canvas>().enabled = !_towerBaseUI.GetComponent<Canvas>().enabled;
            //To DO: upgrade and sell tower
            //_towerBaseUI.SetSelectedTowerBase(this);
            Debug.Log(transform.name);
            return;
         }
         Debug.Log("Have no tower to build");
         return;
      }
      
      if (_existingTower != null)
      {
         Debug.Log("There is an existing tower here");
         return;
      }
      BuildTower(_buildManager.GetTowerToBuild());
   }*/
   
   public  bool CanBuildHere()
   {
      //return false if haven't selected a tower to build
      if (!_buildManager.HaveATowerToBuild())
      {
         return false;
      }
      //return false if there is an existing tower
      if (_existingTower != null)
      {
         return false;
      }
      return true;
   }

   public void BuildTower(TowerBlueprint towerBlueprint)
   {
      GameObject tower = Instantiate(towerBlueprint.GetPrefabLv1(), _towerPlacementArea.transform.position, Quaternion.identity);
      _playerStats.SpendEnergy(towerBlueprint.GetBuildCost());
      _existingTower = tower;
      _buildManager.ClearTowerToBuld();
   }

   void OnMouseEnter()
   {
      /*if (_buildManager.HaveATowerToBuild() && _existingTower != null)
      {
         _renderer.material.color = _errorColor;
      }
      else if(_buildManager.HaveATowerToBuild() && _existingTower == null)
      {
         _renderer.material.color = _hoverColor;
      }*/
   }

   void OnMouseExit()
   {
      //_renderer.material.color = _originalColor;
   }

   void ResizeColliderY()
   {
      float newSizeY = _originalSize.y * 3f;
      _towerBaseCollider.size = new Vector3(_originalSize.x, newSizeY, _originalSize.z);
      float newCenterY = _originalCenter.y + (newSizeY - _originalSize.y) /2f;
      _towerBaseCollider.center = new Vector3(_originalCenter.x, newCenterY, _originalCenter.z);
   }
   
   void RevertColliderSizeY()
   {
      _towerBaseCollider.center = _originalCenter;
      _towerBaseCollider.size = _originalSize;
   }

   public Vector3 GetTowerUIPosition()
   {
      Vector3 pos = new Vector3(_towerPlacementArea.transform.position.x, _towerPlacementArea.transform.position.y + 5f, _towerPlacementArea.transform.position.z);
      return pos;
   }
}
