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

using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.IO;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// ClassificationFind에 대한 요약 설명입니다.
	/// </summary>
	public class ClassificationFind : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Register;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Button Button3;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
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
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(File1.Value != "")
			{
				string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);
				string strFileDiv = FileName.ToString().Substring(FileName.LastIndexOf("."));

				if(strFileDiv.ToLower() != ".xls")// && strFileDiv != ".XLS")
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
				else
				{
					string upLoadFile = Path.GetTempFileName();
					// Temp 파일로 Excel 파일저장
					File1.PostedFile.SaveAs(upLoadFile);

					// Excel 자료를 DataSet 으로 받아낸다.
					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
					con.Open();
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ItemNum FROM [Sheet1$]", con);
					DataSet dsExcel = new DataSet();
					adapter.Fill(dsExcel);
					con.Close();

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					SqlCommand cmd = new SqlCommand();
					cmd.Connection = conn;
					cmd.CommandTimeout = 240;
					cmd.CommandText = "delete From ItemInfo";
					conn.Open();
					cmd.ExecuteNonQuery();
					conn.Close();

					foreach(DataRow row in dsExcel.Tables[0].Rows)
					{					
						
						cmd.CommandText = @"Insert into ItemInfo (ItemNum) values (@ItemNum) ";
						cmd.Parameters.Add("@ItemNum",row["ItemNum"].ToString().Trim());
						conn.Open();
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						conn.Close();
						
					}

					UltraWebGrid1.DataSource = DB_Regist();
					UltraWebGrid1.DataBind();
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}

		private DataSet DB_Regist()
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPNEWCreate";
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandTimeout = 240;
			cmd.CommandText = str;
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;

		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				RegisterStartupScript("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg = UltraWebGrid1;
				UltraWebGridExcelExporter1.Export(uwg);

			}
		}
	}
}
