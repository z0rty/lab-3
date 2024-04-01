using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        object collection = null;

        while (true)
        {
            Console.WriteLine("Выберите тип коллекции (1 - List, 2 - Dictionary): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                collection = new List<object>();
            }
            else if (choice == 2)
            {
                collection = new Dictionary<string, object>();
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
                continue;
            }

            while (true)
            {
                Console.WriteLine("Выберите действие (1 - добавить, 2 - удалить, 3 - редактировать, 4 - просмотреть, 5 - сортировать, 6 - сохранить в файл, 7 - загрузить из файла, 8 - выход): ");
                int action = Convert.ToInt32(Console.ReadLine());

                if (action == 1)
                {
                    // Добавление элемента
                    if (collection is List<object> list)
                    {
                        Console.WriteLine("Введите элемент для добавления: ");
                        object newItem = Console.ReadLine();
                        list.Add(newItem);
                        Console.WriteLine("Элемент добавлен в список.");
                    }
                    else if (collection is Dictionary<string, object> dictionary)
                    {
                        Console.WriteLine("Введите ключ элемента: ");
                        string key = Console.ReadLine();
                        Console.WriteLine("Введите значение элемента: ");
                        object value = Console.ReadLine();
                        dictionary[key] = value;
                        Console.WriteLine("Элемент добавлен в словарь.");
                    }
                }
                else if (action == 2)
                {
                    // Удаление элемента
                    if (collection is List<object> list)
                    {
                        Console.WriteLine("Введите элемент для удаления: ");
                        object itemToRemove = Console.ReadLine();
                        list.Remove(itemToRemove);
                        Console.WriteLine("Элемент удален из списка.");
                    }
                    else if (collection is Dictionary<string, object> dictionary)
                    {
                        Console.WriteLine("Введите ключ элемента для удаления: ");
                        string keyToRemove = Console.ReadLine();
                        dictionary.Remove(keyToRemove);
                        Console.WriteLine("Элемент удален из словаря.");
                    }
                }
                else if (action == 3)
                {
                    // Редактирование элемента
                    if (collection is List<object> list)
                    {
                        Console.WriteLine("Введите индекс элемента для редактирования: ");
                        int indexToEdit = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите новое значение: ");
                        object newValue = Console.ReadLine();
                        list[indexToEdit] = newValue;
                    }
                    else if (collection is Dictionary<string, object> dictionary)
                    {
                        Console.WriteLine("Введите ключ элемента для редактирования: ");
                        string keyToEdit = Console.ReadLine();
                        Console.WriteLine("Введите новое значение: ");
                        object newValue = Console.ReadLine();
                        dictionary[keyToEdit] = newValue;
                    }
                }
                else if (action == 4)
                {
                    // Просмотр элементов
                    Console.WriteLine("Элементы коллекции:");
                    if (collection is List<object> list)
                    {
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else if (collection is Dictionary<string, object> dictionary)
                    {
                        foreach (var pair in dictionary)
                        {
                            Console.WriteLine($"{pair.Key}: {pair.Value}");
                        }
                    }
                }
                else if (action == 5)
                {
                    // Сортировка
                    if (collection is List<object> list)
                    {
                        Console.WriteLine("Выберите алгоритм сортировки (1 - по возрастанию, 2 - по убыванию): ");
                        int sortChoice = Convert.ToInt32(Console.ReadLine());
                        if (sortChoice == 1)
                            list.Sort();
                        else if (sortChoice == 2)
                            list.Sort((a, b) => Comparer<object>.Default.Compare(b, a));
                    }
                    else
                    {
                        Console.WriteLine("Сортировка применима только к коллекции List.");
                    }
                }
                else if (action == 6)
                {
                    // Сохранение в файл
                    Console.WriteLine("Введите имя файла для сохранения: ");
                    string fileNameSave = Console.ReadLine() + ".txt";
                    SaveToFile(collection, fileNameSave);
                }
                else if (action == 7)
                {
                    // Загрузка из файла
                    Console.WriteLine("Введите имя файла для загрузки: ");
                    string fileNameLoad = Console.ReadLine() + ".txt";
                    collection = LoadFromFile(fileNameLoad);
                }
                else if (action == 8)
                {
                    // Выход
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Неверный выбор.");
                }
            }
        }
    }

    static void SaveToFile(object collection, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            if (collection is List<object> list)
            {
                foreach (var item in list)
                {
                    writer.WriteLine(item);
                }
            }
            else if (collection is Dictionary<string, object> dictionary)
            {
                foreach (var pair in dictionary)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
            else
            {
                Console.WriteLine("Невозможно сохранить коллекцию неизвестного типа.");
            }
        }

        Console.WriteLine("Коллекция сохранена в файле.");
    }

    static object LoadFromFile(string fileName)
    {
        object loadedCollection = null;

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length > 0 && lines[0].Contains(":"))
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (var line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        dictionary[parts[0].Trim()] = parts[1].Trim();
                    }
                }
                loadedCollection = dictionary;
            }
            else
            {
                loadedCollection = new List<object>(lines);
            }

            Console.WriteLine("Коллекция загружена из файла.");
        }
        else
        {
            Console.WriteLine("Файл не существует. Возвращена пустая коллекция.");
        }

        return loadedCollection;
    }
}