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
	/// DataBackUpRestoration에 대한 요약 설명입니다.
	/// </summary>
	public class DataBackUpRestoration : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.TextBox TextBox1;

		private static int Count;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string DBString = ConfigurationSettings.AppSettings["ERP"];

				string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";

				string strSQL = @"SELECT * FROM BackUp_Table  ORDER BY idx DESC";

				SqlCommand comm = new SqlCommand(strSQL, conn);
               	SqlDataReader reader = comm.ExecuteReader();

				UltraWebGrid1.DataSource = reader;
				UltraWebGrid1.DataBind();
				
				reader.Close();

				comm = new SqlCommand("SELECT TOP 1 Filename FROM BackUp_Table WHERE Filename LIKE @Date ORDER BY Filename DESC", conn);
				comm.Parameters.Add("@Date", SqlDbType.VarChar).Value = DateTime.Now.ToShortDateString() + "%";
				
				reader = comm.ExecuteReader();

				if(reader.Read())
				{
					int Cnt = reader["Filename"].ToString().Length;
					Count = int.Parse(reader["Filename"].ToString().Substring(Cnt - 4)) + 1;
				}
				else
				{
					Count = 1;
				}
				reader.Close();
				conn.Close();

				Label5.Text = DateTime.Now.ToString();
				Label6.Text = Session["UserName"].ToString();
				Label7.Text = DateTime.Now.ToShortDateString() + '-' + Count.ToString("0000");
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//  TODO : 백업버튼 클릭시
		private void Button2_Click(object sender, System.EventArgs e)
		{

			/*

//			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//			SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//			conn.Open();


			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			//SqlTransaction tr = conn.BeginTransaction();
			try
			{				

				//string DBString = registryKey.GetValue("ConnectionBackUp").ToString();
				string DBString = ConfigurationSettings.AppSettings["ERP"];
				string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";

				SqlCommand comm = new SqlCommand();
				string strSQL = "INSERT INTO BackUp_Table (RegisterDate, Register, Filename,BackUpReason) VALUES (@RegisterDate, @Register, @Filename, @BackUpReason)" ;
				comm.Connection = conn;
				//comm.Transaction = tr;
				comm.CommandTimeout = 240;
				comm.Parameters.Add("@RegisterDate",  Label5.Text);
				comm.Parameters.Add("@Register",  Label6.Text);
				comm.Parameters.Add("@Filename", Label7.Text);
				comm.Parameters.Add("@BackUpReason",TextBox1.Text.Trim());
						
				comm.CommandText = strSQL;
				comm.ExecuteNonQuery();

				comm.CommandText = "BACKUP DATABASE " + DBString.ToString() + " TO DISK = @Filename WITH RESTART";
				
				//comm.Parameters["@Filename"].Value = BackFilesURL + Label7.Text + ".bak";
				
				
							
				comm.Parameters["@Filename"].Value = "D:\\erp\\source\\신동공업\\DBBackUpFiles\\"+ Label7.Text + ".bak";
				//comm.Parameters["@Filename"].Value = "D:\\erp\\source\\Sample-Sindong\\DBBackUpFiles\\"+ Label7.Text + ".bak";
				
				
				
				comm.ExecuteNonQuery();

				
				Binding();

				Count++;
				Label5.Text = DateTime.Now.ToString();
				Label6.Text = Session["UserName"].ToString();
				Label7.Text = DateTime.Now.ToShortDateString() + '-' + Count.ToString("0000");

				Response.Write("<script language=javascript>");
				Response.Write("alert('DB 백업이 완료 되었습니다.');");
				Response.Write("window.open('DataBackUpRestoration.aspx', 'Contents', '');");
				Response.Write("</script>");
				Response.End();
			}
			catch(Exception ee)
			{
				
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");

				//tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
		
*/

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			//string DBString = registryKey.GetValue("ConnectionBackUp").ToString();
			string DBString = ConfigurationSettings.AppSettings["ERP"];
			string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";

			SqlCommand comm = new SqlCommand();
			string strSQL = "INSERT INTO BackUp_Table (RegisterDate, Register, Filename,BackUpReason) VALUES (@RegisterDate, @Register, @Filename, @BackUpReason)" ;
			comm.Connection = conn;
            		
			comm.Parameters.Add("@RegisterDate",  Label5.Text);
			comm.Parameters.Add("@Register",  Label6.Text);
			comm.Parameters.Add("@Filename", Label7.Text);
			comm.Parameters.Add("@BackUpReason",TextBox1.Text.Trim());
						
			comm.CommandText = strSQL;
			comm.ExecuteNonQuery();

			comm.CommandText = "BACKUP DATABASE " + DBString.ToString() + " TO DISK = @Filename WITH RESTART";
			comm.Parameters["@Filename"].Value = BackFilesURL + Label7.Text + ".bak";
			comm.ExecuteNonQuery();

			conn.Close();
			Binding();

			Count++;
			Label5.Text = DateTime.Now.ToString();
			Label6.Text = Session["UserName"].ToString();
			Label7.Text = DateTime.Now.ToShortDateString() + '-' + Count.ToString("0000");

			Response.Write("<script language=javascript>");
			Response.Write("alert('DB 백업이 완료 되었습니다.');");
			Response.Write("window.open('DataBackUpRestoration.aspx', 'Contents', '');");
			Response.Write("</script>");
			Response.End();
			


		}

		// TODO : 복원버튼 클릭시
		private void Button1_Click(object sender, System.EventArgs e)
		{
			/*
			//RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
			//string DBString = registryKey.GetValue("ConnectionBackUp").ToString();
			string DBString = ConfigurationSettings.AppSettings["ERP"];
			//string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";
			string BackFilesURL = "D:\\erp\\source\\신동공업\\DBBackUpFiles\\";

			int ChkCount = 0;
			string filename = "";
			foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in UltraWebGrid1.Rows)
			{
				if((row.Cells.FromKey("chk").Value != null) && (bool.Parse(row.Cells.FromKey("chk").Value.ToString())))
				{
					ChkCount++;
					filename = row.Cells.FromKey("Filename").Value.ToString();
				}
			}

			if(ChkCount <= 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('복원할 파일을 선택하여 주십시오.');");
				Response.Write("</script>");
			}
			else if(ChkCount > 1)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('복원할 파일을 하나만 선택하여 주십시오.');");
				Response.Write("</script>");
			}
			else
			{
				//SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				SqlCommand comm = new SqlCommand("alter database " + DBString.ToString() + " set single_user with ROLLBACK IMMEDIATE RESTORE DATABASE " + DBString.ToString() + " FROM DISK = @FileName WITH RESTART alter database " + DBString.ToString() + " set multi_user with rollback IMMEDIATE", conn);
				comm.CommandTimeout = 240;
				//comm.Parameters.Add("@Filename", BackFilesURL + filename.ToString() + ".bak");
				comm.Parameters.Add("@Filename", "D:\\erp\\source\\신동공업\\DBBackUpFiles\\"+ filename.ToString() + ".bak");
				comm.ExecuteNonQuery();
				conn.Close();

				Response.Write("<script language=javascript>");
				Response.Write("alert('DB가 복원되었습니다.');");
				Response.Write("</script>");
			}
			*/


			//RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\mlim_ERP");
			//string DBString = registryKey.GetValue("ConnectionBackUp").ToString();
			string DBString = ConfigurationSettings.AppSettings["ERP"];
			string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";

			int ChkCount = 0;
			string filename = "";
			foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in UltraWebGrid1.Rows)
			{
				if((row.Cells.FromKey("chk").Value != null) && (bool.Parse(row.Cells.FromKey("chk").Value.ToString())))
				{
					ChkCount++;
					filename = row.Cells.FromKey("Filename").Value.ToString();
				}
			}

			if(ChkCount <= 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('복원할 파일을 선택하여 주십시오.');");
				Response.Write("</script>");
			}
			else if(ChkCount > 1)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('복원할 파일을 하나만 선택하여 주십시오.');");
				Response.Write("</script>");
			}
			else
			{
				//SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				SqlCommand comm = new SqlCommand("alter database " + DBString.ToString() + " set single_user with ROLLBACK IMMEDIATE RESTORE DATABASE " + DBString.ToString() + " FROM DISK = @FileName WITH RESTART alter database " + DBString.ToString() + " set multi_user with rollback IMMEDIATE", conn);
				comm.Parameters.Add("@Filename", BackFilesURL + filename.ToString() + ".bak");
				comm.ExecuteNonQuery();
				conn.Close();

				Response.Write("<script language=javascript>");
				Response.Write("alert('DB가 복원되었습니다.');");
				Response.Write("</script>");
			}
		}

		// TODO : 삭제버튼 클릭시
		private void Button3_Click(object sender, System.EventArgs e)
		{
			string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";
			
			bool check = false;
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			SqlCommand comm = new SqlCommand();
			SqlTransaction Trans = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = Trans;
			string strSQL = "";
            
			try
			{
				comm.Parameters.Add("@Filename", "");
				comm.Parameters.Add("@idx","");
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in UltraWebGrid1.Rows)
				{
					if((row.Cells.FromKey("chk").Value != null) && (bool.Parse(row.Cells.FromKey("chk").Value.ToString())))
					{
						check = true;

						strSQL = @"EXEC master..xp_cmdshell @Filename, no_output";
						comm.Parameters["@Filename"].Value =  "del " + BackFilesURL + row.Cells.FromKey("Filename").Value.ToString() + ".bak";

						comm.CommandText = strSQL;
						comm.ExecuteNonQuery();

						strSQL = @"DELETE FROM BackUp_Table WHERE idx = @idx";
						comm.Parameters["@idx"].Value = row.Cells.FromKey("idx").Value.ToString();

						comm.CommandText = strSQL;
						comm.ExecuteNonQuery();
					}
				}
				
				if(check)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('선택한 DB 자료가 삭제 되었습니다.');");
					Response.Write("</script>");
					Trans.Commit();
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('선택한 DB 없습니다.');");
					Response.Write("</script>");
				}
			}
			catch
			{
				Trans.Rollback();
			}
			finally
			{
				conn.Close();
				Binding();
			}


			/*
			//string BackFilesURL = Request.PhysicalApplicationPath.ToString() + "DBBackUpFiles\\";
			string BackFilesURL = "D:\\erp\\source\\신동공업\\DBBackUpFiles\\";
			
			bool check = false;
			
//			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//			SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//			conn.Open();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			SqlCommand comm = new SqlCommand();
			SqlTransaction Trans = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = Trans;
			string strSQL = "";
            
			try
			{
				comm.Parameters.Add("@Filename", "");
				comm.Parameters.Add("@idx","");
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in UltraWebGrid1.Rows)
				{
					if((row.Cells.FromKey("chk").Value != null) && (bool.Parse(row.Cells.FromKey("chk").Value.ToString())))
					{
						check = true;

						

						strSQL = @"DELETE FROM BackUp_Table WHERE idx = @idx";
						comm.Parameters["@idx"].Value = row.Cells.FromKey("idx").Value.ToString();

						comm.CommandText = strSQL;
						comm.ExecuteNonQuery();
					}
				}
				
				if(check)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('선택한 DB 자료가 삭제 되었습니다.');");
					Response.Write("</script>");
					Trans.Commit();
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('선택한 DB 없습니다.');");
					Response.Write("</script>");
				}
			}
			catch
			{
				Trans.Rollback();
			}
			finally
			{
				conn.Close();
				Binding();
			}
*/
		}

		private void Binding()
		{
//			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//			SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//			conn.Open();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			string strSQL = @"SELECT * FROM BackUp_Table  ORDER BY idx DESC";

			SqlCommand comm = new SqlCommand(strSQL, conn);
                
			SqlDataReader reader = comm.ExecuteReader();

			UltraWebGrid1.DataSource = reader;
			UltraWebGrid1.DataBind();
				
			reader.Close();
			conn.Close();
		}
	}
}