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
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ProductionRequest에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionRequest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList dl_Source;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button bt_Cancel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Stop;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button btnNext;


		private DataSet ds;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Total;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_SourceCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Source;
		protected System.Web.UI.WebControls.DropDownList dl_ProgressState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		
		private static int Division;
	
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
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");
				bt_Stop.Attributes.Add("onClick", "return OK('선택한 항목을 중단');");
				bt_Cancel.Attributes.Add("onClick", "return OK('취소');");
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				
				//	*************************************************
				//	**  진행상태 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				
				string str_Condition = "Select DISTINCT ProgressCondition from PR_HT";
				SqlCommand comm_Condition = new SqlCommand(str_Condition,conn);
				SqlDataAdapter da_Condition = new SqlDataAdapter(comm_Condition) ;
				DataSet ds_Condition = new DataSet() ;
				da_Condition.Fill(ds_Condition);
				
				dl_ProgressState.DataSource = ds_Condition;
				dl_ProgressState.DataTextField = ds_Condition.Tables[0].Columns[0].ToString();
				dl_ProgressState.DataValueField = ds_Condition.Tables[0].Columns[0].ToString();
				dl_ProgressState.DataBind();
				dl_ProgressState.Items.Insert(0, "-선택-") ;
				dl_ProgressState.Items[0].Value = "";

				//	*************************************************
				//	**  생산의뢰원천 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//코드분류표에서 대분류명이 생산의뢰원천인 소분류명을 가지고 옴.
				string str_Source = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '생산의뢰원천'" ;
				SqlCommand comm_Source = new SqlCommand(str_Source,conn);
				SqlDataAdapter da_Source = new SqlDataAdapter(comm_Source) ;
				DataSet ds_Source = new DataSet() ;
				da_Source.Fill(ds_Source);
				
				dl_Source.DataSource = ds_Source;
				dl_Source.DataTextField = ds_Source.Tables[0].Columns[0].ToString();
				dl_Source.DataValueField = ds_Source.Tables[0].Columns[1].ToString();
				dl_Source.DataBind();
				dl_Source.Items.Insert(0, "-선택-") ;
				dl_Source.Items[0].Value = "";

				conn.Close();

				///////////////////////////////////////
				// 비디오버튼을 위해
				// lb_vol에 볼륨번호 최고값을 넣어둔다.
				//////////////////////////////////////
				
				bt_Cancel.Enabled = false;
				this.Volnum();
			}
			if(UltraWebGrid1.Rows.Count == 0)
				bt_Cancel.Enabled = false;
				
			
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			Division = 0;
			bt_Cancel.Enabled = false;
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"ProductionRequestPC");
			del.MainRowDelete();

			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton video = new VideoButton(UltraWebGrid1,"ProductionRequestPC", Volum.Value);
				UltraWebGrid1.DataSource = video.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 중단버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Stop_Click(object sender, System.EventArgs e)
		{
			Stop stop = new Stop(UltraWebGrid1,"ProductionRequestPC",Session["ID"].ToString());
			stop.MainTableStop();
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton video = new VideoButton(UltraWebGrid1,"ProductionRequestPC", Volum.Value);
				UltraWebGrid1.DataSource = video.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 취소버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Cancel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				RegisterStartupScript("","<script>alert('취소할 항목이 없습니다!');</script>");
			}
			else
			{
				UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
				VideoButton aa = new VideoButton(UltraWebGrid1,"ProductionRequestPC", Volum.Value);
				UltraWebGrid1.DataSource = aa.FindVolum();
				UltraWebGrid1.DataBind();
				// 취소객체 생성
				KIT_ERP.Cancel cancle = new KIT_ERP.Cancel(UltraWebGrid1, "ProductionRequestPC", Volum.Value);
				cancle.MainRowCancel();
				Volnum();
				UltraWebGrid1.DisplayLayout.Pager.AllowPaging = true;
				VideoButton video = new VideoButton(UltraWebGrid1,"ProductionRequestPC", Volum.Value);
				UltraWebGrid1.DataSource = video.FindVolum();
				UltraWebGrid1.DataBind();
			}
		}



		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{	
			
			Search aa;
			if(ItemSearchControl1.hdItem.Trim() =="")
				aa = new Search("ProductionRequestPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, dl_ProgressState.SelectedItem.Value,dl_Source.SelectedItem.Value);
			else
				aa = new Search("ProductionRequestPC",ItemSearchControl1.ItemNum,"","",wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, dl_ProgressState.SelectedItem.Value,dl_Source.SelectedItem.Value);
			return aa.DataSet_search();
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
			string str_Vol = "Select Max(VolumNum) From PR_HT";
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
			bt_Cancel.Enabled = true;
			vol_Pre();

			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			VideoButton vol = new VideoButton(UltraWebGrid1,"ProductionRequestPC",Volum.Value);
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
			bt_Cancel.Enabled = true;
			vol_Now();
			VideoButton vol = new VideoButton(UltraWebGrid1,"ProductionRequestPC",Volum.Value);
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
			bt_Cancel.Enabled = true;
			vol_Next();
			
			VideoButton vol = new VideoButton(UltraWebGrid1,"ProductionRequestPC",Volum.Value);
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From PR_HT where VolumNum < @num";
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From PR_HT";
							
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Volum.Value = dr["VolumNum"].ToString();
			dr.Close();
			
			if(Volum.Value == "")
			{
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!')</script>");
				Volum.Value = "0";
				bt_Cancel.Enabled = false;
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
			comm.CommandText = "Select Min(VolumNum) as VolumNum From PR_HT where VolumNum > @num";
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
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				UltraWebGrid uwg_PR = new UltraWebGrid();
				uwg_PR = UltraWebGrid1;
				uwg_PR.DisplayLayout.Pager.AllowPaging = false;
				uwg_PR.Columns.FromKey("chk").Hidden = true;
				if(Division == 0)
				{
					uwg_PR.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(UltraWebGrid1,"ProductionRequestPC",Volum.Value);
					uwg_PR.DataSource = vol.FindVolum();
				}

				uwg_PR.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_PR);

			}
			
		}

		/// <summary>
		/// 페이지이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"ProductionRequestPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 수정편집창을 닫을때 DB로 업데이트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();

			arr.Add(decimal.Parse(lb_Total.Value));//총의뢰량
			arr.Add(lb_SourceCode.Value);//의뢰원천코드
			arr.Add(lb_Source.Value);//의뢰원천
			arr.Add(int.Parse(lb_RowSelectIndex.Value));

			Update up = new Update("ProductionRequestPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();
			
			
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton video = new VideoButton(UltraWebGrid1,"ProductionRequestPC", Volum.Value);
				UltraWebGrid1.DataSource = video.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		

		protected DataSet ProSource
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Source = source.ProSource();				
				return ds_Source;
			}
		}
		
	}
}
