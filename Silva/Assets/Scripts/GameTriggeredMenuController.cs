using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTriggeredMenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Text gameOverScoreText;

    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField] private Text levelScoreText;

    [SerializeField] private GameObject inGameScoreText;

    [SerializeField] string currentLevel;
    [SerializeField] string nextLevel;

    private CharacterController2D playerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<CharacterController2D>();
    }

    public void DisplayMenu (string menuName)
    {
        // Pause the game
        Time.timeScale = 0f;

        // Disable in-game score display
        inGameScoreText.SetActive(false);

        // Disable player movement
        playerScript.canMove = false;

        // Display menu depending on parameter
        if (menuName == "gameOverMenu") //gameOverMenu
        {
            // Adjust gameOverScoreText
            gameOverScoreText.text = "SCORE: " + playerScript.GetScore();
            // Make gameOverMenu visible
            gameOverMenu.SetActive(true);
        }

        else if (menuName == "levelCompletedMenu") //levelCompletedMenu
        {
            // Adjust levelCompletedScoreText
            levelScoreText.text = "SCORE: " + playerScript.GetScore();
            // Make gameOverMenu visible
            levelCompletedMenu.SetActive(true);
        }

        else //invalid input
        {
            return;
        }
    }

    public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void OnNextLevelButtonClick()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void OnStartMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
