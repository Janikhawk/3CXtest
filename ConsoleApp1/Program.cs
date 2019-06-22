 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Mydata> mdList = new List<Mydata>();
            Mydata md1 = new Mydata(2,"two");
            Mydata md2 = new Mydata(1, "one");
            Mydata md3 = new Mydata(4, "four");
            Mydata md4 = new Mydata(3, "three");

            mdList.Add(md1);
            mdList.Add(md2);
            mdList.Add(md3);
            mdList.Add(md4);

            foreach (var item in mdList)
            {
                Console.WriteLine($"Myint {item.MyInt} MyString: {item.MyString}");
            }
            mdList.Sort((x, y) => x.MyString.CompareTo(y.MyString));

            foreach(var item in mdList)
            {
                Console.WriteLine($"Myint {item.MyInt} MyString: {item.MyString}");
            }

            Console.ReadLine();



        }
    }
}
