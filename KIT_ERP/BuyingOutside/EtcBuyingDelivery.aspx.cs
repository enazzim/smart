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

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// EtcBuyingDelivery에 대한 요약 설명입니다.
	/// </summary>
	public class EtcBuyingDelivery : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtD_ItemName;
		protected System.Web.UI.WebControls.TextBox txtD_DeliveryQuantity;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HDIndexNum;
		protected System.Web.UI.WebControls.TextBox txtD_CompanyName;
		protected System.Web.UI.WebControls.TextBox txtD_RemainQuantity;
		protected System.Web.UI.WebControls.TextBox txtD_OrderQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HDRowIndex;
		protected System.Web.UI.WebControls.Button btnReg;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wDcDDeliveryDate;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HDBusinessRegNum;
		protected System.Web.UI.WebControls.TextBox tbApplycost;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdReasonCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdReasonName;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매거래처";
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if ( !IsPostBack )
			{
				PageLoad_Bind();
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

		private void PageLoad_Bind()
		{
			//  OnKeyDown 이벤트가 발생하면 숫자만 받도록 하는 자바스크립트 함수 매핑
			txtD_DeliveryQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

			// OnKeyUp 이벤트가 발생하면 콤마를 찍는 자바스크립트 함수 매핑
			txtD_DeliveryQuantity.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this);");

			// 등록 버튼 클릭시 클라이언트에서 Form 체크
			btnReg.Attributes.Add("onclick", "return CheckForm();");
			wDcDDeliveryDate.NullDateLabel = DateTime.Now.ToShortDateString();

			foreach(ListItem list in ddlYear.Items)
			{
				if(list.Value == DateTime.Now.Year.ToString())
				{
					list.Selected = true;
				}
			}

			for(int a = 0; a < ddlMon.Items.Count; a++)
			{
				if(DateTime.Now.Month == 12)
				{
					if(DateTime.Now.Day > 25)
					{
						for(int b = 0; b< ddlYear.Items.Count;b++)
						{
							ddlYear.Items[b].Selected = false;
							if(int.Parse(ddlYear.Items[b].Value) == DateTime.Now.Year + 1)
								ddlYear.Items[b].Selected = true;
						}
						ddlMon.Items[0].Selected = true;							
						break;
					}
					else
					{
						ddlMon.Items[DateTime.Now.Month-1].Selected = true;
						break;
					}
				}
				else
				{
					if(ddlMon.Items[a].Value == DateTime.Now.Month.ToString() )
					{
						if(DateTime.Now.Day > 25)
							ddlMon.Items[a+1].Selected = true;
						else
							ddlMon.Items[a].Selected = true;
						break;
					}
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnReg.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region 검색버튼 클릭 이벤트 핸들러와 관련 메서드
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string[] strArrValue = new string[5];
			strArrValue[0] = txtItemName.Text;
			strArrValue[1] = CSC1.Company;
			strArrValue[2] = CSC1.BusinessRegistrationNum;
			strArrValue[3] = wcDeliveryDate.Text;
			strArrValue[4] = wcDeliveryDate1.Text;

			this.DataBind(strArrValue);
		}

		private void DataBind(string[] strArrValue)
		{
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingDelivery_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = strArrValue[3] ;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = strArrValue[4] ;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}
		#endregion
		
		#region 납품(등록) 클릭 이벤트 핸들러와 관련 메서드
		private void btnRegistration_Click(object sender, System.EventArgs e)
		{
			int i = int.Parse( HDRowIndex.Value );
			string[] strArrValue = new string[16];

			strArrValue[0] = txtD_ItemName.Text;												// 품목명
			strArrValue[1] = txtD_CompanyName.Text;										// 커래처명
			strArrValue[2] = HDBusinessRegNum.Value;										// 사업자번호
			strArrValue[3] = txtD_OrderQuantity.Text;											// 발주수량
			strArrValue[4] = tbApplycost.Text.Trim();		// 적용단가
			strArrValue[5] = txtD_DeliveryQuantity.Text.Trim();									// 납품수량
			strArrValue[6] = Convert.ToString( Convert.ToDecimal( strArrValue[5] ) * Convert.ToDecimal( strArrValue[4] ) );	// 총금액(단가 * 납품수량)
			strArrValue[7] = Convert.ToString( Convert.ToDecimal( strArrValue[3] ) - Convert.ToDecimal( strArrValue[5] ) );	// 잔량(발주수량 - 납품수량)
			strArrValue[8] = wDcDDeliveryDate.Text.Trim();
			strArrValue[9] = Session["ID"].ToString();
			strArrValue[10] = HDIndexNum.Value.Trim();
			strArrValue[11] = DateTime.Now.ToShortDateString();
			strArrValue[12] = ddlYear.SelectedItem.Value;
			strArrValue[13] = ddlMon.SelectedItem.Value;	
			strArrValue[14] = hdReasonCode.Value;
			strArrValue[15] = hdReasonName.Value;
			
			if(MonthClosing())
			{
				this.DB_Regist(strArrValue);
				Response.Write("<script>alert('등록되었습니다.')</script>");
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 입고가 불가능 합니다!');</script>");
			}
			this.btnSearch_Click(sender, e);
			this.TextBox_Reset();
		}
		/// <summary>
		/// 물리적인 DB에 등록정보들을 입력
		/// </summary>
		/// <param name="strArrValue">등록될 정보 (String 배열)</param>
		private void DB_Regist(string[]  strArrValue)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("EtcBuyingDelivery_Regist", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = strArrValue[0];
			cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			cmd.Parameters.Add("@BusinessRegNum", SqlDbType.VarChar).Value = strArrValue[2];
			cmd.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrValue[3];
			cmd.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = strArrValue[4];
			cmd.Parameters.Add("@DeliveryQuantity", SqlDbType.Decimal).Value = strArrValue[5];
			cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = strArrValue[6];
			cmd.Parameters.Add("@RemainQuantity", SqlDbType.Decimal).Value = strArrValue[7];
			cmd.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime( strArrValue[8] );
			cmd.Parameters.Add("@RegistrationPersonID", SqlDbType.VarChar).Value = strArrValue[9];
			cmd.Parameters.Add("@EtcBuyingOrderHistoryIndex", SqlDbType.Int).Value = strArrValue[10];
			cmd.Parameters.Add("@RegistrationDate", SqlDbType.VarChar).Value = strArrValue[11];
			cmd.Parameters.Add("@DeliveryYear", strArrValue[12]);
			cmd.Parameters.Add("@DeliveryMon", strArrValue[13]);
			cmd.Parameters.Add("@ReasonCode", strArrValue[14]);
			cmd.Parameters.Add("@ReasonName", strArrValue[15]);
			
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
		#endregion


		private void TextBox_Reset()
		{
			txtD_ItemName.Text = "";
			txtD_CompanyName.Text = "";
			txtD_DeliveryQuantity.Text = "";
			txtD_OrderQuantity.Text = "";
			txtD_RemainQuantity.Text = "";
			tbApplycost.Text = "0";
			wDcDDeliveryDate.Value = null;
		}

//		/// <summary>
//		/// 월마감 여부를 확인하는 메소드
//		/// </summary>
//		/// <returns></returns>
//		public bool MonthClosing()
//		{
//			int intYear = 0;
//			int intMonth = 0;
//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
//			string str = "Select [ClosingYear], [ClosingMonth] From MCI_MT Where AffairDistinction = @Distinction";
//			SqlCommand comm =  new SqlCommand(str,conn);
//			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
//
//			try
//			{
//				conn.Open();
//				SqlDataReader dr = comm.ExecuteReader();
//				while(dr.Read())
//				{
//					intYear = int.Parse(dr[0].ToString());
//					intMonth = int.Parse(dr[1].ToString());
//				}
//			} 
//			catch ( Exception e)
//			{
//				Response.Write("<script>alert(\"" + e.Message + "\")</script>"); 
//			} 
//			finally
//			{
//				conn.Close();
//			}
//			if(intYear < int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
//			{
//				return true;
//			}
//			else
//			{
//				if(intMonth < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
//					return true;
//				else
//					return false;
//			}		
//		}

		// Excel
		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		// 페이지
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.btnSearch_Click(sender, e);
		}


		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < int.Parse(ddlYear.SelectedItem.Value))
			{
				return true;
			}
			else if(year == int.Parse(ddlYear.SelectedItem.Value))
			{
				if(month < int.Parse(ddlMon.SelectedItem.Value))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		

			
		}

	}
}
