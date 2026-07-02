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
	/// Purchase에 대한 요약 설명입니다.
	/// </summary>
	public class Purchase : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
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
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label77;
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
		protected System.Web.UI.WebControls.Label lbCompany;
		protected System.Web.UI.WebControls.Label lbFax;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
		protected System.Web.UI.WebControls.Label lbYear;
		protected System.Web.UI.WebControls.Label lbMon;
		protected System.Web.UI.WebControls.Label lbDay;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label76;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label91;
		protected System.Web.UI.WebControls.Label lbName;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.Label Label112;
		protected System.Web.UI.WebControls.Label Label113;
		protected System.Web.UI.WebControls.Label Label114;
		protected System.Web.UI.WebControls.Label Label115;
		protected System.Web.UI.WebControls.Label Label116;
		protected System.Web.UI.WebControls.Label Label117;
		protected System.Web.UI.WebControls.Label Label118;
		protected System.Web.UI.WebControls.Label Label119;
		protected System.Web.UI.WebControls.Label Label120;
		protected System.Web.UI.WebControls.Label Label121;
		protected System.Web.UI.WebControls.Label Label122;
		protected System.Web.UI.WebControls.Label Label89;
		protected System.Web.UI.WebControls.Label Label90;
		protected System.Web.UI.WebControls.Label Label92;
		protected System.Web.UI.WebControls.Label Label93;
		protected System.Web.UI.WebControls.Label Label94;
		protected System.Web.UI.WebControls.Label Label95;
		protected System.Web.UI.WebControls.Label Label96;
		protected System.Web.UI.WebControls.Label Label97;
		protected System.Web.UI.WebControls.Label Label98;
		protected System.Web.UI.WebControls.Label Label99;
		protected System.Web.UI.WebControls.Label Label100;
		protected System.Web.UI.WebControls.Label Label101;
		protected System.Web.UI.WebControls.Label Label102;
		protected System.Web.UI.WebControls.Label Label103;
		protected System.Web.UI.WebControls.Label Label104;
		protected System.Web.UI.WebControls.Label Label105;
		protected System.Web.UI.WebControls.Label Label106;
		protected System.Web.UI.WebControls.Label Label107;
		protected System.Web.UI.WebControls.Label Label108;
		protected System.Web.UI.WebControls.Label Label109;
		protected System.Web.UI.WebControls.Label Label110;
		protected System.Web.UI.WebControls.Label Label111;
		private static int cnt;



		/*

 *			2014년 단가 금액 유출로 단가와 총금액 란의 값을 없앴음.
 *			일단 칸은 그대로 두고 내용만 수정했음
 *			2014-08-28(이창헌부장)
 *

*/

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				Company();
				OrderPrint();
				lbYear.Text = DateTime.Now.Year.ToString();
				lbMon.Text = DateTime.Now.Month.ToString();
				lbDay.Text = DateTime.Now.Day.ToString();

				lbName.Text += uwg.Rows[0].Cells.FromKey("RegistrationPerson").Text;
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
				lbFax.Text = reader["FaxNum"].ToString();			// Fax
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
			else if(cnt == 11)
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

		private string Unit(string ItemNum)
		{
			string StandardUnit = " ";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT ItemNum, SmallClassificationName FROM II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @num";				
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
			Cmd.CommandText = "SELECT ItemNum, SmallClassificationName FROM II_MT inner join PUC_MT on MateralQuality = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @num";				
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

		private string Standard(string ItemNum)
		{
			string txtStandard = " ";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT Standard FROM II_MT Where RecodingState = 1 and ItemNum = @num";				
			Cmd.Parameters.Add("@num",ItemNum);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				txtStandard = reader["Standard"].ToString();
			}

			reader.Close();
			Con.Close();

			return txtStandard;
		}


		private void First()
		{
			Label1.Text = "1";
			Label2.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 품목명
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label4.Text = MateralQuality(uwg.Rows[0].Cells.FromKey("ItemNum").Text);// 재질
			Label5.Text = Standard(uwg.Rows[0].Cells.FromKey("ItemNum").Text);		// 규격
			Label6.Text = Unit(uwg.Rows[0].Cells.FromKey("ItemNum").Text);			// 단위	
			Label7.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label8.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			
/*

 *			2014년 단가 금액 유출로 단가와 총금액 란의 값을 없앴음.
 *			일단 칸은 그대로 두고 내용만 수정했음
 *			2014-08-28(이창헌부장)
 *

*/
			//Label89.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label90.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("TotalCost").Value);	// 금액
			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			if(Classification(uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label112.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label112.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[0].Cells.FromKey("ItemDrawNum").ToString()));
			//Label91.Text = "총금액 : "+ String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("TotalCost").Value);
		}

		private void Second()
		{
			Label9.Text = "2";
			Label10.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품목명
			Label11.Text = uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label12.Text = MateralQuality(uwg.Rows[1].Cells.FromKey("ItemNum").Text);// 재질
			Label13.Text = Standard(uwg.Rows[1].Cells.FromKey("ItemNum").Text);		// 규격
			Label14.Text = Unit(uwg.Rows[1].Cells.FromKey("ItemNum").Text);			// 단위	
			Label15.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label16.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;
			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text);

