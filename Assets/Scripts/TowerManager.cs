using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private BuildManager _buildManager;
    [SerializeField] private TowerBaseUI _towerBaseUI;
    private TowerBase _currentTowerBase;
    private Canvas _towerBaseUICanvas;
    [SerializeField] private PlayerStats _playerStats;
    private TowerBase _selectedTowerBase;
    private GameObject _selectedTower;
    //private TowerBlueprint _selectedTowerBlueprint;
    private int _towerSaleGain;
    [SerializeField] private TMP_Text _towerSaleGainText;
    [SerializeField] private TMP_Text _towerUpgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
        _towerBaseUICanvas = _towerBaseUI.GetComponent<Canvas>();
        if (_towerBaseUICanvas == null)
        {
            Debug.LogError("Tower base UI canvas is null");
        }
    }

    void Update()
    {
        CheckForTowerBase();
        if (Input.GetMouseButtonDown(1))
        {
            HideTowerBaseUI();
        }
    }
    void CheckForTowerBase()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            transform.position = raycastHit.point;
            int hitLayer = raycastHit.collider.gameObject.layer;
            if (LayerMask.LayerToName(hitLayer) == "Base")
            {
                TowerBase towerBase = raycastHit.collider.gameObject.GetComponent<TowerBase>();
                if (towerBase.CanUpgradeOrSellHere() && Input.GetMouseButtonDown(0))
                {
                    _selectedTowerBase = towerBase;
                    //_selectedTowerBlueprint = _selectedTowerBase.GetExistingTowerBlueprint();
                    UpdateTowerSaleGainText(_selectedTowerBase);
                    UpdateTowerUpgradeCostText(_selectedTowerBase);
                    ShowTowerBaseUI(towerBase.GetTowerUIPosition());
                    _selectedTowerBase.UpdateUpgradeButtonState();
                    Debug.Log(_selectedTowerBase.GetTowerLevel());
                } 
            }
        } 
    }

    //UPGRADING TOWERS
    public void UpgradeTower()
    {
        _selectedTowerBase.UpgradeExistingTower();
    }
    
    //SELLING TOWERS
    public void SellTower()
    {
        _selectedTowerBase.SellExistingTower();
    }
    
    void UpdateTowerSaleGainText(TowerBase towerBase)
    {
        TowerBlueprint towerBlueprint = towerBase.GetExistingTowerBlueprint();
        _towerSaleGainText.text = towerBlueprint.GetTowerSaleGain(towerBase.GetTowerLevel()).ToString();
    }

    void UpdateTowerUpgradeCostText(TowerBase towerBase)
    {
        int towerLevel = towerBase.GetTowerLevel();
        TowerBlueprint towerBlueprint = towerBase.GetExistingTowerBlueprint();
        if (towerLevel == 1)
        {
            _towerUpgradeCostText.text = towerBlueprint.GetUpgradeCostToLv2().ToString();
        } else if (towerLevel == 2)
        {
            _towerUpgradeCostText.text = towerBlueprint.GetUpgradeCostToLv3().ToString();
        }
        else
        {
            _towerUpgradeCostText.text = "MAX";
        }
    }
    
    //TOWER BASE UI
    void HideTowerBaseUI()
    {
        _towerBaseUICanvas.enabled = false;
    }

    void ShowTowerBaseUI(Vector3 position)
    {
        _towerBaseUICanvas.enabled = true;
        _towerBaseUICanvas.transform.position = position;
    }

   
}
