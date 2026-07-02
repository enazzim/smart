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
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.SystemInfoManagement.PopupWindows
{
	/// <summary>
	/// PostSearch에 대한 요약 설명입니다.
	/// </summary>
	public class PostSearch : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ListBox ListBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
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
			this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string State = Request["id"].ToString();
			string Post1 = ListBox1.SelectedItem.ToString().Substring(0,3);
			string Post2 = ListBox1.SelectedItem.ToString().Substring(4,3);
			string Address = ListBox1.SelectedItem.ToString().Substring(8);

			Page.RegisterClientScriptBlock("SEND", "<script>Close('" +  State + "','" + Post1 + "','" + Post2 + "','" + Address + "');</script>");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(TextBox1.Text == null || TextBox1.Text == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('동(읍/면/리)을 입력하세요.');");
				Response.Write("history.back();");
				Response.Write("</script>");
			}
			else
			{
				ListBox1.Items.Clear();

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

				string str = "SELECT * FROM ZipCode Where Dong LIKE '%" + TextBox1.Text + "%'";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet dsSet = new DataSet();
				da.Fill(dsSet);

				for(int i = 0; i < dsSet.Tables[0].Rows.Count; i++)
				{
					ListBox1.Items.Add(dsSet.Tables[0].Rows[i]["Zipcode"].ToString() + " " + dsSet.Tables[0].Rows[i]["SiDo"].ToString() + " " + dsSet.Tables[0].Rows[i]["GUGUN"].ToString() + " " + dsSet.Tables[0].Rows[i]["DONG"].ToString() + " " + dsSet.Tables[0].Rows[i]["BUNJI"].ToString());
				}
				ListBox1.SelectedIndex = -1;
			}

		}
	}
}
