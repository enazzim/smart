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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// Recognition에 대한 요약 설명입니다.
	/// </summary>
	public class Recognition : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnRecognition;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;
			CSC1.UnitCostDistinction ="구매외주";

			if(!Page.IsPostBack)
			{
				//사용자 정보 테이블에서 사용자의 등급을 찾아서 그 등급에 맞는 페이지만을 보여준다.
				SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				con.Open();
				string grade = "";
				string str = "Select UserRank From UI_MT Where RecodingState=1 and ID=@id";
				SqlCommand comm = new SqlCommand(str,con);
				comm.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					grade = dr["UserRank"].ToString().Substring(0,2).Trim();
				}
				dr.Close();
				con.Close();

				if(grade == "00" || grade == "01")
				{
					btnRecognition.Enabled = true;
					btnCancel.Enabled = true;
				}

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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.DataGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnRecognition.Click += new System.EventHandler(this.btnRecognition_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			DataGrid1.DataSource = Search();
			DataGrid1.DataBind();
		}

		private DataSet Search()
		{

			SqlDataAdapter adap = new SqlDataAdapter("SPRecognition", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			if(ItemSearchControl1.hdItem.Trim() != "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			adap.SelectCommand.Parameters.Add("@CompanyName",CSC1.Company.Trim());
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum.Trim());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcStartDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@EndDate",wdcEndDate.Text.Trim());						
			adap.SelectCommand.Parameters.Add("@Division",ddlState.SelectedItem.Value.Trim());
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

/*

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPRecognition",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			adap.SelectCommand.Parameters.Add("@CompanyName",CSC1.Company.Trim());
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum.Trim());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcStartDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@EndDate",wdcEndDate.Text.Trim());						
			adap.SelectCommand.Parameters.Add("@Division",ddlState.SelectedItem.Value.Trim());
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);
*/
			return ds;
		}

		private void btnRecognition_Click(object sender, System.EventArgs e)
		{

			if (DataGrid1.Rows.Count == 0)
			{
				Response.Write("<script>alert('승인할 항목이 없습니다.');</script>");
			}
			else
			{
				bool bRowUpdate = false;
				bool test = false;
				
				for( int i = 0 ; i < DataGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( DataGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						
						string[] strArrUdValue = new string[2];
						strArrUdValue[0] = DataGrid1.Rows[i].Cells.FromKey("Division").Value.ToString();			// 거래처등록번호
						strArrUdValue[1] = DataGrid1.Rows[i].Cells.FromKey("HistoryIndex").Value.ToString();						// 발주수량
							
						bRowUpdate = DB_Update(strArrUdValue,"승인") ;						
					}
				}
				
				if ( bRowUpdate )
				{
					if(!test)
						Response.Write("<script>alert('승인 되었습니다.')</script>");
				}

				DataGrid1.DataSource = Search();
				DataGrid1.DataBind();
				
			}

			
			
		}

		private bool DB_Update( string[] strArrUdValue, string Recognition)
		{
			bool Up = false;
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPRecognition_Update";
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = str;
			SqlTransaction tran = null;

			cmd.Parameters.Add("@Division", SqlDbType.VarChar).Value = strArrUdValue[0];
			cmd.Parameters.Add("@HistoryIndex", SqlDbType.Int).Value = int.Parse(strArrUdValue[1]);
			cmd.Parameters.Add("@Recognition", SqlDbType.VarChar).Value = Recognition;
			
			try
			{
				con.Open();
				tran = con.BeginTransaction();
				cmd.Transaction = tran;

				cmd.ExecuteNonQuery();
				
				
				tran.Commit();
				Up = true;
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				con.Close();
				
			}
			return Up;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count == 0)
			{
				Response.Write("<script>alert('취소할 항목이 없습니다.');</script>");
			}
			else
			{
				bool bRowUpdate = false;
				bool test = false;
				
				for( int i = 0 ; i < DataGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( DataGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						
						string[] strArrUdValue = new string[2];
						strArrUdValue[0] = DataGrid1.Rows[i].Cells.FromKey("Division").Value.ToString();			// 거래처등록번호
						strArrUdValue[1] = DataGrid1.Rows[i].Cells.FromKey("HistoryIndex").Value.ToString();						// 발주수량
							
						bRowUpdate = DB_Update(strArrUdValue,"취소") ;						
					}
				}
				
				if ( bRowUpdate )
				{
					if(!test)
						Response.Write("<script>alert('취소 되었습니다.')</script>");
				}
				else
					Response.Write("<script>alert('선택된 행이 없습니다.')</script>");	

				DataGrid1.DataSource = Search();
				DataGrid1.DataBind();
				
			}

			
			
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPRecognition",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			adap.SelectCommand.Parameters.Add("@CompanyName",CSC1.Company.Trim());
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum.Trim());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcStartDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@EndDate",wdcEndDate.Text.Trim());						
			adap.SelectCommand.Parameters.Add("@Division",ddlState.SelectedItem.Value.Trim());
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			DataSet ds = new DataSet();
			
			adap.Fill(ds);

			DataGrid1.DisplayLayout.Pager.AllowPaging = false;
			DataGrid1.Columns.FromKey("chk").Hidden = true;
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataBind();
			uwgExcel.Export(DataGrid1);
		}

		private void DataGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.DataGrid1.DataSource = Search();
			DataGrid1.DataBind();
		}
	}
}
