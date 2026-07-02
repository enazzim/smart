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
	/// DataAndWCChanged에 대한 요약 설명입니다.
	/// </summary>
	public class DataAndWCChanged : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected Infragistics.WebUI.WebCombo.WebCombo WebCombo1;
		protected Infragistics.WebUI.WebCombo.WebCombo wcbItemName;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected KIT_ERP.ProductionManagement.ItemSearchControl.ItemSearchControl ItemSearchControl1;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick", "return confirm('선택한 항목을 삭제하시겠습니까?');");
				btnClear.Attributes.Add("onClick", "ResetTextBox();");

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str = "Select ItemNum, ItemDrawNum, ItemName, ItemInfoIndex From II_MT Where RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds1 = new DataSet() ;
				da.Fill(ds1);

				wcbItemName.DataSource = ds1;
				wcbItemName.DataTextField = ds1.Tables[0].Columns[2].ToString();
				wcbItemName.DataValueField = ds1.Tables[0].Columns[2].ToString();
				wcbItemName.DataBind();
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
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
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
						//모든 진행상태가 대기인것만 취소가 가능하다.
						if(Condition(conn,tr,i))
							WDWP_Calcel(conn,tr,i);
						else
							throw new Exception(UltraWebGrid1.Rows[i].Cells.FromKey("ItemNum").Value.ToString() + " 품목과 함께 수립한 품목중 취소할수 없는 상태의 품목이 존재합니다!");
							
						a = 1;
					}
				}
			
				if(a == 0)
				{
					throw new Exception("항목을 선택해주세요");
				}
				else
				{
					RegisterStartupScript("","<script>Cancel();</script>");	

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

		}



		private bool Condition(SqlConnection con,SqlTransaction trans,int a)
		{
			bool progress = true;

			string str = "Select * From WDWP_HT Where ProductionPlanHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[a].Cells.FromKey("ProductionPlanHistoryIndex").Value.ToString());
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				if(dr["ProgressCondition"].ToString() != "대기")
					progress = false;
				break;
			}
			
			return progress;
		}


		/// <summary>
		/// 삭제할 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="a"></param>
		private void WDWP_Calcel(SqlConnection con,SqlTransaction trans,int a)
		{
			
				string str = "Delete From WDWP_HT Where ProductionPlanHistoryIndex = @index";
				SqlCommand comm = new SqlCommand(str,con);
				comm.Transaction = trans;
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[a].Cells.FromKey("ProductionPlanHistoryIndex").Value.ToString());
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				PP_Update(con,trans,a);
		}


		/// <summary>
		/// 생산계획원장 진행상태 업데이트
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="i"></param>
		private void PP_Update(SqlConnection conn,SqlTransaction tr,int i)
		{
			string str = "Update PP_HT Set ProgressCondition = '대기' where ProductionPlanHistoryIndex =  @index";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("ProductionPlanHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

//		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
//		{
//			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
//			UltraWebGrid1.DataSource = Search();
//			UltraWebGrid1.DataBind();
//		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string itemnum = ItemSearchControl1.ItemNum;
			string itemdrawnum = ItemSearchControl1.ItemDrawNum;
			string itemname = ItemSearchControl1.ItemName;
			

			string str = "Select * From WDWP_HT Where ItemNum like @item and ItemDrawNum like @draw and ItemName like @name and ProgressCondition = '대기'";
			SqlCommand comm = new SqlCommand(str,conn);

			if(itemnum == null || itemnum == "")
				comm.Parameters.Add("@item",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@item",SqlDbType.VarChar).Value = "%"+itemnum+"%";
			
			if(itemdrawnum == null || itemdrawnum == "")
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%"+itemdrawnum+"%";

			if(itemname == null || itemname == "")
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+itemname+"%";
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			return ds;
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			wcbItemName.DataValue = "";
			wcbItemName.DisplayValue = "";
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

	}
}
