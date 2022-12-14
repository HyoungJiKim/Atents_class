using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Console
{
    internal class Test
    {
        private static void Test_Human()
        {
            //Human h1 = new Human();
            //Character c1 = h1;  //상속받은 클래스는 부모 클래스 타입으로 저장가능
            //c1.TestPrintStatus();   //Character의 TestPrintStatus 호출(가상함수가 아니라서)
            //c1.GenerateStatus();    //Human의 GenerateStatus가 호출(가상함수이기 때문)

            //1. Human의 TestPrintStatus 함수를 오버라이드(원래 함수의 기능을 다른 기능으로 변경)(mp출력)
            //2. Human의 Attack 함수를 오버라이드
            //  2.1 Attack을 할 때 30%의 확률로 치명타(치명타는 데미지 2배)
            //h1.TestPrintStatus();
            Test_Battle();
        }

        private static void Test_Battle()
        {
            Human human1 = new Human();
            Human human2 = new Human();
            //Character human1 = new Character(); //할당완료(Instance화). 객체(Object)생성 완료(객체의 인스턴스를 만들었다)
            //Character human2 = new Character("웩져맨"); //Character 타입으로 하나 더 만듬. human1과 human2는 다른 개체

            //어느 한쪽이 죽을 때까지 반복
            //human1.TestPrintStatus();
            //human2.TestPrintStatus();
            while (!human1.IsDead && !human2.IsDead)
            {
                human1.Attack(human2);
                human1.TestPrintStatus();
                human2.TestPrintStatus();
                if (human2.IsDead   /*human2.HP <= 0 */) //human2가 죽으면 공격 멈춤
                {
                    break;
                }
                human2.Attack(human1);
                human1.TestPrintStatus();
                human2.TestPrintStatus();
            }
        }

        static void Test_Battle(Character human1, Character human2)
        {
            while (!human1.IsDead && !human2.IsDead)
            {
                human1.Attack(human2);
                human1.TestPrintStatus();
                human2.TestPrintStatus();
                if (human2.IsDead   /*human2.HP <= 0 */) //human2가 죽으면 공격 멈춤
                {
                    break;
                }
                human2.Attack(human1);
                human1.TestPrintStatus();
                human2.TestPrintStatus();
            }
        }
        private static void Test_Gugudan()
        {
            //실습
            //1. int 타입 파라메터를 하나 받아서 그 숫자에 해당하는 구구단을 출력하는 함수
            //2. 1번에서 만드는 함수는 2~9까지 입력이 들어오면 해당 구구단 출력. 그 외의 숫자는 잘못된 입력이라고 출력.
            //3. 메인 함수에서 숫자를 하나 입력받는 코드

            bool b1 = true;
            bool b2 = false;
            //논리연산자
            //&&(and) - 둘 다 참일 때만 참.
            //||(or) - 둘 중 하나만 참이면 참.
            //~(not) - true는 false, false는 true

            //예습 : 가위바위보 만들기
            Random r = new Random();
            r.Next();
            int a = 10 % 3; // % : 나머지 구하는 연산자 a=1

            string put = Console.ReadLine();
            practiceTest(put);
        }

        static void practiceTest(string put)
        {
            //<= 나 >= 는 두개의 조건이 결합된 것이므로 피하는 것이 좋다.
            int intput;
            int.TryParse(put, out intput);

            if (1 < intput && intput < 10)
            {
                for (int i = 1; i < 10; i++)
                {
                    Console.WriteLine($"{intput}*{i} = {intput * i}");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
        private static void Test_Function()
        {
            string name = "너굴맨";
            int level = 2;
            int hp = 10;
            int maxHP = 20;
            float exp = 0.1f;
            float maxExp = 1.0f;

            Print();
            PrintCharacter(name, level, hp, maxHP, exp, maxExp);
        }

        //Main 함수의 끝

        //함수의 구성요소
        //이름 : 함수들을 구분하기 위한 이름 (Sum)
        //리턴타입 : 함수의 실행 결과로 돌려주는 데이터의 타입 (int, 함수의 이름 앞에 표시된다.)
        //파라메터(매개변수) : 함수가 실행될 때 외부에서 받는 값 ((int a, int b) 두개의 파라메터가 있다. 함수 이름 뒤
        //함수바디 : 함수가 호출될 때 실행될 코드

        //함수의 이름, 리턴타입, 파라메터를 합쳐서 함수 프로토타입, 함수의 주민등록번호.
        //절대로 하나의 프로그램 안에서 겹치지 않음

        private static void PrintCharacter(string name, int level, int hp, int maxHP, float exp, float maxExp)
        {
            //실습 : 파라메터로 받은 데이터를 적당한 양식으로 출력
            Console.WriteLine($"이름\t : {name}\n레벨\t : {level}\nHP\t : {hp,3}/{maxHP,3}\nexp:{exp:f3}/{maxExp:f3}");
        }
        static int Sum(int a, int b)
        {
            int result = a + b;
            return result;
        }

        static void Print()    //리턴해주는 값이 없고, 파라메터도 없는 경우
        {
            Console.WriteLine("Print");
        }

        void Test_Code()
        {
            Console.WriteLine("Hello World!");  //Hello World 를 출력하는 코드
            Console.WriteLine("22.08.16 김형지");//출력
            string str = Console.ReadLine();     //키보드 입력을 받아서 str이라는 string 변수에 저장한다.

            //변수 : 변하는 숫자. 컴퓨터에서 사용할 데이터를 저장할 수 있는 곳.
            //변수의 종류 : 데이터 타입(Data type)
            //int  : 인티저. 정수. 소수점 없는 숫자. 32bit
            //float : 플로트. 실수. 소수점 있는 숫자. 32bit
            //string : 스트링. 문자열. 글자들을 저장. 
            //bool : 불 or 불리언. true/false를 저장. 

            int a = 10; //a라는 인티저 변수에 10이라는 데이터를 넣는다.
            long b = 5000000000; //50억은 int에 넣을 수 없다. => int는 32비트이고 32비트로 표현가능한 숫자의 갯수는 2^32
            int c = -100;
            int d = 2000000000;
            int e = 2000000000;
            int f = d + e;
            Console.WriteLine(f);

            //float의 단점 : 태생적으로 오차가 있을 수 밖에 없다.
            float aa = 0.000123f;
            float ab = 0.9999999999999f;
            float ac = 0.0000000000001f;
            float ad = ab + ac;//결과가 1이 아닐 수도 있다.
            Console.WriteLine(ad);

            string str1 = "Hello";
            string str2 = "안녕!";
            string str3 = $"Hello{a}";//Hello 10
            Console.WriteLine(str3);
            string str4 = str1 + str2;//Hello 안녕!
            Console.WriteLine(str4);

            bool b1 = true;
            bool b2 = false;

            int level = 1;
            int hp = 10;
            float exp = 0.9f;
            string name = "너굴맨";

            //너굴맨의 레벨은 1이고 hp는 10이고 exp는 0.9다
            string str5 = $"{name}의 레벨은 {level}이고 hp는 {hp}이고 exp는 {exp}다";
            Console.WriteLine(str5);

            Console.Write("이름을 입력하세요 : ");
            name = Console.ReadLine();
            Console.Write($"{name}의 레벨을 입력하세요 : ");
            string temp = Console.ReadLine();
            //level = int.Parse(temp);//문자열을 인티져로 변경해주는 코드(숫자로 변경 가능할 때만). 간단하지만 위험
            //level = Convert.ToInt32(temp);//string을 int로 변경해주는 코드(숫자로 변경 불가능하면 0으로). 더 세세하게 변경가능
            int.TryParse(temp, out level);//string을 int로 변경해주는 코드(숫자로 변경 불가능하면 0으로)
            Console.WriteLine($"이름 : {name}\n레벨 : {level}\nHP : {hp}\nexp:{exp}");

            exp = 0.959595f;
            //너굴맨의 레벨은 1이고 HP는 10이고 exp는 90%다.
            Console.WriteLine($"{name}의 레벨은 {level}이고 HP는 {hp}이고 exp는 {exp * 100:F3}%다.");
            Console.WriteLine(str);

            //실습 : 이름, 레벨, hp, 경험치를 각각 입력 받고 출력하는 코드 만들기
            Console.Write("이름을 입력하세요 : ");
            name = Console.ReadLine();

            Console.Write($"{name}의 레벨을 입력하세요 : ");
            string tempLv = Console.ReadLine();
            int.TryParse(tempLv, out level);

            Console.Write($"{name}의 HP를 입력하세요 : ");
            string tempHp = Console.ReadLine();
            int.TryParse(tempHp, out hp);

            Console.Write($"{name}의 exp를 입력하세요 : ");
            string tempexp = Console.ReadLine();
            float.TryParse(tempexp, out exp);

            str5 = $"{name}의 레벨은 {level}이고 HP는 {hp}이고 exp는 {exp * 100:F3}%다.";
            Console.WriteLine(str5);

            //변수 끝---------------------------------------------------------------------------

            //제어문(Control statement)
            //실행되는 코드 라인을 변경할 수 있는 코드
            if (hp < 3)    //참이라면 중괄호 사이에 코드가 실행된다.
            {
                Console.WriteLine("hp가 부족합니다.");    //(hp<3) 참일 때 실행되는 코드
            }
            else if (hp < 10)
            {
                Console.WriteLine("hp가 적당합니다.");    //(hp<3)는 거짓이고 (hp<10)는 참일 때
            }
            else
            {
                Console.WriteLine("hp가 충분합니다.");    //(hp<3)와 (hp<10)이 거짓일 때 실행되는 코드
            }

            switch (hp)
            {
                case 0: //hp가 0일 때
                    Console.WriteLine("HP가 0입니다.");
                    break;
                case 5: //hp가 5일 때
                    Console.WriteLine("HP가 5입니다.");
                    break;
                default:    //위에 설정되지 않은 모든 경우
                    Console.WriteLine("HP가 0과 5가 아닙니다.");
                    break;
            }

            Console.WriteLine("경험치를 추가합니다.");
            Console.Write("추가할 경험치 : ");
            string tempexp2 = Console.ReadLine();
            float exp2;
            float.TryParse(tempexp2, out exp2);
            //실습 : exp의 값과 추가로 입력받은 경험치의 합이 1 이상이면 "레벨업"이라고 출력하고 1미만이면 합계를 출력
            if (exp + exp2 >= 1.0f)
            {
                Console.WriteLine("레벨업");
            }
            else
            {
                Console.WriteLine($"현재 경험치 : {exp + exp2}");
            }

            while (level < 3) //소괄호() 안의 조건이 참이면 중괄호{} 사이의 코드를 실행하는 statement
            {
                Console.WriteLine($"현재 레벨 : {level}");
                level++;    //level = level + 1;    level += 1; //셋 다 같은 코드
                //level += 2;   //level에 2를 더해서 레벨에 넣어라
            }

            for (int i = 0; i < 3; i++)    //i는 0에서 시작해서 3보다 작으면 계속 {}사이의 코드를 실행한다
                                           //i는 {} 사이의 코드를 실행할 때마다 1씩 증가한다.
            {
                Console.WriteLine($"현재 HP : {hp}");
                hp += 10;
            }
            Console.WriteLine($"최종 HP : {hp}");

            do
            {
                Console.WriteLine($"현재 레벨 : {level}");
                level++;

                if (level == 2) //1+i == 2 ......... ==은 양쪽이 같다라는 의미
                {
                    break;
                }
            }
            while (level < 10);
            Console.WriteLine($"최종 Level : {level}");

            //실습 : exp가 1을 넘어 레벨업을 할 때까지 계속 추가 경험치를 입력
            float exp_test = 0.0f;

            while (exp_test < 1.0f) //exp 값이 1보다 작으면 계속 반복
            {
                Console.WriteLine($"경험치를 추가합니다. 현재 경험치 : {exp_test}");
                Console.Write("추가할 경험치 : ");
                string temp_exp = Console.ReadLine();   //입력 받기
                float exp_test2;
                float.TryParse(temp_exp, out exp_test2);    //입력 받은 strubg을 float로 변경
                exp_test += exp_test2;  //입력 받은 값을 exp에 추가
            }
            //while이 끝났다는 이야기는 exp가 1 이상이라는 의미
            Console.WriteLine("레벨업");



        }

    }
}
