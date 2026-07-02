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
	/// FreeBoard에 대한 요약 설명입니다.
	/// </summary>
	public class FreeBoard : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnWrite;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lblPageInfo;
		private int iPage = 0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Session["ID"] = "admin";

			Response.Cookies["HFB_T"]["UpdateHits"] = "0";
			// HFB_T라는 쿠키값이 존재하면 pageNumber값을 iPage변수에 저장하고
			if(Request.Cookies["HFB_T"] != null)
			{
				string pageNumber = Request.Cookies["HFB_T"]["PageNumber"];				
				if(pageNumber != null)
					iPage = int.Parse(pageNumber);
				
					// pageNumber값이 존재하지 않는다면 첫페이지 출력을 위해 iPage = 0으로 한다.
				else					
					iPage = 0;
				
				DataGrid1.CurrentPageIndex = iPage;
			}		
						
			// PageSize값과 VirtualItemCount값을 지정
			if(!IsPostBack)
			{
				DataGrid1.PageSize = 10;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();
				Label1.Text = "◀ 자 유 게 시 판 ▶";
				Listing();
			}
		}


		// 목록출력
		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT TOP 10 HFreeBoardIndex, Title, Contents, RegistrationPerson, Convert(varchar, HFB_T.RegistrationDate, 111)RegistrationDate, Hits, Depth, Password FROM HFB_T WHERE HFreeBoardIndex NOT IN (SELECT TOP "+iPage*10+" HFreeBoardIndex FROM HFB_T ORDER BY HFreeBoardIndex desc) ORDER BY HFreeBoardIndex desc";

			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds, "HFB_T");
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataBind();

			lblPageInfo.Text =  string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);			
		}


		// HFB_T 테이블의 총 레코드 수 구하기
		private int GetTotalRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			string strSql = "SELECT Count(*) FROM HFB_T";
			SqlCommand Cmd = new SqlCommand(strSql, Con);
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
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
			this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// 페이지번호 클릭...
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			iPage = e.NewPageIndex;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			
			// 페이지번호 클릭시 현재 선택한 페이지번호를 쿠키에 저장
			Response.Cookies["HFB_T"]["PageNumber"] = iPage.ToString();
			Listing();
		}



		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// 새로운글 이미지 표시
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["RegistrationDate"].ToString();
				int depth = int.Parse(((DataRowView)e.Item.DataItem)["Depth"].ToString());

				if(depth > 0 )
				{
					System.Web.UI.WebControls.Image blankImg;
					blankImg = new System.Web.UI.WebControls.Image();
					blankImg.ImageUrl = "../Images/blank.gif";
					blankImg.Height = Unit.Pixel(0);
					blankImg.Width = Unit.Pixel(depth * 15);

					e.Item.Cells[0].Controls.AddAt(0, blankImg);
					blankImg = new System.Web.UI.WebControls.Image();
					blankImg.ImageUrl = "../Images/re.gif";
					e.Item.Cells[0].Controls.AddAt(1, blankImg);
				}

				DateTime orginDate = DateTime.Parse(strDate);
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 1440)
				{
					Literal l = new Literal();
					l.Text = " <img src=../images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}
			}

			// 마우스오버시 색깔...
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}


		// 글쓰기 버튼...
		private void btnWrite_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("FreeRegistration.aspx");
		}


		// 그리드에 바인딩할 항목이 하나도 없을때...
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
	}
}
