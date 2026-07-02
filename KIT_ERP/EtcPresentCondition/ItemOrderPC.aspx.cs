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

using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;


namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// ItemOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class ItemOrderPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
		protected System.Web.UI.WebControls.Button btItemSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UWGExcel;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				for(int a = 0; a<=ddlYear.Items.Count; a++) 
				{
					if(ddlYear.Items[a].Value == DateTime.Now.Year.ToString())
					{
						ddlYear.SelectedIndex = a;
						break;
					}					
				}

				ddlMonth.SelectedIndex = DateTime.Now.Month-1;       
				
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
			this.btItemSearch.Click += new System.EventHandler(this.btItemSearch_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btItemSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			
		}

		private DataSet Search()
		{

			DataTable dt = new DataTable();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPItemOrderPC";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;	
			comm.CommandType=CommandType.StoredProcedure;
			comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value);
			comm.Parameters.Add("@Month",ddlMonth.SelectedItem.Value);
			conn.Open();			
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet();
			da.Fill(ds);

			conn.Close();

			return ds;
			
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.DataSource = Search();
			uwg.DataBind();
			UWGExcel.Export(uwg);
		}
	}
}
