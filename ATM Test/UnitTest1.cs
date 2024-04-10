namespace ATM_Test
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void ChangePIN_WithValidPIN_ShouldUpdatePIN()
        {
            // Arrange
            var person = new Person(1, "Test Person");
            string newPin = "1234";

            // Act
            bool result = person.ChangePIN(newPin);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(person.CheckPIN(newPin));
        }

        [TestMethod]
        public void ChangePIN_WithInvalidPIN_ShouldNotUpdatePIN()
        {
            // Arrange
            var person = new Person(1, "Test Person");
            string invalidPin = "abc";
            // Act
            bool result = person.ChangePIN(invalidPin);
            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(person.CheckPIN(invalidPin));
        }

        [TestMethod]
        public void AddBill_ShouldAddBillSuccessfully()
        {
            // Arrange
            var person = new Person(1, "Test Person");
            decimal billAmount = 100.00M;
            // Act
            string billId = person.AddBill(billAmount);
            // Assert
            Assert.IsNotNull(billId);
            Assert.IsTrue(person.Bills.ContainsKey(billId));
            Assert.AreEqual(billAmount, person.Bills[billId]);
        }
        [TestMethod]
        public void CheckPIN_WithCorrectPIN_ShouldReturnTrue()
        {
            // Arrange
            var person = new Person(1, "Test Person");
            string originalPin = "1234";
            person.ChangePIN(originalPin);
            // Act
            bool isValid = person.CheckPIN(originalPin);
            // Assert
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void CheckPIN_WithIncorrectPIN_ShouldReturnFalse()
        {
            // Arrange
            var person = new Person(1, "Test Person");
            string incorrectPin = "0000";
            // Act
            bool isValid = person.CheckPIN(incorrectPin);
            // Assert
            Assert.IsFalse(isValid);
        }
    }
    [TestClass]
    public class ATMTests
    {
        [TestMethod]
        public void Deposit_ValidAmount_UpdatesBalanceCorrectly()
        {
            var person = new Person(1, "Test Person");
            var atm = new ATM();
            decimal depositAmount = 100m;

            bool depositResult = atm.Deposit(person, depositAmount);

            Assert.IsTrue(depositResult);
            Assert.AreEqual(depositAmount, person.Balance);
            Assert.AreEqual(depositAmount, atm.Balance);
        }
        [TestMethod]
        public void Withdraw_ValidAmount_UpdatesBalanceCorrectly()
        {
            // Arrange
            var person = new Person(1, "Test Person") { Balance = 200m }; 
            var atm = new ATM() { Balance = 500m };
            decimal withdrawAmount = 100m;

            // Act
            bool withdrawResult = atm.Withdraw(person, withdrawAmount);

            // Assert
            Assert.IsTrue(withdrawResult);
            Assert.AreEqual(100m, person.Balance);
            Assert.AreEqual(400m, atm.Balance);
        }
        [TestMethod]
        public void Service_AddFunds_IncreasesATMBalance()
        {
            // Arrange
            var atm = new ATM();
            decimal serviceAmount = 1000m;
            // Act
            atm.Service(serviceAmount);
            // Assert
            Assert.AreEqual(serviceAmount, atm.Balance);
        }
        [TestMethod]
        public void BankTransaction_ValidTransfer_UpdatesBothBalances()
        {
            // Arrange
            var sender = new Person(1, "Sender") { Balance = 500m };
            var receiver = new Person(2, "Receiver") { Balance = 100m };
            var atm = new ATM();
            // Act
            bool transactionResult = atm.BankTransaction(sender, receiver, 200m);
            // Assert
            Assert.IsTrue(transactionResult);
            Assert.AreEqual(300m, sender.Balance);
            Assert.AreEqual(300m, receiver.Balance);
        }
    }
}
