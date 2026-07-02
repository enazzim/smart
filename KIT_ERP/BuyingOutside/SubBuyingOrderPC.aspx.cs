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

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// SubBuyingOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class SubBuyingOrderPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate1;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.LinkButton lkbtnUpdate;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;

		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlProperty;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.SubMaterials = true; //부자재 바인딩	
			ItemSearchControl1.Expendable = true;// 소모품
			CSC1.UnitCostDistinction = "구매거래처";

			if(!Page.IsPostBack)
			{
				btnDelete.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 삭제를 하시겠습니까?');");
							
			}	
		
			// 월마감을 체크하여 등록, 수정, 삭제 여부를 클라이언트 단에서 체크하기 위해 JavaScript 전역변수값을 출력 
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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.lkbtnUpdate.Click += new System.EventHandler(this.lkbtnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";

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
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			
		}
		#endregion

		//검색함수
		private DataSet Search()
		{
			SqlDataAdapter adap = new SqlDataAdapter("SubBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			adap.SelectCommand.Parameters.Add("@ItemDrawNum", SqlDbType.VarChar).Value = ItemSearchControl1.ItemDrawNum;
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = ItemSearchControl1.ItemName;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
			adap.SelectCommand.Parameters.Add("@ProgressCondition", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;
			adap.SelectCommand.Parameters.Add("@Property", SqlDbType.VarChar).Value =ddlProperty.SelectedItem.Value;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			return ds;
		}

		//검색버튼
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;

			

			this.UltraWebGrid1.DataSource = Search();
			this.UltraWebGrid1.DataBind();			
		}

		// Excel버튼
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = UltraWebGrid1;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;

			UltraWebGrid1.DataSource = Search();
			uwgExcelImport.DataBind();
			UltraWebGridExcelExporter1.Export(uwgExcelImport);
		}

		//삭제버튼
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(this.UltraWebGrid1.Rows.Count ==0)
			{				
				Response.Write("<script language=javascript>");
				Response.Write("alert('삭제할 품목이 없습니다.')");
				Response.Write("</script>");
			}
			else
			{
				KIT_ERP.Delete delete = new KIT_ERP.Delete(UltraWebGrid1,"SubBuyingOrderPC");
				delete.MainRowDelete();

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();						
			}			
		}

		//페이지이동
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource =  Search();
			UltraWebGrid1.DataBind();
		}
		
        //수정버튼 클릭
		private void lkbtnUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{			
				SBO_HT_Update(conn,tr); //발주원장 수정하는 함수
				
				tr.Commit();
				Response.Write("<script language=javascript>");
				Response.Write("alert('수정되었습니다!');");
				Response.Write("</script>");
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
				conn.Close();
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();		
			}
		}

		//구매발주원장 수정하는 함수
		private void SBO_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			//기존 구매발주원장에서 납입수량,잔량,진행상태를 Update			
			int rowcount = int.Parse(hdIndex.Value);

			string str =" UPDATE SBO_HT SET " +
				" DeliveryQuantity = @DeliveryQuantity,DeliveryDate=@DeliveryDate,"+
				" ApplyUnitCost = @ApplyUnitCost,TotalCost = @TotalCost, " +
				" DeliveryRemainQuantity = @DeliveryRemainQuantity, "+
				" UpdatingPerson = @person,"+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date "+
				"WHERE SubBuyingOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			//발주원장의 잔량에 기존잔량값 + 기존납품량 - 수정후납품량 을 넣는다.
			comm.Parameters.Add("@DeliveryQuantity", SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = DateTime.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("DeliveryDate").Value.ToString());
			comm.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("TotalCost").Value.ToString());
			comm.Parameters.Add("@DeliveryRemainQuantity", SqlDbType.Decimal).Value = decimal.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
		
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = Session["UserName"].ToString();
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[rowcount].Cells.FromKey("SubBuyingOrderHistoryIndex").Value.ToString());
						
			comm.ExecuteNonQuery();	
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = UltraWebGrid1;
			bool check = true;
			for(int count = UltraWebGrid1.Rows.Count-1 ; count >=0 ; count--)
			{
				if(UWG.Rows[count].Cells.FromKey("chk").Value == null || UWG.Rows[count].Cells.FromKey("chk").Value.ToString() == "false")
					UWG.Rows[count].Delete();
			}

			if(UWG.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else if(UWG.Rows.Count > 12)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 너무 많습니다!');</script>");
			}
			else
			{
				for(int i = 0; i < UWG.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UWG.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text != UWG.Rows[i].Cells.FromKey("BusinessRegistrationNum").Text)
					{
						check = false;
						RegisterStartupScript("","<script>alert('발행할 거래처가 2곳 이상입니다!');</script>");
					}
				}
			}


			if(check)
			{
					
				Session["Grid"] = UWG;
				RegisterStartupScript("","<script>window.open('./Popup/SubPurchase.aspx','','width=1000, heigth=800,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
							
			}
			else
			{
				UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
			}
		}
	}
}
