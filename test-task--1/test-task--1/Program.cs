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

{
    
    //====================================================== обьявляем структуры ================ start  ==================================|
      #region описание структура trade
    
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
    #endregion
    //====================================================== обьявляем структуры ================ end  =================================|


    class Program
    {

        static void Main(string[] args) // точка входа
        {
            int schetchik = 0;//переменная счетчик количества проходов цикла
            int quantityTradeRecodLine = 110;//переменная количества элементов масива  содержашего структуры типа TradeRecod----х

            //======================= создаем экземпляры (обьекты) структур и инициализируем их поля ============= start ==============================|
            
            //------------------------------------------------trade-------------------|
            #region trade
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
            #endregion
            //-------------------------------------------------------------------|

            //============= создаем экземпляры (обьекты) структур и инициализируем их поля ======================= end ==========================|



            //============================================= записываем значения из структур в бинарный файл Х*.dat ============== start =======================================|
            string path = @"D:\\_LISTING_\B-files\StructTrade_Line_110.dat";  //путь и имя будующего бинарного файла содержащего  структуры


            try//'''''''''  try '''''''' оператор выполнение которого может привести к ошибке ''''''''''''начало ''''|
            {
                //================== создаем объект BinaryWriter (запись  в бинарный файл)=================================start=========================|
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
                //================== создаем объект BinaryWriter (запись  в бинарный файл)================================= end =====================|
                     
                
            }//'''''''''  try '''''''' оператор выполнение которого может привести к ошибке ''''''''''''конец ''''|


            //-----вывод сообщения о возникшем исключении--------
            #region исключения
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }
            Console.ReadLine();
            #endregion
            //---------------------------------------------------

            //====================================== записываем значения из структур в бинарный файл .*dat ============================ end =============================|

        }
    }
}