/*

 *			2014년 단가 금액 유출로 단가와 총금액 란의 값을 없앴음.
 *			일단 칸은 그대로 두고 내용만 수정했음
 *

*/
		
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label92.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label93.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("TotalCost").Value);	// 금액
			if(Classification(uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label113.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label113.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[1].Cells.FromKey("ItemDrawNum").ToString()));
			
		}

		private void Third()
		{
			Label17.Text = "3";
			Label18.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품목명
			Label19.Text = uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label20.Text = MateralQuality(uwg.Rows[2].Cells.FromKey("ItemNum").Text);// 재질
			Label21.Text = Standard(uwg.Rows[2].Cells.FromKey("ItemNum").Text);		// 규격
			Label22.Text = Unit(uwg.Rows[2].Cells.FromKey("ItemNum").Text);			// 단위	
			Label23.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label24.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;
			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label94.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label95.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("TotalCost").Value);	// 금액
			if(Classification(uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label114.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label114.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[2].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Fourth()
		{
			Label25.Text = "4";
			Label26.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품목명
			Label27.Text = uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label28.Text = MateralQuality(uwg.Rows[3].Cells.FromKey("ItemNum").Text);// 재질
			Label29.Text = Standard(uwg.Rows[3].Cells.FromKey("ItemNum").Text);		// 규격
			Label30.Text = Unit(uwg.Rows[3].Cells.FromKey("ItemNum").Text);			// 단위	
			Label31.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label32.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label96.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label97.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("TotalCost").Value);	// 금액
			if(Classification(uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label115.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label115.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[3].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Fifth()
		{
			Label33.Text = "5";
			Label34.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품목명
			Label35.Text = uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label36.Text = MateralQuality(uwg.Rows[4].Cells.FromKey("ItemNum").Text);// 재질
			Label37.Text = Standard(uwg.Rows[4].Cells.FromKey("ItemNum").Text);		// 규격
			Label38.Text = Unit(uwg.Rows[4].Cells.FromKey("ItemNum").Text);			// 단위	
			Label39.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label40.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label98.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label99.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("TotalCost").Value);	// 금액

			if(Classification(uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label116.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label116.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[4].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Sixth()
		{
			Label41.Text = "6";
			Label42.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품목명
			Label43.Text = uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label44.Text = MateralQuality(uwg.Rows[5].Cells.FromKey("ItemNum").Text);// 재질
			Label45.Text = Standard(uwg.Rows[5].Cells.FromKey("ItemNum").Text);		// 규격
			Label46.Text = Unit(uwg.Rows[5].Cells.FromKey("ItemNum").Text);			// 단위	
			Label47.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label48.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label100.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label101.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("TotalCost").Value);	// 금액

			if(Classification(uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label117.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label117.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[5].Cells.FromKey("ItemDrawNum").ToString()));
		}
		

		private void Seventh()
		{
			Label49.Text = "7";
			Label50.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품목명
			Label51.Text = uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label52.Text = MateralQuality(uwg.Rows[6].Cells.FromKey("ItemNum").Text);// 재질
			Label53.Text = Standard(uwg.Rows[6].Cells.FromKey("ItemNum").Text);		// 규격
			Label54.Text = Unit(uwg.Rows[6].Cells.FromKey("ItemNum").Text);			// 단위	
			Label55.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label56.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label102.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label103.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("TotalCost").Value);	// 금액


			if(Classification(uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label118.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label118.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[6].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Eighth()
		{
			Label57.Text = "8";
			Label58.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품목명
			Label59.Text = uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label60.Text = MateralQuality(uwg.Rows[7].Cells.FromKey("ItemNum").Text);// 재질
			Label61.Text = Standard(uwg.Rows[7].Cells.FromKey("ItemNum").Text);		// 규격
			Label62.Text = Unit(uwg.Rows[7].Cells.FromKey("ItemNum").Text);			// 단위	
			Label63.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label64.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;


			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label104.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label105.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("TotalCost").Value);	// 금액

			if(Classification(uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label119.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label119.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[7].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Ninth()
		{
			Label65.Text = "9";
			Label66.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품목명
			Label67.Text = uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label68.Text = MateralQuality(uwg.Rows[8].Cells.FromKey("ItemNum").Text);// 재질
			Label69.Text = Standard(uwg.Rows[8].Cells.FromKey("ItemNum").Text);		// 규격
			Label70.Text = Unit(uwg.Rows[8].Cells.FromKey("ItemNum").Text);			// 단위	
			Label71.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label72.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label106.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label107.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("TotalCost").Value);	// 금액


			if(Classification(uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label120.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label120.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[8].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Tenth()
		{
			Label73.Text = "10";
			Label74.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품목명
			Label75.Text = uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label76.Text = MateralQuality(uwg.Rows[9].Cells.FromKey("ItemNum").Text);// 재질
			Label77.Text = Standard(uwg.Rows[9].Cells.FromKey("ItemNum").Text);		// 규격
			Label78.Text = Unit(uwg.Rows[9].Cells.FromKey("ItemNum").Text);			// 단위	
			Label79.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label80.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;

			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label108.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ApplyUnitCost").Value);	// 단가
			//Label109.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("TotalCost").Value);	// 금액

			if(Classification(uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label121.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label121.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[9].Cells.FromKey("ItemDrawNum").ToString()));
		}

		private void Eleventh()
		{
			Label81.Text = "11";
			Label82.Text = uwg.Rows[10].Cells.FromKey("ItemName").ToString();			// 품목명
			Label83.Text = uwg.Rows[10].Cells.FromKey("ItemDrawNum").ToString();		// 도면번호
			Label84.Text = MateralQuality(uwg.Rows[10].Cells.FromKey("ItemNum").Text);// 재질
			Label85.Text = Standard(uwg.Rows[10].Cells.FromKey("ItemNum").Text);		// 규격
			Label86.Text = Unit(uwg.Rows[10].Cells.FromKey("ItemNum").Text);			// 단위	
			Label87.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("OrderQuantity").Value);	// 수량
			Label88.Text = DateTime.Parse(uwg.Rows[10].Cells.FromKey("FirstDeliveryDemandDate").Text).Year+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("FirstDeliveryDemandDate").Text).Month+"/ "+DateTime.Parse(uwg.Rows[10].Cells.FromKey("FirstDeliveryDemandDate").Text).Day;


			decimal Total = decimal.Parse(uwg.Rows[0].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("TotalCost").Text)  + decimal.Parse(uwg.Rows[3].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("TotalCost").Text) + decimal.Parse(uwg.Rows[10].Cells.FromKey("TotalCost").Text);
			Label91.Text = "총금액 : "+ String.Format("{0:#,###}", Total) + "원";
			//Label110.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("ApplyUnitCost").Value);	// 단가
           // Label111.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("TotalCost").Value);	// 금액

			if(Classification(uwg.Rows[10].Cells.FromKey("ItemDrawNum").ToString()) == 1)
				Label122.Text = String.Format("{0:#,###}",BusinessStockQuantity(uwg.Rows[10].Cells.FromKey("ItemDrawNum").ToString()));
			else if(Classification(uwg.Rows[10].Cells.FromKey("ItemDrawNum").ToString()) == 2)
				Label122.Text = String.Format("{0:#,###}",RowStockQuantity(uwg.Rows[10].Cells.FromKey("ItemDrawNum").ToString()));
		}


		private int Classification(string ItemNum)
		{
			string Class = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT PropertyClassification FROM II_MT Where RecodingState = 1 and ItemNum = @num";				
			Cmd.Parameters.Add("@num",ItemNum);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Class = reader["PropertyClassification"].ToString();			// 자산분류				
			}
			reader.Close();
			Con.Close();

			if(Class == "상품")
				return 1;
			else if(Class == "원자재")
				return 2;
			else 
				return 4;

		}

		private decimal BusinessStockQuantity(string ItemNum)
		{
			decimal Quantity = 0 ;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT Sum(StockQuantity12) as Quantity FROM BS_MT Where RecodingState = 1 and ItemNum = @num and [Year] = @year";				
			Cmd.Parameters.Add("@num",ItemNum);
			Cmd.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Quantity = decimal.Parse(reader["Quantity"].ToString());			// 재고				
			}
			reader.Close();
			Con.Close();

			return Quantity;
		}


		private decimal RowStockQuantity(string ItemNum)
		{
			decimal Quantity = 0 ;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT (StockQuantity12) as Quantity FROM RMS_MT Where RecodingState = 1 and ItemNum = @num and [Year] = @year";				
			Cmd.Parameters.Add("@num",ItemNum);
			Cmd.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Quantity = decimal.Parse(reader["Quantity"].ToString());			// 재고				
			}
			reader.Close();
			Con.Close();

			return Quantity;

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
