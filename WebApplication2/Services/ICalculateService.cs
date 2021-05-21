using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public interface ICalculateService
    {
        int Summ(int number1, int number2);
        int Subtraction(int number1, int number2);
        int Multiplication(int number1, int number2);
        int Division(int number1, int number2);

    }
}
