using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using System.IO;
using test_task_1;
using System.Collections.Generic;

namespace test_task_1_Unit_Testing_1
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





    [TestClass]
    public class ReadFromBinaryFileTests
    {



        //Unit test 1 создаем тестовый бинарный файл 'Tread-test.dat' содержащий структуру 'tread'(1000000 'миллион'строк )
        //тестирем метод 'fromBinaryFile' с его помощью вычитываем данные из тестового бинарного файла и размещеим их в коллекции
        //после чего сверяем колличество строк в тестовом файле и колличество вычитанных строк в коллекцию, если они равны то тест пройден.  

        [TestMethod]//драйвер


        public void fromBinaryFile_Created_and_Read__Lines()
        {
            //создание заглушки для тестируемого метода в виде тестового бинарного файла 'Trade-test.dat'
            string path = @"D:\\Trade-test.dat";  //путь и имя будующего бинарного файла содержащего  структуры
            int schetchik = 0;//счетчик
            int result1 = 0;//счетчик
            int lines = 1000000;//на скольких строках протестировать?

            //создание экземпляра структуры и инициализфция полей (присвоение значений)
            #region 

            TradeRecord[] trade = new TradeRecord[lines]; // создание экземпяра структуры "TradeRecord" на X строк

            for (int i = 0; i < lines; i++)
            {
                trade[i] = new TradeRecord(0 + i, lines, 640 + i, "строка Unit тестового файла");
            }
            #endregion


            //секция критичная в части исключений

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
                Console.WriteLine("в бинарный файл 'D:\\Trade-test.dat' записано   {0}  строк(и) структуры 'trade'", schetchik);
                result1 = schetchik;
                schetchik = 0;
                #endregion
            }
            #endregion






            //2.выполнить действие над той системой которую мы тестируем

            //ReaderFromBinaryFiles readerFromBinaryFiles = new ReaderFromBinaryFiles();
            ReaderFromBinaryFiles readerFromBinaryFiles = new ReaderFromBinaryFiles(path);

            int result2;//количество строк вычитаных из файла в коллекцию
             readerFromBinaryFiles.fromBinaryFile(path, out result2);


            Assert.AreEqual(result1, result2);//сравнение ожидаемого и полученого

        }








                

           


    }
}
