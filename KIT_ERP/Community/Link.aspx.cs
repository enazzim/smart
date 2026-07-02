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
	/// Link에 대한 요약 설명입니다.
	/// </summary>
	public class Link : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Label lblPageInfo;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		private int iPage = 0;
		private string id = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{	
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 유효성 검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!add()) return false;";

			this.Button3.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			Button4.Attributes.Add("onclick", "return del();");


			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 커뮤니티 > 관련 SITE';</script>");
				page();
//				DataGrid1.PageSize = 16;
//				DataGrid1.VirtualItemCount = GetTotalRecordCount();
//
//				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
//				SqlConnection Con = new SqlConnection(ConnectStr);
//				SqlCommand Cmd = new SqlCommand();
//				Cmd.Connection = Con;
//				Con.Open();
//				
//				id = Session["ID"].ToString();
//				Cmd.CommandText = "SELECT Category FROM CCG_T WHERE RegistrationPersonID = @id OR (Category = 'Category' AND Classify = '2')";
//				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
//				SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
//            
//				DropDownList1.DataSource = reader;
//				DropDownList1.DataTextField = "Category";
//				DropDownList1.DataBind();
//				Listing();				
			}			
		}

		private void page()
		{
			DataGrid1.PageSize = 16;
			DataGrid1.VirtualItemCount = GetTotalRecordCount();

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
				
			id = Session["ID"].ToString();
			Cmd.CommandText = "SELECT Distinct Category FROM CCG_T WHERE RegistrationPersonID = @id AND Classify = '2'";
			Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
//			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
//            
//			DropDownList1.DataSource = reader;
//			DropDownList1.DataTextField = "Category";
//			DropDownList1.DataBind();
			SqlDataAdapter da = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			DropDownList1.DataSource = ds;
			DropDownList1.DataTextField = ds.Tables[0].Columns[0].ToString();
			DropDownList1.DataValueField = ds.Tables[0].Columns[0].ToString();
			DropDownList1.DataBind();


			Con.Close();
            Listing();				

		}

		// 데이타 바인딩...
		private void Listing()
		{			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			id = Session["ID"].ToString();
			Cmd.CommandText = "SELECT TOP 16 CLinkIndex, Classification, SiteName, Link FROM CL_T WHERE RegistrationPersonID = @id AND CLinkIndex NOT IN (SELECT TOP "+iPage*16+" CLinkIndex FROM CL_T WHERE RegistrationPersonID = @id ORDER BY CLinkIndex DESC) ORDER BY CLinkIndex DESC";
			Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			adp.Fill(ds, "CL_T");
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataKeyField = "CLinkIndex";
			DropDownList1.Items.Insert(0, "전체선택") ;
			DropDownList1.Items[0].Value = "";
			DataGrid1.DataBind();				

			lblPageInfo.Text = string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
			Button2.Attributes.Add("onclick", "return Delete_Check();");
		}



		// 전체 레코드 수 구하기...
		private int GetTotalRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			id = Session["ID"].ToString();
			Cmd.CommandText = "SELECT Count(*) FROM CL_T WHERE RegistrationPersonID = @id";
			Cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
			
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
		}



		// DropDownList 선택항목에 따라 레코드 수 구하기...
		private int GetSearchRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			id = Session["ID"].ToString();
			Cmd.CommandText = "SELECT Count(*) FROM CL_T WHERE RegistrationPersonID = @id AND Classification = @Classification";
			Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
			Cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
						
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
			this.DropDownList1.SelectedIndexChanged += new System.EventHandler(this.DropDownList1_SelectedIndexChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		// DropDownList에서 항목 선택시...
		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;			

			string Category = DropDownList1.SelectedItem.Text;
			id = Session["ID"].ToString();

			if(DropDownList1.SelectedIndex == 0)
			{
				DataGrid1.CurrentPageIndex = 0;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();
				Cmd.CommandText = "SELECT TOP 16 CLinkIndex, Classification, SiteName, Link FROM CL_T WHERE RegistrationPersonID = @id AND CLinkIndex NOT IN (SELECT TOP "+iPage*16+" CLinkIndex FROM CL_T WHERE RegistrationPersonID = @id ORDER BY CLinkIndex DESC) ORDER BY CLinkIndex DESC";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
			}
			else
			{
				DataGrid1.CurrentPageIndex = 0;
				DataGrid1.VirtualItemCount = GetSearchRecordCount();
				Cmd.CommandText = "SELECT TOP 16 CLinkIndex, Classification, SiteName, Link FROM CL_T WHERE RegistrationPersonID = @id AND Classification = @Classification AND CLinkIndex NOT IN (SELECT TOP "+iPage*16+" CLinkIndex FROM CL_T WHERE RegistrationPersonID = @id AND Classification = @Classification ORDER BY CLinkIndex DESC) ORDER BY CLinkIndex DESC";			
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				Cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = Category;				
			}						


			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			adp.Fill(ds);
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataKeyField = "CLinkIndex"; 

			DataGrid1.DataBind();
			lblPageInfo.Text = string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
		}

		// 추가버튼 팝업창...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/LinkAdd.aspx', 'LinkAdd', 'width=392, height=133');</script>");
		}


		// 삭제버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			bool count = false;
			id = Session["ID"].ToString();

			foreach(DataGridItem item in DataGrid1.Items)
			{				
				CheckBox cb = (CheckBox)item.FindControl("CheckBox1");
				if(cb.Checked)
				{
					count = true;
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					Con.Open();
					SqlCommand Cmd = new SqlCommand();
					SqlTransaction trans = Con.BeginTransaction();
					Cmd.Connection = Con;
					Cmd.Transaction = trans;

					try
					{
						Cmd.CommandText = "DELETE FROM CL_T WHERE RegistrationPersonID = @id AND SiteName = @SiteName";
						Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
						Cmd.Parameters.Add("@SiteName", SqlDbType.VarChar).Value = item.Cells[2].Text;						
						Cmd.ExecuteNonQuery();
						trans.Commit();
						Response.Write("<script>alert('삭제되었습니다.'); location.href='Link.aspx';; </script>");
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
					}
				}
			}

			// 체크된 항목이 없는 경우...
			if(!count)
				Response.Write("<script>alert('선택된 항목이 없습니다!');</script>");
		}

        
		// 그리드에 바인딩할 항목이 없을때...
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
					summaryCell.Text = "등록된 항목이 없습니다.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}


		// 페이지번호 클릭시...
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{			
			iPage = e.NewPageIndex;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			
			id = Session["ID"].ToString();			
			if(DropDownList1.SelectedIndex == 0)
				Listing();
			else
			{					
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;			

				string Category = DropDownList1.SelectedItem.Text;

				DataGrid1.VirtualItemCount = GetSearchRecordCount();
				Cmd.CommandText = "SELECT TOP 16 CLinkIndex, Classification, SiteName, Link FROM CL_T WHERE RegistrationPersonID = @id AND Classification = @Classification AND CLinkIndex NOT IN (SELECT TOP "+iPage*16+" CLinkIndex FROM CL_T WHERE RegistrationPersonID = @id AND Classification = @Classification ORDER BY CLinkIndex DESC) ORDER BY CLinkIndex DESC";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				Cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = Category;
				
				SqlDataAdapter adp = new SqlDataAdapter(Cmd);
				DataSet ds = new DataSet();
				adp.Fill(ds, "CL_T");
				DataGrid1.DataSource = ds.Tables[0].DefaultView;
				DataGrid1.DataKeyField = "CLinkIndex";
				DataGrid1.DataBind();

				lblPageInfo.Text = string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);	
			}
		}

		// Mouse over...Color
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}


		// Category 추가버튼..
		private void Button3_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;
			
			id = Session["ID"].ToString();

			try
			{
				Cmd.CommandText = "INSERT INTO CCG_T(Category, RegistrationPersonID, Classify) VALUES(@Category, @id, @Classify)";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = TextBox1.Text;
				Cmd.Parameters.Add("@Classify", SqlDbType.SmallInt).Value = 2;
				Cmd.ExecuteNonQuery();
				trans.Commit();
				Response.Write("<script>alert('등록되었습니다.'); location.href='Link.aspx';; </script>");
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				trans.Rollback();
			}
			finally
			{				
				Con.Close();				
			}
		}


		// Category 삭제버튼...
		private void Button4_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			id = Session["ID"].ToString();
			string id2 = id;
			if(DropDownList1.SelectedItem.Text == "Category")
				Response.Write("<script>alert('삭제할 Category를 선택해주십시오');</script>");
			else
			{
				try
				{
					Cmd.CommandText = "DELETE FROM CCG_T WHERE RegistrationPersonID = @id AND Category = @Category";
					Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
					Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
					Cmd.ExecuteNonQuery();
					
					Cmd.CommandText = "DELETE FROM CL_T WHERE RegistrationPersonID = @id2 AND Classification = @Classification";
					Cmd.Parameters.Add("@id2", SqlDbType.VarChar).Value = id2;
					Cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
					Cmd.ExecuteNonQuery();

					trans.Commit();
					Response.Write("<script>alert('삭제되었습니다.'); location.href='Link.aspx';; </script>");
				}
				catch(Exception ex)
				{
					Response.Write(ex.Message);
					trans.Rollback();
				}
				finally
				{
					
					Con.Close();				
				}
			}
		}		
	}
}







