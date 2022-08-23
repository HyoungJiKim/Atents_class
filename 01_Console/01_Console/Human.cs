using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Console
{
    public class Human : Character
    {
        int mp = 100;
        int maxMP = 100;
        public Human()// : base() //상속받은 부모의 생성자도 같이 실행
        {
            GenerateStatus();
        }

        public Human(string newName) : base(newName)
        {
            GenerateStatus();
        }

        //virtual
        //함수에게 붙일 수 있는 키워드
        //함수에 붙이면 그 함수는 가상함수가 된다.
        //가상함수
        //함수가 호출될 때 함수의 위치를 가상 테이블에 기록된 위치를 사용
        public override void GenerateStatus()
        {
            base.GenerateStatus();  //Character의 GenerateStatus 함수 실행
            maxMP = random.Next() % 100;    //추가한 변수만 추가로 처리
            mp = maxMP;
        }

        public void select_Action(Character target)
        {
            int action;
            string temp;
            temp = Console.ReadLine();
            int.TryParse(temp, out action);

            switch (action)
            {
                case 1: //공격
                    Attack(target);
                    break;
                case 2: //스킬
                    if (mp <= 20)
                    {
                        Console.WriteLine("MP가 모자랍니다.");
                        select_Action(target);
                    }
                    else
                    {
                        attack_Skill(target);
                    }
                    break;
                case 3: //방어
                    isSheild = true;
                    Console.WriteLine("방어 태세를 갖춥니다.");
                    break;
                default:
                    Console.WriteLine("커맨드를 다시 입력해주세요.");
                    select_Action(target);
                    break;
            }
        }

        public override void Attack(Character target)
        {
            int damage = strength;

            //random.NextDouble();//0.0~1.0
            if (random.NextDouble() < 0.3f) //이 조건이 참이면 30% 안쪽으로 들어왔다.
            {
                damage *= 2;
                Console.WriteLine("크리티컬 히트!");
            }

            Console.WriteLine($"{name}이 {target.Name}에게 공격을 합니다.(대미지 : {damage})");
            target.TakeDamange(damage);
        }

        void attack_Skill(Character target)
        {
            int damage = intellegence * 3;
            Console.WriteLine($"{name}이 {target.Name}에게 스킬을 사용합니다.(대미지 : {damage})");
            target.TakeDamange(damage);
            mp -= 20;
        }

        public void Skill(Character target)
        {
            //rand.NextDouble() : 0~1
            //rand.NextDouble() * 1.5f : 0~1.5
            //(((random.NextDouble() * 1.5f) + 1) : 1~2.5
            int damage = (int)(((random.NextDouble() * 1.5f) + 1) * intellegence); //지능을 1~2.5배한 결과에서 소수점 제거
        }

        public override void TestPrintStatus()
        {
            //base.TestPrintStatus();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine($"┃ 이름\t:{name,10}                                                ┃ ");
            Console.WriteLine($"┃ HP\t:{hp,4} / {maxHP,4}                                                  ┃ ");
            Console.WriteLine($"┃ MP\t:{mp,4} / {maxMP,4}                                                  ┃ ");
            Console.WriteLine($"┃ 힘\t:{strength,3}                                                          ┃ ");
            Console.WriteLine($"┃ 민첩\t:{dexterity,3}                                                          ┃ ");
            Console.WriteLine($"┃ 지능\t:{intellegence,3}                                                          ┃ ");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }


    }
}
