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
using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// RealBom에 대한 요약 설명입니다.
	/// </summary>
	public class RealBom : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				UltraWebGrid1.DataSource = Databind();
				UltraWebGrid1.DataBind();
			}
		}

		private DataTable Databind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "RealBom";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Connection = conn;
			comm.Parameters.Add("@ItemNum",Request.QueryString["ItemNum"].ToString().Trim());

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("ItemNum", typeof(string));//품목번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemDrawNum", typeof(string));//도면번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("Unit", typeof(string));//단위
			dt.Columns.Add(dc);
			dc = new DataColumn("Standard", typeof(string));//규격
			dt.Columns.Add(dc);
			dc = new DataColumn("MateralQuality", typeof(string));//재질
			dt.Columns.Add(dc);
			dc = new DataColumn("NeedQuantity", typeof(double));//필요량
			dt.Columns.Add(dc);
			

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				DataRow drow = dt.NewRow();
				drow["ItemNum"] = dr["ItemNum"].ToString();
				drow["ItemDrawNum"] = dr["ItemDrawNum"].ToString();
				drow["ItemName"] = dr["ItemName"].ToString();
				drow["Unit"] = Unit(dr["ItemNum"].ToString().Trim());
				drow["Standard"] = Standard(dr["ItemNum"].ToString().Trim());
				drow["MateralQuality"] = MateralQuality(dr["ItemNum"].ToString().Trim());
				drow["NeedQuantity"] = decimal.Parse(dr["Quantity"].ToString().Trim());
				dt.Rows.Add(drow);
			}
			

			return dt;
		}

		private string Unit(string ItemNum)
		{
			string Unit = "";
			string str = "Select SmallClassificationName From II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlCommand comm = new SqlCommand(str,conn);
			conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Unit = dr["SmallClassificationName"].ToString();
			}
			conn.Close();

			return Unit;

		}

		private string Standard(string ItemNum)
		{
			string Standard = "";
			string str = "Select Standard From II_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlCommand comm = new SqlCommand(str,conn);
			conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Standard = dr["Standard"].ToString();
			}
			conn.Close();

			return Standard;
		}

		private string MateralQuality(string ItemNum)
		{
			string MateralQuality = "";
			string str = "Select  SmallClassificationName From II_MT inner join PUC_MT on MateralQuality = SmallClassificationCode Where II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1and ItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlCommand comm = new SqlCommand(str,conn);
			conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				MateralQuality = dr["SmallClassificationName"].ToString();
			}
			conn.Close();

			return MateralQuality;
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}
	}
}
