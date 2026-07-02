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

namespace KIT_ERP.BuyingOutside.PopUp
{
	/// <summary>
	/// OutSideOutStorehousePurchase에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideOutStorehousePurchase : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbCompany;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
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
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.Label Label52;
		protected System.Web.UI.WebControls.Label Label53;
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
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label lbName;
		protected System.Web.UI.WebControls.Label lbName1;

		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label76;
		protected System.Web.UI.WebControls.Label Label78;
		protected System.Web.UI.WebControls.Label Label79;
		protected System.Web.UI.WebControls.Label Label80;
		protected System.Web.UI.WebControls.Label Label81;
		protected System.Web.UI.WebControls.Label Label82;
		protected System.Web.UI.WebControls.Label Label83;
		protected System.Web.UI.WebControls.Label Label84;
		protected System.Web.UI.WebControls.Label Label85;
		protected System.Web.UI.WebControls.Label Label86;
		protected System.Web.UI.WebControls.Label Label87;
		protected System.Web.UI.WebControls.Label Label88;
		protected System.Web.UI.WebControls.Label Label89;
		protected System.Web.UI.WebControls.Label lbCompany1;
		protected System.Web.UI.WebControls.Label Label77;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label72;
		private static int cont;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				OrderPrint();
				//lbYear.Text = DateTime.Now.Year.ToString();
				//lbMon.Text = DateTime.Now.Month.ToString();
				//lbDay.Text = DateTime.Now.Day.ToString();
				lbCompany.Text = uwg.Rows[0].Cells.FromKey("CompanyName").ToString();
				lbCompany1.Text = uwg.Rows[0].Cells.FromKey("CompanyName").ToString();
				lbName.Text += Session["UserName"].ToString();
				lbName1.Text += Session["UserName"].ToString();
			}
		}

		private void OrderPrint()
		{
				
			cont = uwg.Rows.Count;
			
			if(cont == 1)
			{
				First();
			}
			else if(cont == 2)
			{
				First();
				Second();
			}
			else if(cont ==3)
			{
				First();
				Second();
				Third();
			}
			else if(cont ==4)
			{
				First();
				Second();
				Third();
				Fourth();
			}
			else if(cont == 5)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
			}
			else if(cont == 6)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
			}
			else if(cont == 7)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
			}
			else if(cont == 8)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
				Eighth();
			}
			else if(cont == 9)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
				Eighth();
				Ninth();
			}			
			else if(cont == 10)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
				Eighth();
				Ninth();
				Tenth();
			}
			else if(cont == 11)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
				Eighth();
				Ninth();
				Tenth();
				Eleventh();
			}			

		}


		private void First()
		{
			Label1.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();			// 품번
			Label2.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 품목명
			Label3.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label4.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label37.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();			// 품번
			Label38.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 품목명
			Label39.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label40.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Second()
		{
			Label5.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();			// 품번
			Label6.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품목명
			Label7.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label8.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label41.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();			// 품번
			Label42.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품목명
			Label43.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label44.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Third()
		{
			Label9.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();			// 품번
			Label10.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품목명
			Label11.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label12.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label45.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();			// 품번
			Label46.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품목명
			Label47.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label48.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Fourth()
		{
			Label13.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();			// 품번
			Label14.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품목명
			Label15.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label16.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label49.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();			// 품번
			Label50.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품목명
			Label51.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label52.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Fifth()
		{
			Label17.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();			// 품번
			Label18.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품목명
			Label19.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label20.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label53.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();			// 품번
			Label54.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품목명
			Label55.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label56.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}
		private void Sixth()
		{
			Label21.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();			// 품번
			Label22.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품목명
			Label23.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label24.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label57.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();			// 품번
			Label58.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품목명
			Label59.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label60.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}
		
		

		private void Seventh()
		{
			Label25.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();			// 품번
			Label26.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품목명
			Label27.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label28.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label61.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();			// 품번
			Label62.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품목명
			Label63.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label64.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Eighth()
		{
			Label29.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();			// 품번
			Label30.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품목명
			Label31.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label32.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label65.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();			// 품번
			Label66.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품목명
			Label67.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label68.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Ninth()
		{
			Label74.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();			// 품번
			Label75.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품목명
			Label76.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label77.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label82.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();			// 품번
			Label83.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품목명
			Label84.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label85.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}


		private void Tenth()
		{
			Label78.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();			// 품번
			Label79.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품목명
			Label80.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label81.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label86.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();			// 품번
			Label87.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품목명
			Label88.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label89.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
		}

		private void Eleventh()
		{
			Label33.Text = uwg.Rows[10].Cells.FromKey("ItemNum").ToString();			// 품번
			Label34.Text = uwg.Rows[10].Cells.FromKey("ItemName").ToString();			// 품목명
			Label35.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label36.Text = DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Day;//출고

			Label69.Text = uwg.Rows[10].Cells.FromKey("ItemNum").ToString();			// 품번
			Label70.Text = uwg.Rows[10].Cells.FromKey("ItemName").ToString();			// 품목명
			Label71.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량
			Label72.Text = DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("OutStorehouseDate").Text).Day;//출고
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
