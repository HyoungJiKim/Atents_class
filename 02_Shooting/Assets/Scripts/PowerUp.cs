using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Vector2 dir;
    float speed = 3.0f;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        Destroy(gameObject, 10f);
        dir = Random.insideUnitCircle.normalized;
        SetRandomDir();
        StartCoroutine(randomDir());
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * dir, Space.World);
    }

    IEnumerator randomDir()
    {
        while (true)
        {
            //Debug.Log(dir);
            yield return new WaitForSeconds(5f);
            SetRandomDir(false);
        }
    }
    void SetRandomDir(bool allRandom = true)
    {
        if (allRandom)
        {
            dir = Random.insideUnitCircle.normalized;
        }
        else
        {
            //플레이어의 위치에서 아이템 위치로 가는 방향벡터 계산
            Vector2 playerToPowerUp = transform.position - player.transform.position;
            playerToPowerUp = playerToPowerUp.normalized;
            if (Random.value < 0.6f)    //60% 확률로 플레이어 반대방향으로 이동
            {
                dir = Quaternion.Euler(0, 0, Random.Range(-90.0f, 90.0f)) * playerToPowerUp;
                //playerToPowerUp 벡터를 z축으로 -90~+90만큼 회전시켜서 dir에 넣기
            }
            else
            {
                dir = Quaternion.Euler(0, 0, Random.Range(-90.0f, 90.0f)) * -playerToPowerUp;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            //Debug.Log(dir);
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            
            //Debug.Log(dir);
        }
    }
}
