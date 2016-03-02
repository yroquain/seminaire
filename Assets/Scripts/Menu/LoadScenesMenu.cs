using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScenesMenu : MonoBehaviour
{
    public void loadNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void loadTutorial()
    {

    }
    public void loadOptions()
    {

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
