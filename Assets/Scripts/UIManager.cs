using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using static UnityEditor.Progress;
using System;
public class UIManager : MonoBehaviour
{
   
    [SerializeField] GameObject OptionMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject WarningPanel;
    [SerializeField] GameObject PauseBtn;
    [SerializeField] RectTransform PausePanelRect;
    [SerializeField] float TopPosY, MiddlePosY;
    [SerializeField] float TweenDuration;
   
    



    private void Start()
    {

        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        PauseBtn.SetActive(true);
        WarningPanel.SetActive(false);


    }
   
   
   
    public void OnClickPauseBtn()
    { 
        PauseMenu.SetActive(true);
        PauseBtn.SetActive(false);
        PausePanelIn();
    }
    public async void OnClickResumeBtn()
    {
        await PausePanelOut();
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(true);
        //Time.timeScale = 0f;

    }
    public void OnClickBackBtn()
    {

        OptionMenu.SetActive(false);
        PauseMenu.SetActive(true );
    }
    public void OnClickMainMenu()
    {
        WarningPanel.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void OnClickSettingBtn()
    {
        OptionMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void OnClickYesBtn()
    {
        WarningPanel.SetActive(false);
        SceneManager.LoadScene("StartMenu");
    }
    public void OnClickNoBtn()
    {
        WarningPanel.SetActive(false);
        PauseMenu.SetActive(true);
    }
    void PausePanelIn()
    {
        PausePanelRect.DOAnchorPosY(MiddlePosY, TweenDuration).SetUpdate(true);

    }
    async Task PausePanelOut()
    {
        await PausePanelRect.DOAnchorPosY(TopPosY, TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        }

    }
