using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public Toggle fullScreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;
    public Button confirmButton;
    public Button cancelButton;

    private List<ResolutionData> _options = new();

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;

        foreach(Resolution resolution in resolutions)
        {
            if(ResolutionUtility.CheckMinimumResolution(resolution.width) && 
            ResolutionUtility.CheckRefreshRateRatio((float)resolution.refreshRateRatio.value))
                _options.Add(new ResolutionData(resolution.width, resolution.height, resolution.refreshRateRatio));
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(_options.ConvertAll(option => option.ToString()));

        fullScreenToggle.isOn = Screen.fullScreen;

        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        confirmButton.onClick.AddListener(ConfirmSettings);
    }

    void ConfirmSettings()
    {
        gameObject.SetActive(false);
    }

    void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    void SetResolution(int resolutionIndex)
    {
        ResolutionData resolutionData = _options[resolutionIndex];
        Screen.SetResolution(resolutionData.Width, resolutionData.Height, Screen.fullScreen);
        // Screen.SetResolution(resolutionData.Width, resolutionData.Height, FullScreenMode.ExclusiveFullScreen, resolutionData.RefreshRateRatio);
    }
}
