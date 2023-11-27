using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerShop : MonoBehaviour
{
    [Header("Tower Blueprints")]
    [SerializeField] private TowerBlueprint _gunTurret;
    [SerializeField] private TowerBlueprint _artillery;
    
    [Header("Tower Shop Fields")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private BuildManager _buildManager;
    [SerializeField] private TMP_Text _gunTurretCostText;
    [SerializeField] private TMP_Text _artilleryCostText;
    
    [Header("Tower Shop Buttons")]
    [SerializeField] private Button _gunTurretButton;
    [SerializeField] private Button _artilleryButton;
    
    private GameObject _existingTowerGhost;
    private TowerBlueprint[] _towerBlueprints;
    
    
    void Update()
    {
        //Clear Tower To Build when clicking right mouse
        if (Input.GetMouseButtonDown(1))
        {
            _buildManager.ClearTowerToBuld();
        }

        // destroy tower ghost when having no tower to build
        if (!_buildManager.HaveATowerToBuild())
        {
            Destroy(_existingTowerGhost);
        }
        UpdateTowerCostTexts();
        UpdateTowerButtonState();
    }

    
    public void SelectGunTurret()
    {
        Debug.Log("Gun Turret selected");
        _buildManager.SetTowerToBuild(_gunTurret);
        if (_existingTowerGhost == null)
        {
            _existingTowerGhost = Instantiate(_gunTurret.GetTowerghost());
        }
    }
    
    public void SelectArtillery()
    {
        Debug.Log("Artillery selected");
        _buildManager.SetTowerToBuild(_artillery);
        if (_existingTowerGhost == null)
        {
            _existingTowerGhost = Instantiate(_artillery.GetTowerghost());
        }
    }
    
    void UpdateTowerCostTexts()
    {
        _gunTurretCostText.text = _gunTurret.GetBuildCost().ToString();
        _artilleryCostText.text = _artillery.GetBuildCost().ToString();
    }

    void UpdateTowerButtonState()
    {
        int energy = _playerStats.GetEnergy();
        if (energy < _gunTurret.GetBuildCost())
        {
            _gunTurretButton.interactable = false;
        }
        else
        {
            _gunTurretButton.interactable = true;
        }
        if (energy < _artillery.GetBuildCost())
        {
            _artilleryButton.interactable = false;
        }
        else
        {
            _artilleryButton.interactable = true;
        }
    }
}
