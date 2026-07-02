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

namespace KIT_ERP
{
	/// <summary>
	/// Contents에 대한 요약 설명입니다.
	/// </summary>
	public class Contents : System.Web.UI.Page
	{
		
		//공지 바인딩(T_Notice)
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		//파이프 생산관리 바인딩
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		//생산자재 바인딩(T_Product)
		protected System.Web.UI.WebControls.DataGrid DataGrid3;
		//연구소 바인딩(T_Institute)
		protected System.Web.UI.WebControls.DataGrid DataGrid4;
		//업무일지 바인딩(WorkReport)	
		protected System.Web.UI.WebControls.DataGrid DataGrid5;
		//레이저 바인딩(T_Development)
		protected System.Web.UI.WebControls.DataGrid DataGrid6;
		//대표 공지 바인딩(T_PresidentNotice)
		protected System.Web.UI.WebControls.DataGrid DataGrid7;
		//영업QC 바인딩(T_Materials)
		protected System.Web.UI.WebControls.DataGrid DataGrid8;
		//로더생산현황 바인딩 (T_Loader)
		protected System.Web.UI.WebControls.DataGrid DataGrid9;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				Listing();
		}

		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			//공지 바인딩
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate FROM T_Notice Where thread % 1000 = 0  ORDER BY seq DESC";

			SqlDataAdapter adp1 = new SqlDataAdapter(Cmd);
			DataSet ds1 = new DataSet();

			adp1.Fill(ds1);
			DataGrid1.DataSource = ds1.Tables[0].DefaultView;
			DataGrid1.DataBind();



			//파이프 생산관리 바인딩
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Pipe Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp2 = new SqlDataAdapter(Cmd);
			DataSet ds2 = new DataSet();

			adp2.Fill(ds2);
			DataGrid2.DataSource = ds2.Tables[0].DefaultView;
			DataGrid2.DataBind();


			//생산자재 바인딩(T_Product)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Product Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp3 = new SqlDataAdapter(Cmd);
			DataSet ds3 = new DataSet();

			adp3.Fill(ds3);
			DataGrid3.DataSource = ds3.Tables[0].DefaultView;
			DataGrid3.DataBind();


			//연구소 바인딩(T_Institute)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Institute Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp4 = new SqlDataAdapter(Cmd);
			DataSet ds4 = new DataSet();

			adp4.Fill(ds4);
			DataGrid4.DataSource = ds4.Tables[0].DefaultView;
			DataGrid4.DataBind();


			//업무일지 바인딩(WorkReport)
			if(Session["ID"].ToString().ToLower() == "77sindong")
			{
				Cmd.CommandText = "SELECT TOP 4 WR.[Index], Convert(varchar, WR.ReportDate,111)+' '+(Select W.RegistrationPerson From WorkReport W where W.[Index]=WR.[Index] )+  ' 업무일지' as ReportDate , WR.RegistrationDate FROM WorkReport WR where RegistrationPerson is not null   ORDER BY WR.RegistrationDate DESC , [Index] desc";
			}
			else
			{
				Cmd.CommandText = "SELECT TOP 4 [Index], Convert(varchar, ReportDate, 111)+' "+Session["UserName"].ToString()+" 업무일지' as ReportDate, RegistrationDate   FROM WorkReport where RegistrationID = @id   ORDER BY RegistrationDate DESC , [Index] DESC";
				Cmd.Parameters.Add("@id",Session["ID"].ToString());
			}
			SqlDataAdapter adp5 = new SqlDataAdapter(Cmd);
			DataSet ds5 = new DataSet();

			adp5.Fill(ds5);
			DataGrid5.DataSource = ds5.Tables[0].DefaultView;
			DataGrid5.DataBind();



			//레이저 바인딩(T_Development)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Development Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp6 = new SqlDataAdapter(Cmd);
			DataSet ds6 = new DataSet();

			adp6.Fill(ds6);
			DataGrid6.DataSource = ds6.Tables[0].DefaultView;
			DataGrid6.DataBind();


			//대표 공지 바인딩(T_PresidentNotice)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_PresidentNotice Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp7 = new SqlDataAdapter(Cmd);
			DataSet ds7 = new DataSet();

			adp7.Fill(ds7);
			DataGrid7.DataSource = ds7.Tables[0].DefaultView;
			DataGrid7.DataBind();

			//영업QC 바인딩(T_Materials)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Materials Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp8 = new SqlDataAdapter(Cmd);
			DataSet ds8 = new DataSet();

			adp8.Fill(ds8);
			DataGrid8.DataSource = ds8.Tables[0].DefaultView;
			DataGrid8.DataBind();


			//로더생산현황 바인딩 (T_Loader)
			Cmd.CommandText = @"SELECT TOP 4 seq, case 
									when len(Title) > 20 then 
										LEFT(Title, 20) + '...' 
									else Title 
			                      end  as Title, transdate as RegistrationDate  FROM T_Loader Where thread % 1000 = 0  ORDER BY seq DESC";


			SqlDataAdapter adp9 = new SqlDataAdapter(Cmd);
			DataSet ds9 = new DataSet();

			adp9.Fill(ds9);
			DataGrid9.DataSource = ds9.Tables[0].DefaultView;
			DataGrid9.DataBind();


			

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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.DataGrid7.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid7_ItemDataBound);
			this.DataGrid5.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid5_ItemDataBound);
			this.DataGrid3.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid3_ItemDataBound);
			this.DataGrid6.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid6_ItemDataBound);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
			this.DataGrid4.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid4_ItemDataBound);
			this.DataGrid8.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid8_ItemDataBound);
			this.DataGrid9.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid9_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='공지사항'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='개발실'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		private void DataGrid3_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='생산품질'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		private void DataGrid4_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='자재관리'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		private void DataGrid5_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 1440)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
			}
		}

		private void DataGrid6_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='연구소'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		private void DataGrid7_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='대표공지'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		//
		private void DataGrid8_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='로더생산현황'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}

		//로더생산현황 바인딩 (T_Loader)
		private void DataGrid9_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = " <img src=images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
				else
				{
					//읽은사람 가져오기
					string index = ((DataRowView)e.Item.DataItem)["seq"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Cmd.CommandText = "Select Count(ID) From Relation_T Where ReadDate is null and [No] = @no and Division='로더생산현황'";
					Cmd.Parameters.Add("@no", index);
					Con.Open();
					int count = int.Parse(Cmd.ExecuteScalar().ToString());

					if(count != 0)
					{
						Literal l = new Literal();
						l.Text = " <img src=images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}
			}
		}



	}
}
