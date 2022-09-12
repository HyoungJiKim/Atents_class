using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //필요한 변수가 무엇인가 -> Enemy 프리팹, 지속적으로 동작을 하는 시간 간격
    public GameObject enemyPrefab;    //생성할 적의 프리팹
    public GameObject enemyPrefab2;
    protected float interval = 0.5f;   //생성할 시간 간격

    public float minY = -4.5f;
    public float maxY = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Create());  //코루틴 시작(종료는 없음)
    }

    protected virtual IEnumerator Create()
    {
        while (true)
        {
            GameObject obj;
            if (Random.value < 0.1f)
            {
                obj = Instantiate(enemyPrefab2, transform);
            }
            else
            {
                obj = Instantiate(enemyPrefab, transform);
            }
            //생성하고 부모를 이 오브젝트로 설정
            obj.transform.Translate(0, Random.Range(minY, maxY), 0);
            //GameObject obj = Instantiate(enemy, new Vector2(transform.position.x, Random.Range(minY, maxY)), Quaternion.identity);
            //interval 만큼 대기
            yield return new WaitForSeconds(interval);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(transform.position, new(1, Mathf.Abs(maxY) + Mathf.Abs(minY), 1));
    }

}
