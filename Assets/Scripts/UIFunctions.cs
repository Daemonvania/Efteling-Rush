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
    public GameObject window2;
    public GameObject ticketCounter;
    private DontDestroyOnLoad _dontDestroyOnLoad;

    public TMP_Text levelNumber;
    
    public TMP_Text volumeText;

    // Start is called before the first frame update
    void Start()
    {
        ticketCounter.SetActive(false);
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        levelNumber.text = "LEVEL" + " " + _dontDestroyOnLoad.currentLevel;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.enabled = false;
        Time.timeScale = 0;
        startMenu.SetActive(true);
        settingsWindow.SetActive(false);
        customizeWindow.SetActive(false);
      //  window2.SetActive(false);

      AudioListener.volume = _dontDestroyOnLoad.audioLevel;
    }

    // Update is called once per frame
    public void StartGame()
    {
        if (customizeWindow.activeSelf || settingsWindow.activeSelf)
        {
            return;}
        
            Time.timeScale = 1;
            ticketCounter.SetActive(true);
            player.enabled = true;
            startMenu.SetActive(false);
         
    }

    public void ManageWindow(bool enable)
    {
        customizeWindow.SetActive(enable);
        _dontDestroyOnLoad.HidePrices();
    }

    public void ManageWindow2(bool enable)
    {
        window2.SetActive(enable);
        _dontDestroyOnLoad.HidePrices();
    }

    public void ManageSettingsWindow(bool enable)
    {
        settingsWindow.SetActive(enable);
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SaveSystem.SavePlayer(_dontDestroyOnLoad);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SaveSystem.SavePlayer(_dontDestroyOnLoad);
            SceneManager.LoadScene(2);
        }
    }
    
    public void ReloadScene()
    {
        SaveSystem.SavePlayer(_dontDestroyOnLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GiveMoney()
    {
        print("addedTickets");
        _dontDestroyOnLoad.tickets += 10000;
    }

    public void ManageVolume(float volumeLevel)
    {
        AudioListener.volume += volumeLevel;
        if (AudioListener.volume < 0)
        {
            AudioListener.volume = 0;
        }
        if (AudioListener.volume > 1)
        {
            AudioListener.volume = 1;
        }
        _dontDestroyOnLoad.audioLevel = AudioListener.volume;
        int audioDisplay = Mathf.RoundToInt(AudioListener.volume * 10);
        
        volumeText.text = audioDisplay.ToString();
    }
}
