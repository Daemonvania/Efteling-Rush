using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public GameObject startMenu;

    private Player player;
    public GameObject customizeWindow;
    public GameObject settingsWindow;
    private DontDestroyOnLoad _dontDestroyOnLoad;

    public TMP_Text levelNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        levelNumber.text = "LEVEL" + " " +SceneManager.GetActiveScene().buildIndex;
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.enabled = false;
        Time.timeScale = 0;
        startMenu.SetActive(true);
        settingsWindow.SetActive(false);
        customizeWindow.SetActive(false);
    }

    // Update is called once per frame
    public void StartGame()
    {
        Time.timeScale = 1;
        player.enabled = true;
        startMenu.SetActive(false);
    }
    public void ManageWindow(bool enable)
    {
        customizeWindow.SetActive(enable);
        _dontDestroyOnLoad.HidePrices();
    }

    public void ManageSettingsWindow(bool enable)
    {
        settingsWindow.SetActive(enable);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GiveMoney()
    {
        print("addedTickets");
        _dontDestroyOnLoad.tickets += 10000;
    }
}
