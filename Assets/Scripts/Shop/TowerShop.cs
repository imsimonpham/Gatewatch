using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerShop : MonoBehaviour
{
    [Header("Tower Blueprints")]
    [SerializeField] private TowerBlueprint _gunTurret;
    [SerializeField] private TowerBlueprint _artillery;
    [SerializeField] private TowerBlueprint _scorchCannon;
    
    [Header("Tower Shop Fields")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private BuildManager _buildManager;
    [SerializeField] private TMP_Text _gunTurretCostText;
    [SerializeField] private TMP_Text _artilleryCostText;
    [SerializeField] private TMP_Text _scorchCannonCostText;
    
    [Header("Tower Shop Buttons")]
    [SerializeField] private Button _gunTurretButton;
    [SerializeField] private Button _artilleryButton;
    [SerializeField] private Button _scorchCannonButton;
    
    private GameObject _existingTowerGhost;
    private TowerBlueprint[] _towerBlueprints;
    
    
    void Update()
    {
        //Clear Tower To Build when clicking right mouse
        if (Input.GetMouseButtonDown(1))
        {
            _buildManager.ClearTowerToBuild();
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
        //Debug.Log("Gun Turret selected");
        _buildManager.SetTowerToBuild(_gunTurret);
        if (_existingTowerGhost == null)
        {
            _existingTowerGhost = Instantiate(_gunTurret.GetTowerghost());
        }
    }
    
    public void SelectArtillery()
    {
        //Debug.Log("Artillery selected");
        _buildManager.SetTowerToBuild(_artillery);
        if (_existingTowerGhost == null)
        {
            _existingTowerGhost = Instantiate(_artillery.GetTowerghost());
        }
    }
    
    public void SelectScorchCannon()
    {
        //Debug.Log("Scorch Cannon selected");
        _buildManager.SetTowerToBuild(_scorchCannon);
        if (_existingTowerGhost == null)
        {
            _existingTowerGhost = Instantiate(_scorchCannon.GetTowerghost());
        }
    }
    
    void UpdateTowerCostTexts()
    {
        _gunTurretCostText.text = _gunTurret.GetBuildCost().ToString();
        _artilleryCostText.text = _artillery.GetBuildCost().ToString();
        _scorchCannonCostText.text = _scorchCannon.GetBuildCost().ToString();
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
        if (energy < _scorchCannon.GetBuildCost())
        {
            _scorchCannonButton.interactable = false;
        }
        else
        {
            _scorchCannonButton.interactable = true;
        }
    }
}
