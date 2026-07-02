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

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// ItemGroup에 대한 요약 설명입니다.
	/// </summary>
	public class ItemGroup : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnExcel;
		//protected KIT_ERP.ProductionManagement.PopupWindows.dsPlanQuantityResult dsPlanQuantityResult1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser Webdatechooser1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				DataSet dsPlanQuantityResult1 = (KIT_ERP.ProductionManagement.dsPlanQuantityResult)Session["dataset"];
				UltraWebGrid1.DataSource = dsPlanQuantityResult1;
				UltraWebGrid1.DataBind();	
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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancle_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.close();</script>");
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// 생산계획원장에 등록
			// ItemGroup테이블에 진행상태 완료로 변경
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			conn.Open();
			string str = "";

			str = "Select isnull(Max(VolumNum),0) From PP_HT";
			comm.CommandText = str;
			int vol = int.Parse(comm.ExecuteScalar().ToString())+1;

			for(int a = 0; a< UltraWebGrid1.Rows.Count ; a++)
			{
				str=@"Insert into PP_HT (ItemNum, ItemDrawNum, ItemName, ProductionPlanHistorySourceCode, ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate, RowMaterialCalculation, VolumNum, MaterialRequirementVolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, HistorySection) values(@itemnum, @itemdrawnum,@itemname, '03200030',	'기종생산', @total,	@begindate,	@DeliveryDate, '0',	@num, '0', '대기', @Person, @PersonID, @Date, '기종등록')";
				
				comm.CommandText = str;
				comm.Parameters.Add("@itemnum",UltraWebGrid1.Rows[a].Cells[0].Text);
				comm.Parameters.Add("@itemdrawnum",DrawNum(UltraWebGrid1.Rows[a].Cells[0].Text));
				comm.Parameters.Add("@itemname",UltraWebGrid1.Rows[a].Cells[1].Text);
				comm.Parameters.Add("@total",decimal.Parse(UltraWebGrid1.Rows[a].Cells[7].Text));
				if(UltraWebGrid1.Rows[a].Cells[9].Text == null)
					comm.Parameters.Add("@begindate",DateTime.Now.ToShortDateString());
				else
                    comm.Parameters.Add("@begindate",DateTime.Parse(UltraWebGrid1.Rows[a].Cells[9].Text).ToShortDateString());

				if(UltraWebGrid1.Rows[a].Cells[8].Text == null)
					comm.Parameters.Add("@DeliveryDate",DateTime.Now.ToShortDateString());
				else
					comm.Parameters.Add("@DeliveryDate",DateTime.Parse(UltraWebGrid1.Rows[a].Cells[8].Text).ToShortDateString());
				comm.Parameters.Add("@num",vol);		
				comm.Parameters.Add("@Person",Session["UserName"].ToString());
				comm.Parameters.Add("@PersonID",Session["ID"].ToString());				
				comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());

				if(decimal.Parse(UltraWebGrid1.Rows[a].Cells[7].Text) > 0)
					comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}

			str = @"Update ItemGroup_HT Set ProgressCondition = '완료'";
			comm.CommandText= str;
			comm.CommandType = CommandType.Text;
			comm.ExecuteNonQuery();

			Page.RegisterClientScriptBlock("SEND", "<script>Search()</script>");
			RegisterStartupScript("","<script>window.close();</script>");
		}

		private string DrawNum(string ItemNum)
		{
			string Draw = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			conn.Open();
			string str = "Select ItemDrawNum From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);

			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Draw = dr["ItemDrawNum"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();
			conn.Close();

			return Draw;

		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}
	}
}
