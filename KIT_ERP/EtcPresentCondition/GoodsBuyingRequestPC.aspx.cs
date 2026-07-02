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


namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// GoodsBuyingRequestPC에 대한 요약 설명입니다.
	/// </summary>
	public class Table1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Label lb_Vol;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgRO;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList dlProgress;
		protected System.Web.UI.WebControls.DropDownList dlSource;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected DataSet ds;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		private static int Division;//(삭제나 수정시 어느버튼이 눌려졌는지에 따라 검색객체와 비디오버튼 객체를 부른다
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				//	*************************************************
				//	**  진행상태 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
				string str_Condition = "Select DISTINCT ProgressCondition from BR_HT";
				SqlCommand comm_Condition = new SqlCommand(str_Condition,conn);
				SqlDataAdapter da_Condition = new SqlDataAdapter(comm_Condition) ;
				DataSet ds_Condition = new DataSet() ;
				da_Condition.Fill(ds_Condition);
				
				dlProgress.DataSource = ds_Condition;
				dlProgress.DataTextField = ds_Condition.Tables[0].Columns[0].ToString();
				dlProgress.DataValueField = ds_Condition.Tables[0].Columns[0].ToString();
				dlProgress.DataBind();
				dlProgress.Items.Insert(0, "-선택-") ;
				dlProgress.Items[0].Value = "";

				//	*****************************************************
				//	**  구매의뢰원천 드롭다운리스트... 데이타바인딩... **
				//	*****************************************************
				//코드분류표에서 대분류명이 생산의뢰원천인 소분류명을 가지고 옴.
				string str_Source = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '구매의뢰원천' order by SmallClassificationName";
				SqlCommand comm_Source = new SqlCommand(str_Source,conn);
				SqlDataAdapter da_Source = new SqlDataAdapter(comm_Source) ;
				DataSet ds_Source = new DataSet() ;
				da_Source.Fill(ds_Source);
				
				dlSource.DataSource = ds_Source;
				dlSource.DataTextField = ds_Source.Tables[0].Columns[0].ToString();
				dlSource.DataValueField = ds_Source.Tables[0].Columns[1].ToString();
				dlSource.DataBind();
				dlSource.Items.Insert(0, "-선택-") ;
				dlSource.Items[0].Value = "";

				conn.Close();

				Volnum();
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.uwgRO.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgRO_PageIndexChanged_1);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.ID = "Table1";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			uwgRO.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgRO.DataSource = Search();
			uwgRO.DataBind();

			Division = 0;
		}

		private DataSet Search()
		{
			Search aa;
			if(ItemSearchControl1.hdItem.Trim() == "")
				aa = new Search("GoodsBuyingRequestPC",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,dlProgress.SelectedItem.Value,dlSource.SelectedItem.Value,wdcStartDate,wdcEndDate);
			else
				aa = new Search("GoodsBuyingRequestPC",ItemSearchControl1.ItemNum, "", "",dlProgress.SelectedItem.Value,dlSource.SelectedItem.Value,wdcStartDate,wdcEndDate);
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
			// lb_vol에 볼륨번호 최고값을 넣어둔다.
			//////////////////////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();	
			string str_Vol = "Select Max(VolumNum) From BR_HT where PropertyClassification = '상품'";
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
			uwgRO.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgRO.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			
			vol_Pre();

			VideoButton vol = new VideoButton(uwgRO,"GoodsBuyingRequestPC",Volum.Value);
			ds = vol.FindVolum();
			uwgRO.DataSource = ds;
			uwgRO.DataBind();
			Division = 1;
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			uwgRO.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgRO.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			
			vol_Now();
			VideoButton vol = new VideoButton(uwgRO,"GoodsBuyingRequestPC",Volum.Value);
			ds = vol.FindVolum();
			uwgRO.DataSource = ds;
			uwgRO.DataBind();
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
			uwgRO.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgRO.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			
			vol_Next();
			
			VideoButton vol = new VideoButton(uwgRO,"GoodsBuyingRequestPC",Volum.Value);
			ds = vol.FindVolum();
			uwgRO.DataSource = ds;
			uwgRO.DataBind();
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From BR_HT where VolumNum < @num and PropertyClassification = '상품'";
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From BR_HT where PropertyClassification = '상품'";
							
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
			comm.CommandText = "Select Min(VolumNum) as VolumNum From BR_HT where VolumNum > @num and PropertyClassification = '상품'";
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
			if(uwgRO.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				UltraWebGrid uwg_RO = new UltraWebGrid();
				uwg_RO = uwgRO;
				uwg_RO.DisplayLayout.Pager.AllowPaging = false;
				if(Division == 0)
				{
					uwg_RO.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(uwgRO,"GoodsBuyingRequestPC",Volum.Value);
					uwg_RO.DataSource = vol.FindVolum();
				}

				uwg_RO.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_RO);

			}
		}


		/// <summary>
		/// 페이지이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void uwgRO_PageIndexChanged_1(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgRO.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(Division == 0)
			{
				uwgRO.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(uwgRO,"GoodsBuyingRequestPC",Volum.Value);
				uwgRO.DataSource = vol.FindVolum();
			}
			uwgRO.DataBind();
		}
	}
}
