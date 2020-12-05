using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onStartClick()
    {
        SceneManager.LoadScene("scenes/Start");
    }

    public void onQuitClick()
    {
        Application.Quit();
    }

}
