using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_1
{
    public class CarPass
    {
        public string Data {  get; set; }
        public string Number { get; set; }
        public string Region { get; set; }

        public CarPass() { }

        public CarPass(string data, string number)
        {
            Data = data;
            Number = number;
        }
        public void TwoIndex(string data,  string fullNumber)
        {
            Console.WriteLine($"Дата: {Data} Номер_Регион: {Number}");
        }
        public void ThreeIndex(string data, string fullNumber, string region)
        {
            Console.WriteLine($"Дата: {data} Номер: {fullNumber} Регион: {region}");

        }
    }
}
