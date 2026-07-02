using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// StoreHouseStockIndex에 대한 요약 설명입니다.
	/// </summary>
	public class StoreHouseStockIndex : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList dl_Store;
		protected System.Web.UI.WebControls.Label Label1;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_Company;
		protected System.Web.UI.WebControls.Button tb_Search;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnReset;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid StockState;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;


		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩
			ItemSearchControl1.RawMaterials = true;// 원자재
			ItemSearchControl1.HalfFinishedProducts = true;//반제품
			

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string str = @"select  CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex
								from CI_MT Where OutSideOrderCompany = 1 and RecodingState=1 order by CompanyName";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				wc_Company.DataSource = ds;
				wc_Company.DataTextField = ds.Tables[0].Columns[0].ToString();
				wc_Company.DataValueField = ds.Tables[0].Columns[2].ToString();
				wc_Company.DataBind();

			}
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
			this.dl_Store.SelectedIndexChanged += new System.EventHandler(this.dl_Store_SelectedIndexChanged);
			this.tb_Search.Click += new System.EventHandler(this.tb_Search_Click);
			this.UltraWebGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.UltraWebGrid1_ColumnMove);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.btnReset.ServerClick += new System.EventHandler(this.btnReset_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion




		private void tb_Search_Click(object sender, System.EventArgs e)
		{
			if( dl_Store.SelectedIndex == 0 )
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('창고를 선택해주세요!');");
				Response.Write("</script>");
			}
			else if(Label1.Visible == true)
			{
				UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
				UltraWebGrid1.DataSource = Search1();
				UltraWebGrid1.DataBind();
			}
			else
			{
				UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
			}
		}




		private DataSet Search()
		{
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				search = new Search("StoreHouseStockIndex",dl_Store.SelectedItem.Text,int.Parse( dl_Store.SelectedItem.Value ),ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,ItemSearchControl1.hdItem);
			}
			else
			{
				search = new Search("StoreHouseStockIndex",dl_Store.SelectedItem.Text,int.Parse( dl_Store.SelectedItem.Value ),ItemSearchControl1.ItemNum,"", "",ItemSearchControl1.hdItem);
			}			
			return search.DataSet_search();
		}

		private DataSet Search1()
		{

			//외주창고
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPOutStoreQuantity";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandTimeout = 240;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			comm.Parameters.Add("@HiddenItem",ItemSearchControl1.hdItem.Trim());
			if(Label1.Visible == true) 
				if(wc_Company.DisplayValue == null)
					comm.Parameters.Add("@BusinessRegistrationNum","");
				else
					comm.Parameters.Add("@BusinessRegistrationNum",Convert.ToString(wc_Company.DataValue).Trim());
			else
				comm.Parameters.Add("@BusinessRegistrationNum","");						
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			conn.Open();
			da.Fill(ds);
			conn.Close();
			return ds;		




/*
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				search = new Search("StoreHouseStockIndex",dl_Store.SelectedItem.Text,int.Parse( dl_Store.SelectedItem.Value ),wc_Company,ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName);
			}
			else
			{
				search = new Search("StoreHouseStockIndex",dl_Store.SelectedItem.Text,int.Parse( dl_Store.SelectedItem.Value ),wc_Company,ItemSearchControl1.ItemNum,"", "");
			}			
			return search.DataSet_search();
			
*/
		}




		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(Label1.Visible == true)
			{
				UltraWebGrid1.DataSource = Search1();
				UltraWebGrid1.DataBind();
			}
			else
			{
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
			}
		}



		// Excel 출력
		private void Button2_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg_Excel = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwg_Excel = StockState;
			uwg_Excel.DisplayLayout.Pager.AllowPaging = false;
			uwg_Excel.DataSource = ExcelSearch();	
			uwg_Excel.DataBind();
			UltraWebGridExcelExporter1.Export( uwg_Excel );
		}


		private DataSet ExcelSearch()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPExcelStoreHouseStock";

			if(Label1.Visible == true)
				str = "SPOutStoreQuantityExcel";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandTimeout = 240;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			comm.Parameters.Add("@HiddenItem",ItemSearchControl1.hdItem.Trim());
			if(Label1.Visible == true) 
				if(wc_Company.DisplayValue == null)
					comm.Parameters.Add("@BusinessRegistrationNum","");
				else
                    comm.Parameters.Add("@BusinessRegistrationNum",Convert.ToString(wc_Company.DataValue).Trim());
			else
				comm.Parameters.Add("@BusinessRegistrationNum","");
			
			if(Label1.Visible == false)
			{
				comm.Parameters.Add("@StoreName",dl_Store.SelectedItem.Text.Trim());
				comm.Parameters.Add("@Year",DateTime.Now.Year);
				//comm.Parameters.Add("@Year",2016);
			}
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			conn.Open();
			da.Fill(ds);
			conn.Close();
			return ds;			
		}
	

		private void UltraWebGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			tb_Search_Click(sender, e);
		}

		

		private void dl_Store_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(dl_Store.SelectedValue == "6")
			{
				Label1.Visible = true;
				wc_Company.Visible = true;

				UltraWebGrid1.Columns[10].Hidden = false;
				UltraWebGrid1.Columns[11].Hidden = false;


				StockState.Columns[12].Hidden = false;
				StockState.Columns[17].Hidden = false;
			}
			else
			{
				Label1.Visible = false;
				wc_Company.Visible = false;
				UltraWebGrid1.Columns[10].Hidden = true;
				UltraWebGrid1.Columns[11].Hidden = true;


				StockState.Columns[12].Hidden = true;
				StockState.Columns[17].Hidden = true;
			}
		}

		private void btnReset_ServerClick(object sender, System.EventArgs e)
		{
			dl_Store.SelectedIndex = 0;
			wc_Company.DataValue = "";
			Label1.Visible = false;
			wc_Company.Visible = false;
			ItemSearchControl1.ItemNum = "";			
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemName = "";
		}





	}
}
