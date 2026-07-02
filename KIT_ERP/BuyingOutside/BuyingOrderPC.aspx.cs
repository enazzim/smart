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
using Infragistics.WebUI.WebCombo;
using System.Configuration;
using Microsoft.Win32;
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// BuyingOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingOrderPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBO_HT;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.HtmlControls.HtmlInputHidden BuyingOrderHistoryIndex;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;	
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;	
		private static int Division;		
		private DataSet ds;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected System.Web.UI.WebControls.Button btEnd;
		protected System.Web.UI.WebControls.Button btEndCancel;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true;	//상품 바인딩
			CSC1.UnitCostDistinction="구매거래처";
			if(!Page.IsPostBack)
			{


				// 중단버튼에 대하여 onclick속성을 추가 시킴
				btnStop.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 발주를 중단 하시겠습니까?');");
				// 취소버튼에 대하여 onclick속성을 추가 시킴
				btnCancle.Attributes.Add("onClick","return Confirm('발주를 취소 하시겠습니까?');");
				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 삭제를 하시겠습니까?');");
			
				FirstBind();
				

				Volnum();
			}			
			//그리드에 항목이 없을 경우는 취소버튼 비활성화
			if(uwgBO_HT.Rows.Count == 0)
				btnCancle.Enabled = false;
		}

		private void FirstBind()
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
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.uwgBO_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgBO_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btEnd.Click += new System.EventHandler(this.btEnd_Click);
			this.btEndCancel.Click += new System.EventHandler(this.btEndCancel_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		


		// 구매발주원장에서 검색조건에 해당하는 품목들을 검색
		private void btnSearch_Click(object sender, System.EventArgs e)
		{	
			uwgBO_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			uwgBO_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgBO_HT.DataSource = Search();
			this.uwgBO_HT.DataBind();

			Division = 0;
			btnCancle.Enabled = false;

		}

		//Excel로 내리기
		private void btnExcel_Click(object sender, System.EventArgs e)
		{

			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgBO_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;

			if(Division == 0)
			{
				uwgBO_HT.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(uwgBO_HT,"BuyingOrderPC",Volum.Value);
				uwgBO_HT.DataSource = vol.FindVolum();
			}

			uwgExcelImport.DataBind();
			
			uwgExcel.Export(uwgExcelImport);
		}

		//중단버튼을 클릭
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			if(this.uwgBO_HT.Rows.Count ==0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('중단할 품목이 없습니다.')");
				Response.Write("</script>");
			}
			else
			{
				KIT_ERP.Stop stop = new KIT_ERP.Stop(uwgBO_HT, "BuyingOrderPC", Session["ID"].ToString());
				stop.MainTableStop();
				
				if(Division == 0)
				{
					uwgBO_HT.DataSource =Search();
				}
				else
				{
					VideoButton video = new VideoButton(uwgBO_HT,"BuyingOrderPC", Volum.Value);
					uwgBO_HT.DataSource = video.FindVolum();
				}
				uwgBO_HT.DataBind();			
			}			
					
			
		}

		//삭제버튼을 클릭
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(this.uwgBO_HT.Rows.Count ==0)
			{				
				Response.Write("<script language=javascript>");
				Response.Write("alert('삭제할 품목이 없습니다.')");
				Response.Write("</script>");
			}
			else
			{
				KIT_ERP.Delete delete = new KIT_ERP.Delete(uwgBO_HT,"BuyingOrderPC");
				delete.MainRowDelete();

				if(Division == 0)
				{
					uwgBO_HT.DataSource = Search();
				}
				else
				{
					VideoButton video = new VideoButton(uwgBO_HT,"BuyingOrderPC", Volum.Value);
					uwgBO_HT.DataSource = video.FindVolum();
				}
				uwgBO_HT.DataBind();						
			}			
		}

		//취소버튼을 클릭
		private void btnCancle_Click(object sender, System.EventArgs e)
		{
			if(this.uwgBO_HT.Rows.Count ==0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('취소할 품목이 없습니다.')");
				Response.Write("</script>");
			}
			else
			{
//				Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgCancle = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
//				uwgCancle = uwgBO_HT;
//				uwgCancle.DisplayLayout.Pager.AllowPaging = false;

				uwgBO_HT.DisplayLayout.Pager.AllowPaging = false;
				VideoButton aa = new VideoButton(uwgBO_HT,"BuyingOrderPC", Volum.Value);
				uwgBO_HT.DataSource = aa.FindVolum();
				uwgBO_HT.DataBind();
				// 취소객체 생성
				KIT_ERP.Cancel cancle = new KIT_ERP.Cancel(uwgBO_HT, "BuyingOrderPC", Volum.Value);
				cancle.MainRowCancel();
				Volnum();
				uwgBO_HT.DisplayLayout.Pager.AllowPaging = true;
				VideoButton video = new VideoButton(uwgBO_HT,"BuyingOrderPC", Volum.Value);
				uwgBO_HT.DataSource = video.FindVolum();
				uwgBO_HT.DataBind();
					
			}
		}
		
		//그리드 Paging
		private void uwgBO_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgBO_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwgBO_HT.DataSource =  Search();
			uwgBO_HT.DataBind();
		}

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
			string str_Vol = "Select Max(VolumNum) From BO_HT";
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
			uwgBO_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancle.Enabled = true;
			vol_Pre();

			VideoButton vol = new VideoButton(uwgBO_HT,"BuyingOrderPC",Volum.Value);
			ds = vol.FindVolum();
			uwgBO_HT.DataSource = ds;
			uwgBO_HT.DataBind();
			Division = 1;
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			uwgBO_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancle.Enabled = true;
			vol_Now();
			VideoButton vol = new VideoButton(uwgBO_HT,"BuyingOrderPC",Volum.Value);
			ds = vol.FindVolum();
			uwgBO_HT.DataSource = ds;
			uwgBO_HT.DataBind();
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
			uwgBO_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancle.Enabled = true;
			vol_Next();
			
			VideoButton vol = new VideoButton(uwgBO_HT,"BuyingOrderPC",Volum.Value);
			ds = vol.FindVolum();
			uwgBO_HT.DataSource = ds;
			uwgBO_HT.DataBind();
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From BO_HT where VolumNum < @num";
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From BO_HT";
							
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Volum.Value = dr["VolumNum"].ToString();
			dr.Close();

			if(Volum.Value == "")
			{
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!')</script>");
				Volum.Value = "0";
				btnCancle.Enabled = false;
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
			comm.CommandText = "Select Min(VolumNum) as VolumNum From BO_HT where VolumNum > @num";
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

		//수정된 내용을 DataBase에 Update
		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{			
				BO_HT_Update(conn,tr); //발주원장 수정하는 함수
				
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
			}
		}

		//구매발주원장 수정하는 함수
		private void BO_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			//기존 구매발주원장에서 납입수량,잔량,진행상태를 Update			
			int rowcount = int.Parse(hdRowIndex.Value);

			string str =" UPDATE BO_HT SET " +
				" FirstDeliveryDemandQuantity = @firstorderquantity,FirstDeliveryDemandDate=@firstdate,"+
				" SecondDeliveryDemandQuantity=@secondorderquantity ,SecondDeliveryDemandDate=@seconddate, " +
				" ThirdDeliveryDemandQuantity=@thirdorderquantity ,ThirdDeliveryDemandDate=@thirddate, " +
				" FourthDeliveryDemandQuantity=@fourthorderquantity ,FourthDeliveryDemandDate=@fourthdate, " +
				" FifthDeliveryDemandQuantity=@fifthorderquantity ,FifthDeliveryDemandDate=@fifthdate, " +
				" ApplyUnitCost = @applyunitcost,TotalCost = @totalcost, " +
				" OrderQuantity = @orderquantity, RemainQuantity = @requantity, "+
				" UpdatingPerson = @person,"+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date "+
				"WHERE BuyingOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			//발주원장의 잔량에 기존잔량값 + 기존납품량 - 수정후납품량 을 넣는다.
			comm.Parameters.Add("@firstorderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString());
			comm.Parameters.Add("@firstdate", SqlDbType.DateTime).Value = DateTime.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString());
			comm.Parameters.Add("@secondorderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value.ToString());
						
			if(uwgBO_HT.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value == null || uwgBO_HT.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@seconddate", SqlDbType.DateTime).Value = Convert.DBNull;
			else
			comm.Parameters.Add("@seconddate", SqlDbType.DateTime).Value = DateTime.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value.ToString()).ToShortDateString();
			
			
			comm.Parameters.Add("@thirdorderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value.ToString());
			
			if(uwgBO_HT.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || uwgBO_HT.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@thirddate", SqlDbType.DateTime).Value = Convert.DBNull;
			else
			comm.Parameters.Add("@thirddate", SqlDbType.DateTime).Value = DateTime.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value.ToString()).ToShortDateString();
			
			
			comm.Parameters.Add("@fourthorderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value.ToString());
			
			if(uwgBO_HT.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value == null || uwgBO_HT.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@fourthdate", SqlDbType.DateTime).Value = Convert.DBNull;
			else
			comm.Parameters.Add("@fourthdate", SqlDbType.DateTime).Value = DateTime.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value.ToString()).ToShortDateString();
			
		
			comm.Parameters.Add("@fifthorderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value.ToString());
			
			if(uwgBO_HT.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value == null || uwgBO_HT.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@fifthdate", SqlDbType.DateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@fifthdate", SqlDbType.DateTime).Value = DateTime.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value.ToString()).ToShortDateString();
			
			comm.Parameters.Add("@applyunitcost", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@totalcost", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("TotalCost").Value.ToString());
			
			comm.Parameters.Add("@orderquantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("OrderQuantity").Value.ToString());
			comm.Parameters.Add("@requantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("OrderQuantity").Value.ToString());
		
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = Session["UserName"].ToString();
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgBO_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
						
			comm.ExecuteNonQuery();	
		}

		//완료버튼 클릭
		private void btEnd_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsEnd() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < uwgBO_HT.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( uwgBO_HT.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(uwgBO_HT.Rows[i].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_End( index ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('완료 되었습니다.')</script>");
					
					uwgBO_HT.DataSource = Search();
					uwgBO_HT.DataBind();
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

			for( int i = 0 ; i < uwgBO_HT.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( uwgBO_HT.Rows[i].Cells.FromKey("chk").Value ) 
					&& (uwgBO_HT.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "중단" || uwgBO_HT.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "완료"))
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
				
				string str = "Update BO_HT Set ProgressCondition = '완료', UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where BuyingOrderHistoryIndex = @BuyingOrderHistoryIndex";
				
				cmd.Transaction = con.BeginTransaction();
				cmd.CommandText = str;
				cmd.Parameters.Add("@BuyingOrderHistoryIndex", idx);
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

		private void btEndCancel_Click(object sender, System.EventArgs e)
		{
			if ( this.SelectedRow_IsEndCancle() )
			{
				bool bRowRegist = false;
			
			
				for( int i = 0 ; i < uwgBO_HT.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( uwgBO_HT.Rows[i].Cells.FromKey("chk").Value ))
					{
						int index = int.Parse(uwgBO_HT.Rows[i].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());		// 원장인덱스
					
						DB_EndCancle( index ) ;
					

						bRowRegist = true;
					}
				}
				if ( bRowRegist )
				{
					Response.Write("<script>alert('완료취소 되었습니다.')</script>");
			
					uwgBO_HT.DataSource = Search();
					uwgBO_HT.DataBind();
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

			for( int i = 0 ; i < uwgBO_HT.Rows.Count ; i ++)
			{
				if (Convert.ToBoolean( uwgBO_HT.Rows[i].Cells.FromKey("chk").Value) == true 
					&& uwgBO_HT.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "완료")
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
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			decimal order = 0;
			decimal remain = 0;
			string str  = @"select OrderQuantity,RemainQuantity From BO_HT Where BuyingOrderHistoryIndex = @BuyingOrderHistoryIndex ";
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = str;
			cmd.Connection = con;
			con.Open();
			cmd.Parameters.Add("@BuyingOrderHistoryIndex", idx);
			SqlDataReader dr = cmd.ExecuteReader();
			while(dr.Read())
			{
				order = decimal.Parse(dr["OrderQuantity"].ToString());
				remain = decimal.Parse(dr["RemainQuantity"].ToString());
			}
			con.Close();

			if(order - remain == 0 || order - (order - remain) < 0 )
				str = "Update BO_HT Set ProgressCondition = '대기',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where BuyingOrderHistoryIndex = @BuyingOrderHistoryIndex ";
			else
				str = "Update BO_HT Set ProgressCondition = '진행',UpdatingPerson = @Person, UpdatingPersonID = @PersonID, UpdatingDate = @date  where BuyingOrderHistoryIndex = @BuyingOrderHistoryIndex";
			
			try
			{
				con.Open();
				cmd.Transaction = con.BeginTransaction();
				cmd.CommandText = str;
				cmd.Parameters.Add("@Person", Session["UserName"].ToString());
				cmd.Parameters.Add("@PersonID", Session["ID"].ToString());
				cmd.Parameters.Add("@date", DateTime.Now.ToShortDateString());
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

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = uwgBO_HT;
			bool check = true;
			for(int count = uwgBO_HT.Rows.Count-1 ; count >=0 ; count--)
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
				RegisterStartupScript("","<script>window.open('./Popup/Purchase.aspx','','width=1200, heigth=800,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
							
			}
			else
			{
				uwgBO_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				uwgBO_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
				this.uwgBO_HT.DataSource = Search();
				this.uwgBO_HT.DataBind();
			}
			
			
		}

		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new KIT_ERP.Search("BuyingOrderPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value, ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("BuyingOrderPC",ItemSearchControl1.ItemNum, "", "", wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value, ddlItemClassification1.SelectedItem.Value);

			return search.DataSet_search();
		}
	}
}
