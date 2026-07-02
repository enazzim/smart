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
	/// WorkReportWrite에 대한 요약 설명입니다.
	/// </summary>
	public class WorkReportWrite : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox TextBox5;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox TextBox6;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lbDate;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				hdIndex.Value = Request["Index"].Trim().ToString();
				IDFind();
				Authority();
			}
			

			
		}

		private void IDFind()
		{
			if(Session["ID"].ToString().Trim().ToLower() == "admin")
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
				Cmd.CommandText = @"Select ReportDate, BusinessTripPerson, Customer, Equipment, QC, MainPanding, Indication, approval 
				From WorkReport Where [Index]= @index";
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());

				SqlDataReader dr = Cmd.ExecuteReader();
				while(dr.Read())
				{
					if(dr["approval"].ToString() == "기결")
					{
						Label1.Visible = true;
						Label2.Visible = true;
						Label3.Visible = true;
						Label4.Visible = true;
						Label5.Visible = true;
						Label6.Visible = true;
						TextBox1.Visible = false;
						TextBox2.Visible = false;
						TextBox3.Visible = false;
						TextBox4.Visible = false;
						TextBox5.Visible = false;
						TextBox6.Visible = false;

						lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
						Label1.Text = "<br>"+ReplaceBR(dr["BusinessTripPerson"].ToString());
						Label2.Text = "<br>"+ReplaceBR(dr["Customer"].ToString());
						Label3.Text = "<br>"+ReplaceBR(dr["Equipment"].ToString());
						Label4.Text = "<br>"+ReplaceBR(dr["QC"].ToString());
						Label5.Text = "<br>"+ReplaceBR(dr["MainPanding"].ToString());
						Label6.Text = "<br>"+ReplaceBR(dr["Indication"].ToString());

						Button1.Visible = false;
						Button2.Visible = false;

						if(Session["ID"].ToString().Trim().ToLower()  == "admin")
							Button4.Visible = true;

					}
					else
					{
						
						
						if(Session["ID"].ToString().Trim().ToLower()  == "admin")
						{
							Label1.Visible = true;
							Label2.Visible = true;
							Label3.Visible = true;
							Label4.Visible = true;
							Label5.Visible = true;
							Label6.Visible = false;
							TextBox1.Visible = false;
							TextBox2.Visible = false;
							TextBox3.Visible = false;
							TextBox4.Visible = false;
							TextBox5.Visible = false;
							TextBox6.Visible = true;

							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
							Label1.Text = "<br>"+ReplaceBR(dr["BusinessTripPerson"].ToString());
							Label2.Text = "<br>"+ReplaceBR(dr["Customer"].ToString());
							Label3.Text = "<br>"+ReplaceBR(dr["Equipment"].ToString());
							Label4.Text = "<br>"+ReplaceBR(dr["QC"].ToString());
							Label5.Text = "<br>"+ReplaceBR(dr["MainPanding"].ToString());
							TextBox6.Text = dr["Indication"].ToString();

							Button1.Visible = true;
							Button2.Visible = false;
							Button4.Visible = true;
						}
						else if(Session["ID"].ToString().ToLower().Trim() == "kbs" ||
									Session["ID"].ToString().ToLower().Trim() == "ok0145" ||
									Session["ID"].ToString().ToLower().Trim() == "pok513" ||
									Session["ID"].ToString().ToLower().Trim() == "rh4886" ||
									Session["ID"].ToString().ToLower().Trim() == "admin2" ||
									Session["ID"].ToString().ToLower().Trim() == "bhko25")
						{
							Label1.Visible = false;
							Label2.Visible = false;
							Label3.Visible = false;
							Label4.Visible = false;
							Label5.Visible = false;
							Label6.Visible = true;
							TextBox1.Visible = true;
							TextBox2.Visible = true;
							TextBox3.Visible = true;
							TextBox4.Visible = true;
							TextBox5.Visible = true;
							TextBox6.Visible = false;

                            lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
							TextBox1.Text = dr["BusinessTripPerson"].ToString();
							TextBox2.Text = dr["Customer"].ToString();
							TextBox3.Text = dr["Equipment"].ToString();
							TextBox4.Text = dr["QC"].ToString();
							TextBox5.Text = dr["MainPanding"].ToString();
							Label6.Text = "<br>"+ReplaceBR(dr["Indication"].ToString());


//							TextBox1.Text = ReplaceBR(Server.HtmlEncode(dr["BusinessTripPerson"].ToString()));
//							TextBox2.Text = ReplaceBR(Server.HtmlEncode(dr["Customer"].ToString()));
//							TextBox3.Text = ReplaceBR(Server.HtmlEncode(dr["Equipment"].ToString()));
//							TextBox4.Text = ReplaceBR(Server.HtmlEncode(dr["QC"].ToString()));
//							TextBox5.Text = ReplaceBR(Server.HtmlEncode(dr["MainPanding"].ToString()));
//							Label6.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Indication"].ToString()));

							Button1.Visible = false;
							Button2.Visible = true;
							Button4.Visible = false;
						}
						else
						{
							Label1.Visible = true;
							Label2.Visible = true;
							Label3.Visible = true;
							Label4.Visible = true;
							Label5.Visible = true;
							Label6.Visible = true;
							TextBox1.Visible = false;
							TextBox2.Visible = false;
							TextBox3.Visible = false;
							TextBox4.Visible = false;
							TextBox5.Visible = false;
							TextBox6.Visible = false;

							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
							Label1.Text = "<br>"+ReplaceBR(dr["BusinessTripPerson"].ToString());
							Label2.Text = "<br>"+ReplaceBR(dr["Customer"].ToString());
							Label3.Text = "<br>"+ReplaceBR(dr["Equipment"].ToString());
							Label4.Text = "<br>"+ReplaceBR(dr["QC"].ToString());
							Label5.Text = "<br>"+ReplaceBR(dr["MainPanding"].ToString());
							Label6.Text = "<br>"+ReplaceBR(dr["Indication"].ToString());

							Button1.Visible = false;
							Button2.Visible = false;
							Button4.Visible = false;
						}
						


//						if(Session["ID"].ToString().Trim().ToLower()  == "jjs4220")
//						{
//							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//
//							TextBox1.Text = ReplaceBR(Server.HtmlEncode(dr["BusinessTripPerson"].ToString());
//							Label2.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Customer"].ToString());
//							Label3.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Equipment"].ToString());
//							Label4.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["QC"].ToString());
//							Label5.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["MainPanding"].ToString());
//							
//							Label6.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Indication"].ToString());
//						}
//						else if(Session["ID"].ToString().Trim().ToLower()  == "")
//						{
//							lbDate.Text = DateTime.Now.ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							TextBox1.Text = "1.출장자 현황";
//							Label2.Text = "<br>2. 거래업체 협의사항<br><br>없음.";
//							Label3.Text = "<br>3. 설비이상 사항<br><br>없음.";
//							Label4.Text = "<br>4. 품질관리 사항<br><br>없음.";
//							Label5.Text = "<br>5. 주요현안 사항<br><br>없음.";
//							Label6.Text = "<br>6. 지시사항<br><br>없음.";
//						}
//						else if(Session["ID"].ToString().Trim().ToLower()  == "")
//						{
//							lbDate.Text = DateTime.Now.ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							TextBox1.Text = "1.출장자 현황";
//							Label2.Text = "<br>2. 거래업체 협의사항<br><br>없음.";
//							Label3.Text = "<br>3. 설비이상 사항<br><br>없음.";
//							Label4.Text = "<br>4. 품질관리 사항<br><br>없음.";
//							Label5.Text = "<br>5. 주요현안 사항<br><br>없음.";
//							Label6.Text = "<br>6. 지시사항<br><br>없음.";
//						}
//						else if(Session["ID"].ToString().Trim().ToLower()  == "")
//						{
//							lbDate.Text = DateTime.Now.ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							TextBox1.Text = "1.출장자 현황";
//							Label2.Text = "<br>2. 거래업체 협의사항<br><br>없음.";
//							Label3.Text = "<br>3. 설비이상 사항<br><br>없음.";
//							Label4.Text = "<br>4. 품질관리 사항<br><br>없음.";
//							Label5.Text = "<br>5. 주요현안 사항<br><br>없음.";
//							Label6.Text = "<br>6. 지시사항<br><br>없음.";
//						}
//						else if(Session["ID"].ToString().Trim().ToLower()  == "")
//						{
//							lbDate.Text = DateTime.Now.ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							TextBox1.Text = "1.출장자 현황";
//							Label2.Text = "<br>2. 거래업체 협의사항<br><br>없음.";
//							Label3.Text = "<br>3. 설비이상 사항<br><br>없음.";
//							Label4.Text = "<br>4. 품질관리 사항<br><br>없음.";
//							Label5.Text = "<br>5. 주요현안 사항<br><br>없음.";
//							Label6.Text = "<br>6. 지시사항<br><br>없음.";
//						}
//						else if(Session["ID"].ToString().Trim().ToLower()  == "")
//						{
//							lbDate.Text = DateTime.Now.ToShortDateString();
//							Label1.Visible = false;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = true;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							TextBox1.Text = "1.출장자 현황";
//							Label2.Text = "<br>2. 거래업체 협의사항<br><br>없음.";
//							Label3.Text = "<br>3. 설비이상 사항<br><br>없음.";
//							Label4.Text = "<br>4. 품질관리 사항<br><br>없음.";
//							Label5.Text = "<br>5. 주요현안 사항<br><br>없음.";
//							Label6.Text = "<br>6. 지시사항<br><br>없음.";
//						}
//						else if(Session["ID"].ToString().ToLower() == "admin")
//						{
//							lbDate.Text = DateTime.Parse(dr["ReportDate"].ToString()).ToShortDateString();
//							Label1.Visible = true;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = false;
//							TextBox1.Visible = false;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = true;
//
//
//							Label1.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["BusinessTripPerson"].ToString());
//							Label2.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Customer"].ToString());
//							Label3.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Equipment"].ToString());
//							Label4.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["QC"].ToString());
//							Label5.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["MainPanding"].ToString());
//							
//							TextBox6.Text = ReplaceBR(Server.HtmlEncode(dr["Indication"].ToString());
//						}
//						else
//						{
//							Label1.Visible = true;
//							Label2.Visible = true;
//							Label3.Visible = true;
//							Label4.Visible = true;
//							Label5.Visible = true;
//							Label6.Visible = true;
//							TextBox1.Visible = false;
//							TextBox2.Visible = false;
//							TextBox3.Visible = false;
//							TextBox4.Visible = false;
//							TextBox5.Visible = false;
//							TextBox6.Visible = false;
//
//							Label1.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["BusinessTripPerson"].ToString());
//							Label2.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Customer"].ToString());
//							Label3.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Equipment"].ToString());
//							Label4.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["QC"].ToString());
//							Label5.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["MainPanding"].ToString());
//							Label6.Text = "<br>"+ReplaceBR(Server.HtmlEncode(dr["Indication"].ToString());
//
//							Button1.Visible = false;
//							Button2.Visible = false;
//						}
					}
				}
				dr.Close();

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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button3_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkReportList.aspx");
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
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = @"Update WorkReport Set  BusinessTripPerson = @BusinessTripPerson, Customer=@Customer, 
					Equipment=@Equipment, QC=@QC, MainPanding = @MainPanding Where [Index] = @index";
				
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@BusinessTripPerson",TextBox1.Text);
				Cmd.Parameters.Add("@Customer",TextBox2.Text);
				Cmd.Parameters.Add("@Equipment",TextBox3.Text);
				Cmd.Parameters.Add("@QC",TextBox4.Text);
				Cmd.Parameters.Add("@MainPanding",TextBox5.Text);
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
				Cmd.CommandText = @"Update WorkReport Set  Indication = @Indication, Approval=@Approval Where [Index] = @index";
				
				Cmd.Transaction = tr;
				Cmd.Parameters.Add("@index",hdIndex.Value.Trim());
				Cmd.Parameters.Add("@Indication",TextBox6.Text);
				Cmd.Parameters.Add("@Approval","기결");
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
	}
}
