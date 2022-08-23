using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Console
{
    //8.19
    //1. Orc 클래스 완성하기
    //  1.1 오크만의 변수 추가
    //  1.2 오크만의 함수 추가
    //2. Human 클래스 수정하기
    //  2.1 스킬 함수 만들기
    //3. 입력에 따라 행동하는 player 만들기
    //  3.1 "1)공격 2)스킬 3)방어" 3개 중 하나를 입력받아 그대로 행동하게 만들기
    //4. player와 enemy가 싸우게 만들기
    internal class Orc : Character
    {
        int rage = 0;
        int maxRage = 5;

        //const int rageMax = 100;  //상수로 설정. 시작부터 끝까지 값이 변하지 않는다.
        //bool berserker = false;

        public Orc(string _name) : base(_name)
        {
            name = _name;
            GenerateStatus();
            rage = 0;
        }

        public override void GenerateStatus()
        {
            base.GenerateStatus();  //Character의 GenerateStatus 함수 실행
        }

        public override void Attack(Character target)
        {
            int damage = strength;
            Console.WriteLine($"{name}이 {target.Name}에게 공격을 합니다.");

            if (random.NextDouble() < 0.5f) //50% 확률로 빗나감
            {
                Console.WriteLine("Miss!");
                charge_Rage(target);  //빗나가면 분노 게이지 채우기
            }
            else
            {
                Console.WriteLine($"(대미지 : {damage})");
                target.TakeDamange(damage);
            }
        }

        void charge_Rage(Character target)
        {
            rage++;
            if (rage >= maxRage)    //분노 게이지가 맥스 이상이면 필중 강공격
            {
                int damage = strength;
                damage *= 5;
                Console.WriteLine($"{name}이 분노합니다. {target.Name}에게 필살공격을 합니다.(대미지 : {damage})");
                target.TakeDamange(damage);
                rage = 0;
            }
        }

        public override void TestPrintStatus()
        {
            //base.TestPrintStatus();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine($"┃ 이름\t:{name,10}                                                 ┃ ");
            Console.WriteLine($"┃ HP\t:{hp,4} / {maxHP,4}                                                  ┃ ");
            Console.WriteLine($"┃ Rage\t:{rage,4} / {maxRage,4}                                                  ┃ ");
            Console.WriteLine($"┃ 힘\t:{strength,3}                                                          ┃ ");
            Console.WriteLine($"┃ 민첩\t:{dexterity,3}                                                          ┃ ");
            Console.WriteLine($"┃ 지능\t:{intellegence,3}                                                          ┃ ");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }

        //void BerserkerMode(bool on)
        //{
        //    berserker = on;
        //    if (berserker)
        //    {
        //        strength *= 2;
        //    }
        //    else
        //    {
        //        strength = strength >> 1;   //절반으로 줄이기
        //    }
        //}

        //public override void TakeDamange(int damage)
        //{
        //    //맞을 때마다 최대 분노의 1/10 증가 + 데미지 10당 1씩 증가
        //    rage += (int)(rageMax * 0.1f + damage * 0.1f);
        //    if(rage >= rageMax)
        //    {
        //      BerserkerMode(true);
        //    }
        //    base.TakeDamange(damage);
        //}

    }
}
