
namespace MachineStatistic
{
    public interface IMachine
    {
        string EQ { get; }
        string Department { get; }  

        void AddStatusMachine(bool status);

        void AddStatusMachine(int status);

        void AddStatusMachine(double status);

        void AddStatusMachine(long status);

        void AddStatusMachine(float status);

        void AddStatusMachne(char status);
        void AddStatusMachine(string status);

        Statistics GetStatistic(); 


    }
}
