using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10Library;


namespace Task14
{
    class Program
    {
        public static List<Person> Unternehmen = new List<Person>();  //Предприятие
        public static List<Person> Abteilung = new List<Person>(); //Отдел
        public static List<List<Person>> Sammelband = new List<List<Person>>();   //Сборник

        static void Main(string[] args)
        {
            //Выборка: 1. Имена рабочих заданной профессии /// 2. Имена рабочих заданного цеха
            //Счётчик: 1. Кол-во инженеров на работе 
            //Использование операций над множествами: Пересечение, объединение, разность
            //Агрегирование: 1. Средний возраст людей какой-либо профессии

            #region add
            Abteilung.Add(new Administer("First", "Last", 22, "Enemy"));
            Abteilung.Add(new Engineer("List", "T", 29, "Ice", "Effect"));
            Abteilung.Add(new Worker("FName", "LName", 34, "Friend"));
            Abteilung.Add(new Engineer("John", "Junior", 49, "Cold", "Visual"));
            Abteilung.Add(new Worker("Sub", "Zero", 18, "Counter"));
            Abteilung.Add(new Engineer("Linked", "T", 29, "Ice", "Effect"));
            Abteilung.Add(new Engineer("Roman", "Triumpf", 39, "Rezus", "Factor"));
            Administer adm = new Administer("Slesh", "Gta", 52, "Coldest");
            Abteilung.Add(adm);
            Unternehmen.Add(new Administer("Scorpion", "Sheet", 28, "Strike"));
            Unternehmen.Add(new Worker("First", "Of", 28, "All"));
            Unternehmen.Add(adm);
            Sammelband.Add(Abteilung);
            Sammelband.Add(Unternehmen);
            #endregion
            CollectionShow();
            Console.WriteLine();
            PrintColor("Выборка: 1. Имена рабочих заданной профессии (Администратор).",ConsoleColor.Red);
            NameWorkersLinq();
            Sammelband.NameWorkersExpansion("Administer");
            Console.WriteLine();
            PrintColor("Выборка: 2. Имена рабочих заданного цеха.",ConsoleColor.Red);
            Console.Write("Введите название цеха: ");
            string nameWorkShop = Console.ReadLine();
            NameWorkersWorkShopLinq(nameWorkShop);
            Sammelband.NameWorkersWorkShop(nameWorkShop);
            Console.WriteLine();
            PrintColor("Счётчик 1: Кол-во инженеров на работе",ConsoleColor.Red);
            CountEngineerLinq();
            Sammelband.CounterEngineerExpansion();
            Console.WriteLine();
            PrintColor("Операции над множествами",ConsoleColor.Red);
            IntersectionUnionDifferenceLinq();
            Sammelband.IntersectionUnionDifferenceExpansion();
            Console.WriteLine();
            PrintColor("1. Агрегирование: Средний возраст людей какой-либо профессии",ConsoleColor.Red);
            Console.Write("Введите профессию: ");
            string nameWork = Console.ReadLine();
            AgregateAverageAgeLinq(nameWork);
            Sammelband.AgregateAverageAgeExpansion(nameWork);
        }

        public static void AgregateAverageAgeLinq(string NameOfClass)
        {
            PrintColor("LINQ",ConsoleColor.Cyan);
            if (NameOfClass == "Administer")
            {
                double averageAgeAdminister = (from num in Sammelband
                    from t in num
                    where t is Administer
                    select (t.Age)).Average();
                Console.WriteLine("Средний возраст администратора - " + Math.Round(averageAgeAdminister,2));
            }
           else if (NameOfClass == "Worker")
            {
                double averageAgeWorker = (from num in Sammelband
                    from t in num
                    where t is Worker
                    select (t.Age)).Average();
                Console.WriteLine("Средний возраст рабочего - " + Math.Round(averageAgeWorker,2));
            }
            else if (NameOfClass == "Engineer")
            {
                double averageAgeEngineer = (from num in Sammelband
                    from t in num
                    where t is Engineer
                    select (t.Age)).Average();
                Console.WriteLine("Средний возраст инженера - " + Math.Round(averageAgeEngineer,2));
            }
            else
            {
                Console.WriteLine("Неверная профессия");
            }
        }

