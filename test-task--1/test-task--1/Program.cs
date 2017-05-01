#region пространства имен
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
#endregion

namespace test_task__1  // создание бинарных файлов содержащих структуру "trade" описанную в тестовом задании
{
    //описание структуры 'TradeRecord' 
    #region 'TradeRecord'

    [StructLayout(LayoutKind.Sequential, Pack = 1)]// размещение в неуправляемый код
    public struct TradeRecord
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
    #endregion


    class Program
    {
        static void Main(string[] args) // точка входа
        {
            Console.WriteLine("");
            Console.WriteLine("Введите количество строк для создаваемого бинарного файла  'D:\\Trade.dat' : ");
            Console.WriteLine("");
            string r = Console.ReadLine();
            int quantityTradeRecodLine = Convert.ToInt32(r);//количество элементов массива  содержашего структуры типа TradeRecod----х
            string path = @"D:\\Trade.dat";  //путь и имя будующего бинарного файла содержащего  структуры
            int schetchik = 0;//счетчик
            RandomString rnd = new RandomString();

            //создание экземпляра структуры и инициализфция полей (присвоение значений)
            #region 

            TradeRecord[] trade = new TradeRecord[quantityTradeRecodLine]; // создание экземпяра структуры "TradeRecord" на X строк

            for (int i = 0; i < quantityTradeRecodLine; i++)
            {
                trade[i] = new TradeRecord(0 + i, 777, 640 + i, rnd.ArrayRand());
            }
            #endregion


            //секция критичная в части исключений
            try
            {
                //создание экземпляра BinaryWriter (запись  в бинарный файл)
                #region BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))// открывает поток для записи структур в файл
                {

                    #region
                    foreach (TradeRecord t in trade)
                    {
                        writer.Write(t.id);
                        writer.Write(t.account);
                        writer.Write(t.volume);
                        writer.Write(t.comment);

                        schetchik = schetchik + 1;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("в бинарный файл 'D:\\Trade.dat' записано   {0}  строк(и) структуры 'trade'", schetchik);

                    schetchik = 0;

                    #endregion

                }
                #endregion

            }

            //вывод сообщения о возновении исключения
            #region исключения
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }
            Console.ReadLine();
            #endregion




        }
    }
}
