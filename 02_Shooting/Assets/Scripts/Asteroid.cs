using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    float moveSpeed = 2.0f;
    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 4.0f;
    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 360.0f;
    public int score = 50;
    private System.Action<int> onDead;

    public GameObject small;
    [Range(1,16)]
    int splitCount = 3;
    bool crushFlag = false;

    public Vector3 dir = Vector3.left;
    public int hitPoint = 3;

    GameObject explosion;

    private void Awake()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = true;
        sprite.flipY = false;

        //int rand = Random.Range(0, 100) % 2;
        //sprite.flipX = (rand == 0);
        int rand = Random.Range(0, 4);  //0(0b_00), 1(0b_01), 2(0b_10), 3(0b_11)

        //rand & 0b_01 : rand의 제일 오른쪽 비트가 0인지 1인지 확인하는 작업
        //((rand & 0b_01) != 0) : rand의 제일 오른쪽 비트가 1이면 true, 0이면 false
        sprite.flipX = ((rand & 0b_01) != 0);

        //((rand & 0b_10) != 0) : rand의 제일 오른쪽에서 두번째 비트가 1이면 true, 0이면 false
        sprite.flipY = ((rand & 0b_10) != 0);

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        float ratio = (moveSpeed - minMoveSpeed) / (maxMoveSpeed / minMoveSpeed);
        rotateSpeed = ratio * (maxRotateSpeed - minRotateSpeed) + minRotateSpeed;

        //Mathf.Lerp(minRotateSpeed, maxRotateSpeed, ratio);

    }
    private void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;
        StartCoroutine(destroyAsteroid());
    }
    // Update is called once per frame
    void Update()
    {
        //transform.rotation *= Quaternion.Euler(new(0, 0, 90));  //계속 90도씩 회전
        //transform.rotation *= Quaternion.Euler(new(0, 0, rotateSpeed*Time.deltaTime));  //1초에 360도씩 회전
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);   //Vector3.forward를 기준으로 1초에 rotateSpeed만큼 회전
        transform.Translate(moveSpeed * Time.deltaTime * dir, Space.World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + dir * 5.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitPoint--;
            if (hitPoint <= 0 && !crushFlag)
            {
                onDead?.Invoke(score);
                Crush();
            }
        }
    }
    void Crush()
    {
        crushFlag = true;
        explosion.transform.parent = null;  //부모 해제
        explosion.SetActive(true);  //총에 맞았을 때 익스플로전을 활성화

        float randTest = Random.Range(0.0f, 1.0f);

        if (randTest < 0.05f)   //5%의 확률
        {
            splitCount = 20;
        }
        else if (randTest < 0.15f)  //10%의 확률
        {
            splitCount = 10;
        }
        else
        {
            splitCount = Random.Range(3, 6);
        }

        float angleGap = 360.0f / (float)splitCount;    //작은 운석들 사이각
        float gap = Random.Range(0f, angleGap); //운석 방향 변화용

        for (int i = 0; i < splitCount; i++)
        {
            Instantiate(small, transform.position, Quaternion.Euler(0, 0, angleGap * i + gap));
        }

        Destroy(this.gameObject);

    }

    IEnumerator destroyAsteroid()
    {
        float ranTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(ranTime);
        if (!crushFlag)
        {
            Crush();
        }
    }

}
