using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;   // 버튼의 현재 타입
    public Transform buttonScale;   // 변형된 버튼 크기
    // 버튼의 원래 크기
    Vector3 defaultScale;   // 원래 버튼 크기

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.Start:
                SceneManager.LoadScene("Stage1");   // 스테이지1 씬을 불러온다
                break;
            case BTNType.HowToPlay:
                SceneManager.LoadScene("HowToPlay");
                break;
            case BTNType.NextStage:
                SceneManager.LoadScene("Stage2");   // 스테이지2 씬을 불러온다
                break;
            case BTNType.NextStage2:
                SceneManager.LoadScene("Stage3");   // 스테이지3 씬을 불러온다
                break;
            case BTNType.NextStage3:
                SceneManager.LoadScene("Stage4");   // 스테이지4 씬을 불러온다
                break;
            case BTNType.AllClear:
                SceneManager.LoadScene("AllClear");   // All Clear 씬을 불러온다
                break;
            case BTNType.Home:
                SceneManager.LoadScene("Home");   // Home 씬을 불러온다
                break;
            case BTNType.Retry:
                SceneManager.LoadScene("Stage1");   // 스테이지1 씬을 다시 불러온다
                break;
            case BTNType.Retry2:
                SceneManager.LoadScene("Stage2");   // 스테이지2 씬을 다시 불러온다
                break;
            case BTNType.Retry3:
                SceneManager.LoadScene("Stage3");   // 스테이지3 씬을 다시 불러온다
                break;
            case BTNType.Retry4:
                SceneManager.LoadScene("Stage4");   // 스테이지4 씬을 다시 불러온다
                break;
        }
    }

    // 버튼 위에 커서가 있을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    // 버튼 외부에 커서가 있을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
