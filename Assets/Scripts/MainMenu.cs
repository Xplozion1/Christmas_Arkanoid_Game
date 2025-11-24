using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// скрипт главного меню
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); 
    }
    
    public void ExitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
}
