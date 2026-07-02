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

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WCChange에 대한 요약 설명입니다.
	/// </summary>
	public class WCChange : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcWorkDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.WebControls.Button btChange;
		protected System.Web.UI.WebControls.Button btStop;
		protected System.Web.UI.WebControls.Button btWorkOrder;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnCancel;
		DataSet ds_WorkOrder = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				btChange.Attributes.Add("onClick", "return confirm('선택한 작업의 작업구분을 변경 하시겠습니까?');");
				btStop.Attributes.Add("onClick", "return confirm('선택한 작업을 중단 하시겠습니까?');");

				wdcWorkDate.NullDateLabel = DateTime.Now.ToShortDateString();

				page_load();			
				
			}
			else
				ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			
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
			this.btChange.Click += new System.EventHandler(this.btChange_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			this.btWorkOrder.Click += new System.EventHandler(this.btWorkOrder_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void page_load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string WCName = Request.QueryString["WCName"];

			string str = "";
			if(WCName != "외 주")
			{
				str = @"Select * From WDWP_HT Where WCName = @wcname and WorkDistinction = '자가' and (ProgressCondition = '진행' or ProgressCondition = '지시' or ProgressCondition = '대기')";
			}
			else
			{
				str = @"Select * From WDWP_HT Where WorkDistinction = '외주' and (ProgressCondition = '진행' or ProgressCondition = '지시' or ProgressCondition = '대기')";
			}
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@wcname",SqlDbType.VarChar).Value = WCName;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds_WorkOrder = new DataSet();
			da.Fill(ds_WorkOrder);

            UltraWebGrid1.DataSource = ds_WorkOrder;
			UltraWebGrid1.DataBind();

			Session["ds_WorkOrder"] = ds_WorkOrder;
		}


		/// <summary>
		/// 작업지시 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btWorkOrder_Click(object sender, System.EventArgs e)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{
		
				int a = 0;
				for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						WDWP_Update(conn,tr,i);

						//RegisterStartupScript("","<script>window.open('WorkOrder.aspx?WCDailyWorkPlanHistoryIndex=" + UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value + "','작업지시','width=700,height=500px,resizable=yes');</script>");
						a = 1;
					}
				}
			
				if(a == 0)
				{
					RegisterStartupScript("","<script>alert('항목을 선택해 주세요!');</script>");
				}
				else
				{
					RegisterStartupScript("","<script>Order();</script>");	
				}
				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"')</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
			
		}


		/// <summary>
		/// 해당 WC원장에 Update하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="a"></param>
		private void WDWP_Update(SqlConnection con, SqlTransaction trans,int a)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			if(ds_WorkOrder.Tables[0].Rows[a]["ProgressCondition"].ToString() != "진행")
			{

				string str = @"Update WDWP_HT Set WorkDate = @date,ProgressCondition = '지시' where WCDailyWorkPlanHistoryIndex = @index";
				SqlCommand comm = new SqlCommand(str,con);
				comm.Transaction = trans;
				comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Parse(wdcWorkDate.Text).ToShortDateString();
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(ds_WorkOrder.Tables[0].Rows[a]["WCDailyWorkPlanHistoryIndex"].ToString());
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}


		/// <summary>
		/// 작업구분변경버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btChange_Click(object sender, System.EventArgs e)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{
				int a = 0;
				for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
					
						Change(conn,tr,i);
						a = 1;
					}
				}
			
				if(a == 0)
				{
					RegisterStartupScript("","<script>alert('항목을 선택해 주세요!');</script>");

				}
				else 
				{
					RegisterStartupScript("","<script>Change();</script>");	
				}

				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"')</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}
		
		
		/// <summary>
		/// 작업구분 변경 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="a"></param>
		private void Change(SqlConnection con, SqlTransaction trans, int a)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			if(ds_WorkOrder.Tables[0].Rows[a]["ProgressCondition"].ToString() == "대기")
			{
				string str = "";
				if(ds_WorkOrder.Tables[0].Rows[a]["WorkDistinction"].ToString() == "자가")
					str = "Update WDWP_HT Set WorkDistinction = '외주'  where WCDailyWorkPlanHistoryIndex = @index";
				else
					str = "Update WDWP_HT Set WorkDistinction = '자가'  where WCDailyWorkPlanHistoryIndex = @index";

				SqlCommand comm = new SqlCommand(str,con);
				comm.Transaction = trans;
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(ds_WorkOrder.Tables[0].Rows[a]["WCDailyWorkPlanHistoryIndex"].ToString());
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				throw new Exception("대기만 변경이 가능합니다!");
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}

		/// <summary>
		/// 중단버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btStop_Click(object sender, System.EventArgs e)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{
		
				int a = 0;
				for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						Stop(conn,tr,i);
						a = 1;
					}
				}

				if(a == 0)
				{
					RegisterStartupScript("","<script>alert('항목을 선택해 주세요!');</script>");
				}
				else 
				{
					RegisterStartupScript("","<script>Stop();</script>");	
				}

				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"')</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		    
			
			
			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}

		/// <summary>
		/// 중단함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="a"></param>
		private void Stop(SqlConnection con, SqlTransaction trans, int a)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			string str = "Update WDWP_HT Set ProgressCondition = '중단'  where WCDailyWorkPlanHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(ds_WorkOrder.Tables[0].Rows[a]["WCDailyWorkPlanHistoryIndex"].ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}
		
		/// <summary>
		/// 지시된 작업 취소
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{
				int a = 0;
				for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
					
						Cancel(conn,tr,i);
						a = 1;
					}
				}
			
				if(a == 0)
				{
					RegisterStartupScript("","<script>alert('항목을 선택해 주세요!');</script>");

				}
				else 
				{
					RegisterStartupScript("","<script>Order();</script>");	
				}

				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"')</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}


		// <summary>
		/// 작업구분 변경 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="a"></param>
		private void Cancel(SqlConnection con, SqlTransaction trans, int a)
		{
			ds_WorkOrder = (DataSet)Session["ds_WorkOrder"];

			if(ds_WorkOrder.Tables[0].Rows[a]["ProgressCondition"].ToString() == "지시")
			{
				string str = "Update WDWP_HT Set ProgressCondition = '대기'  where WCDailyWorkPlanHistoryIndex = @index";
				
				SqlCommand comm = new SqlCommand(str,con);
				comm.Transaction = trans;
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(ds_WorkOrder.Tables[0].Rows[a]["WCDailyWorkPlanHistoryIndex"].ToString());
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				throw new Exception("지시인것만이 취소가 가능합니다!");
			}

			Session["ds_WorkOrder"] = ds_WorkOrder ;
		}
	}
}
