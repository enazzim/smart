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
	/// HandockOrder에 대한 요약 설명입니다.
	/// </summary>
	public class HandockOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label18;
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
		protected System.Web.UI.WebControls.Label Label32;
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
		protected System.Web.UI.WebControls.Label Label46;
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
		protected System.Web.UI.WebControls.Label Label60;
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
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label76;
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
		protected System.Web.UI.WebControls.Label Label88;
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
		protected System.Web.UI.WebControls.Label Label100;
		protected System.Web.UI.WebControls.Label Label102;
		protected System.Web.UI.WebControls.Label Label104;
		protected System.Web.UI.WebControls.Label Label105;
		protected System.Web.UI.WebControls.Label Label106;
		protected System.Web.UI.WebControls.Label Label107;
		protected System.Web.UI.WebControls.Label Label108;
		protected System.Web.UI.WebControls.Label Label109;
		protected System.Web.UI.WebControls.Label Label110;
		protected System.Web.UI.WebControls.Label Label111;
		protected System.Web.UI.WebControls.Label Label112;
		protected System.Web.UI.WebControls.Label Label113;
		protected System.Web.UI.WebControls.Label Label114;
		protected System.Web.UI.WebControls.Label Label116;
		protected System.Web.UI.WebControls.Label Label118;
		protected System.Web.UI.WebControls.Label Label119;
		protected System.Web.UI.WebControls.Label Label120;
		protected System.Web.UI.WebControls.Label Label121;
		protected System.Web.UI.WebControls.Label Label122;
		protected System.Web.UI.WebControls.Label Label123;
		protected System.Web.UI.WebControls.Label Label124;
		protected System.Web.UI.WebControls.Label Label125;
		protected System.Web.UI.WebControls.Label Label126;
		protected System.Web.UI.WebControls.Label Label127;
		protected System.Web.UI.WebControls.Label Label128;
		protected System.Web.UI.WebControls.Label Label130;
		protected System.Web.UI.WebControls.Label Label132;
		protected System.Web.UI.WebControls.Label Label133;
		protected System.Web.UI.WebControls.Label Label134;
		protected System.Web.UI.WebControls.Label Label135;
		protected System.Web.UI.WebControls.Label Label136;
		protected System.Web.UI.WebControls.Label Label137;
		protected System.Web.UI.WebControls.Label Label138;
		protected System.Web.UI.WebControls.Label Label139;
		protected System.Web.UI.WebControls.Label Label140;
		protected System.Web.UI.WebControls.Label Label141;
		protected System.Web.UI.WebControls.Label Label142;
		protected System.Web.UI.WebControls.Label Label143;
		protected System.Web.UI.WebControls.Label Label144;
		protected System.Web.UI.WebControls.Label Label145;
		protected System.Web.UI.WebControls.Label Label14;

		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
		protected System.Web.UI.WebControls.Label lbDate;
		protected System.Web.UI.WebControls.Label lbName;
		protected System.Web.UI.WebControls.Label lbCompany;
		protected System.Web.UI.WebControls.Label lbTEL;
		protected System.Web.UI.WebControls.Label lbFAX;
		protected System.Web.UI.WebControls.Label lbPerson;
		protected System.Web.UI.WebControls.Label lbNum;
		protected System.Web.UI.WebControls.TextBox Label3;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox Label17;
		protected System.Web.UI.WebControls.TextBox Label31;
		protected System.Web.UI.WebControls.TextBox Label45;
		protected System.Web.UI.WebControls.TextBox Label59;
		protected System.Web.UI.WebControls.TextBox Label73;
		protected System.Web.UI.WebControls.TextBox Label87;
		protected System.Web.UI.WebControls.TextBox Label101;
		protected System.Web.UI.WebControls.TextBox Label115;
		protected System.Web.UI.WebControls.TextBox Label129;
		protected System.Web.UI.WebControls.TextBox Label5;
		protected System.Web.UI.WebControls.TextBox Label19;
		protected System.Web.UI.WebControls.TextBox Label33;
		protected System.Web.UI.WebControls.TextBox Label47;
		protected System.Web.UI.WebControls.TextBox Label61;
		protected System.Web.UI.WebControls.TextBox Label75;
		protected System.Web.UI.WebControls.TextBox Label89;
		protected System.Web.UI.WebControls.TextBox Label103;
		protected System.Web.UI.WebControls.TextBox Label117;
		protected System.Web.UI.WebControls.TextBox Label131;
		private static int cnt;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				Company();
				OrderPrint();
				
				lbDate.Text = DateTime.Now.ToShortDateString();
				lbNum.Text = DateTime.Now.ToShortDateString();
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
				lbTEL.Text = reader["TelephoneNum"].ToString();		// 전화번호
				lbFAX.Text = reader["FaxNum"].ToString();				// FAX
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
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();		// 품목번호
			//Label5.Text = Standard(uwg.Rows[0].Cells.FromKey("ItemNum").Text);		// 규격
			Label4.Text = Unit(uwg.Rows[0].Cells.FromKey("ItemNum").Text);			// 단위	
			Label6.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label7.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label8.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label9.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label10.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label11.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label12.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label13.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label14.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text));
		}

		private void Second()
		{
			Label15.Text = "2";
			Label16.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품목명
			Label17.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label18.Text = Unit(uwg.Rows[1].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label19.Text = Standard(uwg.Rows[1].Cells.FromKey("ItemNum").Text);		// 규격
			Label20.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label21.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label22.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label23.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label24.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label25.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label26.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label27.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label28.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text));
		}

		private void Third()
		{
			Label29.Text = "3";
			Label30.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품목명
			Label31.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label32.Text = Unit(uwg.Rows[2].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label33.Text = Standard(uwg.Rows[2].Cells.FromKey("ItemNum").Text);		// 규격
			Label34.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label35.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label36.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label37.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label38.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label39.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label40.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label41.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label42.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text));
		}

		private void Fourth()
		{
			Label43.Text = "4";
			Label44.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품목명
			Label45.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label46.Text = Unit(uwg.Rows[3].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label47.Text = Standard(uwg.Rows[3].Cells.FromKey("ItemNum").Text);		// 규격
			Label48.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label49.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label50.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label51.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label52.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label53.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label54.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label55.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label56.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text));
		}

		private void Fifth()
		{
			Label57.Text = "5";
			Label58.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품목명
			Label59.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label60.Text = Unit(uwg.Rows[4].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label61.Text = Standard(uwg.Rows[4].Cells.FromKey("ItemNum").Text);		// 규격
			Label62.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label63.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label64.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label65.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label66.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label67.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label68.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label69.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label70.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text));
		}

		private void Sixth()
		{
			Label71.Text = "6";
			Label72.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품목명
			Label73.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label74.Text = Unit(uwg.Rows[5].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label75.Text = Standard(uwg.Rows[5].Cells.FromKey("ItemNum").Text);		// 규격
			Label76.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label77.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label78.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label79.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label80.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label81.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label82.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label83.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label84.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("OrderQuantity").Text));
		}

		private void Seventh()
		{
			Label85.Text = "7";
			Label86.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품목명
			Label87.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label88.Text = Unit(uwg.Rows[6].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label89.Text = Standard(uwg.Rows[6].Cells.FromKey("ItemNum").Text);		// 규격
			Label90.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label91.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label92.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label93.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label94.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label95.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label96.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label97.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label98.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("OrderQuantity").Text));
		}

		private void Eighth()
		{
			Label99.Text = "8";
			Label100.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품목명
			Label101.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label102.Text = Unit(uwg.Rows[7].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label103.Text = Standard(uwg.Rows[7].Cells.FromKey("ItemNum").Text);		// 규격
			Label104.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label105.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label106.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label107.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label108.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label109.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label110.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label111.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label112.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("OrderQuantity").Text));
		}

		private void Ninth()
		{
			Label113.Text = "9";
			Label114.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품목명
			Label115.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label116.Text = Unit(uwg.Rows[8].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label117.Text = Standard(uwg.Rows[8].Cells.FromKey("ItemNum").Text);		// 규격
			Label118.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label119.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[8].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label120.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label121.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[8].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label122.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label123.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[8].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label124.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label125.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label126.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("OrderQuantity").Text));
		}

		private void Tenth()
		{
			Label127.Text = "10";
			Label128.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품목명
			Label129.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();		// 품목번호
			Label130.Text = Unit(uwg.Rows[9].Cells.FromKey("ItemNum").Text);			// 단위	
			//Label131.Text = Standard(uwg.Rows[9].Cells.FromKey("ItemNum").Text);		// 규격
			Label132.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandDate").Text).ToShortDateString();
			Label133.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandQuantity").Value);	// 1차금액
			if(uwg.Rows[9].Cells.FromKey("SecondDeliveryDemandDate").Value != null)
				Label134.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("SecondDeliveryDemandDate").Text).ToShortDateString();
			Label135.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("SecondDeliveryDemandQuantity").Value);	// 2차금액
			if(uwg.Rows[9].Cells.FromKey("ThirdDeliveryDemandDate").Value != null)
				Label136.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("ThirdDeliveryDemandDate").Text).ToShortDateString();
			Label137.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ThirdDeliveryDemandQuantity").Value);	// 3차금액
			if(uwg.Rows[9].Cells.FromKey("FourthDeliveryDemandDate").Value != null)
				Label138.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("FourthDeliveryDemandDate").Text).ToShortDateString();
			Label139.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("FourthDeliveryDemandQuantity").Value);	// 4차금액
			Label140.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("OrderQuantity").Value);	// 총금액

			Label141.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("FirstDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("FirstDeliveryDemandQuantity").Text));
			Label142.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("SecondDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("SecondDeliveryDemandQuantity").Text));
			Label143.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("ThirdDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("ThirdDeliveryDemandQuantity").Text));
			Label144.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("FourthDeliveryDemandQuantity").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("FourthDeliveryDemandQuantity").Text));
			
			Label145.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[1].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[2].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[3].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[4].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[5].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[6].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[7].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[8].Cells.FromKey("OrderQuantity").Text) + decimal.Parse(uwg.Rows[9].Cells.FromKey("OrderQuantity").Text));
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
