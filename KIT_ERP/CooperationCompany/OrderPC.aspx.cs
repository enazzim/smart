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

namespace KIT_ERP.CooperationCompany
{
	/// <summary>
	/// OrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class OrderPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBO_HT;
		protected System.Web.UI.WebControls.Button btnXML_OO_HT;
		protected System.Web.UI.WebControls.Button btnAutoReceive_OO_HT;
		protected System.Web.UI.WebControls.Button btnRegister;
		protected System.Web.UI.WebControls.TextBox txtRemainQuantity;
		protected System.Web.UI.WebControls.TextBox txtCashQuantity;
		protected System.Web.UI.WebControls.TextBox txtOrderQuantity;
		protected System.Web.UI.WebControls.TextBox txtDeliveryQuantity;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected System.Web.UI.WebControls.TextBox txtItemDrawNum;
		protected System.Web.UI.WebControls.TextBox txtItemNum;
		protected System.Web.UI.WebControls.Button btnExcel_BO_HT;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel_BO_HT;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgDR_HT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hddivision;

	

		//페이지 로드시
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 협력사정보 > 발주현황';</script>");
				
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string datetime = DateTime.Now.ToShortDateString();

				if(Session["ID"].ToString() == "admin")
				{
					//저장프로시져를 이용해 로그인 사용자 테이블에서 로그인 아이디가 같고 사업자등록번호가 같은 회사를 Select 해서 그리드에 뿌려준다.
					SqlDataAdapter BO_OO_Adapter = new SqlDataAdapter("select * from OrderPC where (progresscondition ='대기' or progresscondition = '진행')",conn);
					DataSet ds = new DataSet();
					BO_OO_Adapter.Fill(ds);
					this.uwgBO_HT.DataSource = ds.Tables[0];
					this.uwgBO_HT.DataBind();			
				}
				else
				{
					SqlDataAdapter BO_OO_Adapter = new SqlDataAdapter("select * from OrderPC where (progresscondition ='대기' or progresscondition = '진행') and OrderPC.BusinessRegistrationNum = (select BusinessRegistrationNum from UI_MT where ID =@userID)",conn);
					BO_OO_Adapter.SelectCommand.Parameters.Add("@userID",SqlDbType.VarChar).Value = Session["ID"].ToString();
					DataSet ds = new DataSet();
					BO_OO_Adapter.Fill(ds);
					this.uwgBO_HT.DataSource = ds.Tables[0];
					this.uwgBO_HT.DataBind();			
				}
				
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
			this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
			this.btnExcel_BO_HT.Click += new System.EventHandler(this.btnExcel_BO_HT_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//외주거래처에서 납품의뢰를 할 경우
		private void btnDeliveryRequest_Click(object sender, System.EventArgs e)
		{			
			//납품의뢰버튼 클릭시 팝업창이 뜨서 납품의뢰 수량을 기입할수 있는 그리드가 나타나게 된다.
			string formID = "OrderPCPopUP_OutSideOrder";
			Response.Write("<script language = javascript>");
			Response.Write("window.open('OrderPCPopUP_OutSideOrder.aspx?formID=" + formID + "','OrderPCPopUP_OutSideOrder'," + "'width=765" + ",center= yes," + "height=270px');");
			Response.Write("</script>");
		}
	

		//구매거래처에서 납품의뢰를 할경우
		private void btnDeliveryRequest_BO_HT_Click(object sender, System.EventArgs e)
		{			
			//납품의뢰버튼 클릭시 팝업창이 뜨서 납품의뢰 수량을 기입할수 있는 그리드가 나타나게 된다.
			string formID = "OrderPCPopUP_BuyingOrder";
			Response.Write("<script language = javascript>");
			Response.Write("window.open('OrderPCPopUP_BuyingOrder.aspx?formID=" + formID + "','OrderPCPopUP_BuyingOrder'," + "'width=765" + ",center= yes," + "height=270px');");
			Response.Write("</script>");
		}


		//발주현황 텝에 있는 그리드가 엑셀로 다운로드 된다.
		private void btnExcel_BO_HT_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgBO_HT;
			this.uwgExcel_BO_HT.Export(uwgBO_HT);	
		}


