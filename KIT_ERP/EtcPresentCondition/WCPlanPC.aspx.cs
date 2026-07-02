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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// WCPlanPC에 대한 요약 설명입니다.
	/// </summary>
	public class WCPlanPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btExcel;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected int Division;
		protected DataSet ds;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
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
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex =1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
		}


		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
                search = new Search("WCPlanPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,dlProgress.SelectedItem.Text,wdcFromDate,wdcToDate,wdcDeliveryFromDate,wdcDeliveryToDate,wdcBeginDate,wdcEndDate, ddlItemGroup1.SelectedItem.Value.ToString(), ddlItemGroup2.SelectedItem.Value.ToString());
			else
				search = new Search("WCPlanPC",ItemSearchControl1.ItemNum, "", "",dlProgress.SelectedItem.Text,wdcFromDate,wdcToDate,wdcDeliveryFromDate,wdcDeliveryToDate,wdcBeginDate,wdcEndDate, ddlItemGroup1.SelectedItem.Value.ToString(), ddlItemGroup2.SelectedItem.Value.ToString());
			return  search.DataSet_search();
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

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}		
	}
}

