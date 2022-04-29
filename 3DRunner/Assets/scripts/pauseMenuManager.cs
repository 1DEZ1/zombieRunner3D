using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
 
    private void Update() {
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void pauseMenuSetActive(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
