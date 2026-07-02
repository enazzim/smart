using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Data.OleDb;
using Microsoft.Win32;


namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// BuyingUnitCostInfo에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingUnitCodeInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lb_ItemNum;
		protected System.Web.UI.WebControls.Label lb_ItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ItemName;
		protected System.Web.UI.WebControls.Label lb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Label lb_Company;
		protected System.Web.UI.WebControls.Label lb_BeginDate;
		protected System.Web.UI.WebControls.Label lb_EndDate;
		protected System.Web.UI.WebControls.Label lb_StandardUnitCost;
		protected System.Web.UI.WebControls.Label lb_DiscountUnitCost;
		protected System.Web.UI.WebControls.Label lb_OrderRate;
		protected System.Web.UI.WebControls.TextBox tb_OrderRate;
		protected System.Web.UI.WebControls.TextBox tb_DiscountUnitCost;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Button bt_BuyingUnitCost;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Button tbTotalUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		private DataSet dataset = new DataSet();
		protected System.Web.UI.WebControls.TextBox tbUpdateReason;
		protected System.Web.UI.WebControls.Button btHistory;
		private string m_UserName = "";
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			ItemSearchControl1.SubMaterials = true;
			ItemSearchControl1.Expendable = true;// 소모품
			ItemSearchControl1.IsDoPost = true;
			CSC1.UnitCostDistinction="구매거래처";

			if(!Page.IsPostBack)
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 구매단가정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_StandardUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				tb_StandardUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// focus를 얻으면 자동으로 select()
				tb_StandardUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_StandardUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
                			
				tb_DiscountUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_DiscountUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				// focus를 얻으면 자동으로 select()
				tb_DiscountUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_DiscountUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_OrderRate.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				
				// focus를 얻으면 자동으로 select()
				tb_OrderRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_OrderRate.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				
				bt_Clear.Attributes.Add("onClick", "ResettxtBox()");				
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.bt_BuyingUnitCost.Click += new System.EventHandler(this.bt_BuyingUnitCost_Click);
			this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.tbTotalUpdate.Click += new System.EventHandler(this.tbTotalUpdate_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 판매단가보기 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_BuyingUnitCost_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="")
			{
				RegisterStartupScript("","<script>alert('품목을 선택해주세요');</script>");
			}
			else
			{
				tb_OrderRate.Text = "0";
				tb_StandardUnitCost.Text = "";
				tb_DiscountUnitCost.Text = "0";
				wdcBeginDate.Value = "";
				wdcEndDate.Value = "";
				CSC1.BusinessRegistrationNum="";
				CSC1.Company= "";
				lb_Index.Value = "0";

				this.DataView();
			}
		}

		/// <summary>
		/// 품목정보보기 함수
		/// </summary>
		private void DataView()
		{
			View view = new View(ItemSearchControl1.ItemNum.ToString(),"BuyingUnitCodeInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			
		}


		


        /// <summary>
        /// 초기화버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{

			tb_PropertyClassification.Text = "";
			tb_Unit.Text = "";
			tb_Standard.Text = "";

			tb_OrderRate.Text = "";
			tb_StandardUnitCost.Text = "";
			tb_DiscountUnitCost.Text = "0";
			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";

			lb_Index.Value = "0";
			lb_ItemIndex.Text = "";
			
			CSC1.BusinessRegistrationNum="";
			CSC1.Company= "";
			tbUpdateReason.Text = "";


			View view = new View("","BuyingUnitCodeInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();


		}

		

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="" || CSC1.BusinessRegistrationNum == "" || CSC1.Company == "" || tb_OrderRate.Text =="" || tb_StandardUnitCost.Text == "" || wdcBeginDate.Text == " ")
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해주세요!');</script>");
			}
			else if(!Company(CSC1.BusinessRegistrationNum,CSC1.Company))
			{
				RegisterStartupScript("","<script>alert('거래처가 정확하지 않습니다!');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "BuyingUnitCodeInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.BuyingUnitCodeInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add("구매단가");
					InputList.Add(CSC1.BusinessRegistrationNum);//사업자등록번호
					if(tb_PropertyClassification.Text.Trim() == "원자재")
					{
						InputList.Add("14000000");
						InputList.Add("14000000");
					}
					else
					{
						InputList.Add("14009999");
						InputList.Add("14009999");
					}
					InputList.Add(tb_OrderRate.Text);
					InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
					//InputList.Add(hdStandardUnitCost.Value);
					if(tb_DiscountUnitCost.Text.Trim() == "")
						InputList.Add("0");
					else
						InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
						//InputList.Add(hdDiscountUnitCost.Value);
					if(wdcBeginDate.Text.Trim() =="")
						InputList.Add(DateTime.Now.ToShortDateString());
					else
						InputList.Add(wdcBeginDate.Text);
					if(wdcEndDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdcEndDate.Text);
					Session["Reason"] = "신규등록";

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
				}
				this.DataView();
			}


		}

		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="" || CSC1.BusinessRegistrationNum == "" || CSC1.Company == "" || tb_OrderRate.Text =="" || tb_StandardUnitCost.Text == "" || wdcBeginDate.Text == " ")
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해주세요!');</script>");
			}
			else if(!Company(CSC1.BusinessRegistrationNum,CSC1.Company))
			{
				RegisterStartupScript("","<script>alert('거래처가 정확하지 않습니다!');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "BuyingUnitCodeInfo";
					string m_RequireAction = "Update";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.BuyingUnitCodeInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;
			
			
					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add("구매단가");
					InputList.Add(CSC1.BusinessRegistrationNum);;//사업자등록번호
					if(tb_PropertyClassification.Text.Trim() == "원자재")
					{
						InputList.Add("14000000");
						InputList.Add("14000000");
					}
					else
					{
						InputList.Add("14009999");
						InputList.Add("14009999");
					}
					InputList.Add(tb_OrderRate.Text);
					InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
					//InputList.Add(hdStandardUnitCost.Value);
					if(tb_DiscountUnitCost.Text.Trim() == "")
						InputList.Add("0");
					else
						InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
					//InputList.Add(hdDiscountUnitCost.Value);
					if(wdcBeginDate.Text.Trim() =="")
						InputList.Add(DateTime.Now.ToShortDateString());
					else
						InputList.Add(wdcBeginDate.Text);
					if(wdcEndDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdcEndDate.Text);
					InputList.Add(lb_Index.Value);
					Session["Reason"] = tbUpdateReason.Text.Trim();


					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
				
				}
				this.DataView();
			}
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				
				string m_RequirePage = "BuyingUnitCodeInfo";
				string m_RequireAction = "Delete";
				string m_TableName = "UCI_MT";
				int m_Idx = (int)PageName.BuyingUnitCodeInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_Index.Value;
				
				ArrayList InputList = new ArrayList();

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add("구매단가");
				InputList.Add(CSC1.BusinessRegistrationNum);//사업자등록번호
				InputList.Add("14000000");
				InputList.Add("14000000");
				InputList.Add(tb_OrderRate.Text);
				InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
				//InputList.Add(hdStandardUnitCost.Value);
				if(tb_DiscountUnitCost.Text.Trim() == "")
					InputList.Add("0");
				else
					InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
				//InputList.Add(hdDiscountUnitCost.Value);
				if(wdcBeginDate.Text.Trim() =="")
					InputList.Add(DateTime.Now.ToShortDateString());
				else
					InputList.Add(wdcBeginDate.Text);
				if(wdcEndDate.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdcEndDate.Text);
				InputList.Add(lb_Index.Value);
				Session["Reason"] = "삭제";

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();
				
			}

			tb_OrderRate.Text = "0";
			tb_StandardUnitCost.Text = "";
			tb_DiscountUnitCost.Text = "0";
			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";
			CSC1.Company = "";
			CSC1.BusinessRegistrationNum = "";
			tbUpdateReason.Text = "";

			lb_Index.Value = "0";

			this.DataView();
		}

		/// <summary>
		/// 참고보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "구매단가정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		/// <summary>
		/// 어깨창 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/UnitCost.aspx','UnitCost'," + "'width=830" + "," + "height=500px');</script>");
		}


		/// <summary>
		/// 팝업버튼클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void pop_Click(object sender, System.EventArgs e)
