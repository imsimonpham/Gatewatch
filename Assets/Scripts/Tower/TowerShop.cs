using UnityEngine;

public class TowerShop : MonoBehaviour
{
    [SerializeField] private TowerBlueprint _antiAircrafter;
    [SerializeField] private BuildManager _buildManager;

    public void SelectAntiAircrafter()
    {
        Debug.Log("Anti Aircrafter selected");
        _buildManager.SetTowerToBuild(_antiAircrafter);
    }
}