        public static void IntersectionUnionDifferenceLinq()
        {
            var personInter = (from c in Abteilung select c).Intersect(from c2 in Unternehmen select c2);
            var personDiff = (from c in Abteilung select c).Except(from c2 in Unternehmen select c2);
            var personUnion = (from c in Abteilung select c).Union(from c2 in Unternehmen select c2); 
            PrintColor("Разность множеств:",ConsoleColor.Cyan);
            foreach (Person item in personDiff)
                Console.WriteLine(item.Show());
            PrintColor("Пересечение множеств:", ConsoleColor.Cyan);
            foreach (Person item in personInter)
                Console.WriteLine(item.Show());
            PrintColor("Объединение множеств:", ConsoleColor.Cyan);
            foreach (Person item in personUnion)
                Console.WriteLine(item.Show());
        }

        public static void CountEngineerLinq()
        {
            PrintColor("LINQ",ConsoleColor.Cyan);
            int CounterEngineer =
                (from t in Sammelband
                    from g in t
                    where g is Engineer
                    select g).Count();
           Console.WriteLine(CounterEngineer);
        }

        public static void CollectionShow()
        {
            PrintColor("Коллекция 1 (Unternehmen)",ConsoleColor.Yellow);
            foreach (Person item in Unternehmen)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Element - ");
                Console.ResetColor();
                Console.WriteLine(item.Show());
            }

