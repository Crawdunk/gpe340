using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider masterSlider;
    public Slider soundsSlider;
    public Slider musicSlider;

    [Header("Graphics Settings")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    [Header("Buttons")]
    public Button backButton;
    public Button applyButton;

    [Header("Audio Settings")]
    public AudioMixer masterMixer;

    public UnityEvent onEnable;

    void Awake()
    {
        //Build resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        for (int index = 0; index < Screen.resolutions.Length; index++)
        {
            resolutions.Add (string.Format("{0} x {1}", Screen.resolutions [index].width, Screen.resolutions [index].height));
        }
        resolutionDropdown.AddOptions(resolutions);

        //Build quality levels;
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
    }

    void Start()
    {
        
    }


    void Update()
    {

    }

    public void SetSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master Volume", masterSlider.value);
        soundsSlider.value = PlayerPrefs.GetFloat("Sound Volume", soundsSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", musicSlider.value);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        applyButton.interactable = false;
    }

    public void Apply()
    {
        PlayerPrefs.SetFloat("Master Volume", masterSlider.value);
        masterMixer.SetFloat("Master Volume", masterSlider.value);
        PlayerPrefs.SetFloat("Sound Volume", soundsSlider.value);
        masterMixer.SetFloat("Sound Volume", soundsSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
        masterMixer.SetFloat("Music Volume", musicSlider.value);

        if (Screen.fullScreen == true)
        {
            ChangeFullScreen();
        }
        else
        {
            ChangeFullScreen();
        }

        applyButton.interactable = false;

        
    }

    public void ActivateApplyButton()
    {
        applyButton.interactable = true;
    }

    public void ChangeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
