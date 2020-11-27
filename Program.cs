using System;
using System.Collections.Generic;
using System.Linq;
namespace _27112020
{
    class Human
    {
        public string PIB { get; set; }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Birthday.Year;
                if (Birthday > now.AddYears(-age)) age--;
                return age;
            }
        }
        public override string ToString()
        {
            return $"|{PIB,15}| {Age,2} | {Birthday:dd.MM.yyyy} |";
        }
    }
    class Student : Human, IComparable<Student>
    {
        public readonly List<int> marks = new List<int>();// {10, 12, 8, 9, 6};
        public double AverageMarks => marks.Average();
        public void SetMarks()
        {
            marks.Add(new Random().Next(1, 13));
        }
        public void SetMarksH(int m)
        {
            marks.Add(m);
        }
        public override string ToString()
        {
            string m = String.Join(" ", marks.Select(x => $"{x,2}"));
            return $"{base.ToString(),15} {m,20}|";
        }
        public int CompareTo(Student other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var pibComparison = string.Compare(PIB, other.PIB, StringComparison.Ordinal);
            if (pibComparison != 0) return pibComparison;
            return Birthday.CompareTo(other.Birthday);
        }
    }
    class Program
    {
        static Student[] Group()
        {
            Student[] gr =
            {
                new Student {PIB = "Ivan", Birthday = new DateTime(1989, 10, 6)},
                new Student {PIB = "Petro", Birthday = new DateTime(2000, 4, 10)},
                new Student {PIB = "Stepan", Birthday = new DateTime(2003, 10, 3)},
                new Student {PIB = "Igor", Birthday = new DateTime(2002, 2, 23)},
                new Student {PIB = "Inna", Birthday = new DateTime(1982, 10, 10)},
                new Student {PIB = "Pasha", Birthday = new DateTime(1963, 3, 3)},
            };
            foreach (var st in gr)
                for (int i = 0; i < 10; i++)
                    st.SetMarks();
            return gr;
        }

        static void Task1()
        {
            int[] numbers = { 1, 5, 9, 3, 5, 647, 13, 3, 6, 42 };
            var col = numbers.Select(x => x);
            var newcol = numbers.Select(x => x).ToArray();
            foreach (var VARIABLE in col)
                Console.WriteLine($"{VARIABLE},");
            Console.WriteLine($"{string.Join("\t", numbers)}");
            Console.WriteLine($"{string.Join("\t", col)}");
            Console.WriteLine($"{string.Join("\t", newcol)}");
            Console.WriteLine("---------------------------------------");
            numbers[1] = 777;
            Console.WriteLine($"{string.Join("\t", numbers)}");
            Console.WriteLine($"{string.Join("\t", col)}");
            Console.WriteLine($"{string.Join("\t", newcol)}");

        }
        static void TaskWhere()
        {
            string[] students =
            {
                "Petrenko",
                " Ivanenko",
                " Varenko",
                " Ustymenko",
                "Stepanenko",
                " vasylenko",
            };
            var list = new List<string>();
            foreach (var pib in students)
                if (pib.Trim().ToUpper().StartsWith("V"))
                    list.Add(pib);
            //  if (pib[0]=='V' || pib[0]=='v') list.Add(pib);
            list.Sort();
            foreach (var pib in list)
                Console.WriteLine(pib);
            var query1 = from fname in students
                         where fname.Trim().ToUpper().StartsWith("V")
                         select fname;
            var query2 = students.Where(s => s.Trim().ToUpper().StartsWith("V"));
            Console.WriteLine($"{string.Join("\t", query1)}");
            Console.WriteLine($"{string.Join("\t", query2)}");
            int[] numbers = { 1, 5, 9, 3, 5, 647, 13, 3, 6, 42 };
            Console.WriteLine($"{string.Join("\t", numbers)}");
            //<5
            var query3 = from number in numbers
                         where number < 5
                         select number;
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where(n => n < 5);
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where((n, i) => n < 5 && (i & 1) == 1);//парне
            Console.WriteLine($"{string.Join("\t", query3)}");
            query3 = numbers.Where((n, i) => (i & 1) == 1); //парне
            Console.WriteLine($"{string.Join("\t", query3)}");
            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var queryS1 = from s in PE911
                          where s.PIB.Trim().ToLower().EndsWith('a')
                          select s;
            queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().EndsWith('a'));
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            queryS1 = from s in PE911
                      where s.PIB.Trim().ToLower().StartsWith('i') && s.Age > 20
                      select s;
            queryS1 = from s in PE911
                      where s.PIB.Trim().ToLower().StartsWith('i')
                      where s.Age > 20
                      select s;
            queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().StartsWith('i') && s.Age > 20);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            //8.0>
            queryS1 = PE911.Where(s => s.AverageMarks >= 8);
            Console.WriteLine("-------------------------------marks.Average-----------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
        }
        static void TaskSelect()
        {
            int[] numbers = { 1, -5, 9, 3, 5, -647, 13, 3, -6, 42 };
            Console.WriteLine($"{string.Join('\t', numbers)}");
            var col = numbers.Select(x => -x);
            Console.WriteLine($"{string.Join('\t', col)}");
            col = numbers.Select(x => 2 * x);
            Console.WriteLine($"{string.Join('\t', col)}");
            var cold = numbers.Select(x => 1.0 * x / 3);
            Console.WriteLine($"{string.Join('\t', cold)}");
            col = from x in numbers select x * 5;
            Console.WriteLine($"{string.Join('\t', col)}");
            cold = numbers.Select((x, i) => 1.0 * x / (i + x));
            Console.WriteLine($"{string.Join('\t', cold)}");

            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var queryS1 = from s in PE911 select s.PIB;
            queryS1 = PE911.Select(s => s.PIB + " " + s.Age);
            queryS1 = PE911.Select(s => s + " " + s.AverageMarks);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', queryS1)}");
            // queryS1 = PE911.Where(s => s.PIB.Trim().ToLower().EndsWith('a'));
            int[] num = { 1, 5, 9, 8, 3, 4, 6, 5 };
            string[] strNumbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            var listn = num.Select(n => strNumbers[n]);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\t', listn)}");
            var listSt = num.Select(n => new Student
            { PIB = strNumbers[n], Birthday = new DateTime(1989, n % 12, n % 28) });
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', listSt)}");
        }
        static void TaskSelectMany()
        {
            int[] arra = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] arrb = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var TableMult = from a in arra
                            from b in arrb
                            select $"{a,2}*{b,2}={a * b,3}";
            TableMult = arra.SelectMany(x => arrb, (a, b) => $"{a,2}*{b,2}={a * b,3}");

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"{string.Join('\n', TableMult)}");
            var PE911 = Group();
            //  Console.WriteLine($"{string.Join('\n', PE911)}");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', PE911)}");
            var sp = (from st in PE911
                      from m in st.marks
                      where m == 1
                      where st.Age > 15
                      select st).Distinct();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join<Student>('\n', sp)}");
            var sp2 = PE911
                .SelectMany(s => s.marks, (s, m) => new { Stud = s, mark = m })
                .Where(st => st.Stud.Age > 15 && st.mark == 1)
                .Select(x => x.Stud)
                .Distinct();

            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{string.Join('\n', sp2)}");
        }
        static void Main(string[] args)
        {
            //  Task1();
            /// TaskWhere();
            // TaskSelect();
            TaskSelectMany();
        }
    }
}