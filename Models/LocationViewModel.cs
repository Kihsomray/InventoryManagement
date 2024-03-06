using InventoryManagement.Models;

public class LocationViewModel {
    public Location Location { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Inventory> Inventory { get; set; }
}
