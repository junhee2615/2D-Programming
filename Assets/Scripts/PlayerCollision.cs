using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    // 시간 슬라이더
    Slider slTimer;
    float fSliderBarTime;

    public Text timeText;   // 시간 텍스트
    public Text itemText;   // 아이템 텍스트
    public float speed = 10;   // player 스피드
    public AudioSource audioSource1;   // 오디오소스1
    public AudioSource audioSource2;   // 오디오소스2
    // 추가: 게임 시작 카운트 다운을 위한 변수
    public Text countDownText;   // 카운트다운 텍스트
    public GameObject countDownPanel;   // 카운트다운 패널
    bool gameStarted = false;   // 게임이 시작되었는지 여부를 확인

    // Start is called before the first frame update
    void Start()
    {
        slTimer = FindObjectOfType<Slider>().GetComponent<Slider>();
        itemText.enabled = false;   // 처음에 itemText를 숨김

        // 추가: 시작 시 카운트 다운 텍스트 초기화
        countDownPanel.SetActive(true); // 추가: 카운트 다운 패널 활성화
        countDownText.enabled = true;
        countDownText.text = "3";
        StartCoroutine(StartCountDown());

        // 오디오 소스 할당
        audioSource1 = GameObject.Find("audio1").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("audio2").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameStarted) return;   // 게임이 시작되지 않았으면 충돌처리 x

        // "Obstacle" 태그를 가진 물체와 충돌 시
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DecreaseTime(3.0f);   // 충돌 시 시간을 3초 감소
            Debug.Log("충돌");
        }

        // "Item" 태그를 가진 물체와 충돌 시
        if (collision.gameObject.CompareTag("Item"))
        {
            IncreaseTime(5.0f);   // 충돌 시 시간을 5초 증가
            Debug.Log("아이템 획득");
            Destroy(collision.gameObject);   // 아이템 오브젝트 파괴

            StartCoroutine(ShowItemText());   // 코루틴 시작하여 itemText 표시
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameStarted) return;   // 게임이 시작되지 않았으면 충돌처리 x

        if (collision.gameObject.name == "destination2")
        {
            SceneManager.LoadScene("clear1");
        }

        if (collision.gameObject.name == "destination_s2")
        {
            SceneManager.LoadScene("clear2");
        }

        if (collision.gameObject.name == "destination_s3")
        {
            SceneManager.LoadScene("clear3");
        }

        if (collision.gameObject.name == "destination_s4")
        {
            SceneManager.LoadScene("clear4");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) return;   // 게임이 시작되지 않았으면 충돌처리 x

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // print("h: " + h + ", v: " + v);

        // Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0);

        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        //transform.Translate(dir * speed * Time.deltaTime);

        transform.position += dir * speed * Time.deltaTime;

        if (slTimer.value > 0.0f)
        {
            slTimer.value -= Time.deltaTime;
            UpdateTimeText();
        }
        else
        {
            Debug.Log("시간초과");
            
            // 스테이지에 따라 다른 씬 로드
            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                SceneManager.LoadScene("GameOver1");
            }
            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                SceneManager.LoadScene("GameOver2");
            }
            else if (SceneManager.GetActiveScene().name == "Stage3")
            {
                SceneManager.LoadScene("GameOver3");
            }
            else if (SceneManager.GetActiveScene().name == "Stage4")
            {
                SceneManager.LoadScene("GameOver4");
            }
        }
    }

    void DecreaseTime(float amount)
    {
        slTimer.value -= amount;   // 정해진 시간만큼 감소
    }

    void IncreaseTime(float amount)
    {
        slTimer.value += amount;   // 정해진 시간만큼 증가
    }

    void UpdateTimeText()
    {
        int seconds = Mathf.FloorToInt(slTimer.value);

        string timeString = string.Format("Time: {0} sec", seconds);
        timeText.text = timeString;
    }

    IEnumerator ShowItemText()
    {
        itemText.enabled = true;   // itemText 표시
        yield return new WaitForSeconds(0.5f);   // 0.5초동안 표시
        itemText.enabled = false;   // itemText 숨김
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "2";
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "1";
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "게임 시작!";
        yield return new WaitForSeconds(0.9f);

        countDownText.enabled = false;
        countDownPanel.SetActive(false);   //카운트 다운 패널 비활성화
        gameStarted = true;   // 추가: 게임 시작 상태로 설정

        // 오디오 소스 변경하기
        audioSource1.Stop();
        audioSource2.Play();

        Debug.Log("게임 시작!");
    }
}
