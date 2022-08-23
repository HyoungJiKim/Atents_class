//using : 어떤 추가적인 기능을 사용할 것인지 표기
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace : 이름이 겹치는 것을 방지하기 귀해 구눕
namespace _01_Console
{
    //접근제한자 (Access Modifier)
    //public : 누구든지 접근
    //private : 자기자신만 접근
    //protected : 자신과 자신을 상속받은 자식만 접근
    //internal : 같은 어셈블리 안에서는 public 다른 어셈블리면 private
    public class Character   //Character 클래스를 public으로 선언
    {
        //멤버 변수 -> 이 클래스에서 사용된는 데이터
        protected string name;
        protected int hp = 100;
        protected int maxHP = 100;
        protected int strength = 10;
        protected int dexterity = 5;
        protected int intellegence = 7;

        protected bool isDead = false;
        protected bool isSheild = false;

        public string Name => name;
        public bool IsDead => isDead;
        //{
        //    get
        //    {
        //        return isDead;
        //    }
        //}

        //for(int i = 0; i< 100; i++)
        //{
        //    int ranNum = random.Next();
        //    //% : 앞의 숫자를 뒤의 숫자로 나눈 나머지 값을 돌려주는 연산자. 모듈레이트 연산
        //    // 10 % 3 하면 결과는 1
        //    //% 연산의 결과는 항상 0~(뒤 숫자-1)로 나온다.
        //    Console.WriteLine($"랜덤 넘버 : {ranNum}");
        //}


        //배열 : 같은 종류(데이터타입)의 데이터를 한번에 여러개 가지는 유형의 변수
        //int[] intArray; //인티저를 여러개 가질 수 있는 배열
        //intArray = new int[5];    //인티저를 5개 가질 수 있도록 할당

        //nameArray에 기본값 설정(선언과 할당을 동시에)
        string[] nameArray = { "너굴맨", "개굴맨", "ㅁㅁㅁ", "ㄷㄷㄷ", "ㅋㅋㅋ" };

        protected Random random;

        public int HP
        {
            get // 이 프로퍼티를 읽을 때 호출되는 부분. get만 만들면 읽기 전용 같은 효과가 있다.
            {
                return hp;
            }

            private set // 이 프로퍼티에 값을 넣을 때 호출되는 부분. set에 private을 붙이면 쓰는 것은 나만 가능하다.
            {
                hp = value;
                if (hp > maxHP)
                {
                    hp = maxHP;
                }
                if (hp <= 0)
                {
                    // 사망 처리용 함수 호출
                    Dead();
                }
            }
        }

        private void Dead()
        {
            isDead = true;
            Console.WriteLine($"{name}이 사망");
        }

        public Character()
        {
            //Console.WriteLine("생성자 호출");
            //name = "너굴맨";

            //실습
            //1. 이름이 nameArray에 들어있는 것 중 하나로 랜덤하게 선택
            //2. maxHP는 100~200 랜덤하게 선택
            //3. HP는 maxHP와 같은 값
            //4. strength, dexterity, intellegence 는 1~20 랜덤
            //5. TestPrintStatus 함수를 이용해서 최종상태로 출력

            random = new Random(DateTime.Now.Millisecond);
            //int ranNum = random.Next(0, nameArray.Length - 1);
            //name = nameArray[ranNum];
            int ranNum = random.Next(); //0~21억 사이의 숫자 랜덤
            //ranNum -> 0~4로 만들기
            name = nameArray[ranNum % 5];
            //GenerateStatus();
        }

        public Character(string newName)
        {
            //Console.WriteLine($"생성자 호출 - {newName}");
            random = new Random(DateTime.Now.Millisecond);
            //GenerateStatus();
            name = newName;
        }

        public virtual void GenerateStatus()
        {

            hp = maxHP = random.Next(100, 201);

            strength = random.Next(20) + 1;
            dexterity = random.Next(1, 21);
            intellegence = random.Next(1, 21);
        }
        //public void Setname(string newName)
        //{
        //    name = newName;
        //}

        //멤버 함수 -> 이 클래스에서 가지는 기능
        public virtual void Attack(Character target)
        {
            int damage = strength;
            Console.WriteLine($"{name}이 {target.name}에게 공격을 합니다.(대미지 : {damage})");
            target.TakeDamange(damage);
        }
        public virtual void TakeDamange(int damage)
        {
            if (isSheild)
            {
                Console.WriteLine($"{name}이 공격을 방어했습니다.");
                isSheild = false;
            }
            else
            {
                Console.WriteLine($"{name}이 {damage}만큼의 피해를 입었습니다.");
                HP -= damage;
            }
        }
        public virtual void TestPrintStatus()
        {
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine($"┃ 이름\t:{name,10}                                                ┃ ");
            Console.WriteLine($"┃ HP\t:{hp,4} / {maxHP,4}                                                  ┃ ");
            Console.WriteLine($"┃ 힘\t:{strength,3}                                                          ┃ ");
            Console.WriteLine($"┃ 민첩\t:{dexterity,3}                                                          ┃ ");
            Console.WriteLine($"┃ 지능\t:{intellegence,3}                                                          ┃ ");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }
    }
}
