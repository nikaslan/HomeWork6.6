using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork6._6
{
    class Program
    {
        /// <summary>
        /// Считывает содержимое файла и выводит в консоль построчно
        /// </summary>
        /// <param name="filepath">путь к файлу с данными для вывода</param>
        static void readEntrys(string filepath)
        {
            // считываение файла построчко, записываем каждую строку в отдельный элемент массива. 
            // 1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва
            string[] employees = File.ReadAllLines(filepath);
            Console.WriteLine("ID | Добавлен        | Имя                      | Возраст | Рост | Дата рож. | Место рождения");
            // парсинг каждой строки и вывод на экран
            foreach(string employee in employees)
            {
                string[] entry = employee.Split('#');
                Console.WriteLine($"{entry[0],3}| {entry[1],16}| {entry[2],25}| {entry[3],8}| {entry[4],3}см| {entry[5], 10}| {entry[6]}");
            }
        }
        /// <summary>
        /// Пошагово принимает данные о новом сотруднике для внесения его в базу
        /// </summary>
        /// <param name="filepath">путь к файлу "базы"</param>
        static void addEntry(string filepath)
        {

            string[] newEntry = new string[7];
            newEntry[0] = "1";

            //0 - ID
            //1 - Дата добавления
            //2 - ФИО сотрудника
            //3 - Возраст
            //4 - Рост
            //5 - Дата рождения
            //6 - Мето рождения


            if (File.Exists(filepath)) // высчитываем ID для новой записи
            {
                string[] temp = File.ReadAllLines(filepath); 
                newEntry[0] = Convert.ToString(temp.Length+1);

                bool empty = (temp.Length == 0) ? true : false;
            }
            #region ввод данных
            Console.WriteLine("Введите имя нового сотрудника. *Только первые 25 символов имени будут сохранены:");
            newEntry[2] = Console.ReadLine();
            if (newEntry[2].Length > 25) newEntry[2] = newEntry[2].Substring(0,25);

            Console.WriteLine("\nВведите рост нового сотрудника в сантимертах:");
            newEntry[4] = Console.ReadLine();
            if (newEntry[4].Length > 20) newEntry[4] = newEntry[4].Substring(0, 3);

            // ввод дня рождения, высчитываем возраст
            Console.WriteLine("\nВведите год рождения нового сотрудника в формате дд.мм.гггг");
            newEntry[5] = Console.ReadLine().Substring(0, 10);
            DateTime birthday = new DateTime(Convert.ToInt32(newEntry[5].Substring(6,4)), Convert.ToInt32(newEntry[5].Substring(3, 2)),Convert.ToInt32(newEntry[5].Substring(0, 2)));
            
            newEntry[3] = ((DateTime.Now - birthday).TotalDays/365.25).ToString("#");

            // Ввод города рождения
            Console.WriteLine("\nВведите место рождения нового сотрудника");
            newEntry[6] = Console.ReadLine();
            if (newEntry[6].Length > 20) newEntry[6] = newEntry[6].Substring(0, 20);

            newEntry[1] = Convert.ToString(DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString());
            #endregion

            Console.WriteLine("\nВ справочник будет внесена следующая запись:");
            Console.WriteLine("\nИмя                      | Возраст | Рост | Дата рож. | Место рождения");
            Console.WriteLine($"{newEntry[2],25}| {newEntry[3],8}| {newEntry[4],3}см| {newEntry[5],10}| {newEntry[6]}");

            Console.WriteLine("\nЕсли хотите подтвердить внесение записи, нажмите 1. Для отмены - нажмите Esc.");

            while (true)
            {

                
                var key = Console.ReadKey(true).Key;
                if (((char)key) == '1')
                {
                                        
                    string toAppend = Convert.ToInt32(newEntry[0]) == 1 ? newEntry[0] : "\n" + newEntry[0];


                    for(int i = 1; i<newEntry.Length;i++)
                    {
                        toAppend = toAppend + "#" + newEntry[i];
                    }

                    File.AppendAllText(filepath, toAppend);

                    Console.WriteLine("\nЗапись внесена");
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
                // если нажали на что-то, кроме 1,2 или Esc, то это не будет принято 
            }

        }
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            // объявление переменных
            string filepath = "catalogue.txt";


            // основная логика
            Console.WriteLine("Программа-справочник \"Сотрудники\"");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Нажмите 1 для вывода существующих записей. Нажмите 2 для добавления новой записи. Для выхода, нажмите Esc.");
                var key = Console.ReadKey(true).Key;
                if (((char)key) == '1')
                {
                    if (!File.Exists(filepath))
                    {
                        Console.WriteLine("Справочник пуст. Введите хотя бы одну запись, для просмотра.");
                        Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                        Console.ReadKey(true);
                        continue;
                    }
                    readEntrys(filepath);
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    continue;
                }
                else if (((char)key) == '2')
                {
                    addEntry(filepath);
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    continue;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
                // если нажали на что-то, кроме 1,2 или Esc, то это не будет принято 
            }
            
        }
    }
}
