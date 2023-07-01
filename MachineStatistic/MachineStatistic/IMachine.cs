
using static MachineStatistic.MachineBase;

namespace MachineStatistic
{
    public interface IMachine
    {
        string EQ { get; }
        string Department { get; }

        void ManualGenerateDataFile(string status);
        void AddStatusMachine(bool status);

        void AddStatusMachine(int status);

        void AddStatusMachine(double status);

        void AddStatusMachine(long status);

        void AddStatusMachine(float status);

        void AddStatusMachne(char status);
        void AddStatusMachine(string status);

        event StatusAddedDelegate StatusAdded;
        Statistics GetStatistic(); 


    }
}
