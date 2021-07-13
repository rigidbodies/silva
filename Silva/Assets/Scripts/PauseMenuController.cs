using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    bool IsPaused = false;
    public GameObject pauseMenu;
    [SerializeField] GameObject soundInstruction;
    private CharacterController2D playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
                // check if soundInstruction object still exists, as it is destroyed after it faded out
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
        IsPaused = false;
    }
    
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerScript.canMove = false;
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void OnQuitButtonClick()
    {
        // quits the game, doesn't happen if opened in unity editor, only closes application if clicked in built version
        PlayerPrefs.DeleteAll();
        Application.Quit();
        Debug.Log("Quit");
    }


    public void OnStartMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
