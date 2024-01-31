using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    // �ð� �����̴�
    Slider slTimer;
    float fSliderBarTime;

    public Text timeText;   // �ð� �ؽ�Ʈ
    public Text itemText;   // ������ �ؽ�Ʈ
    public float speed = 10;   // player ���ǵ�
    public AudioSource audioSource1;   // ������ҽ�1
    public AudioSource audioSource2;   // ������ҽ�2
    // �߰�: ���� ���� ī��Ʈ �ٿ��� ���� ����
    public Text countDownText;   // ī��Ʈ�ٿ� �ؽ�Ʈ
    public GameObject countDownPanel;   // ī��Ʈ�ٿ� �г�
    bool gameStarted = false;   // ������ ���۵Ǿ����� ���θ� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        slTimer = FindObjectOfType<Slider>().GetComponent<Slider>();
        itemText.enabled = false;   // ó���� itemText�� ����

        // �߰�: ���� �� ī��Ʈ �ٿ� �ؽ�Ʈ �ʱ�ȭ
        countDownPanel.SetActive(true); // �߰�: ī��Ʈ �ٿ� �г� Ȱ��ȭ
        countDownText.enabled = true;
        countDownText.text = "3";
        StartCoroutine(StartCountDown());

        // ����� �ҽ� �Ҵ�
        audioSource1 = GameObject.Find("audio1").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("audio2").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameStarted) return;   // ������ ���۵��� �ʾ����� �浹ó�� x

        // "Obstacle" �±׸� ���� ��ü�� �浹 ��
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DecreaseTime(3.0f);   // �浹 �� �ð��� 3�� ����
            Debug.Log("�浹");
        }

        // "Item" �±׸� ���� ��ü�� �浹 ��
        if (collision.gameObject.CompareTag("Item"))
        {
            IncreaseTime(5.0f);   // �浹 �� �ð��� 5�� ����
            Debug.Log("������ ȹ��");
            Destroy(collision.gameObject);   // ������ ������Ʈ �ı�

            StartCoroutine(ShowItemText());   // �ڷ�ƾ �����Ͽ� itemText ǥ��
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameStarted) return;   // ������ ���۵��� �ʾ����� �浹ó�� x

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
        if (!gameStarted) return;   // ������ ���۵��� �ʾ����� �浹ó�� x

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
            Debug.Log("�ð��ʰ�");
            
            // ���������� ���� �ٸ� �� �ε�
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
        slTimer.value -= amount;   // ������ �ð���ŭ ����
    }

    void IncreaseTime(float amount)
    {
        slTimer.value += amount;   // ������ �ð���ŭ ����
    }

    void UpdateTimeText()
    {
        int seconds = Mathf.FloorToInt(slTimer.value);

        string timeString = string.Format("Time: {0} sec", seconds);
        timeText.text = timeString;
    }

    IEnumerator ShowItemText()
    {
        itemText.enabled = true;   // itemText ǥ��
        yield return new WaitForSeconds(0.5f);   // 0.5�ʵ��� ǥ��
        itemText.enabled = false;   // itemText ����
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "2";
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "1";
        yield return new WaitForSeconds(0.9f);
        countDownText.text = "���� ����!";
        yield return new WaitForSeconds(0.9f);

        countDownText.enabled = false;
        countDownPanel.SetActive(false);   //ī��Ʈ �ٿ� �г� ��Ȱ��ȭ
        gameStarted = true;   // �߰�: ���� ���� ���·� ����

        // ����� �ҽ� �����ϱ�
        audioSource1.Stop();
        audioSource2.Play();

        Debug.Log("���� ����!");
    }
}
