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
using Infragistics.WebUI.UltraWebGrid;

namespace hanaro.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WorkOrder에 대한 요약 설명입니다.
	/// </summary>
	public class WorkOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label33;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcWorkDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcWorkDate1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcWorkDate2;
		protected System.Web.UI.HtmlControls.HtmlInputText PlanQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText PlanQuantity1;
		protected System.Web.UI.HtmlControls.HtmlInputText PlanQuantity2;
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
				Response.Write("<script>window.open('WorkOrder.aspx','','width=710,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
			}
		}


		protected void first()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime";

			SqlCommand Cmd = new SqlCommand(str, conn);
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label8.Text = dr["Mainworker"].ToString();											//작업자
				Label9.Text = Number(dr["StandardTime"].ToString());										//표준시간
			}
			dr.Close();
			conn.Close();

			wdcWorkDate.Value = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();	
			Label5.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label6.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label7.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity.Value =	Number(uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label11.Text =	Number(uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량	
			
			uwg.Rows[0].Delete();
		}

		protected void second()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime";

			SqlCommand Cmd = new SqlCommand(str, conn);
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label8.Text = dr["Mainworker"].ToString();											//작업자
				Label9.Text = Number(dr["StandardTime"].ToString());										//표준시간
			}
			dr.Close();
			conn.Close();

			wdcWorkDate.Value = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();	
			Label5.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label6.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label7.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity.Value =	Number(uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label11.Text =	Number(uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량	
			
			

			//두번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ProcessCode").ToString();
			conn.Open();
			SqlDataReader dr1 = Cmd.ExecuteReader();
			while(dr1.Read())
			{
				Label19.Text = dr1["Mainworker"].ToString();											//작업자
				Label20.Text = Number(dr1["StandardTime"].ToString());										//표준시간
			}
			dr1.Close();
			conn.Close();

			wdcWorkDate1.Value = DateTime.Now.ToShortDateString();										//지시일자
			Label14.Text = uwg.Rows[1].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label15.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label16.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();						//품목명
			Label17.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();						//공정명
			Label18.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity1.Value =	Number(uwg.Rows[1].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label22.Text =	Number(uwg.Rows[1].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량
					
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();		
		}

		protected void third()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = @"select StandardTime,MainWorker,Min(PriorityOrder) as PriorityOrder from wsi_mt 
							where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode= @ProcessCode
							group by Mainworker,StandardTime";

			SqlCommand Cmd = new SqlCommand(str, conn);
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[0].Cells.FromKey("ProcessCode").ToString();
			
			conn.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				Label8.Text = dr["Mainworker"].ToString();											//작업자
				Label9.Text = Number(dr["StandardTime"].ToString());										//표준시간
			}
			dr.Close();
			conn.Close();

			wdcWorkDate.Value = DateTime.Now.ToShortDateString();											//지시일자
			Label3.Text = uwg.Rows[0].Cells.FromKey("ProductItemNum").ToString();
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();	
			Label5.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();						//품목명
			Label6.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();						//공정명
			Label7.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity.Value =	Number(uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label11.Text =	Number(uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량	
			
			

			//두번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[1].Cells.FromKey("ProcessCode").ToString();
			conn.Open();
			SqlDataReader dr1 = Cmd.ExecuteReader();
			while(dr1.Read())
			{
				Label19.Text = dr1["Mainworker"].ToString();											//작업자
				Label20.Text = Number(dr1["StandardTime"].ToString());										//표준시간
			}
			dr1.Close();
			conn.Close();

			wdcWorkDate1.Value = DateTime.Now.ToShortDateString();										//지시일자
			Label14.Text = uwg.Rows[1].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label15.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label16.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();						//품목명
			Label17.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();						//공정명
			Label18.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity1.Value =	Number(uwg.Rows[1].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label22.Text =	Number(uwg.Rows[1].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량
			

			//세번째 선택된 항목의 값을 레이블에 바인딩
			Cmd.Parameters.Clear();
			Cmd.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
			Cmd.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = uwg.Rows[2].Cells.FromKey("ProcessCode").ToString();

			conn.Open();
			SqlDataReader dr2 = Cmd.ExecuteReader();
			while(dr2.Read())
			{
				Label30.Text = dr2["Mainworker"].ToString();											//작업자
				Label31.Text = Number(dr2["StandardTime"].ToString());										//표준시간
			}
			dr2.Close();
			conn.Close();
			wdcWorkDate2.Value = DateTime.Now.ToShortDateString();										//지시일자
			Label25.Text = uwg.Rows[2].Cells.FromKey("ProductItemNum").ToString();					//제품번호
			Label26.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();							//품목번호
			Label27.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();						//품목명
			Label28.Text = uwg.Rows[2].Cells.FromKey("ProcessName").ToString();						//공정명
			Label29.Text = uwg.Rows[2].Cells.FromKey("WCName").Text;								//작업장명
			PlanQuantity2.Value =	Number(uwg.Rows[2].Cells.FromKey("WorkPlanQuantity").ToString());				//작업계획수량
			Label33.Text =	Number(uwg.Rows[2].Cells.FromKey("WorkCompletionQuantity").ToString());			//이전완료수량
					
			uwg.Rows[2].Delete();
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();
		}


		//수량 콤마찍기
		private string Number(string Quantity)
		{
			decimal Qnt = Math.Round(decimal.Parse(Quantity),0);
			string Qnt1 = Qnt.ToString();
			for(int i=Qnt1.Length-1,j=0;i>=0;i--,j++)
			{
				if(j!=0&&(j%3)==0) 
				{ 
					Qnt1 = Qnt1.Insert(i+1,",");
					i--;
					j++;
				}
			}

			return Qnt1;
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
