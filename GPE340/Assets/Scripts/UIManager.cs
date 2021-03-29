using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject weaponDisplay; //Grab different UI elements
    public GameObject settingsMenu;

    void Start()
    {
        loseMenu.SetActive(false);
        pauseMenu.SetActive(false); //Deactivate all UI elements
        weaponDisplay.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true); //Activate pause menu
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false); //deactivate pause
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true); //activate lose and deactivate pause
        pauseMenu.SetActive(false);
    }

    public void ShowWeapon()
    {
        weaponDisplay.SetActive(true); //activate weapon display
    }

    public void HideWeapon()
    {
        weaponDisplay.SetActive(false); //deactivate weapon display
    }

    public void ShowSettingsMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void HideSettingsMenu()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    
}
