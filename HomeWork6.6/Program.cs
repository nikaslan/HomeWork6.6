using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork6._6
{
    class Program
    {
        /// <summary>
        /// Считывает содержимое файла и выводит в консоль построчно
        /// </summary>
        /// <param name="filepath">путь к файлу "базы" справочника</param>
        static void readEntrys(string filepath)
        {
            // считываение файла построчко, записываем каждую строку в отдельный элемент массива. 
            string[] employees = File.ReadAllLines(filepath);
            Console.WriteLine("Список сотрудников:\n");
            Console.WriteLine("ID | Добавлен        | Имя                      | Возраст | Рост | Дата рож. | Место рождения");
            // парсинг каждой строки и вывод на экран
            foreach(string employee in employees)
            {
                if (employee != "")
                {
                    string[] entry = employee.Split('#');
                    Console.WriteLine($"{entry[0],3}| {entry[1],16}| {entry[2],25}| {entry[3],8}| {entry[4],3}см| {entry[5],10}| {entry[6]}");
                }

            }
        }
        /// <summary>
        /// Пошагово принимает данные о новом сотруднике для внесения его в базу
        /// </summary>
        /// <param name="filepath">путь к файлу "базы" справочника</param>
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

            if (File.Exists(filepath)) // высчитываем ID для новой записи = текущее количество строк в файле + 1
            {
                string[] temp = File.ReadAllLines(filepath); 
                newEntry[0] = Convert.ToString(temp.Length+1);

            }
            #region ввод данных
            Console.WriteLine("Введите имя нового сотрудника. *Только первые 25 символов имени будут сохранены:");
            newEntry[2] = Console.ReadLine();
            if (newEntry[2].Length > 25) newEntry[2] = newEntry[2].Substring(0,25);

            Console.WriteLine("\nВведите рост нового сотрудника в сантимертах:");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int hight))
                {
                    if (hight < 50 || hight > 250)
                    {
                        Console.WriteLine("Введенный рост должен быть между 50 см и 250 см");
                        continue;
                    }
                    else
                    {
                        newEntry[4] = Convert.ToString(hight);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный данные о росте.");
                }
                
            }

            // ввод дня рождения, высчитываем возраст
            Console.WriteLine("\nВведите дату рождения нового сотрудника в формате дд.мм.гггг:");
            while (true)
            {
                               
                if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDay))
                {
                                        
                    if (birthDay > DateTime.Now)
                    {
                        Console.WriteLine("Дата рождения должна быть в прошлом.");
                        continue;
                    }
                    newEntry[5] = birthDay.ToString("dd.MM.yyyy");
                    newEntry[3] = (Math.Round(((DateTime.Now - birthDay).TotalDays / 365.25), 0)).ToString();
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный формат времени.");
                }

            }
                        
            // Ввод города рождения
            Console.WriteLine("\nВведите место рождения нового сотрудника:");
            newEntry[6] = Console.ReadLine();
            if (newEntry[6].Length > 20) newEntry[6] = newEntry[6].Substring(0, 20);
           
            #endregion

            #region подтверждение и запись введенных данных в файл
            Console.WriteLine("\nВ справочник будет внесена следующая запись:");
            Console.WriteLine("\nИмя                      | Возраст | Рост | Дата рож. | Место рождения");
            Console.WriteLine($"{newEntry[2],25}| {newEntry[3],8}| {newEntry[4],3}см| {newEntry[5],10}| {newEntry[6]}");

            Console.WriteLine("\nЕсли хотите подтвердить внесение записи, нажмите Enter. Для отмены - нажмите Esc.");

            
            while (true)
            {

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                                        
                    string toAppend = Convert.ToInt32(newEntry[0]) == 1 ? newEntry[0] : "\n" + newEntry[0]; // если запись не первая, то переносим строку перед добавлением
                    // Дата добавления
                    newEntry[1] = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                    for (int i = 1; i<newEntry.Length;i++)
                    {
                        toAppend = toAppend + "#" + newEntry[i];
                    }

                    File.AppendAllText(filepath, toAppend); //добавление новой записи в файл
                    Console.WriteLine("\nЗапись внесена");
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
                // если нажали на что-то, кроме Enter или Esc, то это не будет принято 
            }
            #endregion

        }
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            // объявление переменных
            string filepath = "catalogue.txt";  // путь до файла, в котором хранится "Справочник"

            // основная логика
            
            while (true)
            {
                Console.WriteLine("Программа-справочник \"Сотрудники\".");
                Console.WriteLine("Нажмите 1 для вывода существующих записей. Нажмите 2 для добавления новой записи. Для выхода, нажмите Esc.");
                var key = Console.ReadKey(true).Key;
                if (((char)key) == '1')
                {
                    Console.Clear();
                    if (!File.Exists(filepath))
                    {
                        Console.WriteLine("Справочник пуст. Введите хотя бы одну запись, для просмотра.");
                        Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                        Console.ReadKey(true);
                        Console.Clear();
                        continue;
                    }
                    readEntrys(filepath);
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
                else if (((char)key) == '2')
                {
                    Console.Clear();
                    addEntry(filepath);
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }                
                else if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Программа закрывается..");
                    Thread.Sleep(1000);
                    break;
                }
                Console.Clear();
                // если нажали на что-то, кроме 1,2 или Esc, то это не будет принято 
            }
            
        }
    }
}
