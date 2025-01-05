using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject StartMenu;
    private void Start()
    {
        StartMenu.SetActive(true);  
        OptionMenu.SetActive(false);
    }
    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("Level1");

    }
    public void OnClickOptionBtn()
    {
        StartMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }
    public void OnClickBackBtn()
    {
        StartMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }
    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

}
