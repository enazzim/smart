using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.WebSchedule;

namespace KIT_ERP
{
	/// <summary>
	/// Table에 대한 요약 설명입니다.
	/// </summary>
	public class Table
	{
		private string str ;
		private int mon = DateTime.Now.Month;
		private int year = DateTime.Now.Year;		
		public Table()
		{	
		}
		public Table(int a)
		{	
			mon = a;
		}

		public Table(int a, int b)
		{	
			year = a;
			mon = b;
		}

		/// <summary>
		/// 매입매출테이블 매입금액,미지급금액 증가
		/// </summary>
		/// <returns></returns>
		public string PaymentIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost1 = TotalBuyingCost1 + @cost,"+
						"TotalBuyingCost2 = TotalBuyingCost2 + @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 + @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 + @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney1 = UnPaymentMoney1 + @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost2 = TotalBuyingCost2 + @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 + @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 + @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost3 = TotalBuyingCost3 + @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 + @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost4 = TotalBuyingCost4 + @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost11 = TotalBuyingCost11 + @cost, "+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost12 = TotalBuyingCost12 + @cost, "+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						"WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalBuyingCost1 = TotalBuyingCost1 + @cost,"+
					"TotalBuyingCost2 = TotalBuyingCost2 + @cost,"+
					"TotalBuyingCost3 = TotalBuyingCost3 + @cost,"+
					"TotalBuyingCost4 = TotalBuyingCost4 + @cost,"+ 
					"TotalBuyingCost5 = TotalBuyingCost5 + @cost,"+
					"TotalBuyingCost6 = TotalBuyingCost6 + @cost,"+
					"TotalBuyingCost7 = TotalBuyingCost7 + @cost,"+
					"TotalBuyingCost8 = TotalBuyingCost8 + @cost,"+
					"TotalBuyingCost9 = TotalBuyingCost9 + @cost,"+
					"TotalBuyingCost10 = TotalBuyingCost10 + @cost,"+
					"TotalBuyingCost11 = TotalBuyingCost11 + @cost,"+
					"TotalBuyingCost12 = TotalBuyingCost12 + @cost,"+
					"UnPaymentMoney1 = UnPaymentMoney1 + @cost,"+
					"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
					"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
					"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
					"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
					"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
					"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
					"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
					"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
					"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
					"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
					"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}

		/// <summary>
		/// 매입매출테이블 매입금액,미지급금액 감소
		/// </summary>
		/// <returns></returns>
		public string PaymentDiminutionTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost1 = TotalBuyingCost1 - @cost,"+
						"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost, "+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost "+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						"WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}


			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalBuyingCost1 = TotalBuyingCost1 - @cost,"+
					"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
					"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
					"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
					"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
					"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
					"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
					"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
					"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
					"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
					"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
					"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
					"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
					"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
					"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
					"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
					"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
					"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
					"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
					"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
					"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
					"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
					"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
					"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}

		/// <summary>
		/// 매입매출테이블 매출금액,미수금액 증가
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost1 = TotalSaleCost1 + @cost,"+
						"TotalSaleCost2 = TotalSaleCost2 + @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 + @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 + @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney1 = UncollectMoney1 + @cost,"+
						"UncollectMoney2 = UncollectMoney2 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost2 = TotalSaleCost2 + @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 + @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 + @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney2 = UncollectMoney2 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost3 = TotalSaleCost3 + @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 + @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost4 = TotalSaleCost4 + @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost11 = TotalSaleCost11 + @cost, "+
						"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost12 = TotalSaleCost12 + @cost, "+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						"WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}


			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalSaleCost1 = TotalSaleCost1 + @cost,"+
					"TotalSaleCost2 = TotalSaleCost2 + @cost,"+
					"TotalSaleCost3 = TotalSaleCost3 + @cost,"+
					"TotalSaleCost4 = TotalSaleCost4 + @cost,"+ 
					"TotalSaleCost5 = TotalSaleCost5 + @cost,"+
					"TotalSaleCost6 = TotalSaleCost6 + @cost,"+
					"TotalSaleCost7 = TotalSaleCost7 + @cost,"+
					"TotalSaleCost8 = TotalSaleCost8 + @cost,"+
					"TotalSaleCost9 = TotalSaleCost9 + @cost,"+
					"TotalSaleCost10 = TotalSaleCost10 + @cost,"+
					"TotalSaleCost11 = TotalSaleCost11 + @cost,"+
					"TotalSaleCost12 = TotalSaleCost12 + @cost,"+
					"UncollectMoney1 = UncollectMoney1 + @cost,"+
					"UncollectMoney2 = UncollectMoney2 + @cost,"+
					"UncollectMoney3 = UncollectMoney3 + @cost,"+
					"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
					"UncollectMoney5 = UncollectMoney5 + @cost,"+
					"UncollectMoney6 = UncollectMoney6 + @cost,"+
					"UncollectMoney7 = UncollectMoney7 + @cost,"+
					"UncollectMoney8 = UncollectMoney8 + @cost,"+
					"UncollectMoney9 = UncollectMoney9 + @cost,"+
					"UncollectMoney10 = UncollectMoney10 + @cost,"+
					"UncollectMoney11 = UncollectMoney11 + @cost,"+
					"UncollectMoney12 = UncollectMoney12 + @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}


