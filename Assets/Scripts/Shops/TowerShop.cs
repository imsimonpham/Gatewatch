using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerShop : MonoBehaviour
{
    [Header("Tower Blueprints")]
    [SerializeField] private TowerBlueprint _artillery;
    
    [Header("Tower Shop Fields")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private BuildManager _buildManager;
    [SerializeField] private TMP_Text _artilleryCostText;
    private GameObject _existingTowerGhost;
    private TowerBlueprint[] _towerBlueprints;
    
    void Update()
    {
        //Clear Tower To Build when clicking right mouse
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _buildManager.ClearTowerToBuld();
        }

        // destroy tower ghost when having no tower to build
        if (!_buildManager.HaveATowerToBuild())
        {
            Destroy(_existingTowerGhost);
        }

        UpdateTowerCostTexts();
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
        _artilleryCostText.text = _artillery.GetBuildCost().ToString();
    }
}
