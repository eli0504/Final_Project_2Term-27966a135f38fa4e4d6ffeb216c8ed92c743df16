using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private bool isPaused;

    //panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    //buttons
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance");
        }

        Instance = this;
      
        QuitPausePanel();
    }

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        //Pause logic with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //PAUSE
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Instance.ShowPausePanel();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        QuitPausePanel();
        isPaused = false;
    }

    public void ShowPausePanel()
    {

        pausePanel.SetActive(true);

    }
    public void QuitPausePanel()
    {
        pausePanel.SetActive(false);
    }
    //setings
    public void ShowSettingsPanel()
    {

        settingsPanel.SetActive(true);

    }
    public void QuitSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    //credits
    public void ShowCreditsPanel()
    {

        creditsPanel.SetActive(true);

    }
    public void QuitCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }

    //SCENES

    public void GotoMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoPlayerPrefScene()
    {
        SceneManager.LoadScene("PlayerPref");
    }

    public void GotoLevelOneScene()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GotoLevelTwoScene()
    {

        SceneManager.LoadScene("Level2");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Health.lives = 3;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
