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

namespace KIT_ERP.Community
{
	/// <summary>
	/// WorkDiaryWrite02에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDiaryWrite02 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbDate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.WebControls.TextBox TextBox5;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.Literal Literal10;
		protected System.Web.UI.WebControls.TextBox TextBox10;
		protected System.Web.UI.WebControls.Label Label5;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				hdIndex.Value = Request["Num"].Trim().ToString();
				IDFind();
				Authority();
			}
		}

		private void IDFind()
		{
			if(Session["ID"].ToString().Trim().ToLower() == "77sindong")
			{
				Button1.Visible = true;
				Button2.Visible = false;
			}
			else
			{
				Button1.Visible = false;
				Button2.Visible = true;
			}
		}

		private string ReplaceBR(string s)
		{
			string a =  s.Replace("\n", "<BR>");
			return a.Replace(" ", "&nbsp;");
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
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Authority()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction tr = Con.BeginTransaction();
			

			
			try
			{
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				string str = @"Select W.*, I.*
						From WorkReport W inner join WorkReportID I 
						on W.[Index] = I.[Index] where W.[Index] = @index";
				Cmd.Transaction = tr;
				Cmd.CommandText = str;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());

				SqlDataAdapter da = new SqlDataAdapter(Cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);

				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					if(dr["approval"].ToString() == "기결")
					{
						Literal1.Visible = true;
						Literal2.Visible = true;
						Literal3.Visible = true;
						//Literal4.Visible = true;
						//Literal5.Visible = true;
						//Literal6.Visible = true;
						Label5.Visible = true;
						TextBox1.Visible = false;
						//TextBox2.Visible = false;
						TextBox3.Visible = false;
						TextBox4.Visible = false;
						TextBox5.Visible = false;
						//Textbox6.Visible = false;
						//Textbox7.Visible = false;

						Literal10.Visible = true;
						TextBox10.Visible = false;


						
						lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
						Label2.Text = dr["RegistrationPerson"].ToString();
						Literal1.Text = "<br>"+ReplaceBR(dr["01"].ToString());
						Literal2.Text = "<br>"+ReplaceBR(dr["02"].ToString());
						Literal3.Text = "<br>"+ReplaceBR(dr["03"].ToString());
						//Literal4.Text = "<br>"+ReplaceBR(dr["07"].ToString());
						//Literal5.Text = "<br>"+ReplaceBR(dr["04"].ToString());
						//Literal6.Text = "<br>"+ReplaceBR(dr["05"].ToString());
						Label5.Text = "<br>"+ReplaceBR(dr["06"].ToString());


						Literal10.Text = "<br>"+ReplaceBR(dr["10"].ToString());
						
						Button1.Visible = false;
						Button2.Visible = false;

						if(Session["ID"].ToString().Trim().ToLower()  == "77sindong")
							Button4.Visible = true;
					}
					else
					{
						if(Session["ID"].ToString().Trim().ToLower()  == "77sindong")
						{
							Literal1.Visible = true;
							Literal2.Visible = true;
							Literal3.Visible = true;
							//Literal4.Visible = true;
							//Literal5.Visible = true;
							//Literal6.Visible = true;
							Label5.Visible = false;
							TextBox1.Visible = false;
							//TextBox2.Visible = false;
							TextBox3.Visible = false;
							TextBox4.Visible = false;
							//Textbox6.Visible = false;
							//Textbox7.Visible = false;
							TextBox5.Visible = true;


							Literal10.Visible = true;
							TextBox10.Visible = false;
							
							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
							Label2.Text = dr["RegistrationPerson"].ToString();
							Literal1.Text = "<br>"+ReplaceBR(dr["01"].ToString());
							Literal2.Text = "<br>"+ReplaceBR(dr["02"].ToString());
							Literal3.Text = "<br>"+ReplaceBR(dr["03"].ToString());
							//Literal4.Text = "<br>"+ReplaceBR(dr["07"].ToString());
							//Literal5.Text = "<br>"+ReplaceBR(dr["04"].ToString());
							//Literal6.Text = "<br>"+ReplaceBR(dr["05"].ToString());
							TextBox5.Text = dr["06"].ToString();

							Literal10.Text = "<br>"+ReplaceBR(dr["10"].ToString());

							Button1.Visible = true;
							Button2.Visible = false;
							Button4.Visible = true;
						}
						else
						{
							Literal1.Visible = false;
							Literal2.Visible = false;
							Literal3.Visible = false;
							//Literal4.Visible = false;
							//Literal5.Visible = false;
							//Literal6.Visible = false;
							Label5.Visible = true;
							TextBox1.Visible = true;
							//TextBox2.Visible = true;
							TextBox3.Visible = true;
							TextBox4.Visible = true;
							//Textbox6.Visible = true;
							//Textbox7.Visible = true;
							TextBox5.Visible = false;

							Literal10.Visible = false;
							TextBox10.Visible = true;
							
							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
							Label2.Text = dr["RegistrationPerson"].ToString();
							TextBox1.Text = dr["01"].ToString();
							//TextBox2.Text = dr["07"].ToString();
							TextBox3.Text = dr["02"].ToString();
							TextBox4.Text = dr["03"].ToString();
							//Textbox6.Text = dr["04"].ToString();
							//Textbox7.Text = dr["05"].ToString();
							Label5.Text = "<br>"+ReplaceBR(dr["06"].ToString());
						
							TextBox10.Text = dr["10"].ToString();


							Button1.Visible = false;
							Button2.Visible = true;
							Button4.Visible = false;
						}
					}
				}

				
				tr.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('"+ex.Message+"')</script>");
				tr.Rollback();
			}
			finally
			{
				Con.Close();
			}


		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			bool test = true;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction tr = Con.BeginTransaction();

			try
			{
				string Approval = "";
				string str = "Select Approval From WorkReport where [Index] = @index";
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = str;
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				SqlDataReader dr = Cmd.ExecuteReader();
				Cmd.Parameters.Clear();
				while(dr.Read())
				{
					Approval = dr["Approval"].ToString();
				}
				dr.Close();

				if(Approval == "기결")
					throw new Exception("이미 결재되었습니다!");
				else
					//str =  @"Update WorkReportID Set  [01] = @01, [02] = @02, [03] = @03, [04] = @04, [05] = @05, [07] = @07, [10] = @10  Where [Index] = @index";
					str =  @"Update WorkReportID Set  [01] = @01, [02] = @02, [03] = @03, [10] = @10 Where [Index] = @index";
				
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@01",TextBox1.Text);
				Cmd.Parameters.Add("@02",TextBox3.Text);
				Cmd.Parameters.Add("@03",TextBox4.Text);
				//Cmd.Parameters.Add("@04",Textbox6.Text);
				//Cmd.Parameters.Add("@05",Textbox7.Text);
				//Cmd.Parameters.Add("@07",TextBox2.Text);
				Cmd.Parameters.Add("@10",TextBox10.Text);
				Cmd.CommandText = str;
				Cmd.ExecuteNonQuery();
				tr.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('"+ex.Message+"')</script>");
				tr.Rollback();
				test = false;
			}
			finally
			{
				Con.Close();
				if(test)
					Response.Redirect("WorkReportList.aspx");
			}

		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			bool test = true;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction tr = Con.BeginTransaction();

			try
			{
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = @"Update WorkReport Set  Approval=@Approval Where [Index] = @index";
				
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@Approval","기결");
				Cmd.ExecuteNonQuery();
				Cmd.Parameters.Clear();

				Cmd.CommandText = @"Update WorkReportID Set  [06]=@06 Where [Index] = @index";
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@06",TextBox5.Text);
				Cmd.ExecuteNonQuery();
				Cmd.Parameters.Clear();

				tr.Commit();

			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('"+ex.Message+"')</script>");
				tr.Rollback();
				test = false;
			}
			finally
			{
				Con.Close();
				if(test)
					Response.Redirect("WorkReportList.aspx");
			}

		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			bool test = true;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction tr = Con.BeginTransaction();

			try
			{
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = @"Update WorkReport Set  Approval=@Approval Where [Index] = @index";
				
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@Approval","미결");
				Cmd.ExecuteNonQuery();
				tr.Commit();

			}
			catch(Exception ex)
			{
				Response.Write("<script>alert('"+ex.Message+"')</script>");
				tr.Rollback();
				test = false;
			}
			finally
			{
				Con.Close();
				if(test)
					Authority();
			}
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkReportList.aspx");
		}
	}
}
