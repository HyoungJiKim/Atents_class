using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Planet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float minRightEnd = 40.0f;
    public float maxRightEnd = 60.0f;
    public float minHeight = -8.0f;
    public float maxHeight = -5.0f;

    const float movePositionX = -10.0f;

    private void Update()
    {
        //이 오브젝트는 왼쪽으로 매 초 moveSpeed 만큼 이동
        //이 오브젝트는 movePositionX보다 왼쪽으로 이동하면 오른쪽 끝으로 이동한다.
        //오른쪽 끝의 위치는 minRightEnd~maxRightEnd 사이로 랜덤으로 결정

        if (transform.position.x < movePositionX)
        {
            transform.Translate(transform.right * Random.Range(minRightEnd, maxRightEnd));

            //높이도 minHeight~maxHeight로 조정
            //Vector3 newPos = transform.position;
            Vector3 newPos = new Vector3(
                transform.position.x + Random.Range(minRightEnd, maxRightEnd),
                Random.Range(minHeight, maxHeight), 0.0f);
        }

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
    }
}
