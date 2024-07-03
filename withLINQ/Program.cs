using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //faltou fazer uma conexão com o database :(
            List<Student> studentList = new List<Student>()
            {
                new Student(){ID = 1, Name = "James", Gender = "Male", Age = 24},
                new Student(){ID = 2, Name = "Sara", Gender = "Female", Age = 57},
                new Student(){ID = 3, Name = "Steve", Gender = "Male", Age = 38},
                new Student(){ID = 4, Name = "Pam", Gender = "Female", Age = 12},
                new Student(){ID = 5, Name = "Josh", Gender = "Male", Age = 17},
                new Student(){ID = 6, Name = "Drake", Gender = "Male", Age = 18},
                new Student(){ID = 7, Name = "Megan", Gender = "Female", Age = 11},
                new Student(){ID = 8, Name = "Carly", Gender = "Female", Age = 15},
                new Student(){ID = 9, Name = "Sam", Gender = "Female", Age = 15},
                new Student(){ID = 10, Name = "Fred", Gender = "Male", Age = 17},
                new Student(){ID = 11, Name = "Spencer", Gender = "Male", Age = 30},
                new Student(){ID = 11, Name = "Spencer", Gender = "Male", Age = 30},
                new Student(){ID = 11, Name = "Spencer", Gender = "Male", Age = 30},
                new Student(){ID = 11, Name = "Spencer", Gender = "Male", Age = 30},
            };

            //obs.: IEnumerable<Student> é um tipo relacionado a query(IEnumerable) que se relaciona com o tipo do objeto (Student)

            //query syntax
            IEnumerable<Student> QuerySyntax = from std in studentList
                                               where std.Gender == "Male"
                                               select std;

            //method syntax
            IEnumerable<Student> MethodSyntax = studentList.Where(std => std.Gender == "Male").ToList();

            //mixed syntaxes (veja que por usar o método count, foi mudado o tipo para int)
            int MixedSyntax = (from std in studentList
                             where std.Gender == "Female"
                             select std).Count();

            Console.WriteLine("Resultado proveniente da query syntax:");
            foreach (var student in QuerySyntax)
            {
                Console.WriteLine( $"ID : {student.ID},  Name : {student.Name}");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Resultado proveniente do method syntax:");
            foreach (var student in MethodSyntax)
            {
                Console.WriteLine( $"ID : {student.ID},  Name : {student.Name}");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Resultado da contagem de query mista: {MixedSyntax}");
            Console.WriteLine("\n");

            //orderby
            IEnumerable<Student> OrderByQuery = from std in studentList
                                              orderby std.Age
                                              select std;

            Console.WriteLine("Resultado proveniente do order by:");
            foreach (var student in OrderByQuery)
            {
                Console.WriteLine( $"Name : {student.Name}, Age: {student.Age}");
            }
            Console.WriteLine("\n");

            //o retorno é um grupo que possui a string de genero + o estudante
            IEnumerable<IGrouping<string, Student>> GroupByQuery = from std in studentList
                                              group std by std.Gender into genderGroup
                                              select genderGroup;

            Console.WriteLine("Resultado proveniente do group by:");
            foreach (var group in GroupByQuery)
            {
                Console.WriteLine($"Grupo: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"   {student.Name} - {student.Gender}");
                }
            }
            Console.WriteLine("\n");

            //utilizando o distinct
            IEnumerable<Student> DistinctQuery = studentList.Distinct();
                                                 
            Console.WriteLine("Resultado proveniente do distinct:");
            foreach (var student in DistinctQuery)
            {
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
            }
            Console.WriteLine("\n");

            //Single (veja que foi alterado o tipo para student, pois só irá retornar um)
            //é importante ressaltar que esse método não funciona para quando há itens duplicados
            //por exemplo, se buscarmos pelo id 11 do spencer, nao funcionaria
            Student SingleQuery = studentList.Single(s => s.ID == 1);

            Console.WriteLine("Resultado proveniente do Single:");
            Console.WriteLine($"Name: {SingleQuery.Name}, Age: {SingleQuery.Age}");
            Console.WriteLine("\n");

            //caso não há certeza de que vai ser retornado algum elemento, é possível utilizar o SingleOrDefault para que o sistema não lance excessões
            Student SingleDefaultQuery = studentList.SingleOrDefault(s => s.ID == 12);//utilizando um id que nao existe na base

            Console.WriteLine("Resultado proveniente do SingleOrDefault:");
            Console.WriteLine(SingleDefaultQuery);
            Console.WriteLine("\n");

            //First retorna o primeiro elemento que satisfaz a condicao
            Student FirstQuery = studentList.First(std => std.Gender == "Male");
            Console.WriteLine("Resultado proveniente do First:");
            Console.WriteLine($"Name: {FirstQuery.Name}, Age: {FirstQuery.Age}");
            Console.WriteLine("\n");

            //firstordefault é analogo ao singleordefault
            Student FirstDefaultQuery = studentList.FirstOrDefault(std => std.Gender == "Not Binary");
            Console.WriteLine("Resultado proveniente do FirstOrDefault:");
            Console.WriteLine(FirstDefaultQuery);
            Console.WriteLine("\n");

        }
    }

    public class Student : IEquatable<Student>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age {get; set; }

        //utilizado para usar o distinct corretamente
        public bool Equals(Student other)
        {
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}