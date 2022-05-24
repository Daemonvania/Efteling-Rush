using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    public GameObject startMenu;

    private Player player;
    public GameObject customizeWindow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.enabled = false;
        Time.timeScale = 0;
        startMenu.SetActive(true);
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
    }
}
