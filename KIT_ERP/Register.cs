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
	/// 등록을 위한 객체
	/// </summary>
	public class Register
	{
		
		private string m_PageName;				//요청페이지 담는 필드
		private string m_Table;					//넘어온 테이블 담는 필드
		private string m_User  ;				//사용자 아이디 저장
		private string m_UserName  ;			//사용자 이름 저장
		private string m_Distinction ;			//업무구분(모듈구분)
		private DataSet m_DataSet;				//넘어온 데이터셋 담는 필드

		private string m_ProductionDate;		// 생산시작일 담는 필드
		private string DeliveryDate;			// 납기요구일 담는 필드
		private int vol = 0;					//볼륨번호 담는 필드
		private int volum = 0;					//생산계획원장내의 자재소요산출에 필요한 볼륨번호 담는 필드
		private UltraWebGrid m_grid;			
		private UltraWebGrid m_grid1;
		private string m_Division;				//협력사 구매,외주납품의뢰시 구분자

		private int StoreNum;					//창고번호
		private string m_Date;					//투입일자

		/// <summary>
		/// 작업일보등록시 사용할 필드
		/// </summary>
		private string Child = "";				//창고출고품목 담는 필드
		private decimal NeedQuantity=0;			//창고출고량 담는 필드
		private decimal Cost=0;					//창고출고금액 담는 필드
		private decimal Rate = 100;				//진척율 담는 필드
		private int sequence = 0;				//공정순서 담는 필드

		ArrayList m_List = new ArrayList();		//선택된 그리드 row값을 맨마지막에 넣어서 가져온다
		ArrayList m_List1 = new ArrayList();	//작업일보 공구치구값 넣어두는 곳
		ArrayList alist = new ArrayList();		//구매납품,외주납품에서 필요한 값을 넣어두는 곳

		/// <summary>
		/// 협력사 구매(외주)납품의뢰
		/// </summary>
		/// <param name="a">구매,외주구분</param>
		/// <param name="arr"></param>
		/// <param name="UserID">사용자ID</param>
		public Register( string a, ArrayList arr,string UserID)
		{
			m_List = arr;
			m_Division = a;
			m_User = UserID;
			m_Distinction = "협력";
			m_PageName = "OrderPC";

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();

		}

		/// <summary>
		/// 하위품목 투입생성자
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="date"></param>
		/// <param name="storeNum"></param>
		public Register(ArrayList arr,WebDateChooser date, int storeNum, string id, string name)
		{
			m_List = arr;
			m_User = id;
			m_UserName = name;
			m_Distinction = "생산";
			m_Date = date.Text;
			StoreNum = storeNum;
		}


		

		/// <summary>
		/// 자재소요량산출 페이지 생성자
		/// </summary>
		/// <param name="grid"></param>
		/// <param name="grid2"></param>
		/// <param name="RequestPage"></param>
		/// <param name="User"></param>
		public Register(UltraWebGrid grid, UltraWebGrid grid2,string RequestPage, string User, WebDateChooser wdc)
		{
			m_grid = grid;
			m_grid1 = grid2;
			m_PageName = RequestPage;
			m_User = User;
			m_Table = "MRC_HT";
			m_Distinction = "생산";
			m_Date = wdc.Text.Trim();

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();

			

			//자재소요원장 볼륨
			str = "Select Max(VolumNum) From "+m_Table.ToString()+"";											/////////////////////////////
			SqlCommand comm4 = new SqlCommand(str,conn);														// 볼륨번호 저장		무조건 저장해야한다
			if(m_grid.Rows.Count==0 )																			//				하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
				vol = 0;																						/////////////////////////////
			else if(comm4.ExecuteScalar() == Convert.DBNull || comm4.ExecuteScalar().ToString() == "")
				vol = 0;
			else
				vol = int.Parse(comm4.ExecuteScalar().ToString());	
						
					
			
			//생산계획원장내에있는 자재소요산출에 필요한 볼륨번호
			string str_vol = "Select Max(MaterialRequirementVolumNum) From PP_HT";
			SqlCommand comm_vol = new SqlCommand(str_vol, conn);
			if(comm_vol.ExecuteScalar() == Convert.DBNull || comm_vol.ExecuteScalar().ToString() == "")
				volum = 0;
			else
				volum = Convert.ToInt32(comm_vol.ExecuteScalar());


			conn.Close();

		}

		/// <summary>
		/// 그리드만 넘겨받아 등록하는 생성자
		/// 생산의뢰 페이지, 상품구매의뢰페이지,원자재구매의뢰페이지, 
		/// 외주출고 페이지, 영업창고입고 페이지
		/// </summary>
		/// <param name="grid">넘겨줄 그리드</param>
		/// <param name="RequestPage">요청페이지명</param>
		/// <param name="User">사용자ID</param>
		public Register(UltraWebGrid grid, string RequestPage, string UserID)
		{
			m_grid = grid;
			m_PageName = RequestPage;
			m_User = UserID;

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();


			vol = 0;
			switch(m_PageName)
			{
				case "ProductionRequest":					//생산의뢰
					m_Table = "PR_HT";
					m_Distinction = "영업";

					str = "Select Max(VolumNum) From "+m_Table.ToString()+"";								/////////////////////////////
					SqlCommand comm1 = new SqlCommand(str,conn);											// 볼륨번호 저장		무조건 저장해야한다
					if(m_grid.Rows.Count==0)																//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
						vol = 0;																			/////////////////////////////
					else if(Convert.IsDBNull(comm1.ExecuteScalar()) == false || comm1.ExecuteScalar().ToString() != "")		
						vol = int.Parse(comm1.ExecuteScalar().ToString());
					conn.Close();
					break;
				case "GoodsBuyingRequest":					//상품구매의뢰
					m_Table = "BR_HT";
					m_Distinction = "영업";

					str = "Select Max(VolumNum) From "+m_Table.ToString()+"";								/////////////////////////////
					SqlCommand comm2 = new SqlCommand(str,conn);											// 볼륨번호 저장		무조건 저장해야한다
					if(m_grid.Rows.Count==0)																//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
						vol = 0;																			/////////////////////////////
					else if(comm2.ExecuteScalar() != Convert.DBNull || comm2.ExecuteScalar().ToString() != "")		
						vol = int.Parse(comm2.ExecuteScalar().ToString());									
					conn.Close();
					break;
				case "RowMaterialRequriement":				//원자재구매의뢰
					m_Table = "BR_HT";
					m_Distinction = "생산";

					str = "Select Max(VolumNum) From "+m_Table.ToString()+"";								/////////////////////////////
					SqlCommand comm3 = new SqlCommand(str,conn);											// 볼륨번호 저장		무조건 저장해야한다
					if(m_grid.Rows.Count==0)																//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
						vol = 0;																			/////////////////////////////
					else if(comm3.ExecuteScalar() != Convert.DBNull || comm3.ExecuteScalar().ToString() != "")		
						vol = int.Parse(comm3.ExecuteScalar().ToString());									
					conn.Close();
					break;
				case "OutSideOutStorehouse":				//외주 출고
					m_Table = "OOS_HT";
					m_Distinction = "외주";
					break;
				case "BusinessStorehouseInStorehouse":		//영업창고입고
					m_Table = "IS_HT";
					m_Distinction = "영업";
					break;
				default :
					break;

			}
		}
        

		
		/// <summary>
		/// 구매납품 페이지 생성자, 외주납품페이지 생성자
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="PageName"></param>
		/// <param name="UserID"></param>
		public Register(ArrayList arr,string PageName, string UserID)
		{
			m_List = arr;
			m_PageName = PageName;
			m_User = UserID;

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();

			switch(m_PageName)
			{
				case "BuyingDelivery":						// 구매납품
				{
					m_Distinction = "구매";

					if(m_List[0].ToString() == "0")
					{
						string str_BO = "Select * From BO_HT where BuyingOrderHistoryIndex = @index";
						SqlCommand comm_BO = new SqlCommand(str_BO,conn);
						comm_BO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
						SqlDataReader dr_BO = comm_BO.ExecuteReader();
						while(dr_BO.Read())
						{
							alist.Add(dr_BO["ItemNum"].ToString());
							alist.Add(dr_BO["ItemDrawNum"].ToString());
							alist.Add(dr_BO["ItemName"].ToString());
							alist.Add(dr_BO["CompanyName"].ToString());
							alist.Add(dr_BO["BusinessRegistrationNum"].ToString());
							alist.Add(decimal.Parse(dr_BO["ApplyUnitCost"].ToString()));
							alist.Add(int.Parse(dr_BO["BuyingOrderHistoryIndex"].ToString()));
						}
						dr_BO.Close();
					}
					else
					{
						string str_BDR = "Select * From BDR_HT where BuyingDeliveryRequestHistoryIndex = @index";
						SqlCommand comm_BDR = new SqlCommand(str_BDR,conn);
						comm_BDR.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
						SqlDataReader dr_BDR = comm_BDR.ExecuteReader();
						while(dr_BDR.Read())
						{
							alist.Add(dr_BDR["ItemNum"].ToString());
							alist.Add(dr_BDR["ItemDrawNum"].ToString());
							alist.Add(dr_BDR["ItemName"].ToString());
							alist.Add(dr_BDR["CompanyName"].ToString());
							alist.Add(dr_BDR["BusinessRegistrationNum"].ToString());
							alist.Add(decimal.Parse(dr_BDR["ApplyUnitCost"].ToString()));
							alist.Add(int.Parse(dr_BDR["BuyingOrderHistoryIndex"].ToString()));
							alist.Add(int.Parse(dr_BDR["BuyingDeliveryRequestHistoryIndex"].ToString()));
						}
						dr_BDR.Close();
					}

					break;
				}
				case "OutSideDelivery":						// 외주납품
				{
					
					m_Distinction = "외주";

					if(m_List[0].ToString() == "0")//외주발주원장에서 가져온 값이면
					{
						string str_OO = "Select * From OO_HT where OutSideOrderHistoryIndex = @index";
						SqlCommand comm_OO = new SqlCommand(str_OO,conn);
						comm_OO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
						SqlDataReader dr_OO = comm_OO.ExecuteReader();
						while(dr_OO.Read())
						{
							alist.Add(dr_OO["ItemNum"].ToString());
							alist.Add(dr_OO["ItemDrawNum"].ToString());
							alist.Add(dr_OO["ItemName"].ToString());
							
							alist.Add(dr_OO["BeginProcessCode"].ToString());
							alist.Add(dr_OO["BeginProcess"].ToString());
						
							alist.Add(dr_OO["EndProcessCode"].ToString());
							alist.Add(dr_OO["EndProcess"].ToString());
							alist.Add(dr_OO["CompanyName"].ToString());
							alist.Add(dr_OO["BusinessRegistrationNum"].ToString());
							alist.Add(decimal.Parse(dr_OO["ApplyUnitCost"].ToString()));
							alist.Add(int.Parse(dr_OO["OutSideOrderHistoryIndex"].ToString()));



							SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
							////////////////////////////////////////
							//  선택된 품목의 시작공정순서를 가져온다 //
							////////////////////////////////////////
							
							string str_pro = "Select * From PSI_MT where RecodingState = 1 and ProcessCode = @code and ItemNum = @num";
							SqlCommand comm_pro = new SqlCommand(str_pro,conn1);
							comm_pro.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
							comm_pro.Parameters.Add("@code",SqlDbType.VarChar).Value = alist[3].ToString();
							SqlDataAdapter da = new SqlDataAdapter(comm_pro);
							DataSet ds= new DataSet();
							da.Fill(ds);
							sequence = int.Parse(ds.Tables[0].Rows[0]["ProcessSequenceNum"].ToString());
							alist.Insert(3,sequence);						

							////////////////////////////////////////
							//  선택된 품목의 종료공정순서를 가져온다 //
							////////////////////////////////////////

							int sequence1=0;
							string str_pro1 = "Select * From PSI_MT where RecodingState = 1 and ProcessCode = @code and ItemNum = @num";
							SqlCommand comm_pro1 = new SqlCommand(str_pro1,conn1);
							comm_pro1.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
							comm_pro1.Parameters.Add("@code",SqlDbType.VarChar).Value = alist[6].ToString();
							SqlDataAdapter da1 = new SqlDataAdapter(comm_pro1);
							DataSet ds1= new DataSet();
							da1.Fill(ds1);
							sequence1 = int.Parse(ds1.Tables[0].Rows[0]["ProcessSequenceNum"].ToString());
							alist.Insert(6,sequence1);						

						}
						dr_OO.Close();
						
					}
					else//외주납품의뢰원장에서 자료를 가져온것이다.
					{
						string str_OOR = "Select * From ODR_HT where OutSideDeliveryRequestHistoryIndex = @index";
						SqlCommand comm_OOR = new SqlCommand(str_OOR,conn);
						comm_OOR.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
						SqlDataReader dr_OOR = comm_OOR.ExecuteReader();
						while(dr_OOR.Read())
						{
							alist.Add(dr_OOR["ItemNum"].ToString());
							alist.Add(dr_OOR["ItemDrawNum"].ToString());
							alist.Add(dr_OOR["ItemName"].ToString());
							alist.Add(dr_OOR["BeginProcessSequenceNum"].ToString());//시작공정순서
							alist.Add(dr_OOR["BeginProcessCode"].ToString());				//시작공정코드
							alist.Add(dr_OOR["BeginProcessName"].ToString());				//시작공정명
							alist.Add(dr_OOR["EndProcessSequenceNum"].ToString());	//종료공정순서
							alist.Add(dr_OOR["EndProcessCode"].ToString());					//종료공정코드
							alist.Add(dr_OOR["EndProcessName"].ToString());				//종료공정명		
							alist.Add(dr_OOR["CompanyName"].ToString());
							alist.Add(dr_OOR["BusinessRegistrationNum"].ToString());
							alist.Add(decimal.Parse(dr_OOR["ApplyUnitCost"].ToString()));
							alist.Add(int.Parse(dr_OOR["OutSideOrderHistoryIndex"].ToString()));
							alist.Add(int.Parse(dr_OOR["OutSideDeliveryRequestHistoryIndex"].ToString()));
						}
						dr_OOR.Close();
					}

					break;
				}
				case "SubBuyingDelivery":
				{
					string str_SBO = "Select * From SBO_HT where SubBuyingOrderHistoryIndex = @index";
					SqlCommand comm_SBO = new SqlCommand(str_SBO,conn);
					comm_SBO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[0].ToString());
					SqlDataReader dr_SBO = comm_SBO.ExecuteReader();
					while(dr_SBO.Read())
					{
						alist.Add(dr_SBO["ItemNum"].ToString());
						alist.Add(dr_SBO["ItemDrawNum"].ToString());
						alist.Add(dr_SBO["ItemName"].ToString());
						alist.Add(dr_SBO["CompanyName"].ToString());
						alist.Add(dr_SBO["BusinessRegistrationNum"].ToString());
						alist.Add(decimal.Parse(dr_SBO["ApplyUnitCost"].ToString()));
						alist.Add(int.Parse(dr_SBO["SubBuyingOrderHistoryIndex"].ToString()));
					}
					dr_SBO.Close();
					m_Distinction = "구매";
					break;
				}

				case "OutReceiveingOutStorehose":
					m_Distinction = "영업";
					break;

				default :
					m_Distinction = "외주";
					break;
			}

			conn.Close();

		}

		/// <summary>
		/// 생산계획원장등록시 사용되는 생성자
		/// </summary>
		/// <param name="grid">넘겨줄 그리드</param>
		/// <param name="ProductionDate">생산시작일</param>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="User">사용자ID</param>
		public Register(UltraWebGrid grid, WebDateChooser ProductionDate , string RequestPage, string UserID)
		{
			m_grid = grid;
			m_ProductionDate = ProductionDate.Text;
			m_PageName = RequestPage;
			m_User = UserID;
			DeliveryDate = ProductionDate.Text;
		
			m_Distinction = "생산";

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();
			

			switch(m_PageName)
			{
				case "ProductionPlan":
				{
					m_Table = "PP_HT";

					/////////////////////////////
					// 볼륨번호 저장		무조건 저장해야한다
					//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
					/////////////////////////////
					str = "Select Max(VolumNum) From PP_HT";
					SqlCommand comm1 = new SqlCommand(str,conn);
					if(m_grid.Rows.Count==0 || comm1.ExecuteScalar() == Convert.DBNull || comm1.ExecuteScalar().ToString().Trim() == "")
						vol = 0;
					else
						vol = int.Parse(comm1.ExecuteScalar().ToString());
				}
					break;
				case "RowMaterialRequriement":				//원자재구매의뢰
				{
					m_Table = "BR_HT";
					
					str = "Select Max(VolumNum) From BR_HT";												/////////////////////////////
					SqlCommand comm3 = new SqlCommand(str,conn);											// 볼륨번호 저장		무조건 저장해야한다
					if(m_grid.Rows.Count==0)																//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
						vol = 0;																			/////////////////////////////
					else if(comm3.ExecuteScalar() != Convert.DBNull || comm3.ExecuteScalar().ToString() != "")		
						vol = int.Parse(comm3.ExecuteScalar().ToString());									
					
				}
					break;
				case "ExecutionPlanInfo":
				{
					m_Table = "PP_HT";

					/////////////////////////////
					// 볼륨번호 저장		무조건 저장해야한다
					//						하나씩 등록하므로 체크가 한개되었는지 두개이상인지 파악이 안됨
					/////////////////////////////
					str = "Select Max(VolumNum) From PP_HT";
					SqlCommand comm1 = new SqlCommand(str,conn);
					if(m_grid.Rows.Count==0 || comm1.ExecuteScalar() == Convert.DBNull || comm1.ExecuteScalar().ToString().Trim() == "")
						vol = 0;
					else
						vol = int.Parse(comm1.ExecuteScalar().ToString());
				}
					break;
				default :
					break;
			}


			conn.Close();
		}


		/// <summary>
		/// 그리드와 페이지에 입력값을 넘겨받는 생성자
		/// 영업창고입고, 창고이동, 기타입출고
		/// </summary>
		/// <param name="grid">넘겨줄 그리드</param>
		/// <param name="list">그리드외에 넘겨줄 변수값(맨마지막에는 선택된 그리드 인덱스값을 저장해서 넘긴다</param>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="User">사용자ID</param>
		public Register(UltraWebGrid grid, ArrayList list, string RequestPage, string UserID)
		{
			m_grid = grid;
			m_PageName = RequestPage;
			m_User = UserID;
			
			for(int i = 0; i<list.Count; i++)
			{
				if(list[i] ==null)
					m_List.Add("");
				else
					m_List.Add(list[i]);
			}
		
			switch(m_PageName)
			{
				

				case "BusinessStorehouseInStorehouse":		//영업창고입고
					m_Table = "IS_HT";
					m_Distinction = "영업";
					break;

				case "OtherInOutStorehouse":				//기타입출고
					m_Table = "OIOS_HT";
					m_Distinction = "영업";
					break;
				case "StorehouseMoving":					//창고이동
					m_Table = "SM_HT";
					m_Distinction = "영업";
					break;
				case "AddItemInStore":						//보용품입고
					m_Table = "AS_HT";
					m_Distinction = "영업";
					break;
				
				default :
					break;
			}
										   
			
			

			

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();


			
		}




		/// <summary>
		/// 그리드2개와 페이지에 입력값을 넘겨받는 생성자
		/// 상품제품출고, 매출등록
		/// </summary>
		/// <param name="grid1">입력을 위한 그리드</param>
		/// <param name="grid2">입력후 결과를 보여주는 그리드</param>		 
		/// <param name="list">화면의 입력값(list 맨처음에는 수량, 또는 금액을 입력하고 맨끝에는 선택된Row의 원장의번호가 들어가고 그바로 앞에는 선택된Row의 인덱스값을 넣어둔다</param>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="User">사용자ID</param>
		public Register(UltraWebGrid grid, UltraWebGrid grid2,ArrayList list, string RequestPage, string User)
		{
			m_grid = grid;
			m_grid1 = grid2;
			m_PageName = RequestPage;
			m_User = User;
			
			for(int i = 0; i<list.Count; i++)
			{
				if(list[i] ==null)
					m_List.Add("");
				else
					m_List.Add(list[i]);
			}
		
			switch(m_PageName)
			{
				
				case "GoodsManufactureOutStorehouse":		//상품제춤출고
					m_Table = "OS_HT";
					m_Distinction = "영업";
					break;
				case "SaleHistoryRegistration":					//매출등록
					m_Table = "S_HT";
					m_Distinction = "영업";
					break;

				default :
					break;
			}
										   
			
			

			

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();
		}

		/// <summary>
		/// 작업일보 등록시 생성자
		/// </summary>
		/// <param name="list">작업일보등록페이지의 값</param>
		/// <param name="list1">사용공구,치구 값</param>
		/// <param name="User">사용자ID</param>
		/// <param name="User">사용자ID</param>
		public Register(ArrayList list, ArrayList list1, string UserID, UltraWebGrid grid)
		{
			m_User = UserID;
			m_grid = grid;

			for(int i = 0; i<list.Count; i++)
			{
				if(list[i] ==null)
					m_List.Add("");
				else
					m_List.Add(list[i]);
			}

			for(int i = 0; i<list1.Count; i++)
			{
				if(list1[i] ==null)
					m_List1.Add("");
				else
					m_List1.Add(list1[i]);
			}


			m_Distinction = "생산";

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();
			
			
			conn.Close();
		
		}

		/// <summary>
		/// 하위품목 투입 등록함수
		/// </summary>
		public void SubItemTrowingRegister()
		{
			HttpContext hc = HttpContext.Current;
			
			if(MonthClosing() == false)
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
				hc.Response.Write("</script>");
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlCommand comm = new SqlCommand();
				SqlTransaction tr = conn.BeginTransaction();
				comm.Connection = conn;
				comm.Transaction = tr;

				try
			{	
					// 품목투입원장 생성
					string str = @"Insert into ST_HT (ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, 
							WCName, StoreNum, ThrowingQuanity, ThrowingDate, RegistrationPerson, RegistrationPersonID, RegistrationDate) 
						Values (@ItemNum, @ItemDrawNum, @ItemName, @ProcessSequenceNum, @ProcessCode, @ProcessName, 
							@WCName, @StoreNum, @ThrowingQuanity, @ThrowingDate, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate)";
					
					comm.Parameters.Add("@ItemNum",m_List[0].ToString());
					comm.Parameters.Add("@ItemDrawNum",m_List[1].ToString());
					comm.Parameters.Add("@ItemName",m_List[2].ToString());
					comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_List[3].ToString()));
					comm.Parameters.Add("@ProcessCode",m_List[4].ToString());
					comm.Parameters.Add("@ProcessName",m_List[5].ToString());
					comm.Parameters.Add("@ThrowingQuanity",int.Parse(m_List[6].ToString()));
					comm.Parameters.Add("@WCName",m_List[7].ToString());
					comm.Parameters.Add("@StoreNum",int.Parse(StoreNum.ToString()));
					comm.Parameters.Add("@ThrowingDate",m_Date);
					comm.Parameters.Add("@RegistrationPerson",m_UserName);
					comm.Parameters.Add("@RegistrationPersonID",m_User);
					comm.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

					comm.CommandText = str;
					comm.ExecuteNonQuery();

					// 창고별 수량 감소

					if(StoreNum == 4)//원자재창고
					{
						OutRMS_MTTable(conn,tr);
					}
					else if(StoreNum == 5)//생산창고
					{
						OutPS_MTTable(conn,tr);
					}
					else//영업창고
					{
						OutBS_MTTable(conn,tr);
					}

					hc.Response.Write("<script>alert('투입 했습니다!');</script>");
					tr.Commit();
				}
				catch(Exception ee)
				{
					hc.Response.Write("<script>alert('"+ee.Message +"');</script>");

					tr.Rollback();
				}
				finally
				{
					conn.Close();
				}

			}
		}


		/// <summary>
		/// 하위품목 투입시 원자재창고 출고수량 증가
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void OutRMS_MTTable(SqlConnection con, SqlTransaction trans)
		{
			Decimal m_cost = 0;
			string str = "";
			int year = DateTime.Parse(m_Date).Year;
			int mon = DateTime.Parse(m_Date).Month;

			// 품목의 금액을 구해서 넘겨준다
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm1 = new SqlCommand(str,con);
			comm1.Transaction = trans;
			comm1.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr = comm1.ExecuteReader();
			
			if(dr.Read())
				m_cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();


			Table table = new Table(year,mon);
			str = table.OutRowTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[6].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = m_cost*Decimal.Parse(m_List[6].ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_List[0].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_List[0].ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}

		/// <summary>
		/// 하위품목 투입시 영업창고 출고수량 증가
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void OutBS_MTTable(SqlConnection con, SqlTransaction trans)
		{
			int year = DateTime.Parse(m_Date).Year;
			int mon = DateTime.Parse(m_Date).Month;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
	
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[6].ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@process", SqlDbType.VarChar).Value = "14009999";			
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[6].ToString()) * cost;
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = StoreNum;
			comm.Parameters.Add("@year",year);
			Table table = new Table(year,mon);
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_List[0].ToString();
				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = StoreNum;
				comm.Parameters.Add("@year" ,DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName =  m_List[0].ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


		/// <summary>
		/// 하위품목 투입시 생산창고 출고수량 증가
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void OutPS_MTTable(SqlConnection con, SqlTransaction trans)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			int year = DateTime.Parse(m_Date).Year;
			int mon = DateTime.Parse(m_Date).Month;
			
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			if(dr_cost.Read())
			{
				if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
					m_cost = 0;
				else
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			}
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,con);
			comm_rate.Transaction = trans;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
		
			dr_rate.Close();
			

			string str = "";
			Table table = new Table(year,mon);
			str = table.OutProductionTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[6].ToString());
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value =  m_List[4].ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[6].ToString()) * Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_List[3].ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();


			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value =  m_List[4].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName =  m_List[0].ToString();
				if(reader.Read())
				{
					if(decimal.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}




		
		/// <summary>
		/// 작업일보 등록함수
		/// </summary>
		public bool WDR_Register()
		{
			bool a = false;

			// 월마감 체크후 등록이 가능한지 확인한다
			if(MonthClosing() == false)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
				hc.Response.Write("</script>");
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlCommand comm = new SqlCommand();
				SqlTransaction tr = conn.BeginTransaction();
				comm.Connection = conn;
				comm.Transaction = tr;

				try
				{
					string str = @"Insert into WDR_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,LotNum,ProductItemNum,ProductDrawNum,ProductName,
						ParentItemNum,ParentDrawNum,ParentName,WCInfoIndex,WCName,WorkPlanQuantity,WorkCompletionQuantity, ThisWorkCompletionQuantity, RemainQuantity,
						SuitabilityQuantity,UnSuitabilityQuantity,UnSuitabilityCost,InspectionDecisionCode,InspectionDecision,WorkBeginTime,WorkEndTime,Worker,WorkerID,NonWorkTime1,
						NonWorkTimeCode1,NonWorkTimeReason1,NonWorkTime2,NonWorkTimeCode2,NonWorkTimeReason2,NonWorkTime3,NonWorkTimeCode3,
						NonWorkTimeReason3,UnSuitabilityStatusCode,UnSuitabilityStatusMeaning,UnSuitabilityDetailMeaning,UnSuitabilityCauseCode,
						EtcNum1,EtcNum2,
						UnSuitabilityCauseMeaning,UseTool1,UseJig1,UseTool2,UseJig2,UseTool3,UseJig3,RegistrationPerson,RegistrationPersonID, 
						RegistrationDate,ProductionPlanHistoryIndex,WCDailyWorkPlanHistoryIndex) values(@itemnum,@drawnum,@itemname,@sequence,@processcode,@process,'',@productitemnum,@productdrawnum,@productitemname,
						@parentitemnum,@parentdraw,@parentitemname,@wcinfo,@wcname,@planquantity,@completionquantity,@thiscompletionquantity, @remainquantity,
						@suitquantity,@unsuitquantity,@unsuitcost,@InspectionDecisionCode,@InspectionDecision,@workbegintime,@workendtime,@worker,@workID,@nonworktime1,
						@nonworktimecode1,@nonworktimereason1,@nonworktime2,@nonworktimecode2,@nonworktimereason2,@nonworktime3,@nonworktimecode3,
						@nonworktimereason3,@unsuitstatecode,@unsuitstatemeaning,@unsuitdetailmeaning,@unsuitcausecode,
						@EtcNum1,@EtcNum2,
						@unsuitcausemeaning,@tool1,@jig1,@tool2,@jig2,@tool3,@jig3,@person,@personID,@date,@index,@index1)";
	
				
					
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
					comm.Parameters.Add("@drawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
					comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_List[3].ToString());
					comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = m_List[4].ToString();
					comm.Parameters.Add("@process",SqlDbType.VarChar).Value = m_List[5].ToString();
					comm.Parameters.Add("@productitemnum",SqlDbType.VarChar).Value = m_List[6].ToString();
					comm.Parameters.Add("@productdrawnum",SqlDbType.VarChar).Value = m_List[7].ToString();
					comm.Parameters.Add("@productitemname",SqlDbType.VarChar).Value = m_List[8].ToString();
					comm.Parameters.Add("@parentitemnum",SqlDbType.VarChar).Value = m_List[9].ToString();
					comm.Parameters.Add("@parentdraw",SqlDbType.VarChar).Value = m_List[10].ToString();
					comm.Parameters.Add("@parentitemname",SqlDbType.VarChar).Value = m_List[11].ToString();
					comm.Parameters.Add("@planquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[12].ToString());
					comm.Parameters.Add("@completionquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[13].ToString());
					comm.Parameters.Add("@thiscompletionquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[14].ToString());
					comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[15].ToString());
					comm.Parameters.Add("@suitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
					comm.Parameters.Add("@unsuitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[17].ToString());
					comm.Parameters.Add("@unsuitcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[18].ToString());
					comm.Parameters.Add("@workbegintime",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(m_List[19]).ToString();
					comm.Parameters.Add("@workendtime",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(m_List[20]).ToString();
					comm.Parameters.Add("@nonworktime1",SqlDbType.Decimal).Value = decimal.Parse(m_List[21].ToString());
					comm.Parameters.Add("@nonworktimecode1",SqlDbType.VarChar).Value = m_List[22].ToString();
					comm.Parameters.Add("@nonworktimereason1",SqlDbType.VarChar).Value = m_List[23].ToString();
					comm.Parameters.Add("@nonworktime2",SqlDbType.Decimal).Value = decimal.Parse(m_List[24].ToString());
					comm.Parameters.Add("@nonworktimecode2",SqlDbType.VarChar).Value = m_List[25].ToString();
					comm.Parameters.Add("@nonworktimereason2",SqlDbType.VarChar).Value = m_List[26].ToString();
					comm.Parameters.Add("@nonworktime3",SqlDbType.Decimal).Value = decimal.Parse(m_List[27].ToString());
					comm.Parameters.Add("@nonworktimecode3",SqlDbType.VarChar).Value = m_List[28].ToString();
					comm.Parameters.Add("@nonworktimereason3",SqlDbType.VarChar).Value = m_List[29].ToString();
					comm.Parameters.Add("@unsuitstatecode",SqlDbType.VarChar).Value = m_List[30].ToString();
					comm.Parameters.Add("@unsuitstatemeaning",SqlDbType.VarChar).Value = m_List[31].ToString();
					comm.Parameters.Add("@unsuitdetailmeaning",SqlDbType.VarChar).Value = m_List[32].ToString();
					comm.Parameters.Add("@unsuitcausecode",SqlDbType.VarChar).Value = m_List[33].ToString();
					comm.Parameters.Add("@unsuitcausemeaning",SqlDbType.VarChar).Value = m_List[34].ToString();				
					if(m_List[35].ToString() == "")
						comm.Parameters.Add("@index",SqlDbType.Int).Value = Convert.DBNull;
					else
						comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_List[35].ToString());
					comm.Parameters.Add("@index1",SqlDbType.Int).Value = int.Parse(m_List[37].ToString());
					comm.Parameters.Add("@InspectionDecisionCode",SqlDbType.VarChar).Value = m_List[38].ToString();
					comm.Parameters.Add("@InspectionDecision",SqlDbType.VarChar).Value = m_List[39].ToString();
					
					comm.Parameters.Add("@EtcNum1",SqlDbType.VarChar).Value = m_List[41].ToString();
					comm.Parameters.Add("@EtcNum2",SqlDbType.VarChar).Value = m_List[42].ToString();
					
					
					comm.Parameters.Add("@tool1",SqlDbType.VarChar).Value = m_List1[0].ToString();
					comm.Parameters.Add("@jig1",SqlDbType.VarChar).Value = m_List1[1].ToString();
					comm.Parameters.Add("@tool2",SqlDbType.VarChar).Value = m_List1[2].ToString();
					comm.Parameters.Add("@jig2",SqlDbType.VarChar).Value = m_List1[3].ToString();
					comm.Parameters.Add("@tool3",SqlDbType.VarChar).Value = m_List1[4].ToString();
					comm.Parameters.Add("@jig3",SqlDbType.VarChar).Value = m_List1[5].ToString();
					comm.Parameters.Add("@wcname",SqlDbType.VarChar).Value = m_List1[7].ToString();
					comm.Parameters.Add("@wcinfo",SqlDbType.Int).Value = int.Parse(m_List1[6].ToString());
					comm.Parameters.Add("@workID",SqlDbType.VarChar).Value = m_List1[8].ToString();
					comm.Parameters.Add("@worker",SqlDbType.VarChar).Value = m_List1[9].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personID",SqlDbType.VarChar).Value = m_User.ToString();
					comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					
					comm.CommandText = str;
					comm.ExecuteNonQuery();

				

					WCWD_Update(int.Parse(m_List[37].ToString()),conn,tr);	//WC별 일자별 작업계획원장 번호
					comm.Parameters.Clear();
					





					// 지금공정이 검사이고 최종공정이면 품질검사원장에 레코드를 등록해야 한다.
					// 최종공정은 해당품목의 지금공정순서보다 큰게 없으면 최종공정이다.
					str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum > @num";
					comm.Parameters.Add("@item",SqlDbType.VarChar).Value = m_List[0].ToString();
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
					comm.CommandText = str;
					int count = int.Parse(comm.ExecuteScalar().ToString());
					comm.Parameters.Clear();
					// 품질검사원장에 등록할 단가를 구해야 한다.(품목정보테이블에 기준단가)
					decimal UnitCost = 0;
					str = "Select * From II_MT where RecodingState =1 and ItemNum = @itemnum";
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
					comm.CommandText = str;
					SqlDataReader dr = comm.ExecuteReader();
					while(dr.Read())
					{
						UnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
					}
					dr.Close();
					comm.Parameters.Clear();	
			
					if(Division(m_List[0].ToString()) == 3 && count == 0)//자산분류가 제품이고 최종공정인경우
					{
						// 영업창고입고의뢰원장 등록
						str="Insert into ISR_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,InstorehouseRequestQuantity, InStoreDate, "+
							"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
							"@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@processcode,@processname,@quantity, @InStoreDate,'대기', @Person, @PersonID, @Date,@idx,@section)";

						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
						comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
						comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = m_List[4].ToString();
						comm.Parameters.Add("@processname",SqlDbType.VarChar).Value = m_List[5].ToString();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
						comm.Parameters.Add("@InStoreDate",DateTime.Parse(m_List[20].ToString()).ToShortDateString());
						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
						comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());
							
						comm.Parameters.Add("@idx",SqlDbType.Int).Value = MaxWDR_HT(conn,tr);
						comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "작업일보";
						comm.CommandText = str;
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						// 품질검사원장 등록
						str="Insert into QI_HT (ItemNum,ItemDrawNum,ItemName,CompanyName, BusinessRegistrationNum,QualityInspectionCompleteDate,BeginProcessSequenceNum,EndProcessSequenceNum,BeginProcessName,EndProcessName,RequestQuantity,SuitabilityQuantity,ApplyUnitCost,LotNum,ProgressCondition,"+
							"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex1,HistorySection1,HistoryIndex2,HistorySection2,Recognition) values("+
							"@itemnum,@itemdrawnum,@itemname,@name,@com,@Date,@beginsequence,@endsequence,@beginprocess,@endprocess,@quantity,@quantity,@unitcost,@LotNum,'검사완료', @Person, @PersonID, @RegistrationDate,@idx1,@section1,@idx2,@section2, '승인')";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
						comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "";
						comm.Parameters.Add("@com",SqlDbType.VarChar).Value = "";
						comm.Parameters.Add("@beginsequence",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
						comm.Parameters.Add("@endsequence",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
						comm.Parameters.Add("@beginprocess",SqlDbType.VarChar).Value = m_List[4].ToString();
						comm.Parameters.Add("@endprocess",SqlDbType.VarChar).Value = m_List[4].ToString();
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
						comm.Parameters.Add("@unitcost", SqlDbType.Decimal).Value = UnitCost;
						comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = Convert.DBNull;;
						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
						comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[20].ToString()).ToShortDateString();
						comm.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.Parameters.Add("@idx1",SqlDbType.Int).Value = MaxWDR_HT(conn,tr);
						comm.Parameters.Add("@section1",SqlDbType.VarChar).Value = "작업일보";
						comm.Parameters.Add("@idx2",SqlDbType.Int).Value = int.Parse(m_List[40].ToString());
						comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "작업계획";
						comm.CommandText = str;
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						// 생산계획원장에 수량입력
						if(m_List[35].ToString().Trim() != "")
						{
							//적합수량을 생산계획원장 완료수량에 입력
							str = "Update PP_HT Set ProductionCompleteQuantity = ProductionCompleteQuantity + @Quantity Where ProductionPlanHistoryIndex = @Index";
							comm.Parameters.Add("@Quantity",decimal.Parse(m_List[16].ToString()));
							comm.Parameters.Add("@Index",int.Parse(m_List[35].ToString().Trim()));
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();

							//수정된 수량이 계획수량보다 많으면 완료
							decimal Compare = 0;
							decimal Plan = 0;
							str = "Select ProductionPlanQuantity From PP_HT Where ProductionPlanHistoryIndex = @Index";
							comm.Parameters.Add("@Index",int.Parse(m_List[35].ToString().Trim()));
							comm.CommandText = str;
							SqlDataReader dr1 = comm.ExecuteReader();
							while(dr1.Read())
							{
								Plan = decimal.Parse(dr1["ProductionPlanQuantity"].ToString());
							}
							dr1.Close();
							comm.Parameters.Clear();

							Compare  = Plan - decimal.Parse(m_List[16].ToString());

							if(Compare <= 0)
							{
								str = "Update PP_HT Set ProgressCondition = '완료' Where ProductionPlanHistoryIndex = @Index";
								comm.Parameters.Add("@Index",int.Parse(m_List[35].ToString().Trim()));
								comm.CommandText = str;
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}											
					}
					
					////////////////////////////////////////////////////////////////////////////
					// 작업한 하위품목이 원자재이면 원자재 창고의 출고수량(BOM에 맞는수량)증가//
					// 반제품 이면 생산창고의 출고수량(BOM에 맞는수량)증가   -시작-           //
					////////////////////////////////////////////////////////////////////////////
					///

					if(WorkPlan3() )
					{
						if(IsManaged(conn,tr,m_List[0].ToString()))
							SubItemItem_Quantity(conn,tr);
					}
					else
						SubItemItem_Quantity(conn,tr);
					
					

					////////////////////////////////////////////////////////////////////////////
					// 작업한 하위품목이 원자재이면 원자재 창고의 출고수량(BOM에 맞는수량)증가//
					// 반제품 이면 생산창고의 출고수량(BOM에 맞는수량)증가   -끝-             //
					////////////////////////////////////////////////////////////////////////////
					




					////////////////////////////////////////////////////////////////////////////
					// 사용한 공구및 치구의 누적shot을 설비정보에 누적시켜야 한다.            //
					////////////////////////////////////////////////////////////////////////////


					UseTool_Update(conn,tr);

					


					////////////////////////////////////////////////////////////////////////////
					// 사용한 공구및 치구의 누적shot을 설비정보에 누적시켜야 한다.            //
					////////////////////////////////////////////////////////////////////////////
					
					
					tr.Commit();
					a=true;
				}
				catch(Exception ee)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('"+ee.Message+"');");
					hc.Response.Write("</script>");

					tr.Rollback();
					a = false;
				}
				finally
				{
					conn.Close();
				}
			}
			return a;
		}


		/// <summary>
		/// 관리여부 판단 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private bool IsManaged(SqlConnection con, SqlTransaction trans, string item)
		{
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "Select Unit4 From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			string manage = "예";
							
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				manage = dr["Unit4"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(manage == "예")
				return true;
			else
				return false;

		}

		/// <summary>
		/// 작업계획3이 아닌경우 이력원장에 기록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SubItemItem_Quantity(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "";
			decimal rate =0;
			int year = DateTime.Parse(m_List[19].ToString()).Year;
			int mon = DateTime.Parse(m_List[19].ToString()).Month;
			Table table = new Table(year,mon);

			

			for(int a = 0; a < m_grid.Rows.Count; a++)
			{
				Child = m_grid.Rows[a].Cells.FromKey("ItemNum").Text;
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = Child;
				comm2.CommandText = str;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
				if(Division(Child)==1)//원자재이면
				{

					str = table.OutRowTable();
					decimal quantity = 0;
					if(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text.Trim() == "" || m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text.Trim() == null)
						quantity = 0;
					else
						quantity = decimal.Parse(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text);

					comm2.Parameters.Add("@quantity",decimal.Parse(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text));
					comm2.Parameters.Add("@cost",Cost*quantity);
					comm2.Parameters.Add("@num",Child);
					comm2.Parameters.Add("@year",year);
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();

					//원자재가 출고되는 이력을 기록한다.
					str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'작업일보',@HistoryIndex)";
				
						
					comm2.Parameters.Add("@itemnum",m_grid.Rows[a].Cells.FromKey("ItemNum").Text);									//품목번호
					comm2.Parameters.Add("@itemdrawnum", m_grid.Rows[a].Cells.FromKey("ItemDrawNum").Text);						//도면번호
					comm2.Parameters.Add("@itemname",m_grid.Rows[a].Cells.FromKey("ItemName").Text);								//품목명
					//comm2.Parameters.Add("@ProcessSequenceNum",0);												//공정순서
					//comm2.Parameters.Add("@ProcessCode","14000000");											//공정코드
					//comm2.Parameters.Add("@ProcessName","소재");												//공정명
					//comm2.Parameters.Add("@Division",0);														//입출고구분(출고)
					comm2.Parameters.Add("@Quantity",quantity);			//입출고 수량
					comm2.Parameters.Add("@Date",DateTime.Parse(m_List[20].ToString()).ToShortDateString());	//입출고일
					//comm2.Parameters.Add("@HistoryDivision","작업일보");										//원장구분
					comm2.Parameters.Add("@HistoryIndex",MaxWDR_HT(con,trans));									//원장번호
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();
				}
				else//반제품이면
				{
					str = table.OutProductionTable();
					rate = ProgressRate(Child,m_grid.Rows[a].Cells.FromKey("ProcessCode").Text,con,trans);
					comm2.Parameters.Add("@quantity",decimal.Parse(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text));
					comm2.Parameters.Add("@cost",Cost*decimal.Parse(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text)*(rate/100));
					comm2.Parameters.Add("@num",Child);
					comm2.Parameters.Add("@code", m_grid.Rows[a].Cells.FromKey("ProcessCode").Text);
					comm2.Parameters.Add("@year",year);
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();


					//공정품이 출고되는 이력을 기록한다.
					str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','0',@Quantity,@Date,'작업일보',@HistoryIndex)";
				
						
					comm2.Parameters.Add("@itemnum",m_grid.Rows[a].Cells.FromKey("ItemNum").Text);									//품목번호
					comm2.Parameters.Add("@itemdrawnum", m_grid.Rows[a].Cells.FromKey("ItemDrawNum").Text);						//도면번호
					comm2.Parameters.Add("@itemname",m_grid.Rows[a].Cells.FromKey("ItemName").Text);								//품목명
					comm2.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[a].Cells.FromKey("ProcessSequenceNum").Text));												//공정순서
					comm2.Parameters.Add("@ProcessCode",m_grid.Rows[a].Cells.FromKey("ProcessCode").Text);											//공정코드
					comm2.Parameters.Add("@ProcessName",m_grid.Rows[a].Cells.FromKey("ProcessName").Text);												//공정명
					//comm2.Parameters.Add("@Division",0);														//입출고구분(출고)
					comm2.Parameters.Add("@Quantity",decimal.Parse(m_grid.Rows[a].Cells.FromKey("WorkPlanQuantity").Text));			//입출고 수량
					comm2.Parameters.Add("@Date",DateTime.Parse(m_List[20].ToString()).ToShortDateString());	//입출고일
					//comm2.Parameters.Add("@HistoryDivision","작업일보");										//원장구분
					comm2.Parameters.Add("@HistoryIndex",MaxWDR_HT(con,trans));									//원장번호

					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();
				}


				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					if(Division(Child) == 1)
						strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					else
						strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @num and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[a].Cells.FromKey("ProcessCode").Text;//Process(Child,con,trans);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = Child;
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}




			}
			comm.Parameters.Clear();
						


			//반제품 창고입고품목의 창고금액
			Cost = 0;
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.CommandText = str;
			SqlDataReader dr_Cost = comm.ExecuteReader();
			while(dr_Cost.Read())
			{
				Cost = decimal.Parse(dr_Cost["StandardUnitCost"].ToString());
			}
			dr_Cost.Close();
			comm.Parameters.Clear();
			rate = ProgressRate(m_List[0].ToString(),m_List[4].ToString(),con,trans);
			// 가공된 지금 품목의 생산창고 입고
			str = table.InProductionTable();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[16].ToString())*rate;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.Parameters.Add("@year",year);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//공정품이 입고되는 이력을 기록한다.
			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'작업일보',@HistoryIndex)";
				
						
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
			comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[20].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@HistoryIndex",MaxWDR_HT(con,trans));									//원장번호

			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}



		/// <summary>
		/// 제품출고시 원자재 떨어주는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SubItemBOM_Quantity(SqlConnection con, SqlTransaction trans,int count)
		{
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "";
			
			int year = DateTime.Parse(m_List[2].ToString()).Year;
			int mon = DateTime.Parse(m_List[2].ToString()).Month;
			Table table = new Table(year,mon);

			
			comm.CommandText = "WorkRowBomTree";
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);

			for(int a = 0; a < ds.Tables[0].Rows.Count; a++)
			{
				Child = ds.Tables[0].Rows[a]["ItemNum"].ToString();
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = Child;
				comm2.CommandText = str;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
					str = table.OutRowTable();
					decimal quantity = 0;
					quantity = decimal.Parse(ds.Tables[0].Rows[a]["Quantity"].ToString());

					comm2.Parameters.Add("@quantity",quantity);
					comm2.Parameters.Add("@cost",Cost*quantity);
					comm2.Parameters.Add("@num",Child);
					comm2.Parameters.Add("@year",year);
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();

					//원자재가 출고되는 이력을 기록한다.
					str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				
						
					comm2.Parameters.Add("@itemnum",ds.Tables[0].Rows[a]["ItemNum"].ToString().Trim());									//품목번호
					comm2.Parameters.Add("@itemdrawnum", ds.Tables[0].Rows[a]["ItemDrawNum"].ToString().Trim());						//도면번호
					comm2.Parameters.Add("@itemname",ds.Tables[0].Rows[a]["ItemName"].ToString().Trim());							//품목명
					//comm2.Parameters.Add("@ProcessSequenceNum",0);												//공정순서
					//comm2.Parameters.Add("@ProcessCode","14000000");											//공정코드
					//comm2.Parameters.Add("@ProcessName","소재");												//공정명
					//comm2.Parameters.Add("@Division",0);														//입출고구분(출고)
					comm2.Parameters.Add("@Quantity",quantity);			//입출고 수량
					comm2.Parameters.Add("@Date",DateTime.Parse(m_List[2].ToString()).ToShortDateString());	//입출고일
					//comm2.Parameters.Add("@HistoryDivision","원자재출고");										//원장구분
					comm2.Parameters.Add("@HistoryIndex",MaxOS_HT(con,trans));									//원장번호
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();
				

				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					comm.CommandType = CommandType.Text;;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = Child;
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 출고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}




			}
			comm.Parameters.Clear();
			
		}

		


		/// <summary>
		/// 작업일보 등록 함수에서 사용 
		/// 작업계획3인경우 이력원장 기록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SubItemBOM_Quantity(SqlConnection con, SqlTransaction trans)
		{
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "";
			decimal rate =0;
			int year = DateTime.Parse(m_List[19].ToString()).Year;
			int mon = DateTime.Parse(m_List[19].ToString()).Month;
			Table table = new Table(year,mon);

			if(TopProcess(con,trans,m_List[0].ToString(),int.Parse(m_List[3].ToString())) == true)
			{
			
				//반제품 창고입고품목의 창고금액
				Cost = 0;
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.CommandText = str;
				SqlDataReader dr_Cost = comm.ExecuteReader();
				while(dr_Cost.Read())
				{
					Cost = decimal.Parse(dr_Cost["StandardUnitCost"].ToString());
				}
				dr_Cost.Close();
				comm.Parameters.Clear();
				rate = ProgressRate(m_List[0].ToString(),m_List[4].ToString(),con,trans);
				// 가공된 지금 품목의 생산창고 입고
				str = table.InProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[16].ToString())*rate;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				//공정품이 입고되는 이력을 기록한다.
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'작업일보',@HistoryIndex)";
				
						
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
				comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
				comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.VarChar).Value = int.Parse(m_List[3].ToString());
				comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_List[4].ToString();
				comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_List[5].ToString();
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
				comm.Parameters.Add("@Date",DateTime.Parse(m_List[20].ToString()).ToShortDateString());	//입출고일
				comm.Parameters.Add("@HistoryIndex",MaxWDR_HT(con,trans));									//원장번호

				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
			
		}


		/// <summary>
		/// 등록 품목이 제품이면서 마지막 공정인경우에만 생산창고에 수량을 추가한다.
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="ItemNum"></param>
		/// <param name="SequenceNum"></param>
		/// <returns></returns>
		private bool TopProcess(SqlConnection conn, SqlTransaction tr ,string ItemNum, int SequenceNum)
		{
			string aa = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(aa.ToString() =="제품")
			{
				str = "Select Max(ProcessSequenceNum) From PSI_MT where ItemNum = @ItemNum and RecodingState =1";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = ItemNum;
				comm.CommandText = str;
				int sequence = int.Parse(comm.ExecuteScalar().ToString());
				if(sequence == SequenceNum)
					return true;
				else
					return false;

			}
			else
				return false;

		}


		#region 이전 BOM떨어주는 함수
		private void SubBOM_Quantity(SqlConnection con, SqlTransaction trans)
		{
			//품목과 수량을 던져주면서 생산창고 제품 증가, 원자재 창고 수량 감소시킴.

			
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandType = CommandType.StoredProcedure;
			

			str = "SPTotalNeedCalculation";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", m_List[0].ToString());
			comm.Parameters.Add("@Quantity", decimal.Parse(m_List[16].ToString()));
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 1;
			
			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();

			da.Fill(ds);
			comm.Parameters.Clear();

			
			Table table = new Table(DateTime.Parse(m_List[19].ToString()).Month);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;

				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum", dr["ItemNum"].ToString());
				comm2.CommandText = str;
				comm.CommandType = CommandType.Text;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
				str = table.OutRowTable();
				comm2.Parameters.Add("@quantity",decimal.Parse(dr["Quantity"].ToString()));
				comm2.Parameters.Add("@cost",Cost*decimal.Parse(dr["Quantity"].ToString()));
				comm2.Parameters.Add("@num",dr["ItemNum"].ToString());
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();



				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = dr["ItemNum"].ToString();
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}


				
			}

			//제품 창고입고품목의 창고금액
			Cost = 0;
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.CommandText = str;
			SqlDataReader dr_Cost = comm.ExecuteReader();
			while(dr_Cost.Read())
			{
				Cost = decimal.Parse(dr_Cost["StandardUnitCost"].ToString());
			}
			dr_Cost.Close();
			comm.Parameters.Clear();
			decimal rate = ProgressRate(m_List[0].ToString(),m_List[4].ToString(),con,trans);
			// 가공된 지금 품목의 생산창고 입고
			str = table.InProductionTable();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[16].ToString())*rate;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			
		}

		#endregion

		private void UseTool_Update(SqlConnection con, SqlTransaction trans)
		{
			string str = "";
			SqlCommand comm3 = new SqlCommand();
			comm3.Connection = con;
			comm3.Transaction = trans;
					
			//설비정보에 공구1의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[0].ToString();
			comm3.CommandText = str;
			SqlDataAdapter daShot = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(daShot) ;
			DataSet dsShot = new DataSet() ;
			daShot.Fill(dsShot) ;
			if(dsShot.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsShot.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp;
				dsShot.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsShot.Tables[0].Rows[0]["FirstShot"].ToString());
				daShot.Update(dsShot) ;
			}
			//설비정보에 공구1의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();

			//설비정보에 치구1의 작업shot을 누적하는 부분......시작
					
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[1].ToString();
			comm3.CommandText = str;
			SqlDataAdapter daJig = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilderJig = new SqlCommandBuilder(daJig) ;
			DataSet dsJig = new DataSet() ;
			daJig.Fill(dsJig) ;
			if(dsJig.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsJig.Tables[0].Rows[0]["Cavity"].ToString())));
				dsJig.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig;
				dsJig.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsJig.Tables[0].Rows[0]["FirstShot"].ToString());
				daJig.Update(dsJig) ;



			}
			//설비정보에 치구1의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();
					
			//설비정보에 공구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[2].ToString();
			comm3.CommandText = str;				
			SqlDataAdapter daShot1 = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilder1 = new SqlCommandBuilder(daShot1) ;
			DataSet dsShot1 = new DataSet() ;
			daShot1.Fill(dsShot1) ;
			if(dsShot1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp1 = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsShot1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp1;
				dsShot1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsShot1.Tables[0].Rows[0]["FirstShot"].ToString());
				daShot1.Update(dsShot1) ;
			}
			//설비정보에 공구2의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();

			//설비정보에 치구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[3].ToString();
			comm3.CommandText = str;
			SqlDataAdapter daJig1 = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilderJig1 = new SqlCommandBuilder(daJig1) ;
			DataSet dsJig1 = new DataSet() ;
			daJig1.Fill(dsJig1) ;
			if(dsJig1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig1 = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsJig1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig1;
				dsJig1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsJig1.Tables[0].Rows[0]["FirstShot"].ToString());
				daJig1.Update(dsJig1) ;
				
			}
			//설비정보에 치구2의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();


			//설비정보에 공구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[4].ToString();
			comm3.CommandText = str;				
			SqlDataAdapter daShot2 = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilder2 = new SqlCommandBuilder(daShot2) ;
			DataSet dsShot2 = new DataSet() ;
			daShot2.Fill(dsShot2) ;
			if(dsShot2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp2 = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsShot2.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp2;
				dsShot2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsShot2.Tables[0].Rows[0]["FirstShot"].ToString());
				daShot2.Update(dsShot2) ;
			}
			//설비정보에 공구3의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();

			//설비정보에 치구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where RecodingState = 1 and EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm3.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List1[5].ToString();
			comm3.CommandText = str;
			SqlDataAdapter daJig2 = new SqlDataAdapter(comm3) ;
			SqlCommandBuilder CommandBuilderJig2 = new SqlCommandBuilder(daJig2) ;
			DataSet dsJig2 = new DataSet() ;
			daJig2.Fill(dsJig2) ;
			if(dsJig2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig2 = Math.Round((decimal.Parse(m_List[14].ToString()) / decimal.Parse(dsJig2.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig2;
				dsJig2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString()) + decimal.Parse(dsJig2.Tables[0].Rows[0]["FirstShot"].ToString());
				daJig2.Update(dsJig2) ;
			}
			//설비정보에 치구3의 작업shot을 누적하는 부분......끝
			comm3.Parameters.Clear();
		}




		/// <summary>
		/// 실작업수량에 맞는 하위품목의 수량을 BOM정보를 이용해 떨어주는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BOM_Quantity(SqlConnection con,SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			decimal rate = 0;
			string ProcessCode = "";

			int year = DateTime.Parse(m_List[20].ToString()).Year ;
			int mon = DateTime.Parse(m_List[20].ToString()).Month ;

			//지금 공정이 첫공정이면 하위품목을 떨어주고 그렇지 않으면 바로 아래공정을 떨어준다
			
			//string str = "Select Max(ProcessSequenceNum) as Process From WDWP_HT where ItemNum = @itemnum and ProcessSequenceNum < @num";
			string str = "Select Max(ProcessSequenceNum) as Process From PSI_MT where ItemNum = @itemnum and RecodingState = 1 and  ProcessSequenceNum < @num";
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@num",SqlDbType.TinyInt).Value = int.Parse(m_List[3].ToString());
			comm.CommandText = str;
					
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				if(dr1["Process"] == Convert.DBNull)
					sequence = 0;
				else
					sequence = int.Parse(dr1["Process"].ToString());
			}
			dr1.Close();
			comm.Parameters.Clear();

			Table table = new Table(year,mon);
					
			if(sequence == 0)//첫공정이면
			{

				//창고에서 떨어줄 품목과 수량
				str = "Select ChildItemNum,NeedQuantityNumerator,NeedQuantityDenominator From IOI_MT where RecodingState = 1 and ParentItemNum = @itemnum";
				comm.CommandText = str;
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);
				comm.Parameters.Clear();

				foreach(DataRow drow in ds1.Tables[0].Rows)
				{
					Child = drow["ChildItemNum"].ToString();
					NeedQuantity = decimal.Parse(drow["NeedQuantityNumerator"].ToString())/decimal.Parse(drow["NeedQuantityDenominator"].ToString())* decimal.Parse(m_List[14].ToString());//소요량* 작업완료수량 => 창고에서 떨어낼 량
					SqlCommand comm2 = new SqlCommand();
					comm2.Connection = con;
					comm2.Transaction = trans;
					//창고에서 떨어줄 금액
					str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
					comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = Child;
					comm2.CommandText = str;
					SqlDataReader dr3 = comm2.ExecuteReader();
					while(dr3.Read())
					{
						Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
					}
					dr3.Close();
					comm2.Parameters.Clear();
					//창고에서 떨어줘야 한다
					if(Division(Child)==1)//원자재이면
					{
						str = table.OutRowTable();
						comm2.Parameters.Add("@quantity",SqlDbType.Decimal).Value = NeedQuantity;
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*NeedQuantity;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = Child;
						comm2.Parameters.Add("@year",year);
						comm2.CommandText = str;
						comm2.ExecuteNonQuery();
						comm2.Parameters.Clear();
					}
					else//반제품이면
					{
						str = table.OutProductionTable();
						ProcessCode = Process(Child,con,trans);
						rate = ProgressRate(Child,ProcessCode,con,trans);
						comm2.Parameters.Add("@quantity",SqlDbType.Decimal).Value = NeedQuantity;
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*NeedQuantity*rate;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = Child;
						comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = ProcessCode;
						comm2.Parameters.Add("@year",year);
						comm2.CommandText = str;
						comm2.ExecuteNonQuery();
						comm2.Parameters.Clear();
					}


					//마이너스재고를 허용하지 않으면 
					if(MinusStore() == false)
					{
						string strSQL="";
						if(Division(Child) == 1)
							strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
						else
							strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @num and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Process(Child,con,trans);
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = "";
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
								ItemName = Child;
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
						}
						comm.Parameters.Clear();

					}




				}
				comm.Parameters.Clear();
						


				//반제품 창고입고품목의 창고금액
				Cost = 0;
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.CommandText = str;
				SqlDataReader dr_Cost = comm.ExecuteReader();
				while(dr_Cost.Read())
				{
					Cost = decimal.Parse(dr_Cost["StandardUnitCost"].ToString());
				}
				dr_Cost.Close();
				comm.Parameters.Clear();
				rate = ProgressRate(m_List[0].ToString(),m_List[4].ToString(),con,trans);
				// 가공된 지금 품목의 생산창고 입고
				str = table.InProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[16].ToString())*rate;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
						
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);
				foreach(DataRow drow in ds1.Tables[0].Rows)
				{
					Cost = decimal.Parse(drow["StandardUnitCost"].ToString());
				}
				comm.Parameters.Clear();

				// 이전공정의 진척율
				string code = "";//이전공정코드
				str = "Select ProgressRate,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = sequence;
				comm.CommandText = str;
				SqlDataReader dr_rate = comm.ExecuteReader();
				if(dr_rate.Read())
				{
					if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
						Rate = 100;
					else
						Rate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
					code = dr_rate["ProcessCode"].ToString();
				}
				dr_rate.Close();
				comm.Parameters.Clear();


				str = table.OutProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[14].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[14].ToString())*(Rate/100);
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @num and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_List[0].ToString();
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = code;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
					comm.Parameters.Clear();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = m_List[0].ToString();
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
				}


				//생산창고 입고
				// 현재공정의 진척율
				str = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_List[3].ToString());
				comm.CommandText = str;
				SqlDataReader dr_rate1 = comm.ExecuteReader();
				if(dr_rate1.Read())
				{
					if(dr_rate1["ProgressRate"].ToString() == null || dr_rate1["ProgressRate"].ToString().Trim() =="")
						Rate = 100;
					else
						Rate = Decimal.Parse(dr_rate1["ProgressRate"].ToString());
				}
				dr_rate1.Close();
				comm.Parameters.Clear();

				str = table.InProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[16].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_List[16].ToString())*(Rate/100);
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}


		/// <summary>
		/// 진척
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="code"></param>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <returns></returns>
		private decimal ProgressRate(string Item, string code, SqlConnection conn,SqlTransaction tr)
		{
			string str = @"select * From PSI_MT where RecodingState = 1 and ItemNum=@num and ProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",Item);
			comm.Parameters.Add("@ProcessCode",code);
			SqlDataReader dr = comm.ExecuteReader();
			decimal rate = -1;
			while(dr.Read())
			{
				rate = decimal.Parse(dr["ProgressRate"].ToString())/100;
			}
			dr.Close();

			if(rate == -1)
				throw new Exception(Item+" 품목의 공정정보가 입력되어 있지 않습니다!");
			else
				return rate;
		}
	


		/// <summary>
		/// 하위품목의 최상위공정순서 찾는 함수
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private string Process(string Item, SqlConnection conn,SqlTransaction tr)
		{
			if(Division(Item) != 1)
			{
				string str = @"select * From PSI_MT where RecodingState = 1 and ItemNum=@num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From  PSI_MT where RecodingState = 1 and ItemNum=@num)";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",Item);
				SqlDataReader dr = comm.ExecuteReader();
				string code ="";
				while(dr.Read())
				{
					code = dr["ProcessCode"].ToString();
				}
				dr.Close();

				if(code == "")
					throw new Exception(Item+" 품목의 공정정보가 입력되어 있지 않습니다!");
				else
					return code;
			}
			else
				return "14000000";
		}


		/// <summary>
		/// 작업일보원장에서 제일큰 인덱스값 구하는 함수
		/// 지금 등록하는 레코드의 인덱스값이 제일 큰값으로 되기 때문에
		/// 만일 최종공정품이 검사품인경우 품질검사원장에 등록할때 작업일보원장 인덱스를 기록하고
		/// 무검사품인경우 영업창고입고원장에 이전원장번호 기록시에 사용하기위한 함수
		/// </summary>
		/// <returns></returns>
		private int MaxWDR_HT(SqlConnection con, SqlTransaction trans)
		{
			string str  = "Select Max(WorkDailyReportHistoryIndex) From WDR_HT";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}

		
		/// <summary>
		/// 작업일보등록후 WC별 일자별 작업계획원장 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void WCWD_Update(int rowcount,SqlConnection con, SqlTransaction trans)
		{
			Decimal Quantity = 0;
			string str_quantity = "Select WorkPlanQuantity From WDWP_HT where WCDailyWorkPlanHistoryIndex = @idx";
			SqlCommand comm_quantity = new SqlCommand(str_quantity, con);
			comm_quantity.Transaction  =trans;
			comm_quantity.Parameters.Add("@idx", SqlDbType.Int).Value = m_List[37].ToString();
			SqlDataReader dr = comm_quantity.ExecuteReader();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["WorkPlanQuantity"].ToString());
			}
			dr.Close();

			string str = "";
