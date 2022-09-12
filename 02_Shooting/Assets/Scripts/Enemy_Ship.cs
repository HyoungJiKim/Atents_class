using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Ship : MonoBehaviour
{
    float speed = 3.0f;
    protected GameObject explosion;

    float spawnY;   //생성되었을 때의 기준 높이
    float timeElapsed;  //게임 시작부터 전체 플레이하고 있는 시간

    float amplitude = 1f;    //사인으로 변경되는 위아래 차이. 원래 sin은 -1~+1인데 그것을 변경
    public float frequency = 3f; //사인그래프가 한번 도는데 걸리는 시간
    public int score = 10;
    private System.Action<int> onDead;
    private void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        spawnY = transform.position.y;
        timeElapsed = 0.0f;
        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;
        //explosion.SetActive(false); //비활성화
        //Destroy(gameObject, 15.0f);
    }
    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime : 이전 프레임에서 현재 프레임까지의 시간
        timeElapsed += (Time.deltaTime * frequency);
        float newY = spawnY + Mathf.Sign(timeElapsed) * amplitude;    //0에서 1까지 증가하다 -1까지 감소
        float newX = transform.position.x - speed * Time.deltaTime;
        transform.position = new Vector3(newX, newY);

        //transform.Translate(speed * Time.deltaTime * Vector3.left, Space.Self);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            onDead?.Invoke(score);
            explosion.transform.parent = null;  //부모 해제
            explosion.SetActive(true);  //총에 맞았을 때 익스플로전을 활성화
            Destroy(this.gameObject);   
        }
    }

}
