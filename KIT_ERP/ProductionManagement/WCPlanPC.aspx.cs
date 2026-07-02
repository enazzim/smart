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
using Infragistics.WebUI;
using Infragistics.WebUI.WebSchedule;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;


namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// WCPlanPC1에 대한 요약 설명입니다.
	/// </summary>
	public class WCPlanPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btOrderCancel;
		protected System.Web.UI.WebControls.Button btExcel;
		protected System.Web.UI.WebControls.Button btCommit;
		protected System.Web.UI.WebControls.Button btCommitCancel;
		protected System.Web.UI.WebControls.Button btStop;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.Button bt_Search;
		
		protected System.Web.UI.WebControls.Button btChangeInOut;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected int Division;
		protected DataSet ds;
		protected System.Web.UI.WebControls.Button btCancle;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Button btDelete;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup1;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryToDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList dlProgress;
		//protected System.Web.UI.WebControls.Button btCancle;
		//protected ArrayList List;

		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;


		protected DataSet WCName
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_WCName = source.WCName();
				return ds_WCName;
				
			}
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			
			if(!Page.IsPostBack)
			{
				btAdd.Attributes.Add("onClick", "return confirm('작업계획을 추가 하시겠습니까?');");
				btDelete.Attributes.Add("onClick", "return confirm('선택항목을 삭제 하시겠습니까?');");
				btStop.Attributes.Add("onClick", "return confirm('선택항목을 중단 하시겠습니까?');");
				btCancle.Attributes.Add("onClick", "return confirm('작업계획을 취소 하시겠습니까?');");
				btOrderCancel.Attributes.Add("onClick", "return confirm('작업지시를 취소 하시겠습니까?');");
				btCommit.Attributes.Add("onClick", "return confirm('선택항목을 완료 하시겠습니까?');");
				btCommitCancel.Attributes.Add("onClick", "return confirm('완료항목을 취소 하시겠습니까?');");//btChangeInOut
				btChangeInOut.Attributes.Add("onClick", "return confirm('선택항목의 작업을 변경 하시겠습니까?');");



//				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
//				conn.Open();
//
//				string str_WC = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1 order by WCName";
//				SqlCommand comm_WC = new SqlCommand(str_WC,conn);
//				SqlDataAdapter da_WC = new SqlDataAdapter(comm_WC) ;
//				DataSet ds_WC = new DataSet() ;
//				da_WC.Fill(ds_WC);
//				
//				wc_WCName.DataSource = ds_WC;
//				wc_WCName.DataTextField = ds_WC.Tables[0].Columns[0].ToString();
//				wc_WCName.DataValueField = ds_WC.Tables[0].Columns[0].ToString();
//				wc_WCName.DataBind();
//
//				conn.Close();

				//볼륨번호 찾는 함수
				this.Volnum();
				
				Division = 0;

				Load_Bind();
			}

		}


		private void Load_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			ddlItemGroup1.DataSource = ds_ItemClassification1;
			ddlItemGroup1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemGroup1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemGroup1.DataBind();
			ddlItemGroup1.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup1.Items[0].Value = "";



			


			//	*************************************************
			//	**  품목분류2 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류2인 소분류명을 가지고 옴.
			string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220'" ;
			SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
			SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
			DataSet ds_ItemClassification2 = new DataSet() ;
			da_ItemClassification2.Fill(ds_ItemClassification2);
				
			ddlItemGroup2.DataSource = ds_ItemClassification2;
			ddlItemGroup2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
			ddlItemGroup2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
			ddlItemGroup2.DataBind();
			ddlItemGroup2.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup2.Items[0].Value = "";

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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
			this.btCommit.Click += new System.EventHandler(this.btCommit_Click);
			this.btCommitCancel.Click += new System.EventHandler(this.btCommitCancel_Click);
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btChangeInOut.Click += new System.EventHandler(this.btChangeInOut_Click);
			this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			this.btOrderCancel.Click += new System.EventHandler(this.btOrderCancel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 볼륨번호 찾는 함수
		/// </summary>
		/// <returns></returns>
		private void Volnum()
		{
			///////////////////////////////////////
			// 비디오버튼을 위해
			// Volum에 볼륨번호 최고값을 넣어둔다.
			//////////////////////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();	
			string str_Vol = "Select Max(VolumNum) From WDWP_HT";
			SqlCommand comm_Vol = new SqlCommand(str_Vol,conn);
			if(!Convert.IsDBNull(comm_Vol.ExecuteScalar()))
				Volum.Value = Convert.ToString(comm_Vol.ExecuteScalar());
			else
				Volum.Value = "0";
			conn.Close();

			if(Volum.Value == "0")
			{
				btnNext.Enabled = false;
				btnPre.Enabled = false;
			}
		}

		/// <summary>
		/// 이전버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPre_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btCancle.Enabled = true;
			vol_Pre();

			VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;

			
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btCancle.Enabled = true;
			vol_Now();
			VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Volnum();
			Division = 1;
		}

		/// <summary>
		/// 다음버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNext_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btCancle.Enabled = true;
			vol_Next();
			
			VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;
		}


		/// <summary>
		/// 이전볼퓸번호 찾는 함수
		/// </summary>
		private void vol_Pre()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Max(VolumNum) as VolumNum From WDWP_HT where VolumNum < @num";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(Volum.Value);
							
			if(comm.ExecuteScalar() != Convert.DBNull)
			{
				SqlDataReader dr = comm.ExecuteReader();
				if(dr.Read())
					Volum.Value = dr["VolumNum"].ToString();
			}
			else
			{
				RegisterStartupScript("","<script>alert('맨 처음입니다!')</script>");
				btnPre.Enabled = false;
				btnNext.Enabled = true;
			}
			
			conn.Close();	
			
			
		}

		/// <summary>
		/// 현재 볼륨번호 찾는 함수
		/// </summary>
		private void vol_Now()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Max(VolumNum) as VolumNum From WDWP_HT";
							
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Volum.Value = dr["VolumNum"].ToString();
			dr.Close();
			
			if(Volum.Value == "")
			{
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!')</script>");
				Volum.Value = "0";
				btCancle.Enabled = false;
				btnPre.Enabled = false;
				btnNext.Enabled = false;
			}
			else
			{			
				btnPre.Enabled = true;
				btnNext.Enabled = true;
			}
			conn.Close();
		}

		/// <summary>
		/// 이후볼륨번호 찾는 함수
		/// </summary>
		private void vol_Next()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Min(VolumNum) as VolumNum From WDWP_HT where VolumNum > @num";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(Volum.Value);
							
			if(comm.ExecuteScalar() != Convert.DBNull)
			{
				SqlDataReader dr = comm.ExecuteReader();
				if(dr.Read())
					Volum.Value = dr["VolumNum"].ToString();
			}
			else
			{
				RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
				btnPre.Enabled = true;
				btnNext.Enabled = false;
			}
			
			conn.Close();	
			
		}
        
		/// <summary>
		/// 검색 함수
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex =1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			btCancle.Enabled = false;
			
		}


		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{
			Search search = new Search("WCPlanPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,dlProgress.SelectedItem.Text,wdcFromDate,wdcToDate,wdcDeliveryFromDate,wdcDeliveryToDate,wdcBeginDate,wdcEndDate, ddlItemGroup1.SelectedItem.Value.ToString(), ddlItemGroup2.SelectedItem.Value.ToString());
			return  search.DataSet_search();
		}



        

		private void btCancle_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			VideoButton aa = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
			UltraWebGrid1.DataSource = aa.FindVolum();
			UltraWebGrid1.DataBind();
			Cancel cancel = new Cancel(UltraWebGrid1,"WCPlanPC",Volum.Value);
			cancel.MainRowCancel();
			Volnum();
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = true;
			VideoButton bb = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
			UltraWebGrid1.DataSource = bb.FindVolum();
			UltraWebGrid1.DataBind();
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 작업지시 취소버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btOrderCancel_Click(object sender, System.EventArgs e)
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
				{
					Response.Write("<script>alert('지시가 취소 되었습니다.')</script>");
					
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();

				}
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
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

		/// <summary>
		/// DB에 지시취소로 수정한다.
		/// </summary>
		/// <param name="strArrUdValue"></param>
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

		/// <summary>
		/// 중단버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btStop_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsStop() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_Stop( index ) ;
					

						bRowRegist = true;
					}
				}
				if( bRowRegist )
				{
					Response.Write("<script>alert('중단 되었습니다.')</script>");
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();
				}
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");
			}
			else
			{
				Response.Write("<script>alert('선택 항목들 중 진행상태가 중단할수 없는 항목이 있습니다!')</script>");
			}

		}

		/// <summary>
		/// 중단할수 있는 항목이 있는지 파악하는 함수
		/// </summary>
		/// <returns>체크된 항목들 중에 진행상태가 중단되었거나 완료된경우는 중단할 수 없다. 그러므로 false를 리턴한다</returns>
		private bool SelectedRow_IsStop()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ) 
					&& (UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "중단" || UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "완료"))
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		/// <summary>
		/// DB에 진행상태를 중단으로 수정시킨다
		/// </summary>
		/// <param name="idx"></param>
		private void DB_Stop(int idx)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			try
			{
				con.Open();
				string str = "Update WDWP_HT Set ProgressCondition = '중단',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex";
				cmd.Transaction = con.BeginTransaction();
				cmd.CommandText = str;
				cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
				cmd.Parameters.Add("@Person", Session["UserName"].ToString());
				cmd.Parameters.Add("@PersonID", Session["ID"].ToString());
				cmd.Parameters.Add("@date", DateTime.Now.ToShortDateString());				
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

		/// <summary>
		/// 강제완료버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btCommit_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsEnd() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_End( index ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('완료 되었습니다.')</script>");
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();
				}
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");
			}
			else
			{
				Response.Write("<script>alert('선택 항목들 중 진행상태가 완료할수 없는 항목이 있습니다!')</script>");
			}
		}

		/// <summary>
		/// 완료할수 있는 항목이 있는지 파악하는 함수
		/// </summary>
		/// <returns>체크된 항목들 중에 진행상태가 중단되었거나 완료된경우는 완료할 수 없다. 그러므로 false를 리턴한다</returns>
		private bool SelectedRow_IsEnd()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ) 
					&& (UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "중단" || UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "완료"))
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		/// <summary>
		/// DB에 진행상태를 완료으로 수정시킨다
		/// </summary>
		/// <param name="idx"></param>
		private void DB_End(int idx)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			try
			{
				con.Open();
				
				string str = "Update WDWP_HT Set ProgressCondition = '완료', UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex";
				
				cmd.Transaction = con.BeginTransaction();
				cmd.CommandText = str;
				cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
				cmd.Parameters.Add("@Person", Session["UserName"].ToString());
				cmd.Parameters.Add("@PersonID", Session["ID"].ToString());
				cmd.Parameters.Add("@date", DateTime.Now.ToShortDateString());
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

		/// <summary>
		/// 완료취소버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btCommitCancel_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsEndCancle() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_EndCancle( index ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('완료취소 되었습니다.')</script>");
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();
				}
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");
			}
			else
			{
				Response.Write("<script>alert('선택 항목들 중 진행상태가 완료가 아닌 항목이 있어 취소할 수 없습니다!')</script>");
			}
		}


		/// <summary>
		/// 완료취소할수 있는 항목이 있는지 파악하는 함수
		/// </summary>
		/// <returns>체크된 항목들 중에 진행상태가 완료된경우이외에는 취소할 수 없다. 그러므로 false를 리턴한다</returns>
		private bool SelectedRow_IsEndCancle()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value) == true 
					&& UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "완료")
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		/// <summary>
		/// DB에 진행상태를 진행또는 지시로 변경시킨다
		/// </summary>
		/// <param name="idx"></param>
		private void DB_EndCancle(int idx)
		{
			string WorkDistinction = "자가";
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = @"Select WorkDistinction From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText=str;
			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while(dr.Read())
			{
				WorkDistinction = dr["WorkDistinction"].ToString();
			}
			con.Close();
			cmd.Parameters.Clear();

			//작업완료수량이 0인경우는 지시로 변경시키고 그외의 경우는 진행으로 변경시킨다.
			str  = @"select WorkCompletionQuantity From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			cmd.CommandText = str;
			con.Open();
			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
			decimal count  = decimal.Parse(cmd.ExecuteScalar().ToString());
			cmd.Parameters.Clear();
			con.Close();

			if(count <= 0)
			{
				if(WorkDistinction == "자가")
					str = "Update WDWP_HT Set ProgressCondition = '지시',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
				else
					str = "Update WDWP_HT Set ProgressCondition = '대기',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			}
			else
				str = "Update WDWP_HT Set ProgressCondition = '진행',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex";
			
			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.CommandText = str;
				cmd.Parameters.Add("@Person", Session["UserName"].ToString());
				cmd.Parameters.Add("@PersonID", Session["ID"].ToString());
				cmd.Parameters.Add("@date", DateTime.Now.ToShortDateString());
				cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
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

		/// <summary>
		/// 작업변경 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btChangeInOut_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsChange() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_Change( index, UltraWebGrid1.Rows[i].Cells.FromKey("WorkDistinction").Value.ToString() ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('변경 되었습니다.')</script>");
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();
				}
				else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");
			}
			else
			{
				Response.Write("<script>alert('진행상태가 대기인 항목만 변경이 가능합니다!')</script>");
			}

			
		}

		/// <summary>
		/// 삭제할수 있는 항목이 있는지 파악하는 함수
		/// </summary>
		/// <returns>체크된 항목들 중에 진행상태가 대기인 경우이외에는 삭제할 수 없다. 그러므로 false를 리턴한다</returns>
		private bool SelectedRow_IsChange()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ) 
					&& UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		/// <summary>
		/// 작업구분을 변경시킨다
		/// </summary>
		/// <param name="idx"></param>
		private void DB_Change(int idx, string distinction)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str ="";
			if(distinction == "자가")
				str  = @"Update WDWP_HT Set WorkDistinction = '외주' Where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			else
				str  = @"Update WDWP_HT Set WorkDistinction = '자가' Where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = str;
			cmd.Connection = con;
			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
			
			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
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

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btDelete_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsDelete() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_Delete( index ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('삭제 되었습니다.')</script>");
					if(Division == 0)
					{
						UltraWebGrid1.DataSource = Search();
					}
					else
					{
						VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
						UltraWebGrid1.DataSource = vol.FindVolum();
					}

					UltraWebGrid1.DataBind();
				}
                else
					Response.Write("<script>alert('선택된 품목이 없습니다.')</script>");
			}
			else
			{
				Response.Write("<script>alert('선택 항목들 중 진행상태가 완료할수 없는 항목이 있습니다!')</script>");
			}
		}


		/// <summary>
		/// 삭제할수 있는 항목이 있는지 파악하는 함수
		/// </summary>
		/// <returns>체크된 항목들 중에 진행상태가 대기인 경우이외에는 삭제할 수 없다. 그러므로 false를 리턴한다</returns>
		private bool SelectedRow_IsDelete()
		{
			bool bReturnValue = true;

			for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ) 
					&& UltraWebGrid1.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
				{
					bReturnValue = false;
					break;
				}
			}
			return bReturnValue;
		}

		/// <summary>
		/// 해당 레코드를 삭제한다
		/// </summary>
		/// <param name="idx"></param>
		private void DB_Delete(int idx)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//작업완료수량이 0인경우는 지시로 변경시키고 그외의 경우는 진행으로 변경시킨다.
			string str  = @"Delete From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex ";
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = str;
			cmd.Connection = con;
			cmd.Parameters.Add("@WCDailyWorkPlanHistoryIndex", idx);
			
			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
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

		/// <summary>
		/// Excel저장 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btExcel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				UltraWebGrid uwg = new UltraWebGrid();
				uwg = UltraWebGrid1;
				uwg.DisplayLayout.Pager.AllowPaging = false;
				uwg.Columns.FromKey("chk").Hidden = true;
				if(Division == 0)
				{
					uwg.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
					uwg.DataSource = vol.FindVolum();
				}

				uwg.DataBind();
				UltraWebGridExcelExporter1.Export(uwg);

			}
		}

		/// <summary>
		/// 작업 추가 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btAdd_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('./PopupWindows/WorkPlanAdd.aspx','WCPlanAdd','width=675, height=270px,left=200,top=300');</script>");
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			int index = int.Parse(lb_Index.Value.Substring(15));
			
			ArrayList List = new ArrayList();
			List.Add(index);
			
			Update up = new Update("WCPlanPC",List,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();

			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"WCPlanPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}

			UltraWebGrid1.DataBind();
		}
	}
}
