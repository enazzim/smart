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
	/// TnAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class TnAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblRegistrationPerson;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblHits;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtContents;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Button btnCompanyInfo;
		protected System.Web.UI.WebControls.Button btnItemInfo;
		protected System.Web.UI.WebControls.Button btnItemOrganizationInfo;
		protected System.Web.UI.WebControls.Button btnEquipmentInfo;
		protected System.Web.UI.WebControls.Button btnWCInfo;
		protected System.Web.UI.WebControls.Button btnProcessSequenceInfo;
		protected System.Web.UI.WebControls.Button btnWorkStandardInfo;
		protected System.Web.UI.WebControls.Button btnSellUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnOutSideOrderUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnBuyUnitCostInfo;
		protected System.Web.UI.WebControls.Button btnUserInfo;
		protected System.Web.UI.WebControls.Button btnPublicUseCode;
		protected System.Web.UI.WebControls.Button btnBusinessPlanInfo;
		protected System.Web.UI.WebControls.Button btnExecutionPlanInfo;
		protected System.Web.UI.WebControls.Button btnMonthlyClosingInfo;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		
		private string CTechnologyDataIndex = "";		
		private string updateHits = "0";				
		private static byte [] Files1;        
		private string Nowname = "";
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		private string Writer = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			// 유효성 검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Edit_Check()) return false;";

			this.btnEdit.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnDelete.Attributes.Add("onclick", "return ContentDelete_Check();");

			// 현재 클릭된 글의 CTechnologyDataIndex컬럼값을 Request개체로 얻어서 그 값을 저장한다.
			CTechnologyDataIndex = Request.QueryString["CTechnologyDataIndex"];
			// CTechnologyDataIndex값이 null이면 사용자가 TechnologyData.aspx 페이지에서 특정 제목을 클릭하여 TnAdjustment.aspx 페이지로
			// 이동해 온 것이 아니라, 사용자가 브라우저에 직접적으로 TnAdjustment.aspx 페이지를 요청하여 접근한 것이므로
			// Response.Redirect 메서드를 사용하여 TechnologyData.aspx 페이지로 이동시킨다.
			if(CTechnologyDataIndex == null)
				Response.Redirect("TechnologyData.aspx");

			if(Request.Cookies["CTD_T"] != null)
				updateHits = Request.Cookies["CTD_T"]["UpdateHits"];

			if(!IsPostBack)
			{
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
				LoadCategory();
				LoadData();
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
			this.lbkList.Click += new System.EventHandler(this.lbkList_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnCompanyInfo.Click += new System.EventHandler(this.btnCompanyInfo_Click);
			this.btnItemInfo.Click += new System.EventHandler(this.btnItemInfo_Click);
			this.btnItemOrganizationInfo.Click += new System.EventHandler(this.btnItemOrganizationInfo_Click);
			this.btnEquipmentInfo.Click += new System.EventHandler(this.btnEquipmentInfo_Click);
			this.btnWCInfo.Click += new System.EventHandler(this.btnWCInfo_Click);
			this.btnProcessSequenceInfo.Click += new System.EventHandler(this.btnProcessSequenceInfo_Click);
			this.btnWorkStandardInfo.Click += new System.EventHandler(this.btnWorkStandardInfo_Click);
			this.btnSellUnitCostInfo.Click += new System.EventHandler(this.btnSellUnitCostInfo_Click);
			this.btnOutSideOrderUnitCostInfo.Click += new System.EventHandler(this.btnOutSideOrderUnitCostInfo_Click);
			this.btnBuyUnitCostInfo.Click += new System.EventHandler(this.btnBuyUnitCostInfo_Click);
			this.btnUserInfo.Click += new System.EventHandler(this.btnUserInfo_Click);
			this.btnPublicUseCode.Click += new System.EventHandler(this.btnPublicUseCode_Click);
			this.btnBusinessPlanInfo.Click += new System.EventHandler(this.btnBusinessPlanInfo_Click);
			this.btnExecutionPlanInfo.Click += new System.EventHandler(this.btnExecutionPlanInfo_Click);
			this.btnMonthlyClosingInfo.Click += new System.EventHandler(this.btnMonthlyClosingInfo_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		private void LoadCategory()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;				
			Con.Open();
			Cmd.CommandText = "SELECT Distinct Category FROM CCG_T WHERE Classify = 5";
				
			SqlDataAdapter da = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
				
			DropDownList1.DataSource = ds;
			DropDownList1.DataTextField = ds.Tables[0].Columns[0].ToString();
			DropDownList1.DataValueField = ds.Tables[0].Columns[0].ToString();
			DropDownList1.DataBind();
			Con.Close();
		}

		// 관계테이블 바인딩...
		private void Listing()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT CRelationShipIndex, StandardinfoTableID, SITIndex, case CommunityDataTableID when 'CTD_T' then '기술자료실' end as CommunityDataTableID , CDTIndex FROM CR_T WHERE CommunityDataTableID='CTD_T' AND CDTIndex=@CTechnologyDataIndex ORDER BY CRelationShipIndex DESC";
			Cmd.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int);			
			Cmd.Parameters["@CTechnologyDataIndex"].Value = CTechnologyDataIndex;
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds, "CR_T");
			DataGrid1.DataSource = ds.Tables[0].DefaultView;
			DataGrid1.DataKeyField = "CRelationShipIndex";			
			DataGrid1.DataBind();
			
			foreach(DataGridItem item in DataGrid1.Items)
			{
				LinkButton Lbtn = ((LinkButton)item.FindControl("LinkButton1"));
				Lbtn.Attributes.Add("onclick", "return Delete_Check();");
			}
		}




		// 해당글 내용보기...
		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int);
			Cmd.Parameters["@CTechnologyDataIndex"].Value = CTechnologyDataIndex;

			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE CTD_T SET Hits = Hits + 1 WHERE CTechnologyDataIndex = @CTechnologyDataIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["CTD_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT * FROM CTD_T WHERE CTechnologyDataIndex = @CTechnologyDataIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);			

			if(reader.Read())
			{
				for(int a = 0 ; a <DropDownList1.Items.Count ; a++)
				{
					if(DropDownList1.Items[a].Text == reader["Category"].ToString())
					{
						DropDownList1.Items[a].Selected  = true;
						break;
					}
				}		
	
				Writer = reader["RegistrationPerson"].ToString();
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();
				txtTitle.Text = reader["Title"].ToString();
				txtContents.Text = reader["Contents"].ToString();
				HyperLink1.Text = reader["FileName"].ToString();
				HyperLink1.NavigateUrl = "TnDownload.aspx?CTechnologyDataIndex=" + Request.QueryString["CTechnologyDataIndex"] + "&filename=AppendFile";
				
				if(reader["FileSize"].ToString() != "0")
					Label7.Text = "(" + reader["FileSize"].ToString() + ")";
				else
					Label7.Text = "첨부된 파일없음";

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
				btnMonthlyClosingInfo.Enabled = true;
				btnOutSideOrderUnitCostInfo.Enabled = true;
				btnProcessSequenceInfo.Enabled = true;
				btnPublicUseCode.Enabled = true;
				btnSellUnitCostInfo.Enabled = true;
				btnUserInfo.Enabled = true;
				btnWCInfo.Enabled = true;
				btnWorkStandardInfo.Enabled = true;
			}
			else if(Session["ID"].ToString() == "admin" && Writer != "관리자")
			{
				btnDelete.Enabled = true;
				btnBusinessPlanInfo.Enabled = true;
				btnBuyUnitCostInfo.Enabled = true;
				btnCompanyInfo.Enabled = true;				
				btnEquipmentInfo.Enabled = true;
				btnExecutionPlanInfo.Enabled = true;
				btnItemInfo.Enabled = true;
				btnItemOrganizationInfo.Enabled = true;				
				btnMonthlyClosingInfo.Enabled = true;
				btnOutSideOrderUnitCostInfo.Enabled = true;
				btnProcessSequenceInfo.Enabled = true;
				btnPublicUseCode.Enabled = true;
				btnSellUnitCostInfo.Enabled = true;
				btnUserInfo.Enabled = true;
				btnWCInfo.Enabled = true;
				btnWorkStandardInfo.Enabled = true;
			}

			reader.Close();			
			Con.Close();
		

			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 CTechnologyDataIndex FROM CTD_T WHERE CTechnologyDataIndex < @CTechnologyDataIndex ORDER BY CTechnologyDataIndex DESC", conn);
			comm.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int).Value = Request.QueryString["CTechnologyDataIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkNextContent.Visible = false;
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 CTechnologyDataIndex FROM CTD_T WHERE CTechnologyDataIndex > @CTechnologyDataIndex ORDER BY CTechnologyDataIndex ASC", conn);
			comm.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int).Value = Request.QueryString["CTechnologyDataIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkPreContent.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			hlnkPreContent.NavigateUrl = "TnAdjustment.aspx?CTechnologyDataIndex=" + Pre.ToString();
			hlnkNextContent.NavigateUrl = "TnAdjustment.aspx?CTechnologyDataIndex=" + Next.ToString();
						
			conn.Close();
		}




		// 삭제버튼...
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
				Cmd.CommandText = "DELETE FROM CTD_T WHERE CTechnologyDataIndex = @CTechnologyDataIndex";							
				Cmd.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int);
				Cmd.Parameters["@CTechnologyDataIndex"].Value = CTechnologyDataIndex;
				Cmd.ExecuteNonQuery();
				
				Cmd.CommandText = "DELETE FROM CR_T WHERE CDTIndex = @CTechnologyDataIndex";
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
				Response.Redirect("TechnologyData.aspx");
			}
		}



		// 수정버튼....
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);
			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			try
			{
				if(File1.Value == "")
					Cmd.CommandText = "UPDATE CTD_T SET Title=@Title, Category = @Category, Contents=@Contents, RegistrationDate=@RegistrationDate WHERE CTechnologyDataIndex=@CTechnologyDataIndex";
				else
					Cmd.CommandText = "UPDATE CTD_T SET Title=@Title, Category = @Category, Contents=@Contents, AppendFile=@AppendFile, FileName=@FileName, FileSize=@FileSize, FileType=@FileType, RegistrationDate=@RegistrationDate WHERE CTechnologyDataIndex=@CTechnologyDataIndex";

				Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
				Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = txtTitle.Text;
				Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = txtContents.Text;
				Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
				Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
				Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
				Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType;
				Cmd.Parameters.Add("@CTechnologyDataIndex", SqlDbType.Int).Value = CTechnologyDataIndex;
				Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime).Value = System.DateTime.Now.ToString();
				
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
				Response.Redirect("TechnologyData.aspx");
				Con.Close();
			}
		}



		// 목록으로...
		private void lbkList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("TechnologyData.aspx");
		}

		// 거래처정보 팝업창...
		private void btnCompanyInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/CompanyInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','CompanyInfo', 'resizable=1, scrollbars=1, width=850, height=500');</script>");
		}



		// 품목정보 팝업창...
		private void btnItemInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/ItemInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','ItemInfo', 'resizable=1, scrollbars=1, width=800, height=500');</script>");
		}



		// 품목구성정보 팝업창...
		private void btnItemOrganizationInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/ItemOrganizationInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','ItemOrganizationInfo', 'resizable=1, scrollbars=1, width=800, height=500');</script>");
		}



		// 설비정보 팝업창...
		private void btnEquipmentInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/EquipmentInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','EquipmentInfo', 'resizable=1, scrollbars=1, width=550, height=500');</script>");
		}



		// 작업장정보 팝업창...
		private void btnWCInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/WCInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','WCInfo', 'resizable=1, scrollbars=1, width=710, height=300');</script>");
		}



		// 공정순서정보 팝업창...
		private void btnProcessSequenceInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/ProcessSequenceInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','ProcessSequenceInfo', 'resizable=1, scrollbars=1, width=800, height=500');</script>");
		}



		// 작업표준정보 팝업창...
		private void btnWorkStandardInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/WorkStandardInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"','WorkStandardInfo', 'resizable=1, scrollbars=1, width=800, height=500');</script>");
		}



		// 판매단가정보 팝업창...
		private void btnSellUnitCostInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/SellUnitCostInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'SellUnitCostInfo', 'resizable=1, scrollbars=1, width=810, height=500');</script>");
		}



		// 외주단가정보 팝업창...
		private void btnOutSideOrderUnitCostInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/OutSideOrderUnitCostInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'OutSideOrderUnitCostInfo', 'resizable=1, scrollbars=1, width=810, height=500');</script>");
		}



		// 구매단가정보 팝업창...
		private void btnBuyUnitCostInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/BuyUnitCostInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'BuyUnitCostInfo', 'resizable=1, scrollbars=1, width=810, height=500');</script>");
		}



		// 사용자정보 팝업창...
		private void btnUserInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/UserInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'UserInfo', 'resizable=1, scrollbars=1, width=730, height=500');</script>");
		}



		// 공용코드정보 팝업창...
		private void btnPublicUseCode_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/PublicUseCode.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'PublicUseCode', 'resizable=1, scrollbars=1, width=790, height=800');</script>");
		}



		// 사업계획정보 팝업창...
		private void btnBusinessPlanInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/BusinessPlanInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'BusinessPlanInfo', 'resizable=1, scrollbars=1, width=940, height=300');</script>");		
		}



		// 실행계획정보 팝업창...
		private void btnExecutionPlanInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/ExecutionPlanInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'ExecutionPlanInfo', 'resizable=1, scrollbars=1, width=940, height=300');</script>");		
		}



		// 월마감정보 팝업창...
		private void btnMonthlyClosingInfo_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('Popup/MonthlyClosingInfo.aspx?CTechnologyDataIndex="+CTechnologyDataIndex.ToString()+"', 'MonthlyClosingInfo', 'resizable=1, scrollbars=1, width=600, height=300');</script>");		
		}



		// DataGrid 바인딩시 레코드가 하나도 없을때...
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
					summaryCell.Text = "기준정보 참고자료로 등록된 항목이 없습니다.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}



		// 관계테이블 삭제...
		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string CRelationShipIndex = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			Cmd.Connection = Con;			
			Cmd.Transaction = trans;			
			
			try
			{				
				Cmd.CommandText = "DELETE CR_T WHERE CRelationShipIndex = @CRelationShipIndex";
				Cmd.Parameters.Add("@CRelationShipIndex", SqlDbType.Int);
				Cmd.Parameters["@CRelationShipIndex"].Value = CRelationShipIndex;
				Cmd.ExecuteNonQuery();
				trans.Commit();								
				Response.Write("<script>alert('삭제 되었습니다.'); location.href='TnAdjustment.aspx?CTechnologyDataIndex=" + CTechnologyDataIndex + "';; </script>");
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
