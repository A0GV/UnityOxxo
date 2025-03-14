using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperControl : MonoBehaviour
{
    static public SuperControl Instance;

    public UIControlSuper uiController;

    void Start()
    {   
        // Stops all coroutines that might be running when the scene starts
        StopAllCoroutines();

        // Assigns this instance to the static Instance variable
        Instance = this;

        // Calls the function to show the main UI panel
        uiController.ShowMain();

        // Initializes additional settings or functionalities
        init();
    }

    void init()
    {
        // If the UI controller exists, start the timer for showing the summary panel
        if (uiController != null)
        {
            uiController.StartTime();
        }
    }

    // Loads the MenuScene, typically called when exiting a mini-game
    public void EndMiniGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Loads the EndGamesScene, usually triggered when the game ends
    public void Gotoendgame()
    {
        SceneManager.LoadScene("EndGamesScene");
    }
}
