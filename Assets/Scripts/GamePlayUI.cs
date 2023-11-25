using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private TMP_Text _livesText;
    [SerializeField] private TMP_Text _waveText;

    public void UpDateCountdownText(float time)
    {
        _countdownText.text = string.Format("{0:00}", time);
    }

    public void UpdateEnergyText(int energy)
    {
        _energyText.text = energy.ToString();
    }
    
    public void UpdateLivesText(int lives)
    {
        _livesText.text = lives.ToString();
    }
    
    public void UpdateWaveText(int wave, int maxwave)
    {
        _waveText.text = wave + "/" + maxwave;
    }
}
