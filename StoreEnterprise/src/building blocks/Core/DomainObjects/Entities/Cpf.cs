using Core.DomainObjects.Util;

namespace Core.DomainObjects.Entities;

public class Cpf
{
    public const int CpfMaxLength = 11;
    public string Number { get; private set; }

    protected  Cpf() { }
    
    public Cpf(string number)
    {
        if (!Validate(number)) throw new DomainException("Invalid CPF");
        Number = number;
    }

    public static bool Validate(string cpf)
    {
        cpf = cpf.OnlyNumbers(cpf);
        
        if (cpf.Length != 11) return false;
        
        // Calculation of the first verifier digit
        int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 }; // weights for each digit
        string baseCpf = cpf.Substring(0, 9); // calculation base
        int sum1 = 0;
        for (int i = 0; i < 9; i++)
        {
            sum1 += int.Parse(baseCpf[i].ToString()) * weights1[i];
        }
        int dv1 = sum1 % 11 < 2 ? 0 : 11 - (sum1 % 11); // first verifier digit
        baseCpf += dv1;

        // Calculation of the second verifier digit
        int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 }; // weights for each digit
        int sum2 = 0;
        for (int i = 0; i < 10; i++)
        {
            sum2 += int.Parse(baseCpf[i].ToString()) * weights2[i];
        }
        int dv2 = sum2 % 11 < 2 ? 0 : 11 - (sum2 % 11); // second verifier digit

        // Check if the calculated verifier digits are equal to the ones informed in the CPF
        return cpf.EndsWith(dv1.ToString() + dv2.ToString());
        
    }
}