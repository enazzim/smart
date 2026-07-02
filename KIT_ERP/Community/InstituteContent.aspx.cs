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
	/// InstituteContent에 대한 요약 설명입니다.
	/// </summary>
	public class InstituteContent : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnReply;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox txtContents;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lbWriter;
		private string seq;
		private string Nowname = "";
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnRealWorkStandardInfo;
		protected System.Web.UI.WebControls.Button btnRealItemOrganizationInfo;
		protected System.Web.UI.WebControls.Button btnRealProcessSequenceInfo;
		protected System.Web.UI.WebControls.Button btnExecutionPlanInfo;
		protected System.Web.UI.WebControls.Button btnBusinessPlanInfo;
		protected System.Web.UI.WebControls.Button btnPublicUseCode;
		protected System.Web.UI.WebControls.Button btnUserInfo;
		protected System.Web.UI.WebControls.Button btnBuyUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnOutSideOrderUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnSaleUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnWorkStandardInfo;
		protected System.Web.UI.WebControls.Button btnProcessSequenceInfo;
		protected System.Web.UI.WebControls.Button btnWCInfo;
		protected System.Web.UI.WebControls.Button btnEquipmentInfo;
		protected System.Web.UI.WebControls.Button btnItemOrganizationInfo;
		protected System.Web.UI.WebControls.Button btnItemInfo;
		protected System.Web.UI.WebControls.Button btnCompanyInfo;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		private string Writer = "";
		private string InstituteIndex = "";
	
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
			MyScriptCode = "if(!Edit_Check()) return false;";

			this.btnEdit.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnDelete.Attributes.Add("onclick", "return ContentDelete_Check();");
			// 현재 클릭된 글의 CCompanyStandardIndex컬럼값을 Request개체로 얻어서 그 값을 저장한다.
			InstituteIndex = Request.QueryString["seq"];

			if(!IsPostBack)
			{
				if(Request.Params["seq"] != null)
					seq = Request.Params["seq"].ToString();

				if(seq == string.Empty)
				{
					Response.Redirect("InstituteList.aspx");
					Response.End();
				}

				string id = Session["ID"].ToString();
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;				
				Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;				
				Con.Open();
				Nowname = Cmd.ExecuteScalar().ToString();
				Con.Close();
				LoadData();
				Listing();
			}	
			else
			{
				seq = ViewState["seq"].ToString();
			}
		}


		// 관계테이블 바인딩...
		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT CRelationShipIndex, StandardinfoTableID, SITIndex, case CommunityDataTableID when 'T_Institute' then '연구소' end as CommunityDataTableID , CDTIndex FROM CR_T WHERE CommunityDataTableID='T_Institute' AND CDTIndex=@InstituteIndex ORDER BY CRelationShipIndex DESC";
			Cmd.Parameters.Add("@InstituteIndex", SqlDbType.Int);			
			Cmd.Parameters["@InstituteIndex"].Value = InstituteIndex;
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds, "CR_T");
			Datagrid2.DataSource = ds.Tables[0].DefaultView;
			Datagrid2.DataKeyField = "CRelationShipIndex";			
			Datagrid2.DataBind();
			
			foreach(DataGridItem item in Datagrid2.Items)
			{
				LinkButton Lbtn = ((LinkButton)item.FindControl("LinkButton1"));
				Lbtn.Attributes.Add("onclick", "return Delete_Check();");
			}
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
			
			Cmd.CommandText = "SELECT * FROM T_Institute WHERE seq = @seq";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);			

			if(reader.Read())
			{
				ViewState["seq"] = reader["seq"].ToString();
				ViewState["thread"] = reader["thread"].ToString();
				ViewState["depth"] = reader["depth"].ToString();

				Writer = reader["writer"].ToString();
				lbWriter.Text = reader["writer"].ToString();
				lblRegistrationDate.Text = DateTime.Parse(reader["transdate"].ToString()).ToShortDateString();
				txtTitle.Text = reader["title"].ToString();
				txtContents.Text = reader["content"].ToString();
				HyperLink1.Text = reader["FileName"].ToString();
				HyperLink1.NavigateUrl = "Down.aspx?seq=" + Request.QueryString["seq"] + "&Division=Institute&filename=AppendFile";
				
				if(reader["FileSize"].ToString() == "0")
					Label1.Text = "첨부된 파일없음";

			}			

			if(Nowname == Writer)
			{
				txtTitle.ReadOnly = false;
				txtContents.ReadOnly = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;
				
				btnBusinessPlanInfo.Enabled = true;
				btnBuyUnitCostInfo.Enabled = true;
				btnCompanyInfo.Enabled = true;				
				btnEquipmentInfo.Enabled = true;
				btnExecutionPlanInfo.Enabled = true;
				btnItemInfo.Enabled = true;
				btnItemOrganizationInfo.Enabled = true;				
				btnOutSideOrderUnitCostInfo.Enabled = true;
				btnProcessSequenceInfo.Enabled = true;
				btnPublicUseCode.Enabled = true;
				btnSaleUnitCostInfo.Enabled = true;
				btnUserInfo.Enabled = true;
				btnWCInfo.Enabled = true;
				btnWorkStandardInfo.Enabled = true;
				btnRealItemOrganizationInfo.Enabled = true;
				btnRealProcessSequenceInfo.Enabled = true;
				btnRealWorkStandardInfo.Enabled = true;
			}
			else if(Session["ID"].ToString().ToLower() == "admin")
			{
				btnDelete.Enabled = true;				
			}

			reader.Close();			
			Con.Close();
		

			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "SELECT TOP 1 seq FROM T_Institute WHERE seq < @seq ORDER BY seq DESC";
			comm.Parameters.Add("@seq", SqlDbType.Int).Value = Request.QueryString["seq"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkNextContent.Visible = false;
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 seq FROM T_Institute WHERE seq > @seq ORDER BY seq ASC", conn);
			comm.Parameters.Add("@seq", SqlDbType.Int).Value = Request.QueryString["seq"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkPreContent.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			hlnkPreContent.NavigateUrl = "InstituteContent.aspx?seq=" + Pre.ToString();
			hlnkNextContent.NavigateUrl = "InstituteContent.aspx?seq=" + Next.ToString();



			comm.Parameters.Clear();

			//필독자 바인딩
			comm.CommandText = @"SELECT Name + '(' + R.ID + ')'  as Name ,R.ID FROM UI_MT inner join Relation_T as R on UI_MT.ID = R.ID WHERE (RecodingState = 1) and Division='연구소' and NO = @No ORDER BY Name";
			comm.Parameters.Add("@No",seq);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			ListBox1.DataSource = ds;
			ListBox1.DataTextField = ds.Tables[0].Columns[0].ToString();
			ListBox1.DataValueField = ds.Tables[0].Columns[1].ToString();
			ListBox1.DataBind();
			comm.Parameters.Clear();


			//현재읽는사람을 찾아서 읽었으면 그대로 두고 그렇지 않으면 ReadDate에 오늘 날짜를 기록한다.
			//그런다음 확인자에 ReadDate가 존재하는 레코드 들만 가져온다.

			//1.현재 읽는자가 필독자에 존재하는지 파악
			comm.CommandText = "Select count(ID) From Relation_T where Division='연구소' and NO = @No and [ID] = @id and ReadDate is null";
			comm.Parameters.Add("@No",seq);
			comm.Parameters.Add("@id",Session["ID"].ToString());
			int count = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(count !=0)
			{
				//
				comm.CommandText = "Update Relation_T Set ReadDate = @date where Division='연구소' and NO = @No and [ID] = @id";
				comm.Parameters.Add("@No",seq);
				comm.Parameters.Add("@id",Session["ID"].ToString());
				comm.Parameters.Add("@date",DateTime.Now);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
			comm.CommandText = "SELECT Name + '(' + R.ID + ')'  as Name, R.ReadDate FROM UI_MT inner join Relation_T as R on UI_MT.ID = R.ID WHERE (RecodingState = 1) and Division='연구소' and NO = @No and ReadDate is not null ORDER BY ReadDate, Name";
			comm.Parameters.Add("@No",seq);
			SqlDataAdapter da1 = new SqlDataAdapter(comm);
			DataSet ds1 = new DataSet();
			da1.Fill(ds1);
			DataGrid1.DataSource = ds1;
			DataGrid1.DataBind();

			comm.Parameters.Clear();

			

						
			conn.Close();
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
			this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
			this.lbkList.Click += new System.EventHandler(this.lbkList_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReply_Click(object sender, System.EventArgs e)
		{
			string thread = ViewState["thread"].ToString();
			string depth = ViewState["depth"].ToString();

			Response.Redirect("InstituteWrite.aspx?thread=" + thread + "&depth=" + depth + "&seq=" + seq);
		}

		private void lbkList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("InstituteList.aspx");
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("InstituteEdit.aspx?seq=" + ViewState["seq"].ToString());   
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;			
			
			try
			{
				Cmd.CommandText = "DELETE FROM T_Institute WHERE seq = @seq";							
				Cmd.Parameters.Add("@seq", SqlDbType.Int);
				Cmd.Parameters["@seq"].Value = seq;
				Cmd.ExecuteNonQuery();

				Cmd.CommandText = "Delete From Relation_T Where [No] = @seq and Division = '연구소'";
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
				Response.Redirect("InstituteList.aspx");
			}
		}
	}
}