//			if(Quantity <= (decimal.Parse(m_List[13].ToString())+decimal.Parse(m_List[16].ToString())))	//이전작업량+합격수량
//				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quantity,ProgressCondition = '완료' WHERE WCDailyWorkPlanHistoryIndex = @idx";
//			else 
//				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quantity,ProgressCondition = '진행' WHERE WCDailyWorkPlanHistoryIndex = @idx";
//
//			SqlCommand comm = new SqlCommand(str, con);
//			comm.Transaction  = trans;
//			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_List[37].ToString());
//			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (decimal.Parse(m_List[13].ToString())+decimal.Parse(m_List[16].ToString()));
//			comm.ExecuteNonQuery();

			if(Quantity <= (decimal.Parse(m_List[13].ToString())+decimal.Parse(m_List[14].ToString())))		//이전작업량+금번작업량
				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quantity,ProgressCondition = '완료' WHERE WCDailyWorkPlanHistoryIndex = @idx";
			else 
				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quantity,ProgressCondition = '진행' WHERE WCDailyWorkPlanHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction  = trans;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_List[37].ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (decimal.Parse(m_List[13].ToString())+decimal.Parse(m_List[14].ToString()));
			comm.ExecuteNonQuery();
		}

		

		/// <summary>
		/// 데이터셋을 넘겨받는 생성자
		/// 외주의뢰, 구매발주, 외주발주
		/// </summary>
		/// <param name="dataset">넘겨줄 데이터셋</param>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="UserID">사용자ID</param>
		public Register(DataSet dataset, string RequestPage, string UserID)
		{
			m_DataSet = dataset;
			m_PageName = RequestPage;
			m_User = UserID;


			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();

			switch(m_PageName)
			{
				case "OutsideRequest":				// 외주의뢰
					m_Table = "OOR_HT";
					m_Distinction = "생산";
					break;
				case "BuyingOrder":					// 구매발주
					m_Table = "BO_HT";
					m_Distinction = "구매";
					break;
				case "OutSideOrder":				//외주발주
					m_Table = "OO_HT";
					m_Distinction = "외주";
					break;
				case "WorkPlan":					//WC작업계획
					m_Table = "WP_HT";
					m_Distinction = "생산";
					break;
				default:

					break;

			}
			
			/////////////////////////////
			// 볼륨번호 저장		
			//						
			/////////////////////////////
			
			if(m_PageName != "WorkPlan")
			{
				str = "Select Max(VolumNum) From "+m_Table.ToString()+"";
				SqlCommand comm1 = new SqlCommand(str,conn);
				if(m_DataSet.Tables[0].Rows.Count==0 || comm1.ExecuteScalar() == null || comm1.ExecuteScalar().ToString().Trim() == "")
					vol = 0;
				else
					vol = int.Parse(comm1.ExecuteScalar().ToString());
			}
			conn.Close();


		}


		/// <summary>
		/// 구매납품, 외주납품페이지에서 사용되어지는 함수
		/// </summary>
		public void Registration()
		{
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{
				// 월마감 체크후 등록이 가능한지 확인한다
				if(MonthClosing() == false)
				{
					
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					RowInsert(conn,tr);
				}

				
				hc.Response.Write("<script language=javascript>");
				if(m_PageName == "PreOutStorehousePopUp" || m_PageName == "BuyingDelivery" || m_PageName == "SubBuyingDelivery" || m_PageName == "OutSideDelivery" || m_PageName == "OutReceiveingOutStorehose" )
					hc.Response.Write("alert('등록하였습니다!');");
				else
					hc.Response.Write("window.status='등록되었습니다!';");
				
				hc.Response.Write("</script>");


				tr.Commit();


			}
			catch(Exception ee)
			{
				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}


			
		}
        
		/// <summary>
		/// 그리드의 Row를 하나씩 받아와서 등록하는 함수
		/// 영업창고입고, 창고이동, 기타입출고, 상품제품출고, 매출등록, 보용품입고
		/// </summary>
		/// <returns></returns>
		public void RowRegistration()
		{
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{

				// 월마감 체크후 등록이 가능한지 확인한다
				if(MonthClosing() == false)
				{
					
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					RowInsert(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//그리드 선택번호
				}

				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("window.status='등록되었습니다!';");
				//hc.Response.Write("alert('등록되었습니다!');");
				hc.Response.Write("</script>");


				tr.Commit();


			}
			catch(Exception ee)
			{
				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}


			
		}



		/// <summary>
		/// 그리드 전체를 등록하는 함수
		/// </summary>
		public void GridRegistration()
		{
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{

				// 월마감 체크후 등록이 가능한지 확인한다
				if(MonthClosing() == false)
				{
					
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					for(int i = 0; i < m_grid.Rows.Count; i++)
					{
						GridInsert(conn,tr,i);
					}
					
				
				}

				//if(m_PageName != "RowMaterialRequirementCalculate")
				//{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("window.status='등록되었습니다!';");
				//hc.Response.Write("alert('등록하였습니다!');");
				hc.Response.Write("</script>");
				//}


				tr.Commit();


			}
			catch(Exception ee)
			{
				 
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}



		/// <summary>
		/// 데이터셋을 넘겨받아 등록하는 함수
		/// </summary>
		public void DataSetRegistration()
		{
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{


				// 월마감 체크후 등록이 가능한지 확인한다
				if(MonthClosing() == false)
				{
					
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					DataSetInsert(conn, tr);
				}

				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("window.status='등록되었습니다!';");
				//hc.Response.Write("alert('등록하였습니다!');");
				hc.Response.Write("</script>");


				tr.Commit();


			}
			catch(Exception ee)
			{
				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}


		/// <summary>
		/// 그리드에서 체크된 값들을 하나씩 입력하는 함수
		/// </summary>
		public void MainRegistration()
		{
			int a = 0;	// 선택된 것이 등록되어지면 값이 변한다
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{



				// 월마감 체크후 등록이 가능한지 확인한다
				if(MonthClosing() == false)
				{
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
				
					for(int i = 0; i < m_grid.Rows.Count; i++)
					{
						// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
						if(m_grid.Rows[i].Cells.FromKey("chk").Value == null)
						{
							m_grid.Rows[i].Cells.FromKey("chk").Value = false;
						}

						if(bool.Parse(m_grid.Rows[i].Cells.FromKey("chk").Value.ToString()))
						{
							// 각원장 등록함수
							Insert(conn,tr,i);
							a = 1;
						}
					}

					if(a == 0)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('항목을 선택해 주세요!');");
						hc.Response.Write("</script>");

					}
					else
					{
						// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
						for(int i = 0; i < m_grid.Rows.Count; i++)
						{
							m_grid.Rows[i].Cells[0].Value = false;
						}
					
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("window.status='등록되었습니다!';");
						//hc.Response.Write("alert('등록하였습니다!');");
						hc.Response.Write("</script>");
					}


					


				}

				tr.Commit();
			}
			catch(Exception ee)
			{
				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
			
			
		}


		

		private void RowInsert(SqlConnection con,SqlTransaction trans)
		{
			switch(m_PageName)
			{
				case "SubBuyingDelivery":
				{
					int a = SubBuyingDeliveryRegister(con,trans);									// 부자재구매납품원장 등록
					SubSaleTable(con,trans);													// 매입테이블에 금액증가
					SubBuyingOrderUpdate(con,trans);											// 부자재구매발주원장 진행상태및 잔량수정
					SubBuyingDeliveryHistoryRegister(con,trans,a);								// 부자재입고시 이력원장 등록

					break;
				}

				case "BuyingDelivery":												// 구매납품
				{
					// 구매납품원장인덱스번호가 필요하다 이를 변수 a 에 넣어두고 입고원장 등록시 매개변수로 던져둔다.
					int a = BuyingDeliveryRegister(con,trans);									// 구매납품원장 등록(무검사인경우 입고의뢰원장에 동시에 등록해야 하므로 
			
					
					// 구매납품의뢰원장에서 넘어온것은 납품의뢰원장의 진행상태를 변경시킨다.
					if(m_List[0].ToString() == "1")
					{
						BuyingDeliveryRequestUpdate(con,trans);								// 구매납품의뢰원장 진행상태 변경
					}
					if(Check()==true)												//무검사인경우
					{
						int b = NonBuyingDeliveryQualityInspectionRegister(con,trans,a);	//품질검사등록(검사완료처리)
													//구매납품이력 등록
						if(Division(alist[0].ToString()) == 4)						// 상품인경우
						{
							BuyingDeliveryInStoreRequestRegister(con,trans,b);					//입고의뢰원장 등록(상품인경우)
							BuyingDeliveryInStoreProductionTable(con,trans);						//생산창고테이블 수량증가(상품인 경우)
							//이력원장에 등록
							BuyingDeliveryCommodityHistory(con,trans,a);
							CommodityHistoryRegister(con,trans,a);	//구매외주매출이력원장 등록
						}
						else if(Division(alist[0].ToString()) == 1)					// 원자재인경우
						{
							BuyingDeliveryInStoreRowTable(con,trans);							//원자재 창고테이블 수량증가							
							//이력원장에 등록
							BuyingDeliveryHistory(con,trans,a);
							RowMaterialHistoryRegister(con,trans,a);	//구매외주매출이력원장 등록
						}
						SaleTable(con,trans);													// 매입테이블에 금액증가
						BuyingHistoryRegister(con,trans,a);										// 매입원장 등록
						BuyingOrderUpdate(con,trans);											// 구매발주원장 진행상태및 잔량수정
						
					}
					else// 검사인경우
					{
						//발주번호가 동일한 품질검사대기수량과 현재 입고량의 합이 잔량보다 많으면 입고시키지 않는다
						if(Compare(con, trans, int.Parse(m_List[1].ToString()),decimal.Parse(m_List[2].ToString())))
						{
							BuyingDeliveryQualityInspectionRegister(con, trans,a);						// 품질검사 원장 등록
							BO_HTUpdate(con,trans,a);// 발주원장에 입고대기수량을 넣어준다
						}
					}
				}
					break;
				
				case "OutSideDelivery":						// 외주납품
				{
					int a = OutSideDeliveryRegister(con, trans);							//외주납품원장 등록
					if(m_List[0].ToString() == "1")
					{
						OutSideDeliveryRequestUpdate(con,trans);								//외주납품의뢰원장 진행상태 변경
					}
					if(Check() ==true)						//무검사인경우
					{
						int b = NonOutSideDeliveryQualityInspectionRegister(con, trans,a);			// 품질검사 원장 등록

						OutSideDeliveryQualityHistory(con,trans,a);									// 입고이력원장 등록
						OutSideDeliveryHistoryRegister(con,trans,a);									//구매외주매출이력원장 등록
						//자산분류가 제품인동시에 납품공정이 종료공정인 경우
						if(Division(alist[0].ToString()) == 3 && Last(con,trans,alist[0].ToString(),int.Parse(alist[6].ToString())) == false)
						{
							OutSideDeliveryInStoreRequestRegister(con,trans,b);			//입고의뢰원장 등록(제품인경우)
							OutSideDeliveryInstoreRegister(con,trans,b);					//입고의뢰원장에 등록대신 영업창고에 물건 바로 입고
						}
						SaleTable(con,trans);										// 매입테이블에 금액증가
						OutSideDeliveryInStoreProductionTable(con,trans);				//창고테이블 수량증가및 감소(제품,반제품-생산창고)
						OutSideDelivertOutStoreTable(con,trans,a);						//외주창고 수량 감소(출고된 품목수량)
						OutSideDeliveryBuyingHistoryRegister(con,trans,a);				// 매입원장 등록
						OutSideOrderUpdate(con,trans);									// 외주발주원장 진행상태및 잔량수정

					}
					else									// 검사인경우
					{
						OO_HTUpdate(con,trans,a);// 외주발주원장에 입고대기수량을 넣어준다
						OutSideDeliveryQualityInspectionRegister(con, trans,a);			// 품질검사 원장 등록
					}
					
					

				}
					break;
				case "OrderPC":																// 납품의뢰
				{
					if(m_Division == "1")
						BuyingDeliveryRequestRegister(con, trans);									//구매납품의뢰원장 등록				
					else
						OutSideOrderRequestRegister(con, trans);										//외주납품의뢰원장 등록
				}
					break;
				case "OutReceiveingOutStorehose":
					OutReceiveingOutStorehoseRegister(con,trans);										//제품출고원장 등록
					OutReceiveingOutStorehouseHistroy(con,trans);										//제품출고이력원장 등록
					if(int.Parse(m_List[6].ToString()) == 1)
						BS_MTOut(con,trans);																//영업창고 출고
					else
						AS_MTOut(con,trans);																//영업창고 출고
					DS_MTIn(con,trans);																	//납품창고 입고
					break;
				default :
					OutSideStorehouseRegister(con,trans);								// 선출고
					break;
			}
		}

		private bool Compare(SqlConnection conn,SqlTransaction tr, int index, decimal quantity)
		{
			bool com = true;
			decimal total;
			string str = "Select Sum(InStoreWaitingQuantity) From BO_HT where BuyingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			comm.Parameters.Add("@index",index);
			quantity += decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

            //발주수량 확인	
			str = "Select Sum(RemainQuantity) From BO_HT where BuyingOrderHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",index);
			total = decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if((quantity - total) <= 0)
				com = true;
			else
				throw new Exception("발주량보다 많습니다! 입고대기중인지 확인해주세요!");

			return com;



		}


		private void BS_MTOut(SqlConnection conn,SqlTransaction tr)
		{
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",m_List[0].ToString());
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[7].ToString()).Year;
			int mon = DateTime.Parse(m_List[7].ToString()).Month;
			Table table = new Table(year,mon);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
	
			comm.Parameters.Add("@quantity", decimal.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@num", m_List[0].ToString());
			comm.Parameters.Add("@process", "14009999");			
			comm.Parameters.Add("@cost", decimal.Parse(m_List[5].ToString()) * cost);
			comm.Parameters.Add("@storenum",m_List[6].ToString()); 
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",m_List[0].ToString());
				comm.Parameters.Add("@storenum", m_List[6].ToString()); 
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_List[0].ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}

		/// <summary>
		/// 선출고시 보용품 창고 출고
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void AS_MTOut(SqlConnection conn,SqlTransaction tr)
		{
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",m_List[0].ToString());
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[7].ToString()).Year;
			int mon = DateTime.Parse(m_List[7].ToString()).Month;
			Table table = new Table(year,mon);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
	
			comm.Parameters.Add("@quantity", decimal.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@num", m_List[0].ToString());			
			comm.Parameters.Add("@cost", decimal.Parse(m_List[5].ToString()) * cost);
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.OutAddItemTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM AS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and  [Year] = @year";
				comm.Parameters.Add("@ItemNum",m_List[0].ToString());
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_List[0].ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}
			

		private void DS_MTIn(SqlConnection conn,SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",m_List[0].ToString());
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[7].ToString()).Year;
			int mon = DateTime.Parse(m_List[7].ToString()).Month;
			Table table = new Table(year,mon);

			comm.Parameters.Add("@quantity",  decimal.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@num", m_List[0].ToString());
			comm.Parameters.Add("@cost", decimal.Parse(m_List[5].ToString()) * cost);
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.InDeliveryTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 수주외 출고시 제품출고원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void OutReceiveingOutStorehoseRegister(SqlConnection conn,SqlTransaction tr)
		{
			string str=@"Insert into OS_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,OutStorehouseQuantity,UnInspectionQuantity,ApplyUnitCost,LotNum, OrderNum, 
				BusinessStorehouseNum,OutStoreDate,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) 
				values(@itemnum, @itemdrawnum,@itemname,@com,@comnum,@total,@uninspection, @cost,@LotNum,@OrderNum,@store,@outdate,'대기', @Person, @PersonID, @Date)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum", m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			comm.Parameters.Add("@com", m_List[3].ToString());
			comm.Parameters.Add("@comnum", m_List[4].ToString());
			comm.Parameters.Add("@total",decimal.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@uninspection",decimal.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = ApplyCost(m_List[0].ToString(),m_List[4].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@OrderNum",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@store",SqlDbType.TinyInt).Value = int.Parse(m_List[6].ToString());
			comm.Parameters.Add("@outdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[7].ToString()).ToShortDateString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

        /// <summary>
        /// 수주외 출고시 출고원장 이력등록
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tr"></param>
		private void OutReceiveingOutStorehouseHistroy(SqlConnection conn, SqlTransaction tr)
		{
			string str = "";
			int Division1 = Division(m_List[0].ToString());
			if(Division1 == 2 )
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'보용품창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
			else if(Division1 == 1)
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','보용품창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
			else
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품','영업창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;		
			comm.Parameters.Add("@itemnum", m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			comm.Parameters.Add("@Quantity", decimal.Parse(m_List[5].ToString()));			//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[7].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@ProcessSequenceNum",NumFind(m_List[0].ToString(),conn,tr));			//공정순서
			comm.Parameters.Add("@ProcessCode",CodeFind(m_List[0].ToString(),conn,tr));					//공정코드
			comm.Parameters.Add("@ProcessName",NameFind(CodeFind(m_List[0].ToString(),conn,tr),conn,tr));			//공정명
			//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOS_HT(conn,tr));									//원장번호

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//수주외 출고시 소재를 원자재 창고에 떨어주고 이력생성
			SubItemBOM_Quantity(tr,conn);
		}

		/// <summary>
		/// 제품 선 출고시 원자재 떨어주는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SubItemBOM_Quantity(SqlTransaction trans,SqlConnection con)
		{
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "";
			int year = DateTime.Parse(m_List[7].ToString()).Year;
			int mon = DateTime.Parse(m_List[7].ToString()).Month;
			Table table = new Table(year,mon);

			
			comm.CommandText = "WorkRowBomTree";
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", m_List[0].ToString());
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[5].ToString());
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);

			for(int a = 0; a < ds.Tables[0].Rows.Count; a++)
			{
				Child = ds.Tables[0].Rows[a]["ItemNum"].ToString();
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = Child;
				comm2.CommandText = str;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
				str = table.OutRowTable();
				decimal quantity = 0;
				quantity = decimal.Parse(ds.Tables[0].Rows[a]["Quantity"].ToString());

				comm2.Parameters.Add("@quantity",quantity);
				comm2.Parameters.Add("@cost",Cost*quantity);
				comm2.Parameters.Add("@num",Child);
				comm2.Parameters.Add("@year",year);
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();

				//원자재가 출고되는 이력을 기록한다.
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				
						
				comm2.Parameters.Add("@itemnum",ds.Tables[0].Rows[a]["ItemNum"].ToString().Trim());									//품목번호
				comm2.Parameters.Add("@itemdrawnum", ds.Tables[0].Rows[a]["ItemDrawNum"].ToString().Trim());						//도면번호
				comm2.Parameters.Add("@itemname",ds.Tables[0].Rows[a]["ItemName"].ToString().Trim());							//품목명
				comm2.Parameters.Add("@Quantity",quantity);			//입출고 수량
				comm2.Parameters.Add("@Date",DateTime.Parse(m_List[7].ToString()).ToShortDateString());	//입출고일
				comm2.Parameters.Add("@HistoryIndex",MaxOS_HT(con,trans));									//원장번호
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();
				

				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm2.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
					comm2.Parameters.Add("@year",DateTime.Now.Year);
					comm2.CommandText = strSQL;
					comm2.CommandType = CommandType.Text;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = Child;
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 출고처리할 수 없습니다.");
					}
					comm2.Parameters.Clear();

				}




			}
			comm.Parameters.Clear();
			
		}
		

		private decimal ApplyCost(string ItemNum, string BusinessRegistrationNum)
		{
			decimal StandardUnitCost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			string str = "Select StandardUnitCost From UCI_MT where RecodingState = 1 and ItemNum = @ItemNum and BusinessRegistrationNum = @BusinessRegistrationNum and UnitCostDistinction = '판매단가'";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@BusinessRegistrationNum", BusinessRegistrationNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				StandardUnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			conn.Close();

			return StandardUnitCost;

		}

		

		

		/// <summary>
		/// 하나씩 입력받아 등록하는 함수
		/// </summary>
		/// <param name="rowcount">선택된 그리드 행번호</param>
		private void RowInsert(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			switch(m_PageName)
			{
				case "BusinessStorehouseInStorehouse":			//영업창고입고
				{
					//상품만 처리
//					if(Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text)==4)
//					{
//						int a = BusinessStorehouseInStorehouseRegister(con,trans,rowcount);					//입고원장 등록
//						BusinessStorehouseInStorehouseHistory(con,trans,a);					//이력원장 등록
//						BusinessStorehouseInStorehouseRequestUpdate(con,trans);				//입고의뢰원장 진행상태 수정
//						BusinessStorehouseInStorehousetableUpdate(con,trans);				// 생산창고에서 출고 후 영업창고에 입고 
//						BusinessStorehouseInStorehouseQualityInspectionUpdate(con,trans);	// 품질검사원장 진행상태변경
//						if(!Check(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text))
//							BuyingDeliveryUpdate(con,trans,rowcount);									// 구매입고원장 상태 변
//					}
//					int a = BusinessStorehouseInStorehouseRegister(con,trans,rowcount);					//입고원장 등록
//					//이력원장 등록
//					BusinessStorehouseInStorehouseHistory(con,trans,a);					//이력원장 등록
//					BusinessStorehouseInStorehouseRequestUpdate(con,trans);				//입고의뢰원장 진행상태 수정
//					BusinessStorehouseInStorehousetableUpdate(con,trans);				// 생산창고에서 출고 후 영업창고에 입고 
//					BusinessStorehouseInStorehouseQualityInspectionUpdate(con,trans);	// 품질검사원장 진행상태변경
//					//넘어온 품목이 상품이고 무검사이면 진행상태 수정
//					if(!Check(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) && Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text)==4)
//						BuyingDeliveryUpdate(con,trans,rowcount);									// 구매입고원장 상태 변
				}
					break;
				case "GoodsManufactureOutStorehouse":			//상품제춤출고
					GoodsManufactureOutStorehouseRegister(con, trans, rowcount);					//출고원장에 등록
					GoodsManufactureOutStorehouseHistroy(con,trans,rowcount);						//제품출고원장 기록시 이력원장 등록

					GoodsReceivingOrderUpdate(con, trans,rowcount);									//수주원장에 수량변경

					if(Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 2 || Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 1)
					{
						AS_MTUpdate(con, trans,rowcount);												//보용품창고에 수량변경
						//하위원자재 출고
						if(Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 2)
							SubItemBOM_Quantity(con,trans,rowcount);
					}
					else if(Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 3)	//제품인경우 원자재창고 수량 소진
					{
						if(WorkPlan3())
						{
							if(!IsManaged(con,trans,m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text))//하위 원자재 출고
								SubItemBOM_Quantity(con,trans,rowcount);
							//BS_MTTable(con, trans,rowcount);												//영업창고에 수량변경
						}						
						BS_MTTable(con, trans,rowcount);												//영업창고에 수량변경
					}
					else
						BS_MTTable(con, trans,rowcount);	//영업창고에 수량변경

					DS_MTTableUpdate(con, trans,rowcount);											//납품창고에 수량변경
					
					break;
				case "OtherInOutStorehouse":					//기타입출고
				{
					OtherInOutStorehouseRegister(con, trans,rowcount);								//입출고원장등록
					OtherInOutStorehouseHistory(con,trans,rowcount);							//기타입출고이력등록
					OtherInOutStorehouseTable(con, trans,rowcount);								//창고테이블 수량변경				
				}
					break;
				case "StorehouseMoving":						//창고이동
					StorehouseMovingRegister(con, trans,rowcount);									//창고이동원장 등록
					SM_MTTableUpdate(con, trans,rowcount);											//영업창고 수량변경
					break;				
				case "SaleHistoryRegistration":					//매출등록
					SaleHistoryRegister(con, trans,rowcount);										//매출원장 등록
					if(m_grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value != null)
					{
						SaleReceivingOrderUpdate(con, trans,rowcount);									//수주원장 수량변경
					}
					SaleOutStorehouseUpdate(con, trans,rowcount);								//출고원장 수량및 내용변경
					S_HTTableUpdate(con, trans,rowcount);										//납품창고 수량감소
					S_HTBuyingSaleTableUpdate(con, trans,rowcount);											//매출테이블 금액변경
					break;

				case "AddItemInStore":
					AddItemRegister(con,trans,rowcount);												//보용품입고원장 기록
													
					if(m_List[1].ToString() == "원자재창고")
					{
						RMS_MTUpdate(con,trans,rowcount);												//원자재창고 출고
						RMS_MTAddItemHistoy(con,trans,rowcount);										//원자재창고 출고이력 기록
					}
					else
					{
						if(!WorkPlan3())
						{
							PS_MTPudate(con,trans,rowcount);												//생산창고 출고
							PS_MTAddItemHistoy(con,trans,rowcount);											//생산창고 출고이력 기록
						}
						else
						{
							if(IsManaged(con,trans,m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString().Trim()))
							{
								PS_MTPudate(con,trans,rowcount);												//생산창고 출고
								PS_MTAddItemHistoy(con,trans,rowcount);											//생산창고 출고이력 기록
							}
							else
								InsertStore(con,trans,rowcount);							//하위품목 떨어줌
						}
					}
					AddItemHistoy(con,trans,rowcount);													//보용품입고이력기록
					AS_MTPudate(con,trans,rowcount);													//보용품창고입고					
					break;
				default :
					break;
			}

			

		}

		/// <summary>
		/// 작업계획3 일때 생산창고 품목이 보용품으로 갈때 하위 원자재 창고 수량을 떨어줌
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void InsertStore(SqlConnection conn, SqlTransaction tr,int count)
		{
			SqlCommand comm = new SqlCommand("dbo.WorkRowBomTree", conn);
			comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@Quantity", decimal.Parse(m_List[0].ToString()));
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				SubRMS_MTUpdate(conn,tr,dr["ItemNum"].ToString(),decimal.Parse(dr["Quantity"].ToString()));												//원자재창고 출고
				SubRMS_MTAddItemHistoy(conn,tr,dr["ItemNum"].ToString(),decimal.Parse(dr["Quantity"].ToString()));										//원자재창고 출고이력 기록
			}
		}

		

		/// <summary>
		/// 데이터셋을 등록하는 함수
		/// </summary>
		private void DataSetInsert(SqlConnection con, SqlTransaction trans)
		{
			if(m_PageName == "OutsideRequest")			//외주의뢰
			{
				for(int i=0; i<m_DataSet.Tables[0].Rows.Count; i++)
				{
					OutsideRequestRegister(con,trans,i);							// 외주의뢰원장 등록
					WCPlanUpdate(con,trans,i);										// WC일자별작업계획원장 진행상태(완료) 수정
				}
			}
			else
			{
				for(int i=0; i<m_DataSet.Tables[1].Rows.Count; i++)
				{
					switch(m_PageName)
					{
						case "BuyingOrder":					//구매발주
						{
							BuyingOrderRegister(con,trans,i);								// 구매발주원장 등록
							BuyingRequestUpdate(con,trans,i);								// 구매의뢰원장 진행상태(완료) 수정
						}
							break;
						case "OutSideOrder":				//외주발주
						{
							OutSideOrderRegister(con,trans,i);								// 외주발주원장 등록
							OutSideOrderRequestUpdate(con,trans,i);						// 외주의뢰원장 진행상태(완료) 수정
						}
							break;
						default:
							break;
					}
				}
			}

			
			
		}



		/// <summary>
		/// 선택항목을 원장에 등록하는 함수
		/// </summary>
		/// <param name="count">그리드의 행번호를 넘겨준다</param>
		private void Insert(SqlConnection con, SqlTransaction trans, int count)
		{
			
			switch(m_PageName)
			{
				case "ProductionRequest":						//생산의뢰 등록
					ProductionRequestRegister(con,trans,count);				//생산의뢰원장등록
					ReceivingOrderHistoryUpdate(con,trans,count);				//수주원장 진행상태 수정
					break;
				case "GoodsBuyingRequest":						//상품구매의뢰 등록
					GoodsBuyingRequestRegister(con,trans,count);				//구매의뢰원장 등록
					ReceivingOrderHistoryUpdate(con,trans,count);				//수주원장 진행상태 수정
					break;
				case "ProductionPlan":							//생산계획 등록
					ProductionPlanRegister(con,trans,count);					//생산계획원장등록
					ProductionRequestUpdate(con,trans,count);					//생산의뢰원장 진행상태 수정
					break;
				case "ExecutionPlanInfo":						//실행계획 생산수립버튼 클릭
					ProductionPlanRegister(con,trans,count);					//생산계획원장등록
					ExecutionPlanUpdate(con,trans,count);								//실행계획테이블 필드 수정
					break;
				case "BusinessStorehouseInStorehouse":
				{
					int a = BusinessStorehouseInStorehouseRegister(con,trans,count);					//입고원장 등록
					//이력원장 등록
					BusinessStorehouseInStorehouseHistory(con,trans,a,count);					//이력원장 등록
					BusinessStorehouseInStorehouseRequestUpdate(con,trans,count);				//입고의뢰원장 진행상태 수정
					BusinessStorehouseInStorehousetableUpdate(con,trans,count);				// 생산창고에서 출고 후 영업창고에 입고 
					BusinessStorehouseInStorehouseQualityInspectionUpdate(con,trans,count);	// 품질검사원장 진행상태변경
					//넘어온 품목이 상품이고 무검사이면 진행상태 수정
					if(!Check(m_grid.Rows[count].Cells.FromKey("ItemNum").Text) && Division(m_grid.Rows[count].Cells.FromKey("ItemNum").Text)==4)
						BuyingDeliveryUpdate(con,trans,count);	
					break;
				}

				default :
					break;
			}

			
		}


		/// <summary>
		/// 그리드 전체를 등록하는 함수
		/// </summary>
		/// <param name="count">그리드 Row인덱스번호</param>
		private void GridInsert(SqlConnection con, SqlTransaction trans, int count)
		{
			
			switch(m_PageName)
			{
				
				case "RowMaterialRequirementCalculate":														//자재소요량 산출원장 등록
					RowMaterialRequirementCalculateRegister(con, trans, count);								//생산계획원장 자재소요산출 상태 업데이트
					PP_HTUpdate(con,trans);
					//자재의뢰원장 등록
					RowMaterialRequriementRegist(con,trans,count);

					break;
				case "RowMaterialRequriement":																//원자재 구매의뢰 등록
					RowMaterialRegister(con, trans, count);													//구매의뢰원장 등록
					RowMaterialRequirementUpdate(con, trans, count);										//자재소요원장 진행상태 수정
					break;
				case "OutSideOutStorehouse":																// 외주출고
					OutSideStorehouseRegister(con, trans, count);											//외주출고원장 등록					
					break;
				
				default :
					break;
			}
			

		}


		/// <summary>
		/// 기존의 진행, 대기는 삭제한뒤 새롭게 인서트 해야 한다.
		/// </summary>
		private void WCPlanDelete(SqlConnection conn, SqlTransaction tr)
		{

			string str = "Delete From WDWP_HT Where ProgressCondition = '대기' or ProgressCondition = '진행'";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rowcount"></param>
		private void WCPlanInsert(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "Insert into WDWP_HT (ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, PreProcessName, ProductItemNum, ProductDrawNum, ProductName, ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, ProductionPlanHistoryIndex) Values("
				+"@ItemNum, @ItemDrawNum, @ItemName, @ProcessSequenceNum, @ProcessCode, @ProcessName, @PreProcessName, @ProductItemNum, @ProductDrawNum, @ProductName, @ParentItemNum, @ParentDrawNum, @ParentName, @WCName, @WorkDistinction, @WorkPlanQuantity, @WorkCompletionQuantity, @OrderLeadTime, @WorkDate, @ProgressCondition, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate, @ProductionPlanHistoryIndex, )";
            SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemNum"].ToString().Trim();
			comm.Parameters.Add("@ItemDrawNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemDrawNum"].ToString().Trim();
			comm.Parameters.Add("@ItemName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemName"].ToString().Trim();
			comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.TinyInt).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["ProcessSequenceNum"].ToString());
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProcessName"].ToString();
			comm.Parameters.Add("@PreProcessName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["PreProcessName"].ToString();
			comm.Parameters.Add("@ProductItemNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProductItemNum"].ToString().Trim();
			comm.Parameters.Add("@ProductDrawNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProductDrawNum"].ToString().Trim();
            comm.Parameters.Add("@ProductName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProductName"].ToString().Trim();
			comm.Parameters.Add("@ParentItemNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ParentItemNum"].ToString().Trim();
			comm.Parameters.Add("@ParentDrawNum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ParentDrawNum"].ToString().Trim();
			comm.Parameters.Add("@ParentName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ParentName"].ToString().Trim();
			comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["WCName"].ToString().Trim();
			comm.Parameters.Add("@WorkDistinction",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["WorkDistinction"].ToString();
			comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["WorkPlanQuantity"].ToString());
			comm.Parameters.Add("@WorkCompletionQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["WorkCompletionQuantity"].ToString());
			comm.Parameters.Add("@OrderLeadTime",SqlDbType.Decimal).Value = decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["OrderLeadTime"].ToString());
			comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["WorkDate"].ToString()).ToShortDateString();
			comm.Parameters.Add("@ProgressCondition",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ProgressCondition"].ToString();
			comm.Parameters.Add("@RegistrationPerson",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@RegistrationPersonID",SqlDbType.VarChar).Value = m_User;
			comm.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			
			comm.Parameters.Add("@ProductionPlanHistoryIndex",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["ProductionPlanHistoryIndex"].ToString());
			//comm.Parameters.Add("@WorkPlanHistoryIndex",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["WorkPlanHistoryIndex"].ToString());




		
			comm.ExecuteNonQuery();

		}

		private void WorkPlanUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
//			string str = "Update WP_HT Set ProgressCondition = '완료' where WorkPlanHistoryIndex = @index";
//			SqlCommand comm = new SqlCommand(str,conn);
//			comm.Transaction = tr;
//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["WorkPlanHistoryIndex"].ToString());
//			comm.ExecuteNonQuery();

		}


		
		/// <summary>
		/// 자재소요산출페이지에서 생산계획원장 업데이트 시키는 함수
		/// </summary>
		/// <param name="grid">생산계획원장 그리드</param>
		public void PP_HTUpdate(SqlConnection conn, SqlTransaction tr)
		{
			HttpContext hc = HttpContext.Current;
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			for(int i = 0; i < m_grid1.Rows.Count; i++)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(m_grid1.Rows[i].Cells.FromKey("chk").Value == null)
				{
					m_grid1.Rows[i].Cells.FromKey("chk").Value = false;
				}

				if(bool.Parse(m_grid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
				{
					// 생산계획원장 업데이트함수
					ProductionPlanUpdate(conn, tr,i);
				}
			}				
		}


		/// <summary>
		/// WC별 일자별작업계획원장 진행상태 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void WCPlanUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{			
			string str = "UPDATE WDWP_HT SET ProgressCondition = '완료' WHERE WCDailyWorkPlanHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["WCDailyWorkPlanHistoryIndex"].ToString());
	
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 생산계획등록함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void ProductionPlanRegister(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			

			switch(m_PageName)
			{
				case "ProductionPlan":
				{
					//생산계획 품목이 팬텀인경우에는 BOM에 하위 제품에 대해 생산계획을 세운다.
					if(Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString()) == 5)
					{
						string str = @"Select ChildItemNum,ItemDrawNum,ItemName, (NeedQuantityNumerator/NeedQuantityDenominator) as Quantity 
							From IOI_MT inner join II_MT on IOI_MT.ChildItemNum = II_MT.ItemNum 
							Where ParentItemNum = @ItemNum and IOI_MT.RecodingState = 1 and II_MT.RecodingState = 1";
						SqlCommand comm = new SqlCommand();
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.CommandText = str;
						comm.Connection = conn;
						comm.Transaction = tr;
						DataSet ds = new DataSet();
						SqlDataAdapter da = new SqlDataAdapter(comm);
						da.Fill(ds);
						comm.Parameters.Clear();

						foreach(DataRow dr in ds.Tables[0].Rows)
						{
						
							str="Insert into PP_HT (ItemNum,ItemDrawNum,ItemName,ProductionPlanHistorySourceCode,ProductionPlanHistorySource,"+
								"ProductionPlanQuantity,ProductionBeginDate,DeliveryDate,RowMaterialCalculation,VolumNum,MaterialRequirementVolumNum,ProgressCondition,"+
								"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
								"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@total,@begindate,@DeliveryDate,@calculation,@num,@MaterialRequirementVolumNum,'대기', @Person, @PersonID, @Date, @idx,'생산의뢰')";
						
							comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = dr["ItemDrawNum"].ToString();
							comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = dr["ItemName"].ToString();
							comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03200010";
							comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			
							if(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value.ToString().Trim() =="")
								comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
							else
								comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(dr["Quantity"].ToString())* decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value.ToString().Trim());
							comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate.ToString());
							if(Convert.IsDBNull(dr["RequestDate1"].ToString()))
                                comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate.ToString());
							else
								comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(dr["RequestDate1"].ToString()).ToShortDateString();
							comm.Parameters.Add("@calculation",SqlDbType.Bit).Value = 0;			// 미산출은 0, 산출은1		
							comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
							comm.Parameters.Add("@MaterialRequirementVolumNum",SqlDbType.Int).Value = vol+1;
							comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
							comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
							comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestHistoryIndex").Text);
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();

						}
						
						
						
					}
					else
					{
						string str="Insert into PP_HT (ItemNum,ItemDrawNum,ItemName,ProductionPlanHistorySourceCode,ProductionPlanHistorySource,"+
							"ProductionPlanQuantity,ProductionBeginDate,DeliveryDate,RowMaterialCalculation,VolumNum,MaterialRequirementVolumNum,ProgressCondition,"+
							"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
							"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@total,@begindate,@DeliveryDate,@calculation,@num,@MaterialRequirementVolumNum,'대기', @Person, @PersonID, @Date, @idx,'생산의뢰')";
						SqlCommand comm = new SqlCommand(str,conn);
						comm.Transaction = tr;

						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
						comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03200010";
						comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			
						if(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value.ToString().Trim() =="")
							comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Text);
						comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate.ToString());
						
						if(Convert.IsDBNull(m_grid.Rows[rowcount].Cells.FromKey("RequestDate1").Value.ToString()))
							comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate.ToString());
						else
							comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("RequestDate1").Value.ToString()).ToShortDateString();
						comm.Parameters.Add("@calculation",SqlDbType.Bit).Value = 0;			// 미산출은 0, 산출은1		
						comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;				
						comm.Parameters.Add("@MaterialRequirementVolumNum",SqlDbType.Int).Value = vol+1;
						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
						comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestHistoryIndex").Text);
						comm.ExecuteNonQuery();
					}
				}
					break;
				case "ExecutionPlanInfo":
				{
					string str="Insert into PP_HT (ItemNum,ItemDrawNum,ItemName,ProductionPlanHistorySourceCode,ProductionPlanHistorySource,"+
						"ProductionPlanQuantity,ProductionBeginDate,DeliveryDate,RowMaterialCalculation,VolumNum,MaterialRequirementVolumNum,ProgressCondition,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
						"@itemnum, @itemdrawnum,@itemname,'03200020','실행계획',@total,@begindate,@DeliveryDate,@calculation,@num,@MaterialRequirementVolumNum,'대기', @Person, @PersonID, @Date, @idx,'실행계획')";
					SqlCommand comm = new SqlCommand(str,conn);
					comm.Transaction = tr;
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();

					if(m_grid.Rows[rowcount].Cells.FromKey("PlanQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("PlanQuantity").Value.ToString().Trim() =="")
						comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("PlanQuantity").Text);
					comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate.ToString());
					comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = m_grid.Rows[rowcount].Cells.FromKey("PlanDate").Value.ToString().Trim();
					comm.Parameters.Add("@calculation",SqlDbType.Bit).Value = 0;			// 미산출은 0, 산출은1		
					comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;				
					comm.Parameters.Add("@MaterialRequirementVolumNum",SqlDbType.Int).Value = vol+1;
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ExecutionPlanInfoIndex").Text);
					comm.ExecuteNonQuery();
				}
					break;
			}
		}

		/// <summary>
		/// 생산의뢰원장 업데이트
		/// </summary>
		/// <param name="rowcount"></param>
		private void ProductionRequestUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE PR_HT SET ProgressCondition = '완료' WHERE ProductionRequestHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProductionRequestHistoryIndex").Text);

			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 실행계획 테이블 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void ExecutionPlanUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "Update EPI_MT Set State = @state Where ItemNum = @num and PlanDate = @date";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@date",DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("PlanDate").Text).ToShortDateString());
			comm.Parameters.Add("@state",1);
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 자재소요량 산출원장 등록
		/// </summary>
		/// <param name="rowcount"></param>
		private void RowMaterialRequirementCalculateRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Insert into MRC_HT (ItemNum,ItemDrawNum,ItemName,DeliveryOrderRequestQuantity,RowMaterialQuantityReflection,OrderNonInStorehouseReflection,"+
				"OrderRequestReadyQuantityReflection,SafyRowMaterialQuantityReflection,MinimumOrderQuantityReflection,OrderGapQuantityReflection, "+
				"VolumNum,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
				"@itemnum, @itemdrawnum,@itemname,@quantity,@row,@order,@request,@safy,@min,@gap,@num,'대기', @Person, @PersonID, @Date)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Text);
			comm.Parameters.Add("@row",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("RowMaterialQuantityReflection").Value.ToString();
			comm.Parameters.Add("@order",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("OrderNonInStorehouseReflection").Value.ToString();
			comm.Parameters.Add("@request",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("OrderRequestReadyQuantityReflection").Value.ToString();
			comm.Parameters.Add("@safy",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("SafyRowMaterialQuantityReflection").Value.ToString();
			comm.Parameters.Add("@min",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("MinimumOrderQuantityReflection").Value.ToString();
			comm.Parameters.Add("@gap",SqlDbType.Bit).Value = m_grid.Rows[rowcount].Cells.FromKey("OrderGapQuantityReflection").Value.ToString();
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;				
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

//			string str="Insert into MRC_HT (ItemNum,ItemDrawNum,ItemName,DeliveryOrderRequestQuantity,VolumNum,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
//				"@itemnum, @itemdrawnum,@itemname,@quantity,@num,'대기', @Person, @PersonID, @Date)";
//
//
//			SqlCommand comm = new SqlCommand(str,conn);
//			comm.Transaction = tr;
//
//			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
//			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
//			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
//			
//			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Text);
//			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;				
//			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
//			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
//			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
//			comm.ExecuteNonQuery();
//			comm.Parameters.Clear();
		}

		private int MaxMRC_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str="Select Max(MaterialRequirementCalculateHistoryIndex) From MRC_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int index = int.Parse(comm.ExecuteScalar().ToString());

			return index;
		}



		/// <summary>
		/// 자재의뢰원장에 바로 등록하는 함
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void RowMaterialRequriementRegist(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			int index = MaxMRC_HT(conn,tr);
			string str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,VolumNum,RequestPostCode,RequestPost,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum, @itemdrawnum,@itemname,'원자재',@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@num,@postcode,@post,'대기', @Person, @PersonID, @Date,@idx,@section)";


			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03300010";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Text);
			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Date);
			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryOrderRequestQuantity").Text);
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = "";//의뢰부서코드
			comm.Parameters.Add("@post",SqlDbType.VarChar).Value = "";//의뢰부서
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = index;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "자재소요";		
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str = "Update MRC_HT Set ProgressCondition = '완료' where MaterialRequirementCalculateHistoryIndex = @Index";
			comm.Parameters.Add("@Index",index);
			comm.CommandText =str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}

		private decimal UnitCost(string ItemNum,SqlConnection con, SqlTransaction trans)
		{
			decimal ApplyCost = 0;
			string str = @"Select StandardUnitCost From UCI_MT where RecodingState =1 and ItemNum = @ItemNum and UnitCostDistinction = '구매단가' and OrderRate = (Select Max(OrderRate) From UCI_MT where RecodingState =1 and ItemNum = @ItemNum and UnitCostDistinction = '구매단가') ";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.CommandText = str;
			comm.Transaction = trans;
			comm.Parameters.Add("@itemnum",ItemNum);

			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ApplyCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			return ApplyCost;
		}



		/// <summary>
		/// 생산계획원장의 자재소요 산출여부 컬럼 수정
		/// </summary>
		private void ProductionPlanUpdate(SqlConnection con, SqlTransaction trans,int rowcount)
		{
		
			string str = "UPDATE PP_HT SET RowMaterialCalculation = '1' , MaterialRequirementVolumNum = @vol WHERE ProductionPlanHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction = trans;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_grid1.Rows[rowcount].Cells.FromKey("ProductionPlanHistoryIndex").Text);
			comm.Parameters.Add("@vol", SqlDbType.Int).Value = volum+1;

			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 구매의뢰원장 등록(원자재)
		/// </summary>
		private void RowMaterialRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			
			string str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,VolumNum,RequestPostCode,RequestPost,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum, @itemdrawnum,@itemname,'원자재',@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@num,@postcode,@post,'대기', @Person, @PersonID, @Date,@idx,@section)";


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03300010";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			

			// 1차에 요구량과 요구일이 있는지 판단해서 없으면 요구량과 넘어온 날짜를 1차에 넣어준다
			if(m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString().Trim() =="" || 
				m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString().Trim() =="")
			{
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Text);
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_ProductionDate);
			}
			else
			{
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Text);
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString().Trim());
			}
			if(m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value == null || m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Text);
			
			if(m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value.ToString().Trim());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value.ToString().Trim());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value == null || m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value.ToString().Trim());
			

			if(m_grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@total",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Text);

            decimal Total = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Text);			

			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Convert.DBNull;//ItemCost(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim(), conn, tr);
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Convert.DBNull;//TotalCost(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim(), conn, tr, rowcount, Total);


