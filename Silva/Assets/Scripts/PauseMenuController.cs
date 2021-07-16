using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject soundInstruction;

    private bool isPaused = false;
    private CharacterController2D playerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to CharacterController2D script
        playerScript = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) // Resume game if it is paused
            {
                ResumeGame();
            }
            else // Pause game if not paused
            {
                PauseGame();

                // Check if soundInstruction object still exists, as it is destroyed after it faded out
                if (soundInstruction)
                {
                    soundInstruction.SetActive(false);
                }
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playerScript.canMove = true;
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerScript.canMove = false;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OnQuitButtonClick()
    {
        // Quits the game, doesn't happen if opened in unity editor, only closes application if clicked in built version
        PlayerPrefs.DeleteAll();
        Application.Quit();
        Debug.Log("Quit");
    }


    public void OnStartMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
