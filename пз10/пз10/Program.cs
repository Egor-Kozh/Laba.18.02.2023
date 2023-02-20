/* (описываем институт)
 * 1)класс студентов   2)класс преподавателей   3)класс управленцев    4)класс вспомогательный
 * преподователи принимает экз у студентов 
 * управленцы создают указы для студентов и преподователей
 * вспомогательные делают запросы 
 * 1) список приказов 1.1)приказы для преподователей 1.2)для студентов 1.3) для всех (+ кто издал это указ)
 * 2) перечень преподователей у кого есть студенты должники по их предмету и список этих студентов 
 * преподователи ведут несколько предметов
 * 3) по указанному студенту вывести его долги и преподователя кто их принимает
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace hey
{
    class Programm
    {
        static void Main()
        {
            List<Stud> stud = new List<Stud>();
            List<Prepod> prepod = new List<Prepod>();
            List<Glav> glav = new List<Glav>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==Университет==\n\n1) Заполнение списков\n2) Функционал\n");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1) { Zapoln(ref stud, ref prepod, ref glav); }
                else if (key.Key == ConsoleKey.D2) { Func(ref stud, ref prepod, ref glav); }
            }
        }
        static void Zapoln(ref List<Stud> stud, ref List<Prepod> prepod, ref List<Glav> glav)
        {
            Console.Clear();
            Console.WriteLine("==Заполнение списков==\n\n1) Заполнение студентов\n2) Заполнение преподователей\n" +
                "3) Заполнение руководителей");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.D1) { AddStud(ref stud); }
            else if (key.Key == ConsoleKey.D2) { AddPrepod(ref prepod); }
            else if (key.Key == ConsoleKey.D3) { AddGlav(ref glav); }
        }
        static void Func(ref List<Stud> stud, ref List<Prepod> prepod, ref List<Glav> glav)
        {
            Console.Clear();
            Console.WriteLine("==Функции==\n\n1) Список приказов для...\n2) Перечень преподователей у которых есть должники по их предмету\n" +
                "3) Студент должник, его долги и преподователь который принимает его предмет");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.D1) { Ykazi(ref glav); }
            else if (key.Key == ConsoleKey.D2) { ListPrepod(ref prepod, ref stud); }
            else if (key.Key == ConsoleKey.D3) { DolgStud(ref prepod, ref stud); }
        }

        static void DolgStud(ref List<Prepod> prepod, ref List<Stud> stud)
        {
            Console.Clear();
            Console.WriteLine("==Должники==\n\nУкажите студента должника\n");
            string name = Console.ReadLine();
            string group = "";
            bool flag = false;
            List<string> cashe2 = new List<string>();
            for (int i = 0; i < stud.Count; i++)
            {
                if (stud[i].FIO == name)
                {
                    flag = true;
                    name = stud[i].FIO;
                    group = stud[i].Group;
                    for (int j = 0; j < stud[i].Subj.Count; j++)
                    {
                        if (stud[i].Score[j] < 3)
                        {
                            cashe2.Add(stud[i].Subj[j]);
                        }
                    }
                    break;
                }
            }
            if (flag == false) { Console.WriteLine("Данного студента нет в списке"); Console.ReadKey(); }
            else if(flag == true && cashe2.Count == 0) { Console.WriteLine("Данный студент не должник"); Console.ReadLine(); }
            else
            {
                List<string> cashe3 = new List<string>();
                for (int i = 0; i < prepod.Count; i++)
                {
                    for(int j = 0; j < prepod[i].Groups.Count; j++)
                    {
                        if (prepod[i].Groups[j] == group)
                        {
                            for(int k = 0; k < prepod[i].Subjs.Count; k++)
                            {
                                for(int o = 0; o < cashe2.Count; o++)
                                {
                                    if (prepod[i].Subjs[k] == cashe2[o])
                                    {
                                        cashe3.Add(prepod[i].FIO);
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine($"Студент {name}");
                for(int i = 0; i < cashe3.Count; i++)
                {
                    Console.WriteLine($"Долг по {cashe2[i]}. Преподователь: {cashe3[i]}");
                }
                Console.ReadLine();
            }
        }

        static void ListPrepod(ref List<Prepod> prepod, ref List<Stud> stud)
        {
            Console.Clear();
            Console.WriteLine("==Преподователи и их должники==\n\n");
            for (int i = 0; i < prepod.Count; i++)
            {
                List<string> cashe6 = new List<string>();
                for (int j = 0; j < prepod[i].Groups.Count; j++)
                {
                    for (int k = 0; k < stud.Count; k++)
                    {
                        if (stud[k].Group == prepod[i].Groups[j])
                        {
                            for (int l = 0; l < prepod[i].Subjs.Count; l++)
                            {
                                for (int o = 0; o < stud[k].Subj.Count; o++)
                                {
                                    if ((prepod[i].Subjs[l] == stud[k].Subj[o]) && stud[k].Score[o] < 3)
                                    {
                                        cashe6.Add(stud[k].FIO);
                                    }
                                }
                            }
                        }    
                    }
                }
                if(cashe6.Count != 0)
                {
                    Console.Write($"Преподователь {prepod[i].FIO}\nДолжники: ");
                    for(int q = 0; q < cashe6.Count; q++)
                    {
                        Console.WriteLine(cashe6[q]);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
            
        }

        static void Ykazi(ref List<Glav> glav)
        {
            Console.Clear();
            Console.WriteLine("==Список указов==\n\n1) Для студентов\n2) Для преподователей\n3) Для всех");
            ConsoleKeyInfo key5 = Console.ReadKey(true);
            if (key5.Key == ConsoleKey.D1) { YkazStud(ref glav); }
            else if (key5.Key == ConsoleKey.D2) { YkazPrepod(ref glav); }
            else if (key5.Key == ConsoleKey.D3) { YkazAll(ref glav); }
        }

        static void YkazStud(ref List<Glav> glav)
        {
            Console.Clear();
            for(int i = 0; i < glav.Count; i++)
            {
                for (int j = 0; j < glav[i].Ykaz.Count; j++)
                {
                    string[] idx = glav[i].ID[j].Split('.');
                    if (idx[0] == "1")
                    {
                        Console.Write($"Указ {glav[i].ID[j]}\n{glav[i].Ykaz[j]}\n{glav[i].FIO}\n");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void YkazPrepod(ref List<Glav> glav)
        {
            Console.Clear();
            for (int i = 0; i < glav.Count; i++)
            {
                for (int j = 0; j < glav[i].Ykaz.Count; j++)
                {
                    string[] idx = glav[i].ID[j].Split('.');
                    if (idx[0] == "2")
                    {
                        Console.Write($"Указ {glav[i].ID[j]}\n{glav[i].Ykaz[j]}\n{glav[i].FIO}\n");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        static void YkazAll(ref List<Glav> glav)
        {
            Console.Clear();
            for (int i = 0; i < glav.Count; i++)
            {
                for (int j = 0; j < glav[i].Ykaz.Count; j++)
                {
                    string[] idx = glav[i].ID[j].Split('.');
                    if (idx[0] == "3")
                    {
                        Console.Write($"Указ {glav[i].ID[j]}\n{glav[i].Ykaz[j]}\n{glav[i].FIO}\n");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }



        static void AddStud(ref List<Stud> stud)
        {
            Console.Clear();
            Console.WriteLine("==Заполнение студентов==\n");
            bool IsOpen = true; 
            while(IsOpen)
            {
                Console.Write("ФИО: ");
                string fio = Console.ReadLine();
                Console.Write("Группа: ");
                string group = Console.ReadLine();

                List<string> subj = new List<string>();
                bool isopen1 = true;
                Console.WriteLine("Space - закончить || Enter - продолжить");
                while (isopen1)
                {
                    Console.Write("Предмет: ");
                    subj.Add(Console.ReadLine());

                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    if (key2.Key == ConsoleKey.Spacebar)
                    {
                        isopen1 = false;
                    }
                }

                List<int> score = new List<int>();
                for (int i = 0; i < subj.Count; i++)
                {
                    Console.Write($"Оценка по {subj[i]}: ");
                    score.Add(Convert.ToInt32(Console.ReadLine()));
                }

                stud.Add(new Stud(fio, group, subj, score));
                Console.WriteLine("Space - закончить || Enter - продолжить");
                ConsoleKeyInfo key1 = Console.ReadKey(true);
                if (key1.Key == ConsoleKey.Spacebar)
                {
                    IsOpen = false;
                }
                Console.WriteLine();
            }
        }

        static void AddPrepod(ref List<Prepod> prepod)
        {
            Console.Clear();
            Console.WriteLine("==Заполнение преподователей==\n");
            bool IsOpen = true;
            while (IsOpen)
            {
                Console.Write("ФИО: ");
                string fio = Console.ReadLine();

                List<string> groups = new List<string>();
                bool isopen2 = true;
                Console.WriteLine("Space - закончить || Enter - продолжить");
                while (isopen2)
                {
                    Console.Write("Группа: ");
                    groups.Add(Console.ReadLine());

                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    if (key2.Key == ConsoleKey.Spacebar)
                    {
                        isopen2 = false;
                    }
                }

                List<string> subjs = new List<string>();
                bool isopen1 = true;
                Console.WriteLine("Space - закончить || Enter - продолжить");
                while (isopen1)
                {
                    Console.Write("Предмет: ");
                    subjs.Add(Console.ReadLine());

                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    if (key2.Key == ConsoleKey.Spacebar)
                    {
                        isopen1 = false;
                    }
                }

                prepod.Add(new Prepod(fio, groups, subjs));
                Console.WriteLine("Space - закончить || Enter - продолжить");
                ConsoleKeyInfo key1 = Console.ReadKey(true);
                if (key1.Key == ConsoleKey.Spacebar)
                {
                    IsOpen = false;
                }
                Console.WriteLine();
            }
        }

        static void AddGlav(ref List<Glav> glav)
        {
            Console.Clear();
            Console.WriteLine("==Заполнение руководителей==\n");
            bool IsOpen = true;
            while (IsOpen)
            {
                Console.Write("ФИО: ");
                string fio = Console.ReadLine();

                List<string> ykaz = new List<string>();
                List<string> id = new List<string>();
                bool isopen2 = true;
                while (isopen2)
                {
                    Console.Write("Указ: ");
                    ykaz.Add(Console.ReadLine());
                    Console.WriteLine("Для кого указ:\n1) Для студентов\n2) Для преподователей\n" +
                        "3) Для всех");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    if (key2.Key == ConsoleKey.D1) { id.Add($"1.{id.Count}.5564"); }
                    else if (key2.Key == ConsoleKey.D2) { id.Add($"2.{id.Count}.5564"); }
                    else if (key2.Key == ConsoleKey.D3) { id.Add($"3.{id.Count}.5564"); }
                    Console.WriteLine("Space - закончить || Enter - продолжить");
                    ConsoleKeyInfo key3 = Console.ReadKey(true);
                    if (key3.Key == ConsoleKey.Spacebar) { isopen2 = false; }
                }

                glav.Add(new Glav(fio, ykaz, id));
                Console.WriteLine("Space - закончить || Enter - продолжить");
                ConsoleKeyInfo key1 = Console.ReadKey(true);
                if (key1.Key == ConsoleKey.Spacebar)
                {
                    IsOpen = false;
                }
                Console.WriteLine();
            }
        }


    }
    class Stud
    {
        public string FIO;
        public string Group;
        public List<string> Subj;
        public List<int> Score;

        public Stud(string fio, string group, List<string> subj, List<int> score)
        {
            FIO = fio;
            Group = group;
            Subj = subj;
            Score = score;
        }
    }
    class Prepod
    {
        public string FIO;
        public List<string> Subjs;
        public List<string> Groups;

        public Prepod(string fio, List<string> groups, List<string> subjs)
        {
            FIO = fio;
            Groups = groups;
            Subjs = subjs;
        }
    }
    class Glav
    {
        public string FIO;
        public List<string> Ykaz;
        public List<string> ID;

        public Glav(string fio, List<string> ykaz, List<string> id)
        {
            FIO = fio;
            Ykaz = ykaz;
            ID = id;
        }
    }
}