//			if(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@cost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);
//
//			if(m_grid.Rows[rowcount].Cells.FromKey("TotalCost").Value == null || m_grid.Rows[rowcount].Cells.FromKey("TotalCost").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@totalcost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("TotalCost").Text);

			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			
			comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = "";//의뢰부서코드
			comm.Parameters.Add("@post",SqlDbType.VarChar).Value = "";//의뢰부서

			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text);
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "자재소요";			
			comm.ExecuteNonQuery();
		}



		private decimal ItemCost(string Item, SqlConnection Con, SqlTransaction trans)
		{
			decimal UnitCost = 0;
			string str = "Select StandardUnitCost From UCI_MT where UnitCostDistinction = '구매단가' and RecodingState = 1 and ItemNum = @ItemNum and OrderRate = (Select Max(OrderRate) From UCI_MT where UnitCostDistinction = '구매단가' and RecodingState = 1 and ItemNum = @ItemNum ) ";
			SqlCommand comm = new SqlCommand(str,Con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",Item);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				UnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			return UnitCost;
		}

		private decimal TotalCost(string Item, SqlConnection Con, SqlTransaction trans, int row, decimal Total)
		{
			
			decimal UnitCost = 0;
			string str = "Select StandardUnitCost From UCI_MT where UnitCostDistinction = '구매단가' and RecodingState = 1 and ItemNum = @ItemNum and OrderRate = (Select Max(OrderRate) From UCI_MT where UnitCostDistinction = '구매단가' and RecodingState = 1 and ItemNum = @ItemNum ) ";
			SqlCommand comm = new SqlCommand(str,Con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",Item);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				UnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			decimal TotalUnitCost = UnitCost * Total;

			return TotalUnitCost;
		}

		/// <summary>
		/// 원자재 구매의뢰후 자재소요원장 진행상태 수정 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void RowMaterialRequirementUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "UPDATE MRC_HT SET ProgressCondition = '완료' WHERE MaterialRequirementCalculateHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text);
			comm.ExecuteNonQuery();
		}





		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////// 구매, 외주 관련 함수들 //////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// 구매발주원장 등록
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingOrderRegister(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Insert into BO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,OrderRate,RemainQuantity,StoreQuantity,VolumNum,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,BuyingRequestHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@orderrate,@remainquantity, @storequantity,@num,'대기', @Person, @PersonID, @Date,@idx)";


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemNum"].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemDrawNum"].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemName"].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["CompanyName"].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["BusinessRegistrationNum"].ToString();
			
			if(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value =  Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString().Trim() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString().Trim()  =="")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString().Trim()  == "")
				comm.Parameters.Add("@total",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString().Trim()  == "")
				comm.Parameters.Add("@cost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString().Trim()  == "")
				comm.Parameters.Add("@totalcost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString() =="")
				comm.Parameters.Add("@orderrate",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@orderrate",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString() == "")
				comm.Parameters.Add("@remainquantity",SqlDbType.Float).Value = 0;
			else
				comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString());
			

			comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = StoreQuantity(conn,tr,m_DataSet.Tables[1].Rows[rowcount]["ItemNum"].ToString());
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[1].Rows[rowcount]["BuyingRequestHistoryIndex"].ToString());

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}


	
		/// <summary>
		/// 자산분류 찾는 메서드
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="item"></param>
		private string PropertyClassification(SqlConnection conn, SqlTransaction tr,string item)
		{
			string m_PropertyClassification = "원자재";
			string str = "Select PropertyClassification From II_MT where RecodingState =1 and ItemNum=@item";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				m_PropertyClassification = dr["PropertyClassification"].ToString();
			dr.Close();
			comm.Parameters.Clear();

			return m_PropertyClassification;
		}

		/// <summary>
		/// 구매발주시 현재고량을 입력하는 메서드
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private decimal StoreQuantity(SqlConnection conn, SqlTransaction tr,string item)
		{

			
			string m_PropertyClassification = PropertyClassification(conn,tr,item);
			string str = "";
			decimal m_Quantity = 0;

			
			if(m_PropertyClassification == "원자재")
				str = "Select StockQuantity12 From RMS_MT where RecodingState =1 and [Year] = @year and ItemNum = @item";
			else
				str = "Select StockQuantity12 From BS_MT where RecodingState =1 and [Year] = @year and ItemNum = @item and BusinessStorehouseNum =1";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
			comm.Parameters.Add("@year", SqlDbType.Int).Value = DateTime.Now.Year;

			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				m_Quantity = Decimal.Parse(dr["StockQuantity12"].ToString());
			dr.Close();

			return m_Quantity;
		}



		/// <summary>
		/// 구매발주원장 등록후 구매의뢰원장 진행상태를 완료로 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingRequestUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			
			string str = "UPDATE BR_HT SET ProgressCondition = '완료' WHERE BuyingRequestHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[1].Rows[rowcount]["BuyingRequestHistoryIndex"].ToString());
	
			comm.ExecuteNonQuery();
		}
	



		/// <summary>
		/// 외주발주원장 등록함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideOrderRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = @"Insert into OO_HT (ItemNum,ItemDrawNum,ItemName,UnitCostDistinction,BeginProcessCode,BeginProcess,EndProcessCode,EndProcess,CompanyName,BusinessRegistrationNum,
				OrderRate,FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,ThirdDeliveryDemandQuantity,
				ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,
				ApplyUnitCost,TotalCost,RemainQuantity,VolumNum,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderRequestHistoryIndex) values(
				@itemnum, @itemdrawnum,@itemname,@distinction,@beginsourcecode,@beginsource,@endsourcecode,@endsource,@com,@comnum,@rate,@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@remain,@num,'대기', @Person, @PersonID, @Date,@idx)";


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
            
			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemNum"].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemDrawNum"].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["ItemName"].ToString();
			comm.Parameters.Add("@distinction",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["UnitCostDistinction"].ToString();
			comm.Parameters.Add("@beginsourcecode",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["BeginProcessCode"].ToString();
			comm.Parameters.Add("@beginsource",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["BeginProcess"].ToString();
			comm.Parameters.Add("@endsourcecode",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["EndProcessCode"].ToString();
			comm.Parameters.Add("@endsource",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["EndProcess"].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["CompanyName"].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_DataSet.Tables[1].Rows[rowcount]["BusinessRegistrationNum"].ToString();
			
			if(m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString() =="")
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["OrderRate"].ToString());

			if(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FirstDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["SecondDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString());

			if(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString().Trim()  =="")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FourthDeliveryDemandDate"].ToString());

			if(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[1].Rows[rowcount]["FifthDeliveryDemandDate"].ToString());
	
			if(m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString() =="")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["OrderQuantity"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString() =="")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["ApplyUnitCost"].ToString());
			
			if(m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString() =="")
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["TotalCost"].ToString());
			if(m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString() == null || m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[1].Rows[rowcount]["RemainQuantity"].ToString());
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[1].Rows[rowcount]["OutSideOrderRequestHistoryIndex"].ToString());

			comm.ExecuteNonQuery();
		}

        
		/// <summary>
		/// 외주의뢰원장 진행상태 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideOrderRequestUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			
			string str = "UPDATE OOR_HT SET ProgressCondition = '완료' WHERE OutSideOrderRequestHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[1].Rows[rowcount]["OutSideOrderRequestHistoryIndex"].ToString());
			comm.ExecuteNonQuery();
		}





		/// <summary>
		/// 외주의뢰원장 등록
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutsideRequestRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
		{

			string str="Insert into OOR_HT (ItemNum,ItemDrawNum,ItemName,UnitCostDistinction,BeginProcessCode,BeginProcess,EndProcessCode,EndProcess,ApplyUnitCost,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,TotalCost,VolumNum,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,WCDailyWorkPlanHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@distinction,@beginsourcecode,@beginsource,@endsourcecode,@endsource,"+
				"@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@num,'대기', @Person, @PersonID, @Date,@idx)";


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemNum"].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemDrawNum"].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["ItemName"].ToString();
			comm.Parameters.Add("@distinction",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["UnitCostDistinction"].ToString();
			comm.Parameters.Add("@beginsourcecode",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["BeginProcessCode"].ToString();
			comm.Parameters.Add("@beginsource",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["BeginProcess"].ToString();
			comm.Parameters.Add("@endsourcecode",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["EndProcessCode"].ToString();
			comm.Parameters.Add("@endsource",SqlDbType.VarChar).Value = m_DataSet.Tables[0].Rows[rowcount]["EndProcess"].ToString();
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["FirstDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[0].Rows[rowcount]["FirstDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[0].Rows[rowcount]["FirstDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["FirstDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["SecondDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[0].Rows[rowcount]["SecondDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[0].Rows[rowcount]["SecondDeliveryDemandDate"].ToString().Trim() =="")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["SecondDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["ThirdDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[0].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[0].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["ThirdDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["FourthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[0].Rows[rowcount]["FourthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[0].Rows[rowcount]["FourthDeliveryDemandDate"].ToString().Trim()  =="")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["FourthDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["FifthDeliveryDemandQuantity"].ToString());
			if(m_DataSet.Tables[0].Rows[rowcount]["FifthDeliveryDemandDate"].ToString() == null || m_DataSet.Tables[0].Rows[rowcount]["FifthDeliveryDemandDate"].ToString().Trim()  == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_DataSet.Tables[0].Rows[rowcount]["FifthDeliveryDemandDate"].ToString());
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["OrderQuantity"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["OrderQuantity"].ToString().Trim() == "")	
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["OrderQuantity"].ToString());
			
			
			if(m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["TotalCost"].ToString() == null || m_DataSet.Tables[m_Table.ToString()].Rows[rowcount]["TotalCost"].ToString().Trim() == "")	
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_DataSet.Tables[0].Rows[rowcount]["TotalCost"].ToString());
			
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_DataSet.Tables[0].Rows[rowcount]["WCDailyWorkPlanHistoryIndex"].ToString());

			comm.ExecuteNonQuery();
		}
		



		




		


		/// <summary>
		/// 구매납품원장 등록
		/// </summary>
		/// <param name="rowcount"></param>
		private int BuyingDeliveryRegister(SqlConnection conn,SqlTransaction tr)
		{
			int a =0;
			

			string str="";
			if(m_List[0].ToString() == "0")
			{
				str="Insert into BD_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,DeliveryQuantity,NowStockQuantity,DeliveryDate,StandardUnitCost,ApplyUnitCost,TotalCost,LotNum, [Year] ,[Month],"+
					"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,BuyingOrderHistoryIndex) values("+
					"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@total,@NowStockQuantity,@DeliveryDate, @standardcost, @cost,@totalcost,@LotNum,@year, @mon,'대기', @Person, @PersonID, @Date,@idx)";
			}
			else
			{
				str="Insert into BD_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,DeliveryQuantity,NowStockQuantity,DeliveryDate,StandardUnitCost,ApplyUnitCost,TotalCost,LotNum,[Year] ,[Month],"+
					"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,BuyingOrderHistoryIndex, BuyingDeliveryRequestHistoryIndex) values("+
					"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@total,@NowStockQuantity, @DeliveryDate,@standardcost, @cost,@totalcost,@LotNum,@year, @mon,'대기', @Person, @PersonID, @Date,@idx, @idx1)";
			}

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
			
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());
			comm.Parameters.Add("@NowStockQuantity",NowStockQuantity(alist[0].ToString(),conn,tr));

			comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse((m_List[3].ToString()));
			
			comm.Parameters.Add("@standardcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
					
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString()) * decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[7].ToString()));
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(alist[6].ToString());
		
			if(m_List[0].ToString() == "1")
				comm.Parameters.Add("@idx1",SqlDbType.Int).Value = int.Parse(alist[7].ToString());
						
			comm.ExecuteNonQuery();
				
			str = "Select Max(BuyingDeliveryHistoryIndex) From BD_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			a = Convert.ToInt32(comm1.ExecuteScalar());
			
			return a ;
			
		}

		private decimal NowStockQuantity(string ItemNum,SqlConnection con,SqlTransaction trans)
		{
			int division = Division(ItemNum);
			int Year = DateTime.Now.Year;
			int Month = DateTime.Now.Month;
			decimal Quantity = 0;
			string str = "";
			if(division == 1)
			{
				str = "Select  StockQuantity"+Month+" as StockQuantity From RMS_MT where ItemNum = @ItemNum and RecodingState =1 and [Year] = @Year";
			}
			else 
			{
				str = "Select  StockQuantity"+Month+" as StockQuantity From BS_MT where ItemNum = @ItemNum and BusinessStorehouseNum  =1 and RecodingState =1 and [Year] = @Year";
			}
			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@Year",Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["StockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 구매납품 등록시 구매납품의뢰원장 진행상태 완료로변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingDeliveryRequestUpdate(SqlConnection conn,SqlTransaction tr)
		{				
			string str = "UPDATE BDR_HT SET ProgressCondition = '완료' WHERE BuyingDeliveryRequestHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[7].ToString());
			comm.ExecuteNonQuery();
		}

		


		/// <summary>
		/// 구매납품원장등록시 무검사인 상품인경우 입고의뢰원장 에 등록
		/// </summary>
		/// <param name="rowcount"></param>
		/// <param name="aa">구매납품원장인덱스</param>
		private void BuyingDeliveryInStoreRequestRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str="Insert into ISR_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,InstorehouseRequestQuantity, InStoreDate, "+
				"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum,@itemdrawnum,@itemname,'99','14009999','상품',@com,@comnum,@quantity,@Instoredate,'대기', @Person, @PersonID, @Date,@idx,@section)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
			
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString().Trim());
			comm.Parameters.Add("@Instoredate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[3].ToString().Trim()).ToShortDateString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = aa;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "품질검사";
			
			
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 구매납품중 무검사인 상품에 대해 생산창고에 입고수량 증가 함수
		/// </summary>
		/// <param name="count"></param>
		private void BuyingDeliveryInStoreProductionTable(SqlConnection conn, SqlTransaction tr)
		{
			Decimal m_cost = 0;

			int year = DateTime.Parse(m_List[3].ToString()).Year;
			int mon = DateTime.Parse(m_List[3].ToString()).Month;

			//int year = int.Parse(m_List[6].ToString());
			//int mon = int.Parse(m_List[7].ToString());
			
		
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());
				
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_cost.ToString())*Decimal.Parse(m_List[2].ToString());
			comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = 0;
			comm.Parameters.Add("@code","14009999");
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			
			comm.CommandText= table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		} 

		/// <summary>
		/// 구매납품중 무검사인 원자재에 대해 원자재창고에 입고수량 증가 함수
		/// </summary>
		private void BuyingDeliveryInStoreRowTable(SqlConnection conn, SqlTransaction tr)
		{
			
			int year = DateTime.Parse(m_List[3].ToString()).Year;
			int mon = DateTime.Parse(m_List[3].ToString()).Month;

//			int year = int.Parse(m_List[6].ToString());
//			int mon = int.Parse(m_List[7].ToString());

			decimal m_cost = decimal.Parse(m_List[4].ToString());

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
				
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_List[2].ToString()) ;
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
				
			comm.CommandText= table.InRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


		}

		private void RowMaterialHistoryRegister(SqlConnection con,SqlTransaction trans, int aa)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,0,'14000000','소재',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'등록', @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			comm.Parameters.Add("@CompanyName", alist[3].ToString());								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",alist[4].ToString());
			//comm.Parameters.Add("@ProcessSequenceNum",0);											//공정순서
			//comm.Parameters.Add("@ProcessCode","14000000");											//공정코드
			//comm.Parameters.Add("@ProcessName","소재");												//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입출고 수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_List[4].ToString()));
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 무검사 원자재 구매입고시 등록하는 이력
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="aa">납품원장번호</param>
		private void BuyingDeliveryHistory(SqlConnection con,SqlTransaction trans, int aa)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','1',@Quantity,@Date,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			//comm.Parameters.Add("@ProcessSequenceNum",0);											//공정순서
			//comm.Parameters.Add("@ProcessCode","14000000");											//공정코드
			//comm.Parameters.Add("@ProcessName","소재");												//공정명
			//comm.Parameters.Add("@Division",1);														//입출고구분
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}


		private void CommodityHistoryRegister(SqlConnection con,SqlTransaction trans, int aa)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date],UpDatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품', @CompanyName, @BusinessRegistrationNum,@Quantity,@UnitCost,'등록',@Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			//comm.Parameters.Add("@ProcessSequenceNum",99);											//공정순서
			//comm.Parameters.Add("@ProcessCode","14009999");											//공정코드
			//comm.Parameters.Add("@ProcessName","최종품");												//공정명
			comm.Parameters.Add("@CompanyName", alist[3].ToString());								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",alist[4].ToString());
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입고 수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_List[4].ToString()));					//입고단가
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 무검사 상품 구매입고시 등록하는 이력
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="aa"></param>
		private void BuyingDeliveryCommodityHistory(SqlConnection con,SqlTransaction trans, int aa)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품','생산창고','1',@Quantity,@Date,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;

			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			//comm.Parameters.Add("@ProcessSequenceNum",99);											//공정순서
			//comm.Parameters.Add("@ProcessCode","14009999");											//공정코드
			//comm.Parameters.Add("@ProcessName","최종품");												//공정명
			//comm.Parameters.Add("@Division",1);														//입출고구분
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 구매납품목이 검사인 경우의 품질검사 등록 함수
		/// </summary>
		/// <param name="aa">구매납품원장인덱스번호</param>
		private void BuyingDeliveryQualityInspectionRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str=@"Insert into QI_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,BeginProcessSequenceNum,EndProcessSequenceNum,BeginProcessName,EndProcessName,RequestQuantity,NowStockQuantity,StandardUnitCost, ApplyUnitCost,LotNum,ProgressCondition,
				RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex1,HistorySection1,HistoryIndex2,HistorySection2,SuitabilityQuantity,UnSuitabilityQuantity,UnSuitabilityCost,[Year], [Month]) values(
				@itemnum,@itemdrawnum,@itemname,@com,@comnum,@Begin,@End,@BeginName,@EndName,@quantity,@NowStockQuantity,@standardcost, @cost,@LotNum,'대기', @Person, @PersonID, @Date,@idx1,@section1,@idx2,@section2,0,0,0,@year, @mon)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
		
			//시작공정 번호 , 종료공정번호
			if(Division(alist[0].ToString())==1)
			{
				comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = 0;
				comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = 0;
			}
			else 
			{
				comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = 99;
				comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = 99;
			}

			//시작공정 코드 , 종료공정코드
			if(Division(alist[0].ToString())==1)
			{
				comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = "14000000";
				comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = "14000000";
			}
			else 
			{
				comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = "14009999";
				comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = "14009999";
			}

			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());
			comm.Parameters.Add("@NowStockQuantity",NowStockQuantity(alist[0].ToString(),conn,tr));
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@standardcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx1",SqlDbType.Int).Value = aa.ToString();
			comm.Parameters.Add("@section1",SqlDbType.VarChar).Value = "구매납품";
			if(m_List[0].ToString() == "0")
			{
				comm.Parameters.Add("@idx2",SqlDbType.Int).Value = alist[6].ToString();
				comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "구매발주";
			}
			else
			{
				comm.Parameters.Add("@idx2",SqlDbType.Int).Value = alist[7].ToString();
				comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "구매납품의뢰";
			}
			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[7].ToString()));
			comm.ExecuteNonQuery();

		}

		/// <summary>
		/// 검사품들에 대해서 입고시마다 입고대기수량을 넣어둔다.
		/// 검사가 끝나기전까지는 계속 누적한다. 검사가 완료되면 입고대기수량은 0로 만든다(품질검사쪽에서 처리)
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="aa"></param>
		private void BO_HTUpdate(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str = "UPDATE BO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @Quantity WHERE BuyingOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 구매납품목이 무검사인 경우의 품질검사 등록 함수
		/// </summary>
		/// <param name="aa">구매납품원장인덱스번호</param>
		private int NonBuyingDeliveryQualityInspectionRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str=@"Insert into QI_HT(ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,QualityInspectionCompleteDate,BeginProcessSequenceNum, EndProcessSequenceNum,BeginProcessName,EndProcessName,
				RequestQuantity,SuitabilityQuantity,UnSuitabilityQuantity,NowStockQuantity, StandardUnitCost, ApplyUnitCost,UnSuitabilityCost,InspectionDecisionCode,InspectionDecisionMeaning,UnSuitabilityCauseCode,UnSuitabilityCauseMeaning,UnSuitabilityStatusCode,
				UnSuitabilityStatusMeaning,UnSuitabilityDetailMeaning,Investigator,InvestigatorID,LotNum,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,
				HistoryIndex1,HistorySection1,HistoryIndex2,HistorySection2, [Year], [Month]) Values(@itemnum,@itemdrawnum,@itemname,@com,@comnum,@Date, @Begin,@End,@BeginName,@EndName,
				@quantity,@quantity,0,@NowStockQuantity,@standardcost,@cost,0,'','','','','','','',@Person, @PersonID,@LotNum,'검사완료', @Person, @PersonID,@RegistrationDate,@idx1,@section1,@idx2,@section2,@year,@month)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
		
			//시작공정 번호 , 종료공정번호
			if(Division(alist[0].ToString())==1)
			{
				comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = 0;
				comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = 0;
			}
			else 
			{
				comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = 99;
				comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = 99;
			}

			//시작공정 코드 , 종료공정코드
			if(Division(alist[0].ToString())==1)
			{
				comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = "14000000";
				comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = "14000000";
			}
			else 
			{
				comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = "14009999";
				comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = "14009999";
			}

			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());
			
			comm.Parameters.Add("@suitability", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());
			comm.Parameters.Add("@NowStockQuantity",NowStockQuantity(alist[0].ToString(),conn,tr));
			comm.Parameters.Add("@standardcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();
			
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();			
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[3].ToString()).ToShortDateString();
			comm.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx1",SqlDbType.Int).Value = aa.ToString();
			comm.Parameters.Add("@section1",SqlDbType.VarChar).Value = "구매납품";
			if(m_List[0].ToString() == "0")
			{
				comm.Parameters.Add("@idx2",SqlDbType.Int).Value = alist[6].ToString();
				comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "구매발주";
			}
			else
			{
				comm.Parameters.Add("@idx2",SqlDbType.Int).Value = alist[7].ToString();
				comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "구매납품의뢰";
			}

			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@month",int.Parse(m_List[7].ToString()));

			comm.ExecuteNonQuery();

			str = "Select Max(QualityInspectionHistoryIndex) From QI_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			int a = Convert.ToInt32(comm1.ExecuteScalar());
			
			return a ;

		}

		/// <summary>
		///  부자재구매납품원장 등록
		/// </summary>
		/// <param name="rowcount"></param>
		private int SubBuyingDeliveryRegister(SqlConnection conn,SqlTransaction tr)
		{
			int a = 0;
			string str="Insert into SBD_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,DeliveryQuantity,DeliveryDate,StandardUnitCost,ApplyUnitCost,TotalCost,[Year] ,[Month],"+
				"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,SubBuyingOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@total,@DeliveryDate,@standardcost, @cost,@totalcost,@year, @mon,'대기', @Person, @PersonID, @Date,@idx)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
			
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString());

			comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse((m_List[2].ToString()));
			
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[3].ToString());
			comm.Parameters.Add("@standardcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[6].ToString());
					
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString()) * decimal.Parse(m_List[3].ToString());
			comm.Parameters.Add("@year",int.Parse(m_List[4].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[5].ToString()));
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			comm.ExecuteNonQuery();

			str = "Select Max(SubBuyingDeliveryHistoryIndex) From SBD_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			a = Convert.ToInt32(comm1.ExecuteScalar());
			
			return a ;
		}

		/// <summary>
		/// 부자재구매입고 후 매입매출테이블에 금액 누적
		/// </summary>
		private void SubSaleTable(SqlConnection conn, SqlTransaction tr)
		{
			decimal totalcost= decimal.Parse(m_List[3].ToString()) * decimal.Parse(m_List[1].ToString());
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			int year = int.Parse(m_List[4].ToString());
			int mon = int.Parse(m_List[5].ToString());

			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = alist[4].ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = totalcost;
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);

			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 부자재구매입고후 부자재발주원장 잔량을 변경하는 함수
		/// 잔량이 0 이면 진행상태 완료
		/// </summary>
		/// <param name="rowcount"></param>
		private void SubBuyingOrderUpdate(SqlConnection conn, SqlTransaction tr)
		{

			Decimal Remain = 0;
			Decimal quantity = 0;
			Decimal list;

			// 구매발주원장에서 총발주량을 구한다
			string str_BO = "Select DeliveryQuantity, DeliveryRemainQuantity From SBO_HT where SubBuyingOrderHistoryIndex = @index";
			SqlCommand comm_BO = new SqlCommand(str_BO,conn);
			comm_BO.Transaction = tr;
			comm_BO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			SqlDataReader dr_BO = comm_BO.ExecuteReader();
			while(dr_BO.Read())
			{
				quantity = decimal.Parse(dr_BO["DeliveryQuantity"].ToString());
				Remain = decimal.Parse(dr_BO["DeliveryRemainQuantity"].ToString());
			}
			dr_BO.Close();


			if(m_List[1].ToString() ==null || m_List[1].ToString() =="")
				list = 0;
			else
				list = Decimal.Parse(m_List[1].ToString().Trim());

			Remain = Remain - list;
			Math.Round(Remain,2);

			string str = "";
			
			if(Remain <=0)
				str = "UPDATE SBO_HT SET ProgressCondition = '완료', DeliveryRemainQuantity = @remain WHERE SubBuyingOrderHistoryIndex = @idx";
			else
				str = "UPDATE SBO_HT SET ProgressCondition = '진행', DeliveryRemainQuantity = @remain WHERE SubBuyingOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 부자재 입고시 이력원장 등록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="aa"></param>
		private void SubBuyingDeliveryHistoryRegister(SqlConnection con,SqlTransaction trans, int aa)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName, ProcessCode, ProcessName, CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'', '', @CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'등록', @Date,@Person, @PersonID,'부자재구매입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			comm.Parameters.Add("@CompanyName", alist[3].ToString());								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",alist[4].ToString());
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[1].ToString()));					//입출고 수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_List[3].ToString()));
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[2].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 구매,외주납품원장 등록후 매입매출테이블에 금액 누적
		/// </summary>
		private void SaleTable(SqlConnection conn, SqlTransaction tr)
		{
			decimal totalcost= decimal.Parse(m_List[4].ToString()) * decimal.Parse(m_List[2].ToString());
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			//int year = DateTime.Parse(m_List[3].ToString()).Year;
			//int mon = DateTime.Parse(m_List[3].ToString()).Month;

			int year = int.Parse(m_List[6].ToString());
			int mon = int.Parse(m_List[7].ToString());

			if(m_PageName == "BuyingDelivery")
			{
				comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = alist[4].ToString();
			}
			else
			{
				comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = alist[10].ToString();
			}
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = totalcost;
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);

			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 무검사 구매납품시 매입원장 등록함수
		/// </summary>
		/// <param name="aa"></param>
		private void BuyingHistoryRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str="Insert into B_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,BuyingQuantity,ApplyUnitCost,TotalCost,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum,@itemdrawnum,@itemname,@com,@comnum,@quantity,@cost,@totalcost,@Person, @PersonID, @Date,@idx,@section)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[4].ToString();
			
			if(m_List[2] == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

			if(m_List[4] == null || m_List[4].ToString() == "")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
			
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString()) * decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = aa;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "구매납품";

			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 외주납품원장 등록함수
		/// </summary>
		private int OutSideDeliveryRegister(SqlConnection conn, SqlTransaction tr)
		{
			int a =0;
			
			string str = "";

			if(m_List[0].ToString() == "0")
			{
				str="Insert into OSD_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,DeliveryQuantity,DeliveryDate,StandardUnitCost,ApplyUnitCost,TotalCost,LotNum,"+
					"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderHistoryIndex,[Year],[Month]) values("+
					"@itemnum, @itemdrawnum,@itemname,@sequencenum,@processcode,@process,@com,@comnum,@total,@DeliveryDate,@standardcost, @cost,@totalcost,@LotNum,'대기', @Person, @PersonID, @Date,@idx,@year,@mon)";
			}
			else
			{
				str="Insert into OSD_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,DeliveryQuantity,DeliveryDate,StandardUnitCost,ApplyUnitCost,TotalCost,LotNum,"+
					"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderHistoryIndex,OutSideDeliveryRequestHistoryIndex,[Year],[Month]) values("+
					"@itemnum, @itemdrawnum,@itemname,@sequencenum,@processcode,@process,@com,@comnum,@total, @DeliveryDate,@standardcost,@cost,@totalcost,@LotNum,'대기', @Person, @PersonID, @Date,@idx,@idx1,@year,@mon)";
			}
			


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = alist[7].ToString();
			comm.Parameters.Add("@process",SqlDbType.VarChar).Value = alist[8].ToString();
			comm.Parameters.Add("@sequencenum",SqlDbType.TinyInt).Value = int.Parse(alist[6].ToString());
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
			
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());

			comm.Parameters.Add("@DeliveryDate", SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[3].ToString()); 

			if(alist[0] == null || alist[0].ToString() == "")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
				
			comm.Parameters.Add("@standardcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());//표준단가

			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString()) * decimal.Parse(m_List[4].ToString());

			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();

			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			if(m_List[0].ToString() == "1")
				comm.Parameters.Add("@idx1",SqlDbType.Int).Value = int.Parse(alist[13].ToString());

			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[7].ToString()));
			comm.ExecuteNonQuery();	
			
			str = "Select Max(OutSideDeliveryHistoryIndex) From OSD_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			a = Convert.ToInt32(comm1.ExecuteScalar());
			return a;
		}



		/// <summary>
		/// 외주납품등록시 외주납품의뢰 진행상태 변경
		/// </summary>
		private void OutSideDeliveryRequestUpdate(SqlConnection conn, SqlTransaction tr)
		{			
			string str = "UPDATE ODR_HT SET ProgressCondition = '완료' WHERE OutSideDeliveryRequestHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[13].ToString());
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 외주납품 등록시 바로 영업창고 입고
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="aa"></param>
		private void OutSideDeliveryInstoreRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			
			Decimal m_cost = 0;
			string str = "";
			
			
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();

			int year = DateTime.Parse(m_List[3].ToString()).Year;
			int mon = DateTime.Parse(m_List[3].ToString()).Month;


			Table table = new Table(year,mon);
			str = table.InBusinessTable();
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@storenum",SqlDbType.VarChar).Value = 1;

			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString())* Decimal.Parse(m_cost.ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 외주납품 등록시 입고의뢰원장 등록함수
		/// </summary>
		/// <param name="aa"></param>
		private void OutSideDeliveryInStoreRequestRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{

			string str="Insert into ISR_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,InstorehouseRequestQuantity, InStoreDate, "+
				"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum,@itemdrawnum,@itemname,@sequencenum,@ProcessCode, @ProcessName, @com,@comnum,@quantity, @inStoreDate,'대기', @Person, @PersonID, @Date,@idx,@section)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@sequencenum",SqlDbType.TinyInt).Value = int.Parse(alist[6].ToString());
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = alist[7].ToString();
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = alist[8].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();

			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());
			comm.Parameters.Add("@inStoreDate",DateTime.Parse(m_List[3].ToString()).ToShortDateString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = aa;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "품질검사";
			
		
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 검사품들에 대해서 입고시마다 입고대기수량을 넣어둔다.
		/// 검사가 끝나기전까지는 계속 누적한다. 검사가 완료되면 입고대기수량은 0로 만든다(품질검사쪽에서 처리)
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="aa"></param>
		private void OO_HTUpdate(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str = "UPDATE OO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @Quantity WHERE OutSideOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 외주납품 등록후 무검사인경우 생산창고에 입고수량 증가 함수
		/// </summary>
		private void OutSideDeliveryInStoreProductionTable(SqlConnection conn, SqlTransaction tr)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 0;
			string str = "";
			
			
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Transaction = tr;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = alist[7].ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();			
			if(dr_rate.Read())
				m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			dr_rate.Close();

			int year = DateTime.Parse(m_List[3].ToString()).Year;
			int mon = DateTime.Parse(m_List[3].ToString()).Month;


			Table table = new Table(year,mon);
			str = table.InProductionTable();
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();

			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = alist[7].ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
		}


		

		/// <summary>
		/// 외주납품원장 등록후 외주창고 출고수량 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void OutSideDelivertOutStoreTable(SqlConnection conn, SqlTransaction tr , int index)
		{
			//해당 품목이 가장 낮은 공정이면 하위품목들의 최상위공정을 떨어주고
			//그렇지 않으면 해당공정의 바로 아래공정품을 외주창고에서 떨어낸다
			//이때 하위품목의 자산분류가 원자재이면 해당 품목을 떨어준다

			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			string str = @"Select isnull(Min(ProcessSequenceNum),0) From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@num",alist[0].ToString());
			comm.Parameters.Add("@sequence",int.Parse(alist[3].ToString()));
			comm.CommandText = str;
			
			int year = DateTime.Parse(m_List[3].ToString()).Year;
			int mon = DateTime.Parse(m_List[3].ToString()).Month;

			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
			{
				comm.Parameters.Clear();
				//공정순서에 가서 해당자품목을 떨어준다
				str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @num";
				comm.Parameters.Add("@num",alist[0].ToString());
				comm.CommandText = str;
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(comm);
				da.Fill(ds);
				comm.Parameters.Clear();
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					//하위품목이 원자재이면 바로 떨어준다
					if(Division(dr["ChildItemNum"].ToString()) == 1)
					{
						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,conn);
						comm1.Transaction = tr;
						comm1.Parameters.Add("@parent",alist[0].ToString());
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(alist[0].ToString() + " 품목의 품목구성이 없거나 올바르지 않습니다!");
						Table table = new Table(year,mon);
						str = table.OutTable();
						SqlCommand comm2 = new SqlCommand(str,conn);
						comm2.Transaction = tr;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
						if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
							comm2.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = a * Decimal.Parse(m_List[2].ToString());

						comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = a * Decimal.Parse(m_List[2].ToString())* Decimal.Parse(m_cost.ToString()) ;
						comm2.Parameters.Add("@year",year);

						comm2.ExecuteNonQuery();

						comm2.Parameters.Clear();

						//외주입고시 외주창고에서 출고되는 수량
						string	str1=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','외주창고','0',@Quantity,@Date,'외주납품',@HistoryIndex)";
			
						comm2.CommandText = str1;
						comm2.CommandType = CommandType.Text;
						comm2.Parameters.Add("@itemnum",dr["ChildItemNum"].ToString());
						comm2.Parameters.Add("@itemdrawnum",Draw(dr["ChildItemNum"].ToString()));
						comm2.Parameters.Add("@itemname",Name(dr["ChildItemNum"].ToString()));
						//comm2.Parameters.Add("@ProcessSequenceNum",0);
						//comm2.Parameters.Add("@ProcessCode", "14000000");
						//comm2.Parameters.Add("@ProcessName","소재");
						//comm2.Parameters.Add("@Division",0);												//입출고구분(출고)
						comm2.Parameters.Add("@Quantity", a * Decimal.Parse(m_List[2].ToString()));
						comm2.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()));
						//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
						comm2.Parameters.Add("@HistoryIndex",index);				//원장번호
						comm2.ExecuteNonQuery();	
						comm2.Parameters.Clear();
						
						

						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
							comm.Parameters.Add("@year" ,DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

					}
					else
					{
						//공정품이면 자품목의 최대공정을 떨어준다.
						string code = "";
						int seq = 0;
						str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num)";
						comm.Parameters.Add("@num",dr["ChildItemNum"].ToString());
						comm.CommandText = str;
						
						SqlDataReader dr_Code = comm.ExecuteReader();
						while(dr_Code.Read())
						{
							code = dr_Code["ProcessCode"].ToString();
							seq = int.Parse(dr_Code["ProcessSequenceNum"].ToString());
						}
						dr_Code.Close();
						comm.Parameters.Clear();

						if(seq == 0)
							throw new Exception("하위공정이 존재하지 않습니다!");

						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						// 품목의 진척율
						string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,conn);
						comm_rate.Transaction = tr;
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						SqlDataReader dr_rate = comm_rate.ExecuteReader();			
						if(dr_rate.Read())
							m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
						dr_rate.Close();
						comm_rate.Parameters.Clear();


						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,conn);
						comm1.Transaction = tr;
						comm1.Parameters.Add("@parent",alist[0].ToString());
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();


						Table table = new Table(year,mon);
						str = table.OutTable();
						SqlCommand comm2 = new SqlCommand(str,conn);
						comm2.Transaction = tr;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
						if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
							comm2.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = a * Decimal.Parse(m_List[2].ToString());

						comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
						comm2.Parameters.Add("@year",year);

						comm2.ExecuteNonQuery();
						comm2.Parameters.Clear();


						//외주입고시 외주창고에서 출고되는 수량
						string	str1=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'외주납품',@HistoryIndex)";
			
						comm2.CommandText = str1;
						comm2.CommandType = CommandType.Text;
						comm2.Parameters.Add("@itemnum",dr["ChildItemNum"].ToString());
						comm2.Parameters.Add("@itemdrawnum",Draw(dr["ChildItemNum"].ToString()));
						comm2.Parameters.Add("@itemname",Name(dr["ChildItemNum"].ToString()));
						comm2.Parameters.Add("@ProcessSequenceNum",seq);
						comm2.Parameters.Add("@ProcessCode", code);
						comm2.Parameters.Add("@ProcessName",Process(dr["ChildItemNum"].ToString(),seq));
						//comm2.Parameters.Add("@Division",0);												//입출고구분(출고)
						comm2.Parameters.Add("@Quantity", a*Decimal.Parse(m_List[2].ToString()));
						comm2.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()));
						//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
						comm2.Parameters.Add("@HistoryIndex",index);				//원장번호
						comm2.ExecuteNonQuery();	
						comm2.Parameters.Clear();


						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
							comm.Parameters.Add("@year" ,DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();
						
							string StockQuantity = "False";
							string ItemName = alist[0].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

					}
				}

			}
			else
			{
				comm.Parameters.Clear();
				//해당품목의 바로 아래 공정을 떨어준다
				string code = "";
				int seq = 0;
				str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence)";
				comm.Parameters.Add("@num",alist[0].ToString());
				comm.Parameters.Add("@sequence",int.Parse(alist[3].ToString()));
				comm.CommandText = str;
				
				SqlDataReader dr_Code = comm.ExecuteReader();
				while(dr_Code.Read())
				{
					code = dr_Code["ProcessCode"].ToString();
					seq = int.Parse(dr_Code["ProcessSequenceNum"].ToString());
				}
				dr_Code.Close();
				comm.Parameters.Clear();
				// 품목의 금액
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,conn);
				comm_cost.Transaction = tr;
				comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
				SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
				if(dr_cost.Read())
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
				dr_cost.Close();

				// 품목의 진척율
				string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				SqlCommand comm_rate = new SqlCommand(str_rate,conn);
				comm_rate.Transaction = tr;
				comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
				comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				SqlDataReader dr_rate = comm_rate.ExecuteReader();			
				if(dr_rate.Read())
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
				dr_rate.Close();
				comm_rate.Parameters.Clear();

				//외주창고에 떨어줄 품목
				Table table = new Table(year,mon);
				str = table.OutTable();
				SqlCommand comm2 = new SqlCommand(str,conn);
				comm2.Transaction = tr;
				comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
				comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
				if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
					comm2.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
				else
					comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

				comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
				comm2.Parameters.Add("@year",year);

				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();

				//외주입고시 외주창고에서 출고되는 수량
				string	str1=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'외주납품',@HistoryIndex)";
			
				comm2.CommandText = str1;
				comm2.CommandType = CommandType.Text;
				comm2.Parameters.Add("@itemnum",alist[0].ToString());
				comm2.Parameters.Add("@itemdrawnum",alist[1].ToString());
				comm2.Parameters.Add("@itemname",alist[2].ToString());
				comm2.Parameters.Add("@ProcessSequenceNum",seq);
				comm2.Parameters.Add("@ProcessCode", code);
				comm2.Parameters.Add("@ProcessName",Process(alist[0].ToString(),seq));
				//comm2.Parameters.Add("@Division",0);												//입출고구분(출고)
				comm2.Parameters.Add("@Quantity", Decimal.Parse(m_List[2].ToString()));
				comm2.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()));
				//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
				comm2.Parameters.Add("@HistoryIndex",index);				//원장번호
				comm2.ExecuteNonQuery();	
				comm2.Parameters.Clear();


				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  alist[0].ToString();
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
					comm.Parameters.Add("@year" ,DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = alist[0].ToString();
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
					}
				}
			}
		}


		private string Draw(string Item)
		{
			string ItemDrawNum = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ItemDrawNum From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemDrawNum = dr["ItemDrawNum"].ToString();
			}
			conn.Close();
			
			return ItemDrawNum;
			
		}

		private string Name(string Item)
		{
			string ItemName = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ItemName From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemName = dr["ItemName"].ToString();
			}
			conn.Close();
			
			return ItemName;
		}

		private string Process(string Item, int Sequence)
		{
			string ProcessName = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select SmallClassificationName as ProcessName From PSI_MT Inner join PUC_MT on ProcessCode = SmallClassificationCode where ItemNum = @ItemNum and ProcessSequenceNum = @num and PSI_MT.RecodingState = 1 and PUC_MT.RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@num",Sequence);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ProcessName = dr["ProcessName"].ToString();
			}
			conn.Close();
			
			return ProcessName;
		}


		/// <summary>
		/// 외주납품원장 등록후 매입원장 등록 함수
		/// </summary>
		/// <param name="aa"></param>
		private void OutSideDeliveryBuyingHistoryRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
			string str="Insert into B_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,BuyingQuantity,ApplyUnitCost,TotalCost,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum,@itemdrawnum,@itemname,@com,@comnum,@quantity,@cost,@totalcost,@Person, @PersonID, @Date,@idx,@section)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();
			
			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = Decimal.Parse(m_List[2].ToString());

			if(m_List[4] == null || m_List[4].ToString() == "")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[4].ToString());

			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString()) * decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = aa;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "외주납품";
			
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 외주납품 등록후 품질검사원장 등록
		/// </summary>
		/// <param name="aa"></param>
		private void OutSideDeliveryQualityInspectionRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
		
			string str="Insert into QI_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,BeginProcessSequenceNum,EndProcessSequenceNum,BeginProcessName,EndProcessName,RequestQuantity,StandardUnitCost,ApplyUnitCost,LotNum,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex1,HistorySection1,HistoryIndex2,HistorySection2,SuitabilityQuantity,UnSuitabilityQuantity,UnSuitabilityCost,[Year],[Month]) values("+
				"@itemnum,@itemdrawnum,@itemname,@com,@comnum,@Begin,@End,@BeginName,@EndName,@quantity,@standardcost,@cost,@LotNum,'대기', @Person, @PersonID, @Date,@idx1,@section1,@idx2,@section2,0,0,0,@year,@mon)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = int.Parse(alist[3].ToString());//시작공정순서
			comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = int.Parse(alist[6].ToString());	//종료공정순서
			comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = alist[4].ToString();	//시작공정코드
			comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = alist[7].ToString();		//종료공정코드
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();


			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());

			//comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString())*decimal.Parse(alist[11].ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@standardcost", SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx1",SqlDbType.Int).Value = int.Parse(aa.ToString());
			comm.Parameters.Add("@section1",SqlDbType.VarChar).Value = "외주납품";
			comm.Parameters.Add("@idx2",SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "외주발주";

			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[7].ToString()));
			
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 무검사 외주납품 등록후 품질검사원장 등록
		/// </summary>
		/// <param name="aa"></param>
		private int NonOutSideDeliveryQualityInspectionRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
		
			string str=@"Insert into QI_HT(ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,QualityInspectionCompleteDate,BeginProcessSequenceNum,EndProcessSequenceNum,BeginProcessName,EndProcessName,
				RequestQuantity,SuitabilityQuantity,UnSuitabilityQuantity,StandardUnitCost,ApplyUnitCost,UnSuitabilityCost,InspectionDecisionCode,InspectionDecisionMeaning,UnSuitabilityCauseCode,UnSuitabilityCauseMeaning,UnSuitabilityStatusCode,
				UnSuitabilityStatusMeaning,UnSuitabilityDetailMeaning,Investigator,InvestigatorID,LotNum,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,
				HistoryIndex1,HistorySection1,HistoryIndex2,HistorySection2,[Year],[Month]) Values(@itemnum,@itemdrawnum,@itemname,@com,@comnum,@Date,@Begin,@End,@BeginName,@EndName,
				@quantity,@quantity,0,@standardcost,@cost,0,'','','','','','','',@Person, @PersonID,@LotNum,'검사완료', @Person, @PersonID,@RegistrationDate,@idx1,@section1,@idx2,@section2,@year,@mon)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = alist[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = alist[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = alist[2].ToString();
			comm.Parameters.Add("@Begin",SqlDbType.TinyInt).Value = int.Parse(alist[3].ToString());//시작공정순서
			comm.Parameters.Add("@End",SqlDbType.TinyInt).Value = int.Parse(alist[6].ToString());	//종료공정순서
			comm.Parameters.Add("@BeginName",SqlDbType.VarChar).Value = alist[4].ToString();	//시작공정코드
			comm.Parameters.Add("@EndName",SqlDbType.VarChar).Value = alist[7].ToString();		//종료공정코드
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = alist[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = alist[10].ToString();


			if(m_List[2].ToString() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());

			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[4].ToString());
			comm.Parameters.Add("@standardcost", SqlDbType.Decimal).Value = decimal.Parse(m_List[8].ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[3].ToString()).ToShortDateString();
			comm.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx1",SqlDbType.Int).Value = int.Parse(aa.ToString());
			comm.Parameters.Add("@section1",SqlDbType.VarChar).Value = "외주납품";
			comm.Parameters.Add("@idx2",SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			comm.Parameters.Add("@section2",SqlDbType.VarChar).Value = "외주발주";

			comm.Parameters.Add("@year",int.Parse(m_List[6].ToString()));
			comm.Parameters.Add("@mon",int.Parse(m_List[7].ToString()));
			
			comm.ExecuteNonQuery();

			str = "Select Max(QualityInspectionHistoryIndex) From QI_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			int a = Convert.ToInt32(comm1.ExecuteScalar());
			
			return a ;
		}


		/// <summary>
		/// 외주입고 등록시 기록하는 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="aa"></param>
		private void OutSideDeliveryQualityHistory(SqlConnection conn, SqlTransaction tr, int aa)
		{
		
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'외주납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(alist[6].ToString()));				//공정순서
			comm.Parameters.Add("@ProcessCode",alist[7].ToString());								//공정코드
			comm.Parameters.Add("@ProcessName",alist[8].ToString());								//공정명
			//comm.Parameters.Add("@Division",1);													//입출고구분
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			//comm.Parameters.Add("@HistoryDivision","외주납품");									//원장구분
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();
		}

		private void OutSideDeliveryHistoryRegister(SqlConnection conn, SqlTransaction tr, int aa)
		{
		
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date],UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,'등록',@Date,@Person, @PersonID,'외주납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",alist[0].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", alist[1].ToString());								//도면번호
			comm.Parameters.Add("@itemname",alist[2].ToString());									//품목명
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(alist[6].ToString()));				//공정순서
			comm.Parameters.Add("@ProcessCode",alist[7].ToString());								//공정코드
			comm.Parameters.Add("@ProcessName",alist[8].ToString());								//공정명
			comm.Parameters.Add("@CompanyName",alist[9].ToString());								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",alist[10].ToString());
			
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[2].ToString()));					//입고 수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_List[4].ToString()));					//입고 금액
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[3].ToString()).ToShortDateString());	//입출고일
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",aa);												//원장번호

			comm.ExecuteNonQuery();


			
		}


		/// <summary>
		/// 외주츨고 등록함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideStorehouseRegister(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Insert into OOS_HT (ItemNum,ItemDrawNum,ItemName,OrderRate,ProgressRate,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,"+
				"ThistimeOutStorehouseQuantity,OutStorehouseDate,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderHistoryIndex,ThisOrderQuantity, ThisDeliveryDate) values("+
				"@itemnum, @itemdrawnum,@itemname,@rate,@progress,@sequencenum,@processcode,@process,@com,@comnum,@quantity,@outstorehousedate,'대기', @Person, @PersonID, @Date,@idx,@ThisOrderQuantity, @ThisDeliveryDate)";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			
			if(m_grid.Rows[rowcount].Cells.FromKey("OrderRate").Value == null ||m_grid.Rows[rowcount].Cells.FromKey("OrderRate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("OrderRate").Text);

			if(m_grid.Rows[rowcount].Cells.FromKey("ProgressRate").Value == null ||m_grid.Rows[rowcount].Cells.FromKey("ProgressRate").Value.ToString().Trim() == "")
				comm.Parameters.Add("@progress",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@progress",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProgressRate").Text);

			comm.Parameters.Add("@sequencenum",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);
			comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@process",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ProcessName").Value.ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("CompanyName").Value.ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			
			if(m_grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			
			//comm.Parameters.Add("@outstorehousedate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text);
			//comm.Parameters.Add("@outstorehousedate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text.Substring(0,10).ToString());
			comm.Parameters.Add("@outstorehousedate",DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).ToShortDateString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
            comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Text);
			comm.Parameters.Add("@ThisOrderQuantity",decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ThisOrderQuantity").Text));
			comm.Parameters.Add("@ThisDeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("ThisDeliveryDate").Text).ToShortDateString();
			comm.ExecuteNonQuery();
			
			int a = Division(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			if(a == 1)//원자재
			{
				if(!WorkPlan3())//작업계획전략이 3이 아닌경우
				{
					OutSideStorehouseRowTable(conn,tr, rowcount);					// 원자재 출고수량증가
					
				}
				else//관리여부 예인경우
				{
					if(IsManaged(conn,tr, m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString()))
					{
						OutSideStorehouseRowTable(conn,tr, rowcount);				// 원자재 출고수량증가
						
					}
				}
				OutSideStorehouseHistory(conn,tr,rowcount);						// 외주출고 이력원장 등록
			}
			else//반제품
			{
				if(!WorkPlan3())//작업계획전략이 3이 아닌경우
				{
					OutSideStorehouseProductionTable(conn,tr,rowcount);				// 생산창고 출고수량증가
					OutSideStorehouseProductionHistory(conn,tr,rowcount);			// 외주출고 공정품 이력원장 등록
				}
				else//관리여부 예인경우
				{
					if(IsManaged(conn,tr, m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString()))
					{
						OutSideStorehouseProductionTable(conn,tr,rowcount);				// 생산창고 출고수량증가
						
					}
				}
				OutSideStorehouseProductionHistory(conn,tr,rowcount);			// 외주출고 공정품 이력원장 등록
			}
			
			if(Exist(conn, tr, rowcount))
				OutSideStorehouseOutSideTable(conn, tr, rowcount);						// 외주창고 입고수량증가
			else
				throw new Exception("품목 "+ m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString() + "이 "+m_grid.Rows[rowcount].Cells.FromKey("CompanyName").Value.ToString()+" 외주창고에 존재하지 않습니다!");
		}

		bool Exist(SqlConnection con, SqlTransaction trans, int count)
		{
			string str = "Select Count(*) From OS_MT where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode and BusinessRegistrationNum = @BusinessRegistrationNum and [Year] = @year";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Parameters.Add("@ItemNum",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@ProcessCode",m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString());
			comm.Parameters.Add("@BusinessRegistrationNum",m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString());
			comm.Parameters.Add("@year",DateTime.Now.Year);

			int a = int.Parse(comm.ExecuteScalar().ToString());

			if(a == 0)
				return false;
			else
				return true;

		}

		/// <summary>
		/// 공정품 외주출고 이력원장 등록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void OutSideStorehouseProductionHistory(SqlConnection con, SqlTransaction trans, int count)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','0',@Quantity,@Date,'외주출고',@HistoryIndex)";
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text);
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessName").Value.ToString();
			//comm.Parameters.Add("@Division",0);												//입출고구분(출고)
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).ToShortDateString().Substring(0,10).ToString());
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();


			
			//외주창고에 수량이 입고되는 이력원장 기록
			
			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','1',@Quantity,@Date,'외주출고',@HistoryIndex)";
			comm.CommandText = str;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text);
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessName").Value.ToString();
			//comm.Parameters.Add("@Division",1);												//입출고구분(출고)
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).ToShortDateString().Substring(0,10).ToString());
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호			
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 외주출고 등록시 이력원장 등록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void OutSideStorehouseHistory(SqlConnection con, SqlTransaction trans, int count)
		{
			string	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'외주출고',@HistoryIndex)";
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			//comm.Parameters.Add("@ProcessSequenceNum",0);
			//comm.Parameters.Add("@ProcessCode", "14000000");
			//comm.Parameters.Add("@ProcessName","소재");
			//comm.Parameters.Add("@Division",0);												//입출고구분(입고)
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).ToShortDateString().Substring(0,10).ToString());
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();


			//외주창고에 입고되는 이력기록
			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','외주창고','1',@Quantity,@Date,'외주출고',@HistoryIndex)";
			comm.CommandText = str;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			//comm.Parameters.Add("@ProcessSequenceNum",0);
			//comm.Parameters.Add("@ProcessCode", "14000000");
			//comm.Parameters.Add("@ProcessName","소재");
			//comm.Parameters.Add("@Division",1);												//입출고구분(입고)
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).ToShortDateString().Substring(0,10).ToString());
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 선출고
		/// </summary>
		private void OutSideStorehouseRegister(SqlConnection conn, SqlTransaction tr)
		{
			string str="Insert into OOS_HT (ItemNum,ItemDrawNum,ItemName,OrderRate,ProgressRate,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName,BusinessRegistrationNum,"+
				"ThistimeOutStorehouseQuantity,OutStorehouseDate,LotNum, ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
				"@itemnum, @itemdrawnum,@itemname,@rate,@progress,@sequencenum,@processcode,@process,@com,@comnum,@quantity,@outstorehousedate,@LotNum,'대기', @Person, @PersonID, @Date)";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
			comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = 100;

			if(m_List[3] == null || m_List[3].ToString() == "")
				comm.Parameters.Add("@progress",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@progress",SqlDbType.Decimal).Value = decimal.Parse(m_List[3].ToString());

			comm.Parameters.Add("@sequencenum",SqlDbType.Int).Value = int.Parse(m_List[4].ToString());
			comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@process",SqlDbType.VarChar).Value = m_List[6].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_List[7].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[8].ToString();
			
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			
			comm.Parameters.Add("@outstorehousedate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[10].ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[11].ToString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
			int a = Division(m_List[0].ToString());
			if(a == 1)	//원자재
			{
				if(!WorkPlan3())//작업계획전략이 3이 아닌경우
				{
					OutSideStorehouseRowTable(conn,tr);							// 원자재 출고수량증가
					
				}
				else // 3이면서 관리여부가 예인경우
				{
					if(IsManaged(conn,tr,m_List[0].ToString()))
					{
						OutSideStorehouseRowTable(conn,tr);							// 원자재 출고수량증가
						
					}
				}
				BeforeOutSideStorehouseHistory(conn,tr);;					// 외주출고 이력원장 등록
			}
			else //반제품인경우
			{
				if(!WorkPlan3())//작업계획전략이 3이 아닌경우
				{
					OutSideStorehouseProductionTable(conn,tr);					// 생산창고 출고수량증가
					
				}
				else // 3이면서 관리여부가 예인경우
				{
					if(IsManaged(conn,tr,m_List[0].ToString()))
					{
						OutSideStorehouseRowTable(conn,tr);							// 원자재 출고수량증가						
					}
				}
				BeforeOutSideStorehouseProductionTable(conn,tr);			//외주출고원장 이력등록
			}


			OutSideStorehouseOutSideTable(conn,tr);						// 외주창고 입고수량증가
		}

		/// <summary>
		/// 작업계획전략3인지 판단함수
		/// </summary>
		/// <returns></returns>
		private bool WorkPlan3()
		{
			bool Work = false;
			// 현재 설정되어있는 각종 정보를 가져옴
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			string strSQL = @"SELECT WorkPlan3 FROM SCS_T";
			SqlCommand comm = new SqlCommand(strSQL, conn);

			SqlDataReader reader = comm.ExecuteReader();
			if(reader.Read())
			{
				Work = bool.Parse(reader["WorkPlan3"].ToString());
			}
			conn.Close();

			return Work;

		}


		/// <summary>
		/// 선출고시 이력원장 등록(공정품)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BeforeOutSideStorehouseProductionTable(SqlConnection con, SqlTransaction trans)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','0',@Quantity,@Date,'외주출고',@HistoryIndex)";
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum",m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_List[4].ToString()));
			comm.Parameters.Add("@ProcessCode", m_List[5].ToString());
			comm.Parameters.Add("@ProcessName",m_List[6].ToString());
			//comm.Parameters.Add("@Division",0);												//입출고구분(출고)
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[10].ToString()));
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();

			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','1',@Quantity,@Date,'외주출고',@HistoryIndex)";
			comm.CommandText = str;
			comm.Parameters.Add("@itemnum",m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_List[4].ToString()));
			comm.Parameters.Add("@ProcessCode", m_List[5].ToString());
			comm.Parameters.Add("@ProcessName",m_List[6].ToString());
			//comm.Parameters.Add("@Division",1);												//입출고구분(출고)
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[10].ToString()));
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 선출고시 이력원장 등록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BeforeOutSideStorehouseHistory(SqlConnection con, SqlTransaction trans)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'외주출고',@HistoryIndex)";
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum",m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			//comm.Parameters.Add("@ProcessSequenceNum",0);
			//comm.Parameters.Add("@ProcessCode", "14000000");
			//comm.Parameters.Add("@ProcessName","소재");
			//comm.Parameters.Add("@Division",0);												//입출고구분(출고)
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[10].ToString()));
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();


			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','외주창고','1',@Quantity,@Date,'외주출고',@HistoryIndex)";
				
			comm.CommandText = str;
			comm.Parameters.Add("@itemnum",m_List[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_List[1].ToString());
			comm.Parameters.Add("@itemname", m_List[2].ToString());
			//comm.Parameters.Add("@ProcessSequenceNum",0);
			//comm.Parameters.Add("@ProcessCode", "14000000");
			//comm.Parameters.Add("@ProcessName","소재");
			//comm.Parameters.Add("@Division",0);												//입출고구분(출고)
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[10].ToString()));
			//comm.Parameters.Add("@HistoryDivision","외주출고");								//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOOS_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 외주출고원장 인덱스 구하는 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <returns></returns>
		private int MaxOOS_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str  = "Select Max(OutSideOutStorehouseHistoryIndex) From OOS_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}



		


		/// <summary>
		/// 원자재창고에서 출고수량 증가 함수
		/// </summary>
		/// <param name="count"></param>
		private void OutSideStorehouseRowTable(SqlConnection con, SqlTransaction trans, int count)
		{
			Decimal m_cost = 0;
			string str = "";
			// 품목의 금액을 구해서 넘겨준다
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			
			if(dr.Read())
				m_cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();


			int year = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Month;

			Table table = new Table(year,mon);
			string str1 = table.OutRowTable();
			
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = m_cost * Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@year",year);
			comm.CommandText = str1;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
			
		}

		/// <summary>
		/// 선출고시 원자재창고에서 출고수량 증가 함수
		/// </summary>
		private void OutSideStorehouseRowTable(SqlConnection con,SqlTransaction trans)
		{
			Decimal m_cost = 0;
			string str = "";

			// 품목의 금액을 구해서 넘겨준다
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm1 = new SqlCommand(str,con);
			comm1.Transaction = trans;
			comm1.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr = comm1.ExecuteReader();
			
			if(dr.Read())
				m_cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();


			int year = DateTime.Parse(m_List[10].ToString()).Year;
			int mon = DateTime.Parse(m_List[10].ToString()).Month;

			Table table = new Table(year,mon);
			str = table.OutRowTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			
			if(m_List[9] == null || m_List[9].ToString() == "0")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = m_cost*Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_List[0].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_List[0].ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
			
		}

		/// <summary>
		/// 생산창고에서 출고수량 증가 함수
		/// </summary>
		/// <param name="count"></param>
		private void OutSideStorehouseProductionTable(SqlConnection con,SqlTransaction trans, int count)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			int year = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Month;

			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			if(dr_cost.Read())
			{
				if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
					m_progressrate = 100;
				else
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			}
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,con);
			comm_rate.Transaction = trans;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
		
			dr_rate.Close();
			

			string str = "";
			Table table = new Table(year,mon);
			str = table.OutProductionTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text) * Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@year",year);
			//comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Value.ToString());
			//comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(decimal.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}


		/// <summary>
		/// 선출고시 생산창고 출고수량 증가
		/// </summary>
		private void OutSideStorehouseProductionTable(SqlConnection con,SqlTransaction trans)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 100;
			
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			if(dr_cost.Read())
			{
				if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
					m_cost = 0;
				else
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			}
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,con);
			comm_rate.Transaction = trans;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[5].ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
		
			dr_rate.Close();
			

			
			int year = DateTime.Parse(m_List[10].ToString()).Year;
			int mon = DateTime.Parse(m_List[10].ToString()).Month;

			Table table = new Table(year,mon);
			string str = table.OutProductionTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value =  m_List[5].ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString()) * Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			//comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_List[4].ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();


			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_List[0].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value =  m_List[5].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName =  m_List[0].ToString();
				if(reader.Read())
				{
					if(decimal.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}

		/// <summary>
		/// 외주창고의 입고수량 증가
		/// </summary>
		/// <param name="count"></param>
		private void OutSideStorehouseOutSideTable(SqlConnection con, SqlTransaction trans, int count)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
			{
				if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
					m_cost = 0;
				else
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			}
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode=@code";
			SqlCommand comm_rate = new SqlCommand(str_rate,con);
			comm_rate.Transaction = trans;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();
			
			if(dr_rate.Read())
			{

				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 100;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
			dr_rate.Close();

			int year = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseDate").Text).Month;
			
			Table table = new Table(year,mon);
			string str = table.InTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			if(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value == null ||m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_grid.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text) * Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
		}

		
		/// <summary>
		/// 선출고시 외주창고 입고수량 증가
		/// </summary>
		private void OutSideStorehouseOutSideTable(SqlConnection con, SqlTransaction trans)
		{
			Decimal m_cost = 0;
			Decimal m_progressrate = 100;
			
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
			{
				if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			}
			dr_cost.Close();

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,con);
			comm_rate.Transaction = trans;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[5].ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();
			
			if(dr_rate.Read())
			{

				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 100;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
			dr_rate.Close();

			int year = DateTime.Parse(m_List[10].ToString()).Year;
			int mon = DateTime.Parse(m_List[10].ToString()).Month;
			
			Table table = new Table(year,mon);
			string str = table.InTable();
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			if(m_List[9] == null || m_List[9].ToString() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString());
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[9].ToString()) * Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[8].ToString();
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 구매납품의뢰원장에 등록하는 함수
		/// </summary>
		/// <param name="count"></param>
		private void BuyingDeliveryRequestRegister(SqlConnection conn, SqlTransaction tr)
		{
			string str="Insert into BDR_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,DeliveryRequestQuantity,ApplyUnitCost,TotalCost,"+
				"ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,BuyingOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@quantity,@cost,@totalcost,'대기', @Person, @PersonID, @Date,@idx)";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_List[3].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[4].ToString();
			
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[5].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[6].ToString());
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[7].ToString());
			
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_List[8].ToString());
			comm.ExecuteNonQuery();
		}


		
		/// <summary>
		/// 외주납품의뢰원장에 등록하는 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideOrderRequestRegister(SqlConnection conn, SqlTransaction tr)
		{
			string str="Insert into ODR_HT (ItemNum,ItemDrawNum,ItemName,BeginProcessSequenceNum,BeginProcessCode,BeginProcessName,EndProcessSequenceNum,EndProcessCode,EndProcessName,CompanyName,BusinessRegistrationNum,DeliveryRequestQuantity,"+
				"ApplyUnitCost,TotalCost,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@beginsequencenum,@beginprocesscode,@beginprocess,@endsequencenum,@endprocesscode,@endprocess,@com,@comnum,@quantity,@cost,@totalcost,'대기', @Person, @PersonID, @Date,@idx)";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_List[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_List[2].ToString();
			comm.Parameters.Add("@beginprocesscode",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.Parameters.Add("@beginprocess",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@endprocesscode",SqlDbType.VarChar).Value = m_List[7].ToString();
			comm.Parameters.Add("@endprocess",SqlDbType.VarChar).Value = m_List[8].ToString();
			
			////////////////////////////////////////
			//  선택된 품목의 시작공정순서를 가져온다 
			//  품목과 시작공정코드를 가지고 공정순서테이블에서
			//  시작공정순서를 찾아온다
			////////////////////////////////////////
			string str_pro = "Select * From PSI_MT where RecodingState = 1 and ProcessCode = @code and ItemNum = @num";
			SqlCommand comm_pro = new SqlCommand(str_pro,conn);
			comm_pro.Transaction= tr;
			comm_pro.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm_pro.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[4].ToString();
			SqlDataAdapter da = new SqlDataAdapter(comm_pro);
			DataSet ds= new DataSet();
			da.Fill(ds);

			comm.Parameters.Add("@beginsequencenum",SqlDbType.Int).Value = int.Parse(ds.Tables[0].Rows[0]["ProcessSequenceNum"].ToString());//시작공정순서
			

			////////////////////////////////////////
			//  선택된 품목의 종료공정순서를 가져온다 
			//  품목과 종료공정코드를 가지고 공정순서테이블에서
			//  종료공정순서를 찾아온다
			////////////////////////////////////////
			
			string str_pro1 = "Select * From PSI_MT where RecodingState = 1 and ProcessCode = @code and ItemNum = @num";
			SqlCommand comm_pro1 = new SqlCommand(str_pro1,conn);
			comm_pro1.Transaction = tr;
			comm_pro1.Parameters.Add("@num",SqlDbType.VarChar).Value = m_List[0].ToString();
			comm_pro1.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[7].ToString();
			SqlDataAdapter da1 = new SqlDataAdapter(comm_pro1);
			DataSet ds1= new DataSet();
			da1.Fill(ds1);
			
			comm.Parameters.Add("@endsequencenum",SqlDbType.Int).Value = int.Parse(ds1.Tables[0].Rows[0]["ProcessSequenceNum"].ToString()); //종료공정순서



			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_List[9].ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[10].ToString();
			
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_List[11].ToString());
			
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[12].ToString());

			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_List[13].ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_List[14].ToString());
			comm.ExecuteNonQuery();

		}




		/// <summary>
		/// 구매납품후 무검사인경우 발주원장 잔량을 변경하는 함수
		/// 잔량이 0 이면 진행상태 완료
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingOrderUpdate(SqlConnection conn, SqlTransaction tr)
		{

			Decimal Remain = 0;
			Decimal quantity = 0;
			Decimal list;

			// 구매발주원장에서 총발주량을 구한다
			string str_BO = "Select * From BO_HT where BuyingOrderHistoryIndex = @index";
			SqlCommand comm_BO = new SqlCommand(str_BO,conn);
			comm_BO.Transaction = tr;
			comm_BO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			SqlDataReader dr_BO = comm_BO.ExecuteReader();
			while(dr_BO.Read())
			{
				quantity = decimal.Parse(dr_BO["OrderQuantity"].ToString());
				Remain = decimal.Parse(dr_BO["RemainQuantity"].ToString());
			}
			dr_BO.Close();


			if(m_List[2].ToString() ==null || m_List[2].ToString() =="")
				list = 0;
			else
				list = Decimal.Parse(m_List[2].ToString().Trim());

			//Remain = quantity - list;
			Remain = Remain - list;
			Math.Round(Remain,2);

			string str = "";
			
			if(Remain <=0)
				str = "UPDATE BO_HT SET ProgressCondition = '완료', RemainQuantity = @remain WHERE BuyingOrderHistoryIndex = @idx";
			else
				str = "UPDATE BO_HT SET ProgressCondition = '진행', RemainQuantity = @remain WHERE BuyingOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[6].ToString());
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 무검사로 입고시 구매입고원장 진행상태 완료로 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void BuyingDeliveryUpdate(SqlConnection conn, SqlTransaction tr, int index)
		{

			string str = "Select HistoryIndex1 From QI_HT where QualityInspectionHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Parameters.Add("@idx",int.Parse(m_grid.Rows[index].Cells.FromKey("HistoryIndex").Text));
			comm.Connection = conn;
			comm.Transaction = tr;
			int count = int.Parse(comm.ExecuteScalar().ToString());

			str ="UPDATE BD_HT SET ProgressCondition = '완료' WHERE BuyingDeliveryHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",count);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


		}




		/// <summary>
		/// 외주납품목중 무검사인경우 발주원장 잔량을 변경하는 함수
		/// 잔량이 0이면 진행상태를 완료로 처리
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideOrderUpdate(SqlConnection conn, SqlTransaction tr)
		{
			Decimal Remain = 0;
			Decimal quantity=0;
			Decimal list;

			string str_OO = "Select * From OO_HT where OutSideOrderHistoryIndex = @index";
			SqlCommand comm_OO = new SqlCommand(str_OO,conn);
			comm_OO.Transaction = tr;
			comm_OO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			SqlDataReader dr_OO = comm_OO.ExecuteReader();
			while(dr_OO.Read())
			{
				quantity = decimal.Parse(dr_OO["OrderQuantity"].ToString());
				Remain = decimal.Parse(dr_OO["RemainQuantity"].ToString());
			}
			dr_OO.Close();
			if(m_List[2].ToString() ==null || m_List[2].ToString() =="")
				list = 0;
			else
				list = Decimal.Parse(m_List[2].ToString().Trim());

			//Remain = quantity - list;
			Remain = Remain - list;
			Math.Round(Remain,2);


			string str = "";
			
			if(Remain <=0)
				str = "UPDATE OO_HT SET ProgressCondition = '완료', RemainQuantity = @remain WHERE OutSideOrderHistoryIndex = @idx";
			else
				str = "UPDATE OO_HT SET ProgressCondition = '진행', RemainQuantity = @remain WHERE OutSideOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(alist[12].ToString());
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.ExecuteNonQuery();
		}
		

		


		






		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////                             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////// 영업관련 원장및 테이블 변경 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////                             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



		/// <summary>
		/// 생산의뢰원장 등록
		/// </summary>
		/// <param name="rowcount">그리드의 행번호를 넘겨준다</param>
		private void ProductionRequestRegister(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,ProductionRequestSourceCode,ProductionRequestSource,"+
				"CompanyName,BusinessRegistrationNum,PropertyClassification,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
				"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
				"VolumNum,ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate,ReceivingOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@comname,@comnum,@classification,@total,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,@num,'대기', @Person, @PersonID, @Date,@idx)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03100010";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
			comm.Parameters.Add("@comname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("CompanyName").Value.ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("PropertyClassification").Value.ToString();

			if(m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString() ==null || m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Text);
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Text);

			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value == null || Convert.IsDBNull(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value))
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString());

			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value == null)
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value == null)
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString();
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value == null)
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Text);
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value == null)
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() =="")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);
			
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 팬텀 품목에 대한 생산의뢰원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void ProductionRequestRegister1(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str_Bom = @"Select ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator,ItemDrawNum, ItemName, From IOI_MT inner join II_MT on IOI_MT.ParentItemNum = II_MT.ItemNum Where IOI_MT.ParentItemNum = @num and IOI_MT.RecodingState = 1";
			SqlCommand comm_Bom = new SqlCommand(str_Bom,conn);
			comm_Bom.Transaction = tr;
			comm_Bom.Parameters.Add("@num",m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			SqlDataAdapter da = new SqlDataAdapter(comm_Bom);
			DataSet ds_Bom = new DataSet();
			da.Fill(ds_Bom);

			foreach(DataRow dr in ds_Bom.Tables[0].Rows)
			{
			
				string str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,ProductionRequestSourceCode,ProductionRequestSource,"+
					"CompanyName,BusinessRegistrationNum,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
					"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
					"VolumNum,ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate,ReceivingOrderHistoryIndex) values("+
					"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@comname,@comnum,@total,@quantity1,@date1,@quantity2,@date2,"+
					"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,@num,'대기', @Person, @PersonID, @Date,@idx)";

				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;

				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
				comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = dr["ItemDrawNum"].ToString();
				comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = dr["ItemName"].ToString();
				comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03100010";
				comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";
				comm.Parameters.Add("@comname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("CompanyName").Value.ToString();
				comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			
				if(m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString() ==null || m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Value.ToString().Trim() =="")
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("TotalReceiveingOrderQuantity").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);
			
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value =Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);

				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value == null || Convert.IsDBNull(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value))
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				else
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString());

				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString().Trim() =="")
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value == null)
					comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				else
					comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value.ToString());
			
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value.ToString().Trim() =="")
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value == null)
					comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				else
					comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
			
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString().Trim() =="")
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value == null)
					comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				else
					comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Text);
			
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString().Trim() =="")
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Math.Round(decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text)*decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()),2);
				if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value == null)
					comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				else
					comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value.ToString());
			
				if(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() =="")
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);
			
				comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
				comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
				comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
				comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
				comm.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 수주원장 업데이트
		/// </summary>
		/// <param name="rowcount"></param>
		private void ReceivingOrderHistoryUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			
			string str = "UPDATE RO_HT SET ProgressCondition = '진행' WHERE ReceivingOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction= tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
	
			comm.ExecuteNonQuery();
		}


