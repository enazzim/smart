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

namespace KIT_ERP.Community
{
	/// <summary>
	/// ProductWrite에 대한 요약 설명입니다.
	/// </summary>
	public class ProductWrite : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnApply;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		private static byte [] Files1;

		private bool ReplyFlag = false;
		private string thread = string.Empty;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected KIT_ERP.Community.dsUser dsUser1;
		private string depth = string.Empty;
		private string seq;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.btnApply.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnCancel.Attributes.Add("onclick", "return Cancel_Check();");

			if (Request.Params["thread"] != null)
				thread = Request.Params["thread"].ToString();
			if (Request.Params["depth"] != null)
				depth = Request.Params["depth"].ToString();
			
			

			if(thread != string.Empty && depth != string.Empty)
			{
				if (Request.Params["seq"] != null)
					seq = Request.Params["seq"].ToString();
				ReplyFlag = true;
				LoadData();
			}


			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				

				if(Session["ID"] != null)
				{
					WriteName(Session["ID"].ToString());
				}

				sqlDataAdapter1.Fill(dsUser1);
				ListBox1.DataBind();
				
			}	
		}

		private void WriteName(string id)
		{
			string ConnecStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnecStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id and RecodingState = 1";
			Cmd.Parameters.Add("@id", id);
			Con.Open();
			RegistrationPerson.Text = Cmd.ExecuteScalar().ToString();
			RegistrationPerson.ReadOnly = true;
			Con.Close();
		}

		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@seq", SqlDbType.Int);
			Cmd.Parameters["@seq"].Value = seq;
			
			Cmd.CommandText = "SELECT * FROM T_Product WHERE seq = @seq";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);			

			if(reader.Read())
			{
				ViewState["seq"] = reader["seq"].ToString();
				ViewState["thread"] = reader["thread"].ToString();
				ViewState["depth"] = reader["depth"].ToString();

				RegistrationPerson.Text = reader["writer"].ToString();
				if(ReplyFlag)
					Title.Text = "[RE] "+reader["title"].ToString();
				else
					Title.Text = reader["title"].ToString();
				

			}			
			reader.Close();			
			Con.Close();
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
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.dsUser1 = new KIT_ERP.Community.dsUser();
			((System.ComponentModel.ISupportInitialize)(this.dsUser1)).BeginInit();
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "UI_MT", new System.Data.Common.DataColumnMapping[] {
																																																			   new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																			   new System.Data.Common.DataColumnMapping("ID", "ID")})});
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT Name + \'(\' + ID + \')\' AS Name, ID FROM UI_MT WHERE (RecodingState = 1) ORD" +
				"ER BY Name";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			// 
			// dsUser1
			// 
			this.dsUser1.DataSetName = "dsUser";
			this.dsUser1.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsUser1)).EndInit();

		}
		#endregion

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			bool truefalse = false;
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);

			

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);


			string sp;
			if(ReplyFlag)
				sp = "UP_INSERT_REPLY";
			else
				sp = "UP_INSERT_BOARDNEW";

			SqlCommand Cmd = new SqlCommand();
			Cmd.CommandText = sp;
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Transaction = trans;
			Cmd.Connection = Con;
			Cmd.CommandType = CommandType.StoredProcedure;
			
			
			

			try
			{
				if(File1.Value.Trim() != "" && FileSize == 0)
				{
					throw new Exception("파일크기가 0보다 커야 합니다!");
				}

				if(ReplyFlag)
				{
					int thread = int.Parse(this.thread);
					int depth = int.Parse(this.depth);
					int prevThread = (thread-1)/1000 * 1000;

					Cmd.Parameters.Add("@ParentThread", thread);
					Cmd.Parameters.Add("@PrevThread", prevThread);
					Cmd.Parameters.Add("@depth", depth + 1);
				}

				Cmd.Parameters.Add("@writer", SqlDbType.VarChar).Value = RegistrationPerson.Text;
				Cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title.Text;			
				Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
				Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
				Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
				Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType.ToString();
				Cmd.Parameters.Add("@content", SqlDbType.VarChar).Value = Content.Text;
				Cmd.Parameters.Add("@Division","생산품질");

				Cmd.ExecuteNonQuery();

				Cmd.Parameters.Clear();
				//필독자 처리
				foreach(ListItem item in ListBox1.Items)
				{
					if(item.Selected)
					{
						sp = "UP_Relation_BOARDNEW";
						Cmd.Parameters.Add("@ID", item.Value);
						Cmd.Parameters.Add("@Division", "생산품질");
						Cmd.Parameters.Add("@No", Max(Con,trans));
						Cmd.CommandText = sp;
						Cmd.CommandType = CommandType.StoredProcedure;
						Cmd.ExecuteNonQuery();					
						Cmd.Parameters.Clear();
					}
				}
				//string script = "<script>alert('저장되었습니다');</script>";
				//Page.RegisterClientScriptBlock("done", script);
				//Response.Redirect("DevelopmentList.aspx");
				
				truefalse = true;
				trans.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('"+ex.Message+"');</script>");
				trans.Rollback();
			}
			finally
			{
				Con.Close();

				if(truefalse)
					Response.Redirect("ProductList.aspx");
			}
		}

		private int Max(SqlConnection conn, SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = "Select Max(seq) From T_Product";
			int count  = int.Parse(comm.ExecuteScalar().ToString());
			
			return count;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("ProductList.aspx");
		}
	}
}
