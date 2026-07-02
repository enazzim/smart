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
	/// OutSideStorehousePurchase에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideStorehousePurchase : System.Web.UI.Page
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
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label75;
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
		protected System.Web.UI.WebControls.Label Label87;
		protected System.Web.UI.WebControls.Label Label88;
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
		protected System.Web.UI.WebControls.Label Label123;
		protected System.Web.UI.WebControls.Label Label124;
		protected System.Web.UI.WebControls.Label Label125;
		protected System.Web.UI.WebControls.Label Label126;
		protected System.Web.UI.WebControls.Label Label127;
		protected System.Web.UI.WebControls.Label Label128;
		protected System.Web.UI.WebControls.Label Label129;
		protected System.Web.UI.WebControls.Label Label130;
		protected System.Web.UI.WebControls.Label Label131;
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
		protected System.Web.UI.WebControls.Label Label146;
		protected System.Web.UI.WebControls.Label Label147;
		protected System.Web.UI.WebControls.Label Label148;
		protected System.Web.UI.WebControls.Label Label149;
		protected System.Web.UI.WebControls.Label Label150;
		protected System.Web.UI.WebControls.Label Label151;
		protected System.Web.UI.WebControls.Label Label152;
		protected System.Web.UI.WebControls.Label Label153;
		protected System.Web.UI.WebControls.Label Label154;
		protected System.Web.UI.WebControls.Label Label155;
		protected System.Web.UI.WebControls.Label Label156;
		protected System.Web.UI.WebControls.Label Label157;
		protected System.Web.UI.WebControls.Label Label158;
		protected System.Web.UI.WebControls.Label Label159;
		protected System.Web.UI.WebControls.Label Label160;
		protected System.Web.UI.WebControls.Label Label161;
		protected System.Web.UI.WebControls.Label Label162;
		protected System.Web.UI.WebControls.Label Label163;
		protected System.Web.UI.WebControls.Label Label164;
		protected System.Web.UI.WebControls.Label Label165;
		protected System.Web.UI.WebControls.Label Label166;
		protected System.Web.UI.WebControls.Label Label167;
		protected System.Web.UI.WebControls.Label Label168;
		protected System.Web.UI.WebControls.Label Label169;
		protected System.Web.UI.WebControls.Label Label170;
		protected System.Web.UI.WebControls.Label Label171;
		protected System.Web.UI.WebControls.Label Label172;
		protected System.Web.UI.WebControls.Label Label173;
		protected System.Web.UI.WebControls.Label Label174;
		protected System.Web.UI.WebControls.Label Label175;
		protected System.Web.UI.WebControls.Label Label176;
		protected System.Web.UI.WebControls.Label Label177;
		protected System.Web.UI.WebControls.Label Label178;
		protected System.Web.UI.WebControls.Label Label179;
		protected System.Web.UI.WebControls.Label Label180;
		protected System.Web.UI.WebControls.Label Label181;
		protected System.Web.UI.WebControls.Label Label182;
		protected System.Web.UI.WebControls.Label Label183;
		protected System.Web.UI.WebControls.Label Label184;
		protected System.Web.UI.WebControls.Label Label185;
		protected System.Web.UI.WebControls.Label Label186;
		protected System.Web.UI.WebControls.Label Label187;
		protected System.Web.UI.WebControls.Label Label188;
		protected System.Web.UI.WebControls.Label Label189;
		protected System.Web.UI.WebControls.Label Label190;
		protected System.Web.UI.WebControls.Label Label191;
		protected System.Web.UI.WebControls.Label Label192;
		protected System.Web.UI.WebControls.Label Label193;
		protected System.Web.UI.WebControls.Label Label194;
		protected System.Web.UI.WebControls.Label Label195;
		protected System.Web.UI.WebControls.Label Label196;
		protected System.Web.UI.WebControls.Label Label197;
		protected System.Web.UI.WebControls.Label Label198;
		protected System.Web.UI.WebControls.Label Label199;
		protected System.Web.UI.WebControls.Label Label200;
		protected System.Web.UI.WebControls.Label Label201;
		protected System.Web.UI.WebControls.Label Label202;
		protected System.Web.UI.WebControls.Label Label203;
		protected System.Web.UI.WebControls.Label Label204;
		protected System.Web.UI.WebControls.Label Label205;
		protected System.Web.UI.WebControls.Label Label206;
		protected System.Web.UI.WebControls.Label Label207;
		protected System.Web.UI.WebControls.Label Label208;
		protected System.Web.UI.WebControls.Label Label209;
		protected System.Web.UI.WebControls.Label Label210;
		protected System.Web.UI.WebControls.Label Label211;
		protected System.Web.UI.WebControls.Label Label212;
		protected System.Web.UI.WebControls.Label Label213;
		protected System.Web.UI.WebControls.Label Label214;
		protected System.Web.UI.WebControls.Label Label215;
		protected System.Web.UI.WebControls.Label Label216;
		protected System.Web.UI.WebControls.Label Label217;
		protected System.Web.UI.WebControls.Label Label218;
		protected System.Web.UI.WebControls.Label Label219;
		protected System.Web.UI.WebControls.Label Label220;
		protected System.Web.UI.WebControls.Label Label221;
		protected System.Web.UI.WebControls.Label Label222;
		protected System.Web.UI.WebControls.Label Label223;
		protected System.Web.UI.WebControls.Label Label224;
		protected System.Web.UI.WebControls.Label Label225;
		protected System.Web.UI.WebControls.Label Label226;
		protected System.Web.UI.WebControls.Label Label227;
		protected System.Web.UI.WebControls.Label Label228;
		protected System.Web.UI.WebControls.Label Label229;
		protected System.Web.UI.WebControls.Label Label230;
		protected System.Web.UI.WebControls.Label Label231;
		protected System.Web.UI.WebControls.Label Label232;
		protected System.Web.UI.WebControls.Label Label233;
		protected System.Web.UI.WebControls.Label Label234;
		protected System.Web.UI.WebControls.Label Label235;
		protected System.Web.UI.WebControls.Label Label236;
		protected System.Web.UI.WebControls.Label Label237;
		protected System.Web.UI.WebControls.Label Label238;
		protected System.Web.UI.WebControls.Label Label239;
		protected System.Web.UI.WebControls.Label Label240;
		protected System.Web.UI.WebControls.Label Label241;
		protected System.Web.UI.WebControls.Label Label242;
		protected System.Web.UI.WebControls.Label Label243;
		protected System.Web.UI.WebControls.Label Label244;
		protected System.Web.UI.WebControls.Label Label245;
		protected System.Web.UI.WebControls.Label Label246;
		protected System.Web.UI.WebControls.Label Label247;
		protected System.Web.UI.WebControls.Label Label248;
		protected System.Web.UI.WebControls.Label Label249;
		protected System.Web.UI.WebControls.Label Label250;
		protected System.Web.UI.WebControls.Label Label251;
		protected System.Web.UI.WebControls.Label Label252;
		
		protected System.Web.UI.WebControls.Label lbComName2;
		protected System.Web.UI.WebControls.Label lbComName1;
		protected System.Web.UI.WebControls.Label lbComName;
		protected System.Web.UI.WebControls.Label lbOutDate;
		protected System.Web.UI.WebControls.Label lbOutDate1;
		protected System.Web.UI.WebControls.Label lbInDate;

		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;

		private static int Outcont;

		//Label9.Text = DateTime.Now.Year.ToString().Substring(2,2)+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!IsPostBack)
			{
				uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];
				
				lbComName.Text= uwg.Rows[0].Cells.FromKey("CompanyName").ToString();
				lbComName1.Text= lbComName.Text;
				lbComName2.Text= lbComName.Text;
				lbOutDate.Text = DateTime.Now.Year+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day;
				lbOutDate1.Text = DateTime.Now.Year+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day;
				

				OrderPrint();
			}
			


		}

		private void OrderPrint()
		{
				
			Outcont = uwg.Rows.Count;
			
			if(Outcont == 1)
			{
				First();
			}
			else if(Outcont == 2)
			{
				First();
				Second();
			}
			else if(Outcont ==3)
			{
				First();
				Second();
				Third();
			}
			else if(Outcont ==4)
			{
				First();
				Second();
				Third();
				Fourth();
			}
			else if(Outcont == 5)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
			}
			else if(Outcont == 6)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
			}
			else if(Outcont == 7)
			{
				First();
				Second();
				Third();
				Fourth();
				Fifth();
				Sixth();
				Seventh();
			}
			else if(Outcont == 8)
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
			else if(Outcont == 9)
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
			else if(Outcont == 10)
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
			else if(Outcont == 11)
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
			else if(Outcont == 12)
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
				Twelfth();
			}			
			else if(Outcont == 13)
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
				Twelfth();
				Thirteenth();
			}
			else if(Outcont == 14)
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
				Twelfth();
				Thirteenth();
				Fourteenth();
			}	

		}

		private void First()
		{
			Label1.Text = "1";			// 품번
			Label2.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();			// 품번
			Label3.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 품명
			Label4.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label127.Text = "1";			// 품번
			Label128.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();			// 품번
			Label129.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();			// 품명
			Label130.Text = String.Format("{0:#,###}",uwg.Rows[0].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[0].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label5.Text = FindItemNum(int.Parse(uwg.Rows[0].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label6.Text = FindItemName(int.Parse(uwg.Rows[0].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label7.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[0].Cells.FromKey("ThisOrderQuantity").Text));
				Label8.Text = FindProcess(int.Parse(uwg.Rows[0].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label9.Text = DateTime.Parse(uwg.Rows[0].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[0].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[0].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[0].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label131.Text = Label5.Text;
				Label132.Text = Label6.Text;
				Label133.Text = Label7.Text;
				Label134.Text = Label8.Text;
				Label135.Text = Label9.Text;
			}			
		}

		private void Second()
		{
			Label10.Text = "2";			// 품번
			Label11.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();			// 품번
			Label12.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품명
			Label13.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label136.Text = "2";			// 품번
			Label137.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();			// 품번
			Label138.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();			// 품명
			Label139.Text = String.Format("{0:#,###}",uwg.Rows[1].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[1].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label14.Text = FindItemNum(int.Parse(uwg.Rows[1].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label15.Text = FindItemName(int.Parse(uwg.Rows[1].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label16.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[1].Cells.FromKey("ThisOrderQuantity").Text));
				Label17.Text = FindProcess(int.Parse(uwg.Rows[1].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label18.Text = DateTime.Parse(uwg.Rows[1].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[1].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[1].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[1].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label140.Text = Label14.Text;
				Label141.Text = Label15.Text;
				Label142.Text = Label16.Text;
				Label143.Text = Label17.Text;
				Label144.Text = Label18.Text;
			}			
		}

		private void Third()
		{
			Label19.Text = "3";			// 품번
			Label20.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();			// 품번
			Label21.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품명
			Label22.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label145.Text = "3";			// 품번
			Label146.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();			// 품번
			Label147.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();			// 품명
			Label148.Text = String.Format("{0:#,###}",uwg.Rows[2].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[2].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label23.Text = FindItemNum(int.Parse(uwg.Rows[2].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label24.Text = FindItemName(int.Parse(uwg.Rows[2].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label25.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[2].Cells.FromKey("ThisOrderQuantity").Text));
				Label26.Text = FindProcess(int.Parse(uwg.Rows[2].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label27.Text = DateTime.Parse(uwg.Rows[2].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[2].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[2].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[2].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label149.Text = Label23.Text;
				Label150.Text = Label24.Text;
				Label151.Text = Label25.Text;
				Label152.Text = Label26.Text;
				Label153.Text = Label27.Text;
			}			
		}


		private void Fourth()
		{
			Label28.Text = "4";			// 품번
			Label29.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();			// 품번
			Label30.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품명
			Label31.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label154.Text = "4";			// 품번
			Label155.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();			// 품번
			Label156.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();			// 품명
			Label157.Text = String.Format("{0:#,###}",uwg.Rows[3].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[3].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label32.Text = FindItemNum(int.Parse(uwg.Rows[3].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label33.Text = FindItemName(int.Parse(uwg.Rows[3].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label34.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[3].Cells.FromKey("ThisOrderQuantity").Text));
				Label35.Text = FindProcess(int.Parse(uwg.Rows[3].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label36.Text = DateTime.Parse(uwg.Rows[3].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[3].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[3].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[3].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label158.Text = Label32.Text;
				Label159.Text = Label33.Text;
				Label160.Text = Label34.Text;
				Label161.Text = Label35.Text;
				Label162.Text = Label36.Text;
			}			
		}

		private void Fifth()
		{
			Label37.Text = "5";			// 품번
			Label38.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();			// 품번
			Label39.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품명
			Label40.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label163.Text = "5";			// 품번
			Label164.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();			// 품번
			Label165.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();			// 품명
			Label166.Text = String.Format("{0:#,###}",uwg.Rows[4].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[4].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label41.Text = FindItemNum(int.Parse(uwg.Rows[4].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label42.Text = FindItemName(int.Parse(uwg.Rows[4].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label43.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[4].Cells.FromKey("ThisOrderQuantity").Text));
				Label44.Text = FindProcess(int.Parse(uwg.Rows[4].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label45.Text = DateTime.Parse(uwg.Rows[4].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[4].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[4].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[4].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label167.Text = Label41.Text;
				Label168.Text = Label42.Text;
				Label169.Text = Label43.Text;
				Label170.Text = Label44.Text;
				Label171.Text = Label45.Text;
			}			
		}

		private void Sixth()
		{
			Label46.Text = "6";			// 품번
			Label47.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();			// 품번
			Label48.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품명
			Label49.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label172.Text = "6";			// 품번
			Label173.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();			// 품번
			Label174.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();			// 품명
			Label175.Text = String.Format("{0:#,###}",uwg.Rows[5].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[5].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label50.Text = FindItemNum(int.Parse(uwg.Rows[5].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label51.Text = FindItemName(int.Parse(uwg.Rows[5].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label52.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[5].Cells.FromKey("ThisOrderQuantity").Text));
				Label53.Text = FindProcess(int.Parse(uwg.Rows[5].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label54.Text = DateTime.Parse(uwg.Rows[5].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[5].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[5].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[5].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label176.Text = Label50.Text;
				Label177.Text = Label51.Text;
				Label178.Text = Label52.Text;
				Label179.Text = Label53.Text;
				Label180.Text = Label54.Text;
			}			
		}

		private void Seventh()
		{
			Label55.Text = "7";			// 품번
			Label56.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();			// 품번
			Label57.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품명
			Label58.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label181.Text = "7";			// 품번
			Label182.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();			// 품번
			Label183.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();			// 품명
			Label184.Text = String.Format("{0:#,###}",uwg.Rows[6].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[6].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label59.Text = FindItemNum(int.Parse(uwg.Rows[6].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label60.Text = FindItemName(int.Parse(uwg.Rows[6].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label61.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[6].Cells.FromKey("ThisOrderQuantity").Text));
				Label62.Text = FindProcess(int.Parse(uwg.Rows[6].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label63.Text = DateTime.Parse(uwg.Rows[6].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[6].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[6].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[6].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label185.Text = Label59.Text;
				Label186.Text = Label60.Text;
				Label187.Text = Label61.Text;
				Label188.Text = Label62.Text;
				Label189.Text = Label63.Text;
			}			
		}

		private void Eighth()
		{
			Label64.Text = "8";			// 품번
			Label65.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();			// 품번
			Label66.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품명
			Label67.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label190.Text = "8";			// 품번
			Label191.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();			// 품번
			Label192.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();			// 품명
			Label193.Text = String.Format("{0:#,###}",uwg.Rows[7].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[7].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label68.Text = FindItemNum(int.Parse(uwg.Rows[7].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label69.Text = FindItemName(int.Parse(uwg.Rows[7].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label70.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[7].Cells.FromKey("ThisOrderQuantity").Text));
				Label71.Text = FindProcess(int.Parse(uwg.Rows[7].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label72.Text = DateTime.Parse(uwg.Rows[7].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[7].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[7].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[7].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label194.Text = Label68.Text;
				Label195.Text = Label69.Text;
				Label196.Text = Label70.Text;
				Label197.Text = Label71.Text;
				Label198.Text = Label72.Text;
			}			
		}

		private void Ninth()
		{
			Label73.Text = "9";			// 품번
			Label74.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();			// 품번
			Label75.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품명
			Label76.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label199.Text = "9";			// 품번
			Label200.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();			// 품번
			Label201.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();			// 품명
			Label202.Text = String.Format("{0:#,###}",uwg.Rows[8].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[8].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label77.Text = FindItemNum(int.Parse(uwg.Rows[8].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label78.Text = FindItemName(int.Parse(uwg.Rows[8].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label79.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[8].Cells.FromKey("ThisOrderQuantity").Text));
				Label80.Text = FindProcess(int.Parse(uwg.Rows[8].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label81.Text = DateTime.Parse(uwg.Rows[8].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[8].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[8].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[8].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label203.Text = Label77.Text;
				Label204.Text = Label78.Text;
				Label205.Text = Label79.Text;
				Label206.Text = Label80.Text;
				Label207.Text = Label81.Text;
			}			
		}

		private void Tenth()
		{
			Label82.Text = "10";			// 품번
			Label83.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();			// 품번
			Label84.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품명
			Label85.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label208.Text = "10";			// 품번
			Label209.Text = uwg.Rows[9].Cells.FromKey("ItemNum").ToString();			// 품번
			Label210.Text = uwg.Rows[9].Cells.FromKey("ItemName").ToString();			// 품명
			Label211.Text = String.Format("{0:#,###}",uwg.Rows[9].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[9].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label86.Text = FindItemNum(int.Parse(uwg.Rows[9].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label87.Text = FindItemName(int.Parse(uwg.Rows[9].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label88.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[9].Cells.FromKey("ThisOrderQuantity").Text));
				Label89.Text = FindProcess(int.Parse(uwg.Rows[9].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label90.Text = DateTime.Parse(uwg.Rows[9].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[9].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[9].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[9].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label212.Text = Label86.Text;
				Label213.Text = Label87.Text;
				Label214.Text = Label88.Text;
				Label215.Text = Label89.Text;
				Label216.Text = Label90.Text;
			}			
		}


		private void Eleventh()
		{
			Label91.Text = "11";			// 품번
			Label92.Text = uwg.Rows[10].Cells.FromKey("ItemNum").ToString();			// 품번
			Label93.Text = uwg.Rows[10].Cells.FromKey("ItemName").ToString();			// 품명
			Label94.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label217.Text = "11";			// 품번
			Label218.Text = uwg.Rows[10].Cells.FromKey("ItemNum").ToString();			// 품번
			Label219.Text = uwg.Rows[10].Cells.FromKey("ItemName").ToString();			// 품명
			Label220.Text = String.Format("{0:#,###}",uwg.Rows[10].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[10].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label95.Text = FindItemNum(int.Parse(uwg.Rows[10].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label96.Text = FindItemName(int.Parse(uwg.Rows[10].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label97.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[10].Cells.FromKey("ThisOrderQuantity").Text));
				Label98.Text = FindProcess(int.Parse(uwg.Rows[10].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label99.Text = DateTime.Parse(uwg.Rows[10].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[10].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[10].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[10].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label221.Text = Label95.Text;
				Label222.Text = Label96.Text;
				Label223.Text = Label97.Text;
				Label224.Text = Label98.Text;
				Label225.Text = Label99.Text;
			}			
		}

		private void Twelfth()
		{
			Label100.Text = "12";			// 품번
			Label101.Text = uwg.Rows[11].Cells.FromKey("ItemNum").ToString();			// 품번
			Label102.Text = uwg.Rows[11].Cells.FromKey("ItemName").ToString();			// 품명
			Label103.Text = String.Format("{0:#,###}",uwg.Rows[11].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label226.Text = "12";			// 품번
			Label227.Text = uwg.Rows[11].Cells.FromKey("ItemNum").ToString();			// 품번
			Label228.Text = uwg.Rows[11].Cells.FromKey("ItemName").ToString();			// 품명
			Label229.Text = String.Format("{0:#,###}",uwg.Rows[11].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[11].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label104.Text = FindItemNum(int.Parse(uwg.Rows[11].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label105.Text = FindItemName(int.Parse(uwg.Rows[11].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label106.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[11].Cells.FromKey("ThisOrderQuantity").Text));
				Label107.Text = FindProcess(int.Parse(uwg.Rows[11].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label108.Text = DateTime.Parse(uwg.Rows[11].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[11].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[11].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[11].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label230.Text = Label104.Text;
				Label231.Text = Label105.Text;
				Label232.Text = Label106.Text;
				Label233.Text = Label107.Text;
				Label234.Text = Label108.Text;
			}			
		}

		private void Thirteenth()
		{
			Label109.Text = "13";			// 품번
			Label110.Text = uwg.Rows[12].Cells.FromKey("ItemNum").ToString();			// 품번
			Label111.Text = uwg.Rows[12].Cells.FromKey("ItemName").ToString();			// 품명
			Label112.Text = String.Format("{0:#,###}",uwg.Rows[12].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label235.Text = "13";			// 품번
			Label236.Text = uwg.Rows[12].Cells.FromKey("ItemNum").ToString();			// 품번
			Label237.Text = uwg.Rows[12].Cells.FromKey("ItemName").ToString();			// 품명
			Label238.Text = String.Format("{0:#,###}",uwg.Rows[12].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[12].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label113.Text = FindItemNum(int.Parse(uwg.Rows[12].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label114.Text = FindItemName(int.Parse(uwg.Rows[12].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label115.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[12].Cells.FromKey("ThisOrderQuantity").Text));
				Label116.Text = FindProcess(int.Parse(uwg.Rows[12].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label117.Text = DateTime.Parse(uwg.Rows[12].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[12].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[12].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[12].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label239.Text = Label113.Text;
				Label240.Text = Label114.Text;
				Label241.Text = Label115.Text;
				Label242.Text = Label116.Text;
				Label243.Text = Label117.Text;
			}			
		}

		private void Fourteenth()
		{
			Label118.Text = "14";			// 품번
			Label119.Text = uwg.Rows[13].Cells.FromKey("ItemNum").ToString();			// 품번
			Label120.Text = uwg.Rows[13].Cells.FromKey("ItemName").ToString();			// 품명
			Label121.Text = String.Format("{0:#,###}",uwg.Rows[13].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			Label244.Text = "14";			// 품번
			Label245.Text = uwg.Rows[13].Cells.FromKey("ItemNum").ToString();			// 품번
			Label246.Text = uwg.Rows[13].Cells.FromKey("ItemName").ToString();			// 품명
			Label247.Text = String.Format("{0:#,###}",uwg.Rows[13].Cells.FromKey("ThistimeOutStorehouseQuantity").Value);	// 출고수량

			if(uwg.Rows[13].Cells.FromKey("OutSideOrderHistoryIndex").Value != null)
			{
				Label122.Text = FindItemNum(int.Parse(uwg.Rows[13].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label123.Text = FindItemName(int.Parse(uwg.Rows[13].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label124.Text = String.Format("{0:#,###}",decimal.Parse(uwg.Rows[13].Cells.FromKey("ThisOrderQuantity").Text));
				Label125.Text = FindProcess(int.Parse(uwg.Rows[13].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));
				Label126.Text = DateTime.Parse(uwg.Rows[13].Cells.FromKey("ThisDeliveryDate").ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(uwg.Rows[13].Cells.FromKey("ThisDeliveryDate").ToString()).Month +"/"+DateTime.Parse(uwg.Rows[13].Cells.FromKey("ThisDeliveryDate").ToString()).Day;//FindIDate(int.Parse(uwg.Rows[13].Cells.FromKey("OutSideOrderHistoryIndex").ToString()));

				Label248.Text = Label122.Text;
				Label249.Text = Label123.Text;
				Label250.Text = Label124.Text;
				Label251.Text = Label125.Text;
				Label252.Text = Label126.Text;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private string FindItemNum(int idx)
		{
			string ItemNum = "";

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT ItemNum FROM OO_HT Where OutSideOrderHistoryIndex = @idx";
			Cmd.Parameters.Add("@idx",idx);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				ItemNum = reader["ItemNum"].ToString();
			}

			reader.Close();
			Con.Close();


			return ItemNum;
		}

		private string FindItemName(int idx)
		{
			string ItemName = "";

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT ItemName FROM OO_HT Where OutSideOrderHistoryIndex = @idx";
			Cmd.Parameters.Add("@idx",idx);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				ItemName = reader["ItemName"].ToString();
			}

			reader.Close();
			Con.Close();

			return ItemName;
		}

		private decimal FindIQuantity(int idx)
		{
			decimal Quantity = 0;

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT OrderQuantity FROM OO_HT Where OutSideOrderHistoryIndex = @idx";
			Cmd.Parameters.Add("@idx",idx);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Quantity = decimal.Parse(reader["OrderQuantity"].ToString());
			}

			reader.Close();
			Con.Close();

			return Quantity;
		}

		private string FindProcess(int idx)
		{
			string Process = "";

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT EndProcess FROM OO_HT Where OutSideOrderHistoryIndex = @idx";
			Cmd.Parameters.Add("@idx",idx);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Process = reader["EndProcess"].ToString();
			}

			reader.Close();
			Con.Close();

			return Process;
		}

		private string FindIDate(int idx)
		{
			string Date = "";

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT FirstDeliveryDemandDate FROM OO_HT Where OutSideOrderHistoryIndex = @idx";
			Cmd.Parameters.Add("@idx",idx);
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
			
			if(reader.Read())
			{
				Date = DateTime.Parse(reader["FirstDeliveryDemandDate"].ToString()).Year.ToString().Substring(2,2)+"/"+DateTime.Parse(reader["FirstDeliveryDemandDate"].ToString()).Month +"/"+DateTime.Parse(reader["FirstDeliveryDemandDate"].ToString()).Day;
			}
			reader.Close();
			Con.Close();
		
			return Date;


		}
	}
}
