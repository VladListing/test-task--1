using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace test_task__1  // создание бинарных файлов содержащих структуры описанные в тестовом задании


//====================================================== обьявляем структуры ================ начало  ====================================
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]  // размещение в неуправляемый  код
    public struct Header //обьявление структуры "Header"
    {
        public int version;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]  //маршалинг в неуправляемый код
        public string type;


        //конструктор "Header"
        public Header(int e, string k)
        {
            version = e;
            type = k;
        }

    }

    //[Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)] // размещение в неуправляемый код
    public struct TradeRecord //обьявление структуры "TradeRecord"
    {

        public int id;
        public int account;
        public double volume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] //маршалинг в неуправляемый код
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
    //====================================================== обьявляем структуры ================ конец  ====================================


    class Program
    {


        static void Main(string[] args) // точка входа
        {
            int schetchik = 0;//переменная счетчик количества проходов цикла
            int quantityHeaderLine = 1;//переменная количества элементов масива  содержашего структуры типа Header----х
            int quantityTradeRecodLine = 110;//переменная количества элементов масива  содержашего структуры типа TradeRecod----х

            //BinaryFormatter formatter = new BinaryFormatter();// создаем экземпляр сериализатора для бинарных данных

            //=================================================== создаем экземпляры (обьекты) структур и инициализируем их поля ============= start =====================================|

            //------------------------------------------------------------------------------------------------------------------------|
            Header[] header = new Header[quantityHeaderLine]; // создание экземпяра структуры "TradeRecord" на 1-ну строку
            header[0] = new Header(0001, "USD-EUR"); // инециализируем поля , присваиваем им значения через конструктор

            for (int i = 0; i < quantityHeaderLine; i++)
            {
                header[i] = new Header(0 + i, "USD-EUR");// инециализируем поля , присваиваем им значения через конструктор
            }
            //---------------------------------------------------------------------------------------------------------------------|


            //--------------------------------------------------------------------------------------------------------------------------|
            //TradeRecord[] trade = new TradeRecord[7]; // создание экземпяра структуры "TradeRecord" на 7-мь строк
            //trade[0] = new TradeRecord(01, 771, 6401, "profit 1");// инециализируем поля , присваиваем им значения через конструктор
            //trade[1] = new TradeRecord(02, 772, 6402, "profit 2");
            //trade[2] = new TradeRecord(03, 773, 6403, "profit 3");
            //trade[3] = new TradeRecord(04, 774, 6404, "profit 4");
            //trade[4] = new TradeRecord(05, 775, 6405, "profit 5");
            //trade[5] = new TradeRecord(06, 776, 6406, "profit 6");
            //trade[6] = new TradeRecord(07, 777, 6407, "profit 7");

            TradeRecord[] trade = new TradeRecord[quantityTradeRecodLine]; // создание экземпяра структуры "TradeRecord" на X строк
            //TradeRecord[] newtrade = new TradeRecord[quantityTradeRecodLine]; // создание экземпяра структуры "TradeRecord" на X строк


            for (int i = 0; i < quantityTradeRecodLine; i++)
            {
                trade[i] = new TradeRecord(0 + i, 771 + i, 640 + i, "profit " + i);// инециализируем поля , присваиваем им значения через конструктор
            }
            //----------------------------------------------------------------------------------------------------------------------|

            //=================================================== создаем экземпляры (обьекты) структур и инициализируем их поля ======================= end ==========================|



            //============================================= записываем значения из структур в бинарный файл Х*.dat ============== start ====================================================|
            string path = @"D:\\_LISTING_\B-files\StructTrade_Line_110.dat";  //путь и имя будующего бинарного файла содержащего  структуры


            try
            {
                // создаем объект BinaryWriter (запись в бинарный файл)
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))// открывае поток для записи структур в файл
                {
                    //-------циклом  записываем в файл значение  каждого поля структуры "Header" --------------start--------------------|
                    foreach (Header z in header)
                    {
                        writer.Write(z.version);
                        writer.Write(z.type);

                        schetchik = schetchik + 1;
                    }

                    Console.WriteLine("Счетчик записанных в файл строк структуры header: {0}  ", schetchik);

                    schetchik = 0;

                    //var len1 = Marshal.SizeOf(typeof(Header));
                    //------------------ записываем в файл значение  каждого поля структуры "Header"-----------------------end--------|

                    //--записываем через цикл в файл значение каждого поля строк структуры "TradeRecord"-----------start------------------|
                    foreach (TradeRecord t in trade)
                    {
                        writer.Write(t.id);
                        writer.Write(t.account);
                        writer.Write(t.volume);
                        writer.Write(t.comment);

                        schetchik = schetchik + 1;
                    }

                    Console.WriteLine("Счетчик записанных в файл строк строк структуры trade: {0}  ", schetchik);

                    schetchik = 0;

                    //var len2 = Marshal.SizeOf(typeof(TradeRecord));
                    //-записываем через цикл в файл значение каждого поля строк структуры "TradeRecord"-----------------------end------|
                }

                //============================================= записываем значения из структур в бинарный файл .*dat ============== end ============================================|




                // создаем объект BinaryReader (чтение  из бинарного файла)
                //    using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                //{
                //        Console.WriteLine();//пустая строка
                //        reader.BaseStream.Position = 0;// устанавливаем "курсор" на 0-вую позицию в бинарном файле

                //        // считываем через цикл каждое значение полей строк структуры "Header" из файла "treding.dat" и выводим на экран

                //        //while (reader.PeekChar() > -1)
                //        ////while (reader.BaseStream.Position < 12)//пока позиция курсора не превышает 12-тую в бинарном файле

                //        //{
                //        //    //var poz = reader.Current;
                //        //    int version = reader.ReadInt32();
                //        //    string type = reader.ReadString();

                //        //    Console.WriteLine("версия: {0}      тип: {1} ", version, type);
                //        //}
                //        //    Console.WriteLine();//пустая строка

                //        //reader.BaseStream.Position =  12;// устанавливаем "курсор" на 12-вую позицию в бинарном файле

                //        // пока не достигнут конец файла
                //        // считываем через цикл каждое значение полей строк структуры "TradeRecord" из файла "treding.dat" и выводим на экран
                //        while (reader.PeekChar() > -1)// пока не достигнут конец файла
                //        {
                //            int id = reader.ReadInt32();
                //            int account = reader.ReadInt32();
                //            double volume = reader.ReadDouble();
                //            string comment = reader.ReadString();

                //            schetchik = schetchik + 1;

                //            Console.WriteLine("id: {0}      счет: {1}     уровень: {2}       комментарий: {3} ", id, account, volume, comment);
                //        }

                //        Console.WriteLine(); //пустая строка

                //        Console.WriteLine("Счетчик вычитанных из бинарного файла строк строк структуры trade: {0}  ", schetchik);

                //    }
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
