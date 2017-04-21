using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace test_task__1
{


    public struct TradeRecord
    {
        //
        public string name;
        public string capital;
        public int area;
        public double people;


        //конструктор
        public TradeRecord(string n, string c, int a, double p)
        {
            name = n;
            capital = c;
            people = p;
            area = a;
        }
    }


        class Program
    {
        static void Main(string[] args)
        {
           


        TradeRecord[] states = new TradeRecord[2];
        states[0] = new TradeRecord("Германия", "Берлин",  357168,  80.8);
        states[1] = new TradeRecord("Франция", "Париж", 640679, 64.7);
 
        string path= @"D:\\_LISTING_\B-files\treding.dat";
 
        try
        {
            // создаем объект BinaryWriter
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                // записываем в файл значение каждого поля структуры
                foreach (TradeRecord s in states)
                {
                    writer.Write(s.name);
                    writer.Write(s.capital);
                    writer.Write(s.area);
                    writer.Write(s.people);
                }
            }




            // создаем объект BinaryReader (чтение  из бинарного файла)
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    string capital = reader.ReadString();
                    int area = reader.ReadInt32();
                    double population = reader.ReadDouble();
 
                    Console.WriteLine("Страна: {0}  столица: {1}  площадь {2} кв. км   численность населения: {3} млн. чел.", 
                        name, capital, area, population);
                }
            }
        }




        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.ReadLine();
    



        }
    }
}
