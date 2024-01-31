using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;   // ��ư�� ���� Ÿ��
    public Transform buttonScale;   // ������ ��ư ũ��
    // ��ư�� ���� ũ��
    Vector3 defaultScale;   // ���� ��ư ũ��

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.Start:
                SceneManager.LoadScene("Stage1");   // ��������1 ���� �ҷ��´�
                break;
            case BTNType.HowToPlay:
                SceneManager.LoadScene("HowToPlay");
                break;
            case BTNType.NextStage:
                SceneManager.LoadScene("Stage2");   // ��������2 ���� �ҷ��´�
                break;
            case BTNType.NextStage2:
                SceneManager.LoadScene("Stage3");   // ��������3 ���� �ҷ��´�
                break;
            case BTNType.NextStage3:
                SceneManager.LoadScene("Stage4");   // ��������4 ���� �ҷ��´�
                break;
            case BTNType.AllClear:
                SceneManager.LoadScene("AllClear");   // All Clear ���� �ҷ��´�
                break;
            case BTNType.Home:
                SceneManager.LoadScene("Home");   // Home ���� �ҷ��´�
                break;
            case BTNType.Retry:
                SceneManager.LoadScene("Stage1");   // ��������1 ���� �ٽ� �ҷ��´�
                break;
            case BTNType.Retry2:
                SceneManager.LoadScene("Stage2");   // ��������2 ���� �ٽ� �ҷ��´�
                break;
            case BTNType.Retry3:
                SceneManager.LoadScene("Stage3");   // ��������3 ���� �ٽ� �ҷ��´�
                break;
            case BTNType.Retry4:
                SceneManager.LoadScene("Stage4");   // ��������4 ���� �ٽ� �ҷ��´�
                break;
        }
    }

    // ��ư ���� Ŀ���� ���� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    // ��ư �ܺο� Ŀ���� ���� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
