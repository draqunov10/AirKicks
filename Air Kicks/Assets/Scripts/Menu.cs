using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;
    private GameObject pauseButton;

    public void replay() {
        SceneManager.LoadScene("MainGame");
    }

    public void pause() {
        playerMovement.enabled = false;
        cameraFollow.enabled = false;
        pauseButton = GameObject.FindWithTag("Pause Button");
        pauseButton.SetActive(false);
    }

    public void resume() {
        playerMovement.enabled = true;
        cameraFollow.enabled = true;
        pauseButton.SetActive(true);
    }

    public void mainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void shop() {
        Debug.Log("Kunyari-Shop Pops Up");
    }
}
