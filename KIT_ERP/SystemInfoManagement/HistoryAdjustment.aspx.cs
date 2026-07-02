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
using Microsoft.Win32;
using System.Configuration;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// HistoryAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class HistoryAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser Webdatechooser2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser WebDateChooser1;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 시스템정보 > 원장 정리';</script>");

//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionString").ToString());
//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string strSQL = @"SELECT DISTINCT HistoryDivision AS HistoryDivision FROM Arrangement_MT";

				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					ListItem item = new ListItem(reader["HistoryDivision"].ToString());
					DropDownList1.Items.Add(item);
				}
				DropDownList1.Items.Insert(0, "--선택하세요--");
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
			this.DropDownList1.SelectedIndexChanged += new System.EventHandler(this.DropDownList1_SelectedIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// TODO : 정리버튼 클릭시
		private void Button1_Click(object sender, System.EventArgs e)
		{
			string minDate = "";
			string maxDate = "";

			if(WebDateChooser1.Text.Trim() == "")
			{
				minDate = "1900-01-01";
			}
			else
			{
				minDate = WebDateChooser1.Text;
			}

			if(Webdatechooser2.Text.Trim() == "")
			{
				maxDate = DateTime.Now.ToShortDateString();
			}
			else
			{
				maxDate = Webdatechooser2.Text;
			}
			
			if(DropDownList2.Enabled)
			{
				
				if(DateTime.Parse(minDate).Ticks > DateTime.Parse(maxDate).Ticks)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('앞의 날짜가 뒷의 날짜보다 클수는 없습니다.');");
					Response.Write("history.back();");
					Response.Write("</script>");
					Response.End();
				}
				else
				{

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();

					string strSQL = "DELETE FROM " + DropDownList2.SelectedValue.ToString() + " WHERE RegistrationDate > @minDate AND RegistrationDate < @maxDate";

					//string strSQL = @"DELETE FROM " + DropDownList2.SelectedValue.ToString() + 
					//	" WHERE (ProgressCondition = '완료' OR ProgressCondition = '중단') AND UpdatingDate > @minDate AND UpdatingDate < @maxDate";

					SqlCommand comm = new SqlCommand(strSQL, conn);
					comm.Parameters.Add("@minDate", DateTime.Parse(minDate));
					comm.Parameters.Add("@maxDate", DateTime.Parse(maxDate));
					comm.ExecuteNonQuery();

					Response.Write("<script language=javascript>");
					Response.Write("alert('원장이 정리되었습니다.');");
					Response.Write("window.open('HistoryAdjustment.aspx', 'Contents', '');");
					Response.Write("</script>");
					Response.End();
					
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('정리할 원장을 선택하세요.');");
				Response.Write("window.open('HistoryAdjustment.aspx', 'Contents', '');");
				Response.Write("</script>");
				Response.End();
			}
		}

		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropDownList1.SelectedIndex == 0)
			{
				DropDownList2.Enabled = false;
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string strSQL = @"SELECT HistoryName, TableName FROM Arrangement_MT WHERE HistoryDivision = @HistoryDivision";

				SqlCommand comm = new SqlCommand(strSQL, conn);
				comm.Parameters.Add("@HistoryDivision", DropDownList1.SelectedItem.Text);

				SqlDataAdapter adapter = new SqlDataAdapter(comm);
				
				DataSet ds = new DataSet();

				adapter.Fill(ds);				

				DropDownList2.DataSource = ds;
				DropDownList2.DataTextField = ds.Tables[0].Columns["HistoryName"].ToString();
				DropDownList2.DataValueField = ds.Tables[0].Columns["TableName"].ToString();
				DropDownList2.DataBind();
				
				DropDownList2.Enabled = true;
			}
		}
	}
}