		//납품의뢰버튼을 클릭
		private void btnRegister_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
		
		
			if(hddivision.Value.Trim() =="")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('품목을 선택하세요');");
				Response.Write("</script>");

			}

			else if(this.txtDeliveryQuantity.Text == "0" || this.txtDeliveryQuantity.Text.Trim() == "")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('납품의뢰 할 수량이 없습니다.!');");
				Response.Write("</script>");
			}

				//지금 납품의뢰 하고자 하는 품목이 구매납품의뢰 인지 외주납품의뢰인지를 판단
				//구매의뢰납품
			else if(int.Parse(hddivision.Value) == 1)
			{
			
			
				arr.Add(txtItemNum.Text);//품목번호
				arr.Add(txtItemDrawNum.Text);//도면번호
				arr.Add(txtItemName.Text);//품목명
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("CompanyName").Value.ToString());//거래처명
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Value.ToString());//사업자등록번호
				arr.Add(decimal.Parse(txtDeliveryQuantity.Text));//납품의뢰수량
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("ApplyUnitCost").Value.ToString());//단가
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("TotalCost").Value.ToString());//TotalCost
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());//구매발주원장번호
	
			}

				//외주의뢰납품
			else
			{			
				arr.Add(txtItemNum.Text);//품목번호
				arr.Add(txtItemDrawNum.Text);//도면번호
				arr.Add(txtItemName.Text);//품목명
				
				
				arr.Add(ProcessSequence(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcessCode").Value.ToString()));//공정순서번호
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcessCode").Value.ToString());//공정순서번호
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcess").Value.ToString());//공정명

				arr.Add(ProcessSequence(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcessCode").Value.ToString()));//공정순서번호
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcessCode").Value.ToString());//공정순서번호
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("EndProcess").Value.ToString());//공정명


				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("CompanyName").Value.ToString());//거래처명
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Value.ToString());//사업자등록번호
				arr.Add(decimal.Parse(txtDeliveryQuantity.Text));//납품의뢰수량
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("ApplyUnitCost").Value.ToString());//단가
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("TotalCost").Value.ToString());//TotalCost
				arr.Add(this.uwgBO_HT.Rows[int.Parse(hdRowIndex.Value)].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());//외주발주원장번호
			
			}		

			//등록..
			KIT_ERP.Register register = new KIT_ERP.Register(hddivision.Value,arr,Session["ID"].ToString());
			register.Registration();
		

			/////////////////////////////////////////////////////////////
			//납품의뢰를 하고 나면 납품의뢰품목에 관한것 항목들 초기화///
			/////////////////////////////////////////////////////////////
			txtItemNum.Text = null;				//품목번호 초기화
			txtItemDrawNum.Text = null;			//도면번호 초기화
			txtItemName.Text = null;			//품목명 초기화
			txtDeliveryQuantity.Text = null;	//검색 버튼 누르고난뒤 납품수량 텍스트박스 초기화						
			txtOrderQuantity.Text = null;		//발주수량 텍스트 박스 초기화
			txtRemainQuantity.Text = null;		//잔량 텍스트박스 초기화
			txtCashQuantity.Text = null;		//납입수량 텍스트박스 초기화



			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//저장프로시져를 이용해 로그인 사용자 테이블에서 로그인 아이디가 같고 사업자등록번호가 같은 회사를 Select 해서 그리드에 뿌려준다.//
			//DeliveryRequestBiding 저장프로시져 사용/////////////////////////////////////////////////////////////////////////////////////
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter da= new SqlDataAdapter("DeliveryRequestBiding",conn);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.Add("@userID",SqlDbType.VarChar,10).Value = Session["ID"].ToString();
			DataSet ds = new DataSet();
			da.Fill(ds);
			this.uwgDR_HT.DataSource = ds.Tables[0];
			this.uwgDR_HT.DataBind();	
	}		


		//공정번호 찾기함수
		private int ProcessSequence(string code)
		{
			int sequence = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode=@code and ItemNum = @item";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
			comm.Parameters.Add("@item",SqlDbType.VarChar).Value = txtItemNum.Text;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				sequence = int.Parse(dr["ProcessSequenceNum"].ToString());
			}
			dr.Close();
			conn.Close();

			return sequence;

		}	
	}
}
