namespace Lab_1
{
    class Program // если num.Length будет равно 2 - вызвать метод, который принимает 2 аргумента, если размер 3 - вызвать другой метод, который примет с доп свойством
    {
        static void Main()
        {
            Console.WriteLine("Введите дату и номер автомобиля, например, дату в формате: гггг.мм.дд номер в формаете: \"А000АА000\"");
            string[] input = Console.ReadLine().Split(" ");
            string index;
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].Trim();
            }
            CarPass carPass = new CarPass();
            string data = input[0];
            string number = input[1];
            
            if (input.Length == 2)
            {
                carPass.TwoIndex(data, number);
            }
            else if (input.Length == 3)
            {
                string region = input[2];
                carPass.ThreeIndex(data, number, region);
            }
            else
            {
                Console.WriteLine("Неверное кол-во аргументов. Ожидалось 2 или 3!");
            }

        }
    }
}