//		private void GoodsBuyingRequestRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
//		{
//			//먼저 영업창고 납품창고에 임시창고를 생성
//			//수주원장의 대기/진행인 상품들을 GroupBy 해 잔량을 확인한 다음
//			//하나씩 감안해 임시창고 수량을 감소
//			//그런다음 지금 선택되어진 품목의 수량만틈 감소
//			//마이너스 만큼 의뢰
//
//			decimal TempQuantity = 0;
//
//			if(rowcount == 0)
//			{
//				TempStockCreate(conn,tr);
//			
//				//창고잔량
//				TempQuantity = RemainQuantityDiminution(conn,tr,rowcount);
//			
//				//외주의뢰,외주발주잔량
//				TempQuantity += BuyingQuantity(conn,tr,rowcount);
//			}
//
//			//해당 품목 의뢰수량
//			decimal RequestQuantity = (decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text)+
//				decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text));
//
//			if(TempQuantity-RequestQuantity < 0)
//				GoodsRequestRegister(conn, tr,rowcount, TempQuantity);
//		}
//
//		private void TempStockCreate(SqlConnection con,SqlTransaction trans)
//		{
//			SqlCommand comm = new SqlCommand("SPStockStoreTableMaking",con);
//			comm.Transaction = trans;
//			comm.CommandType = CommandType.StoredProcedure;
//			comm.ExecuteNonQuery();
//		}
//
//		/// <summary>
//		/// 구매의뢰원장 대기 및 구매발주원장 대기,진행 수량을 임시창고수량에 누적
//		/// </summary>
//		/// <param name="con"></param>
//		/// <param name="trans"></param>
//		/// <param name="count"></param>
//		/// <returns></returns>
//		private decimal BuyingQuantity(SqlConnection con, SqlTransaction trans,int count)
//		{
//			decimal RequestQuantity = 0;
//			decimal RemainQuantity = 0;
//			decimal Quantity = 0;
//
//			string str = @"Select Sum(OrderQuantity) as OrderQuantity From OOR_HT where ProgressCondition = '대기' and  ItemNum = @num Group by ItemNum";
//			SqlCommand comm = new SqlCommand();
//			comm.CommandText = str;
//			comm.Connection = con;
//			comm.Transaction = trans;
//			comm.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
//
//			SqlDataReader dr = comm.ExecuteReader();
//			while(dr.Read())
//			{
//				RequestQuantity = decimal.Parse(dr["OrderQuantity"].ToString());
//			}
//			dr.Close();
//			comm.Parameters.Clear();
//			
//
//			str = @"Select Sum(RemainQuantity) as RemainQuantity From OO_HT Where (ProgressCondition = '대기' or ProgressCondition = '진행')
//					and ItemNum = @num Group by ItemNum";
//			SqlCommand comm1 = new SqlCommand();
//			comm1.CommandText = str;
//			comm1.Connection = con;
//			comm1.Transaction = trans;
//			comm1.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
//			
//			
//
//			SqlDataReader dr1 = comm1.ExecuteReader();
//			while(dr1.Read())
//			{
//				RemainQuantity = decimal.Parse(dr1["RemainQuantity"].ToString());
//
//				if(RemainQuantity < 0 )
//					RemainQuantity = 0;
//			}
//			dr1.Close();
//			comm1.Parameters.Clear();
//
//
//			Quantity = RemainQuantity;
//
//
//			str = @"Update TBStockStore Set Quantity += @Quantity Where ItemNum = @num";
//			SqlCommand comm2 = new SqlCommand();
//			comm2.CommandText = str;
//			comm2.Connection = con;
//			comm2.Transaction = trans;
//			comm2.Parameters.Add("@Quantity",Quantity);
//			comm2.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
//			comm2.ExecuteNonQuery();
//
//
//			return Quantity;
//
//		}
//
//
//		private decimal RemainQuantityDiminution(SqlConnection con, SqlTransaction trans,int count)
//		{
//
//
//
//			//decimal StoreQuantity = 0;
//			decimal RemainQuantity = 0;
//			decimal Quantity = 0;
//			string str = @"Select Sum(RemainderQuantity) as RemainderQuantity From RO_HT Where (ProgressCondition = '대기' or ProgressCondition = '진행')
//					and ItemNum = @num Group by ItemNum";
//			SqlCommand comm1 = new SqlCommand();
//			comm1.CommandText = str;
//			comm1.Connection = con;
//			comm1.Transaction = trans;
//			comm1.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
//			
//			
//
//			SqlDataReader dr1 = comm1.ExecuteReader();
//			while(dr1.Read())
//			{
//				RemainQuantity = decimal.Parse(dr1["RemainderQuantity"].ToString());
//
//				if(RemainQuantity < 0 )
//					RemainQuantity = 0;
//			}
//			dr1.Close();
//			comm1.Parameters.Clear();
//
//
//			str = @"Update TBStockStore Set Quantity -= @Quantity Where ItemNum = @num";
//			SqlCommand comm2 = new SqlCommand();
//			comm2.CommandText = str;
//			comm2.Connection = con;
//			comm2.Transaction = trans;
//			comm2.Parameters.Add("@Quantity",RemainQuantity);
//			comm2.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
//			comm2.ExecuteNonQuery();
//
//
//			return Quantity;
//
//		}
//
//
//		/// <summary>
//		/// 구매의뢰원장 등록함수(상품)
//		/// </summary>
//		/// <param name="conn"></param>
//		/// <param name="tr"></param>
//		/// <param name="rowcount">그리드의 행번호</param>
//		/// <param name="Quantity">잉여수량</param>
//		private void GoodsRequestRegister(SqlConnection con, SqlTransaction trans,int count,decimal Quantity)
//		{
//			string str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
//				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
//				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
//				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,VolumNum,RequestPostCode,RequestPost,ProgressCondition,"+
//				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
//				"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
//				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@num,@postcode,@post,'대기', @Person, @PersonID, @Date,@idx,@section)";
//
//
//			SqlCommand comm = new SqlCommand(str,con);
//			comm.Transaction = trans;
//
//			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
//			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
//			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
//			comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("PropertyClassification").Value.ToString();
//			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03300010";
//			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";			
//			
//			
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Text);
//
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate1").Value.ToString());
//
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text);
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value.ToString());
//			
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text);
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
//			
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Text);
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Value.ToString());
//			
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString().Trim() =="")
//				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
//			else
//				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text);
//			if(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value == null || m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value.ToString());
//			
//			decimal total = (decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text)+
//				decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text));
//
//			comm.Parameters.Add("@total",SqlDbType.Decimal).Value =  total - Quantity;
//
//			if(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@cost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text);
//			
//			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = (decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text)+
//				decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Text)+decimal.Parse(m_grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text))*decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text);
//
//			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;
//
//			comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = m_UserName.ToString();
//			comm.Parameters.Add("@post",SqlDbType.VarChar).Value = m_UserName.ToString();
//
//			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
//			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
//			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
//			comm.Parameters.Add("@idx",SqlDbType.Int).Value =int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
//			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "수주";
//			
//			
//			
//			comm.ExecuteNonQuery();
//			
//			
//		}


		/// <summary>
		/// 구매의뢰원장 등록함수(상품)
		/// </summary>
		/// <param name="rowcount">그리드의 행번호</param>
		private void GoodsBuyingRequestRegister(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,VolumNum,RequestPostCode,RequestPost,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
				"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@num,@postcode,@post,'대기', @Person, @PersonID, @Date,@idx,@section)";


			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_grid.Rows[rowcount].Cells.FromKey("PropertyClassification").Value.ToString();
			comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = "03300010";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "정상";			
			
			
			if(m_grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Value == null || m_grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text);

			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString());

			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value.ToString());
			
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text);
			if(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value == null || m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value.ToString().Trim() == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value.ToString());
			
			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text);

			//comm.Parameters.Add("@total",SqlDbType.Decimal).Value = (decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text)+
			//	decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Text)+decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text));

