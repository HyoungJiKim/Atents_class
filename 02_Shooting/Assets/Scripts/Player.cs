using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class Player : MonoBehaviour
{
    //public delegate void DelegateName();    //이런 종류의 델리게이트가 있다(리턴, 파라메터 없는 함수를 저장하는 델리게이트
    //public DelegateName del;    //DelegateName 타입으로 del이라는 이름의 델리게이트 만듬
    //Action del2;    //리턴타입이 void, 파라메터도 없는 델리게이트 del2를 만듬
    //Action<int> del3;   //리턴타입이 void, 파라메터는 int 하나인 델리게이트
    //Func<int, float> del4;  //리턴타입이 int고, 파라메터는 float 하나인 델리게이트 del4를 만듬

    Player_InputAction inputActions;
    float speed = 3.0f;
    float boost = 1.0f;
    Rigidbody2D rigid;
    Vector3 dir;
    Animator anim;
    public GameObject bullet;
    //public GameObject enemy;
    //bool flag = false;
    //bool isFire = false;
    float fireInterval = 0.5f;
    //float fireTimeCount = 0.0f;

    //Transform[] fireposition;
    //public GameObject flash;
    Transform firePositionRoot;
    GameObject flash;

    float fireAngle = 30.0f;
    int power = 0;
    int powerUpBonus;

    public GameObject explosion;
    bool isDead = false;

    int Power
    {
        get => power;
        set
        {
            power = value;
            if (power > 3)
            {
                AddScore(powerUpBonus);
            }
            //if (power > 3)
            //{
            //    power = 3;
            //}
            //if (power < 1)
            //{
            //    power = 1;
            //}
            power = Mathf.Clamp(power, 1, 3);
            while (firePositionRoot.childCount > 0)
            {
                Transform temp = firePositionRoot.GetChild(0);
                temp.parent = null;
                Destroy(temp.gameObject);
            }

            for(int i = 0; i < power; i++)
            {
                GameObject firePos = new GameObject();
                firePos.name = $"FirePosition_{i}";
                firePos.transform.parent = firePositionRoot;
                firePos.transform.localPosition = Vector3.zero; //아랫줄과 같은 기능
                //firePos.transform.position = firePositionRoot.transform.position;

                firePos.transform.rotation = Quaternion.Euler(0, 0, (power - 1) * (fireAngle * 0.5f) + i * -fireAngle);
                firePos.transform.Translate(1.0f, 0, 0);
            }
        }
    }

    IEnumerator fireCoroutine;

    public int initialLife = 3;
    int life;
    public Action<int> onLifeChange;
    int Life
    {
        //get
        //{
        //    return life;
        //}
        get => life;
        set
        {
            if (life != value)  //값에 변경이 일어남
            {
                if (life > value)
                {
                    //life 감소한 상황(새로운 값(value)이 옛날 값(life)보다 작다=>감소했다)
                    StartCoroutine(EnterInvincibleMode());
                }
                life = value;
                if (life <= 0)
                {
                    Dead();
                }
                //(변수명)?. : 왼쪽 변수가 null이면 null. 아니면 (변수명)멤버에 접근
                onLifeChange?.Invoke(life); 
                //라이프가 변경될 때 onLifeChange 델리게이트에 등록된 함수들을 실행시킨다.
            }
        }
    }
    const float InvincibleTime = 1.0f;
    Collider2D bodyCollider;
    SpriteRenderer spriteRenderer;
    bool isInvincibleMode = false;
    float timeElapsed = 0.0f;

    public int totalScore = 0;
    public Action<int> onScoreChange;

    //Awake->OnEnable->Start
    //이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호출
    private void Awake()
    {
        inputActions = new Player_InputAction();
        rigid = GetComponent<Rigidbody2D>();    //한번만 찾고 저장해서 계속 쓰기(메모리 더 쓰고 성능아끼기)
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //fireposition = new Transform[transform.childCount - 1];
        //for (int i = 0; i < transform.childCount - 1; i++)
        //{
        //    fireposition[i] = transform.GetChild(i);
        //}
        //flash = transform.GetChild(transform.childCount - 1).gameObject;
        firePositionRoot = transform.GetChild(0);
        flash = transform.GetChild(1).gameObject;
        flash.SetActive(false);
        fireCoroutine = Fire();

    }

        //이 스크립트가 들어있는 게임 오브젝트가 활성화 되었을 때 호출
    private void OnEnable()
    {
        inputActions.Player.Enable();   //오브젝트가 생성되면 입력을 받도록 활성화
        inputActions.Player.Move.performed += OnMove;   // Move 액션이 performed 일 때 OnMove 함수 실행하도록 연결
        inputActions.Player.Move.canceled += OnMove;    // Move 액션이 canceled 일 때 OnMove 함수 실행하도록 연결
        inputActions.Player.Fire.performed += OnFireStart;
        inputActions.Player.Fire.canceled += OnFireStop;
        inputActions.Player.AddSpeed.performed += OnSpeedOn;
        inputActions.Player.AddSpeed.canceled += OnSpeedOff;
    }
    private void OnDisable()
    {
        InputDisable();
    }

    void InputDisable()
    {
        inputActions.Player.AddSpeed.canceled -= OnSpeedOn;
        inputActions.Player.AddSpeed.performed -= OnSpeedOff;
        inputActions.Player.Fire.canceled -= OnFireStop;
        inputActions.Player.Fire.performed -= OnFireStart;
        inputActions.Player.Move.canceled -= OnMove;    // 연결해 놓은 함수 해제(안전을 위해)
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Exception : 예외 상황( 무엇을 해야 할지 지정이 안되어있는 예외 일때 )
        //throw new NotImplementedException();    // NotImplementedException 을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽이는 코드
        //Debug.Log("이동 입력");
        dir = context.ReadValue<Vector2>();

        //dir.y > 0 //W를 누름
        //dir.y == 0    //W,S 중 아무것도 안눌렀다.
        //dir.y < 0 //S를 눌렀다.
        anim.SetFloat("InputY", dir.y);
    }

    private void OnFireStart(InputAction.CallbackContext _)
    {
        //float value = UnityEngine.Random.Range(0.0f, 10.0f);    //value에는 0.0~10.0의 랜덤값이 들어간다.
        //Instantiate(bullet, transform.position, Quaternion.identity);
        //isFire = true;
        StartCoroutine(fireCoroutine);
    }
    private void OnFireStop(InputAction.CallbackContext _)
    {
        //Instantiate(bullet, transform.position, Quaternion.identity);
        //isFire = false;
        StopCoroutine(fireCoroutine);
    }

    IEnumerator Fire()
    {
        //yield return null;  //다음 프레임에 이어서 시작해라
        //yield return new WaitForSeconds(1.0f);  //1초 후에 이어서 실행

        while (true)
        {
            //for(int i = 0; i < fireposition.Length; i++)
            //{
            //    Instantiate(bullet, fireposition[i].position, fireposition[i].rotation);
            //    //Instantiate(생성할 프리팹); //프리팹이(0,0,0)위치에 (0,0,0)회전에 (1,1,1) 크기로 만들어짐
            //    //Instantiate(생성할 프리팹, 생성할 위치, 생성될 때의 회전)
            //    //Vector3 angle = fireposition[i].rotation.eulerAngles;   //현재 회전 값을 각각 몇도씩 회전했는지 확인 가능
            //}
            for(int i = 0; i < firePositionRoot.childCount; i++)
            {
                // bullet이라는 프리팹을 firePosition[i]의 위치에 (0,0,0) 회전으로 만들어라
                //GameObject bulletInstance = Instantiate(bullet, firePosition[i].position, Quaternion.identity);

                // bullet이라는 프리팹을 firePosition[i]의 위치에 firePosition[i]의 회전으로 만들어라
                Instantiate(bullet, firePositionRoot.GetChild(i).position, firePositionRoot.GetChild(i).rotation);
            }
            flash.SetActive(true);
            StartCoroutine(FlashControll());
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator FlashControll()
    {
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }


    private void OnSpeedOn(InputAction.CallbackContext context)
    {
        //Debug.Log("가속");
        boost *= 2.0f;
    }

    private void OnSpeedOff(InputAction.CallbackContext context)
    {
        boost = 1.0f;
    }

    //이 스크립트가 들어있는 게임 오브젝트가 비활성화 되었을 때 호출

    // Start is called before the first frame update
    void Start()
    {
        //dropEnemy();
        Power = 1;
        Life = initialLife;
        totalScore = 0;     // 점수 초기화
        AddScore(0);        // UI 갱신용
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += speed * Time.deltaTime * dir;
        //transform.Translate(speed * Time.deltaTime * dir);
        if (isInvincibleMode)
        {
            timeElapsed += Time.deltaTime * 30.0f;
            float alpha = (Mathf.Cos(timeElapsed) + 1.0f) * 0.5f;   //cos결과를 1~0으로 변경
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }

    private void FixedUpdate()  //일정 시간(물리 업데이트 시간) 간격으로 호출
    {
        //transform.Translate(speed * Time.fixedDeltaTime * dir);
        //이 스크립트 파일이 들어있는 오브젝트에서 컴포넌트를 찾아 리턴 없으면 null
        //그러나 GetComponent는 무거운 함수 => (Update나 FixedUpdate처럼 주기적 또는 자주 호출되는 함수에서는 권장x)
        //Rigidbody2D rigid = GetComponent<Rigidbody2D>();    
        //rigid.AddForce(speed * Time.fixedDeltaTime * dir);  //관성이 있는 움직임

        if (!isDead)
        {
            rigid.MovePosition(transform.position + speed * boost * Time.fixedDeltaTime * dir);
        }
        else
        {
            rigid.AddForce(Vector2.left * 0.1f, ForceMode2D.Impulse);
            rigid.AddTorque(10.0f);
        }

        //fireTimeCount += Time.fixedDeltaTime;
        //if (isFire && fireTimeCount > fireInterval)
        //{
        //    Instantiate(bullet, transform.position, Quaternion.identity);
        //    fireInterval = 0.0f;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Power++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Life--;
        }

        //Debug.Log("OnCollisionEnter2D");    //Collider와 부딪혔을 때
    }

    IEnumerator EnterInvincibleMode()
    {
        //bodyCollider.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        isInvincibleMode = true;
        timeElapsed = 0.0f;

        yield return new WaitForSeconds(InvincibleTime);

        spriteRenderer.color = Color.white;
        isInvincibleMode = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        //bodyCollider.enabled = !isDead;
    }

    void Dead()
    {
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
        bodyCollider.enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        InputDisable();
        rigid.gravityScale = 1.0f;
        rigid.freezeRotation = false;
        StopCoroutine(fireCoroutine);
    }


    public void AddScore(int score)
    {
        totalScore += score; 
        onScoreChange?.Invoke(totalScore);

        // 1. 이벤트가 발생하는 곳(델리게이트 작성) -> 신호만 보내기
        // 2. 실제 액션이 일어나는 곳(델리게이트에 함수 등록)
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionStay2D");    //Collider와 계속 접촉하고 있을 때(매 프레임)
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionExit2D");    //Collider와 접촉이 떨어지는 순간
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log("OnTriggerEnter2D");    //트리거와 부딪혔을 때
    //    //flag = true;
    //    //Debug.Log("Game Over");
    //    if (collision.CompareTag("PowerUp"))
    //    {
    //        Power++;
    //        Destroy(collision.gameObject);
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log("OnTriggerStay2D");    //트리거와 계속 겹쳐있을 때(매 프레임)
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("OnTriggerExit2D");    //트리거에서 나갔을 때
    //}

    //void dropEnemy()
    //{
    //    if (!flag)
    //    {
    //        float position = UnityEngine.Random.Range(-8.0f, 8.0f);
    //        Instantiate(enemy, new Vector2(position, 6.0f), Quaternion.identity);
    //        Invoke("dropEnemy", 0.5f);
    //    }
    //}

}
