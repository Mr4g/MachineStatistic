

namespace MachineStatistic
{
    public class Vlidation
    {     
        public static int VlidationEq(string eq)
        {

            int eqNumber;
            bool validEq;
            validEq = int.TryParse(eq, out eqNumber) && eq.Length == 5;
            if (validEq == true) 
            {
                return eqNumber;
            }
            else
            {
                throw new Exception($"To EQ {eq} nie jest poprawne ..");
            }
        }

        public static string VlidationDepartment(string department) 
        {
            bool validDepartment = false;
            validDepartment = !string.IsNullOrEmpty(department) && department.All(c => char.IsLetter(c));
            if (validDepartment == true) 
            {
                department = department.ToUpper();   
                return department;
            }
            else
            {
                throw new Exception($"To jest {department} nieprawidłowy dział na którym znajduje się maszyna");
            }

        }
    }
}
