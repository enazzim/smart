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

using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// WorkDailyReportRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDailyReportRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Button Button5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList dl_Reason1;
		protected System.Web.UI.WebControls.DropDownList dl_Reason3;
		protected System.Web.UI.WebControls.DropDownList dl_InspectionDecision;
		protected System.Web.UI.WebControls.TextBox tb_ItemNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemDrawNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemName;
		protected System.Web.UI.WebControls.TextBox tb_ProcessName;
		protected System.Web.UI.WebControls.TextBox tb_ProcessSequenceNum;
		protected System.Web.UI.WebControls.TextBox tb_WorkPlanQuantity;
		protected System.Web.UI.WebControls.TextBox tb_RemainQuantity;
		protected System.Web.UI.WebControls.TextBox tb_SuitabilityQuantity;
		protected System.Web.UI.WebControls.TextBox tb_UnSuittabilityQuantity;
		protected System.Web.UI.WebControls.DropDownList dl_UnSuitabilityStatus;
		protected System.Web.UI.WebControls.DropDownList dl_UnSuitabilityCause;
		protected System.Web.UI.WebControls.DropDownList dl_Reason2;
		protected System.Web.UI.WebControls.TextBox tb_NonWorkTime1;
		protected System.Web.UI.WebControls.TextBox tb_NonWorkTime2;
		protected System.Web.UI.WebControls.TextBox tb_NonWorkTime3;
		protected System.Web.UI.WebControls.TextBox tb_UnSuitabilityCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ProductDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ParentItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ParentDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ParentName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ProcessCode;
		protected System.Web.UI.WebControls.TextBox tb_ProductName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ProductNum;
		protected System.Web.UI.WebControls.TextBox tb_UnSuitabilityDetailMeaning;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_ProductionPlanHistoryIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_WorkPlanHistoryIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_WCDailyWorkPlanHistoryIndex;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox tb_WorkCompletionQuantity1;
		protected System.Web.UI.WebControls.TextBox tb_WorkCompletionQuantity;
		protected System.Web.UI.WebControls.Label Label16;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginTime;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndTime;
		protected System.Web.UI.WebControls.Label Label6;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Button btLot;
		protected System.Web.UI.WebControls.TextBox Textbox5;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLotNum;
		protected System.Web.UI.WebControls.TextBox Textbox6;
		protected System.Web.UI.WebControls.DropDownList dlWCName;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;


		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				//Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 작업일보 등록';</script>");

				tb_WorkCompletionQuantity1.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량을 계산하는 자바스크립트 함수 매핑
				tb_WorkCompletionQuantity1.Attributes.Add("OnKeyUp", "return Process()");
				// focus를 얻으면 자동으로 select()
				tb_WorkCompletionQuantity1.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_WorkCompletionQuantity1.Attributes.Add("OnBlur", "OnBlur_Float(this);");

