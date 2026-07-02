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
	/// MLimOutOrder에 대한 요약 설명입니다.
	/// </summary>
	public class MLimOutOrder : System.Web.UI.Page
	{
		private static int cnt;
		protected System.Web.UI.WebControls.Label Label88;
		protected System.Web.UI.WebControls.Label Label87;
		protected System.Web.UI.WebControls.Label Label86;
		protected System.Web.UI.WebControls.Label Label85;
		protected System.Web.UI.WebControls.Label Label84;
		protected System.Web.UI.WebControls.Label Label83;
		protected System.Web.UI.WebControls.Label Label82;
		protected System.Web.UI.WebControls.Label Label81;
		protected System.Web.UI.WebControls.Label Label80;
		protected System.Web.UI.WebControls.Label Label79;
		protected System.Web.UI.WebControls.Label Label78;
		protected System.Web.UI.WebControls.Label Label77;
		protected System.Web.UI.WebControls.Label Label76;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.Label Label61;
		protected System.Web.UI.WebControls.Label Label60;
		protected System.Web.UI.WebControls.Label Label59;
		protected System.Web.UI.WebControls.Label Label58;
		protected System.Web.UI.WebControls.Label Label57;
		protected System.Web.UI.WebControls.Label Label56;
		protected System.Web.UI.WebControls.Label Label55;
		protected System.Web.UI.WebControls.Label Label54;
		protected System.Web.UI.WebControls.Label Label53;
		protected System.Web.UI.WebControls.Label Label52;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox tbDate;
		protected System.Web.UI.WebControls.Label lbTel;
		protected System.Web.UI.WebControls.Label lbName;
		protected System.Web.UI.WebControls.Label lbPerson;
		protected System.Web.UI.WebControls.Label lbCompany;
		protected System.Web.UI.WebControls.Label Label89;
		protected System.Web.UI.WebControls.Label Label90;
		protected System.Web.UI.WebControls.Label Label91;
		protected System.Web.UI.WebControls.Label Label92;
		protected System.Web.UI.WebControls.Label Label93;
		protected System.Web.UI.WebControls.Label Label94;
		protected System.Web.UI.WebControls.Label Label95;
		protected System.Web.UI.WebControls.Label Label96;
		protected System.Web.UI.WebControls.Label Label97;
		protected System.Web.UI.WebControls.Label Label98;
		protected System.Web.UI.WebControls.Label Label99;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				Company();
				OrderPrint();
				
				tbDate.Text = DateTime.Now.ToShortDateString();
				lbName.Text = Session["UserName"].ToString();
			}
		}

		private void Company()
		{
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
				lbCompany.Text = reader["CompanyName"].ToString();			// 거래처명
				lbTel.Text = reader["TelephoneNum"].ToString();		// 전화번호
				lbPerson.Text = reader["CompanyPersonInCharge"].ToString();				// 담당자
			}

			reader.Close();
			Con.Close();
		}


		private void OrderPrint()
		{
				
			cnt = uwg.Rows.Count;
			
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

		private string Unit(string ItemNum)
		{
			string StandardUnit = " ";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT SmallClassificationName FROM II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @num";				
			Cmd.Parameters.Add("@num",ItemNum);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				StandardUnit = reader["SmallClassificationName"].ToString();
			}

			reader.Close();
			Con.Close();

			return StandardUnit;
		}

		private string MateralQuality(string ItemNum)
		{
			string Materal = " ";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT SmallClassificationName FROM II_MT inner join PUC_MT on MateralQuality = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @num";				
			Cmd.Parameters.Add("@num",ItemNum);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Materal = reader["SmallClassificationName"].ToString();
			}

			reader.Close();
			Con.Close();

			return Materal;
		}

		private string Measure(string ItemNum, string Code)
		{
			string Measure = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT Measure FROM PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @Code";				
			Cmd.Parameters.Add("@num",ItemNum);
			Cmd.Parameters.Add("@Code",Code);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Measure = reader["Measure"].ToString();
			}

			reader.Close();
			Con.Close();

			return Measure;
		}

		private void First()
		{
			Label1.Text = "1";
			Label2.Text = uwg.Rows[0].Cells.FromKey("EndProcess").Text;		// 공정
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label4.Text = uwg.Rows[0].Cells.FromKey("ItemName").Text;		// 품명
			Label5.Text = Unit(uwg.Rows[0].Cells.FromKey("ItemNum").Text);			// 단위	
			Label6.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label7.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label8.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label89.Text = Measure(uwg.Rows[0].Cells.FromKey("ItemNum").Text,uwg.Rows[0].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Second()
		{
			Label9.Text = "2";
			Label10.Text = uwg.Rows[1].Cells.FromKey("EndProcess").Text;		// 공정
			Label11.Text = uwg.Rows[1].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label12.Text = uwg.Rows[1].Cells.FromKey("ItemName").Text;		// 품명
			Label13.Text = Unit(uwg.Rows[1].Cells.FromKey("ItemNum").Text);			// 단위	
			Label14.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label15.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label16.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label90.Text = Measure(uwg.Rows[1].Cells.FromKey("ItemNum").Text,uwg.Rows[1].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Third()
		{
			Label17.Text = "3";
			Label18.Text = uwg.Rows[2].Cells.FromKey("EndProcess").Text;		// 공정
			Label19.Text = uwg.Rows[2].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label20.Text = uwg.Rows[2].Cells.FromKey("ItemName").Text;		// 품명
			Label21.Text = Unit(uwg.Rows[2].Cells.FromKey("ItemNum").Text);			// 단위	
			Label22.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label23.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label24.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label91.Text = Measure(uwg.Rows[2].Cells.FromKey("ItemNum").Text,uwg.Rows[2].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Fourth()
		{
			Label25.Text = "4";
			Label26.Text = uwg.Rows[3].Cells.FromKey("EndProcess").Text;		// 공정
			Label27.Text = uwg.Rows[3].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label28.Text = uwg.Rows[3].Cells.FromKey("ItemName").Text;		// 품명
			Label29.Text = Unit(uwg.Rows[3].Cells.FromKey("ItemNum").Text);			// 단위	
			Label30.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label31.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label32.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label92.Text = Measure(uwg.Rows[3].Cells.FromKey("ItemNum").Text,uwg.Rows[3].Cells.FromKey("EndProcessCode").Text);	//치수
		}


		private void Fifth()
		{
			Label33.Text = "5";
			Label34.Text = uwg.Rows[4].Cells.FromKey("EndProcess").Text;		// 공정
			Label35.Text = uwg.Rows[4].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label36.Text = uwg.Rows[4].Cells.FromKey("ItemName").Text;		// 품명
			Label37.Text = Unit(uwg.Rows[4].Cells.FromKey("ItemNum").Text);			// 단위	
			Label38.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label39.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label40.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label93.Text = Measure(uwg.Rows[4].Cells.FromKey("ItemNum").Text,uwg.Rows[4].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Sixth()
		{
			Label41.Text = "6";
			Label42.Text = uwg.Rows[5].Cells.FromKey("EndProcess").Text;		// 공정
			Label43.Text = uwg.Rows[5].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label44.Text = uwg.Rows[5].Cells.FromKey("ItemName").Text;		// 품명
			Label45.Text = Unit(uwg.Rows[5].Cells.FromKey("ItemNum").Text);			// 단위	
			Label46.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label47.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label48.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label94.Text = Measure(uwg.Rows[5].Cells.FromKey("ItemNum").Text,uwg.Rows[5].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Seventh()
		{
			Label49.Text = "7";
			Label50.Text = uwg.Rows[6].Cells.FromKey("EndProcess").Text;		// 공정
			Label51.Text = uwg.Rows[6].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label52.Text = uwg.Rows[6].Cells.FromKey("ItemName").Text;		// 품명
			Label53.Text = Unit(uwg.Rows[6].Cells.FromKey("ItemNum").Text);			// 단위	
			Label54.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label55.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label56.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label95.Text = Measure(uwg.Rows[6].Cells.FromKey("ItemNum").Text,uwg.Rows[6].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Eighth()
		{
			Label57.Text = "8";
			Label58.Text = uwg.Rows[7].Cells.FromKey("EndProcess").Text;		// 공정
			Label59.Text = uwg.Rows[7].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label60.Text = uwg.Rows[7].Cells.FromKey("ItemName").Text;		// 품명
			Label61.Text = Unit(uwg.Rows[7].Cells.FromKey("ItemNum").Text);			// 단위	
			Label62.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label63.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label64.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label96.Text = Measure(uwg.Rows[7].Cells.FromKey("ItemNum").Text,uwg.Rows[7].Cells.FromKey("EndProcessCode").Text);	//치수
		}

		private void Ninth()
		{
			Label65.Text = "9";
			Label66.Text = uwg.Rows[8].Cells.FromKey("EndProcess").Text;		// 공정
			Label67.Text = uwg.Rows[8].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label68.Text = uwg.Rows[8].Cells.FromKey("ItemName").Text;		// 품명
			Label69.Text = Unit(uwg.Rows[8].Cells.FromKey("ItemNum").Text);			// 단위	
			Label70.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label71.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label72.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label97.Text = Measure(uwg.Rows[8].Cells.FromKey("ItemNum").Text,uwg.Rows[8].Cells.FromKey("EndProcessCode").Text);	//치수
		}


		private void Tenth()
		{
			Label73.Text = "10";
			Label74.Text = uwg.Rows[9].Cells.FromKey("EndProcess").Text;		// 공정
			Label75.Text = uwg.Rows[9].Cells.FromKey("ItemDrawNum").Text;			// 도번
			Label76.Text = uwg.Rows[9].Cells.FromKey("ItemName").Text;		// 품명
			Label77.Text = Unit(uwg.Rows[9].Cells.FromKey("ItemNum").Text);			// 단위	
			Label78.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 수량
			Label79.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			Label80.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();	//납기일
			Label98.Text = Measure(uwg.Rows[9].Cells.FromKey("ItemNum").Text,uwg.Rows[9].Cells.FromKey("EndProcessCode").Text);	//치수
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
