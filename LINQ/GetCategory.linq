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
 GetCategories().Dump();
}

// You can define other methods, fields, classes and namespaces here

// method

public List<CategoryView> GetCategories()
{
	return Categories.Where(c => !c.RemoveFromViewFlag)
						.Select(c => new CategoryView
						{
							CategoryID = c.CategoryID,
							Description = c.Description,
							RemoveFromViewFlag = c.RemoveFromViewFlag
						}).ToList();
}


// view model

public class CategoryView
{
	public int CategoryID { get; set; }
	public string Description { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}