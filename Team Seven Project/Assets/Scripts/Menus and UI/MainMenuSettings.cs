using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle = null;

    //Resolution
    [SerializeField] private Button _resolutionLeftButton = null;
    [SerializeField] private Button _resolutionRightButton = null;
    [SerializeField] private TextMeshProUGUI _resolutionText = null;

    //Quality
    [SerializeField] private Button _qualityLeftButton = null;
    [SerializeField] private Button _qualityRightButton = null;
    [SerializeField] private TextMeshProUGUI _qualityText = null;

    //Vsync
    [SerializeField] private Button _vSyncLeftButton = null;
    [SerializeField] private Button _vSyncRightButton = null;
    [SerializeField] private TextMeshProUGUI _vSyncText = null;


}
