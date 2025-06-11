<Query Kind="Program">
  <Connection>
    <ID>75cf5d86-aa57-4001-9bd9-3fbbf87ea44f</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.\SQLEXPRESS</Server>
    <Database>eBike_2025</Database>
    <DisplayName>eBike_2025-Entity</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	GetCustomerVehicles(5).Dump();

	GetCustomerVehicle("321ZX147K4289", 5).Dump();
}

// You can define other methods, fields, classes and namespaces here


// method
public List<CustomerVehicleView> GetCustomerVehicles(int customerid)
{
	return CustomerVehicles.Where(cv => cv.CustomerID == customerid && !cv.RemoveFromViewFlag)
							.Select(cv => new CustomerVehicleView
							{
								Vin = cv.VehicleIdentification,
								CustomerID = cv.CustomerID,
								MakeModel = cv.Make.Trim() + ", " + cv.Model,
								RemoveFromViewFlag = cv.RemoveFromViewFlag
							}).ToList();
}


public CustomerVehicleView GetCustomerVehicle(string vin, int customerid)
{
	return CustomerVehicles
			.Where(cv => cv.CustomerID == customerid && cv.VehicleIdentification.Equals(vin) && !cv.RemoveFromViewFlag)
			.Select(cv => new CustomerVehicleView
			{
				Vin = cv.VehicleIdentification,
				CustomerID = cv.CustomerID,
				MakeModel = cv.Make.Trim() + ", " + cv.Model,
				RemoveFromViewFlag = cv.RemoveFromViewFlag

			}).FirstOrDefault();

}


public class CustomerVehicleView
{
	public string Vin { get; set; }
	public int CustomerID { get; set; }
	public string MakeModel { get; set; }
	public bool RemoveFromViewFlag { get; set; }

}
