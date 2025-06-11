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
	// sales coupon
	GetCoupon("TopHat").Dump();
	// expired coupon
	GetCoupon("OilChg02").Dump();
	GetCoupon("Tune02").Dump();
	
	GetCoupon("Save20").Dump();
	// valid coupon
	GetCoupon("Save10").Dump();
	
}

// You can define other methods, fields, classes and namespaces here

// method
public CouponView GetCoupon(string couponValue)
{
	return Coupons.Where(c => c.CouponIDValue == couponValue && !c.RemoveFromViewFlag
					 && DateTime.Today >= c.StartDate   && DateTime.Today <= c.EndDate
					)
					.Select(c => new CouponView
					{
						CouponIDValue = c.CouponIDValue,
						StartDate = c.StartDate,
						EndDate = c.EndDate,
						CouponDiscount = c.CouponDiscount,
						SalesOrService = c.SalesOrService,
						RemoveFromViewFlag = c.RemoveFromViewFlag
					})
					.FirstOrDefault();
}






//view model 

public class CouponView
{
	public int CouponID { get; set; }
	public string CouponIDValue { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public int CouponDiscount { get; set; }
	public int SalesOrService { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}