//			if(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "")
//				comm.Parameters.Add("@cost",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//			else
//				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);

			decimal Total = decimal.Parse(m_grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text);
			
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Convert.DBNull;
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Convert.DBNull;//TotalCost(m_grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim(),conn,tr,rowcount, Total);
			
			comm.Parameters.Add("@num",SqlDbType.Int).Value = vol+1;

			comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@post",SqlDbType.VarChar).Value = m_UserName.ToString();

			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value =int.Parse(m_grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "수주";
			
			
			
			comm.ExecuteNonQuery();
			
			
		}


		/// <summary>
		/// 입고원장 등록함수
		/// </summary>
		/// <param name="count"></param>
		private int BusinessStorehouseInStorehouseRegister(SqlConnection conn,SqlTransaction tr, int idx)
		{
			string str = @"Insert Into IS_HT (ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, 
					ProcessName, InStorehouseQuantity, BusinessStorehouseNum, InStoreDate, ProgressCondition, 
					RegistrationPerson, RegistrationPersonID, RegistrationDate, InStorehouseRequestHistoryIndex) 
					values(@itemNum, @itemDrawNum, @itemName, @processSequenceNum, @processCode, 
					@processName, @inStorehouseQuantity, @businessStorehouseNum, @InStoreDate, '대기', @Person, @PersonID, @date, @index)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemNum",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemDrawNum",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemName",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemName").Value.ToString();
			if(m_grid.Rows[idx].Cells.FromKey("ProcessSequenceNum").Value == null || m_grid.Rows[idx].Cells.FromKey("ProcessSequenceNum").Value.ToString() == "")
				comm.Parameters.Add("@processSequenceNum",SqlDbType.Int).Value = 0;
			else
				comm.Parameters.Add("@processSequenceNum",SqlDbType.Int).Value = int.Parse(m_grid.Rows[idx].Cells.FromKey("ProcessSequenceNum").Value.ToString());
			comm.Parameters.Add("@processCode",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@processName",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessName").Value.ToString();
			comm.Parameters.Add("@inStorehouseQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text);
			comm.Parameters.Add("@businessStorehouseNum","1");//
			comm.Parameters.Add("@InStoreDate", DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).ToShortDateString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value =  m_User;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[idx].Cells.FromKey("InStorehouseRequestHistoryIndex").Value.ToString());

			comm.ExecuteNonQuery();

			str = "Select Max(InStorehouseHistoryIndex) From IS_HT";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Transaction = tr;
			int a = Convert.ToInt32(comm1.ExecuteScalar());
			return a;

		}


		/// <summary>
		/// 제품입고 등록시 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="index"></param>
		private void BusinessStorehouseInStorehouseHistory(SqlConnection conn,SqlTransaction tr, int index, int idx)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'99','14009999','최종품','영업창고','1',@Quantity,@Date,'제품입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Connection = conn;
			comm.Transaction = tr;

			comm.Parameters.Add("@itemNum", m_grid.Rows[idx].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@itemDrawNum",m_grid.Rows[idx].Cells.FromKey("ItemDrawNum").Value.ToString());
			comm.Parameters.Add("@itemName", m_grid.Rows[idx].Cells.FromKey("ItemName").Value.ToString());
			comm.Parameters.Add("@Quantity",decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text));					//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).ToShortDateString());	//입출고일
			comm.Parameters.Add("@HistoryIndex",index);												//원장번호
            comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(Division(m_grid.Rows[idx].Cells.FromKey("ItemNum").Value.ToString()) == 4)
			{
			
				//제품입고시 생산창고 출고
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'99','14009999','최종품','생산창고','0',@Quantity,@Date,'제품입고',@HistoryIndex)";
				
				comm.Parameters.Add("@itemNum", m_grid.Rows[idx].Cells.FromKey("ItemNum").Value.ToString());
				comm.Parameters.Add("@itemDrawNum",m_grid.Rows[idx].Cells.FromKey("ItemDrawNum").Value.ToString());
				comm.Parameters.Add("@itemName", m_grid.Rows[idx].Cells.FromKey("ItemName").Value.ToString());
				//comm.Parameters.Add("@ProcessSequenceNum",99);				//공정순서
				//comm.Parameters.Add("@ProcessCode","14009999");								//공정코드
				//comm.Parameters.Add("@ProcessName","최종품");								//공정명
				//comm.Parameters.Add("@Division",0);													//입출고구분
				comm.Parameters.Add("@Quantity",decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text));					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).ToShortDateString());	//입출고일
				//comm.Parameters.Add("@HistoryDivision","제품입고");									//원장구분
				comm.Parameters.Add("@HistoryIndex",index);												//원장번호
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
			else
			{
				//제품입고시 생산창고 출고
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','0',@Quantity,@Date,'제품입고',@HistoryIndex)";
				
				comm.Parameters.Add("@itemNum", m_grid.Rows[idx].Cells.FromKey("ItemNum").Value.ToString());
				comm.Parameters.Add("@itemDrawNum",m_grid.Rows[idx].Cells.FromKey("ItemDrawNum").Value.ToString());
				comm.Parameters.Add("@itemName", m_grid.Rows[idx].Cells.FromKey("ItemName").Value.ToString());
				comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[idx].Cells.FromKey("ProcessSequenceNum").Value.ToString()));				//공정순서
				comm.Parameters.Add("@ProcessCode",m_grid.Rows[idx].Cells.FromKey("ProcessCode").Value.ToString());								//공정코드
				comm.Parameters.Add("@ProcessName",m_grid.Rows[idx].Cells.FromKey("ProcessName").Value.ToString());								//공정명
				//comm.Parameters.Add("@Division",0);													//입출고구분
				comm.Parameters.Add("@Quantity",decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text));					//입출고 수량					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).ToShortDateString());	//입출고일
				//comm.Parameters.Add("@HistoryDivision","제품입고");									//원장구분
				comm.Parameters.Add("@HistoryIndex",index);												//원장번호

				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
		}

		/// <summary>
		/// 입고원장 등록시 입고의뢰원장 진행상태 변경(완료)함수
		/// </summary>
		/// <param name="count"></param>
		private void BusinessStorehouseInStorehouseRequestUpdate(SqlConnection conn,SqlTransaction tr, int idx)
		{
			string str = "Update ISR_HT Set ProgressCondition = '완료' where InStorehouseRequestHistoryIndex = @index";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[idx].Cells.FromKey("InStorehouseRequestHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 입고원장 등록시 품질검사원장 진행상태 변경(입고완료) 함수
		/// </summary>
		/// <param name="count"></param>
		private void BusinessStorehouseInStorehouseQualityInspectionUpdate(SqlConnection conn, SqlTransaction tr, int idx)
		{
			
			//string str = "Update QI_HT Set ProgressCondition = '입고완료' where HistoryIndex1 = @index and HistorySection1 = @Section ";
			string str = "Update QI_HT Set ProgressCondition = '입고완료' where QualityInspectionHistoryIndex = @index ";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[idx].Cells.FromKey("HistoryIndex").Value.ToString());
			//comm.Parameters.Add("@Section",SqlDbType.VarChar).Value = m_grid.Rows[int.Parse(m_List[2].ToString())].Cells.FromKey("HistorySection").Value.ToString();
			comm.ExecuteNonQuery();

		}


		/// <summary>
		/// 입고원장 등록시 생산창고에서 출고수량증가시키고 영업창고 입고수량 증가시키는 함수
		/// </summary>
		/// <param name="count"></param>
		private void BusinessStorehouseInStorehousetableUpdate(SqlConnection conn, SqlTransaction tr, int idx)
		{
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			///

			///////////////////////////////////////////
			// 창고의 금액을 위해 공정정보에서       //
			// 진척율을   가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal rate = 0;

			if(Division(m_grid.Rows[idx].Cells.FromKey("ItemNum").Text) == 4)
				rate = 100;
			else
			{
				string str_rate = "Select ProgressRate From PSI_MT Where RecodingState =1 and ItemNum = @ItemNum and ProcessCode = @code";
				SqlCommand comm_rate = new SqlCommand(str_rate,conn);
				comm_rate.Transaction = tr;
				comm_rate.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
				comm_rate.Parameters.Add("@code", SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessCode").Text;
				SqlDataReader dr_rate = comm_rate.ExecuteReader();
				if(dr_rate.Read())
					rate = decimal.Parse(dr_rate["ProgressRate"].ToString());
				dr_rate.Close();
			}
			///////////////////////////////////////////
			// 창고의 금액을 위해 공정정보에서       //
			// 진척율을   가져와 계산한다.   -끝-    //
			///////////////////////////////////////////

			int year = DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[idx].Cells.FromKey("InStoreDate").Text).Month;

			// 영업창고 입고수량 증가
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text);
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text)*cost;
			comm.Parameters.Add("@storenum", "1");//창고번호
			comm.Parameters.Add("@year",year);

			
			Table table = new Table(year,mon);
			comm.CommandText = table.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(!WorkPlan3())
			{
				// 생산창고 출고수량 증가
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text);
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text)*cost*rate/100;
				if(Division(m_grid.Rows[idx].Cells.FromKey("ItemNum").Text) == 4)
					comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14009999";
				else
					comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@year",year);
				comm.CommandText = table.OutProductionTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				if(IsManaged(conn,tr,m_grid.Rows[idx].Cells.FromKey("ItemNum").Text))
				{
					// 생산창고 출고수량 증가
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text);
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[idx].Cells.FromKey("InstorehouseRequestQuantity").Text)*cost*rate/100;
					if(Division(m_grid.Rows[idx].Cells.FromKey("ItemNum").Text) == 4)
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14009999";
					else
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessCode").Text;
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.OutProductionTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @num and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[idx].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[idx].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
			



		}


		/// <summary>
		/// 출고원장 등록함수
		/// </summary>
		/// <param name="count"></param>
		private void GoodsManufactureOutStorehouseRegister(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str="Insert into OS_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,OutStorehouseQuantity,UnInspectionQuantity,ApplyUnitCost,LotNum, OrderNum, "+
				"BusinessStorehouseNum,OutStoreDate,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,ReceivingOrderHistoryIndex) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@total,@uninspection, @cost,@LotNum,@OrderNum,@store,@outdate,'대기', @Person, @PersonID, @Date,@idx)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("CompanyName").Value.ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			
			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());

			comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			
			if(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value == null || m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_List[3].ToString();
			comm.Parameters.Add("@OrderNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("OrderNum").Value.ToString().Trim();
			comm.Parameters.Add("@store",SqlDbType.TinyInt).Value = int.Parse(m_List[1].ToString());
			comm.Parameters.Add("@outdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[2].ToString()).ToShortDateString();
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			
			
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 제품출고등록시 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void GoodsManufactureOutStorehouseHistroy(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str = "";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			
			comm.Transaction = tr;	
			if(Division(m_grid.Rows[count].Cells.FromKey("ItemNum").Text) == 1 || Division(m_grid.Rows[count].Cells.FromKey("ItemNum").Text) == 2 )
			{
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'보용품창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				comm.Parameters.Add("@itemnum",m_grid.Rows[count].Cells.FromKey("ItemNum").Text);									//품목번호
				comm.Parameters.Add("@itemdrawnum", m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);						//도면번호
				comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);								//품목명
				comm.Parameters.Add("@ProcessSequenceNum",NumFind(m_grid.Rows[count].Cells.FromKey("ItemNum").Text,conn,tr));			//공정순서
				comm.Parameters.Add("@ProcessCode",CodeFind(m_grid.Rows[count].Cells.FromKey("ItemNum").Text,conn,tr));					//공정코드
				comm.Parameters.Add("@ProcessName",NameFind(CodeFind(m_grid.Rows[count].Cells.FromKey("ItemNum").Text,conn,tr),conn,tr));					//공정명
				if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
					comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@Quantity", decimal.Parse(m_List[0].ToString()));			//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(m_List[2].ToString()).ToShortDateString());	//입출고일
				comm.Parameters.Add("@HistoryIndex",MaxOS_HT(conn,tr));									//원장번호
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품','영업창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				
				
				comm.Parameters.Add("@itemnum",m_grid.Rows[count].Cells.FromKey("ItemNum").Text);									//품목번호
				comm.Parameters.Add("@itemdrawnum", m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);						//도면번호
				comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);								//품목명
				//comm.Parameters.Add("@ProcessSequenceNum",0);												//공정순서
				//comm.Parameters.Add("@ProcessCode","14000000");											//공정코드
				//comm.Parameters.Add("@ProcessName","소재");												//공정명
				//comm.Parameters.Add("@Division",0);														//입출고구분(출고)
				if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
					comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
				else
					comm.Parameters.Add("@Quantity", decimal.Parse(m_List[0].ToString()));			//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(m_List[2].ToString()).ToShortDateString());	//입출고일
				//comm.Parameters.Add("@HistoryDivision","제품출고");										//원장구분
				comm.Parameters.Add("@HistoryIndex",MaxOS_HT(conn,tr));									//원장번호
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}


		/// <summary>
		/// 품목의 공정순서를 찾는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int NumFind(string ItemNum, SqlConnection con, SqlTransaction trans)
		{
			int Num = -10;
			string str  = "Select ProcessSequenceNum From AS_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",ItemNum);
			Num = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if(Num == -10)
				throw new Exception(" 보용품 창고에 " +ItemNum+" 품목이 존재하지 않습니다!");
			return Num;
		}


		/// <summary>
		/// 품목의 공정 코드를 찾는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private string CodeFind(string ItemNum, SqlConnection con, SqlTransaction trans)
		{
			string Code = "";
			string str  = "Select ProcessCode From AS_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Code = dr["ProcessCode"].ToString();
			dr.Close();
			comm.Parameters.Clear();
			if(Code == "")
				throw new Exception("품목 "+ItemNum+"이 존재하지 않습니다");
			return Code;
		}


		private string NameFind(string Code,SqlConnection con, SqlTransaction trans)
		{
			string Name = "";
			string str  = "Select SmallClassificationName From PUC_MT where RecodingState = 1 and SmallClassificationCode = @Code";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Code",Code);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Name = dr["SmallClassificationName"].ToString();
			dr.Close();
			comm.Parameters.Clear();
			
			return Name;
		}

        /// <summary>
        /// 제품출고원장번호 찿기
        /// </summary>
        /// <returns></returns>
		private int MaxOS_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str  = "Select Max(OutStorehouseHistoryIndex) From OS_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}


		/// <summary>
		/// 출고원장 등록후 수주원장 수량 수정함수
		/// </summary>
		/// <param name="count"></param>
		private void GoodsReceivingOrderUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str = "UPDATE RO_HT SET ProgressCondition = '진행', OutStorehouseQuantity = @quantity,UnInspectionQuantity = @unquantity WHERE ReceivingOrderHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Text) + decimal.Parse(m_List[0].ToString());
			
			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@unquantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@unquantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("UnInspectionQuantity").Text) + decimal.Parse(m_List[0].ToString());

			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
	
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 보용품 창고에서 출고수량 증가함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void AS_MTUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[2].ToString()).Year;
			int mon = DateTime.Parse(m_List[2].ToString()).Month;
			Table table = new Table(year,mon);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
	
			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.OutAddItemTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM AS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}
		/// <summary>
		/// 영업창고에서 출고수량 증가함수
		/// </summary>
		/// <param name="count"></param>
		private void BS_MTTable(SqlConnection conn, SqlTransaction tr, int count)
		{
			

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[2].ToString()).Year;
			int mon = DateTime.Parse(m_List[2].ToString()).Month;
			Table table = new Table(year,mon);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
	
			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@process", SqlDbType.VarChar).Value = "14009999";			
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[1].ToString());
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True" && Division(ItemName) == 4)
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}

		/// <summary>
		/// 출고원장 등록후 납품창고에 입고수량 증가
		/// </summary>
		/// <param name="count"></param>
		private void DS_MTTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_List[2].ToString()).Year;
			int mon = DateTime.Parse(m_List[2].ToString()).Month;
			Table table = new Table(year,mon);

			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
			comm.Parameters.Add("@year",year);
			comm.CommandText= table.InDeliveryTable();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 기타입출고원장 등록
		/// </summary>
		/// <param name="count"></param>
		private void OtherInOutStorehouseRegister(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str="Insert into OIOS_HT (ItemNum, ItemDrawNum, ItemName,ProcessSequenceNum, ProcessCode, ProcessName, StorehouseName, BusinessStorehouseNum, InOutStorehouseDistinction, InOutStorehouseQuantity, InOutStorehouseReasonCode,InOutStorehouseReason, InOutStorehouseDetailReason, InOutDate, CompanyName, BusinessRegistrationNum, RegistrationPerson, RegistrationPersonID, RegistrationDate ) values("+
				"@itemnum, @itemdrawnum,@itemname,@processnum, @processcode, @process, @StorehouseName,@BusinessStorehouseNum,@InOutStorehouseDistinction,@InOutStorehouseQuantity, @InOutStorehouseReasonCode, "+
				"@InOutStorehouseReason,@InOutStorehouseDetailReason, @InOutDate, @com, @businessnum, @Person, @PersonID, @Date)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@processnum",SqlDbType.TinyInt).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Value.ToString());
			comm.Parameters.Add("@processcode",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@process",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessName").Value.ToString();
			comm.Parameters.Add("@StorehouseName",SqlDbType.VarChar).Value = m_List[2].ToString();//창고명
			comm.Parameters.Add("@BusinessStorehouseNum",SqlDbType.Int).Value = int.Parse(m_List[3].ToString());//영업창고번호
			comm.Parameters.Add("@InOutStorehouseDistinction",SqlDbType.Bit).Value = int.Parse(m_List[1].ToString());//입출고도면
			comm.Parameters.Add("@InOutStorehouseQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@InOutStorehouseReasonCode",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.Parameters.Add("@InOutStorehouseReason",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.Parameters.Add("@InOutStorehouseDetailReason",SqlDbType.VarChar).Value = m_List[6].ToString();
			comm.Parameters.Add("@InOutDate",DateTime.Parse(m_List[9].ToString()).ToShortDateString());

			if(m_List[7].ToString().Trim() == "")
				comm.Parameters.Add("@com",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_List[7].ToString();
			if(m_List[8].ToString().Trim() == "")
				comm.Parameters.Add("@businessnum",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@businessnum",SqlDbType.VarChar).Value = m_List[8].ToString();
			
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[9].ToString()).ToShortDateString());

			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 기타입출고등록시 이력원장 기록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void OtherInOutStorehouseHistory(SqlConnection conn, SqlTransaction tr, int count)
		{
			string StoreName = "";
			switch(m_List[2].ToString())
			{
				case "원자재창고":
					StoreName = "원자재창고";
					break;
				case "생산창고":
					StoreName = "생산창고";
					break;
				case "외주창고":
					StoreName = "외주창고";
					break;
				case "납품창고":
					StoreName = "납품창고";
					break;
				default:
					StoreName = "영업창고";
					break;
			}
			//원자재가 출고되는 이력을 기록한다.
			string 	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@StoreName,@Division,@Quantity,@Date,'기타입출고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@itemnum", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@itemdrawnum",m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);
			comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text));
			comm.Parameters.Add("@ProcessCode", m_grid.Rows[count].Cells.FromKey("ProcessCode").Text);
			comm.Parameters.Add("@ProcessName",m_grid.Rows[count].Cells.FromKey("ProcessName").Text);
			comm.Parameters.Add("@StoreName",StoreName);
			comm.Parameters.Add("@Division",int.Parse(m_List[1].ToString()));							//입출고구분(입고 1, 출고 0)
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[0].ToString()));						//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[9].ToString()).ToShortDateString());								//입출고일
			//comm.Parameters.Add("@HistoryDivision","기타입출고");										//원장구분
			comm.Parameters.Add("@HistoryIndex",MaxOIOS_HT(conn,tr));									//원장번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private int MaxOIOS_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str  = "Select Max(OtherInOutStorehouseHistoryIndex) From OIOS_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}

		/// <summary>
		/// 기타입출고원장 등록시 각 창고테이블에 수량변경
		/// </summary>
		/// <param name="count"></param>
		private void OtherInOutStorehouseTable(SqlConnection conn, SqlTransaction tr, int count)
		{
			string m_store;//창고테이블 명 
			switch(m_List[2].ToString())
			{
				case "원자재창고":
					m_store = "RMS_MT";
					break;
				case "생산창고":
					m_store = "PS_MT";
					break;
				case "외주창고":
					m_store = "OS_MT";
					break;
				case "납품창고":
					m_store = "DS_MT";
					break;
				default:
					m_store = "BS_MT";
					break;
			}


			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			int year = DateTime.Parse(m_List[9].ToString()).Year;
			int mon = DateTime.Parse(m_List[9].ToString()).Month;

			Table table = new Table(year,mon);

			switch(m_store)
			{
				case "RMS_MT":
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction =tr;
					comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
				
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
					comm.Parameters.Add("@year",year);
			
					
					if(m_List[1].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InRowTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutRowTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}
						}
						

					}

					
				}
					break;
				case "PS_MT":
				{
					Decimal m_cost = 0; //금액
					Decimal m_progressrate = 0;//진척율
					string m_process = "";//공정코드
					
			
			
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction =tr;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
					
					if(dr_cost.Read())
						m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
					dr_cost.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////

					 
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -시작- //
					//////////////////////////////////////////
					string str_rate = "Select ProgressRate,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
					SqlCommand comm_rate = new SqlCommand(str_rate,conn);
					comm_rate.Transaction =tr;
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
					comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();
					if(dr_rate.Read())
					{
						m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
						m_process = dr_rate["ProcessCode"].ToString();
					}
					dr_rate.Close();
					if(m_process == "")//공정순서에 없으면 상품
					{
						m_progressrate = 100;
						m_process = "14009999";
					}

					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -끝-   //
					//////////////////////////////////////////

					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();

					if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());

					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_process;
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_cost.ToString()) * decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_progressrate.ToString())/100;
					comm.Parameters.Add("@year",year);
			
					
					
					if(m_List[1].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InProductionTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutProductionTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
						
						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_process;
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}
						}
					}

				}
					break;
				case "BS_MT":
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction =tr;
					comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////


					// 영업창고 입고수량 증가
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString())*cost;
					comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[3].ToString());//창고번호
					comm.Parameters.Add("@year",year);					
					
					if(m_List[1].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InBusinessTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutBusinessTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[3].ToString());//창고번호
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}
						}

					}
				}
					break;
				case "OS_MT":
				{
					Decimal m_cost = 0; //금액
					Decimal m_progressrate = 0;//진척율
					int m_prcesssequence = 0;//공정순서
					
			
			
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction =tr;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
					if(dr_cost.Read())
						m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
					dr_cost.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////

					 
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -시작- //
					//////////////////////////////////////////
					string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
					SqlCommand comm_rate = new SqlCommand(str_rate,conn);
					comm_rate.Transaction =tr;
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
					comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();
					if(dr_rate.Read())
					{
						m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
						m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
					}
					dr_rate.Close();
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -끝-   //
					//////////////////////////////////////////

					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();

					if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());

					if(Division(m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString()) == 1)
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_cost.ToString());
					else
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_cost.ToString()) * decimal.Parse(m_progressrate.ToString())/100;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[8].ToString();
					comm.Parameters.Add("@year",year);
									
					
					if(m_List[1].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
					}
					//마이너스 재고허용 여부
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_List[8].ToString();
						comm.Parameters.Add("@year" ,DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}

				}
					break;
				default:			// 납품창고
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction =tr;
					comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
				
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
					comm.Parameters.Add("@year",year);
			
					if(m_List[1].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InDeliveryTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutDeliveryTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@year" ,DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}
						}

					}

				}
					break;
			}
			

			
		}


		/// <summary>
		/// 보용품입고원장 기록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void AddItemRegister(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str= "Insert into AS_HT (ItemNum, ItemDrawNum, ItemName, ProcessCode, ProcessSequenceNum, ProcessName, ReasonCode, ReasonName, StoreName,  InstoreQuantity, InstoreDate, RegistrationPerson, RegistrationPersonID, RegistrationDate) values ("+
				"@itemnum, @itemdrawnum,@itemname,@ProcessCode,@ProcessSequenceNum,@ProcessName, @ReasonCode,@ReasonName, @StoreName, @InstoreQuantity, @InstoreDate,"+
				"@Person, @PersonID, @Date)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();//거래처명
			comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Value.ToString());//사업자등록번호
			comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessName").Value.ToString();//발주번호
			comm.Parameters.Add("@ReasonCode",SqlDbType.VarChar).Value = m_List[2].ToString();
			comm.Parameters.Add("@ReasonName",SqlDbType.VarChar).Value = m_List[3].ToString();
			comm.Parameters.Add("@StoreName",SqlDbType.VarChar).Value = m_List[1].ToString();//창고  
			comm.Parameters.Add("@InstoreQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//입고수량
			comm.Parameters.Add("@InstoreDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[4].ToString());//입고일자
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());
			
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 보용품 입고 이력원장 기록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void AddItemHistoy(SqlConnection conn, SqlTransaction tr, int count)
		{
			string StoreName = "보용품창고";
			//보용품창고에 입고되는 이력을 기록한다.
			string 	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@StoreName,@Division,@Quantity,@Date,'보용품입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@itemnum", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@itemdrawnum",m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);
			comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text));
			comm.Parameters.Add("@ProcessCode", m_grid.Rows[count].Cells.FromKey("ProcessCode").Text);
			comm.Parameters.Add("@ProcessName",m_grid.Rows[count].Cells.FromKey("ProcessName").Text);
			comm.Parameters.Add("@StoreName",StoreName);
			comm.Parameters.Add("@Division",1);							//입출고구분(입고 1, 출고 0)
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[0].ToString()));						//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[4].ToString()).ToShortDateString());								//입출고일
			comm.Parameters.Add("@HistoryIndex",MaxAS_HT(conn,tr));									//원장번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private int MaxAS_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str  = "Select Max(AddItemInStoreIndex) From AS_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}


		private void SubRMS_MTAddItemHistoy(SqlConnection con, SqlTransaction trans, string ItemNum, decimal Quantity)
		{
			string StoreName = "원자재창고";
			//원자재창고에 출고되는 이력을 기록한다.
			string 	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@StoreName,0,@Quantity,@Date,'보용품입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum", ItemNum);
			comm.Parameters.Add("@itemdrawnum",SearchItemDrawNum(con,trans,ItemNum));
			comm.Parameters.Add("@itemname",SearchItemName(con,trans,ItemNum));
			comm.Parameters.Add("@ProcessSequenceNum","0");
			comm.Parameters.Add("@ProcessCode", "14000000");
			comm.Parameters.Add("@ProcessName","소재");
			comm.Parameters.Add("@StoreName",StoreName);
			//comm.Parameters.Add("@Division",0);							//입출고구분(입고 1, 출고 0)
			comm.Parameters.Add("@Quantity",Quantity);						//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[4].ToString()).ToShortDateString());								//입출고일
			comm.Parameters.Add("@HistoryIndex",MaxAS_HT(con,trans));									//원장번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private string SearchItemDrawNum(SqlConnection conn, SqlTransaction tr,string ItemNum)
		{
			string ItemDrawNum = "";
			string 	str=@"Select ItemDrawNum From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@ItemNum", ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				ItemDrawNum = dr["ItemDrawNum"].ToString();
			dr.Close();
			return ItemDrawNum;
		}

		private string SearchItemName(SqlConnection conn, SqlTransaction tr,string ItemNum)
		{
			string ItemName = "";
			string 	str=@"Select ItemName From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@ItemNum", ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				ItemName = dr["ItemName"].ToString();
			dr.Close();
			return ItemName;
		}

		/// <summary>
		/// 보용품 입고시 원자재 창고 출고 이력원장 기록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void RMS_MTAddItemHistoy(SqlConnection conn, SqlTransaction tr, int count)
		{
			string StoreName = "원자재창고";
			//원자재창고에 출고되는 이력을 기록한다.
			string 	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@StoreName,0,@Quantity,@Date,'보용품입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@itemnum", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@itemdrawnum",m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);
			comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text));
			comm.Parameters.Add("@ProcessCode", m_grid.Rows[count].Cells.FromKey("ProcessCode").Text);
			comm.Parameters.Add("@ProcessName",m_grid.Rows[count].Cells.FromKey("ProcessName").Text);
			comm.Parameters.Add("@StoreName",StoreName);
			//comm.Parameters.Add("@Division",0);							//입출고구분(입고 1, 출고 0)
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[0].ToString()));						//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[4].ToString()).ToShortDateString());								//입출고일
			comm.Parameters.Add("@HistoryIndex",MaxAS_HT(conn,tr));									//원장번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		// <summary>
		/// 보용품 입고시 생산 창고 출고 이력원장 기록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void PS_MTAddItemHistoy(SqlConnection conn, SqlTransaction tr, int count)
		{
			string StoreName = "생산창고";
			//생산창고에 출고되는 이력을 기록한다.
			string 	str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@StoreName,0,@Quantity,@Date,'보용품입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;			
			comm.Parameters.Add("@itemnum", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@itemdrawnum",m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Text);
			comm.Parameters.Add("@itemname",m_grid.Rows[count].Cells.FromKey("ItemName").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Text));
			comm.Parameters.Add("@ProcessCode", m_grid.Rows[count].Cells.FromKey("ProcessCode").Text);
			comm.Parameters.Add("@ProcessName",m_grid.Rows[count].Cells.FromKey("ProcessName").Text);
			comm.Parameters.Add("@StoreName",StoreName);
			//comm.Parameters.Add("@Division",0);							//입출고구분(입고 1, 출고 0)
			comm.Parameters.Add("@Quantity",decimal.Parse(m_List[0].ToString()));						//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(m_List[4].ToString()).ToShortDateString());								//입출고일
			comm.Parameters.Add("@HistoryIndex",MaxAS_HT(conn,tr));									//원장번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}



		private void SubRMS_MTUpdate(SqlConnection con, SqlTransaction trans, string ItemNum, decimal Quantity)
		{
			int year = DateTime.Parse(m_List[4].ToString()).Year;
			int mon = DateTime.Parse(m_List[4].ToString()).Month;

			Table table = new Table(year,mon);

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction =trans;
			comm_cost.Parameters.Add("@itemnum",ItemNum);
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;	
			comm.Connection = con;

			comm.Parameters.Add("@quantity", Quantity);
			comm.Parameters.Add("@num", ItemNum);
			comm.Parameters.Add("@cost", Quantity * cost);
			comm.Parameters.Add("@year",year);
			
			
			
			comm.CommandText= table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = ItemNum;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


		/// <summary>
		/// 보용품입고후 원자재창고 출고수량 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void RMS_MTUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			

			int year = DateTime.Parse(m_List[4].ToString()).Year;
			int mon = DateTime.Parse(m_List[4].ToString()).Month;

			Table table = new Table(year,mon);

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction =tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = tr;	
			comm.Connection = conn;

			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14000000";
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString()) * cost;
			comm.Parameters.Add("@year",year);
			
			
			
			comm.CommandText= table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


		/// <summary>
		/// 보용품 창고 입고시 생산창고 출고수량 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void PS_MTPudate(SqlConnection conn, SqlTransaction tr, int count)
		{
			decimal m_cost = 0; //금액
			decimal m_progressrate = 0;//진척율
			string m_process = "";//공정코드
			
			int year = DateTime.Parse(m_List[4].ToString()).Year;
			int mon = DateTime.Parse(m_List[4].ToString()).Month;

			Table table = new Table(year,mon);
			
			
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction =tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
					
			if(dr_cost.Read())
				m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////

					 
			//////////////////////////////////////////
			// 품목의 진척율, 공정순서을 위해		//
			// 품목정보테이블에서					//
			// 기준단가를 가져와 계산한다.   -시작- //
			//////////////////////////////////////////
			string str_rate = "Select ProgressRate,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Transaction =tr;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();
			if(dr_rate.Read())
			{
				m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
				m_process = dr_rate["ProcessCode"].ToString();
			}
			dr_rate.Close();
			

			//////////////////////////////////////////
			// 품목의 진척율, 공정순서을 위해		//
			// 품목정보테이블에서					//
			// 기준단가를 가져와 계산한다.   -끝-   //
			//////////////////////////////////////////

			SqlCommand comm = new SqlCommand();
			comm.Transaction = tr;	
			comm.Connection = conn;
		
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();

			if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());

			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_process;
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_cost.ToString()) * decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_progressrate.ToString())/100;
			comm.Parameters.Add("@year",year);
			
					
			
			comm.CommandText= table.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
						
			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_process;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
			
		}


		/// <summary>
		/// 보용품 창고 입고
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="count"></param>
		private void AS_MTPudate(SqlConnection conn, SqlTransaction tr, int count)
		{
			decimal cost = 0; //금액

			//////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction =tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
					
			if(dr_cost.Read())
				cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			///
			string str = "Select Count(*) From AS_MT where RecodingState = 1 and [Year] = @year and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Count = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			
			//레코드가 존재하면 업데이트 존재하지 않으면 인서트-납품창고도 생성
			if(Count == 0)
			{
				str = @"Insert into AS_MT (ItemNum,ProcessSequenceNum, ProcessCode, [Year]) 
							values (@ItemNum, '99', '14009999', @year) ; Insert into DS_MT (ItemNum,ProcessSequenceNum, ProcessCode, [Year]) 
							values (@ItemNum, '99', '14009999', @year)";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
				//comm.Parameters.Add("@ProcessSequenceNum",m_grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Value.ToString());
				//comm.Parameters.Add("@ProcessCode",m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString());
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
			int year = DateTime.Parse(m_List[4].ToString()).Year;
			int mon = DateTime.Parse(m_List[4].ToString()).Month;


			Table table = new Table(year,mon);


			comm.CommandText = table.InAddItemTable();
			comm.Parameters.Add("@num",m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString());
			//comm.Parameters.Add("@code",m_grid.Rows[count].Cells.FromKey("ProcessCode").Value.ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(cost.ToString()) * decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@year",DateTime.Now.Year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}
		
		/// <summary>
		/// 매출원장 등록함수
		/// </summary>
		/// <param name="count"></param>
		private void SaleHistoryRegister(SqlConnection conn, SqlTransaction tr, int count)
		{

			string str= "Insert into S_HT (ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, OrderNum, OutStorehouseQuantity, SuitabilityQuantity, ApplyUnitCost, TotalCost, SupplementaryValueTaxRate,SaleDate,RegistrationPerson, RegistrationPersonID, RegistrationDate, OutStorehouseHistoryIndex, ReceivingOrderHistoryIndex) values ("+
				"@itemnum, @itemdrawnum,@itemname,@com,@businessNum,@OrderNum, @quantity,@suitabilityquantity, @applyunitcost, @totalcost, @tax, @SaleDate,"+
				"@Person, @PersonID, @Date,@index, @index1)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("CompanyName").Value.ToString();//거래처명
			comm.Parameters.Add("@businessNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//사업자등록번호
			if(m_grid.Rows[count].Cells.FromKey("OrderNum").Value == null)
				comm.Parameters.Add("@OrderNum",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@OrderNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("OrderNum").Value.ToString();//발주번호
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//출고수량
			comm.Parameters.Add("@suitabilityquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//합격수량
			comm.Parameters.Add("@applyunitcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[12].ToString());//적용단가  
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString());//총금액
			comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_List[11].ToString());//부가세율
			comm.Parameters.Add("@SaleDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_List[13].ToString());//매출일자
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장번호
			if(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value == null)
				comm.Parameters.Add("@index1",SqlDbType.Int).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@index1",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 수주원장 수량변경
		/// </summary>
		/// <param name="count"></param>
		private void SaleReceivingOrderUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			//수주원장에는 누적시켜야 하므로 일단 
			//수주원장에 있는 수량들을 가지고 와야 한다.
			decimal quantity1=0;//합격수량
			decimal quantity2=0;//미검수량

			string str_RO= "Select * From RO_HT Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
			comm_RO.Transaction = tr;
			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			SqlDataReader dr = comm_RO.ExecuteReader();
			while(dr.Read())
			{
				quantity1 = decimal.Parse(dr["SuitabilityQuantity"].ToString());
				quantity2 = decimal.Parse(dr["UnInspectionQuantity"].ToString());
			}
			dr.Close();
			comm_RO.Parameters.Clear();
			


			//실제 수주원장에 수량 업데이트
			//잔량이 0 이하면 진행상태를 완료로 바꾼다.

			string str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity + @quantity1, UnInspectionQuantity = UnInspectionQuantity - @quantity2, RemainderQuantity= RemainderQuantity - @quantity1 Where ReceivingOrderHistoryIndex = @index";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//매출원장 합격수량
			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//수주원장 미검수량 - 매출원장 출고수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			// 업데이트 한 뒤 잔량이 0이하이면 진행상태를 완료로 바꾼다.
			decimal quantity = 0;
			str = "Select RemainderQuantity From RO_HT where ReceivingOrderHistoryIndex = @index";
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			SqlDataReader dr_RO = comm.ExecuteReader();
			while(dr_RO.Read())
			{
				quantity = decimal.Parse(dr_RO["RemainderQuantity"].ToString());
			}
			dr_RO.Close();
			
			if(quantity <= 0)
			{
				str = "Update RO_HT Set ProgressCondition = '완료' Where ReceivingOrderHistoryIndex = @index";
				comm.CommandText = str;
				comm.ExecuteNonQuery();
			}
			comm.Parameters.Clear();


//			string str;
//			if((quantity3-decimal.Parse(m_List[0].ToString())) <= 0)
//				str = "Update RO_HT Set ProgressCondition = '완료', SuitabilityQuantity = SuitabilityQuantity + @quantity1, UnInspectionQuantity = UnInspectionQuantity - @quantity2, RemainderQuantity= 0 Where ReceivingOrderHistoryIndex = @index";
//			else
//				str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity + @quantity1, UnInspectionQuantity = UnInspectionQuantity - @quantity2, RemainderQuantity= RemainderQuantity - @quantity1 Where ReceivingOrderHistoryIndex = @index";
//
//			SqlCommand comm = new SqlCommand(str,conn);
//			comm.Transaction = tr;
//			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//매출원장 합격수량
//			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//수주원장 미검수량 - 매출원장 출고수량
//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
//			comm.ExecuteNonQuery();
//			comm.Parameters.Clear();




		}




		/// <summary>
		/// 출고수량 수량 및 내용변경
		/// </summary>
		/// <param name="count"></param>
		private void SaleOutStorehouseUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			//실제 출고원장에 수량및 내용 업데이트
			//검사수량(합격수량+부적합수량)이 출고수량과 동일하면 진행상태 완료로 변경
			string str;
			if(decimal.Parse(m_List[0].ToString()) + decimal.Parse(m_List[2].ToString()) == decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString()))
			{				
				str = "Update OS_HT Set UnInspectionQuantity = @quantity1, SuitabilityQuantity = @quantity2, UnSuitabilityQuantity = @quantity3, "+
					"UnSuitabilityCauseCode = @causecode, UnSuitabilityCauseMeaning = @cause, UnSuitabilityStatusCode = @statuscode, UnSuitabilityStatusMeaning = @status, "+
					"UnSuitabilityDetailMeaning = @detail, InspectionDecisionCode = @decisioncode, InspectionDecisionMeaning = @decision,UnSuitabilityCost = @uncost, ProgressCondition = '완료' "+
					"Where OutStorehouseHistoryIndex = @index";
			}
			else
			{
				str = "Update OS_HT Set UnInspectionQuantity = @quantity1, SuitabilityQuantity = @quantity2, UnSuitabilityQuantity = @quantity3, "+
					"UnSuitabilityCauseCode = @causecode, UnSuitabilityCauseMeaning = @cause, UnSuitabilityStatusCode = @statuscode, UnSuitabilityStatusMeaning = @status, "+
					"UnSuitabilityDetailMeaning = @detail, InspectionDecisionCode = @decisioncode, InspectionDecisionMeaning = @decision,UnSuitabilityCost = @uncost "+
					"Where OutStorehouseHistoryIndex = @index";
			}

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) - (decimal.Parse(m_List[0].ToString())+decimal.Parse(m_List[2].ToString()));//출고수량 - (합격수량+부적합수량)
			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//합격수량
			comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());//부적합수량
			comm.Parameters.Add("@causecode",SqlDbType.VarChar).Value = m_List[6].ToString();//부적합원인코드
			comm.Parameters.Add("@cause",SqlDbType.VarChar).Value = m_List[7].ToString();//부적합원인
			comm.Parameters.Add("@statuscode",SqlDbType.VarChar).Value = m_List[8].ToString();//부적합현상코드
			comm.Parameters.Add("@status",SqlDbType.VarChar).Value = m_List[9].ToString();//부적합현상
			comm.Parameters.Add("@detail",SqlDbType.VarChar).Value = m_List[10].ToString();//부적합세부내용
			comm.Parameters.Add("@decisioncode",SqlDbType.VarChar).Value = m_List[4].ToString();//검사판정코드
			comm.Parameters.Add("@decision",SqlDbType.VarChar).Value = m_List[5].ToString();//검사판정
			comm.Parameters.Add("@uncost",SqlDbType.Decimal).Value = decimal.Parse(m_List[3].ToString());//부적합수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장번호

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 매출원장 등록후 납품창고에 출고수량 증가
		/// </summary>
		/// <param name="count"></param>
		private void S_HTTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			int year = DateTime.Parse(m_List[13].ToString()).Year;
			int mon = DateTime.Parse(m_List[13].ToString()).Month;

			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString())+decimal.Parse(m_List[2].ToString());//납품창고에는 합격이든 불합격이든 검사수량 전량을 떨어준다
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(m_List[0].ToString())+decimal.Parse(m_List[2].ToString())) * cost;
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutDeliveryTable();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 매출원장 등록후 매입매출테이블에 매출금액 증가
		/// </summary>
		/// <param name="count"></param>
		private void S_HTBuyingSaleTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			int year = DateTime.Parse(m_List[13].ToString()).Year;
			int mon = DateTime.Parse(m_List[13].ToString()).Month;


			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Text;
			//comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text)*decimal.Parse(m_List[0].ToString());//합격수량*적용단가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString());//총금액
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText = table.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();
		}

		
		/// <summary>
		/// 창고이동원장 등록
		/// </summary>
		/// <param name="count"></param>
		private void StorehouseMovingRegister(SqlConnection conn, SqlTransaction tr, int count)
		{
			string str = @"Insert into SM_HT (ItemNum, ItemDrawNum, ItemName, ProcessCode, 
					OriginalStorehouseName, BusinessStorehouseNum1, MovingStorehoseName, BusinessStorehouseNum2, MovingQuantity, 
					RegistrationPerson, RegistrationPersonID, RegistrationDate) 
					values(@itemNum, @itemDrawNum, @itemName, @processCode, @orignal, @num1, @move, @num2, @quantity,
					@Person, @PersonID, @date)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemDrawNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemName",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@processCode",SqlDbType.VarChar).Value = "14009999";
			comm.Parameters.Add("@orignal",SqlDbType.VarChar).Value = m_List[1].ToString();
			comm.Parameters.Add("@num1",SqlDbType.Int).Value = int.Parse(m_List[2].ToString());
			comm.Parameters.Add("@move",SqlDbType.VarChar).Value =  m_List[3].ToString();
			comm.Parameters.Add("@num2",SqlDbType.Int).Value = int.Parse(m_List[4].ToString());
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 영업창고 수량변경
		/// </summary>
		/// <param name="count"></param>
		private void SM_MTTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			
			int year = DateTime.Now.Year;
			int mon = DateTime.Now.Month;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			// 영업창고 출고수량 증가(원창고)
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString())*cost;
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[2].ToString());//원 창고번호
			comm.Parameters.Add("@year",year);


			Table table = new Table(year,mon);					
			
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			// 영업창고 입고수량 증가(이동창고)
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString())*cost;
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[4].ToString());//이동 창고번호
			comm.Parameters.Add("@year",year);
			
			comm.CommandText= table.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[2].ToString());//원 창고번호
				comm.Parameters.Add("@year" ,DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


















		


		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = m_Distinction.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
			{
				return true;
			}
			else if(year == int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim())) 
			{
				if(month < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}	
			
		}



		


		/// <summary>
		/// 검사품인지 무검사인지 판단하는 함수 
		/// 0이면 무검사,1이면 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool Check()
		{
			string aa = "0";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["CheckDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "True")
				return false;
			else
				return true;
		}


		/// <summary>
		/// 검사품인지 무검사인지 판단하는 함수 
		/// 0이면 무검사,1이면 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool Check(string itemnum)
		{
			string aa = "0";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["CheckDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "False")
				return false;//무검사
			else
				return true;
		}



		/// <summary>
		/// 품목의 자산분류가 무었인지 판단하는 함수
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private int Division()
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = alist[0].ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else //if(aa.ToString() =="팬텀")
				return 5;
		}

		private bool Last(SqlConnection conn, SqlTransaction tr, string itemnum, int sequence)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			string str  = "select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum > @num";
			comm.Parameters.Add("@item",itemnum);
			comm.Parameters.Add("@num",sequence);
			comm.CommandText = str;
			int count = int.Parse(comm.ExecuteScalar().ToString());
			if(count == 0)
				return false;
			else
				return true;
		}


		/// <summary>
		/// 생산의뢰등록시 자산분류가 팬텀인지 
		/// 파악하기위해 호출하는 함수
		/// 작업일보등록시 창고에 입출력을 위해
		/// 품목의 종류를 구분하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private int Division(string ItemNum)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else //if(aa.ToString() =="팬텀")
				return 5;
		}

		/// <summary>
		/// 마이너스 재고 허용여부
		/// </summary>
		/// <returns></returns>
		private bool MinusStore()
		{
			bool aa = true;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			string str = "Select MinusRowMaterialPermissionUsed From SCS_T";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = bool.Parse(dr["MinusRowMaterialPermissionUsed"].ToString());
			}
			dr.Close();
			conn.Close();

			return aa;
		}



		/// <summary>
		/// 제품출고시 매출등록 사용 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="UserID">사용자id</param>
		/// <param name="grid">제품출고 원장과 바인딩된 그리드</param>
		public Register(string RequestPage, string UserID, UltraWebGrid grid)
		{
			m_grid = grid;
			m_PageName = RequestPage;
			m_User = UserID;
			m_Distinction = "영업";

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_User.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();
			
			
		}



		public void SaleRegistration()
		{

			//int a = 0;	// 선택된 것이 등록되어지면 값이 변한다
			HttpContext hc = HttpContext.Current;

			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction trans = con.BeginTransaction();
			comm.Connection = con;
			comm.Transaction = trans;

			try
			{		
				
				OSSaleHistoryRegister(con, trans);										//매출원장 등록
				OSSaleOutStorehouseUpdate(con, trans);								//출고원장 수량및 내용변경
				OSSaleReceivingOrderUpdate(con, trans);									//수주원장 수량변경
				OSS_HTTableUpdate(con, trans);										//납품창고 수량감소
				OSS_HTBuyingSaleTableUpdate(con, trans);											//매출테이블 금액변경
				BOS_HTHistoryRegister(con, trans);										//매출원장 등록

				trans.Commit();
			}
			catch(Exception ee)
			{
				
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				trans.Rollback();
			}
			finally
			{
				con.Close();
			}
			
		}





		/// <summary>
		/// 매출원장 등록함수
		/// </summary>
		/// <param name="count"></param>
		private void OSSaleHistoryRegister(SqlConnection conn, SqlTransaction tr)
		{
			string str= "Insert into S_HT (ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, OutStorehouseQuantity, SuitabilityQuantity, ApplyUnitCost, TotalCost, SupplementaryValueTaxRate,SaleDate,RegistrationPerson, RegistrationPersonID, RegistrationDate, OutStorehouseHistoryIndex, ReceivingOrderHistoryIndex) values ("+
				"@itemnum, @itemdrawnum,@itemname,@com,@businessNum,@quantity,@suitabilityquantity, @applyunitcost, @totalcost, @tax, @SaleDate,"+
				"@Person, @PersonID, @Date,@index, @index1)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("CompanyName").Value.ToString();//거래처명
			comm.Parameters.Add("@businessNum",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//사업자등록번호
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//출고수량
			comm.Parameters.Add("@suitabilityquantity",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//출고수량
			comm.Parameters.Add("@applyunitcost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("ApplyUnitCost").Value.ToString());//출고수량  
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) * decimal.Parse(m_grid.Rows[0].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = 10;
			comm.Parameters.Add("@SaleDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Value.ToString());//매출일자
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_User.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장번호
			comm.Parameters.Add("@index1",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 수주원장 수량변경
		/// </summary>
		/// <param name="count"></param>
		private void OSSaleReceivingOrderUpdate(SqlConnection conn, SqlTransaction tr)
		{
			//수주원장에는 누적시켜야 하므로 일단 
			//수주원장에 있는 수량들을 가지고 와야 한다.
			decimal quantity1=0;//합격수량
			decimal quantity2=0;//미검수량

			string str_RO= "Select * From RO_HT Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
			comm_RO.Transaction = tr;
			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			SqlDataReader dr = comm_RO.ExecuteReader();
			while(dr.Read())
			{
				quantity1 = decimal.Parse(dr["SuitabilityQuantity"].ToString());
				quantity2 = decimal.Parse(dr["UnInspectionQuantity"].ToString());
			}
			dr.Close();
			comm_RO.Parameters.Clear();
			


			//실제 수주원장에 수량 업데이트
			//잔량이 0 이하면 진행상태를 완료로 바꾼다.

			string str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity + @quantity1, UnInspectionQuantity = UnInspectionQuantity - @quantity2, RemainderQuantity= RemainderQuantity - @quantity1 Where ReceivingOrderHistoryIndex = @index";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//매출원장 합격수량
			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//수주원장 미검수량 - 매출원장 출고수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			// 업데이트 한 뒤 잔량이 0이하이면 진행상태를 완료로 바꾼다.
			decimal quantity = 0;
			str = "Select RemainderQuantity From RO_HT where ReceivingOrderHistoryIndex = @index";
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			SqlDataReader dr_RO = comm.ExecuteReader();
			while(dr_RO.Read())
			{
				quantity = decimal.Parse(dr_RO["RemainderQuantity"].ToString());
			}
			dr_RO.Close();
			
			if(quantity <= 0)
			{
				str = "Update RO_HT Set ProgressCondition = '완료' Where ReceivingOrderHistoryIndex = @index";
				comm.CommandText = str;
				comm.ExecuteNonQuery();
			}
			comm.Parameters.Clear();
		}




		/// <summary>
		/// 출고수량 수량 및 내용변경
		/// </summary>
		/// <param name="count"></param>
		private void OSSaleOutStorehouseUpdate(SqlConnection conn, SqlTransaction tr)
		{
			//실제 출고원장에 수량및 내용 업데이트
			//검사수량(합격수량+부적합수량)이 출고수량과 동일하면 진행상태 완료로 변경
			string str = @"Update OS_HT Set UnInspectionQuantity = @quantity1, SuitabilityQuantity = @quantity2, UnSuitabilityQuantity = @quantity3, 
					ProgressCondition = '완료' Where OutStorehouseHistoryIndex = @index";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;//미검수량
			comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//합격수량
			comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0; //부적합수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장번호

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 매출원장 등록후 납품창고에 출고수량 증가
		/// </summary>
		/// <param name="count"></param>
		private void OSS_HTTableUpdate(SqlConnection conn, SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			int year = DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Text).Month;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//합격수량
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("ItemNum").Text;//품목번호
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Value.ToString()))* cost;
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutDeliveryTable();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 매출원장 등록후 매입매출테이블에 매출금액 증가
		/// </summary>
		/// <param name="count"></param>
		private void OSS_HTBuyingSaleTableUpdate(SqlConnection conn, SqlTransaction tr)
		{
			int year = DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Text).Month;

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_grid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Text) * decimal.Parse(m_grid.Rows[0].Cells.FromKey("ApplyUnitCost").Text);//총금액
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText = table.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();
		}

		private void BOS_HTHistoryRegister(SqlConnection con, SqlTransaction trans)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품',@CompanyName,@BusinessRegistrationNum, @Quantity,@UnitCost,'등록',@Date,@Person, @PersonID,'매출등록',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum",m_grid.Rows[0].Cells.FromKey("ItemNum").Text);									//품목번호
			comm.Parameters.Add("@itemdrawnum", m_grid.Rows[0].Cells.FromKey("ItemDrawNum").Text);								//도면번호
			comm.Parameters.Add("@itemname",m_grid.Rows[0].Cells.FromKey("ItemName").Text);									//품목명
			//comm.Parameters.Add("@ProcessSequenceNum",0);											//공정순서
			//comm.Parameters.Add("@ProcessCode","14000000");											//공정코드
			//comm.Parameters.Add("@ProcessName","소재");												//공정명
			comm.Parameters.Add("@CompanyName",m_grid.Rows[0].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",m_grid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text);
			comm.Parameters.Add("@Quantity",decimal.Parse(m_grid.Rows[0].Cells.FromKey("OutStorehouseQuantity").Text));					//입출고 수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_grid.Rows[0].Cells.FromKey("ApplyUnitCost").Text));
			comm.Parameters.Add("@Date",DateTime.Parse(m_grid.Rows[0].Cells.FromKey("OutStoreDate").Text));	//입출고일
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",MaxS_HT(con,trans));							//원장번호
			comm.ExecuteNonQuery();
		}

		private int MaxS_HT(SqlConnection con, SqlTransaction trans)
		{
			int index = 0;
			string str=@"Select isnull(SaleHistoryIndex,0) as SaleHistoryIndex From S_HT";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				index = int.Parse(dr["SaleHistoryIndex"].ToString()); 
			}
			dr.Close();

			if(index == 0)
				throw new Exception("매출원장에 등록된 내용이 없습니다!");
			
			return index;
			
		}
	}
}