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

namespace KIT_ERP.Community
{
	/// <summary>
	/// WorkDiary에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDiary : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lblPageInfo;
		protected System.Web.UI.WebControls.Button Button1;
		private int iPage = 0;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			
			//id 권한이 00인경우를 제외하고 무조건 신규등록은 보여야 된다
			if(IDAuthority(Session["ID"].ToString().ToLower()))
			{
				if(Session["ID"] == "sdi21")
				{
					Button1.Visible = false;
				}
				else
					Button1.Visible = true;
			}
			else
			{
				Button1.Visible = false;
			}

			//



			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 커뮤니티 > 업무일지';</script>");
				DataGrid1.PageSize = 15;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();
				Listing();
				DropdownListToday();
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
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		// 목록출력
		/// <summary>
		/// 목록출력시 id가 sdi21인경우 전부 리스팅 하고 그렇지 않으면 id인것만 리스팅
		/// </summary>
		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;	
			if(Session["ID"].ToString().ToLower() == "sdi21")
				Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate + ' 업무일지' as ReportDate, Approval, WorkDiaryWriter, [Index] From WorkReport order by [Index] desc";
			else
				Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate + ' 업무일지' as ReportDate, Approval, WorkDiaryWriter, [Index] From WorkReport where WorkDiaryWriter = @id order by [Index] desc";
//			Cmd.CommandText = "SELECT TOP 15 WorkDateTitle+' 업무일지' as WorkDateTitle,  NoWriter, Closing, RegistrationDate FROM CWD_T WHERE Listing = 1 AND WorkDateTitle NOT IN(SELECT TOP "+iPage*15+" WorkDateTitle FROM CWD_T WHERE Listing = 1 GROUP BY WorkDateTitle ORDER BY WorkDateTitle DESC) GROUP BY WorkDateTitle, NoWriter, Closing, RegistrationDate ORDER BY WorkDateTitle DESC";
			
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds, "CWD_T");
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataBind();
			lblPageInfo.Text =  string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
		}


		private void DropdownListToday()
		{
			string yy = DateTime.Now.ToString().Substring(0,4);
			string mm = DateTime.Now.ToString().Substring(5,2);
			string dd = DateTime.Now.ToString().Substring(8,2);
			
			DropDownList1.SelectedItem.Text = yy;
			DropDownList2.SelectedItem.Text = mm;
			DropDownList3.SelectedItem.Text = dd;
		}



		// CWD_T 테이블의 총 레코드 수 구하기
		private int GetTotalRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			string strSql = "SELECT Count(*) FROM CWD_T";
			SqlCommand Cmd = new SqlCommand(strSql, Con);
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return (totalCount/10);
		}


		// 페이지번호 클릭시...
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			iPage = e.NewPageIndex;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			
			// 페이지번호 클릭시 현재 선택한 페이지번호를 쿠키에 저장
			Response.Cookies["CN_T"]["PageNumber"] = iPage.ToString();
			Listing();
		}



		// 새로운글 이미지 표시
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);

				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 1440)
				{
					Literal l = new Literal();
					l.Text = " <img src=../images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
			}

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}


		// 레코드가 하나도 없을때...
		private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			TableCell summaryCell;
			if ( e.Item.ItemType == ListItemType.Footer )
			{
				int cellCountNum = e.Item.Cells.Count;

				for ( int i = 1 ; i < cellCountNum ;i++ )
				{
					e.Item.Cells.RemoveAt(0); //footer의 칼럼을 1개로 합치기
				} 

				((DataGrid)sender).ShowFooter = false;
				summaryCell = e.Item.Cells[0];

				if( ((DataGrid)sender).Items.Count == 0)
				{
					summaryCell.Text = "리스팅할 수 있는 업무일지가 없습니다.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}


		// 리스팅 안함 버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			string yy = DropDownList1.SelectedItem.Text;
			string mm = DropDownList2.SelectedItem.Text;
			string dd = DropDownList3.SelectedItem.Text;			
			string yymmdd = yy + "/" + mm + "/" + dd;
			int count = 0;

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();			
			SqlTransaction trans = Con.BeginTransaction();			
			Cmd.Transaction = trans;

			try
			{
				Cmd.CommandText = "UPDATE CWD_T SET Listing=@Listing WHERE WorkDateTitle=@yymmdd";
				Cmd.Parameters.Add("@Listing", SqlDbType.Bit).Value = 0;
				Cmd.Parameters.Add("@yymmdd", SqlDbType.VarChar).Value = yymmdd;
				
				count = Cmd.ExecuteNonQuery();
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
				Listing();
			}

			if(count == 0)
			{
				Response.Write("<script>alert('해당일의 업무일지는 리스팅되어 있지 않습니다.');</script>");
			}
		}


		// 정리버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			string yy = DropDownList1.SelectedItem.Text;
			string mm = DropDownList2.SelectedItem.Text;
			string dd = DropDownList3.SelectedItem.Text;			
			string yymmdd = yy + "/" + mm + "/" + dd;
			int count = 0;

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();			
			SqlTransaction trans = Con.BeginTransaction();			
			Cmd.Transaction = trans;

			try
			{
				Cmd.CommandText = "DELETE FROM CWD_T WHERE Listing = 1 AND WorkDateTitle=@yymmdd";				
				Cmd.Parameters.Add("@yymmdd", SqlDbType.VarChar).Value = yymmdd;
				
				count = Cmd.ExecuteNonQuery();
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
				Listing();
			}

			if(count == 0)
			{
				Response.Write("<script>alert('해당일의 업무일지는 이미 정리되었거나 목록에 없습니다.');</script>");
			}
		}

		
		// 리스팅 버튼...
		private void Button3_Click(object sender, System.EventArgs e)
		{
			string yy = DropDownList1.SelectedItem.Text;
			string mm = DropDownList2.SelectedItem.Text;
			string dd = DropDownList3.SelectedItem.Text;			
			string yymmdd = yy + "/" + mm + "/" + dd;
			int count = 0;

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();			
			SqlTransaction trans = Con.BeginTransaction();			
			Cmd.Transaction = trans;

			try
			{
				Cmd.CommandText = "UPDATE CWD_T SET Listing=@Listing WHERE WorkDateTitle=@yymmdd";
				Cmd.Parameters.Add("@Listing", SqlDbType.Bit).Value = 1;
				Cmd.Parameters.Add("@yymmdd", SqlDbType.VarChar).Value = yymmdd;
				
				count = Cmd.ExecuteNonQuery();
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
				Listing();
			}

			if(count == 0)
			{
				Response.Write("<script>alert('해당일의 업무일지는 존재하지않거나 이미 리스팅되어 있습니다.');</script>");
			}
		}

		
		// 업무일지 생성...
		private void Button4_Click(object sender, System.EventArgs e)
		{

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ID,Name From UI_MT Where RecodingState = 1 and (ID != 'admin' and BusinessRegistrationNum = '' ) order by ID";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds1 = new DataSet() ;
			da.Fill(ds1);


			string yy = DropDownList1.SelectedItem.Text;
			string mm = DropDownList2.SelectedItem.Text;
			string dd = DropDownList3.SelectedItem.Text;			
			string yymmdd = yy + "/" + mm + "/" + dd;	
			string id = "";
			string name = "";

			if(ds1.Tables[0].Rows.Count == 0)
			{
				RegisterStartupScript("","<script language=javascript>alert('관리자외에 사용자가 없습니다!');</script>");
			}
			else
			{
				foreach(DataRow dr in ds1.Tables[0].Rows)
				{
					id = dr["ID"].ToString();
					name = dr["Name"].ToString();

					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;
					Con.Open();
			
					Cmd.CommandText = "INSERT INTO CWD_T(WorkDateTitle, Id, Name) VALUES(@WorkDateTitle, @Id, @Name)";
					Cmd.Parameters.Add("@WorkDateTitle", SqlDbType.VarChar).Value = yymmdd;
					Cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
					Cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
					Cmd.ExecuteNonQuery();
					Con.Close();
				}
			}

			Response.Redirect("WorkDiary.aspx");
		}
	}
}
