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
        
        static bool fileExist(string filepath)
        {
            bool exists = File.Exists(filepath);
            return exists;
        }
        
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
                Console.WriteLine($"{entry[0],3}| {entry[1],16}| {entry[2],25}| {entry[3],8}| {entry[4],5}| {entry[5], 10}| {entry[6]}");
            }
        }

        static void addEntry()
        {
            Console.WriteLine("Введите имя нового сотрудника");

            Console.WriteLine("Введите рост нового сотрудника");

            Console.WriteLine("Введите дату рождения нового сотрудника");

            Console.WriteLine("Введите место рождения нового сотрудника");
        }
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // объявление переменных
            string filepath = "catalogue.txt";


            // основная логика
            Console.WriteLine("Программа-справочник \"Сотрудники\"");
            Console.WriteLine("Нажмите 1 для вывода существующих записей. Нажмите 2 для добавления новой записи");

            if (fileExist(filepath))
            {
                readEntrys(filepath);
            } 
            else
            {
                Console.WriteLine("Файл не существует, создаю файл");
                File.Create(filepath);
            }
            

            Console.ReadKey();


        }
    }
}
