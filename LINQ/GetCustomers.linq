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
	//GetCustomers("a").Dump();

	GetCustomer(5).Dump();
}

// You can define other methods, fields, classes and namespaces here

// method
public List<CustomerSearchView> GetCustomers(string lastname)
{

	return Customers
								.Where(x => x.LastName.Contains(lastname) && !x.RemoveFromViewFlag)
								.OrderBy(x => x.LastName)
								.Select(x => new CustomerSearchView
								{
									CustomerID = x.CustomerID,

									Name = x.FirstName + " " + x.LastName,
									ContactPhone = x.ContactPhone,
									Address = x.Address,
									RemoveFromViewFlag = x.RemoveFromViewFlag,

								})
								.ToList();
}


public CustomerSearchView GetCustomer(int customerid)
{
	return Customers.Where(c => c.CustomerID == customerid && !c.RemoveFromViewFlag)
						.Select(c => new CustomerSearchView
						{
							CustomerID = c.CustomerID,
							Name = c.FirstName + " " + c.LastName,
							Address = c.Address,
							ContactPhone = c.ContactPhone,
							RemoveFromViewFlag = c.RemoveFromViewFlag
						}).FirstOrDefault();

}




public class CustomerSearchView
{
	public int CustomerID { get; set; }
	public string Name { get; set; }

	public string Address { get; set; }

	public string ContactPhone { get; set; }

	public bool RemoveFromViewFlag { get; set; }

}
















