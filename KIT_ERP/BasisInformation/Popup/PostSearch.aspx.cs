using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Services;

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// PostSearch에 대한 요약 설명입니다.
	/// </summary>
	public class PostSearch : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden index;
		protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			TextBox1.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
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
			this.UltraWebGrid1.SelectedRowsChange += new Infragistics.WebUI.UltraWebGrid.SelectedRowsChangeEventHandler(this.UltraWebGrid1_SelectedRowsChange);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str_zip = "Delete Temp_zip";
			SqlCommand com = new SqlCommand(str_zip,conn);
			com.ExecuteNonQuery();
			

			string zip = "";
			string add = "";

			if(TextBox1.Text == null || TextBox1.Text == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('동(읍/면/리)을 입력하세요.');");
				Response.Write("</script>");
			}
			else
			{
				string str = "SELECT * FROM ZipCode Where Dong LIKE '%" + TextBox1.Text + "%'";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds_zip = new DataSet();
				da.Fill(ds_zip);
				
				int a =0;
				foreach(DataRow drow in ds_zip.Tables[0].Rows)
				{
					
					zip = drow["Zipcode"].ToString();
					add = drow["SiDo"].ToString() + " " +drow["GuGun"].ToString() + " " + drow["Dong"].ToString() + " " +drow["BunJi"].ToString();

					string str1 = "InSert into Temp_zip(Temp_index,Temp_zip,Temp_add) values(@index,@zip,@add)";
					SqlCommand comm1 = new SqlCommand(str1,conn);
					comm1.Parameters.Add("@index",SqlDbType.Int).Value = a++;
					comm1.Parameters.Add("@zip",SqlDbType.VarChar).Value = zip;
					comm1.Parameters.Add("@add",SqlDbType.VarChar).Value = add;
					comm1.ExecuteNonQuery();
                   
				}
               
				
				string str2 = "Select * From Temp_zip ";//Where Temp_add LIKE '%" + TextBox1.Text + "%'";
				SqlCommand comm2 = new SqlCommand(str2,conn);
				SqlDataReader dr1 = comm2.ExecuteReader();
				UltraWebGrid1.DataSource = dr1;
				UltraWebGrid1.DataBind();
				dr1.Close();
			}
			conn.Close();
		}

		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			

			string formID = Request.QueryString["formID"];
			string box = Request.QueryString["text"];

			string str = "Select * From Temp_zip Where Temp_index = @index";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(index.Value);

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			Response.Write("<script language='javascript'>");
			Response.Write("opener." + formID + "." + box + ".value =" + "'" + ds.Tables[0].Rows[0]["Temp_zip"].ToString() + " " +ds.Tables[0].Rows[0]["Temp_add"].ToString() + "';");
			Response.Write("window.close();");
			Response.Write("</script>");


			str = "Delete Temp_zip";
			SqlCommand com = new SqlCommand(str,conn);
			com.ExecuteNonQuery();
			conn.Close();

			Response.End();
			
		}
	}
}
