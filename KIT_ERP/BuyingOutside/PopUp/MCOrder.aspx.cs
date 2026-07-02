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

namespace hanaro.BuyingOutside.PopUp
{
	/// <summary>
	/// MCOrder에 대한 요약 설명입니다.
	/// </summary>
	public class MCOrder : System.Web.UI.Page
	{
		
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
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
		protected System.Web.UI.WebControls.Label Label36;
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
		protected System.Web.UI.WebControls.Label lb_Com;
		protected System.Web.UI.WebControls.TextBox lb_Year;
		protected System.Web.UI.WebControls.TextBox lb_Mon;
		protected System.Web.UI.WebControls.TextBox lb_Day;
		
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				
				lb_Year.Text = DateTime.Now.Year.ToString();
				lb_Mon.Text = DateTime.Now.Month.ToString();
				lb_Day.Text = DateTime.Now.Day.ToString();
				lb_Com.Text = Company();
				OrderPrint();
			}
		}



		private string Company()
		{
			string Com = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT * FROM CI_MT Where RecodingState = 1 and BusinessRegistrationNum = @num";				
			Cmd.Parameters.Add("@num",uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString());
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Com = reader["CompanyName"].ToString();			// 거래처명
			}
			reader.Close();
			Con.Close();

			return Com;
		}


		private void OrderPrint()
		{
				
			int cnt = uwg.Rows.Count;
			
			if(cnt == 1)
			{
				First();
			}
			else if(cnt == 2)
			{
				First();
				Second();
			}
			else if(cnt ==3)
			{
				First();
				Second();
				Third();
			}
			else if(cnt ==4)
			{
				First();
				Second();
				Third();
				Fourth();
			}
			else if(cnt == 5)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
			}
			else if(cnt == 6)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
			}
			else if(cnt == 7)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
			}
			else if(cnt == 8)
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
			else if(cnt == 9)
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
			else if(cnt == 10)
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
		}


		private void First()
		{
			//Label1.Text = Company(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label2.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label5.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 단위	
			Label6.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label7.Text = uwg.Rows[0].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Second()
		{
			//Label8.Text = Company(uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label9.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label10.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label11.Text = uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label12.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 단위	
			Label13.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[1].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label14.Text = uwg.Rows[1].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Third()
		{
			//Label15.Text = Company(uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label16.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label17.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label18.Text = uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label19.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 단위	
			Label20.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[2].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label21.Text = uwg.Rows[2].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Fourth()
		{
			//Label22.Text = Company(uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label23.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label24.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label25.Text = uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label26.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 단위	
			Label27.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[3].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label28.Text = uwg.Rows[3].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Fifth()
		{
			//Label29.Text = Company(uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label30.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label31.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label32.Text = uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label33.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 단위	
			Label34.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[4].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label35.Text = uwg.Rows[4].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Sixth()
		{
			//Label36.Text = Company(uwg.Rows[5].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label37.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label38.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label39.Text = uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label40.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 단위	
			Label41.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[5].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label42.Text = uwg.Rows[5].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Seventh()
		{
			//Label43.Text = Company(uwg.Rows[6].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label44.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label45.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label46.Text = uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label47.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 단위	
			Label48.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[6].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label49.Text = uwg.Rows[6].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Eighth()
		{
			//Label50.Text = Company(uwg.Rows[7].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label51.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label52.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label53.Text = uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label54.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 단위	
			Label55.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[7].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label56.Text = uwg.Rows[7].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Ninth()
		{
			//Label57.Text = Company(uwg.Rows[8].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label58.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label59.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label60.Text = uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label61.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 단위	
			Label62.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[8].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label63.Text = uwg.Rows[8].Cells.FromKey("ProcessName").ToString();	// 종료공정
		}

		private void Tenth()
		{
			//Label64.Text = Company(uwg.Rows[9].Cells.FromKey("BusinessRegistrationNum").ToString());
			//Label65.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();			// 품목명
			Label66.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label67.Text = uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString();		// 규격
			Label68.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 단위	
			Label69.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[9].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			Label70.Text = uwg.Rows[9].Cells.FromKey("ProcessName").ToString();	// 종료공정
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
