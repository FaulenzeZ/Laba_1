namespace Lab_1
{
    class Program // если num.Length будет равно 2 - вызвать метод, который принимает 2 аргумента, если размер 3 - вызвать другой метод, который примет с доп свойством
    {
        static void Main()
        {
            Console.WriteLine("Введите данные (Дата Номер [Регион]):");
            Console.WriteLine("Пример 1: 2024.05.15 А123ВС77");
            Console.WriteLine("Пример 2: 2024.05.15 А123ВС 77");

            string inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine)) return; 

            string[] input = inputLine.Split(" ");

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Trim();
            }
            CarPass carPass = new CarPass();

            if (input.Length == 2)
            {
                carPass.TwoIndex(input[0], input[1]);
            }
            else if (input.Length == 3)
            {
                carPass.ThreeIndex(input[0], input[1], input[2]);
            }
            else
            {
                Console.WriteLine("Неверное кол-во аргументов. Ожидалось 2 или 3!");
            }
            Console.WriteLine(carPass.Data);
            Console.WriteLine(carPass.Number);
            Console.WriteLine(carPass.Region);

        }
    }
}