			return str;
		}

		/// <summary>
		/// 매입매출테이블 매출금액 감소
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyDiminutionTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost1 = TotalSaleCost1 - @cost,"+
						"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney1 = UncollectMoney1 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost11 = TotalSaleCost11 - @cost, "+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost12 = TotalSaleCost12 - @cost, "+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						"WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}


			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalSaleCost1 = TotalSaleCost1 - @cost,"+
					"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
					"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
					"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
					"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
					"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
					"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
					"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
					"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
					"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
					"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
					"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
					"UncollectMoney1 = UncollectMoney1 - @cost,"+
					"UncollectMoney2 = UncollectMoney2 - @cost,"+
					"UncollectMoney3 = UncollectMoney3 - @cost,"+
					"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
					"UncollectMoney5 = UncollectMoney5 - @cost,"+
					"UncollectMoney6 = UncollectMoney6 - @cost,"+
					"UncollectMoney7 = UncollectMoney7 - @cost,"+
					"UncollectMoney8 = UncollectMoney8 - @cost,"+
					"UncollectMoney9 = UncollectMoney9 - @cost,"+
					"UncollectMoney10 = UncollectMoney10 - @cost,"+
					"UncollectMoney11 = UncollectMoney11 - @cost,"+
					"UncollectMoney12 = UncollectMoney12 - @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}
			
			return str;
		}


		


		/// <summary>
		/// 매입매출테이블 수금액증가, 미지급액 감소
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyIncreaseUnCollectMoneyDiminutionTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney1 = CollectMoney1 + @cost,"+
						"CollectMoney2 = CollectMoney2 + @cost,"+
						"CollectMoney3 = CollectMoney3 + @cost,"+
						"CollectMoney4 = CollectMoney4 + @cost,"+ 
						"CollectMoney5 = CollectMoney5 + @cost,"+
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney1 = UncollectMoney1 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney2 = CollectMoney2 + @cost,"+
						"CollectMoney3 = CollectMoney3 + @cost,"+
						"CollectMoney4 = CollectMoney4 + @cost,"+ 
						"CollectMoney5 = CollectMoney5 + @cost,"+
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney3 = CollectMoney3 + @cost,"+
						"CollectMoney4 = CollectMoney4 + @cost,"+ 
						"CollectMoney5 = CollectMoney5 + @cost,"+
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney4 = CollectMoney4 + @cost,"+ 
						"CollectMoney5 = CollectMoney5 + @cost,"+
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney5 = CollectMoney5 + @cost,"+
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney6 = CollectMoney6 + @cost,"+
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney7 = CollectMoney7 + @cost,"+
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney8 = CollectMoney8 + @cost,"+
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney9 = CollectMoney9 + @cost,"+
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney10 = CollectMoney10 + @cost,"+
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney11 = CollectMoney11 + @cost,"+
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney12 = CollectMoney12 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}


			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"CollectMoney1 = CollectMoney1 + @cost,"+
					"CollectMoney2 = CollectMoney2 + @cost,"+
					"CollectMoney3 = CollectMoney3 + @cost,"+
					"CollectMoney4 = CollectMoney4 + @cost,"+ 
					"CollectMoney5 = CollectMoney5 + @cost,"+
					"CollectMoney6 = CollectMoney6 + @cost,"+
					"CollectMoney7 = CollectMoney7 + @cost,"+
					"CollectMoney8 = CollectMoney8 + @cost,"+
					"CollectMoney9 = CollectMoney9 + @cost,"+
					"CollectMoney10 = CollectMoney10 + @cost,"+
					"CollectMoney11 = CollectMoney11 + @cost,"+
					"CollectMoney12 = CollectMoney12 + @cost,"+
					"UncollectMoney1 = UncollectMoney1 - @cost,"+
					"UncollectMoney2 = UncollectMoney2 - @cost,"+
					"UncollectMoney3 = UncollectMoney3 - @cost,"+
					"UncollectMoney4 = UncollectMoney4 - @cost,"+ 
					"UncollectMoney5 = UncollectMoney5 - @cost,"+
					"UncollectMoney6 = UncollectMoney6 - @cost,"+
					"UncollectMoney7 = UncollectMoney7 - @cost,"+
					"UncollectMoney8 = UncollectMoney8 - @cost,"+
					"UncollectMoney9 = UncollectMoney9 - @cost,"+
					"UncollectMoney10 = UncollectMoney10 - @cost,"+
					"UncollectMoney11 = UncollectMoney11 - @cost,"+
					"UncollectMoney12 = UncollectMoney12 - @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}


			return str;
		}


		/// <summary>
		/// 매입매출테이블 수금액감소, 미지급액 증가
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyDiminutionUnCollectMoneyIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney1 = CollectMoney1 - @cost,"+
						"CollectMoney2 = CollectMoney2 - @cost,"+
						"CollectMoney3 = CollectMoney3 - @cost,"+
						"CollectMoney4 = CollectMoney4 - @cost,"+ 
						"CollectMoney5 = CollectMoney5 - @cost,"+
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney1 = UncollectMoney1 + @cost,"+
						"UncollectMoney2 = UncollectMoney2 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney2 = CollectMoney2 - @cost,"+
						"CollectMoney3 = CollectMoney3 - @cost,"+
						"CollectMoney4 = CollectMoney4 - @cost,"+ 
						"CollectMoney5 = CollectMoney5 - @cost,"+
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 + @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney3 = CollectMoney3 - @cost,"+
						"CollectMoney4 = CollectMoney4 - @cost,"+ 
						"CollectMoney5 = CollectMoney5 - @cost,"+
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 + @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"CollectMoney4 = CollectMoney4 - @cost,"+ 
						"CollectMoney5 = CollectMoney5 - @cost,"+
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney5 = CollectMoney5 - @cost,"+
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 + @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney6 = CollectMoney6 - @cost,"+
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 + @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney7 = CollectMoney7 - @cost,"+
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 + @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney8 = CollectMoney8 - @cost,"+
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 + @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney9 = CollectMoney9 - @cost,"+
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 + @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney10 = CollectMoney10 - @cost,"+
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 + @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney11 = CollectMoney11 - @cost,"+
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 + @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+ 
						"CollectMoney12 = CollectMoney12 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}


			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"CollectMoney1 = CollectMoney1 - @cost,"+
					"CollectMoney2 = CollectMoney2 - @cost,"+
					"CollectMoney3 = CollectMoney3 - @cost,"+
					"CollectMoney4 = CollectMoney4 - @cost,"+ 
					"CollectMoney5 = CollectMoney5 - @cost,"+
					"CollectMoney6 = CollectMoney6 - @cost,"+
					"CollectMoney7 = CollectMoney7 - @cost,"+
					"CollectMoney8 = CollectMoney8 - @cost,"+
					"CollectMoney9 = CollectMoney9 - @cost,"+
					"CollectMoney10 = CollectMoney10 - @cost,"+
					"CollectMoney11 = CollectMoney11 - @cost,"+
					"CollectMoney12 = CollectMoney12 - @cost,"+
					"UncollectMoney1 = UncollectMoney1 + @cost,"+
					"UncollectMoney2 = UncollectMoney2 + @cost,"+
					"UncollectMoney3 = UncollectMoney3 + @cost,"+
					"UncollectMoney4 = UncollectMoney4 + @cost,"+ 
					"UncollectMoney5 = UncollectMoney5 + @cost,"+
					"UncollectMoney6 = UncollectMoney6 + @cost,"+
					"UncollectMoney7 = UncollectMoney7 + @cost,"+
					"UncollectMoney8 = UncollectMoney8 + @cost,"+
					"UncollectMoney9 = UncollectMoney9 + @cost,"+
					"UncollectMoney10 = UncollectMoney10 + @cost,"+
					"UncollectMoney11 = UncollectMoney11 + @cost,"+
					"UncollectMoney12 = UncollectMoney12 + @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}

		/// <summary>
		/// 매입매출테이블 지급금액 증가, 미지급액 감소
		/// </summary>
		/// <returns></returns>
		public string UNCollectMoneyIncreasePaymentMoneyDiminutionTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney1 = PaymentMoney1 + @cost,"+
						"PaymentMoney2 = PaymentMoney2 + @cost,"+
						"PaymentMoney3 = PaymentMoney3 + @cost,"+
						"PaymentMoney4 = PaymentMoney4 + @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 + @cost,"+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney2 = PaymentMoney2 + @cost,"+
						"PaymentMoney3 = PaymentMoney3 + @cost,"+
						"PaymentMoney4 = PaymentMoney4 + @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 + @cost,"+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney3 = PaymentMoney3 + @cost,"+
						"PaymentMoney4 = PaymentMoney4 + @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 + @cost,"+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney4 = PaymentMoney4 + @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 + @cost,"+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney5 = PaymentMoney5 + @cost,"+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney6 = PaymentMoney6 + @cost,"+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney7 = PaymentMoney7 + @cost,"+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney8 = PaymentMoney8 + @cost,"+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney9 = PaymentMoney9 + @cost,"+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney10 = PaymentMoney10 + @cost,"+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney11 = PaymentMoney11 + @cost,"+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney12 = PaymentMoney12 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"PaymentMoney1 = PaymentMoney1 + @cost,"+
					"PaymentMoney2 = PaymentMoney2 + @cost,"+
					"PaymentMoney3 = PaymentMoney3 + @cost,"+
					"PaymentMoney4 = PaymentMoney4 + @cost,"+ 
					"PaymentMoney5 = PaymentMoney5 + @cost,"+
					"PaymentMoney6 = PaymentMoney6 + @cost,"+
					"PaymentMoney7 = PaymentMoney7 + @cost,"+
					"PaymentMoney8 = PaymentMoney8 + @cost,"+
					"PaymentMoney9 = PaymentMoney9 + @cost,"+
					"PaymentMoney10 = PaymentMoney10 + @cost,"+
					"PaymentMoney11 = PaymentMoney11 + @cost,"+
					"PaymentMoney12 = PaymentMoney12 + @cost,"+
					"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
					"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
					"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
					"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+ 
					"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
					"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
					"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
					"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
					"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
					"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
					"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
					"UnPaymentMoney12 = UnPaymentMoney12 - @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}


			return str;
		}



		/// <summary>
		/// 매입매출테이블 지급금액 감소, 미지급액 증가
		/// </summary>
		/// <returns></returns>
		public string UNCollectMoneyDiminutionPaymentMoneyIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney1 = PaymentMoney1 - @cost,"+
						"PaymentMoney2 = PaymentMoney2 - @cost,"+
						"PaymentMoney3 = PaymentMoney3 - @cost,"+
						"PaymentMoney4 = PaymentMoney4 - @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 - @cost,"+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney1 = UnPaymentMoney1 + @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney2 = PaymentMoney2 - @cost,"+
						"PaymentMoney3 = PaymentMoney3 - @cost,"+
						"PaymentMoney4 = PaymentMoney4 - @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 - @cost,"+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney3 = PaymentMoney3 - @cost,"+
						"PaymentMoney4 = PaymentMoney4 - @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 - @cost,"+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney4 = PaymentMoney4 - @cost,"+ 
						"PaymentMoney5 = PaymentMoney5 - @cost,"+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney5 = PaymentMoney5 - @cost,"+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney6 = PaymentMoney6 - @cost,"+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney7 = PaymentMoney7 - @cost,"+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney8 = PaymentMoney8 - @cost,"+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney9 = PaymentMoney9 - @cost,"+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney10 = PaymentMoney10 - @cost,"+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney11 = PaymentMoney11 - @cost,"+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"PaymentMoney12 = PaymentMoney12 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"PaymentMoney1 = PaymentMoney1 - @cost,"+
					"PaymentMoney2 = PaymentMoney2 - @cost,"+
					"PaymentMoney3 = PaymentMoney3 - @cost,"+
					"PaymentMoney4 = PaymentMoney4 - @cost,"+ 
					"PaymentMoney5 = PaymentMoney5 - @cost,"+
					"PaymentMoney6 = PaymentMoney6 - @cost,"+
					"PaymentMoney7 = PaymentMoney7 - @cost,"+
					"PaymentMoney8 = PaymentMoney8 - @cost,"+
					"PaymentMoney9 = PaymentMoney9 - @cost,"+
					"PaymentMoney10 = PaymentMoney10 - @cost,"+
					"PaymentMoney11 = PaymentMoney11 - @cost,"+
					"PaymentMoney12 = PaymentMoney12 - @cost,"+
					"UnPaymentMoney1 = UnPaymentMoney1 + @cost,"+
					"UnPaymentMoney2 = UnPaymentMoney2 + @cost,"+
					"UnPaymentMoney3 = UnPaymentMoney3 + @cost,"+
					"UnPaymentMoney4 = UnPaymentMoney4 + @cost,"+ 
					"UnPaymentMoney5 = UnPaymentMoney5 + @cost,"+
					"UnPaymentMoney6 = UnPaymentMoney6 + @cost,"+
					"UnPaymentMoney7 = UnPaymentMoney7 + @cost,"+
					"UnPaymentMoney8 = UnPaymentMoney8 + @cost,"+
					"UnPaymentMoney9 = UnPaymentMoney9 + @cost,"+
					"UnPaymentMoney10 = UnPaymentMoney10 + @cost,"+
					"UnPaymentMoney11 = UnPaymentMoney11 + @cost,"+
					"UnPaymentMoney12 = UnPaymentMoney12 + @cost "+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}






















		/// <summary>
		/// 영업창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutBusinessTable()
		{
			
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE BS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = "+a+"";
			}



			return str;

		}
		
		/// <summary>
		/// 영업창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InBusinessTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity,"
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE BS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = "+a+"";
			}

			return str;
		}

		/// <summary>
		/// 납품창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutDeliveryTable()
		{
			
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE DS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE DS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and [Year] = "+a+"";
			}

			return str;

		}
		
		/// <summary>
		/// 납품창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InDeliveryTable()
		{
					
			switch(mon)
			{
				case 1 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE DS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE DS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and [Year] = "+a+"";
			}

			return str;
		}


		/// <summary>
		/// 원자재창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutRowTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE RMS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000'  and [Year] = "+a+"";
			}

			return str;

		}
		
		/// <summary>
		/// 원자재창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InRowTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE RMS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE RMS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000'  and [Year] = "+a+"";
			}

			return str;
		}



		/// <summary>
		/// 생산창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutProductionTable()
		{
			switch(mon)
			{
				case 1 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE PS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE PS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code  and [Year] = "+a+"";
			}

			return str;

		}
		
		/// <summary>
		/// 생산창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InProductionTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE PS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE PS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code  and [Year] = "+a+"";
			}

			return str;
		}



		/// <summary>
		/// 외주창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE OS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE OS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;

		}
		
		/// <summary>
		/// 외주창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE OS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE OS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}


		/// <summary>
		/// 클레임금액 증가
		/// 클레임금액은 등록되면 매출테이블에서 금액이 감소된다
		/// </summary>
		/// <returns></returns>
		public string ClaimMoneyIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost1 = TotalSaleCost1 - @cost,"+
						"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney1 = UncollectMoney1 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney2 = UncollectMoney2 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney3 = UncollectMoney3 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney4 = UncollectMoney4 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney5 = UncollectMoney5 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney6 = UncollectMoney6 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney7 = UncollectMoney7 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney8 = UncollectMoney8 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney9 = UncollectMoney9 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
						"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney10 = UncollectMoney10 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost11 = TotalSaleCost11 - @cost, "+
						"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
						"UncollectMoney11 = UncollectMoney11 - @cost,"+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalSaleCost12 = TotalSaleCost12 - @cost, "+
						"UncollectMoney12 = UncollectMoney12 - @cost"+
						"WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalSaleCost1 = TotalSaleCost1 - @cost,"+
					"TotalSaleCost2 = TotalSaleCost2 - @cost,"+
					"TotalSaleCost3 = TotalSaleCost3 - @cost,"+
					"TotalSaleCost4 = TotalSaleCost4 - @cost,"+ 
					"TotalSaleCost5 = TotalSaleCost5 - @cost,"+
					"TotalSaleCost6 = TotalSaleCost6 - @cost,"+
					"TotalSaleCost7 = TotalSaleCost7 - @cost,"+
					"TotalSaleCost8 = TotalSaleCost8 - @cost,"+
					"TotalSaleCost9 = TotalSaleCost9 - @cost,"+
					"TotalSaleCost10 = TotalSaleCost10 - @cost,"+
					"TotalSaleCost11 = TotalSaleCost11 - @cost,"+
					"TotalSaleCost12 = TotalSaleCost12 - @cost,"+
					"UncollectMoney1 = UncollectMoney1 - @cost,"+
					"UncollectMoney2 = UncollectMoney2 - @cost,"+
					"UncollectMoney3 = UncollectMoney3 - @cost,"+
					"UncollectMoney4 = UncollectMoney4 - @cost,"+
					"UncollectMoney5 = UncollectMoney5 - @cost,"+
					"UncollectMoney6 = UncollectMoney6 - @cost,"+
					"UncollectMoney7 = UncollectMoney7 - @cost,"+
					"UncollectMoney8 = UncollectMoney8 - @cost,"+
					"UncollectMoney9 = UncollectMoney9 - @cost,"+
					"UncollectMoney10 = UncollectMoney10 - @cost,"+
					"UncollectMoney11 = UncollectMoney11 - @cost,"+
					"UncollectMoney12 = UncollectMoney12 - @cost"+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}

			return str;
		}


		/// <summary>
		/// 지급 클레임금액 증가
		/// 지급 클레임금액은 등록되면 매입테이블에서 미지급금액이 감소된다
		/// </summary>
		/// <returns></returns>
		public string PayClaimMoneyIncreaseTable()
		{
			switch(mon)
			{
				case 1:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost1 = TotalBuyingCost1 - @cost,"+
						"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 2:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 3:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 4:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 5:
					str = "UPDATE BSI_MT SET "+ 
						"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 6:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 7:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 8:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 9:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 10:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 11:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost11 = TotalBuyingCost11 - @cost, "+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
						"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				case 12:
					str = "UPDATE BSI_MT SET "+
						"TotalBuyingCost12 = TotalBuyingCost12 - @cost, "+
						"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
						" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE BSI_MT SET "+
					"TotalBuyingCost1 = TotalBuyingCost1 - @cost,"+
					"TotalBuyingCost2 = TotalBuyingCost2 - @cost,"+
					"TotalBuyingCost3 = TotalBuyingCost3 - @cost,"+
					"TotalBuyingCost4 = TotalBuyingCost4 - @cost,"+ 
					"TotalBuyingCost5 = TotalBuyingCost5 - @cost,"+
					"TotalBuyingCost6 = TotalBuyingCost6 - @cost,"+
					"TotalBuyingCost7 = TotalBuyingCost7 - @cost,"+
					"TotalBuyingCost8 = TotalBuyingCost8 - @cost,"+
					"TotalBuyingCost9 = TotalBuyingCost9 - @cost,"+
					"TotalBuyingCost10 = TotalBuyingCost10 - @cost,"+
					"TotalBuyingCost11 = TotalBuyingCost11 - @cost,"+
					"TotalBuyingCost12 = TotalBuyingCost12 - @cost,"+
					"UnPaymentMoney1 = UnPaymentMoney1 - @cost,"+
					"UnPaymentMoney2 = UnPaymentMoney2 - @cost,"+
					"UnPaymentMoney3 = UnPaymentMoney3 - @cost,"+
					"UnPaymentMoney4 = UnPaymentMoney4 - @cost,"+
					"UnPaymentMoney5 = UnPaymentMoney5 - @cost,"+
					"UnPaymentMoney6 = UnPaymentMoney6 - @cost,"+
					"UnPaymentMoney7 = UnPaymentMoney7 - @cost,"+
					"UnPaymentMoney8 = UnPaymentMoney8 - @cost,"+
					"UnPaymentMoney9 = UnPaymentMoney9 - @cost,"+
					"UnPaymentMoney10 = UnPaymentMoney10 - @cost,"+
					"UnPaymentMoney11 = UnPaymentMoney11 - @cost,"+
					"UnPaymentMoney12 = UnPaymentMoney12 - @cost"+
					" WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = "+a+"";
			}
			return str;
		}


		/// <summary>
		/// 보용품창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutAddItemTable()
		{
			
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 - @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 - @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num   and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity2 = OutStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 - @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost2 = OutStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 - @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity3 = OutStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 - @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost3 = OutStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 - @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity4 = OutStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 - @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost4 = OutStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 - @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity5 = OutStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 - @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost5 = OutStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 - @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity6 = OutStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 - @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost6 = OutStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 - @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity7 = OutStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 - @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost7 = OutStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 - @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity8 = OutStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 - @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost8 = OutStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 - @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity9 = OutStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 - @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost9 = OutStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 - @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity10 = OutStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 - @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost10 = OutStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 - @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity11 = OutStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 - @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost11 = OutStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 - @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE AS_MT SET "
						+ "OutStorehouseQuantity12 = OutStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 - @quantity, "
						+ "OutStorehouseCost12 = OutStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 - @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE AS_MT SET "
					//+ "OutStorehouseQuantity1 = OutStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 - @quantity, "
					+ "StockQuantity2 = StockQuantity2 - @quantity, "
					+ "StockQuantity3 = StockQuantity3 - @quantity, "
					+ "StockQuantity4 = StockQuantity4 - @quantity, "
					+ "StockQuantity5 = StockQuantity5 - @quantity, "
					+ "StockQuantity6 = StockQuantity6 - @quantity, "
					+ "StockQuantity7 = StockQuantity7 - @quantity, "
					+ "StockQuantity8 = StockQuantity8 - @quantity, "
					+ "StockQuantity9 = StockQuantity9 - @quantity, "
					+ "StockQuantity10 = StockQuantity10 - @quantity, "
					+ "StockQuantity11 = StockQuantity11 - @quantity, "
					+ "StockQuantity12 = StockQuantity12 - @quantity, "
					//+ "OutStorehouseCost1 = OutStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 - @cost, "
					+ "StockCost2 = StockCost2 - @cost, "
					+ "StockCost3 = StockCost3 - @cost, "
					+ "StockCost4 = StockCost4 - @cost, "
					+ "StockCost5 = StockCost5 - @cost, "
					+ "StockCost6 = StockCost6 - @cost, "
					+ "StockCost7 = StockCost7 - @cost, "
					+ "StockCost8 = StockCost8 - @cost, "
					+ "StockCost9 = StockCost9 - @cost, "
					+ "StockCost10 = StockCost10 - @cost, "
					+ "StockCost11 = StockCost11 - @cost, "
					+ "StockCost12 = StockCost12 - @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = "+a+"";
			}



			return str;

		}
		
		/// <summary>
		/// 보용품 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InAddItemTable()
		{
			
			switch(mon)
			{
				case 1 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
						+ "StockQuantity1 = StockQuantity1 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
						+ "StockCost1 = StockCost1 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 2 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity2 = InStorehouseQuantity2 + @quantity, "
						+ "StockQuantity2 = StockQuantity2 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost2 = InStorehouseCost2 + @cost, "
						+ "StockCost2 = StockCost2 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "		
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 3 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity3 = InStorehouseQuantity3 + @quantity, "
						+ "StockQuantity3 = StockQuantity3 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost3 = InStorehouseCost3 + @cost, "
						+ "StockCost3 = StockCost3 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 4 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity4 = InStorehouseQuantity4 + @quantity, "
						+ "StockQuantity4 = StockQuantity4 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost4 = InStorehouseCost4 + @cost, "
						+ "StockCost4 = StockCost4 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 5 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity5 = InStorehouseQuantity5 + @quantity, "
						+ "StockQuantity5 = StockQuantity5 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost5 = InStorehouseCost5 + @cost, "
						+ "StockCost5 = StockCost5 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 6 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity6 = InStorehouseQuantity6 + @quantity, "
						+ "StockQuantity6 = StockQuantity6 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost6 = InStorehouseCost6 + @cost, "
						+ "StockCost6 = StockCost6 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 7 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity7 = InStorehouseQuantity7 + @quantity, "
						+ "StockQuantity7 = StockQuantity7 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost7 = InStorehouseCost7 + @cost, "
						+ "StockCost7 = StockCost7 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 8 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity8 = InStorehouseQuantity8 + @quantity, "
						+ "StockQuantity8 = StockQuantity8 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost8 = InStorehouseCost8 + @cost, "
						+ "StockCost8 = StockCost8 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 9 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity9 = InStorehouseQuantity9 + @quantity, "
						+ "StockQuantity9 = StockQuantity9 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost9 = InStorehouseCost9 + @cost, "
						+ "StockCost9 = StockCost9 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num and [Year] = @year";
					break;
				case 10 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity10 = InStorehouseQuantity10 + @quantity, "
						+ "StockQuantity10 = StockQuantity10 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity,"
						+ "InStorehouseCost10 = InStorehouseCost10 + @cost, "
						+ "StockCost10 = StockCost10 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 11 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity11 = InStorehouseQuantity11 + @quantity, "
						+ "StockQuantity11 = StockQuantity11 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost11 = InStorehouseCost11 + @cost, "
						+ "StockCost11 = StockCost11 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				case 12 :
					str = "UPDATE AS_MT SET "
						+ "InStorehouseQuantity12 = InStorehouseQuantity12 + @quantity, "
						+ "StockQuantity12 = StockQuantity12 + @quantity, "
						+ "InStorehouseCost12 = InStorehouseCost12 + @cost, "
						+ "StockCost12 = StockCost12 + @cost "
						+ " WHERE RecodingState = 1 and  ItemNum = @num  and [Year] = @year";
					break;
				default:
					break;
			}

			for(int a = year+1; a<= DateTime.Now.Year; a++)
			{
				str += "; UPDATE AS_MT SET "
					//+ "InStorehouseQuantity1 = InStorehouseQuantity1 + @quantity, "
					+ "StockQuantity1 = StockQuantity1 + @quantity, "
					+ "StockQuantity2 = StockQuantity2 + @quantity, "
					+ "StockQuantity3 = StockQuantity3 + @quantity, "
					+ "StockQuantity4 = StockQuantity4 + @quantity, "
					+ "StockQuantity5 = StockQuantity5 + @quantity, "
					+ "StockQuantity6 = StockQuantity6 + @quantity, "
					+ "StockQuantity7 = StockQuantity7 + @quantity, "
					+ "StockQuantity8 = StockQuantity8 + @quantity, "
					+ "StockQuantity9 = StockQuantity9 + @quantity, "
					+ "StockQuantity10 = StockQuantity10 + @quantity, "
					+ "StockQuantity11 = StockQuantity11 + @quantity, "
					+ "StockQuantity12 = StockQuantity12 + @quantity, "
					//+ "InStorehouseCost1 = InStorehouseCost1 + @cost, "
					+ "StockCost1 = StockCost1 + @cost, "
					+ "StockCost2 = StockCost2 + @cost, "
					+ "StockCost3 = StockCost3 + @cost, "
					+ "StockCost4 = StockCost4 + @cost, "
					+ "StockCost5 = StockCost5 + @cost, "
					+ "StockCost6 = StockCost6 + @cost, "
					+ "StockCost7 = StockCost7 + @cost, "
					+ "StockCost8 = StockCost8 + @cost, "
					+ "StockCost9 = StockCost9 + @cost, "
					+ "StockCost10 = StockCost10 + @cost, "
					+ "StockCost11 = StockCost11 + @cost, "
					+ "StockCost12 = StockCost12 + @cost "
					+ " WHERE RecodingState = 1 and  ItemNum = @num and [Year] = "+a+"";
			}

			return str;
		}
	}
}
