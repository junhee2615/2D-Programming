using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f;   // ��ֹ��� �̵� �ӵ�
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)   // ���������� �̵�
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else   // �������� �̵�
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if (transform.position.x >= 4f)
        {
            movingRight = false;   // ���߱�
        }
        else if (transform.position.x <= -4f)
        {
            movingRight = true;   // �����̱�
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // "Player" �±׸� ���� ������Ʈ�� �浹�ϸ�
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect()
    {
        // ��ֹ� ���� ���������� ����
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        // ��ֹ� ���� ������� ����
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }
}