//				tb_UnSuittabilityQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
//				// onkeyup 이벤트가 발생하면 수량을 계산하는 자바스크립트 함수 매핑
//				tb_UnSuittabilityQuantity.Attributes.Add("OnKeyUp", "return Suitability()");
//				// focus를 얻으면 자동으로 select()
//				tb_UnSuittabilityQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
//				// focus를 잃으면 다시 입력란을 체크
//				tb_UnSuittabilityQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				tb_SuitabilityQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량을 계산하는 자바스크립트 함수 매핑
				tb_SuitabilityQuantity.Attributes.Add("OnKeyUp", "return Suitability()");
				// focus를 얻으면 자동으로 select()
				tb_SuitabilityQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_SuitabilityQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				TextBox1.Attributes.Add("OnKeyDown", "OnlyNum(this);");
				TextBox1.Attributes.Add("OnKeyUp", "return Process1()");
				TextBox1.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
		
				TextBox2.Attributes.Add("OnKeyDown", "OnlyNum(this);");
				TextBox2.Attributes.Add("OnKeyUp", "return Process2()");
				TextBox2.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				TextBox3.Attributes.Add("OnKeyDown", "OnlyNum(this);");
				TextBox3.Attributes.Add("OnKeyUp", "return Process3()");
				TextBox3.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				TextBox4.Attributes.Add("OnKeyDown", "OnlyNum(this);");
				TextBox4.Attributes.Add("OnKeyUp", "return Process4()");
				TextBox4.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				wdcBeginTime.NullDateLabel = DateTime.Now.ToShortDateString();
				wdcEndTime.NullDateLabel = DateTime.Now.ToShortDateString();

				//세화에서 요구한 일자11/29일
				//wdcBeginTime.NullDateLabel = DateTime.Today.AddDays(-1).ToShortDateString();
				//wdcEndTime.NullDateLabel = DateTime.Now.ToShortDateString();
				

				page_Load();
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
			this.Button5.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.UltraWebGrid2.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid2_PageIndexChanged);
			this.btLot.Click += new System.EventHandler(this.btLot_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.Button2.Click += new System.EventHandler(this.Button5_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

//			string str = "Select ItemName,ItemNum,ItemDrawNum,ItemInfoIndex From II_MT where RecodingState = 1 and (PropertyClassification = '제품' or PropertyClassification = '반제품') order by ItemName";
//			SqlCommand comm = new SqlCommand(str,conn);
//			SqlDataAdapter da = new SqlDataAdapter(comm) ;
//			DataSet ds = new DataSet() ;
//			da.Fill(ds);
//
//			wc_ItemName.DataSource = ds;
//			wc_ItemName.DataTextField = ds.Tables[0].Columns[0].ToString();
//			wc_ItemName.DataValueField = ds.Tables[0].Columns[0].ToString();
//			wc_ItemName.DataBind();


			string str1 = "Select WCName, WCInfoIndex From WCI_MT where RecodingState = 1 order by WCName";
			SqlCommand comm1 = new SqlCommand(str1,conn);
			SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
			DataSet ds1 = new DataSet() ;
			da1.Fill(ds1);

			dlWCName.DataSource = ds1;
			dlWCName.DataTextField = ds1.Tables[0].Columns[0].ToString();
			dlWCName.DataValueField = ds1.Tables[0].Columns[0].ToString();
			dlWCName.DataBind();
			dlWCName.Items.Insert(0, "-선택-") ;
			dlWCName.Items[0].Value = "";


			//	*************************************************
			//	**  부적합현상 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 부적합현상인 소분류명을 가지고 옴.
			string str_Status = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1000'" ;
			SqlCommand comm_Status = new SqlCommand(str_Status,conn);
			SqlDataAdapter da_Status = new SqlDataAdapter(comm_Status) ;
			DataSet ds_Status = new DataSet() ;
			da_Status.Fill(ds_Status);
				
			dl_UnSuitabilityStatus.DataSource = ds_Status;
			dl_UnSuitabilityStatus.DataTextField = ds_Status.Tables[0].Columns[0].ToString();
			dl_UnSuitabilityStatus.DataValueField = ds_Status.Tables[0].Columns[1].ToString();
			dl_UnSuitabilityStatus.DataBind();
			dl_UnSuitabilityStatus.Items.Insert(0, "-선택-") ;
			dl_UnSuitabilityStatus.Items[0].Value = "";

			//	*************************************************
			//	**  부적합원인 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 부적합현상인 소분류명을 가지고 옴.
			string str_Cause = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1010'" ;
			SqlCommand comm_Cause = new SqlCommand(str_Cause,conn);
			SqlDataAdapter da_Cause = new SqlDataAdapter(comm_Cause) ;
			DataSet ds_Cause = new DataSet() ;
			da_Cause.Fill(ds_Cause);
				
			dl_UnSuitabilityCause.DataSource = ds_Cause;
			dl_UnSuitabilityCause.DataTextField = ds_Cause.Tables[0].Columns[0].ToString();
			dl_UnSuitabilityCause.DataValueField = ds_Cause.Tables[0].Columns[1].ToString();
			dl_UnSuitabilityCause.DataBind();
			dl_UnSuitabilityCause.Items.Insert(0, "-선택-") ;
			dl_UnSuitabilityCause.Items[0].Value = "";


			//	*************************************************
			//	**  검사판정 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 검사판정인 소분류명을 가지고 옴.
			string str_Decision = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1310'" ;
			SqlCommand comm_Decision = new SqlCommand(str_Decision,conn);
			SqlDataAdapter da_Decision = new SqlDataAdapter(comm_Decision) ;
			DataSet ds_Decision = new DataSet() ;
			da_Decision.Fill(ds_Decision);
				
			dl_InspectionDecision.DataSource = ds_Decision;
			dl_InspectionDecision.DataTextField = ds_Decision.Tables[0].Columns[0].ToString();
			dl_InspectionDecision.DataValueField = ds_Decision.Tables[0].Columns[1].ToString();
			dl_InspectionDecision.DataBind();
			dl_InspectionDecision.Items.Insert(0, "-선택-") ;
			dl_InspectionDecision.Items[0].Value = "";



			//	*************************************************
			//	**  비작업사유 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 비작업사유인 소분류명을 가지고 옴.
			string str_Reason = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1210'" ;
			SqlCommand comm_Reason = new SqlCommand(str_Reason,conn);
			SqlDataAdapter da_Reason = new SqlDataAdapter(comm_Reason) ;
			DataSet ds_Reason = new DataSet() ;
			da_Reason.Fill(ds_Reason);
				
			dl_Reason1.DataSource = ds_Reason;
			dl_Reason1.DataTextField = ds_Reason.Tables[0].Columns[0].ToString();
			dl_Reason1.DataValueField = ds_Reason.Tables[0].Columns[1].ToString();
			dl_Reason1.DataBind();
			dl_Reason1.Items.Insert(0, "-선택-") ;
			dl_Reason1.Items[0].Value = "";

			dl_Reason2.DataSource = ds_Reason;
			dl_Reason2.DataTextField = ds_Reason.Tables[0].Columns[0].ToString();
			dl_Reason2.DataValueField = ds_Reason.Tables[0].Columns[1].ToString();
			dl_Reason2.DataBind();
			dl_Reason2.Items.Insert(0, "-선택-") ;
			dl_Reason2.Items[0].Value = "";

			dl_Reason3.DataSource = ds_Reason;
			dl_Reason3.DataTextField = ds_Reason.Tables[0].Columns[0].ToString();
			dl_Reason3.DataValueField = ds_Reason.Tables[0].Columns[1].ToString();
			dl_Reason3.DataBind();
			dl_Reason3.Items.Insert(0, "-선택-") ;
			dl_Reason3.Items[0].Value = "";

			conn.Close();
		}
		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button2_Click(object sender, System.EventArgs e)
		{
			//wc_ItemName.DataValue="";
			ItemSearchControl1.ClearTextBox();
			dlWCName.SelectedIndex =0;
			wdc_FromDate.Value="";
			wdc_ToDate.Value = "";
		}

		/// <summary>
		/// 검색버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid2.DataSource = Search();
			UltraWebGrid2.DataBind();
		}

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button5_Click(object sender, System.EventArgs e)
		{
			if(tb_WCDailyWorkPlanHistoryIndex.Value.Trim() == "")
				RegisterStartupScript("","<script>alert('항목을 선택하세요!');</script>");
			else if(tb_WorkCompletionQuantity1.Text=="0" || tb_WorkCompletionQuantity1.Text.Trim()=="")
				RegisterStartupScript("","<script>alert('완료수량을 입력하세요!');</script>");
			//else if(tb_SuitabilityQuantity.Text=="0" || tb_SuitabilityQuantity.Text.Trim()=="")
			else if(tb_SuitabilityQuantity.Text.Trim()=="")
				RegisterStartupScript("","<script>alert('적합수량을 입력하세요!');</script>");
			else if(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "")
				RegisterStartupScript("","<script>alert('작업시간을 정확히 입력하세요!');</script>");
			else
			{
				//Textbox5.Text = hdLotNum.Value;
				ArrayList arr = new ArrayList();

				arr.Add(tb_ItemNum.Text);													//0
				arr.Add(tb_ItemDrawNum.Text);												//1
				arr.Add(tb_ItemName.Text);													//2
				arr.Add(int.Parse(tb_ProcessSequenceNum.Text));								//3
				arr.Add(tb_ProcessCode.Value);												//4
				arr.Add(tb_ProcessName.Text);												//5
				if(tb_ProductNum.Value == "null")
				{
					arr.Add("");															//6
					arr.Add("");															//7
					arr.Add("");															//8
				}
				else
				{
					arr.Add(tb_ProductNum.Value);											//6
					arr.Add(tb_ProductDrawNum.Value);										//7
					arr.Add(tb_ProductName.Text);											//8					
				}

				if(tb_ParentItemNum.Value == "null")
				{
					arr.Add("");															//9
					arr.Add("");;															//10
					arr.Add("");															//11
				}
				else
				{
					arr.Add(tb_ParentItemNum.Value);										//9
					arr.Add(tb_ParentDrawNum.Value);										//10
					arr.Add(tb_ParentName.Value);											//11
				}

				arr.Add(decimal.Parse(tb_WorkPlanQuantity.Text));		// 작업계획수량		12
				arr.Add(decimal.Parse(tb_WorkCompletionQuantity.Text)); // 이전 작업수량	13
				arr.Add(decimal.Parse(tb_WorkCompletionQuantity1.Text));// 금번 작업수량	14
				arr.Add(decimal.Parse(tb_RemainQuantity.Text));			// 잔량				15
				arr.Add(decimal.Parse(tb_SuitabilityQuantity.Text));	// 합격수량			16
				if(tb_UnSuittabilityQuantity.Text.Trim() == "")
					arr.Add("0");										// 부적합수량		17
				else
					arr.Add(decimal.Parse(tb_UnSuittabilityQuantity.Text));	// 부적합수량		17
				arr.Add(decimal.Parse(tb_UnSuitabilityCost.Text));		// 부적합금액		18
				arr.Add(wdcBeginTime.Text + " " + TextBox1.Text + ":" + TextBox2.Text);		//19
				arr.Add(wdcEndTime.Text + " " + TextBox3.Text + ":" + TextBox4.Text);		//20
				arr.Add(decimal.Parse(tb_NonWorkTime1.Text));								//21
				if(dl_Reason1.SelectedItem.Value == "")
				{
					arr.Add(dl_Reason1.SelectedItem.Value);									//22
					arr.Add(dl_Reason1.SelectedItem.Value);								//23
				}
				else
				{
					arr.Add(dl_Reason1.SelectedItem.Value);
					arr.Add(dl_Reason1.SelectedItem.Text);
				}
				arr.Add(decimal.Parse(tb_NonWorkTime2.Text));								//24
				if(dl_Reason2.SelectedItem.Value == "")
				{
					arr.Add(dl_Reason2.SelectedItem.Value);								//25
					arr.Add(dl_Reason2.SelectedItem.Value);								//26
				}
				else
				{
					arr.Add(dl_Reason2.SelectedItem.Value);
					arr.Add(dl_Reason2.SelectedItem.Text);
				}
				arr.Add(decimal.Parse(tb_NonWorkTime3.Text));								//27
				if(dl_Reason3.SelectedItem.Value == "")
				{
					arr.Add(dl_Reason3.SelectedItem.Value);								//28
					arr.Add(dl_Reason3.SelectedItem.Value);								//29
				}
				else
				{
					arr.Add(dl_Reason3.SelectedItem.Value);
					arr.Add(dl_Reason3.SelectedItem.Text);
				}
				if(dl_UnSuitabilityStatus.SelectedItem.Value == "")
				{
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Value);					//30
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Value);					//31
				}
				else
				{
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Value);
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Text);
				}
				
				arr.Add(tb_UnSuitabilityDetailMeaning.Text);							//32
				if(dl_UnSuitabilityCause.SelectedItem.Value == "")
				{
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Value);					//33
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Value);					//34
				}
				else
				{
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Value);
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Text);
				}
				if(tb_ProductionPlanHistoryIndex.Value.Trim() == "null")
					arr.Add("");															//35
				else
					arr.Add(int.Parse(tb_ProductionPlanHistoryIndex.Value));					//35
				arr.Add(0);																	//36
				arr.Add(int.Parse(tb_WCDailyWorkPlanHistoryIndex.Value));					//37

				if(dl_InspectionDecision.SelectedItem.Value == "")
				{
					arr.Add(dl_InspectionDecision.SelectedItem.Value);					//38
					arr.Add(dl_InspectionDecision.SelectedItem.Value);					//39
				}
				else
				{
					arr.Add(dl_InspectionDecision.SelectedItem.Value);
					arr.Add(dl_InspectionDecision.SelectedItem.Text);

				}
				arr.Add(int.Parse(tb_WCDailyWorkPlanHistoryIndex.Value));					//40
				arr.Add(Textbox5.Text);														//41 Lot번호
				arr.Add(Textbox6.Text);														//42 Lot번호
				
				Session["arr"] = arr;
				//작업장명과 공구치구 찾는 함수
				WCName_Find(tb_ItemNum.Text,int.Parse(tb_ProcessSequenceNum.Text));

				RegisterStartupScript("","<script>window.open('./PopupWindows/UseTool.aspx','UseTool','width=650,height=470,left=200,top=300, center=yes, resizable= yes');</script>");				
				//Page.RegisterClientScriptBlock("SEND","<script>window.showModalDialog('./PopupWindows/UseTool.aspx', 'UseTool', 'dialogHeight: 140px; dialogWidth: 760px; left: 300px; top: 400px, center: yes; resizable:yes; status: no;');</script>");				
				UltraWebGrid2.DataSource = Search();
				UltraWebGrid2.DataBind();
			}
		}

		private void WCName_Find(string ItemNum, int ProcessSequenceNum)
		{
			ArrayList list = new ArrayList();

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				string str = @"Select WCName,ToolName1,JigName1,ToolName2,JigName2,ToolName3,JigName3, MainWorkerID From WSI_MT 
						where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @sequence and PriorityOrder = (Select Min(PriorityOrder) From WSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @sequence)";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum;
				comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = ProcessSequenceNum;
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count != 0 )
				{
					list.Add(ds.Tables[0].Rows[0]["WCName"].ToString());
					
					if(ds.Tables[0].Rows[0]["ToolName1"].ToString() == null || ds.Tables[0].Rows[0]["ToolName1"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["ToolName1"].ToString());
					
					if(ds.Tables[0].Rows[0]["JigName1"].ToString() == null || ds.Tables[0].Rows[0]["JigName1"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["JigName1"].ToString());
					
					if(ds.Tables[0].Rows[0]["ToolName2"].ToString() == null || ds.Tables[0].Rows[0]["ToolName2"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["ToolName2"].ToString());

					if(ds.Tables[0].Rows[0]["JigName2"].ToString() == null || ds.Tables[0].Rows[0]["JigName2"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["JigName2"].ToString());
					
					if(ds.Tables[0].Rows[0]["ToolName3"].ToString() == null || ds.Tables[0].Rows[0]["ToolName3"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["ToolName3"].ToString());

					if(ds.Tables[0].Rows[0]["JigName3"].ToString() == null || ds.Tables[0].Rows[0]["JigName3"].ToString() == "")
						list.Add("");
					else
						list.Add(ds.Tables[0].Rows[0]["JigName3"].ToString());

					list.Add(ds.Tables[0].Rows[0]["MainWorkerID"].ToString());
				}
				else
				{
					for(int a = 0; a<8;a++)
					list.Add("");
				}

				Session["list"] = list;

				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>"+ee.Message+"</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}

		private void UltraWebGrid2_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid2.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			UltraWebGrid2.DataSource =  Search();
			UltraWebGrid2.DataBind();
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			tb_ItemNum.Text = "";
			tb_ItemDrawNum.Text = "";
			tb_ItemName.Text = "";
			tb_ProductName.Text = "";
			tb_ProductDrawNum.Value = "";
			tb_ProductNum.Value = "";
			tb_ParentItemNum.Value = "";
			tb_ParentDrawNum.Value = "";
			tb_ParentName.Value = "";
			tb_ProcessName.Text = "";
			tb_ProcessCode.Value = "";
			tb_ProcessSequenceNum.Text = "";

			tb_WorkPlanQuantity.Text = "0";
			tb_WorkCompletionQuantity.Text = "0";
			tb_WorkCompletionQuantity1.Text = "0";
			tb_RemainQuantity.Text = "0";
			tb_SuitabilityQuantity.Text = "0";
			//tb_SuitabilityQuantity.Text = "";
			tb_UnSuittabilityQuantity.Text = "0";
			TextBox1.Text = "";
			TextBox2.Text = "";
			TextBox3.Text = "";
			TextBox4.Text = "";
			Textbox5.Text = "";
			Textbox6.Text = "";

			dl_Reason1.SelectedIndex = 0;
			dl_Reason2.SelectedIndex = 0;
			dl_Reason3.SelectedIndex = 0;
			tb_NonWorkTime1.Text = "0";
			tb_NonWorkTime2.Text = "0";
			tb_NonWorkTime3.Text = "0";

			dl_UnSuitabilityStatus.SelectedIndex = 0;
			dl_UnSuitabilityCause.SelectedIndex = 0;
			tb_UnSuitabilityCost.Text = "0";
			tb_UnSuitabilityDetailMeaning.Text = "";
			dl_InspectionDecision.SelectedIndex = 0;
			tb_ProductionPlanHistoryIndex.Value = "";
			tb_WorkPlanHistoryIndex.Value = "";
			tb_WCDailyWorkPlanHistoryIndex.Value = "";

			UltraWebGrid2.DataSource = Search();
			UltraWebGrid2.DataBind();
		}

		private void btLot_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('../LotNumberManagement.aspx','LotNumberManagement','width=760,height=400,left=200,top=300, center=yes, resizable= yes');</script>");				
		}


		private DataSet Search()
		{

			bool WorkPlan3 = false;
			SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn1.Open();

			string strSQL = @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, 
								  OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed,OutOrderUsed,
								  BundlingUsed,WorkPlan1, WorkPlan2, WorkPlan3,InWorkConsidering,MinusStockAddUsed FROM SCS_T";

			SqlCommand comm = new SqlCommand(strSQL, conn1);

			SqlDataReader reader = comm.ExecuteReader();

			while(reader.Read())
			{
				if(bool.Parse(reader["WorkPlan3"].ToString()))
					WorkPlan3 = true;

			}
			reader.Close();
			conn1.Close();			

			if(WorkPlan3)
			{
				Search search = new Search("WorkDailyReportRegistration1",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,dlWCName,wdc_FromDate,wdc_ToDate);
				return search.DataSet_search();
			}
			else
			{
				Search search = new Search("WorkDailyReportRegistration",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,dlWCName,wdc_FromDate,wdc_ToDate);
				return search.DataSet_search();
			}
		}
	}
}
