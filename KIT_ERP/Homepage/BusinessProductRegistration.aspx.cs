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

namespace KIT_ERP.Homepage
{
	/// <summary>
	/// BusinessProductRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessProductRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;

		private static byte [] Files1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 유효성검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function') ";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.Button2.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			Button1.Attributes.Add("onclick", "return Cancel_Check()");

			if(!IsPostBack)
			{
				Label1.Text = "◀ 사업 / 제품개요 ▶";

				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				Con.Open();
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = "SELECT Category FROM CCG_T WHERE Classify = 0";
				SqlDataAdapter da = new SqlDataAdapter(Cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);

				DropDownList1.DataSource = ds;
				DropDownList1.DataTextField = ds.Tables[0].Columns[0].ToString();
				DropDownList1.DataValueField = ds.Tables[0].Columns[0].ToString();
				DropDownList1.DataBind();
				DropDownList1.Items.Insert(0, "전체선택") ;
				DropDownList1.Items[0].Value = "";
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
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		// 취소버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("BusinessProduct.aspx");
		}



		// 등록버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);			
			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			
			if(CheckBox1.Checked)
			{
				if(TextBox1.Text =="")
					Response.Write("<script>alert('Category를 입력해 주십시오!');</script>");
				else
				{
					try
					{
						Cmd.CommandText = "INSERT INTO CCG_T(Category, Classify) VALUES(@Category, @Classify)";
						Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = TextBox1.Text;
						Cmd.Parameters.Add("@Classify", SqlDbType.SmallInt).Value = 0;
						Cmd.ExecuteNonQuery();

						Cmd.CommandText = "INSERT INTO HBP_T(Title, Contents, AppendFile, FileName, FileSize, FileType, RegistrationPerson, Distinction, Category) VALUES(@Title, @Contents, @AppendFile, @FileName, @FileSize, @FileType, @RegistrationPerson, @Distinction, @Category2)";
						Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title.Text;
						Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = Content.Text;
						Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
						Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
						Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
						Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType.ToString();
						Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar).Value = Session["UserName"].ToString();
						Cmd.Parameters.Add("@Distinction", SqlDbType.Bit).Value = false;
						Cmd.Parameters.Add("@Category2", SqlDbType.VarChar).Value = TextBox1.Text;

						Cmd.ExecuteNonQuery();
						trans.Commit();
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
						Response.Cookies["HBP_T"]["PageNumber"] = "0";
						Response.Redirect("BusinessProduct.aspx");
					}
				}
			}

			
			else if(!CheckBox1.Checked)
			{
				if(DropDownList1.SelectedIndex == 0)
					Response.Write("<script>alert('Category를 선택해 주십시오!');</script>");
				else
				{
					try
					{
						Cmd.CommandText = "INSERT INTO HBP_T(Title, Contents, AppendFile, FileName, FileSize, FileType, RegistrationPerson, Distinction, Category) VALUES(@Title, @Contents, @AppendFile, @FileName, @FileSize, @FileType, @RegistrationPerson, @Distinction, @Category)";

						Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title.Text;
						Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = Content.Text;
						Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
						Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
						Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
						Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType.ToString();
						Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar).Value = Session["UserName"].ToString();
						Cmd.Parameters.Add("@Distinction", SqlDbType.Bit).Value = false;
						Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;

						Cmd.ExecuteNonQuery();
						trans.Commit();
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
						Response.Cookies["HBP_T"]["PageNumber"] = "0";
						Response.Redirect("BusinessProduct.aspx");
					}
				}
			}			
		}



		private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(CheckBox1.Checked)
			{
				DropDownList1.Enabled = false;
				TextBox1.Visible = true;
			}
			else
			{
				DropDownList1.Enabled = true;
				TextBox1.Visible = false;
			}
		}
	}
}
