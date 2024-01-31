using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;   // 플레이어 게임 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");   // 플레이어 게임 오브젝트를 찾는다.
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라를 플레이어에 포커스한다.
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            transform.position.x, playerPos.y, transform.position.z);
    }
}
