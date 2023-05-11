
static void Main(string[] args)
{

}

namespace TestableMethods
{
    public class Vehicle
    {
        public string Licence { get; set; }
        public bool Pass { get; set; }
        public Vehicle() { }
        public Vehicle(string licence, bool pass)
        {
            this.Licence = licence;
            this.Pass = pass;
        }
    }

    public class VehicleTracker
    {
        //PROPERTIES
        public string Address { get; set; }
        public int Capacity { get; set; }
        public int SlotsAvailable { get; set; }
        public Dictionary<int, Vehicle> VehicleList { get; set; }
        public VehicleTracker(int capacity, string address)
        {
            this.Capacity = capacity;
            this.Address = address;
            this.VehicleList = new Dictionary<int, Vehicle>();

            this.GenerateSlots();
        }

        // STATIC PROPERTIES
        public static string BadSearchMessage = "Error: Search did not yield any result.";
        public static string BadSlotNumberMessage = "Error: No slot with number ";
        public static string SlotsFullMessage = "Error: no slots available.";

        // METHODS
        public void GenerateSlots()
        {
            this.VehicleList.Clear();
            for (int i = 0; i <= this.Capacity; i++)
            {
                this.VehicleList.Add(i, null);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList)
            {
                if (slot.Value == null)
                {
                    this.VehicleList[slot.Key] = vehicle;
                    this.SlotsAvailable++;
                    return;
                }
            }
            throw new IndexOutOfRangeException(SlotsFullMessage);
        }

        public Vehicle GetVehicleByLicence(string licence)
        {
            return VehicleList.Values.FirstOrDefault(v => v?.Licence == licence);
        }

        public int GetSlotNumberByLicence(string licence)
        {
            foreach (KeyValuePair<int, Vehicle> kvp in VehicleList)
            {
                if (kvp.Value?.Licence == licence)
                {
                    return kvp.Key;
                }
            }
            return -1;
        }
        public void RemoveVehicle(string licence)
        {
            try
            {
                int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
                this.SlotsAvailable--;
                this.VehicleList[slot] = null;
            }
            catch
            {
                throw new NullReferenceException(BadSearchMessage);
            }
        }

        public bool RemoveVehicle(int slotNumber)
        {
            if (slotNumber > this.Capacity)
            {
                return false;
            }
            this.VehicleList[slotNumber] = null;
            this.SlotsAvailable++;
            return true;
        }

        public List<Vehicle> ParkedPassholders()
        {
            List<Vehicle> passHolders = new List<Vehicle>();
            //passHolders.Add(this.VehicleList.FirstOrDefault(v => v.Value.Pass).Value);

            foreach (KeyValuePair<int, Vehicle> kvp in this.VehicleList)
            {
                if (kvp.Value != null && kvp.Value.Pass)
                {
                    passHolders.Add(kvp.Value);
                }
            }
            return passHolders;
        }

        public int PassholderPercentage()
        {
            int passHolders = ParkedPassholders().Count();
            //int percentage = (passHolders / this.Capacity) * 100;

            double percentage = ((double)passHolders / this.Capacity) * 100;

            return (int)percentage;
        }
    };
}


