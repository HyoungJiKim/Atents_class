using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject hit;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
        //Space.Self : 자기기준, Space.World : 씬 기준
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //1. CompareTag는 숫자와 숫자를 비교하지만 == 은 문자열 비교이기에 좋지 않음
        //collision.gameObject.tag == "Enemy"
        if(collision.gameObject.CompareTag("Enemy"))
        {
            hit.transform.parent = null;
            hit.transform.position = collision.contacts[0].point;
            hit.gameObject.SetActive(true);
            //Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
