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

using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// OutsideRequestPC에 대한 요약 설명입니다.
	/// </summary>
	public class OutsideRequestPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected static int Division;	//검색버튼이 눌러졌는지 비디오버튼이 눌러졌는지 파악하는 변수
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Commodity = true;
			if(!Page.IsPostBack)
				this.Volnum();

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
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
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
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new Search("OutsideRequestPC",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,wdcFromDate,wdcToDate);
			else
				search = new Search("OutsideRequestPC",ItemSearchControl1.ItemNum, "", "",wdcFromDate,wdcToDate);
			return search.DataSet_search();
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
			string str_Vol = "Select Max(VolumNum) From OOR_HT";
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
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			vol_Pre();

			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			UltraWebGrid1.DataSource = vol.FindVolum();
			UltraWebGrid1.DataBind();
			Division = 1;
			btnNext.Enabled = true;
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			vol_Now();
			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			UltraWebGrid1.DataSource = vol.FindVolum();
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
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			vol_Next();
			
			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			UltraWebGrid1.DataSource = vol.FindVolum();
			UltraWebGrid1.DataBind();
			Division = 1;
			btnPre.Enabled = true;
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From OOR_HT where VolumNum < @num";
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From OOR_HT";
							
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
			comm.CommandText = "Select Min(VolumNum) as VolumNum From OOR_HT where VolumNum > @num";
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
		/// 페이지 이동
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
				VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
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
				RegisterStartupScript("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg = UltraWebGrid1;
				uwg.DisplayLayout.Pager.AllowPaging = false;
				if(Division == 0)
				{
					uwg.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
					uwg.DataSource = vol.FindVolum();
				}

				uwg.DataBind();
				UltraWebGridExcelExporter1.Export(uwg);

			}
		}
	}
}
