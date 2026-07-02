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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WorkOrder_Pop에 대한 요약 설명입니다.
	/// </summary>
	public class WorkOrder_Pop : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label54;
		protected System.Web.UI.WebControls.Label Label55;
		protected System.Web.UI.WebControls.Label Label56;
		protected System.Web.UI.WebControls.Label Label57;
		protected System.Web.UI.WebControls.Label Label58;
		protected System.Web.UI.WebControls.Label Label59;
		protected System.Web.UI.WebControls.Label Label60;
		protected System.Web.UI.WebControls.Label Label61;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{

			//세션에 저장된 그리드를 웹그리드 변수에 저장
			uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
						
			//그리드를 반복하면서 체크되지 않는 항목은 지운다.
			for(int i = uwg.Rows.Count-1; i >= 0;i--)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(uwg.Rows[i].Cells.FromKey("chk").Value == null)
				{
					uwg.Rows[i].Cells.FromKey("chk").Value = false;
					uwg.Rows[i].Delete();
				}				

			}			

			//체크된 항목중에 3개까지 한페이지에 바인딩 식별위한 변수
			int cnt = uwg.Rows.Count;

			
			//체크된 항목에 값을 읽어 레이블에 값을 체운다.
			//첫번째 선택된 값을 항목에 채움
			if(cnt == 1)
			{
				first();
			}
				//두번째 선택된 값을 항목에 채움
			else if(cnt == 2)
			{
				second();				
			}
				//세번째 선택된 값을 항목에 채움
			else if(cnt == 3)
			{
				third();
				
			}
			else
			{
				third();
				//Response.Write("<script>window.open('WorkOrder_Pop.aspx','','');</script>");
				Response.Write("<script>window.open('WorkOrder_Pop.aspx','','width=710,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
			}
		}

		protected void first()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,ToolName1,ToolName2,JigName1,JigName2,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime,ToolName1,ToolName2,JigName1,JigName2";

			SqlCommand Cmd = new SqlCommand(str, conn);
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label9.Text = dr["Mainworker"].ToString();																			//작업자
				Label5.Text = dr["StandardTime"].ToString();											//표준시간
				Label12.Text = dr["ToolName1"].ToString();												//사용공구1
				Label7.Text = dr["JigName1"].ToString();												//사용치구1
				Label6.Text = dr["ToolName2"].ToString();												//사용공구2
				Label13.Text = dr["JigName2"].ToString();												//사용치구2
			}
			dr.Close();
			conn.Close();

			Label2.Text = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label11.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label14.Text =	uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label19.Text =	uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량
			Label1.Text = Etc(uwg.Rows[0].Cells.FromKey("ItemNum").ToString(), uwg.Rows[0].Cells.FromKey("ProcessCode").ToString());
			
			uwg.Rows[0].Delete();
		}

		protected void second()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,ToolName1,ToolName2,JigName1,JigName2,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime,ToolName1,ToolName2,JigName1,JigName2";

			SqlCommand Cmd = new SqlCommand(str,conn);			
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label9.Text = dr["Mainworker"].ToString();																			//작업자
				Label5.Text = dr["StandardTime"].ToString();											//표준시간
				Label12.Text = dr["ToolName1"].ToString();												//사용공구1
				Label7.Text = dr["JigName1"].ToString();												//사용치구1
				Label6.Text = dr["ToolName2"].ToString();												//사용공구2
				Label13.Text = dr["JigName2"].ToString();												//사용치구2
			}
			dr.Close();
			conn.Close();

			Label2.Text = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label11.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label14.Text =	uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label19.Text =	uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량	
			Label1.Text = Etc(uwg.Rows[0].Cells.FromKey("ItemNum").ToString(), uwg.Rows[0].Cells.FromKey("ProcessCode").ToString());

			

			//두번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ProcessCode").ToString();
			conn.Open();
			SqlDataReader dr1 = Cmd.ExecuteReader();
			while(dr1.Read())
			{
				Label31.Text = dr1["Mainworker"].ToString();											//작업자
				Label35.Text = dr1["StandardTime"].ToString();											//표준시간
				Label36.Text = dr1["ToolName1"].ToString();												//사용공구1
				Label37.Text = dr1["JigName1"].ToString();												//사용치구1
				Label38.Text = dr1["ToolName2"].ToString();												//사용공구2
				Label39.Text = dr1["JigName2"].ToString();												//사용치구2
			}
			dr1.Close();
			conn.Close();

			Label28.Text = DateTime.Now.ToShortDateString();										//지시일자
			Label29.Text = uwg.Rows[1].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label30.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;								//작업장명
			Label32.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label33.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();						//품목명
			Label34.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();						//공정명
			Label40.Text =	uwg.Rows[1].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label41.Text =	uwg.Rows[1].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량
			Label15.Text = Etc(uwg.Rows[1].Cells.FromKey("ItemNum").ToString(), uwg.Rows[1].Cells.FromKey("ProcessCode").ToString());
					
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();		
		}

		protected void third()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,ToolName1,ToolName2,JigName1,JigName2,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime,ToolName1,ToolName2,JigName1,JigName2";

			SqlCommand Cmd = new SqlCommand(str,conn);			
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label9.Text = dr["Mainworker"].ToString();																			//작업자
				Label5.Text = dr["StandardTime"].ToString();											//표준시간
				Label12.Text = dr["ToolName1"].ToString();												//사용공구1
				Label7.Text = dr["JigName1"].ToString();												//사용치구1
				Label6.Text = dr["ToolName2"].ToString();												//사용공구2
				Label13.Text = dr["JigName2"].ToString();												//사용치구2
			}
			dr.Close();
			conn.Close();

			Label2.Text = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label11.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label14.Text =	uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label19.Text =	uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량	
			Label1.Text = Etc(uwg.Rows[0].Cells.FromKey("ItemNum").ToString(), uwg.Rows[0].Cells.FromKey("ProcessCode").ToString());

			

			//두번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ProcessCode").ToString();
			conn.Open();
			SqlDataReader dr1 = Cmd.ExecuteReader();
			while(dr1.Read())
			{
				Label31.Text = dr1["Mainworker"].ToString();											//작업자
				Label35.Text = dr1["StandardTime"].ToString();											//표준시간
				Label36.Text = dr1["ToolName1"].ToString();												//사용공구1
				Label37.Text = dr1["JigName1"].ToString();												//사용치구1
				Label38.Text = dr1["ToolName2"].ToString();												//사용공구2
				Label39.Text = dr1["JigName2"].ToString();												//사용치구2
			}
			dr1.Close();
			conn.Close();

			Label28.Text = DateTime.Now.ToShortDateString();										//지시일자
			Label29.Text = uwg.Rows[1].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label30.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;								//작업장명
			Label32.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label33.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();						//품목명
			Label34.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();						//공정명
			Label40.Text =	uwg.Rows[1].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label41.Text =	uwg.Rows[1].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량
			Label15.Text = Etc(uwg.Rows[1].Cells.FromKey("ItemNum").ToString(), uwg.Rows[1].Cells.FromKey("ProcessCode").ToString());
					

			

			//세번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[2].Cells.FromKey("ProcessCode").ToString();

			conn.Open();
			SqlDataReader dr2 = Cmd.ExecuteReader();
			while(dr2.Read())
			{
				Label57.Text = dr2["Mainworker"].ToString();											//작업자
				Label59.Text = dr2["StandardTime"].ToString();											//표준시간
				Label64.Text = dr2["ToolName1"].ToString();												//사용공구1
				Label63.Text = dr2["JigName1"].ToString();												//사용치구1
				Label62.Text = dr2["ToolName2"].ToString();												//사용공구2
				Label65.Text = dr2["JigName2"].ToString();												//사용치구2
			}
			dr2.Close();
			conn.Close();
			Label54.Text = DateTime.Now.ToShortDateString();										//지시일자
			Label55.Text = uwg.Rows[2].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label56.Text = uwg.Rows[2].Cells.FromKey("WCName").Text;								//작업장명
			Label58.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label61.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();						//품목명
			Label60.Text = uwg.Rows[2].Cells.FromKey("ProcessName").ToString();						//공정명
			Label66.Text =	uwg.Rows[2].Cells.FromKey("WorkPlanQuantity").ToString();				//작업계획수량
			Label75.Text =	uwg.Rows[2].Cells.FromKey("WorkCompletionQuantity").ToString();			//이전완료수량
			Label16.Text = Etc(uwg.Rows[2].Cells.FromKey("ItemNum").ToString(), uwg.Rows[2].Cells.FromKey("ProcessCode").ToString());
					
			uwg.Rows[2].Delete();
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();
		}


		private string Etc(string ItemNum, string Code)
		{
			string EtcText = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select EtcText From PSI_MT where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode";

			SqlCommand Cmd = new SqlCommand(str,conn);			
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				EtcText = dr["EtcText"].ToString();
			}
			dr.Close();
			conn.Close();

			return EtcText;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
