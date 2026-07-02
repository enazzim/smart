using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.Community.Popup
{
	/// <summary>
	/// ItemInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ItemInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btSearch;
		protected KIT_ERP.Community.ItemControl.ItemSearch ItemSearch1;
		private string MaterialsIndex;
		private string ProductIndex;
		private string DevelopmentIndex;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			MaterialsIndex = Request.QueryString["MaterialsIndex"];
			ProductIndex = Request.QueryString["ProductIndex"];
			DevelopmentIndex = Request.QueryString["DevelopmentIndex"];
			ItemSearch1.ChildPhantom = true;
			ItemSearch1.ChildProducts = true;
			ItemSearch1.ChildHalfFinishedProducts = true;
			ItemSearch1.ChildICommodity = true;
			ItemSearch1.ChildRawMaterials = true;

			
			if(!IsPostBack)
			{
				Label1.Text = "<품목정보>";
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand("select count(ItemInfoIndex) From II_MT where RecodingState = 1",Con);
				Con.Open();
				int RecordCount = int.Parse(Cmd.ExecuteScalar().ToString());
				Con.Close();
				DataGrid1.VirtualItemCount = RecordCount;
				DataGridBind();
			}
		}


		private void DataGridBind()
		{
			int PageNo, PageCount;

			PageNo = DataGrid1.CurrentPageIndex + 1;
			PageCount = (int)(DataGrid1.VirtualItemCount / DataGrid1.PageSize) + 1;

			int TopCount = PageNo * DataGrid1.PageSize;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"select Top "+TopCount.ToString()+ @" ItemNum, ItemDrawNum, ItemName, PropertyClassification, PUC_MT.SmallClassificationName as Unit, Standard, 
					case Texture WHEN 1 THEN '예' ELSE '아니오' END AS Texture,
					SupplementaryValueTaxRate,
					PUC_MT1.SmallClassificationName as StockUnit, 
					PUC_MT2.SmallClassificationName as BOMUnit, 
					PUC_MT3.SmallClassificationName as PurchaseUnit, 
					PUC_MT4.SmallClassificationName as SaleUnit, 
					case IOChackable WHEN 1 THEN '예' ELSE '아니오' END AS IOChackable, 
					case StockManagable WHEN 1 THEN '예' ELSE '아니오' END AS StockManagable, 
					case CheckDistinction WHEN 1 THEN '검사' ELSE '무검사' END AS CheckDistinction,  
					case OrderPlan WHEN 1 THEN '계산발주' ELSE '임의발주' END AS OrderPlan,
					PUC_MT5.SmallClassificationName as ItemType, 
					PUC_MT6.SmallClassificationName as MateralQuality, 
					ItemState, Maker, 
					PUC_MT7.SmallClassificationName as ItemClassification1, 
					PUC_MT8.SmallClassificationName as ItemClassification2, 
					PUC_MT9.SmallClassificationName as ItemClassification3,  
					PUC_MT10.SmallClassificationName as ItemClassification4, 
					Standard1, 
					PUC_MT11.SmallClassificationName as Unit1, 
					Standard2, 
					PUC_MT12.SmallClassificationName as Unit2, 
					Standard3, 
					PUC_MT13.SmallClassificationName as Unit3, 
					Standard4, 
					PUC_MT14.SmallClassificationName as Unit4, 
					ProductWeight, MaterialWeight, SupplyTerm, 
					case DomesticImportDistinction WHEN 1 THEN '내자' ELSE '외자' END AS DomesticImportDistinction, 
					SafetyStockQuantity, OrderIntervalQuantity, MinOrderQuantity, StiffenDeep, ProductHeatTreatmentDescription, MaterialHeatTreatmentDescription, 
					MaterialHeatTreatmentRequestDegree, CuttingSpace, TariffRate, StandardUnitCost, MainPurchaseCompany, MainOutSideOrderCompany, MainSaleCompany, ChargePerson, ItemInfoIndex 
					From II_MT
					left outer join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode 
					left outer join PUC_MT PUC_MT1 on II_MT.StockUnit = PUC_MT1.SmallClassificationCode
					left outer join PUC_MT PUC_MT2 on II_MT.BOMUnit = PUC_MT2.SmallClassificationCode
					left outer join PUC_MT PUC_MT3 on II_MT.PurchaseUnit = PUC_MT3.SmallClassificationCode
					left outer join PUC_MT PUC_MT4 on II_MT.SaleUnit = PUC_MT4.SmallClassificationCode
					left outer join PUC_MT PUC_MT5 on II_MT.ItemType = PUC_MT5.SmallClassificationCode
					left outer join PUC_MT PUC_MT6 on II_MT.MateralQuality = PUC_MT6.SmallClassificationCode
					left outer join PUC_MT PUC_MT7 on II_MT.ItemClassification1 = PUC_MT7.SmallClassificationCode
					left outer join PUC_MT PUC_MT8 on II_MT.ItemClassification2 = PUC_MT8.SmallClassificationCode
					left outer join PUC_MT PUC_MT9 on II_MT.ItemClassification3 = PUC_MT9.SmallClassificationCode
					left outer join PUC_MT PUC_MT10 on II_MT.ItemClassification4 = PUC_MT10.SmallClassificationCode
					left outer join PUC_MT PUC_MT11 on II_MT.Unit1 = PUC_MT11.SmallClassificationCode
					left outer join PUC_MT PUC_MT12 on II_MT.Unit2 = PUC_MT12.SmallClassificationCode
					left outer join PUC_MT PUC_MT13 on II_MT.Unit3 = PUC_MT13.SmallClassificationCode
					left outer join PUC_MT PUC_MT14 on II_MT.Unit4 = PUC_MT14.SmallClassificationCode
					Where II_MT.RecodingState = 1 and II_MT.ItemNum Like @ItemNum and II_MT.ItemDrawNum like @ItemDrawNum and II_MT.ItemName like @ItemName order by ItemNum";

			Cmd.Parameters.Add("@ItemNum", "%"+ItemSearch1.ChildItemNum+"%");
			Cmd.Parameters.Add("@ItemDrawNum", "%"+ItemSearch1.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ItemName", "%"+ItemSearch1.ChildItemName+"%");
	
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			int StartRecord = DataGrid1.CurrentPageIndex * DataGrid1.PageSize;
			adp.Fill(ds,StartRecord, DataGrid1.PageSize, "ItemInfo");

			adp.Fill(ds);
			DataGrid1.DataSource = ds;
			DataGrid1.DataMember = "ItemInfo";
			DataGrid1.DataBind();
		}

		#region Web Form 디자이너에서 생성한 코드
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		
		// 창닫기 버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.close();</script>");
		}



		// 등록 버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			bool count = false;    // 체크된 항목이 없을경우 사용하기 위해...
			
			foreach(DataGridItem item in DataGrid1.Items)
			{
				CheckBox cb = ((CheckBox)item.FindControl("CheckBox1"));
				if(cb.Checked)
				{
					count = true;					
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					Con.Open();
					SqlCommand Cmd = new SqlCommand();
					SqlTransaction trans = Con.BeginTransaction();
					Cmd.Connection = Con;
					Cmd.Transaction = trans;
					
					try
					{
						if(MaterialsIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('품목정보', "+ item.Cells[1].Text +", 'T_Materials', "+ MaterialsIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../MaterialsContent.aspx?seq=" +MaterialsIndex+ "'; self.close();  </script>");
						}
						else if(ProductIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('품목정보', "+ item.Cells[1].Text +", 'T_Product', "+ ProductIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../ProductContent.aspx?seq=" +ProductIndex+ "'; self.close();  </script>");
						}
						else
						{
							Cmd.CommandText = "INSERT CR_T VALUES('품목정보', "+ item.Cells[1].Text +", 'T_Development', "+ DevelopmentIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../DevelopmentContent.aspx?seq=" +DevelopmentIndex+ "'; self.close();  </script>");
						}
	
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
					}
				}				
			}
			
			// 체크된 항목이 없을경우...
			if(!count)
				Response.Write("<script>alert('선택된 내용이 없습니다.');</script>");
		}



		// 마우스 오버시 색깔...
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			DataGridBind();
		}

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			DataGrid1.CurrentPageIndex = 0;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"select Count(ItemInfoIndex) FROM II_MT Where II_MT.RecodingState = 1 and II_MT.ItemNum Like @ItemNum and II_MT.ItemDrawNum like @ItemDrawNum and II_MT.ItemName like @ItemName";
			Cmd.Parameters.Add("@ItemNum", "%"+ItemSearch1.ChildItemNum+"%");
			Cmd.Parameters.Add("@ItemDrawNum", "%"+ItemSearch1.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ItemName", "%"+ItemSearch1.ChildItemName+"%");
	
			
			Con.Open();
			int RecordCount = int.Parse(Cmd.ExecuteScalar().ToString());
			Con.Close();
			DataGrid1.VirtualItemCount = RecordCount;
			DataGridBind();
		}
	}
}
