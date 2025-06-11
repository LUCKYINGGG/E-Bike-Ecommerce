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
	GetStandardJobs().Dump();
	GetStandardJob(5).Dump();
}

// You can define other methods, fields, classes and namespaces here

// Methods
public List<StandardJobsView> GetStandardJobs()
{
	return StandardJobs.Where(sj => !sj.RemoveFromViewFlag)
						.Select(sj => new StandardJobsView
						{
							StandardJobID = sj.StandardJobID,
							Description = sj.Description,
							StandardHours = sj.StandardHours,
							ExtPrice = sj.StandardHours * 65.5m,
							RemoveFromViewFlag = sj.RemoveFromViewFlag

						}).ToList();

}


public StandardJobsView GetStandardJob(int standardJobID)
{
	return StandardJobs.Where(sj => sj.StandardJobID == standardJobID && !sj.RemoveFromViewFlag)
						.Select(sj => new StandardJobsView
						{
							StandardJobID = sj.StandardJobID,
							Description = sj.Description,
							StandardHours = sj.StandardHours,
							ExtPrice = 65.5m * sj.StandardHours,
							RemoveFromViewFlag = sj.RemoveFromViewFlag
						}).FirstOrDefault();
}


// View Models

public class StandardJobsView
{
	public int StandardJobID { get; set; }
	public string Description { get; set; }
	public decimal StandardHours { get; set; }
	public bool RemoveFromViewFlag { get; set; }
	public decimal ExtPrice { get; set; }
	public const decimal Rate = 65.6m;
}
