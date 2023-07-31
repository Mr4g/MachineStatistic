

namespace MachineStatistic.test
{
    public class Tests
    {
        [Test]

        public void Test_AddStatusMachine_ValidStatus()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");

            // Act
            machine.AddStatusMachine("1");
            machine.AddStatusMachine("0");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("false");

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.AreEqual(new List<string> { "true", "false", "true", "false" }, machine.machineStatus);
        }

        [Test]
        
        public void Test_StatusValidation_ValidStatus()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");
            machine.AddStatusMachine("1");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");
            // Act
            machine.StatusValidation();
        }

        [Test]
 
        public void Test_StatusValidation_InvalidStatus_WaitingAndProducing()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");

            // Act & Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => machine.StatusValidation());
        }

        [Test]
        public void Test_StatusValidation_InvalidStatus_WaitingAndPartsNOK()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("false");

            // Act & Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => machine.StatusValidation());
        }
        [Test]
        public void Test_StatusValidation_InvalidStatus_PartsOKAndPartsNOK()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("true");
            machine.AddStatusMachine("true");

            // Act & Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => machine.StatusValidation());
        }

        [Test]
        public void Test_StatusValidation_InvalidStatus_AllFalse()
        {
            // Arrange
            MachineCUPP machine = new MachineCUPP(1, "DepartmentA");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");
            machine.AddStatusMachine("false");

            // Act & Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => machine.StatusValidation());
        }

    }
}

