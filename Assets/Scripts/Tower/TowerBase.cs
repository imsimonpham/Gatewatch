using UnityEngine;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour
{
   [SerializeField] private GameObject _towerPlacementArea;
   [SerializeField] private GameObject _existingTower;
   private BuildManager _buildManager;
   private PlayerStats _playerStats;
   private GamePlayUI _gamePlayUI;
   private BoxCollider _towerBaseCollider;
   private TowerBaseUI _towerBaseUI;
   private TowerBlueprint _existingTowerBlueprint;
   private Canvas _towerBaseUICanvas;
   private Button _upgradeButton;
   private int _towerLevel;
   private bool _canUpgrade;

   void Start()
   {
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

      _towerBaseUI = GameObject.FindWithTag("TowerBaseUI").GetComponent<TowerBaseUI>();
      if (_towerBaseUI == null)
      {
         Debug.LogError("Tower Base UI is null");
      }

      _towerBaseUICanvas = _towerBaseUI.GetComponent<Canvas>();
      if (_towerBaseUICanvas == null)
      {
         Debug.LogError("Tower base UI canvas is null");
      }

      _upgradeButton = GameObject.FindWithTag("UpgradeButton").GetComponent<Button>();
      if (_upgradeButton == null)
      {
         Debug.LogError("Tower base UI canvas is null");
      }
   }

   void Update()
   {
      
   }

   public bool CanBuildHere()
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

   public GameObject GetExistingTower()
   {
      return _existingTower;
   }

   public TowerBlueprint GetExistingTowerBlueprint()
   {
      return _existingTowerBlueprint;
   }

   public bool CanUpgradeOrSellHere()
   {
      //return false if there is a tower to build
      if (_buildManager.HaveATowerToBuild())
      {
         return false;
      }

      // return false if is there is no existing tower
      if (_existingTower == null)
      {
         return false;
      }

      return true;
   }

   public void BuildTower(TowerBlueprint towerBlueprint)
   {
      _existingTowerBlueprint = towerBlueprint;
      if (!HasEnoughEnergyToBuild(towerBlueprint))
      {
         _existingTowerBlueprint = null;
         return;
      }
      GameObject tower = Instantiate(towerBlueprint.GetPrefabLv1(), _towerPlacementArea.transform.position,
         Quaternion.identity);
      _playerStats.SpendEnergy(towerBlueprint.GetBuildCost());
      _existingTower = tower;
      _towerLevel = 1;
      _buildManager.ClearTowerToBuld();
   }

   public void UpgradeExistingTower()
   {
      GameObject newTowerPrefab;
      if (_towerLevel == 1)
      {
         if (!HasEnoughEnergyToUpgrade(2))
         {
            return;
         }
         _towerLevel = 2;
         Debug.Log("Upgrade to level 2");
         newTowerPrefab = _existingTowerBlueprint.GetPrefabLv2();
         Destroy(_existingTower);
         GameObject tower = Instantiate(newTowerPrefab, _towerPlacementArea.transform.position, Quaternion.identity);
         _playerStats.SpendEnergy(_existingTowerBlueprint.GetUpgradeCostToLv2());
         _existingTower = tower;
      }
      else if (_towerLevel == 2)
      {
         if (!HasEnoughEnergyToUpgrade(3))
         {
            return;
         }
         _towerLevel = 3;
         Debug.Log("Upgrade to level 3");
         newTowerPrefab = _existingTowerBlueprint.GetPrefabLv3();
         Destroy(_existingTower);
         GameObject tower = Instantiate(newTowerPrefab, _towerPlacementArea.transform.position, Quaternion.identity);
         _playerStats.SpendEnergy(_existingTowerBlueprint.GetUpgradeCostToLv3());
         _existingTower = tower;
      }

      _towerBaseUICanvas.enabled = false;
   }

   public void SellExistingTower()
   {
      int towerSaleGain = _existingTowerBlueprint.GetTowerSaleGain(_towerLevel);
      _playerStats.GainEnergy(towerSaleGain);
      _existingTowerBlueprint = null;
      Destroy(_existingTower);
      _towerBaseUICanvas.enabled = false;
   }

   public Vector3 GetTowerUIPosition()
   {
      Vector3 pos = new Vector3(_towerPlacementArea.transform.position.x, _towerPlacementArea.transform.position.y + 8f,
         _towerPlacementArea.transform.position.z);
      return pos;
   }

   private bool HasEnoughEnergyToBuild(TowerBlueprint towerBlueprint)
   {
      int energy = _playerStats.GetEnergy();
      if (energy < towerBlueprint.GetBuildCost())
      {
         return false;
      }
      return true;
   }
   private bool HasEnoughEnergyToUpgrade(int towerLevel)
   {
      int energy = _playerStats.GetEnergy();
      if (_towerLevel == 1 && energy < _existingTowerBlueprint.GetUpgradeCostToLv2())
      {
         return false;
      }
      if (_towerLevel == 2 && energy < _existingTowerBlueprint.GetUpgradeCostToLv3())
      {
         return false;
      }
      return true;
   }

   public void UpdateUpgradeButtonState()
   {
      if (HasEnoughEnergyToUpgrade(_towerLevel) && _towerLevel <= 2)
      {
         _upgradeButton.interactable = true;
      }
      else
      {
         _upgradeButton.interactable = false;
      }
   }

   public int GetTowerLevel()
   {
      return _towerLevel;
   }
}
