<Query Kind="Program">
  <Connection>
    <ID>bf450f11-4bbd-47f8-b936-ec2eeef483d8</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>UNDEADPUNK-LP</Server>
    <Database>eBike_2025</Database>
    <DisplayName>eBike_2025-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <TrustServerCertificate>True</TrustServerCertificate>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//create sales and returns LINQ settings here
}

// You can define other methods, fields, classes and namespaces here

#region Methods
public List<CustomerSalesSearchView> GetCustomers(string phoneNumber)
{
	return Customers.Where(x => x.ContactPhone.Contains(phoneNumber) && !x.RemoveFromViewFlag)
		   .OrderBy(x => x.LastName)
		   .Select(x => new CustomerSalesSearchView
		   {
		   		CustomerID = x.CustomerID,
				FullName = x.FirstName + " " + x.LastName,
				PhoneNumber = x.ContactPhone,
				Address = x.Address,
			   RemoveFromViewFlag = x.RemoveFromViewFlag
				
		   })
		   .ToList();
}

public CustomerSalesSearchView GetCustomers(int customerID)
{
	return Customers.Where(x => x.CustomerID == customerID && !x.RemoveFromViewFlag)
					.Select(x => new CustomerSalesSearchView
					{
						CustomerID = x.CustomerID,
						FullName = x.FirstName + " " + x.LastName,
						PhoneNumber = x.ContactPhone,
						Address = x.Address
					}).FirstOrDefault();
}


public List<CustomerPartsView> GetOrderedParts(int purchaseOrderDetailId)
{
	return PurchaseOrderDetails.Where(x => x.PurchaseOrderDetailID == purchaseOrderDetailId && !x.RemoveFromViewFlag)
	.Select(x => new CustomerPartsView
	{
		PurchaseOrderDetailID = x.PurchaseOrderDetailID,
		PartID = x.PartID,
		Quantity = x.Quantity,
		RemoveFromViewFlag = x.RemoveFromViewFlag,
		Parts = x.Parts.Select(pt => new PartsView 
		{
			Description = pt.Description,
			CategoryID = pt.CategoryID,
			RemoveFromViewFlag = pt.RemoveFromViewFlag
		}).ToList()
	}).ToList();
}

public CustomerPartsView GetOrderedParts(string description, int purchaseOrderDetailId)
{
	return PurchaseOrderDetails.Where(x => x.Part.Description.Equals(description) && x.PurchaseOrderDetailID == purchaseOrderDetailId && !x.RemoveFromViewFlag)
	.Select(x => new CustomerPartsView
	{
		PurchaseOrderDetailID = x.PurchaseOrderDetailID,
		PartID = x.PartID,
		Quantity = x.Quantity,
		RemoveFromViewFlag = x.RemoveFromViewFlag,
		Parts = x.Parts.Select(pt => new PartsView
		{
			Description = pt.Description,
			CategoryID = pt.CategoryID,
			RemoveFromViewFlag = pt.RemoveFromViewFlag
		}).ToList()
	}).FirstOrDefault();
}

#endregion

#region Views
public class CustomerSalesSearchView
{
	public int CustomerID { get; set; }
	public string FullName { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class CustomerPartsView
{
	public int PurchaseOrderDetailID { get; set; }
	public int PartID { get; set; }
	public int Quantity { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public List<PartsView> Parts { get; set; } = new();
}

public class PartsView
{
	public string Description { get; set; }
	public int CategoryID { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}
#endregion