//		{
//			// 품목명찾기 팝업창 띄우기
//			string txt = Textbox1.Text ;
//			string formID = "BuyingUnitCodeInfo" ;
//			string txtID = "ItemIndex";
//
//			RegisterStartupScript("","<script>window.open('./Popup/Item.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

		
		/// <summary>
		/// 참고보기 그리드 더블클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Ultrawebgrid2_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				//				tb_ItemNum.Text = dr["ItemNum"].ToString();
				//				tb_ItemDrawNum.Text = dr["ItemDrawNum"].ToString();
				//				tb_ItemName.Text = dr["ItemName"].ToString();
				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_Unit.Text = dr["Unit"].ToString();
				tb_Standard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tb_Unit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}

		private void tbTotalUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection con1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con1.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,con1);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			con1.Close();

			if(File1.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(File1.PostedFile.FileName) + System.IO.Path.GetFileName(File1.PostedFile.FileName);
			
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls" || FileName.ToString().Substring(FileName.ToString().Length - 3) == "XLS")
				{

					string upLoadFile = Path.GetTempFileName();
					File1.PostedFile.SaveAs(upLoadFile);

					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
			
					con.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con);
						
					adapter.Fill(dataset);
					con.Close();

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();
					SqlTransaction tr = conn.BeginTransaction();

					try
					{

						UnitCodeInfo(conn,tr);
						
						Response.Write("<script language=javascript>");
						Response.Write("alert('수정하였습니다.');");
						Response.Write("</script>");
						tr.Commit();
					}
					catch(Exception ee)
					{
						Response.Write("<script language=javascript>");
						Response.Write("alert('"+ee.Message+"');");
						Response.Write("</script>");

						tr.Rollback();
					}
					finally
					{
						File.Delete(upLoadFile);
						conn.Close();
					}
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}


		private void UnitCodeInfo(SqlConnection con, SqlTransaction trans)
		{
			
			foreach(DataRow row in dataset.Tables[0].Rows)
			{
				string str = "";
				SqlCommand comm = new SqlCommand();
				comm.Connection = con;
				comm.Transaction = trans;
				switch(row["단가구분"].ToString().Trim())
				{
					case "외주단가" :
						str = @"Update UCI_MT Set StandardUnitCost = @UnitCost, OrderRate=@OrderRate,BeginDate = @BeginDate, UpdatingPerson  =@User, UpdatingPersonID = @ID, UpdatingDate = @UpdatingDate
						Where ItemNum = @ItemNum and BusinessRegistrationNum = @BusinessRegistrationNum 
						and BeginProcessCode = @BeginProcessCode and EndProcessCode = @EndProcessCode and RecodingState = 1 and UnitCostDistinction = '외주단가'";
						comm.Parameters.Add("@UnitCost",row["기준단가"].ToString().Trim());
						comm.Parameters.Add("@ItemNum",row["품목번호"].ToString().Trim());
						comm.Parameters.Add("@BusinessRegistrationNum",row["거래처번호"].ToString().Trim());
						comm.Parameters.Add("@BeginProcessCode",UnitCode(row["시작공정"].ToString().Trim(),con,trans));
						comm.Parameters.Add("@EndProcessCode",UnitCode(row["종료공정"].ToString().Trim(),con,trans));
						if(row["발주비율"].ToString().Trim() == "")
							comm.Parameters.Add("@OrderRate",0);
						else
                            comm.Parameters.Add("@OrderRate",row["발주비율"].ToString().Trim());

						if(row["적용시작일"].ToString().Trim() == "")
							comm.Parameters.Add("@BeginDate",DateTime.Now.ToShortDateString());
						else
							comm.Parameters.Add("@BeginDate",DateTime.Parse(row["적용시작일"].ToString().Trim()).ToShortDateString());

						comm.Parameters.Add("@User",m_UserName);
						comm.Parameters.Add("@ID",Session["ID"].ToString().Trim());
						comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
						break;
					case "판매단가"	:
						str = @"Update UCI_MT Set StandardUnitCost = @UnitCost , OrderRate=@OrderRate,BeginDate = @BeginDate, UpdatingPerson  =@User, UpdatingPersonID = @ID, UpdatingDate = @UpdatingDate Where ItemNum = @ItemNum 
						and BusinessRegistrationNum = @BusinessRegistrationNum 
						and RecodingState = 1  and UnitCostDistinction = '판매단가'";
						comm.Parameters.Add("@UnitCost",row["기준단가"].ToString().Trim());
						comm.Parameters.Add("@ItemNum",row["품목번호"].ToString().Trim());
						comm.Parameters.Add("@BusinessRegistrationNum",row["거래처번호"].ToString().Trim());
						comm.Parameters.Add("@BeginProcessCode","14009999");
						comm.Parameters.Add("@EndProcessCode","14009999");
						if(row["발주비율"].ToString().Trim() == "")
							comm.Parameters.Add("@OrderRate",0);
						else
							comm.Parameters.Add("@OrderRate",row["발주비율"].ToString().Trim());

						if(row["적용시작일"].ToString().Trim() == "")
							comm.Parameters.Add("@BeginDate",DateTime.Now.ToShortDateString());
						else
							comm.Parameters.Add("@BeginDate",DateTime.Parse(row["적용시작일"].ToString().Trim()).ToShortDateString());
						comm.Parameters.Add("@User",m_UserName);
						comm.Parameters.Add("@ID",Session["ID"].ToString().Trim());
						comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
						break;
					case "구매단가" :
						str = @"Update UCI_MT Set StandardUnitCost = @UnitCost, OrderRate=@OrderRate,BeginDate = @BeginDate, UpdatingPerson  =@User, UpdatingPersonID = @ID, UpdatingDate = @UpdatingDate Where ItemNum = @ItemNum 
						and BusinessRegistrationNum = @BusinessRegistrationNum and RecodingState = 1
						and UnitCostDistinction = '구매단가'";
						comm.Parameters.Add("@UnitCost",row["기준단가"].ToString().Trim());
						comm.Parameters.Add("@ItemNum",row["품목번호"].ToString().Trim());
						comm.Parameters.Add("@BusinessRegistrationNum",row["거래처번호"].ToString().Trim());
						comm.Parameters.Add("@BeginProcessCode","14000000");
						comm.Parameters.Add("@EndProcessCode","14000000");
						if(row["발주비율"].ToString().Trim() == "")
							comm.Parameters.Add("@OrderRate",0);
						else
							comm.Parameters.Add("@OrderRate",row["발주비율"].ToString().Trim());

						if(row["적용시작일"].ToString().Trim() == "")
							comm.Parameters.Add("@BeginDate",DateTime.Now.ToShortDateString());
						else
							comm.Parameters.Add("@BeginDate",DateTime.Parse(row["적용시작일"].ToString().Trim()).ToShortDateString());
						comm.Parameters.Add("@User",m_UserName);
						comm.Parameters.Add("@ID",Session["ID"].ToString().Trim());
						comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
						break;
				}
				comm.CommandText = str;
				

				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				str = @"Insert Into UPUCI_HT (ItemNum, UnitCostDistinction, BusinessRegistrationNum, BeginProcessCode, 
				EndProcessCode,	OrderRate, StandardUnitCost, DiscountUnitCost, BeginDate, EndDate,UpdateReason,
				UpdatingPerson, UpdatingPersonID, UpdatingDate, UnitCostInfoIndex)
				values (@ItemNum, @UnitCostDistinction, @BusinessRegistrationNum, @BeginProcessCode, 
				@EndProcessCode, @OrderRate, @StandardUnitCost, @DiscountUnitCost, @BeginDate, @EndDate, @UpdateReason,
				@UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @UnitCostInfoIndex)";

				comm.Parameters.Add("@ItemNum", row["품목번호"].ToString().Trim());
				comm.Parameters.Add("@UnitCostDistinction", row["단가구분"].ToString().Trim());
				comm.Parameters.Add("@BusinessRegistrationNum", row["거래처번호"].ToString().Trim());
				if(row["단가구분"].ToString().Trim() == "판매단가")
				{
					comm.Parameters.Add("@BeginProcessCode","14009999");
					comm.Parameters.Add("@EndProcessCode","14009999");
				}
				else if(row["단가구분"].ToString().Trim() == "구매단가")
				{
					comm.Parameters.Add("@BeginProcessCode","14000000");
					comm.Parameters.Add("@EndProcessCode","14000000");
				}
				else if(row["단가구분"].ToString().Trim() == "외주단가")
				{
					comm.Parameters.Add("@BeginProcessCode", row["시작공정"].ToString().Trim());
					comm.Parameters.Add("@EndProcessCode", row["종료공정"].ToString().Trim());
				}

				//comm.Parameters.Add("@OrderRate", decimal.Parse(row["발주비율"].ToString().Trim()));
				if(row["발주비율"].ToString().Trim() == "")
					comm.Parameters.Add("@OrderRate",0);
				else
					comm.Parameters.Add("@OrderRate",row["발주비율"].ToString().Trim());
				comm.Parameters.Add("@StandardUnitCost", decimal.Parse(row["기준단가"].ToString().Trim()));
				if(row["할인단가"].ToString().Trim() == "")
					comm.Parameters.Add("@DiscountUnitCost", "0");
				else
                    comm.Parameters.Add("@DiscountUnitCost", decimal.Parse(row["할인단가"].ToString().Trim()));
				if(row["적용시작일"].ToString().Trim() == "")
					comm.Parameters.Add("@BeginDate", DateTime.Now.ToShortDateString());
				else
					comm.Parameters.Add("@BeginDate", DateTime.Parse(row["적용시작일"].ToString().Trim()).ToShortDateString());

				if(row["적용종료일"].ToString().Trim() == "")
					comm.Parameters.Add("@EndDate", "2076-12-31");
				else
					comm.Parameters.Add("@EndDate", DateTime.Parse(row["적용종료일"].ToString().Trim()).ToShortDateString());
				
				if(row["변경사유"].ToString().Trim() == "")
					comm.Parameters.Add("@UpdateReason","일괄수정");
				else
					comm.Parameters.Add("@UpdateReason",row["변경사유"].ToString().Trim());
				comm.Parameters.Add("@UpdatingPerson",m_UserName);
				comm.Parameters.Add("@UpdatingPersonID",Session["ID"].ToString().Trim());
				comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@UnitCostInfoIndex", FindUCI_MT(con,trans,row["품목번호"].ToString(),row["거래처번호"].ToString(),row["단가구분"].ToString()));
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
				

			}
		}

		private int FindUCI_MT(SqlConnection conn, SqlTransaction tr,string Item, string Num, string Division)
		{
			string str = @"Select isnull(Max(UnitCostInfoIndex),0) From UCI_MT Where ItemNum = @ItemNum and 
				BusinessRegistrationNum = @BusinessRegistrationNum and RecodingState = 1 and UnitCostDistinction = @UnitCostDistinction";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum", Item);
			comm.Parameters.Add("@UnitCostDistinction", Division);
			comm.Parameters.Add("@BusinessRegistrationNum", Num);
			comm.CommandText = str;

			int result = int.Parse(comm.ExecuteScalar().ToString());
			if(result == 0)
				throw new Exception(Division + "에 품목번호가 " + Item + "이며 거래처번호가 "+Num+"인 레코드가 없습니다!");
			return result;
			
		}

		private string UnitCode(string Unit,SqlConnection con, SqlTransaction trans)
		{
			string Code = "";

			if(Unit.Trim() == "")
			{
				return Code;
			}
			else
			{
				
				SqlCommand comm = new SqlCommand();
				comm.Connection = con;
				comm.Transaction = trans;
				string str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '1400' and SmallClassificationName = @Unit";
				
				comm.CommandText = str;
				comm.Parameters.Add("@Unit",Unit.Trim());
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					Code = dr["SmallClassificationCode"].ToString();
				}
				dr.Close();

				if(Code == "")
					throw new Exception(""+Unit+" 인 공정이 존재하지 않습니다!");

				return Code;
			}
		}

		private void btHistory_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('UpdateUnitCostHistory.aspx','UpdateUnitCostHistory','width=830,height=600px,resizable=yes');</script>");
		}
		

		private bool Company(string Num, string Name)
		{
			string str = "Select Count(*) From CI_MT Where BusinessRegistrationNum = @Num and CompanyName = @Name and RecodingState  =1 and BuyingCompany = 1";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@Num",Num);
			comm.Parameters.Add("@Name",Name);
			conn.Open();
			int count = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			if(count == 0)
				return false;
			else
				return true;
		}

	}
}
