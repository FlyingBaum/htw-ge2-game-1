using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private InputActionAsset controls;
    private GameObject EndOfGameScreen;
    private InputAction menuEsc;
    private bool isPauseMenuActive = false;
    private Text Player1Score;private Text Player2Score;
    private Text winnerAnnouncement;
    private Text timer;
    //Awake is called before Start and works kind of like a constructor / initialisor
    private void Awake()
    {
        var gameplayActionMap = controls.FindActionMap("Navigation");
        menuEsc = gameplayActionMap.FindAction("MenuEsc");
    }
    // Start is called before the first frame update
    void Start()
    {
        EndOfGameScreen = GameObject.Find("EndOfGameScreen");
        Player1Score = GameObject.Find("Player1Score").GetComponent<Text>();
        Player2Score = GameObject.Find("Player2Score").GetComponent<Text>();
        winnerAnnouncement = GameObject.Find("WinnerAnnouncement").GetComponent<Text>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        UpdateScoreBoard(0, 0);
        pauseMenu.SetActive(false);
        EndOfGameScreen.SetActive(false);
    }

    public void UpdateScoreBoard(int player1Score, int player2Score)
    {
        Player1Score.text = string.Format("{0}", player1Score);
        Player2Score.text = string.Format("{0}", player2Score);
    }
    public void ShowEndOfGameScreen(string message)
    {
        timer.text = "";
        winnerAnnouncement.text = message;
        EndOfGameScreen.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (menuEsc.WasReleasedThisFrame() && !isPauseMenuActive)
        {

            ReturnOnClick();
        }

        else if (menuEsc.WasReleasedThisFrame() && isPauseMenuActive)
        {
            ReturnOnClick();
        }
    }
    public void ResetOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ReturnOnClick()
    {
        Debug.Log("resume button was clicked");
        if (isPauseMenuActive)
        {
            
            pauseMenu.SetActive(false);
            isPauseMenuActive = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            isPauseMenuActive = true;
            Time.timeScale = 0;
        }
    }

    public void UpdateTimer(float remainingTime)
    {
        if (remainingTime > 0) timer.text = string.Format("{0:00}:{1:00}", (int)(remainingTime / 60), (int)(remainingTime % 60));
        else timer.text = "";
    }
}
