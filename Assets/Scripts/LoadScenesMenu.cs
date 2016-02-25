using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScenesMenu : MonoBehaviour
{
    public void loadNewGame()
    {
        SceneManager.LoadScene(1);
       // Application.LoadLevel(0);
    }

    public void loadTutorial()
    {

    }
    public void loadOptions()
    {

    }

    public void quitGame()
    {
        Debug.Log("tototo");
        Application.Quit();
    }
}
