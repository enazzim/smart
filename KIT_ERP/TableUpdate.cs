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
	public class TableUpdate
	{
		private string str ;
		private int mon = DateTime.Now.Month;
		private int year = DateTime.Now.Year;
				
		public TableUpdate()
		{	
		}
		public TableUpdate(int a)
		{	
			mon = a;
		}

		/// <summary>
		/// 테이블 수정 생성자
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public TableUpdate(int a,int b)
		{
			year = a;
			mon = b;
		}


		/// <summary>
		/// 매입매출테이블 매입금액 증가
		/// </summary>
		/// <returns></returns>
		public string PaymentIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "BuyingCost"+mon+" = BuyingCost"+mon+" + @cost, "
				+ "TotalBuyingCost = TotalBuyingCost + @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}

		/// <summary>
		/// 매입매출테이블 매입금액 감소
		/// </summary>
		/// <returns></returns>
		public string PaymentDiminutionTable()
		{
			str = "Update BSI_MT SET "
				+ "BuyingCost"+mon+" = BuyingCost"+mon+" - @cost, "
				+ "TotalBuyingCost = TotalBuyingCost - @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";
			
			return str;
		}


		/// <summary>
		/// 매입매출테이블 매출금액 증가
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "SaleCost"+mon+" = SaleCost"+mon+" + @cost, "
				+ "TotalSaleCost = TotalSaleCost + @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
			
		}

		/// <summary>
		/// 매입매출테이블 매출금액 감소
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyDiminutionTable()
		{
			str = "Update BSI_MT SET "
				+ "SaleCost"+mon+" = SaleCost"+mon+" - @cost, "
				+ "TotalSaleCost = TotalSaleCost - @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}


		/// <summary>
		/// 매입매출테이블 수금액증가
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyIncreaseUnCollectMoneyDiminutionTable()
		{
			str = "Update BSI_MT SET "
				+ "CollectMoney"+mon+" = CollectMoney"+mon+" + @cost, "
				+ "TotalCollectMoney = TotalCollectMoney + @cost "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}

		/// <summary>
		/// 매입매출테이블 수금액감소
		/// </summary>
		/// <returns></returns>
		public string CollectMoneyDiminutionUnCollectMoneyIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "CollectMoney"+mon+" = CollectMoney"+mon+" - @cost, "
				+ "TotalCollectMoney = TotalCollectMoney - @cost "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}


		/// <summary>
		/// 매입매출테이블 지급금액 증가
		/// </summary>
		/// <returns></returns>
		public string UNCollectMoneyIncreasePaymentMoneyDiminutionTable()
		{
			str = "Update BSI_MT SET "
				+ "PaymentMoney"+mon+" = PaymentMoney"+mon+" + @cost, "
				+ "TotalPaymentMoney = TotalPaymentMoney + @cost "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}

		/// <summary>
		/// 매입매출테이블 지급금액 감소
		/// </summary>
		/// <returns></returns>
		public string UNCollectMoneyDiminutionPaymentMoneyIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "PaymentMoney"+mon+" = PaymentMoney"+mon+" - @cost, "
				+ "TotalPaymentMoney = TotalPaymentMoney - @cost "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}


		/// <summary>
		/// 클레임금액 증가
		/// 클레임금액은 등록되면 매출테이블에서 총매출금액이 감소된다
		/// </summary>
		/// <returns></returns>
		public string ClaimMoneyIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "ClameCost"+mon+" = ClameCost"+mon+" + @cost, "
				+ "TotalSaleCost = TotalSaleCost - @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}


		/// <summary>
		/// 지급 클레임금액 증가
		/// 지급 클레임금액은 등록되면 매입테이블에서 총매입금액이감소된다
		/// </summary>
		/// <returns></returns>
		public string PayClaimMoneyIncreaseTable()
		{
			str = "Update BSI_MT SET "
				+ "PayClameCost"+mon+" = PayClameCost"+mon+" + @cost, "
				+ "TotalBuyingCost = TotalBuyingCost - @cost, "
				+ "WHERE RecodingState = 1 and  BusinessRegistrationNum = @comnum and [Year] = @year";		
			
			return str;
		}


		/// <summary>
		/// 영업창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutBusinessTable()
		{
			str = "UPDATE BS_MT SET "
				+ "OutStorehouseQuantity"+mon+" = OutStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" - @quantity, ";
			}
			
			str += "OutStorehouseCost"+mon+" = OutStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" - @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year ";

			return str;
			
		}
		
		/// <summary>
		/// 영업창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InBusinessTable()
		{
			str = "UPDATE BS_MT SET "
				+ "InStorehouseQuantity"+mon+" = InStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" + @quantity, ";
			}
			
			str += "InStorehouseCost"+mon+" = InStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" + @cost, ";
			}

			str += "  WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999'  and BusinessStorehouseNum = @storenum and [Year] = @year  ";

			return str;
			
			
		}


		
		/// <summary>
		/// 납품창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutDeliveryTable()
		{
			str = "UPDATE DS_MT SET "
				+ "OutStorehouseQuantity"+mon+" = OutStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" - @quantity, ";
			}
			
			str += "OutStorehouseCost"+mon+" = OutStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" - @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year ";

			return str;
			
		}
		
		/// <summary>
		/// 납품창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InDeliveryTable()
		{

			str = "UPDATE DS_MT SET "
				+ "InStorehouseQuantity"+mon+" = InStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" + @quantity, ";
			}
			
			str += "InStorehouseCost"+mon+" = InStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" + @cost, ";
			}

			str += "  WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14009999' and [Year] = @year  ";

			return str;
		}



		/// <summary>
		/// 원자재창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutRowTable()
		{
			str = "UPDATE RMS_MT SET "
						+ "OutStorehouseQuantity"+mon+" = OutStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=1 ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" - @quantity, ";
			}
			
			str += "OutStorehouseCost"+mon+" = OutStorehouseCost"+mon+" + @cost, ";

			for(int a=1 ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" - @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year ";

			return str;

		}
		
		/// <summary>
		/// 원자재창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InRowTable()
		{
			str = "UPDATE RMS_MT SET "
				+ "InStorehouseQuantity"+mon+" = InStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" + @quantity, ";
			}
			
			str += "InStorehouseCost"+mon+" = InStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" + @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = '14000000' and [Year] = @year  ";

			return str;
		}



		/// <summary>
		/// 생산창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutProductionTable()
		{

			str = "UPDATE PS_MT SET "
				+ "OutStorehouseQuantity"+mon+" = OutStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=1 ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" - @quantity, ";
			}
			
			str += "OutStorehouseCost"+mon+" = OutStorehouseCost"+mon+" + @cost, ";

			for(int a=1 ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" - @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year ";

			return str;
		}
		
		/// <summary>
		/// 생산창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InProductionTable()
		{
			str = "UPDATE PS_MT SET "
				+ "InStorehouseQuantity"+mon+" = InStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" + @quantity, ";
			}
			
			str += "InStorehouseCost"+mon+" = InStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" + @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and [Year] = @year ";

			return str;
			
		}



		/// <summary>
		/// 외주창고 출고수량 증가
		/// </summary>
		/// <returns></returns>
		public string OutTable()
		{
			str = "UPDATE OS_MT SET "
				+ "OutStorehouseQuantity"+mon+" = OutStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=1 ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" - @quantity, ";
			}
			
			str += "OutStorehouseCost"+mon+" = OutStorehouseCost"+mon+" + @cost, ";

			for(int a=1 ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" - @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year ";

			return str;

		}
		
		/// <summary>
		/// 외주창고 입고수량 증가
		/// </summary>
		/// <returns></returns>
		public string InTable()
		{

			str = "UPDATE OS_MT SET "
				+ "InStorehouseQuantity"+mon+" = InStorehouseQuantity"+mon+" + @quantity, ";
			for(int a=mon ; a<= 12; a++)
			{
				str += "StockQuantity"+a+" = StockQuantity"+a+" + @quantity, ";
			}
			
			str += "InStorehouseCost"+mon+" = InStorehouseCost"+mon+" + @cost, ";

			for(int a=mon ; a<= 12; a++)
			{
				str += "StockCost"+a+" = StockCost"+a+" + @cost, ";
			}

			str += " WHERE RecodingState = 1 and  ItemNum = @num AND ProcessCode = @code and BusinessRegistrationNum = @comnum  and [Year] = @year ";

			return str;
		}
		
	}
}
