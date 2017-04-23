using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace test_task__1  // создание бинарных файлов
{
       
    public struct Header //обьявление структуры "Header"
    {
        public int version;
        //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=16)]
        public string type;


        //конструктор "Header"
        public Header(int e, string k)
        {
            version = e;
            type = k;
        }

    }



    public struct TradeRecord //обьявление структуры "TradeRecord"
    {

        public int id;
        public int account;
        public double volume;
        //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=64)]
        public string comment;


        //конструктор "TradeRecord"
        public TradeRecord(int a, int b, double c, string d)
        {
            id = a;
            account = b;
            volume = c;
            comment = d;

        }
    }

    

    class Program
    {
       

        static void Main(string[] args) // точка входа
        {

            Header[] header = new Header[1]; // создание экземпяра структуры "TradeRecord" на 1-ну строку
            header[0] = new Header(0001, "USD-EUR"); // инециализируем поля , присваиваем им значения через конструктор


            TradeRecord[] trade = new TradeRecord[7]; // создание экземпяра структуры "TradeRecord" на 7-мь строк
            trade[0] = new TradeRecord(01, 771, 6401, "profit 1");// инециализируем поля , присваиваем им значения через конструктор
            trade[1] = new TradeRecord(02, 772, 6402, "profit 2");
            trade[2] = new TradeRecord(03, 773, 6403, "profit 3");
            trade[3] = new TradeRecord(04, 774, 6404, "profit 4");
            trade[4] = new TradeRecord(05, 775, 6405, "profit 5");
            trade[5] = new TradeRecord(06, 776, 6406, "profit 6");
            trade[6] = new TradeRecord(07, 777, 6407, "profit 7");




            string path = @"D:\\_LISTING_\B-files\treding.dat";  //путь и имя бинарного файла со структурами



        try
        {
                // создаем объект BinaryWriter (запись в бинарный файл)
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    //// записываем в файл значение  каждого поля структуры "Header"
                    foreach (Header z in header)
                    {
                        writer.Write(z.version);
                        writer.Write(z.type);
                    }

                    var len1 = Marshal.SizeOf(typeof (Header));
                    

                    //записываем через цикл в файл значение каждого поля строк структуры "TradeRecord"
                    foreach (TradeRecord t in trade)
                    {
                        writer.Write(t.id);
                        writer.Write(t.account);
                        writer.Write(t.volume);
                        writer.Write(t.comment);
                    }
                    var len2 = Marshal.SizeOf(typeof(TradeRecord));
                }




                // создаем объект BinaryReader (чтение  из бинарного файла)
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                    Console.WriteLine();//пустая строка
                    reader.BaseStream.Position = 0;// устанавливаем "курсор" на 0-вую позицию в бинарном файле
                    
                    // считываем через цикл каждое значение полей строк структуры "Header" из файла "treding.dat" и выводим на экран

                    //while (reader.PeekChar() > -1)
                    while (reader.BaseStream.Position < 12)//пока позиция курсора не превышает 12-тую в бинарном файле

                    {
                        //var poz = reader.Current;
                        int version = reader.ReadInt32();
                        string type = reader.ReadString();

                        Console.WriteLine("версия: {0}      тип: {1} ", version, type);
                    }
                        Console.WriteLine();//пустая строка

                    reader.BaseStream.Position =  12;// устанавливаем "курсор" на 12-вую позицию в бинарном файле

                    // пока не достигнут конец файла
                    // считываем через цикл каждое значение полей строк структуры "TradeRecord" из файла "treding.dat" и выводим на экран
                    while (reader.PeekChar() > -1)// пока не достигнут конец файла
                    {
                        int id = reader.ReadInt32();
                        int account = reader.ReadInt32();
                        double volume = reader.ReadDouble();
                        string comment = reader.ReadString();

                        Console.WriteLine("id: {0}      счет: {1}     уровень: {2}       комментарий: {3} ", id, account, volume, comment);
                    }
                }
            }



        //вывод сообщения о возникшем исключении
        catch (Exception m)
        {
            Console.WriteLine(m.Message);
        }
        Console.ReadLine();
    



        }
    }
}
