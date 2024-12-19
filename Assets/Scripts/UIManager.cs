using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject OptionMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject WarningPanel;
    [SerializeField] GameObject PauseBtn;
    [SerializeField] RectTransform PausePanelRect;
    [SerializeField] float TopPosY, MiddlePosY;
    [SerializeField] float TweenDuration;
    
    private void Start()
    {
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        PauseBtn.SetActive(false);
        WarningPanel.SetActive(false);

    }
    public void OnClickPLayBtn()
    {
        //SceneManager.LoadScene("SampleScene");
        StartMenu.SetActive(false);
        Time.timeScale= 1.0f;
        PauseBtn.SetActive(true);

    }
    public void OnClickOptionBtn()
    {
        OptionMenu.SetActive(true);
        StartMenu.SetActive(false);

    }
    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
    public void OnClickPauseBtn()
    {
        PauseMenu.SetActive(true );
        PauseBtn .SetActive(false);
        Time.timeScale = 0f;
        PausePanelIn();
    }
    public async void OnClickResumeBtn()
    {
       await PausePanelOut();
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(true) ;
        Time.timeScale = 0f;

    }
    public void OnClickBackBtn()
    {
        StartMenu.SetActive(true);
        OptionMenu.SetActive(false );   
    }
    public void OnClickMainMenu()
    {
        WarningPanel.SetActive(true);
        PauseMenu .SetActive(false) ;   
    }
    public void OnClickYesBtn()
    {

        StartMenu.SetActive(true) ;
        WarningPanel.SetActive(false) ; 
    }
    public void OnClickNoBtn()
    { 
        WarningPanel .SetActive(false) ;
        PauseMenu .SetActive(true) ;
    }
    void PausePanelIn()
    {
        PausePanelRect.DOAnchorPosY(MiddlePosY, TweenDuration).SetUpdate(true);

    }
    async  Task  PausePanelOut()
    {
        await PausePanelRect.DOAnchorPosY(TopPosY, TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
