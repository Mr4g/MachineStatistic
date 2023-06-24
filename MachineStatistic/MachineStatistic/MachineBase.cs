
namespace MachineStatistic
{
    public abstract class MachineBase : IMachine
    {
        public MachineBase(string eq, string department)
        {
            this.EQ = eq;
            this.Department = department;
        }
        public string EQ { get; private set; }
        public string Department { get; private set; }

        public abstract void AddStatusMachine(bool status);
        public abstract void AddStatusMachine(int status);
        public abstract void AddStatusMachine(double status);
        public abstract void AddStatusMachine(long status);
        public abstract void AddStatusMachine(float status);
        public abstract void AddStatusMachine(string status);
        public abstract void AddStatusMachne(char status);
        public abstract Statistics GetStatistic();
    }
}
