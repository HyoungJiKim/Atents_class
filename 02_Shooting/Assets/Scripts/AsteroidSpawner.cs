using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : EnemySpawner
{
    Transform destination;

    private void Awake()
    {
        //오브젝트가 생성된 직후
        //이 오브젝트 안에 있는 모든 컴포넌트가 생성이 완료되었다.
        //그리고 이 오브젝트의 자식오브젝트들도 모두 생성이 완료되었다.
        //destination = transform.Find("DestinationArea");    //"DestinationArea"라는 이름을 가진 자식 찾기
        destination = transform.GetChild(0);
    }

    //private void Start()
    //{
    //    //첫번째 업데이트 실행 직전 호출
    //    //나와 다른 오브젝트를 가져와야 할 때 사용
    //}
    protected override IEnumerator Create()
    {
        while (true)
        {
            GameObject obj = Instantiate(enemyPrefab, transform);
            obj.transform.Translate(0, Random.Range(minY, maxY), 0);
            Vector3 destPosition = destination.position + new Vector3(0, Random.Range(minY, maxY), 0);
            Asteroid asteroid = obj.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                //운석이 destPosition으로 가는 방향벡터를 구하고
                //dir를 방향벡터로 만들어준다
                asteroid.dir = (destPosition - asteroid.transform.position).normalized;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new(1, Mathf.Abs(maxY) + Mathf.Abs(minY), 1));

        if (destination == null)
        {
            destination = transform.GetChild(0);    //첫번째 자식 찾기
        }
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(destination.position, new(1, Mathf.Abs(maxY) + Mathf.Abs(minY), 1));
    }
}
