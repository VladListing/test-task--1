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
    //====================================================== обьявляем структуры ================ start  ==================================|
    #region описание структура trade

    [StructLayout(LayoutKind.Sequential, Pack = 1)]// размещение в неуправляемый код
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
    #endregion
    //=========================================================================================== end  =================================|

     class Program
    {
        static void Main(string[] args) // точка входа
        {
            int quantityTradeRecodLine = 10000;//количество элементов массива  содержашего структуры типа TradeRecod----х
            string path = @"D:\\_LISTING_\B-files\StructTrade.dat";  //путь и имя будующего бинарного файла содержащего  структуры
            int schetchik = 0;//счетчик
            RandomString rnd = new RandomString();
            
            //======================= создаем экземпляры (обьекты) структур и инициализируем их поля ============================================ start ========|

            //------------------------------------------------trade-------------------|
            #region trade

            TradeRecord[] trade = new TradeRecord[quantityTradeRecodLine]; // создание экземпяра структуры "TradeRecord" на X строк

            for (int i = 0; i < quantityTradeRecodLine; i++)
            {
                trade[i] = new TradeRecord(0 + i, 771 + i, 640 + i, rnd.ArrayRand() );// инециализируем поля , присваиваем им значения через конструктор
            }
            #endregion
            //-------------------------------------------------------------------|

            //============= создаем экземпляры (обьекты) структур и инициализируем их поля ======================================================= end =====|

            //============================================= записываем значения из структур в бинарный файл Х*.dat =============================== start =======|
            
            try//'''''''''  try '''''''' оператор ''''''''''''начало ''''|
            {
                //================== создаем объект BinaryWriter (запись  в бинарный файл)============ start=========================|
                #region BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))// открывае поток для записи структур в файл
                {
                    //--записываем через цикл в файл значение каждого поля строк структуры "TradeRecord"-----------start------------------|
                    #region
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
                                        
                    #endregion
                    //-записываем через цикл в файл значение каждого поля строк структуры "TradeRecord"-----------------------end------|
                }
                # endregion
                //==================================================================================== end =====================|
                      
            }//'''''''''  try ''''''''''''''''''''''''''''''''''''''конец ''''|
            
            //-----вывод сообщения о возновении исключения--------
            #region исключения
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }
            Console.ReadLine();
            #endregion
            //---------------------------------------------------

            //====================================== записываем значения из структур в бинарный файл .*dat ======================================== end ====|

        }
    }
}
