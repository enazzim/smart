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


namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// WorkOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class WorkOrderPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcStartDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Commodity = true;

			if ( !IsPostBack )
			{
				Button2.Attributes.Add("onclick", "return RegistCheck('지시취소')");
				Button3.Attributes.Add("onclick", "return RegistCheck('중단')");
				Load_WebCombo_Bind();
			}

			// true 가 반환되면 등록가능
			if ( MonthClosing() )
			{
				Response.Write("<script>var MonthCloseing = \"0\"; </script>");	// 전역변수에 "0" 을 할당
			}
			else
			{
				// 월마감 되었음. - 등록 불가
				Response.Write("<script>var MonthCloseing = \"1\"; </script>");	// 전역변수에 "1" 을 할당
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region 월마감 여부를 확인하는 메소드
		/// <summary>
		/// 월마감 여부를 확인하는 메소드
		/// </summary>
		/// <returns></returns>
		public bool MonthClosing()
		{
			int intYear = 0;
			int intMonth = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select [ClosingYear], [ClosingMonth] From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "생산";

			try
			{
				conn.Open();
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					intYear = int.Parse(dr[0].ToString());
					intMonth = int.Parse(dr[1].ToString());
				}
			} 
			catch ( Exception e)
			{
				Response.Write("<script>alert(\"" + e.Message + "\")</script>"); 
			} 
			finally
			{
				conn.Close();
			}

			if(intYear < int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
			{
				return true;
			}
			else if(intYear == int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim())) 
			{
				if(intMonth < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}	
					
		}
		#endregion

		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			adap.SelectCommand.Parameters.Add("@Wcname", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);
			wcWCName.DataSource = ds.Tables[0].DefaultView;
			wcWCName.DataTextField = "작업장 명";
			wcWCName.DataValueField = "WCInfoIndex";

			ddlItemClassification1.DataSource = ds.Tables[1].DefaultView;
			ddlItemClassification1.DataTextField = "SmallClassificationName";
			ddlItemClassification1.DataValueField = "SmallClassificationCode";

			ddlItemClassification2.DataSource = ds.Tables[2].DefaultView;
			ddlItemClassification2.DataTextField = "SmallClassificationName";
			ddlItemClassification2.DataValueField = "SmallClassificationCode";

			ddlItemClassification3.DataSource = ds.Tables[3].DefaultView;
			ddlItemClassification3.DataTextField = "SmallClassificationName";
			ddlItemClassification3.DataValueField = "SmallClassificationCode";

			ddlItemClassification4.DataSource = ds.Tables[4].DefaultView;
			ddlItemClassification4.DataTextField = "SmallClassificationName";
			ddlItemClassification4.DataValueField = "SmallClassificationCode";

			this.DataBind();
			ListItem item = new ListItem("전체", "");

			wcWCName.Columns[0].Hidden = true;

			ddlItemClassification1.Items.Insert(0, item);
			ddlItemClassification2.Items.Insert(0, item);
			ddlItemClassification3.Items.Insert(0, item);
			ddlItemClassification4.Items.Insert(0, item);

			wcWCName.Columns[1].Width = 150;
			wcWCName.Columns[2].Width = 130;
			wcWCName.Columns[3].Width = 80;
		}

		// 지시취소 버튼
		private void Button2_Click(object sender, System.EventArgs e)
		{			
			if ( this.SelectedRow_IsStandBy() )
			{
				bool bRowRegist = false;

				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						string[] strArrUdValue = new string[2];

						strArrUdValue[0] = UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString();		// 원장인덱스
						strArrUdValue[1] = Session["ID"].ToString();																	// 등록자 ID

						DB_RegistOrderCancel( strArrUdValue ) ;

						bRowRegist = true;
					}
				}
				
				if ( bRowRegist )
					Response.Write("<script>alert('완료 되었습니다.')</script>");
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");

				btnSearch_Click(sender, e);
			}
			else
			{
				Response.Write("<script>alert('현재 진행상태가 \"지시\"인 품목만 지시취소 할 수 있습니다.')</script>");
			}
		}

		/// <summary>
		/// 진행상태가 "지시"인 행만 선택하였는지 판별
		/// </summary>
		/// <returns>선택된 항목들이 모두 "지시"인 경우 true를 리턴</returns>
		private bool SelectedRow_IsStandBy()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ) 
					&& UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "지시" )
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		// DB에 지시취소로 수정한다.
		private void DB_RegistOrderCancel(string[] strArrUdValue)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("WorkOrderPC_OrderCancel", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", SqlDbType.Int).Value = strArrUdValue[0];
			cmd.Parameters.Add("@RegistID", SqlDbType.VarChar).Value = strArrUdValue[1];

			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				cmd.Transaction.Rollback();
			}
			finally
			{
				con.Close();
			}
		}

		// 검색버튼 클릭
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strWcName = "";
			string strItemClassification1 = "";
			string strItemClassification2 = "";
			string strItemClassification3 = "";
			string strItemClassification4 = "";
			string strDIV = "" ;
			string strItemNum = "";
			string strItemName = "";
			string strItemDrawNum = "";
			string strStartDate = "";

			strWcName = wcWCName.DisplayValue;
			strItemClassification1 = ddlItemClassification1.SelectedValue;
			strItemClassification2 = ddlItemClassification2.SelectedValue;
			strItemClassification3 = ddlItemClassification3.SelectedValue;
			strItemClassification4 = ddlItemClassification4.SelectedValue;
			strDIV = ddlState.SelectedValue;
			strItemNum = ItemSearchControl1.ItemNum.Trim();
			strItemName = ItemSearchControl1.ItemName.Trim();
			strItemDrawNum = ItemSearchControl1.ItemDrawNum.Trim();
			strStartDate = wcStartDate.Text;

			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("WorkOrderPC_Search", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@ItemClassification1", SqlDbType.VarChar).Value = strItemClassification1;
			cmd.Parameters.Add("@ItemClassification2", SqlDbType.VarChar).Value = strItemClassification2;
			cmd.Parameters.Add("@ItemClassification3", SqlDbType.VarChar).Value = strItemClassification3;
			cmd.Parameters.Add("@ItemClassification4", SqlDbType.VarChar).Value = strItemClassification4;

			cmd.Parameters.Add("@workdate", SqlDbType.VarChar).Value = strStartDate;

			cmd.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = strItemNum;
			cmd.Parameters.Add("@ItemDrawNum", SqlDbType.VarChar).Value = strItemDrawNum;
			cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strItemName;

			cmd.Parameters.Add("@ProgressCondition", SqlDbType.VarChar).Value = strDIV;
			cmd.Parameters.Add("@WCName", SqlDbType.VarChar).Value = strWcName;
			SqlDataReader dr = null;

			try
			{
				con.Open();
				dr = cmd.ExecuteReader();
				UltraWebGrid1.DataSource = dr;
				UltraWebGrid1.DataBind();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
			}
			finally
			{
				dr.Close();
				con.Close();			
			}
		}

		// 인덱스 체인지
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.btnSearch_Click(sender, e);
		}

		// Excel 버튼
		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.Columns.FromKey("chk").Hidden = true;
			btnSearch_Click(sender, e);
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		// 중단버튼 클릭
		private void Button3_Click(object sender, System.EventArgs e)
		{
			bool bRowRegist = false;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
				{
					string[] strArrUdValue = new string[2];

					strArrUdValue[0] = UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString();		// 원장인덱스
					strArrUdValue[1] = Session["ID"].ToString();																	// 등록자 ID

					DB_RegistStop( strArrUdValue ) ;

					bRowRegist = true;
				}
			}
				
			if ( bRowRegist )
				Response.Write("<script>alert('완료 되었습니다.')</script>");
			else
				Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");

			btnSearch_Click(sender, e);
		}

		// DB에 진행상태를 중단으로 
		private void DB_RegistStop(string[] strArrUdValue)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("WorkOrderPC_Stop", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", SqlDbType.Int).Value = strArrUdValue[0];
			cmd.Parameters.Add("@RegistID", SqlDbType.VarChar).Value = strArrUdValue[1];

			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.ExecuteNonQuery();
				cmd.Transaction.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				cmd.Transaction.Rollback();
			}
			finally
			{
				con.Close();
			}
		}


		
	}
}
