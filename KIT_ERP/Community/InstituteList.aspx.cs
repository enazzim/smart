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
	/// InstituteList에 대한 요약 설명입니다.
	/// </summary>
	public class InstituteList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnWrite;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				DataGrid1.DataSource = Search();
				DataGrid1.DataBind();
			}
		}

		private DataSet Search()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"Select  seq, thread, depth, writer, title, Convert(varchar, transdate, 111) as transdate FROM T_Institute ORDER BY thread desc";			
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds);

			return ds;
			
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

		private void btnWrite_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("InstituteWrite.aspx");
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			DataGrid1.DataSource = Search();
			DataGrid1.DataBind();
		
		}

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
					summaryCell.Text = "등록된 글이 없습니다!.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// 새로운글 이미지 표시
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["transdate"].ToString();
				string seq = ((DataRowView)e.Item.DataItem)["seq"].ToString();												 
				

				DateTime orginDate = DateTime.Parse(strDate);
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 2880)
				{
					Literal l = new Literal();
					l.Text = "&nbsp; <img src=../images/new.gif>";
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
						l.Text = "&nbsp; <img src=../images/new.gif>";
						e.Item.Cells[0].Controls.Add(l);
					}
				}

				int depth = (int)((DataRowView)e.Item.DataItem)["depth"];

				if(depth > 0 )
				{
					System.Web.UI.WebControls.Image blankImg;
					blankImg = new System.Web.UI.WebControls.Image();
					blankImg.ImageUrl = "../images/blank.gif";
					blankImg.Height = Unit.Pixel(0);
					blankImg.Width = Unit.Pixel(depth * 15);

					e.Item.Cells[0].Controls.AddAt(0, blankImg);

					blankImg = new System.Web.UI.WebControls.Image();
					blankImg.ImageUrl = "../images/re.gif";

					e.Item.Cells[0].Controls.AddAt(1, blankImg);
				}
			}

			// 마우스오버시 색깔...
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#F0F0F0'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}


		private bool Exist(string item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select count(ID) From Relation_T where No = @no  and ReadDate is not null and Division = '개발실'";
			comm.Parameters.Add("@no",item);
			int result = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			conn.Close();
			if(result == 0)
				return false;
			else
				return true;
		}
	}
}
