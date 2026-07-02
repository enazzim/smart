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
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;


namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ProductionRequestPC에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionRequestPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		//protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Regist;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRemainQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdTotalCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩
			CSC1.UnitCostDistinction = "수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
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
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";
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
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Regist.Click += new System.EventHandler(this.bt_Regist_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Regist_Click(object sender, System.EventArgs e)
		{
			string m_User = Session["ID"].ToString();
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
			
				Register reg = new Register(UltraWebGrid1, "ProductionRequest", m_User);
				reg.MainRegistration();
				this.Table_Search();
			}

		}


		private void Table_Search()
		{
			// 생산의뢰
			Search search;
			if(ItemSearchControl1.hdItem.Trim() =="")
                search = new Search("ProductionRequest",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,ddlItemClassification1.SelectedItem.Value,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			else
				search = new Search("ProductionRequest",ItemSearchControl1.ItemNum,"","",wdcStartDate,ddlItemClassification1.SelectedItem.Value,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();
		}



		private void Button1_Click(object sender, System.EventArgs e)
		{	
			UltraWebGrid UWG = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			UWG = UltraWebGrid1;

			int a = 0;
			//체크 항목중 진행상태가 대기이거나 중단일때만 삭제 가능
			//그리드를 반복하면서 체크되지 않는 항목은 지운다.
			for(int i = UWG.Rows.Count-1; i >= 0;i--)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(UWG.Rows[i].Cells.FromKey("chk").Value == null)
				{
					UWG.Rows[i].Cells.FromKey("chk").Value = false;
					//UWG.Rows[i].Delete();
				}

				if(bool.Parse(UWG.Rows[i].Cells.FromKey("chk").Value.ToString()))
				{
					a = 1;
					break;
				}
			}
	
			//if(UWG.Rows.Count == 0)
			if(a == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택해주세요!');");
				Response.Write("</script>");
			}
			else
			{
				Session["uwg"] = UWG;
				Page.RegisterClientScriptBlock("","<script>window.open('PopUp/StockRemainder.aspx','','width = 850, height= 250,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");	
			}
		}



		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			int a = int.Parse(hdRowIndex.Value);
			ProductionRequestRegister(a);
			ReceivingOrderHistoryUpdate(a);

			Response.Write("<script language=javascript>");
			Response.Write("alert('등록하였습니다!');");
			Response.Write("</script>");
		
			this.Table_Search();
		}


		private void ProductionRequestRegister(int count)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,ProductionRequestSourceCode,ProductionRequestSource,"+
				"CompanyName,BusinessRegistrationNum,PropertyClassification,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
				"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
				"ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate,ReceivingOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@comname,@comnum,@classification,@total,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,'대기', @Person, @PersonID, @Date,@idx)";

			SqlCommand comm = new SqlCommand(str,conn);

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03100010";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			comm.Parameters.Add("@comname",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("CompanyName").Value.ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = UltraWebGrid1.Rows[count].Cells.FromKey("PropertyClassification").Value.ToString();

			if(UltraWebGrid1.Rows[count].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString() ==null || UltraWebGrid1.Rows[count].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("TotalReceiveingOrderQuantity").Text);
			
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Text);

			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value == null || Convert.IsDBNull(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value))
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value.ToString());

			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text);
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value == null)
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value.ToString());
			
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text);
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value == null)
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
			
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString();
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate4").Value == null)
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate4").Text);
			
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text);
			if(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value == null)
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value.ToString());
			
			if(UltraWebGrid1.Rows[count].Cells.FromKey("ApplyUnitCost").Value == null || UltraWebGrid1.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() =="")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ApplyUnitCost").Text);
			
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = Session["UserName"].ToString();			
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();
		}

		
		/// <summary>
		/// 수주원장 업데이트
		/// </summary>
		/// <param name="rowcount"></param>
		private void ReceivingOrderHistoryUpdate(int count)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			

			string str = "UPDATE RO_HT SET ProgressCondition = '진행' WHERE ReceivingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
	
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			Table_Search();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			Table_Search();
		}

		

		
	}
}
