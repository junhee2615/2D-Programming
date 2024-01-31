using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f;   // 장애물의 이동 속도
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)   // 오른쪽으로 이동
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else   // 왼쪽으로 이동
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if (transform.position.x >= 4f)
        {
            movingRight = false;   // 멈추기
        }
        else if (transform.position.x <= -4f)
        {
            movingRight = true;   // 움직이기
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // "Player" 태그를 가진 오브젝트와 충돌하면
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect()
    {
        // 장애물 색이 빨간색으로 변함
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        // 장애물 색이 흰색으로 변함
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }
}
