using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    public void OnPlayButtonClick()
    {
        // Get the button that is being clicked
        GameObject button = EventSystem.current.currentSelectedGameObject;

        if (button.name == "Level1Button")
        {
            SceneManager.LoadScene("Level1");
        } else if (button.name == "Level2Button")
        {
            SceneManager.LoadScene("Level2");
        } else if (button.name == "Level3Button")
        {
            Debug.Log("Load level 3");
            // TODO: load level 3
        }
    }

    public void OnQuitButtonClick()
    {
        // quits the game, doesn't happen if opened in unity editor, only closes application if clicked in built version
        PlayerPrefs.DeleteAll();
        Application.Quit();
        Debug.Log("Quit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
