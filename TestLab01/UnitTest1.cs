using TestableMethods;

namespace TestLab01
{
    [TestClass]
    public class VehicleUnitTests
    {
        Vehicle TestVehicle = new Vehicle();
    }

    [TestClass]
    public class VehicleTrackerUnitTests
    {
        Vehicle customerOne = new Vehicle("A01T22", true);

        public VehicleTracker vt = new VehicleTracker(10, "123 Fake St");

        [TestMethod]
        public void TestGenerateSlots()
        {
            vt.GenerateSlots();
            Assert.AreEqual(vt.Capacity + 1, vt.VehicleList.Count);
        }

        [TestMethod]
        public void AddVehicle_AddCustomerOneTovt()
        {
            vt.AddVehicle(customerOne);
            Assert.IsTrue(vt.VehicleList.ContainsValue(customerOne));
        }

        [TestMethod]
        public void AddVehicle_ThrowsArgumentExceptionWithSlotsFull()
        {
            for (int i = 0; i < vt.Capacity; i++)
            {
                vt.AddVehicle(new Vehicle());
            }
            vt.AddVehicle(customerOne);

            Assert.ThrowsException<IndexOutOfRangeException>(() => vt.AddVehicle(customerOne), VehicleTracker.SlotsFullMessage);
        }

        [TestMethod]
        public void TestRemoveVehicleByLicence()
        {
            vt.AddVehicle(customerOne);

            vt.RemoveVehicle(customerOne.Licence);

            Assert.IsNull(vt.GetVehicleByLicence(customerOne.Licence));
        }

        [TestMethod]
        public void RemoveVehicleByLicence_ThrowsArgumentException()
        {
            string licence = "NonExistingLicence";


            Assert.ThrowsException<NullReferenceException>(() => vt.RemoveVehicle(licence), VehicleTracker.BadSearchMessage);


        }

        [TestMethod]
        public void TestRemoveVehicleBySlotNumber()
        {
            vt.AddVehicle(customerOne);

            vt.RemoveVehicle(0);

            Assert.IsNull(vt.VehicleList[0]);
        }

        [TestMethod]
        public void TestParkedPassholders()
        {
            Vehicle customerOne = new Vehicle("A01T22", true);
            Vehicle customerTwo = new Vehicle("A01T23", true);
            Vehicle customerThree = new Vehicle("A01T24", false);
            Vehicle customerFour = new Vehicle("A01T25", false);

            vt.VehicleList.Clear();
            vt.VehicleList.Add(1, customerOne);
            vt.VehicleList.Add(2, customerTwo);
            vt.VehicleList.Add(3, customerThree);
            vt.VehicleList.Add(4, customerFour);

            List<Vehicle> vehicles = vt.ParkedPassholders();

            Assert.AreEqual(2, vehicles.Count);

        }

        [TestMethod]
        public void TestPassholderPercentage()
        {
            vt.GenerateSlots();

            vt.VehicleList[0] = customerOne;

            vt.SlotsAvailable--;

            int passHolderPercentage = vt.PassholderPercentage();

            Assert.AreEqual(10, passHolderPercentage);
        }
    }

}