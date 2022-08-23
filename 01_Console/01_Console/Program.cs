using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Console
{
    internal class Program
    {
        //스코프(Scope) : 변수나 함수를 사용할 수 있는 범위. 변수를 선언한 시점에서 해당 변수가 포함된 중괄호가 끝나는 지점까지.
        static void Main(string[] args)
        {
            //int sumResult = Sum(10, 20);    //break point 단축기F9
            //Console.WriteLine($"SumResult : {sumResult}");
            //Test_Function();
            //Test_Gugudan();
            //Test_Battle();
            //Test_Human();

            Practice_Battle();

            Console.ReadKey();                  //키 입력 대기


        }
        private static void Practice_Battle()
        {
            Human player;
            string result;

            do
            {
                Console.Write("당신의 이름을 입력해주세요 : ");
                string name = Console.ReadLine();
                player = new Human(name);
                Console.Write($"이대로 진행하시겠습니까? (Y/N) : ");
                result = Console.ReadLine();
            }
            //while (!(result == "Y" || result == "y" || result == "N" || result == "n"))
            while (result != "Y" && result != "y" && result != "N" && result != "n");

            Orc enemy = new Orc("오크");

            Console.WriteLine("오크가 나타났다.");
            Console.WriteLine("\n\n------------------전투시작------------------");
            while (!player.IsDead && !enemy.IsDead)
            {
                Console.WriteLine("행동을 입력해주세요.\n1)공격 2)스킬 3)방어");
                player.select_Action(enemy);
                player.TestPrintStatus();
                enemy.TestPrintStatus();
                if (enemy.IsDead)
                {
                    break;
                }
                enemy.Attack(player);
                player.TestPrintStatus();
                enemy.TestPrintStatus();
            }

        }


    }
}
