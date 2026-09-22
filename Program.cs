using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace PatientRegistration
{
    struct Date
    {
        public int dd;
        public int mm;
        public int yyyy;

        public Date(int dd, int mm, int yyyy)
        {
            this.dd = dd;
            this.mm = mm;
            this.yyyy = yyyy;
        }
    }

    class Patient
    {
        public string passport;
        public string name;
        public Date birth_date;
        public string phone;
        public double temperature;
        public int loyal;

        public Patient(string passport, string name, Date birth_date, string phone, double temperature, int loyal)
        {
            this.passport = passport;
            this.name = name;
            this.birth_date = birth_date;
            this.phone = phone;
            this.temperature = temperature;
            this.loyal = loyal;
        }

        public override string ToString()
        {
            return $"Паспорт: {passport}\n" +
                   $"ФИО: {name}\n" +
                   $"Дата рождения: {birth_date.yyyy:0000}-{birth_date.mm:00}-{birth_date.dd:00}\n" +
                   $"Телефон: {phone}\n" +
                   $"Температура: {temperature:F2}\n" +
                   $"Лояльность: {loyal}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Регистрация пациента\n");

            string filePath = "patients.txt";
            List<Patient> patients = new List<Patient>();

            bool addMore;
            do
            {
                Patient patient = InputPatient();
                patients.Add(patient);
                Console.WriteLine("\nПациент успешно добавлен в базу.");

                addMore = AskYesNo("\nДобавить ещё одного пациента? (yes/no): ");

            } while (addMore);

            SaveToFile(patients, filePath);
            Console.WriteLine($"\nДанные сохранены в файл: {filePath}");

            Console.WriteLine("\n=== Список зарегистрированных пациентов ===");
            if (patients.Count == 0)
            {
                Console.WriteLine("Список пуст.");
            }
            else
            {
                for (int i = 0; i < patients.Count; i++)
                {
                    Console.WriteLine($"\n--- Пациент #{i + 1} ---");
                    Console.WriteLine(patients[i]);
                }
            }
            Console.WriteLine($"\nФайл сохранён по пути: {Path.GetFullPath(filePath)}");
            Console.ReadKey();
        }

        static void SaveToFile(List<Patient> patients, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Patient p in patients)
                {
                    sw.WriteLine($"{p.passport};{p.name};{p.birth_date.dd};{p.birth_date.mm};{p.birth_date.yyyy};{p.phone};{p.temperature}; {p.loyal}");
                }
            }
        }

        static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string answer = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (answer == "yes" || answer == "y" || answer == "да" || answer == "д")
                    return true;
                if (answer == "no" || answer == "n" || answer == "нет" || answer == "н")
                    return false;

                Console.WriteLine("Пожалуйста, введите yes или no.");
            }
        }

        static Patient InputPatient()
        {
            string passport = InputPassport();
            string name = InputName();
            Date birthDate = InputBirthDate();
            string phone = InputPhone();
            double temperature = InputTemperature();
            int loyal = Loyal();

            return new Patient(passport, name, birthDate, phone, temperature, loyal);
        }

        static string InputPassport()
        {
            string pattern = @"^\d{2}\s\d{2}-\d{6}$";
            string input;
            bool isValid;

            do
            {
                Console.Write("Введите паспорт (формат: ss ss-nnnnnn): ");
                input = Console.ReadLine()?.Trim() ?? "";
                isValid = Regex.IsMatch(input, pattern);

                if (!isValid)
                {
                    Console.WriteLine("Ошибка! Паспорт должен быть в формате: XX XX-XXXXXX (цифры)");
                }
            } while (!isValid);

            return input;
        }

        static string InputName()
        {
            string input;
            bool isValid;

            do
            {
                Console.Write("Введите ФИО пациента: ");
                input = Console.ReadLine()?.Trim() ?? "";
                isValid = !string.IsNullOrWhiteSpace(input) && input.Length >= 2;

                if (!isValid)
                {
                    Console.WriteLine("Ошибка! ФИО не может быть пустым и должно содержать минимум 2 символа");
                }
            } while (!isValid);

            return input;
        }

        static Date InputBirthDate()
        {
            string pattern = @"^(\d{4})-(\d{2})-(\d{2})$";
            string input;
            bool isValid;
            Date birthDate = new Date();

            do
            {
                Console.Write("Введите дату рождения (формат: YYYY-MM-DD): ");
                input = Console.ReadLine()?.Trim() ?? "";
                isValid = Regex.IsMatch(input, pattern);

                if (isValid)
                {
                    string[] parts = input.Split('-');
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);

                    try
                    {
                        DateTime date = new DateTime(year, month, day);

                        if (date > DateTime.Now)
                        {
                            isValid = false;
                            Console.WriteLine("Ошибка! Дата рождения не может быть в будущем");
                        }
                        else
                        {
                            birthDate = new Date(day, month, year);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        isValid = false;
                        Console.WriteLine("Ошибка! Некорректная дата. Проверьте правильность ввода");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка! Дата должна быть в формате YYYY-MM-DD (например, 1990-05-15)");
                }
            } while (!isValid);

            return birthDate;
        }

        static string InputPhone()
        {
            string pattern1 = @"^\+\d\(\d{3}\)\s\d{3}-\d{2}-\d{2}$";
            string pattern2 = @"^\d\(\d{3}\)\s\d{3}-\d{4}$";
            string input;
            bool isValid;

            do
            {
                Console.Write("Введите телефон (формат: +X(XXX) XXX-XX-XX или X(XXX) XXX-XXXX): ");
                input = Console.ReadLine()?.Trim() ?? "";
                isValid = Regex.IsMatch(input, pattern1) || Regex.IsMatch(input, pattern2);

                if (!isValid)
                {
                    Console.WriteLine("Ошибка! Телефон должен быть в формате: +X(XXX) XXX-XX-XX или X(XXX) XXX-XXXX");
                }
            } while (!isValid);

            return input;
        }
        static int Loyal()
        {
            int input;
            bool isValid;
            do
            {
                Console.Write("Ведите лояльность пациета от 0 до 100 включительно: ");
                input = int.Parse(Console.ReadLine());
                if (input <= 100 && input >= 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    Console.WriteLine("Введите от 0 до 100!");
                }
            } while (!isValid);
            return input;

        }

        static double InputTemperature()
        {
            string input;
            bool isValid;
            double temperature = 0;

            do
            {
                Console.Write("Введите температуру (формат: XX.XX): ");
                input = Console.ReadLine()?.Trim().Replace('.', ',') ?? "";
                isValid = double.TryParse(input, out temperature);

                if (isValid)
                {
                    if (temperature < 35.0 || temperature > 42.0)
                    {
                        isValid = false;
                        Console.WriteLine("Ошибка! Температура должна быть в диапазоне 35.0 - 42.0°C");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка! Введите корректное число (например, 36.6)");
                }
            } while (!isValid);

            return temperature;
        }
    }
}