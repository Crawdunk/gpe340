using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isPaused;
    public float lives;

    public Player player;
    public UIManager uiManager;

    public TextMeshProUGUI livesCount;
    public TextMeshProUGUI ammoCount;

    public UnityEvent onPause;
    public UnityEvent onResume;
    public UnityEvent onLose;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        lives = 3;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        uiManager = GetComponent<UIManager>();
    }
   
    // Update is called once per frame
    void Update()
    {
        livesCount.text = "Lives: " + lives;

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }

        if (lives <= 0)
        {
            LoseGame();
        }

        if(player.weaponIsEquipped)
        {
            uiManager.ShowWeapon();
         
            if(player.equippedWeapon.isReloading)
            {
                ammoCount.text = "Reloading...";
            }    
            else
            {
                ammoCount.text = player.equippedWeapon.currentAmmo + "/" + player.equippedWeapon.maxAmmo;
            }
        }
        else
        {
            uiManager.HideWeapon();
        }
    }


    //GAME CONTROLS
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Instance.onPause.Invoke();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Instance.onResume.Invoke();
    }

    public void LoseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Instance.onLose.Invoke();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Respawn()
    {
        Pause();
    }
}

