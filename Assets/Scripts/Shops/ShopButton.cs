using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
   [SerializeField] private TMP_Text _towerCostText;
   [SerializeField] private PlayerStats _playerStats;
   private int _towerCost;
   private Button _button;
   
   void Start()
   {
      
      // Example: Assuming _towerCostText.text contains a valid integer as a string
      string costString = _towerCostText.text;

      // Convert the string to an integer
      if (int.TryParse(costString, out int towerCost))
      {
         _towerCost = towerCost;
      }
      else
      {
         // Handle the case where the conversion failed
         Debug.LogError("Conversion failed. Invalid format in _towerCostText.");
      }

      _button = GetComponent<Button>();
   }

   void Update()
   {
      ToggleButton();
   }

   void ToggleButton()
   {
      if (_playerStats.GetEnergy() >= _towerCost)
      {
         _button.interactable = true;
      }
      else
      {
         _button.interactable = false;
      }
   }
}