            PrintColor("Коллекция 1 (Abteilung)",ConsoleColor.Green);
            foreach (Person item in Abteilung)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Element - ");
                Console.ResetColor();
                Console.WriteLine(item.Show());
            }
        }

        public static void PrintColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static void NameWorkersWorkShopLinq(string NameWorkShop)
        {
            PrintColor("LINQ",ConsoleColor.Cyan);
            List<Worker> listWorkers = new List<Worker>();
            Worker wrkWorker = new Worker("","",0,"");
            foreach (List<Person> itemList in Sammelband)
            foreach (Person p in itemList)
                if (p is Worker)
                {
                    wrkWorker = p as Worker;
                    listWorkers.Add(wrkWorker);
                }
            var SelectedNames = from t in listWorkers
                where t.NameWorkShop.Equals(NameWorkShop)
                orderby t
                select t;
            foreach (Worker item in SelectedNames)
                Console.WriteLine(item.Show());
        }

        public static void NameWorkersLinq()
        {
            PrintColor("LINQ",ConsoleColor.Cyan);
            var SelectedSammelband = from t in Sammelband
                from g in t
                where g is Administer
                orderby g
                select g;
            foreach (Person item in SelectedSammelband)
                Console.WriteLine(item.FirstName);
        }
    }


    //Расширенный метод
    public static class Tested
    {
        public static void AgregateAverageAgeExpansion(this List<List<Person>> list, string NameOfClass)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Методы расширения");
            Console.ResetColor();
            double averageAge = 0;
            double counterAge = 0;
            double counterPersons = 0;
            if (NameOfClass == "Administer")
            {
                foreach (List<Person> itemList in list)
                foreach (Person item in itemList)
                        if (item is Administer)
                        {
                            counterAge += item.Age;
                            counterPersons++;
                        }
                averageAge = counterAge / counterPersons;
                Console.WriteLine("Средний возраст администратора - " + Math.Round(averageAge,2));
            }
            else if (NameOfClass == "Worker")
            {
                foreach (List<Person> itemList in list)
                foreach (Person item in itemList)
                    if (item is Worker)
                    {
                        counterAge += item.Age;
                        counterPersons++;
                    }
                averageAge = counterAge / counterPersons;
                Console.WriteLine("Средний возраст администратора - " + Math.Round(averageAge,2));
            }
            else if (NameOfClass == "Engineer")
            {
                foreach (List<Person> itemList in list)
                foreach (Person item in itemList)
                    if (item is Engineer)
                    {
                        counterAge += item.Age;
                        counterPersons++;
                    }
                averageAge = counterAge / counterPersons;
                Console.WriteLine("Средний возраст администратора - " + Math.Round(averageAge,2));
            }
            else Console.WriteLine("Неверные данные ввода профессии");
        }

        public static void IntersectionUnionDifferenceExpansion(this List<List<Person>> list)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Методы расширения");
            Console.ResetColor();
            int Counter = 0;
            int Virtus = 0;
            List<Person> FirstList = new List<Person>();
            List<Person> SecondList = new List<Person>();
            List<Person> ThirdList = new List<Person>();
            foreach (List<Person> itemList in list)
            {
                if(Counter == 0)
                    foreach (Person item in itemList)
                        FirstList.Add(item);
                else
                    foreach (Person item in itemList)
                        SecondList.Add(item);
                Counter++;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Разность множеств");
            Console.ResetColor();
            for (int i = 0; i < FirstList.Count; i++)
            {
                Virtus = 0;
                for (int j = 0; j < SecondList.Count; j++)
                {
                    if (FirstList[i].Equals(SecondList[j]))
                        Virtus++;
                }
                if(Virtus == 0)
                    Console.WriteLine(FirstList[i].Show());
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Пересечение множеств");
            Console.ResetColor();
            for (int i = 0; i < FirstList.Count; i++)
            {
                for (int j = 0; j < SecondList.Count; j++)
                {
                    if(FirstList[i] == SecondList[j])
                        Console.WriteLine(FirstList[i].Show());
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Объединение множеств");
            Console.ResetColor();
            for (int i = 0; i < FirstList.Count; i++)
            {
                ThirdList.Add(FirstList[i]);
            }
            for (int i = 0; i < SecondList.Count; i++)
            {
                ThirdList.Add(SecondList[i]);
            }
            foreach (Person item in ThirdList)
            {
                Console.WriteLine(item.Show());
            }
        }

        public static void CounterEngineerExpansion(this List<List<Person>> list)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Методы расширения");
            Console.ResetColor();
            int counter = 0;
            foreach (List<Person> itemList in list)
            foreach (Person item in itemList)
                if (item is Engineer)
                    counter++;
            Console.WriteLine(counter);
        }

        public static void NameWorkersWorkShop(this List<List<Person>> list, string NameWorkShop)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Методы расширения");
            Console.ResetColor();
            List<Worker> listWorkers = new List<Worker>();
            Worker wrkWorker = new Worker("", "", 0, "");
            foreach (List<Person> itemList in list)
            foreach (Person p in itemList)
                if (p is Worker)
                {
                    wrkWorker = p as Worker;
                    listWorkers.Add(wrkWorker);
                }
            Array.Sort(listWorkers.ToArray());
            foreach (Worker item in listWorkers)
                if(item.NameWorkShop == NameWorkShop) 
                    Console.WriteLine(item.Show());
        }

        public static void NameWorkersExpansion(this List<List<Person>> list, string NameOfClass)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Методы расширения");
            Console.ResetColor();
            if (NameOfClass == "Administer")
            {
                var res = list.SelectMany(f => f).Where(e => e is Administer).Select(x => x);
                foreach (Person item in res)
                    Console.WriteLine(item.FirstName);
            }

            if (NameOfClass == "Worker")
            {
                var res = list.SelectMany(f => f).Where(e => e is Worker).Select(x => x);
                foreach (Person item in res)
                    Console.WriteLine(item.FirstName);
            }

            if (NameOfClass == "Engineer")
            {
                var res = list.SelectMany(f => f).Where(e => e is Engineer).Select(x => x);
                foreach (Person item in res)
                    Console.WriteLine(item.FirstName);
            }
        }
    }
}
