using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _countdownText;

    public void UpDateCountdownText(float time)
    {
        _countdownText.text = string.Format("{0:00}", time);
    }
}
