using System;
using System.Configuration;
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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ExecutionPlanInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ExecutionPlanInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label lb_ItemNum;
		protected System.Web.UI.WebControls.Label lb_ItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ItemName;
		protected System.Web.UI.WebControls.Label lb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.Label lb_PlanQuantity;
		protected System.Web.UI.WebControls.Label lb_SaleUnitCost;
		protected System.Web.UI.WebControls.Label lb_TotalPlanCost;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_ExecutionPlanView;
		protected System.Web.UI.WebControls.Label lb_Date;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.WebControls.TextBox tb_PlanQuantity;
		protected System.Web.UI.WebControls.TextBox tb_SaleUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_PlanTotalCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true; //상품 바인딩
//			ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
//			ItemSearchControl1.Phantom = true; //팬텀 바인딩	
//			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	

			ItemSearchControl1.IsDoPost = true;
			
			if(!Page.IsPostBack)
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 실행계획정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_PlanQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_PlanQuantity.Attributes.Add("OnKeyUp", "return Process()");
			
				// focus를 얻으면 자동으로 select()
				tb_PlanQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			
				// focus를 잃으면 다시 입력란을 체크
				tb_PlanQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");



				tb_SaleUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_SaleUnitCost.Attributes.Add("OnKeyUp", "return Process()");
			
				// focus를 얻으면 자동으로 select()
				tb_SaleUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			
				// focus를 잃으면 다시 입력란을 체크
				tb_SaleUnitCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");
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
			this.bt_ExecutionPlanView.Click += new System.EventHandler(this.bt_ExecutionPlanView_Click);
			this.bt_Reference.Click += new System.EventHandler(this.bt_Reference_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		/// <summary>
		/// 실행계획보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_ExecutionPlanView_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택후 버튼을 눌러주세요');");
				Response.Write("</script>");
			}
			else
                Data_View();
		}


		private void Data_View()
		{
			View view = new View(ItemSearchControl1.ItemNum.ToString(), "ExecutionPlanInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
		}

		

		private void clear()
		{
			wdcDate.Value = "";
			tb_PlanQuantity.Text = "0";
			tb_SaleUnitCost.Text = "0";
			tb_PlanTotalCost.Text ="0";
			lb_Index.Value = "0";
		}
        

		
		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			ArrayList list = new ArrayList();

			list.Add(ItemSearchControl1.ItemNum.ToString());
			list.Add(wdcDate.Text);
			list.Add(tb_PlanQuantity.Text.Trim());
			list.Add(tb_SaleUnitCost.Text.Trim());
			list.Add(tb_PlanTotalCost.Text.Trim());
			list.Add(0);

			MasterInfoRecordRUD rud = new MasterInfoRecordRUD("ExecutionPlanInfo", "Registration","EPI_MT",(int)PageName.ExecutionPlanInfo, Session["ID"].ToString(),list,"낱개입력");
			rud.ActivateRUD();

			Data_View();

		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 수정은 불가능 합니다!');</script>");
			}
			else
			{
				ArrayList list = new ArrayList();

				list.Add(ItemSearchControl1.ItemNum.ToString());
				list.Add(wdcDate.Text);
				list.Add(tb_PlanQuantity.Text.Trim());
				list.Add(tb_SaleUnitCost.Text.Trim());
				list.Add(tb_PlanTotalCost.Text.Trim());
				list.Add(0);
				list.Add(lb_Index.Value);
				
				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("ExecutionPlanInfo", "Update","EPI_MT",(int)PageName.ExecutionPlanInfo,Session["ID"].ToString(),list,"낱개입력");
				rud.ActivateRUD();

				Data_View();
			}
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 삭제는 불가능 합니다!');</script>");
			}
			else
			{
				ArrayList list = new ArrayList();

				list.Add(ItemSearchControl1.ItemNum.ToString());
				if(wdcDate.Text.Trim() == "")
					list.Add("");
				else
					list.Add(wdcDate.Value.ToString().Substring(0,10).Trim());
				list.Add(tb_PlanQuantity.Text.Trim());
				list.Add(tb_SaleUnitCost.Text.Trim());
				list.Add(tb_PlanTotalCost.Text.Trim());
				list.Add(0);
				list.Add(lb_Index.Value);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("ExecutionPlanInfo", "Delete","EPI_MT",(int)PageName.ExecutionPlanInfo,Session["ID"].ToString(),list,"낱개입력");
				rud.ActivateRUD();

				Data_View();
				clear();
			}
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
//			tb_ItemNum.Text = "";
//			tb_ItemDrawNum.Text = "";
//			tb_ItemName.Text = "";
			tb_PropertyClassification.Text = "";
			tb_Unit.Text = "";
			tb_Standard.Text ="";
			lb_ItemIndex.Text = "0";

			clear();
		}

		/// <summary>
		/// 어깨창 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/ExecutionPlan.aspx','ExecutionPlan'," + "'width=820" + "," + "height=500px');</script>");
		}

		/// <summary>
		/// 참고보기버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{
				string m_TableName = "실행계획정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference().Tables[0];
				Ultrawebgrid2.DataBind();
			}
		}

//		private void pop_Click(object sender, System.EventArgs e)
//		{
//			// 품목명찾기 팝업창 띄우기
//			string txt = TextBox1.Text ;
//			string formID = "ExecutionPlanInfo" ;
//			string txtID = "ItemIndex";
//
//			RegisterStartupScript("","<script language=javascript>window.open('./Popup/Item.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

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
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '상품') and RecodingState = 1";
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
	}
}
