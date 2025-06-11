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
	GetParts(2).Dump();
	GetPart(102).Dump();
}

// You can define other methods, fields, classes and namespaces here

// method
public List<PartView> GetParts(int categoryid)
{
	return Parts.Where(p => p.CategoryID == categoryid && !p.RemoveFromViewFlag)

			.Select(p => new PartView
			{
				PartID = p.PartID,
				Description = p.Description,
				SellingPrice = p.SellingPrice,
				QuantityOnHand = p.QuantityOnHand,
				ReorderLevel = p.ReorderLevel,
				QuantityOnOrder = p.QuantityOnOrder,
				Refundable = p.Refundable,
				Discontinued = p.Discontinued,
				VendorID = p.VendorID,
				RemoveFromViewFlag = p.RemoveFromViewFlag,
				CategoryID = p.CategoryID

			}).ToList();
}

public PartView GetPart(int partID)
{
	return Parts.Where(p => p.PartID == partID && !p.RemoveFromViewFlag)
				.Select(p => new PartView
				{
					Description= p.Description,
					SellingPrice = p.SellingPrice,
					QuantityOnHand = p.QuantityOnHand,
					ReorderLevel=p.ReorderLevel,
					QuantityOnOrder =p.QuantityOnOrder,
					CategoryID=p.CategoryID,
					Refundable =p.Refundable,
					Discontinued=p.Discontinued,
					VendorID=p.VendorID,
					RemoveFromViewFlag =p.RemoveFromViewFlag

				}).FirstOrDefault();

}


// view model

public class PartView
{
	public int PartID { get; set; }
	public string Description { get; set; }
	public decimal SellingPrice { get; set; }
	public int QuantityOnHand { get; set; }
	public int ReorderLevel { get; set; }
	public int QuantityOnOrder { get; set; }
	public int CategoryID { get; set; }
	public string Refundable { get; set; }
	public bool Discontinued { get; set; }
	public int VendorID { get; set; }
	public bool RemoveFromViewFlag { get; set; }

}
