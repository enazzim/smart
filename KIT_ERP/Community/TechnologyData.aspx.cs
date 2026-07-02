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
	/// TechnologyData에 대한 요약 설명입니다.
	/// </summary>
	public class TechnologyData : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnWrite;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lblPageInfo;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Button Button1;
		private int iPage = 0;		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}

			Response.Cookies["CTD_T"]["UpdateHits"] = "0";
			// CTD_T라는 쿠키값이 존재하면 pageNumber값을 iPage변수에 저장하고
			if(Request.Cookies["CTD_T"] != null)
			{
				string pageNumber = Request.Cookies["CTD_T"]["PageNumber"];				
				if(pageNumber != null)
					iPage = int.Parse(pageNumber);
				
					// pageNumber값이 존재하지 않는다면 첫페이지 출력을 위해 iPage = 0으로 한다.
				else
					
					iPage = 0;
				DataGrid1.CurrentPageIndex = iPage;
			}
		
			// 현재 로그인된 사용자가 admin이 아니면 글쓰기 버튼을 사용할 수 없다.
//			if(Session["ID"].ToString() != "admin")
//				btnWrite.Enabled = false;
			
			// PageSize값과 VirtualItemCount값을 지정
			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 커뮤니티 > 기술자료실';</script>");
				DataGrid1.PageSize = 15;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();
				page();
				Listing();
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
			this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
	
		private void page()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
								
			Cmd.CommandText = "SELECT Distinct Category FROM CCG_T WHERE Classify = '5'";

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

		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT TOP 15 CTechnologyDataIndex, Title, Contents, FileName, RegistrationPerson, Convert(varchar, CTD_T.RegistrationDate, 111)RegistrationDate, Hits FROM CTD_T WHERE CTechnologyDataIndex NOT IN (SELECT TOP "+iPage*15+" CTechnologyDataIndex FROM CTD_T ORDER BY CTechnologyDataIndex desc) ORDER BY CTechnologyDataIndex desc";

			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds, "CTD_T");
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataBind();

			lblPageInfo.Text =  string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
		}




		// CTD_T 테이블의 총 레코드 수 구하기
		private int GetTotalRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			string strSql = "SELECT Count(*) FROM CTD_T";
			SqlCommand Cmd = new SqlCommand(strSql, Con);
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
		}


		// 페이지번호 클릭시...
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			iPage = e.NewPageIndex;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			
			// 페이지번호 클릭시 현재 선택한 페이지번호를 쿠키에 저장
			Response.Cookies["CTD_T"]["PageNumber"] = iPage.ToString();
			Listing();
		}



		// 새로운 이미지 표시...
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

			// 마우스 오버시 색깔...
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}



		// 글쓰기 버튼...
		private void btnWrite_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("TnRegistration.aspx");		
		}



		// 데이타그리드 바인딩시 레코드가 하나도 없을때...
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
					summaryCell.Text = "등록된 글이 없습니다.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			if(DropDownList1.SelectedIndex == 0)
				Response.Write("<script>alert('삭제할 Category를 선택해주십시오');</script>");
			else
			{
				try
				{
					Cmd.CommandText = "DELETE FROM CCG_T WHERE Category = @Category AND Classify = 5";
					Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
					Cmd.ExecuteNonQuery();
					
					Cmd.CommandText = "DELETE FROM CTD_T WHERE Category = @Category2";
					Cmd.Parameters.Add("@Category2", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
					Cmd.ExecuteNonQuery();
					
					trans.Commit();
					Con.Close();	
					Response.Write("<script>alert('삭제되었습니다.');location.href='TechnologyData.aspx'; </script>");
				}
				catch(Exception ex)
				{
					Response.Write(ex.Message);
					trans.Rollback();
					Con.Close();	
				}
				finally
				{
					page();
				}
				
			}
		}

		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;			

			string Category = DropDownList1.SelectedItem.Text;

			if(DropDownList1.SelectedIndex == 0)
			{
				DataGrid1.CurrentPageIndex = 0;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();
				Cmd.CommandText = "SELECT TOP 10 CTechnologyDataIndex, Title, Contents, RegistrationPerson, Convert(varchar, CTD_T.RegistrationDate, 111)RegistrationDate, Hits FROM CTD_T WHERE CTechnologyDataIndex NOT IN(SELECT TOP "+iPage*10+" CTechnologyDataIndex FROM CTD_T ORDER BY CTechnologyDataIndex desc) ORDER BY CTechnologyDataIndex desc";
			}
			else
			{
				DataGrid1.CurrentPageIndex = 0;
				DataGrid1.VirtualItemCount = GetSearchRecordCount();
				Cmd.CommandText = "SELECT TOP 10 CTechnologyDataIndex, Title, Contents, RegistrationPerson, Convert(varchar, CTD_T.RegistrationDate, 111)RegistrationDate, Hits FROM CTD_T WHERE Category = @Category AND CTechnologyDataIndex NOT IN(SELECT TOP "+iPage*10+" CTechnologyDataIndex FROM CTD_T WHERE Category = @Category ORDER BY CTechnologyDataIndex desc) ORDER BY CTechnologyDataIndex desc";
				Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = Category;				
			}						

			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			adp.Fill(ds);
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataKeyField = "CTechnologyDataIndex"; 
			DataGrid1.DataBind();
			lblPageInfo.Text = string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
		}	

		// DropDownList 선택항목에 따라 레코드 수 구하기...
		private int GetSearchRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			Cmd.CommandText = "SELECT Count(*) FROM CTD_T WHERE Category = @Category";			
			Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
						
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
		}
	}
}
