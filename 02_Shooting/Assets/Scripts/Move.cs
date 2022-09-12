using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    float speed = 1.0f;
    Vector3 vector;


    //유니티 이벤트 함수 : 유니티가 특정 타이밍에 실행시키는 함수
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello Unity");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 : 벡터를 표현하기 위한 구조체, 위치를 표현할 때도 많이 사용
        //벡터 : 힘의 방향과 크기를 나타내는 단위
        //transform.position += new Vector3(1, 0, 0);

        //new Vector3(1, 0, 0);   //오른쪽, Vector3.right;
        //new Vector3(-1, 0, 0);   //왼쪽, Vector3.left;
        //new Vector3(0, 1, 0);   //위쪽, Vector3.up;
        //new Vector3(0, -1, 0);   //아래쪽, Vector3.down;

        //Time.deltaTime : 이전 프레임에서 지금 프레임까지 걸린 시간 => 1프레임당 걸린 시간
        //transform.position += (speed * Time.deltaTime * Vector3.up); //매 프레임마다 위쪽 방향으로 speed만큼 움직여라

        //Test_InputManager();
        transform.position += (speed * Time.deltaTime * vector);

        //Input System
        //Event-driven(이벤트 드리븐) 방식으로 구현->일이 있을 때만 동작(전력 절약에 적합)
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        //if (context.started)    //매핑된 키가 눌린 직후
        //{
        //    Debug.Log("입력들어옴 - started");
        //}
        //if (context.performed)  //매핑된 키가 확실하게 눌려졌다.
        //{
        //    inputDir = context.ReadValue<Vector2>();
        //    Debug.Log(inputDir);
        //    vector = inputDir;  //dir.x = inputdir.x; dir.y = inputdir.y; dir.z = 0.0f;
        //    Debug.Log("입력들어옴 - performed");
        //}
        //if (context.canceled)   //매핑된 키가 떨어졌을 때
        //{
        //    vector = inputDir = new Vector2(0, 0);
        //    Debug.Log("입력들어옴 - canceled");
        //}

        Vector2 inputDir = context.ReadValue<Vector2>();
        vector = inputDir;  //dir.x = inputdir.x; dir.y = inputdir.y; dir.z = 0.0f;


        //vector : 방향과 크기
        //Vector2 : 유니티에서 제공하는 구조체(struct). 2차원 벡터를 표현하기 위한 구조체(x,y)
        //Vector3 : 유니티에서 제공하는 구조체(struct). 3차원 벡터를 표현하기 위한 구조체(x,y,z)
    }

    public void FireInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("발사");
        }
    }


    //private void Test_InputManager()
    //{
    //    //Input Manager를 이용한 입력처리
    //    //Busy wait이 발생(하는 일은 없지만 사용되고 있는 상태)
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        vector = Vector3.up;
    //        Debug.Log("W");
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        vector = Vector3.left;
    //        Debug.Log("A");
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        vector = Vector3.down;
    //        Debug.Log("S");
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        vector = Vector3.right;
    //        Debug.Log("D");
    //    }
    //}
}
