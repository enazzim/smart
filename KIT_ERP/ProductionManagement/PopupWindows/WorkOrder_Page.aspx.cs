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

namespace mlim_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WorkOrder_Page에 대한 요약 설명입니다.
	/// </summary>
	public class WorkOrder_Page : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label101;
		protected System.Web.UI.WebControls.Label Label100;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
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

			SetFill();

			if(cnt == 1)
			{
				first();
				Session.Remove("Grid");
			}
				//두번째 선택된 값을 항목에 채움
			else if(cnt == 2)
			{
				if(uwg.Rows[0].Cells.FromKey("ItemNum").Text == uwg.Rows[1].Cells.FromKey("ItemNum").Text)
				{
					second();
					Session.Remove("Grid");
				}
				else
				{
					Session.Remove("Grid");
					RegisterClientScriptBlock("","<script>test();</script>");
				}

			}
				//세번째 선택된 값을 항목에 채움
			else if(cnt == 3)
			{
				if(uwg.Rows[0].Cells.FromKey("ItemNum").Text == uwg.Rows[1].Cells.FromKey("ItemNum").Text)
				{
					if(uwg.Rows[1].Cells.FromKey("ItemNum").Text == uwg.Rows[2].Cells.FromKey("ItemNum").Text)
					{
						third();
						Session.Remove("Grid");
					}
					else
					{
						Session.Remove("Grid");
						RegisterClientScriptBlock("","<script>test();</script>");
					}
				}
				else
				{
					Session.Remove("Grid");
					RegisterClientScriptBlock("","<script>test();</script>");
				}

				
			}
			else if(cnt == 4)
			{
				if(uwg.Rows[0].Cells.FromKey("ItemNum").Text == uwg.Rows[1].Cells.FromKey("ItemNum").Text)
				{
					if(uwg.Rows[1].Cells.FromKey("ItemNum").Text == uwg.Rows[2].Cells.FromKey("ItemNum").Text)
					{
						if(uwg.Rows[2].Cells.FromKey("ItemNum").Text == uwg.Rows[3].Cells.FromKey("ItemNum").Text)
						{
							fourth();
							Session.Remove("Grid");
						}
						else
						{
							Session.Remove("Grid");
							RegisterClientScriptBlock("","<script>test();</script>");
						}
					}
					else
					{
						Session.Remove("Grid");
						RegisterClientScriptBlock("","<script>test();</script>");
					}
				}
				else
				{
					Session.Remove("Grid");
					RegisterClientScriptBlock("","<script>test();</script>");
				}
				
			}
			else
			{
				if(uwg.Rows[0].Cells.FromKey("ItemNum").Text == uwg.Rows[1].Cells.FromKey("ItemNum").Text)
				{
					if(uwg.Rows[1].Cells.FromKey("ItemNum").Text == uwg.Rows[2].Cells.FromKey("ItemNum").Text)
					{
						if(uwg.Rows[2].Cells.FromKey("ItemNum").Text == uwg.Rows[3].Cells.FromKey("ItemNum").Text)
						{
							fourth();
							Response.Write("<script>window.open('WorkOrder_Page.aspx','','width=710,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
						}
						else
						{
							Session.Remove("Grid");
							RegisterClientScriptBlock("","<script>test();</script>");
						}
					}
					else
					{
						Session.Remove("Grid");
						RegisterClientScriptBlock("","<script>test();</script>");
					}
				}
				else
				{
					Session.Remove("Grid");
					RegisterClientScriptBlock("","<script>test();</script>");
				}
				
			}
		}

		private void SetFill()
		{
			Label22.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = @"Select SmallClassificationName, Standard From II_MT inner join PUC_MT on ItemClassification1 = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			comm.Parameters.Add("@ItemNum",uwg.Rows[0].Cells.FromKey("ItemNum").ToString());
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Label1.Text = dr["SmallClassificationName"].ToString();
				Label20.Text = dr["Standard"].ToString();				
			}
			dr.Close();
			comm.Parameters.Clear();

			comm.CommandText = @"Select SmallClassificationName From II_MT inner join PUC_MT on MateralQuality = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			comm.Parameters.Add("@ItemNum",uwg.Rows[0].Cells.FromKey("ItemNum").ToString());
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				Label21.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();

			conn.Close();
			
			Label2.Text = "&nbsp;";
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
			Label5.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("WorkDate").ToString()).ToShortDateString();
			//Label6.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("WorkDenotationDate").ToString()).ToShortDateString();
			decimal Quantity = 0;
			Quantity = decimal.Parse(uwg.Rows[0].Cells.FromKey("WorkPlanQuantity").ToString()) - decimal.Parse(uwg.Rows[0].Cells.FromKey("WorkCompletionQuantity").ToString());
			Label7.Text = string.Format("{0:D}",Quantity.ToString());	

		}

		protected void first()
		{
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessSequenceNum").Text;				//순서
			Label9.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[0].Cells.FromKey("WCName").Text.Trim() != "")
			Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;						//작업장
			
			uwg.Rows[0].Delete();
		}

		protected void second()
		{
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessSequenceNum").Text;				//순서
			Label9.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[0].Cells.FromKey("WCName").Text.Trim() != "")
				Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;					//작업장

			Label11.Text = uwg.Rows[1].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label12.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[1].Cells.FromKey("WCName").Text.Trim() != "")
				Label13.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;					//작업장
			
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();
		}

		
		protected void third()
		{
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessSequenceNum").Text;				//순서
			Label9.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[0].Cells.FromKey("WCName").Text.Trim() != "")
				Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;					//작업장

			Label11.Text = uwg.Rows[1].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label12.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[1].Cells.FromKey("WCName").Text.Trim() != "")
				Label13.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;					//작업장

			Label14.Text = uwg.Rows[2].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label15.Text = uwg.Rows[2].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[2].Cells.FromKey("WCName").Text.Trim() != "")
				Label16.Text = uwg.Rows[2].Cells.FromKey("WCName").Text;					//작업장

			uwg.Rows[2].Delete();
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();
		}

		protected void fourth()
		{
			Label8.Text = uwg.Rows[0].Cells.FromKey("ProcessSequenceNum").Text;				//순서
			Label9.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[0].Cells.FromKey("WCName").Text.Trim() != "")
				Label10.Text = uwg.Rows[0].Cells.FromKey("WCName").Text;					//작업장

			Label11.Text = uwg.Rows[1].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label12.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[1].Cells.FromKey("WCName").Text.Trim() != "")
				Label13.Text = uwg.Rows[1].Cells.FromKey("WCName").Text;					//작업장

			Label14.Text = uwg.Rows[2].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label15.Text = uwg.Rows[2].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[2].Cells.FromKey("WCName").Text.Trim() != "")
				Label16.Text = uwg.Rows[2].Cells.FromKey("WCName").Text;					//작업장


			Label17.Text = uwg.Rows[3].Cells.FromKey("ProcessSequenceNum").Text;			//순서
			Label18.Text = uwg.Rows[3].Cells.FromKey("ProcessName").ToString();				//공정명
			if(uwg.Rows[3].Cells.FromKey("WCName").Text.Trim() != "")
				Label19.Text = uwg.Rows[3].Cells.FromKey("WCName").Text;					//작업장

			
			uwg.Rows[3].Delete();
			uwg.Rows[2].Delete();
			uwg.Rows[1].Delete();
			uwg.Rows[0].Delete();
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
