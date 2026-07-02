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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// MasterInfoRecordRUD에 대한 요약 설명입니다.
	/// </summary>
	public class MasterInfoRecordRUD : System.Web.UI.Page
	{
		private string m_PageName;		//요청페이지 이름을  담는 필드
		private string m_ActionName;	//요청동작(버튼)의 종류를  담는 필드
		private string m_TableName;		// 넘어온 테이블명을 담는 필드
		private int m_PageInt ;			// 테이블 이름(행), 필드 이름(열)로 구성되는 2차원배열에서의 테이블 이름값

		private string m_Division;		// 기준정보 일괄입력과 구분하기 위한 필드(등록완료시 m_Division이 기준정보이면 완료메세지를 보내준다.
		private string m_UserID ;		//사용자 아이디 저장
		private string UserName;		
		
		
		ArrayList m_InputList = new ArrayList();		//요청페이지에서 입력되어진 값을 넘겨받아 저장하는 ArrayList
		


		
		/// <summary>
		/// 각테이블의 필드를 담는 배열
		/// </summary>
		string[][] m_FieldName = new string[20][] ;


		/// <summary>
		/// 각 테이블의 키 필드를 담는 배열
		/// </summary>
		string[][] m_KeyFieldName = new string[20][];


		/// <summary>
		/// 넘어온 테이블의 내용키필드값을 담는 배열
		/// </summary>
		string[][] m_KeyFieldValue = new string[20][];


		/// <summary>
		/// 등록수정삭제 생성자
		/// </summary>
		/// <param name="RequirePage">요청페이지</param>
		/// <param name="RequireAction">요청액션</param>
		/// <param name="TableName">테이블명</param>
		/// <param name="Idx">테이블 이름 인덱스</param>
		/// <param name="User">사용자ID</param>
		/// <param name="aaa">입력변수를 담은 리스트</param>
		/// <param name="Division">일괄입력과 개별입력 구분필드</param>
		public MasterInfoRecordRUD(string RequiredPage, string RequiredAction, string RequiredTableName, int Idx, string UserID, ArrayList aaa, string Division)
		{

			
			m_PageName = RequiredPage.ToString();
			m_ActionName = RequiredAction.ToString();
			m_TableName = RequiredTableName.ToString();
			m_PageInt = Idx;
			m_UserID = UserID.ToString();
			m_Division = Division.ToString();

			for(int i = 0; i < aaa.Count; i++)
			{
				if(aaa[i] == null)
					m_InputList.Add("");
				else
					m_InputList.Add(aaa[i]);
			}

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			
			UserName = Session["UserName"].ToString();


			// 거래처정보필드
			m_FieldName[0] = new string[36]
				{
					MasterTableField.ReceiveingOrderCompany,
					MasterTableField.OutSideOrderCompany,
					MasterTableField.BuyingCompany,
					MasterTableField.CostCompany,
					MasterTableField.CompanyName,
					MasterTableField.PresidentName,
					MasterTableField.BusinessRegistrationNum,
					MasterTableField.CorporationRegistrationNum,
					MasterTableField.BusinessCompanyAddress,
					MasterTableField.TaxBillAddress,
					MasterTableField.HomepageAddress,
					MasterTableField.BusinessClassification,
					MasterTableField.BusinessItem,
					MasterTableField.CurrentTradeState,
					MasterTableField.SupplementaryValueTax,
					MasterTableField.SupplementaryValueTaxRate,
					MasterTableField.TelephoneNum,
					MasterTableField.FaxNum,
					MasterTableField.TradeClassification1,
					MasterTableField.TradeClassification2,
					MasterTableField.TradeClassification3,
					MasterTableField.SaleStandardDate,
					MasterTableField.SaleStandardBillDate,
					MasterTableField.BillApprovalStandard,
					MasterTableField.FixPeriodCollectMoneyDate1,
					MasterTableField.FixPeriodCollectMoneyDate2,
					MasterTableField.CompanyPersonInCharge,
					MasterTableField.CompanyPersonInChargeEmail,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationDate,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.CompanyInfoIndex
				};
			// 거래처정보 내용키
			m_KeyFieldName[0] = new string[1]
				{
					MasterTableField.BusinessRegistrationNum,
			};


			//품목정보필드
			m_FieldName[1] = new string[59]
				{
					MasterTableField.ItemNum,						//품목번호
					MasterTableField.ItemDrawNum,					//도면번호
					MasterTableField.ItemName,						//품목명
					MasterTableField.PropertyClassification,		//자산분류
					MasterTableField.Unit,							//단위
					MasterTableField.Standard,						//규격
					MasterTableField.Texture,						//과세여부
					MasterTableField.SupplementaryValueTaxRate,		//부가세율
					MasterTableField.StockUnit,						//재고단위
					MasterTableField.BOMUnit,						//BOM단위
					MasterTableField.PurchaseUnit,					//구매단위
					MasterTableField.SaleUnit,						//판매단위
					MasterTableField.IOChackable,					//자재산출여부
					MasterTableField.StockManagable,				//재고관리여부
					MasterTableField.CheckDistinction,				//검사여부
					MasterTableField.OrderPlan,						//발주방침
					MasterTableField.ItemType,						//품목타입
					MasterTableField.MateralQuality,				//재질
					MasterTableField.ItemState,						//품목상태
					MasterTableField.Maker,							//메이커
					MasterTableField.ItemClassification1,			//분류1
					MasterTableField.ItemClassification2,			//분류2
					MasterTableField.ItemClassification3,			//분류3
					MasterTableField.ItemClassification4,			//분류4
					MasterTableField.Standard1,						//규격1
					MasterTableField.Unit1,							//단위1
					MasterTableField.Standard2,						//규격2
					MasterTableField.Unit2,							//단위2
					MasterTableField.Standard3,						//규격3
					MasterTableField.Unit3,							//단위3
					MasterTableField.Standard4,						//규격4
					MasterTableField.Unit4,							//단위4
					MasterTableField.ProductWeight,					//제품중량
					MasterTableField.MaterialWeight,				//소재중량
					MasterTableField.SupplyTerm,					//조달기간
					MasterTableField.CompleteLeadTime,				//완료리드타임
					MasterTableField.DomesticImportDistinction,		//내외자도면
					MasterTableField.SafetyStockQuantity,			//안전재고량
					MasterTableField.OrderIntervalQuantity,			//발주간격수량
					MasterTableField.MinOrderQuantity,				//최소발주량
					MasterTableField.StiffenDeep,					//경화깊이
					MasterTableField.ProductHeatTreatmentDescription,	//제품열처리사양
					MasterTableField.MaterialHeatTreatmentDescription,	//소재열처리사양
					MasterTableField.MaterialHeatTreatmentRequestDegree,//소재열처리요구정도
					MasterTableField.CuttingSpace,						//절단여유
					MasterTableField.TariffRate,						//관세율
					MasterTableField.StandardUnitCost,					//기준단가
					MasterTableField.MainPurchaseCompany,				//주구입처
					MasterTableField.MainOutSideOrderCompany,			//주외주처
					MasterTableField.MainSaleCompany,					//주판매처
					MasterTableField.ChargePerson,						//담당자
					MasterTableField.RecodingState,						//레코드상태
					MasterTableField.RegistrationPerson,				//등록자
					MasterTableField.RegistrationPersonID,				//등록자ID
					MasterTableField.RegistrationDate,					//등록일
					MasterTableField.UpdatingPerson,					//수정자
					MasterTableField.UpdatingPersonID,					//수정자ID
					MasterTableField.UpdatingDate,						//수정일
					MasterTableField.ItemInfoIndex						//품목인덱스
				};
			// 품목정보 내용키
			m_KeyFieldName[1] = new string[1]
				{
					MasterTableField.ItemNum
				};


			// 품목구성 정보 필드
			m_FieldName[2] =new string[19]
				{
					MasterTableField.ParentItemNum,					//모품번호
					MasterTableField.ChildItemNum,					//자품번호
					MasterTableField.NeedQuantityNumerator,			//소요량분자
					MasterTableField.NeedQuantityDenominator,		//소요량분모
					MasterTableField.ProcessManagement,				//공정관리
					MasterTableField.SubDivision,					//하위도면
					MasterTableField.SupplyDivision,				//조달방법
					MasterTableField.BOMUnit,						//Bom단위
					MasterTableField.BeginDate,						//적용일
					MasterTableField.EndDate,						//종료일
					MasterTableField.UpdateReason,					//변경사유
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.ItemOrganizationInfoIndex
				};
			// 품목구성정보 내용키
			m_KeyFieldName[2] = new string[2]
				{
					MasterTableField.ParentItemNum,
					MasterTableField.ChildItemNum
				};

			// WC정보필드
			m_FieldName[3] = new string[17]
				{
					MasterTableField.WCName,					//WC명
					MasterTableField.MainProcessCode,			//대표공정
					MasterTableField.RetentionStaff,			//
					MasterTableField.State,						//현상태
					MasterTableField.CapacityDistinction,
					MasterTableField.OperationTime,
					MasterTableField.ElectricCapacity,
					MasterTableField.UnitTimeUseCost,
					MasterTableField.Sorting,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.WCInfoIndex
				};
			// WC정보 내용키
			m_KeyFieldName[3] = new string[1]
				{
					MasterTableField.WCName
				};

			// 공정순서정보 필드
			m_FieldName[4] = new string[17]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.ProcessCode,
					MasterTableField.WorkDistinction,
					MasterTableField.WCName,
					MasterTableField.OutsideOrderRate,
					MasterTableField.ProgressRate,
					MasterTableField.LeadTime,
					MasterTableField.EtcText,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.ProcessSequenceInfoIndex

				};
			// 공정순서정보 내용키
			m_KeyFieldName[4] = new string[2]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum
				};
			
			// 설비정보 필드
			m_FieldName[5] = new string[42]
				{
					
					MasterTableField.EquipmentNum,			//설비번호
					MasterTableField.EquipmentName,			//설비명
					MasterTableField.EquipmentClassification,	//설비종류
					MasterTableField.Standard,					//규격
					MasterTableField.Capacity,
					MasterTableField.ElectricCapacity,			//전력량
					MasterTableField.UnitTimeUseCost,				//단위사간당사용료
					MasterTableField.Unit,							//단위
					MasterTableField.WCName,				//작업장명
					MasterTableField.InjectionStaffNum,		//투입인원
					MasterTableField.Location,				//위치
					MasterTableField.OccupancyArea,			//차지면적
					MasterTableField.BuyingDate,			//구입일
					MasterTableField.BuyingCost,			//구입비용
					MasterTableField.InstrumentNum,			//기기번호
					MasterTableField.EquipmentState,			//설비상태
					MasterTableField.CheckDate,				//검교정일
					MasterTableField.ValidPeriod,			//유효기간
					MasterTableField.CheckAgency,			//검교정기관
					MasterTableField.ReadyTime,				//가동준비시간
					MasterTableField.Cavity,				//
					MasterTableField.ValidityYear,			//내용년수
					MasterTableField.DesignShot,			//설계샷
					MasterTableField.FirstShot,				//초기샷
					MasterTableField.TotalShot,				//누계샷
					MasterTableField.WorkShot,				//작업샷
					MasterTableField.ManagePeriod1,			//관리주기1
					MasterTableField.ManageDate1,			//관리일1
					MasterTableField.ManagePeriod2,
					MasterTableField.ManageDate2,
					MasterTableField.ManagePeriod3,
					MasterTableField.ManageDate3,
					MasterTableField.ManagePeriod4,
					MasterTableField.ManageDate4,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.EquipmentInfoIndex
				};
			//설비정보 내용키
			m_KeyFieldName[5] = new string[1]
				{
					MasterTableField.EquipmentNum
				};

			//작업표준정보 필드
			m_FieldName[6] = new string[28]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.ProcessCode,
					MasterTableField.WCName,
					MasterTableField.PriorityOrder,
                    MasterTableField.MainWorkerID,
					MasterTableField.MainWorker,
					MasterTableField.ToolName1,
					MasterTableField.JigName1,
					MasterTableField.ToolName2,
					MasterTableField.JigName2,
					MasterTableField.ToolName3,
					MasterTableField.JigName3,
					MasterTableField.SetupTime,
					MasterTableField.RealProcessingTime,
					MasterTableField.SpaceTime,
					MasterTableField.StandardTime,
					MasterTableField.WaitTime,
					MasterTableField.LotSize,
					MasterTableField.Cavity,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.WorkStandardInfoIndex
				};
			//작업표준정보 내용키
			m_KeyFieldName[6] = new string[3]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.PriorityOrder

				};



			// 판매단가정보 필드
			m_FieldName[7] = new string[19]
				{
					MasterTableField.ItemNum,
					MasterTableField.UnitCostDistinction,
					MasterTableField.BusinessRegistrationNum,
					MasterTableField.BeginProcessCode,
					MasterTableField.EndProcessCode,
					MasterTableField.OrderRate,
					MasterTableField.StandardUnitCost,
					MasterTableField.DiscountUnitCost,
					MasterTableField.BeginDate,
					MasterTableField.EndDate,
					MasterTableField.UpdateReason,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.UnitCostInfoIndex
				};
			//판매단가정보 내용키
			m_KeyFieldName[7] = new string[3]
				{
					MasterTableField.ItemNum,
					MasterTableField.UnitCostDistinction,
					MasterTableField.BusinessRegistrationNum
				};


			// 구매단가 정보 필드
			m_FieldName[8] = new string[19]
			{
				MasterTableField.ItemNum,
				MasterTableField.UnitCostDistinction,
				MasterTableField.BusinessRegistrationNum,
				MasterTableField.BeginProcessCode,
				MasterTableField.EndProcessCode,
				MasterTableField.OrderRate,
				MasterTableField.StandardUnitCost,
				MasterTableField.DiscountUnitCost,
				MasterTableField.BeginDate,
				MasterTableField.EndDate,
				MasterTableField.UpdateReason,
				MasterTableField.RecodingState,
				MasterTableField.RegistrationPerson,
				MasterTableField.RegistrationPersonID,
				MasterTableField.RegistrationDate,
				MasterTableField.UpdatingPerson,
				MasterTableField.UpdatingPersonID,
				MasterTableField.UpdatingDate,
				MasterTableField.UnitCostInfoIndex
			};
			//구매단가정보 내용키
			m_KeyFieldName[8] = new string[3]
				{
					MasterTableField.ItemNum,
					MasterTableField.UnitCostDistinction,
					MasterTableField.BusinessRegistrationNum
				};


			// 외주단가 정보 필드
			m_FieldName[9] = new string[19]
			{
				MasterTableField.ItemNum,
				MasterTableField.UnitCostDistinction,
				MasterTableField.BusinessRegistrationNum,
				MasterTableField.BeginProcessCode,
				MasterTableField.EndProcessCode,
				MasterTableField.OrderRate,
				MasterTableField.StandardUnitCost,
				MasterTableField.DiscountUnitCost,
				MasterTableField.BeginDate,
				MasterTableField.EndDate,
				MasterTableField.UpdateReason,
				MasterTableField.RecodingState,
				MasterTableField.RegistrationPerson,
				MasterTableField.RegistrationPersonID,
				MasterTableField.RegistrationDate,
				MasterTableField.UpdatingPerson,
				MasterTableField.UpdatingPersonID,
				MasterTableField.UpdatingDate,
				MasterTableField.UnitCostInfoIndex
			};
			//외주단가 정보 내용키
			m_KeyFieldName[9] = new string[4]
				{
					MasterTableField.ItemNum,
					MasterTableField.BusinessRegistrationNum,
					MasterTableField.BeginProcessCode,
					MasterTableField.EndProcessCode,

				};


			// 사용자정보 필드
			m_FieldName[10] = new string[45]
				{


					MasterTableField.ID,							
					MasterTableField.Password,
					MasterTableField.Name,
					MasterTableField.PostCode,
					MasterTableField.Responsibility,
					MasterTableField.IdentificationNumber,
					MasterTableField.Telephone1,
					MasterTableField.Telephone2,
					MasterTableField.Address,					
					MasterTableField.EnterDate,					
					MasterTableField.HomepageCompetence,
					MasterTableField.StandardiInfoCompetence,
					MasterTableField.UserRank,
					MasterTableField.Picture,
					MasterTableField.EMail,
					MasterTableField.BusinessRegistrationNum,
					MasterTableField.WorkDiary,
					MasterTableField.ShotcutUserMenuPage1,
					MasterTableField.ShotcutUserMenuPage2,
					MasterTableField.ShotcutUserMenuPage3,
					MasterTableField.ShotcutUserMenuPage4,
					MasterTableField.ShotcutUserMenuPage5,
					MasterTableField.ShotcutUserMenuPage6,
					MasterTableField.ShotcutUserMenuPage7,
					MasterTableField.ShotcutUserMenuPage8,
					MasterTableField.ShotcutUserMenuPage9,
					MasterTableField.ShotcutUserMenuPage10,
					MasterTableField.ShotcutUserMenuPage11,
					MasterTableField.ShotcutUserMenuPage12,
					MasterTableField.ShotcutUserMenuPage13,
					MasterTableField.ShotcutUserMenuPage14,
					MasterTableField.ShotcutUserMenuPage15,
					MasterTableField.ShotcutUserMenuPage16,
					MasterTableField.ShotcutUserMenuPage17,
					MasterTableField.ShotcutUserMenuPage18,
					MasterTableField.ShotcutUserMenuPage19,
					MasterTableField.ShotcutUserMenuPage20,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.UserInfoIndex
				};
			//사용자정보 내용키
			m_KeyFieldName[10] = new string[1]
				{
					MasterTableField.ID
				};


			// 공용코드 필드
			m_FieldName[11] = new string[12]
				{
					MasterTableField.LargeClassificationCode,
					MasterTableField.LargeClassificationName,
					MasterTableField.SmallClassificationCode,
					MasterTableField.SmallClassificationName,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.PublicUseCodeIndex
				};
			//공용코드 내용키
			m_KeyFieldName[11] = new string[2]
				{
					MasterTableField.LargeClassificationCode,
					MasterTableField.SmallClassificationCode
				};

			// 생산달력 필드
			m_FieldName[12] = new string[8]
				{
					MasterTableField.WCName,
					MasterTableField.WC_Year,
					MasterTableField.WC_Month,
					MasterTableField.WC_Day,
					MasterTableField.WC_Time,
					MasterTableField.Content,
					MasterTableField.RecodingState,
					MasterTableField.ProductionCalendarInfoIndex
				};

			// 생산달력 내용키
			m_KeyFieldName[12] = new string[4]
				{
					MasterTableField.WCName,
					MasterTableField.WC_Year,
					MasterTableField.WC_Month,
					MasterTableField.WC_Day
				};
			
			// 생산달력 필드
			m_FieldName[13] = new string[8]
				{
					MasterTableField.WCName,
					MasterTableField.WC_Year,
					MasterTableField.WC_Month,
					MasterTableField.WC_Day,
					MasterTableField.WC_Time,
					MasterTableField.Content,
					MasterTableField.RecodingState,
					MasterTableField.ProductionCalendarInfoIndex
				};

			// 생산달력 내용키
			m_KeyFieldName[13] = new string[4]
				{
					MasterTableField.WCName,
					MasterTableField.WC_Year,
					MasterTableField.WC_Month,
					MasterTableField.WC_Day
				};

			//사업계획 필드
			m_FieldName[14] = new string[16]
				{
					MasterTableField.ItemNum,
					MasterTableField.PlanYear,
					MasterTableField.PlanMonth,
					MasterTableField.PlanDay,
					MasterTableField.PlanQuantity,
					MasterTableField.SaleUnitCost,
					MasterTableField.PlanTotalCost,	
					MasterTableField.BusinessRegistrationNum,				
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.BusinessPlanInfoIndex
				};
			//사업계획등록 내용키
			m_KeyFieldName[14] = new string[5]
				{
					MasterTableField.ItemNum,
					MasterTableField.PlanYear,
					MasterTableField.PlanMonth,
					MasterTableField.PlanDay,
					MasterTableField.BusinessRegistrationNum
					
				};
				
			
			


			// 실행계획 필드
			m_FieldName[15] = new string[14]
				{
					MasterTableField.ItemNum,
					MasterTableField.PlanDate,
					MasterTableField.PlanQuantity,
					MasterTableField.SaleUnitCost,
					MasterTableField.PlanTotalCost,
					MasterTableField.State,
					MasterTableField.RecodingState,					
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.ExecutionPlanInfoIndex
				};
			//실행계획 내용키
			m_KeyFieldName[15] = new string[2]
				{
					MasterTableField.ItemNum,
					MasterTableField.PlanDate
				};


			// 월마감 필드
			m_FieldName[16] = new string[9]
				{
					MasterTableField.AffairDistinction,
					MasterTableField.ClosingYear,
					MasterTableField.ClosingMonth,
					MasterTableField.ClosingPerson,
					MasterTableField.ClosingPersonID,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.MonthClosingInfoIndex
				};
			//월마감 내용키
			m_KeyFieldName[16] = new string[1]
				{
					MasterTableField.AffairDistinction
				};



			// Real품목구성 정보 필드
			m_FieldName[17] =new string[19]
				{
					MasterTableField.ParentItemNum,					//모품번호
					MasterTableField.ChildItemNum,					//자품번호
					MasterTableField.NeedQuantityNumerator,			//소요량분자
					MasterTableField.NeedQuantityDenominator,		//소요량분모
					MasterTableField.ProcessManagement,				//공정관리
					MasterTableField.SubDivision,					//하위도면
					MasterTableField.SupplyDivision,				//조달방법
					MasterTableField.BOMUnit,						//Bom단위
					MasterTableField.BeginDate,						//적용일
					MasterTableField.EndDate,						//종료일
					MasterTableField.UpdateReason,					//변경사유
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.RealItemOrganizationInfoIndex
				};
			// Real품목구성정보 내용키
			m_KeyFieldName[17] = new string[2]
				{
					MasterTableField.ParentItemNum,
					MasterTableField.ChildItemNum
				};




			// Real공정순서정보 필드
			m_FieldName[18] = new string[17]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.ProcessCode,
					MasterTableField.WorkDistinction,
					MasterTableField.WCName,
					MasterTableField.OutsideOrderRate,
					MasterTableField.ProgressRate,
					MasterTableField.LeadTime,
					MasterTableField.EtcText,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.RealProcessSequenceInfoIndex

				};
			// Real공정순서정보 내용키
			m_KeyFieldName[18] = new string[2]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum
				};

			//Real작업표준정보
			m_FieldName[19] = new string[28]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.ProcessCode,
					MasterTableField.WCName,
					MasterTableField.PriorityOrder,
					MasterTableField.MainWorkerID,
					MasterTableField.MainWorker,
					MasterTableField.ToolName1,
					MasterTableField.JigName1,
					MasterTableField.ToolName2,
					MasterTableField.JigName2,
					MasterTableField.ToolName3,
					MasterTableField.JigName3,
					MasterTableField.SetupTime,
					MasterTableField.RealProcessingTime,
					MasterTableField.SpaceTime,
					MasterTableField.StandardTime,
					MasterTableField.WaitTime,
					MasterTableField.LotSize,
					MasterTableField.Cavity,
					MasterTableField.RecodingState,
					MasterTableField.RegistrationPerson,
					MasterTableField.RegistrationPersonID,
					MasterTableField.RegistrationDate,
					MasterTableField.UpdatingPerson,
					MasterTableField.UpdatingPersonID,
					MasterTableField.UpdatingDate,
					MasterTableField.RealWorkStandardInfoIndex
				};
			//Real작업표준정보 내용키
			m_KeyFieldName[19] = new string[3]
				{
					MasterTableField.ItemNum,
					MasterTableField.ProcessSequenceNum,
					MasterTableField.PriorityOrder

				};
				

			



			//품목 테이블 내용키
			m_KeyFieldValue[1] = new string[1]
				{
					m_InputList[0].ToString()
				};

			//품목구성테이블 내용키
			m_KeyFieldValue[2] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString()

				};

			//WC정보테이블 내용키
			m_KeyFieldValue[3] = new string[1]
				{
					m_InputList[0].ToString()
				};

			


			//설비정보테이블 내용키
			m_KeyFieldValue[5] = new string[1]
				{
					m_InputList[0].ToString()

				};

		

			//판매단가테이블 내용키
			m_KeyFieldValue[7] = new string[3]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[2].ToString()

				};

			//구매단가테이블 내용키
			m_KeyFieldValue[8] = new string[3]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[2].ToString()

				};

	
			//사용자정보테이블 내용키
			m_KeyFieldValue[10] = new string[1]
				{
					m_InputList[0].ToString()
				};

			//공용코드테이블 내용키
			m_KeyFieldValue[11] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[2].ToString()
				};

			// 생산달력테이블 내용키
			m_KeyFieldValue[12] = new string[4]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[2].ToString(),
					m_InputList[3].ToString()
				};

			// 생산달력테이블 내용키
			m_KeyFieldValue[13] = new string[4]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[2].ToString(),
					m_InputList[3].ToString()
				};

			

			//실행계획테이블 내용키
			m_KeyFieldValue[15] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
				};

			//월마감테이블 내용키
			m_KeyFieldValue[16] = new string[1]
				{
					m_InputList[0].ToString()
				};
			//Real품목구성테이블 내용키
			m_KeyFieldValue[17] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString()

				};



			if(m_InputList.Count >4)
			{
				//공정순서테이블 내용키
				m_KeyFieldValue[4] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString()

				};

				//작업표준테이블 내용키
				m_KeyFieldValue[6] = new string[3]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[4].ToString()
				};

				//외주단가테이블 내용키
				m_KeyFieldValue[9] = new string[4]
				{
					m_InputList[0].ToString(),
					m_InputList[2].ToString(),
					m_InputList[3].ToString(),
					m_InputList[4].ToString()

				};

				//Real공정순서테이블 내용키
				m_KeyFieldValue[18] = new string[2]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString()

				};

				//Real작업표준테이블 내용키
				m_KeyFieldValue[19] = new string[3]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[4].ToString()
				};
			}


			if(m_InputList.Count > 6)
			{
				//거래처 테이블내용키
				m_KeyFieldValue[0] = new string[1]
				{
					m_InputList[6].ToString()
				};
			}
			if(m_InputList.Count > 7)
			{
				//사업계획테이블 내용키
				m_KeyFieldValue[14] = new string[5]
				{
					m_InputList[0].ToString(),
					m_InputList[1].ToString(),
					m_InputList[2].ToString(),
					m_InputList[3].ToString(),
					m_InputList[7].ToString()
					
				};
			}

								 
		}


		public void ActivateRUD()
		{
			if(ValidateUserAuthority() == false)	//사용권한이 있는지 확인
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('권한이 없습니다.');");
				hc.Response.Write("</script>");
				return;
			}

			if(InspectExistingRecord() == false )	//레코드가 존재하는지 확인(레코드가 없으면 등록가능)
			{										//단순히 내용키가 동일한것이 있는지만 확인한다
				switch(m_ActionName)				//ValidateFieldValue() 함수에서는 인덱스값과 내용키가
				{									//동일한 레코드를 확인해준다.
					case "Registration":
						RegisterRecord();			// 레코드 등록 함수 호출
						break;
					default :	
						HttpContext hc = HttpContext.Current;
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('해당 레코드가 없습니다');");
						hc.Response.Write("</script>");
						break;
				}
			}				
			else	// 레코드가 있으면 수정, 삭제 가능
			{
				switch(m_ActionName)
				{
					case "Update":					
						UpdateRecord();
						break;
					case "Delete":					
						DeleteRecord();
						break;
					case "Registration":
						HttpContext hc = HttpContext.Current;
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('이미 등록한 항목입니다.');");
						hc.Response.Write("</script>");
						break;
				}
			}				
			return;
		}




		/// <summary>
		/// 등록권한이 있는 사용자인지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool ValidateUserAuthority()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select count(*) From UI_MT Where RecodingState = 1 and StandardiInfoCompetence = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_UserID.ToString();

			

			int ExistingRecord = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			if(ExistingRecord == 0)
				return false;				
			else
				return true;

			
		}
		
		/// <summary>
		/// 레코드가 존재하는지 여부를 확인하는 함수
		/// 단순히 내용키가 동일한것이 있는지만 확인한다
		/// ValidateFieldValue() 함수에서는 인덱스값과 내용키가
		///	동일한 레코드를 확인해준다.		 
		/// </summary>
		/// <returns></returns>
		private bool InspectExistingRecord()
		{	
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
		
			string comString = "Select count(*) From " + m_TableName.ToString() + " Where RecodingState = 1 and ";
			for(int i=0 ; i < m_KeyFieldName[m_PageInt].Length ; i++)
			{
				comString += m_KeyFieldName[m_PageInt][i] + " = '" 
					+ m_KeyFieldValue[m_PageInt][i]+"'" ;
				if(i < m_KeyFieldName[m_PageInt].Length - 1)
					comString += " AND " ;
			}

			SqlCommand comm = new SqlCommand(comString,conn);
			int ExistingRecord = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			if(ExistingRecord == 0)
				return false;				
			else
				return true;
		}
		

		/// <summary>
		/// 등록메소드
		/// </summary>
		private void RegisterRecord()
		{
			if(ValidateFieldValue() == true)		// 유효성 검사 함수(넘어오는 입력갑들의 유효성을 검사)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlTransaction tr = conn.BeginTransaction();
				try
				{
					MainTableRegistration(conn,tr);			// 메인테이블에 현재의 레코드를 등록
					RelatedTableRegistration(conn,tr);			// 관련테이블 등록하는 함수 호출
					if(m_Division.ToString() == "낱개입력")
					{
						HttpContext hc = HttpContext.Current;
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("window.status='등록이 완료되었습니다!';");
						//hc.Response.Write("alert('등록이 완료되었습니다.');");
						hc.Response.Write("</script>");
					}
					tr.Commit();
				}
				catch(Exception ee)
				{
					HttpContext hc = HttpContext.Current;
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
		}


		/// <summary>
		/// 유효성 검사 함수
		/// 논리적 이상유무를 판단해주는 함수로 사용한다.
		/// 요청페이지마다 각자 틀리므로 전부 만들어야 한다.
		/// </summary>
		/// <returns></returns>
		private bool ValidateFieldValue()
		{
			if(m_ActionName.ToString() == "Registration" || m_ActionName.ToString() == "Update" )
			{
				switch(m_PageName)
				{
					case "CompanyInfo":
						return CompanyInfoValidateField();				// 거래처정보 입력,수정시 논리적으로 이상이
																		// 있는 항목들에 대해 체크한다
					
					case "ItemInfo":									// 품목정보 입력시 이상유무에 대한 체크
						return ItemInfoValidateField();	
						
					case "ItemOrganizationInfo":						// 품목구성 입력시 이상유무에 대한체크
						return ItemOrganizationInfoValidateField();	

					case "WCInfo":										// 작업장정보
						return WCInfoValidateField();		

					case "ProcessSequenceInfo":							// 공정순서정보
						return ProcessSequenceInfoValidateField();

					case "EquipmentInfo":								// 설비정보
						return EquipmentInfoValidateField();

					case "WorkStandardInfo":							// 작업표준정보
						return WorkStandardInfoValidateField();

					case "SaleUnitCodeInfo":							// 판매단가정보
						return SaleUnitCodeInfoValidateField();

					case "BuyingUnitCodeInfo":							// 구매단가
						return BuyingUnitCodeInfoValidateField();

					case "OutSideOrderUnitCodeInfo":					// 외주단가
						return OutSideOrderUnitCodeInfoValidateField();

					case "UserInfo":									// 사용자정보
						return UserInfoValidateField();

					case "PublicUseCode":								// 공용코드
						return PublicUseCodeValidateField();

					case "StandardProductionCalendarInfo":				// 기본생산달력
						return ProductionCalendarInfoValidateField();

					case "WCProductionCalendarInfo":					// WC별 생산달력
						return ProductionCalendarInfoValidateField();

					case "BusinessPlanInfo":							// 사업계획
						return BusinessPlanInfoValidateField();

					case "ExecutionPlanInfo":							// 실행계획
						return ExecutionPlanInfoValidateField();

					case "MonthlyClosingInfo":							// 월마감
						return MonthlyClosingInfoValidateField();

					case "RealItemOrganizationInfo":						// 진품목구성 입력시 이상유무에 대한체크
						return ItemOrganizationInfoValidateField();	

					case "RealProcessSequenceInfo":							// 진공정순서정보
						return ProcessSequenceInfoValidateField();

					case "RealWorkStandardInfo":							// 진작업표준정보
						return WorkStandardInfoValidateField();
					default:
						return true;
						
				}
			}
			else
				return true;
		}



		/// <summary>
		/// 거래처정보 테이블의 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool CompanyInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{

					if(m_InputList[0].ToString() =="0" && m_InputList[1].ToString()=="0" && m_InputList[2].ToString()=="0" && m_InputList[3].ToString()=="0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('거래처구분을 선택해주세요');");
						hc.Response.Write("</script>");

						return false;
					}
					else if(m_InputList[4].ToString() == "" || m_InputList[5].ToString() =="" || m_InputList[6].ToString() == "" || 
						m_InputList[8].ToString().Trim() == "" || m_InputList[9].ToString().Trim() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");

						return false;
					}
					else
						return true;
				}
				case "Update":
				{
					//	내용키의 변화가 있는지 확인해서 있으면 오류처리를 한다.
					//	당연히 위의 내용도 체크를 해야 한다.
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() =="0" && m_InputList[1].ToString()=="0" && m_InputList[2].ToString()=="0" && m_InputList[3].ToString()=="0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('거래처 구분을 선택해주세요');");
						hc.Response.Write("</script>");

						return false;
					}
					else if(m_InputList[4].ToString() == "" || m_InputList[5].ToString() =="" || m_InputList[6].ToString() == "" || 
						m_InputList[8].ToString().Trim() == "" || m_InputList[9].ToString().Trim() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");

						return false;
					}
					else
						return true;
				}
				default :
					return true;
					
			}
		}

		/// <summary>
		/// 품목정보 테이블의 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool ItemInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "" || m_InputList[20].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					else
					{
						SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
						conn.Open();
						string str = "Select count(*) From II_MT Where RecodingState = 1 and ItemDrawNum = '"+m_InputList[1].ToString()+"'";
						SqlCommand comm = new SqlCommand(str,conn);
						int Exist = int.Parse(comm.ExecuteScalar().ToString());
						conn.Close();
						if(Exist != 0)
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('동일한 도면번호가 존재합니다!');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "" || m_InputList[20].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					else
					{
						SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
						conn.Open();
						string str = "Select count(*) From II_MT Where RecodingState = 1 and ItemDrawNum = '"+m_InputList[1].ToString()+"' and ItemNum != '"+m_InputList[0].ToString()+"'";
						SqlCommand comm = new SqlCommand(str,conn);
						int Exist = int.Parse(comm.ExecuteScalar().ToString());
						//int Exist = comm.ExecuteNonQuery();
						conn.Close();
						if(Exist != 0)
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('동일한 도면번호가 존재합니다!');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					return true;
				}
				default :
					return true;
			}
			
		}

		
        /// <summary>
        /// 품목구성정보 테이블 유효성검사
        /// </summary>
        /// <returns></returns>
		private bool ItemOrganizationInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString().Trim() == m_InputList[1].ToString().Trim())
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('모품목과 자품목이 동일합니다!');");
						hc.Response.Write("</script>");
						return false;
					}
					else if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "")
					//	m_InputList[4].ToString() == "" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "" )//|| m_InputList[7].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "" ||
						m_InputList[8].ToString() == ""  )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 작업장정보 테이블의 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool WCInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;
			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[5].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[5].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 공정순서 테이블의 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool ProcessSequenceInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[3].ToString() == "외주")
					{
						if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0")
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('필수항목을 입력해주세요');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					else
					{
						if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "")
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('필수항목을 입력해주세요');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[3].ToString() == "외주")
					{
						if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0")
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('필수항목을 입력해주세요');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					else
					{
						if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "")
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('필수항목을 입력해주세요');");
							hc.Response.Write("</script>");
							return false;
						}
					}
					
					return true;
				}
				default :
					return true;
			}
			
		}

		

		/// <summary>
		/// 작업표준 테이블의 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool WorkStandardInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "0" || m_InputList[4].ToString().Trim() == "" || m_InputList[5].ToString() == "" || m_InputList[16].ToString() == "" || m_InputList[16].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[3].ToString() == "0" || m_InputList[4].ToString() == "0" || m_InputList[4].ToString().Trim() == "" || m_InputList[5].ToString() == "" || m_InputList[16].ToString() == "" || m_InputList[16].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
			
		}


		/// <summary>
		/// 설비정보 유효성 검사  WorkStandardInfoValidateField
		/// </summary>
		/// <returns></returns>
		private bool EquipmentInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[8].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "0" || m_InputList[8].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 판매단가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool SaleUnitCodeInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}

		/// <summary>
		/// 구매단가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool BuyingUnitCodeInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" ||  m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 외주단가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool OutSideOrderUnitCodeInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					//if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "" || m_InputList[4].ToString() == "" || m_InputList[5].ToString() == "0" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "" )
					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "" || m_InputList[4].ToString() == "" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					//if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "" || m_InputList[4].ToString() == ""|| m_InputList[5].ToString() == "0" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "")
					if(m_InputList[0].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[3].ToString() == "" || m_InputList[4].ToString() == ""|| m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}
		


		/// <summary>
		/// 사용자 정보 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool UserInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[6].ToString() == "" || m_InputList[12].ToString() == "0" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "" || m_InputList[2].ToString() == "" || m_InputList[6].ToString() == "" || m_InputList[12].ToString() == "0" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}				
					return true;
				}
				default :
					return true;
			}

		}

		/// <summary>
		/// 공용코드 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool PublicUseCodeValidateField()
		{
			HttpContext hc = HttpContext.Current;
			if(m_InputList[3].ToString() == "")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('소분류명을 입력해주세요');");
				hc.Response.Write("</script>");
				return false;
			}
					
			return true;
		}


		/// <summary>
		/// 생산달력테이블 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ProductionCalendarInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[4].ToString().Trim() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[4].ToString().Trim() == "")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}

		}


		


		/// <summary>
		/// 사업계획 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool BusinessPlanInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "0" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0" || m_InputList[7].ToString() == "" || Convert.IsDBNull(m_InputList[7].ToString()) ||
						m_InputList[4].ToString() == "0" || m_InputList[4].ToString() == "" || m_InputList[5].ToString() == "0" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "0" || m_InputList[6].ToString() == "" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString() == "0" || m_InputList[2].ToString() == "0" || m_InputList[3].ToString() == "0" || m_InputList[7].ToString() == "" || Convert.IsDBNull(m_InputList[7].ToString()) ||
						m_InputList[4].ToString() == "0" || m_InputList[4].ToString() == "" || m_InputList[5].ToString() == "0" || m_InputList[5].ToString() == "" || m_InputList[6].ToString() == "0" || m_InputList[6].ToString() == "" )
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}

		}


		/// <summary>
		/// 실행계획 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ExecutionPlanInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Registration":
				{
					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString().Trim() == "" || m_InputList[1].ToString().Length > 10 || m_InputList[2].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
					
					return true;
				}
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[0].ToString() == "" || m_InputList[1].ToString().Trim() == "" || m_InputList[1].ToString().Length > 10 || m_InputList[2].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 월마감 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool MonthlyClosingInfoValidateField()
		{
			HttpContext hc = HttpContext.Current;

			switch(m_ActionName)
			{
				case "Update":
				{
					if(ExistingRecord() == false)
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('수정할수 없는 항목이 있습니다!');");
						hc.Response.Write("</script>");
						return false;
					}

					if(m_InputList[1].ToString() == "0" || m_InputList[2].ToString() == "0")
					{
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('필수항목을 입력해주세요');");
						hc.Response.Write("</script>");
						return false;
					}
										
					return true;
				}
				default :
					return true;
			}
		}


		/// <summary>
		/// 내용키와 인덱스가 동일한지 파악하는 함수
		/// </summary>
		/// <returns></returns>
		private bool ExistingRecord()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			string comString = "Select count(*) From " + m_TableName.ToString() + " Where RecodingState = 1 and "+m_FieldName[m_PageInt][m_FieldName[m_PageInt].Length - 1].ToString()+" = " + m_InputList[m_InputList.Count-1] + " and ";
			for(int i=0 ; i < m_KeyFieldName[m_PageInt].Length ; i++)
			{
				comString += m_KeyFieldName[m_PageInt][i] + " = '" 
					+ m_KeyFieldValue[m_PageInt][i]+"'" ;
				if(i < m_KeyFieldName[m_PageInt].Length - 1)
					comString += " AND " ;
			}
		
			SqlCommand comm = new SqlCommand(comString,conn);
			int ExistingRecord = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			
			if(ExistingRecord == 0)
				return false;				
			else
				return true;
		}




		/// <summary>
		/// 인서트함수
		/// </summary>
		private void MainTableRegistration(SqlConnection con, SqlTransaction trans)
		{
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			if(m_PageName.ToString() == "WCProductionCalendarInfo" || m_PageName.ToString() == "StandardProductionCalendarInfo")
			{
				str = "Insert into "+m_TableName.ToString()+ "(";
				for(int i =0 ; i< m_InputList.Count;i++)
				{
					str += m_FieldName[m_PageInt][i].ToString() + ", ";
				}
				str += " RecodingState) values('";


				for(int i =0 ; i< m_InputList.Count;i++)
				{
					str += m_InputList[i].ToString() + "', '";
				}
				str += "1')";
			}
			else
			{
				str = "Insert into "+m_TableName.ToString()+ "(";
				for(int i =0 ; i< m_InputList.Count;i++)
				{
					str += m_FieldName[m_PageInt][i].ToString() + ", ";
				}
				str += " RecodingState, RegistrationDate, RegistrationPerson,RegistrationPersonID) values('";


				for(int i =0 ; i< m_InputList.Count;i++)
				{
					str += m_InputList[i].ToString() + "', '";
				}
				str += "1', @Date, @Person,@PersonID)";

				comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();
				comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = UserName.ToString();
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}






		/// <summary>
		/// 관련테이블 레코드 생성함수
		/// </summary>
		private void RelatedTableRegistration(SqlConnection con, SqlTransaction trans)
		{

			switch(m_PageName)
			{
				case "CompanyInfo":
					BuyingSaleInfoTable(con,trans);					// 매입매출테이블을 생성하는 함수를 호출한다.
					break;
				case "ItemInfo":
					if(m_InputList[3].ToString() == "원자재")			//자산분류가 원자재이면 원자재창고와 외주창고에 입주시킨다.
					{
						RowMetarialStorehouseTable(con,trans);			// 원자재창고테이블을 생성하는 함수 호출
						//RowOutSideStorehouseTable(con,trans);				// 외주창고테이블을 생성하는 함수 호출
					}
					else if(m_InputList[3].ToString() == "제품"|| m_InputList[3].ToString() == "상품")
					{
						BusinessStorehouse(con,trans);				// 영업창고
						DeliveryStorehouse(con,trans);				// 납품창고
						if(m_InputList[3].ToString() == "상품")
							ProductStorehouseTable(con,trans);				//생산창고 테이블을 생성하는 함수 호출
					}
					break;
				case "ItemOrganizationInfo":
					// 변경이력 등록(초기등록)
					UpdateHistory(con,trans);
					break;
				case "ProcessSequenceInfo":
					ProductStorehouseTable(con,trans);				//생산창고 테이블을 생성하는 함수 호출
					//OutSideStorehouseTable(con,trans);				//외주창고 테이블을 생성하는 함수 호출
					break;
				case "WCInfo":
					ProductionCalendarTable(con,trans);				//생산달력 생성
					break;
				case "OutSideOrderUnitCodeInfo":					
					OutSideStoreTable(con,trans);					//하위품목을 외주창고에 입고시키는 테이블
					UpdateUCI_MTHistory(con,trans);
					break;
				case "SaleUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "BuyingUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "RealItemOrganizationInfo":
					UpdateRealHistory(con,trans);
					break;
				default:
					break;
			}
		}


		/// <summary>
		/// 품목구성 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void UpdateHistory(SqlConnection conn,SqlTransaction tr)
		{
			string str = @"Insert Into UPIOI_HT (ParentItemNum, ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator, 
				ProcessManagement, SubDivision, SupplyDivision, BOMUnit, BeginDate, EndDate, UpdateReason, 
				UpdatingPerson, UpdatingPersonID, UpdatingDate, ItemOrganizationInfoIndex)
				values (@ParentItemNum, @ChildItemNum, @NeedQuantityNumerator, @NeedQuantityDenominator, 
				@ProcessManagement, @SubDivision, @SupplyDivision, @BOMUnit, @BeginDate, @EndDate, @UpdateReason, 
				@UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @ItemOrganizationInfoIndex)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@ParentItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@ChildItemNum", m_InputList[1].ToString());
			comm.Parameters.Add("@NeedQuantityNumerator", m_InputList[2].ToString());
			comm.Parameters.Add("@NeedQuantityDenominator", m_InputList[3].ToString());
			comm.Parameters.Add("@ProcessManagement", m_InputList[4].ToString());
			comm.Parameters.Add("@SubDivision", m_InputList[5].ToString());
			comm.Parameters.Add("@SupplyDivision", m_InputList[6].ToString());
			comm.Parameters.Add("@BOMUnit", m_InputList[7].ToString());
			comm.Parameters.Add("@BeginDate", m_InputList[8].ToString());
			comm.Parameters.Add("@EndDate", m_InputList[9].ToString());
			comm.Parameters.Add("@UpdateReason",Session["Reason"].ToString());
			comm.Parameters.Add("@UpdatingPerson", UserName);
			comm.Parameters.Add("@UpdatingPersonID", m_UserID);
			comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
			if(m_ActionName == "Registration")
                comm.Parameters.Add("@ItemOrganizationInfoIndex", MaxIOI_MT(conn,tr));
			else
				comm.Parameters.Add("@ItemOrganizationInfoIndex", int.Parse(m_InputList[10].ToString()));

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Session.Remove("Reason");
		}

		/// <summary>
		/// 품목구성정보테이블원장번호 찾기
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int MaxIOI_MT(SqlConnection con,SqlTransaction trans)
		{
			string str = "Select Max(ItemOrganizationInfoIndex) From IOI_MT where RecodingState = 1 and ParentItemNum=@ParentItemNum and ChildItemNum = @ChildItemNum";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ParentItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@ChildItemNum", m_InputList[1].ToString());

			int result = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			return result;
		}




		/// <summary>
		/// 단가테이블 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void UpdateUCI_MTHistory(SqlConnection conn,SqlTransaction tr)
		{
			string str = @"Insert Into UPUCI_HT (ItemNum, UnitCostDistinction, BusinessRegistrationNum, BeginProcessCode, 
				EndProcessCode,	OrderRate, StandardUnitCost, DiscountUnitCost, BeginDate, EndDate,UpdateReason,
				UpdatingPerson, UpdatingPersonID, UpdatingDate, UnitCostInfoIndex)
				values (@ItemNum, @UnitCostDistinction, @BusinessRegistrationNum, @BeginProcessCode, 
				@EndProcessCode, @OrderRate, @StandardUnitCost, @DiscountUnitCost, @BeginDate, @EndDate, @UpdateReason,
				@UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @UnitCostInfoIndex)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@UnitCostDistinction", m_InputList[1].ToString());
			comm.Parameters.Add("@BusinessRegistrationNum", m_InputList[2].ToString());
			comm.Parameters.Add("@BeginProcessCode", m_InputList[3].ToString());
			comm.Parameters.Add("@EndProcessCode", m_InputList[4].ToString());
			comm.Parameters.Add("@OrderRate", decimal.Parse(m_InputList[5].ToString()));
			comm.Parameters.Add("@StandardUnitCost", decimal.Parse(m_InputList[6].ToString()));
			comm.Parameters.Add("@DiscountUnitCost", decimal.Parse(m_InputList[7].ToString()));
			comm.Parameters.Add("@BeginDate", m_InputList[8].ToString());
			comm.Parameters.Add("@EndDate", m_InputList[9].ToString());
			comm.Parameters.Add("@UpdateReason",Session["Reason"].ToString());
			comm.Parameters.Add("@UpdatingPerson", UserName);
			comm.Parameters.Add("@UpdatingPersonID", m_UserID);
			comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
			if(m_ActionName == "Registration")
				comm.Parameters.Add("@UnitCostInfoIndex", MaxUCI_MT(conn,tr));
			else
				comm.Parameters.Add("@UnitCostInfoIndex", int.Parse(m_InputList[10].ToString()));

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Session.Remove("Reason");
		}

		/// <summary>
		/// 단가테이블원장번호 찾기
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int MaxUCI_MT(SqlConnection con,SqlTransaction trans)
		{
			string str = "Select Max(UnitCostInfoIndex) From UCI_MT where ItemNum = @ItemNum and RecodingState =1 and BusinessRegistrationNum = @BusinessRegistrationNum and UnitCostDistinction = @UnitCostDistinction";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@UnitCostDistinction", m_InputList[1].ToString());
			comm.Parameters.Add("@BusinessRegistrationNum", m_InputList[2].ToString());

			int result = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			return result;
		}


		/// <summary>
		/// 진품목구성 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void UpdateRealHistory(SqlConnection conn,SqlTransaction tr)
		{
			string str = @"Insert Into UPRIOI_HT (ParentItemNum, ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator, 
				ProcessManagement, SubDivision, SupplyDivision, BOMUnit, BeginDate, EndDate, UpdateReason, 
				UpdatingPerson, UpdatingPersonID, UpdatingDate, RealItemOrganizationInfoIndex)
				values (@ParentItemNum, @ChildItemNum, @NeedQuantityNumerator, @NeedQuantityDenominator, 
				@ProcessManagement, @SubDivision, @SupplyDivision, @BOMUnit, @BeginDate, @EndDate, @UpdateReason, 
				@UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @RealItemOrganizationInfoIndex)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@ParentItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@ChildItemNum", m_InputList[1].ToString());
			comm.Parameters.Add("@NeedQuantityNumerator", m_InputList[2].ToString());
			comm.Parameters.Add("@NeedQuantityDenominator", m_InputList[3].ToString());
			comm.Parameters.Add("@ProcessManagement", m_InputList[4].ToString());
			comm.Parameters.Add("@SubDivision", m_InputList[5].ToString());
			comm.Parameters.Add("@SupplyDivision", m_InputList[6].ToString());
			comm.Parameters.Add("@BOMUnit", m_InputList[7].ToString());
			comm.Parameters.Add("@BeginDate", m_InputList[8].ToString());
			comm.Parameters.Add("@EndDate", m_InputList[9].ToString());
			comm.Parameters.Add("@UpdateReason",Session["Reason"].ToString());
			comm.Parameters.Add("@UpdatingPerson", UserName);
			comm.Parameters.Add("@UpdatingPersonID", m_UserID);
			comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
			if(m_ActionName == "Registration")
				comm.Parameters.Add("@RealItemOrganizationInfoIndex", MaxRIOI_MT(conn,tr));
			else
				comm.Parameters.Add("@RealItemOrganizationInfoIndex", int.Parse(m_InputList[10].ToString()));

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Session.Remove("Reason");
		}
		
		/// <summary>
		/// 품목구성정보테이블원장번호 찾기
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int MaxRIOI_MT(SqlConnection con,SqlTransaction trans)
		{
			string str = "Select Max(RealItemOrganizationInfoIndex) From RIOI_MT where RecodingState = 1 and ParentItemNum=@ParentItemNum and ChildItemNum = @ChildItemNum";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ParentItemNum", m_InputList[0].ToString());
			comm.Parameters.Add("@ChildItemNum", m_InputList[1].ToString());

			int result = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			return result;
		}



		//매입매출테이블 생성하는 함수
		private void BuyingSaleInfoTable(SqlConnection conn,SqlTransaction tr)
		{
			
//			기존의 레코드가 존재하는지 확인하는 부분을 입력해둘것
			string str = "";
			
			//비용거래처는 매입매출테이블을 생성할 필요없음
			if(m_InputList[0].ToString() == "1" ||  m_InputList[1].ToString() == "1" || m_InputList[2].ToString() == "1")
			{
				if(BSI_MT_ExistingRecord(conn,tr) == true)
				{
					str = "Update BSI_MT Set SaleDistinction = @sale, BuyingDistinction = @buy where BusinessRegistrationNum = @num and [Year] = @year";
				}
				else
				{
					str = "INSERT INTO BSI_MT (BusinessRegistrationNum, SaleDistinction, BuyingDistinction, [Year], RecodingState) Values(@num,@sale,@buy,@year,1)";
				}
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[6].ToString();
				if(m_InputList[0].ToString() == "1")
					comm.Parameters.Add("@sale",SqlDbType.Bit).Value = true;
				else
					comm.Parameters.Add("@sale",SqlDbType.Bit).Value = false;
				bool buy = false;
				if(m_InputList[1].ToString() == "1" ||  m_InputList[2].ToString() == "1")
					buy = true;
				comm.Parameters.Add("@buy",SqlDbType.Bit).Value = buy;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 매입매출 테이블에 기존의 레코드가 존재하는지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool BSI_MT_ExistingRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From BSI_MT Where RecodingState = 1 and BusinessRegistrationNum = @num and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction =trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[6].ToString();
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}


	
		/// <summary>
		/// 원자재창고테이블에 레코드를 생성하는 함수
		/// </summary>
		private void RowMetarialStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			//		기존의 레코드가 존재하는지 확인하는 부분을 입력해둘것
			//		레코드가 존재하면 해당레코드를 초기화 시킨다.
			string str = "";
			if(RMS_MT_ExistingRecord(conn,tr) == false)
			{
				
				str = "Insert Into RMS_MT (ItemNum,ProcessSequenceNum,ProcessCode,[Year], RecodingState) values(@num,'0','14000000',@year,1)";
			
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}

		/// <summary>
		/// 원자재 창고에 레코드가 있는지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool RMS_MT_ExistingRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From RMS_MT Where RecodingState = 1 and ItemNum = @num and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[6].ToString();
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}

		/// <summary>
		/// 영업창고테이블에 레코드를 생성하는 함수
		/// </summary>
		private void BusinessStorehouse(SqlConnection conn, SqlTransaction tr)
		{
			//기존의 레코드가 존재하는지 확인하는 부분을 입력해둘것
			//레코드가 존재하면 해당레코드를 초기화 시킨다.
			string str = "";
			if(BS_MT_ExistingRecord(conn,tr) == false)
			{
				
				// 영업1,2,3 창고를 생성하기위해서
				for(int i=1 ; i < 4; i++)
				{
					str = "Insert Into BS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessStorehouseNum,[Year],RecodingState) values(@num,'99','14009999',@house,@year,1)";
					SqlCommand comm = new SqlCommand(str,conn);
					comm.Transaction = tr;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@house",SqlDbType.TinyInt).Value = i;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}
		}

		/// <summary>
		/// 영업창고에 레코드가 존재하는지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool BS_MT_ExistingRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From BS_MT Where RecodingState = 1 and ItemNum = @num and [Year] = @year ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}


		/// <summary>
		/// 납품창고테이블에 레코드를 등록하는 함수
		/// </summary>
		private void DeliveryStorehouse(SqlConnection conn, SqlTransaction tr)
		{
			string str = "";
			if(DS_MT_ExistingRecord(conn, tr) == false)
			{
				
				str = "Insert Into DS_MT (ItemNum,ProcessSequenceNum,ProcessCode,[Year],RecodingState) values(@num,@sequence,@code,@year,1)";
			
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = 99;
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14009999";
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}

		/// <summary>
		/// 납품창고에 레코드가 존재하는지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool DS_MT_ExistingRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From DS_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14009999";
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}


		/// <summary>
		/// 생산창고테이블 레코드 등록함수
		/// </summary>
		private void ProductStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "";
			if(PS_MT_ExistingRecord(conn,tr) == false)
			{
				str = "Insert Into PS_MT (ItemNum,ProcessSequenceNum,ProcessCode,[Year],RecodingState) values(@num,@sequence,@code,@year,1)";
				//str = "Insert Into PS_MT (ItemNum,ProcessSequenceNum,ProcessCode,RecodingState) values(@num,@sequence,@code,1)";
			
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();

				if(m_InputList[3].ToString() == "상품")
				{
					comm.Parameters.Add("@sequence",SqlDbType.Int).Value = 99;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14009999";
				}
				else
				{
					comm.Parameters.Add("@sequence",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[2].ToString();
				}
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 생산창고에 동일한 품목이 있는지 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool PS_MT_ExistingRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From PS_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[2].ToString();
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}


		/// <summary>
		/// 작업장 정보 등록시 생산달력테이블 생성
		/// </summary>
		private void ProductionCalendarTable(SqlConnection conn, SqlTransaction tr)
		{

			// 먼저 기준작업장 달력이 생성 되었는지 확인한다음 생성되어있으면 있는 만큼만 각 작업장별로 만들어준다.
			// 오늘 이후부터 생성한다.
			int year = int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).ToString());
			int mon = int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).ToString());
			int day = int.Parse(DateTime.Now.ToShortDateString().Substring(8,2).ToString());

			string str = "Select * From PCI_MT Where RecodingState = 1 and WCName = '기준 작업장' and WC_Year >=@year and WC_Month >= @mon";
			SqlCommand comm1 = new SqlCommand();
			comm1.Connection = conn;
			comm1.Transaction= tr;
			comm1.CommandText = str;
			comm1.Parameters.Add("@year",year);
			comm1.Parameters.Add("@mon",mon);

			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm1);
			da.Fill(ds);

			string str_PCI = "";

			foreach(DataRow row in ds.Tables[0].Rows)
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;			
				comm.Transaction= tr;
				if(int.Parse(row["WC_Month"].ToString()) == mon && int.Parse(row["WC_Day"].ToString()) >=day)
				{
					str_PCI = "Insert PCI_MT (WCName,WC_Year,WC_Month,WC_Day,WC_Time,RecodingState) values(@name,@year,@mon,@day,@time,1)";
					comm.CommandText = str_PCI;
					comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(row["WC_Year"].ToString());
					comm.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(row["WC_Month"].ToString());
					comm.Parameters.Add("@day",SqlDbType.Int).Value = int.Parse(row["WC_Day"].ToString());
					comm.Parameters.Add("@time",SqlDbType.Decimal).Value = decimal.Parse(row["WC_Time"].ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
				else if(int.Parse(row["WC_Month"].ToString()) > mon)
				{
					str_PCI = "Insert PCI_MT (WCName,WC_Year,WC_Month,WC_Day,WC_Time,RecodingState) values(@name,@year,@mon,@day,@time,1)";
					comm.CommandText = str_PCI;
					comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(row["WC_Year"].ToString());
					comm.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(row["WC_Month"].ToString());
					comm.Parameters.Add("@day",SqlDbType.Int).Value = int.Parse(row["WC_Day"].ToString());
					comm.Parameters.Add("@time",SqlDbType.Decimal).Value = decimal.Parse(row["WC_Time"].ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
				
			}
		}


		/// <summary>
		/// 외주창고 생성함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void OutSideStoreTable(SqlConnection conn, SqlTransaction tr)
		{
			//시작공정의 하위품목을 가지고 등록한다.
			//시작공정이 최하위공정이면 바로 아래단계품목들을 출고시킨다
			//이때 바로 아랫단계가 원자재면 외주창고에 레코드를 생성시킨다
			//또한 아랫단계가 반제품인경우 공정을 순서대로 정렬해서 그 공정을 하나씩 꺼내어 외주단가 테이블에
			//동일한 거래처가 아닌것이 존재하면 그 공정을 등록하고 외주단가에 그 공정이 없으면 하나씩 내려가면서
			//공정을 판단해 등록시킨다.


			// 일단 발주원장의 시작공정 아래단계가 어떤 공정인지 알아야 한다
			// 공정순서가 제일 낮은 것이면 바로 아래단계 품목이 나가야 하고
			// 그렇지 않으면 그아래 공정순서 품목이 나가야 한다.
			// 그러므로 아랫단계에 공정이 있는지 없는지 확인하는것이 우선이다

			// 일단 지금 시작공정의 공정순서를 구한다음 그아래 가 있는지 확인한다
			int sequence = 0;

			string str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode = @code and ItemNum = @itemnum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[3].ToString();				//시작공정코드
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			
			SqlDataReader dr_num = comm.ExecuteReader();
			while(dr_num.Read())
			{
				sequence = int.Parse(dr_num["ProcessSequenceNum"].ToString());
			}
			dr_num.Close();
			comm.Parameters.Clear();
			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();


			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Code = dr2["ProcessCode"].ToString();
			}
			dr2.Close();
			comm.Parameters.Clear();

			

			// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
			// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
			string st_ItemNum = m_InputList[0].ToString();			//품목번호

			

			if(sequence1 != 0)
			{
				//지금 공정 이하를 스택에 저장시킨다.
				//하나씩 꺼내서 해당 공정이 자가이거나, 단가 테이블에 해당 품목의 공정이 종료공정으로 가지고 있으면 다른회사로 나가는 품목이므로
				//바로 아래공정을 출고시킨다.
				//꺼낸 품목의 공정순서를 넣어둔다.
				Stack stack = new Stack();
				str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum < @num  order by ProcessSequenceNum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequence;
				comm.CommandText = str;
				SqlDataReader dr_Sequence = comm.ExecuteReader();
				while(dr_Sequence.Read())
				{
					stack.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
				}
				dr_Sequence.Close();
				comm.Parameters.Clear();

				// 스택1에 있는 공정순서를 하나씩꺼낸다.
				while(stack.Count > 0)
				{
					int sequencenum = int.Parse(stack.Pop().ToString());
					str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
					comm.CommandText =str;
					comm.Parameters.Add("@item",SqlDbType.VarChar).Value = st_ItemNum;//품목번호
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
					int aaa = int.Parse(comm.ExecuteScalar().ToString());
					comm.Parameters.Clear();
								
					//공정코드를 구한다
					string processcode="";
					str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
						" FROM      PSI_MT INNER JOIN " +
						" PUC_MT ON ProcessCode = SmallClassificationCode " +
						" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
					comm.CommandText= str;
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
					SqlDataReader dr_code = comm.ExecuteReader();
					comm.Parameters.Clear();
					while(dr_code.Read())
					{
						processcode = dr_code["ProcessCode"].ToString();//공정코드
					}
					dr_code.Close();


					// 외주가 아니면 등록
					if(aaa != 0)
					{
						if(OS_MT_ExistingRecord(conn,tr,st_ItemNum,sequencenum,m_InputList[2].ToString()) == false)
						{
							str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,@sequence,@code,@Com,@year,1)";
									
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = st_ItemNum;			//품목번호
							comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
							comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						}
						break;
					}
					else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
					{
						// 외주단가에서 동일회사 외주공정인지 파악한다.
						str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
						comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
						comm.CommandText = str;
						int find = int.Parse(comm.ExecuteScalar().ToString());
						comm.Parameters.Clear();
									

						// 동일회사가 아니면 등록
						if(find == 0)
						{
							if(OS_MT_ExistingRecord(conn,tr,st_ItemNum,sequencenum,m_InputList[2].ToString()) == false)
							{
								str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,@sequence,@code,@Com,@year,1)";
								comm.CommandText = str;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = st_ItemNum;			//품목번호
								comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
								comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
								comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
								comm.Parameters.Add("@year",DateTime.Now.Year);
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
							break;
						}
					}
				}
			}			
			else
			{
				// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
				Stack st = new Stack();	

				string item = "";
				//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
				//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
				str = "SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum WHERE (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
				
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.CommandText= str;
				SqlDataReader dr_Child = comm.ExecuteReader();
				while(dr_Child.Read())
				{
					st.Push(dr_Child["ItemNum"].ToString());
				}
				dr_Child.Close();
				comm.Parameters.Clear();
				
				
				// 스택에 품목이 있는지 확인한다.
				if(st.Count != 0)
				{
					// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
					while(st.Count > 0)
					{	
						item =st.Pop().ToString();
						// 꺼낸품목의 공정순서를 스택1에 넣는다
						Stack st1 = new Stack();					
						int sequencenum=0;

						//꺼낸 품목의 공정순서를 넣어둔다.
						str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
						comm.CommandText = str;
						SqlDataReader dr_Sequence = comm.ExecuteReader();
						while(dr_Sequence.Read())
						{
							//공정순서
							st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
						}
						dr_Sequence.Close();
						comm.Parameters.Clear();

						
						//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
						if(st1.Count == 0)
						{
							//품번,도번,품명 구한다
							string itemdraw=""; string name="";
							str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
							
							comm.CommandText = str;
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
							SqlDataReader dr_Item = comm.ExecuteReader();
							comm.Parameters.Clear();
							while(dr_Item.Read())
							{
								itemdraw = dr_Item["ItemDrawNum"].ToString();
								name = dr_Item["ItemName"].ToString();
							}
							dr_Item.Close();
														
							if(OS_MT_ExistingRecord(conn,tr,item,0,m_InputList[2].ToString()) == false)
							{
								str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,'0','14000000',@Com,@year,1)";
							
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();
								comm.Parameters.Add("@year",DateTime.Now.Year);
								comm.CommandText = str;
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}
						else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
						{
							// 스택1에 있는 공정순서를 하나씩꺼낸다.
							while(st1.Count > 0)
							{
								sequencenum = int.Parse(st1.Pop().ToString());
								str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
								comm.CommandText =str;
								comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
								int aaa = int.Parse(comm.ExecuteScalar().ToString());
								comm.Parameters.Clear();
								
								//공정코드를 구한다
								string processcode="";
								str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
									" FROM      PSI_MT INNER JOIN " +
									" PUC_MT ON ProcessCode = SmallClassificationCode " +
									" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
								comm.CommandText= str;
								comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
								SqlDataReader dr_code = comm.ExecuteReader();
								comm.Parameters.Clear();
								while(dr_code.Read())
								{
									processcode = dr_code["ProcessCode"].ToString();//공정코드
								}
								dr_code.Close();


								// 외주가 아니면 등록
								if(aaa != 0)
								{
									if(OS_MT_ExistingRecord(conn,tr,item,sequencenum,m_InputList[2].ToString()) == false)
									{
										str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,@sequence,@code,@Com,@year,1)";
									
										comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
										comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
										comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
										comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
										comm.Parameters.Add("@year",DateTime.Now.Year);
										comm.CommandText = str;
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();
									}
									break;
								}
								else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
								{
									// 외주단가에서 동일회사 외주공정인지 파악한다.
									str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 등록
									if(find == 0)
									{
										if(OS_MT_ExistingRecord(conn,tr,item,sequencenum,m_InputList[2].ToString()) == false)
										{
											str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,@sequence,@code,@Com,@year,1)";
											comm.CommandText = str;
											comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
											comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
											comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
											comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
											comm.Parameters.Add("@year",DateTime.Now.Year);
											comm.ExecuteNonQuery();
											comm.Parameters.Clear();
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 외주창고에 레코드 존재하는지여부
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Sequnce"></param>
		/// <param name="Company"></param>
		/// <returns></returns>
		private bool OS_MT_ExistingRecord(SqlConnection con,SqlTransaction trans,string ItemNum, int Sequnce,string Company)
		{
			string str = "Select count(*) From OS_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @seq and BusinessRegistrationNum = @com and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",ItemNum);
			comm.Parameters.Add("@seq",Sequnce);
			comm.Parameters.Add("@com",Company);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}

		

		/// <summary>
		/// 수정메소드
		/// </summary>
		private void UpdateRecord()
		{
			if(ValidateFieldValue() == true)		// 유효성 검사 함수
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlTransaction tr = conn.BeginTransaction();
				try
				{
				
					MainTableUpdate(conn,tr);					// 메인테이블의 레코드를 수정하는 함수
					RelationTableUpdate(conn,tr);
					if(m_PageName == "CompanyInfo")
						BuyingSaleInfoTable(conn,tr);
					tr.Commit();
				}
				catch(Exception ee)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert("+ee.Message+"');");
					hc.Response.Write("</script>");

					tr.Rollback();
				}
				finally
				{
					conn.Close();
				}
			}
		}

		/// <summary>
		/// 업데이트 함수
		/// </summary>
		private void MainTableUpdate(SqlConnection conn, SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str = "";
			if(m_PageName == "WCProductionCalendarInfo" || m_PageName == "StandardProductionCalendarInfo")
			{
				str = "Update " +m_TableName.ToString()+ " set ";
				for(int i=0 ; i < (m_InputList.Count-1) ; i++)
				{
					str += m_FieldName[m_PageInt][i] + " = '" 
						+ m_InputList[i] + "', ";
				}

				str += "RecodingState = 1  where "+m_FieldName[m_PageInt][m_FieldName[m_PageInt].Length-1].ToString()+ " = @index";
			}
			else
			{
				str = "Update " +m_TableName.ToString()+ " set ";
				for(int i=0 ; i < (m_InputList.Count-1) ; i++)
				{
					str += m_FieldName[m_PageInt][i] + " = '" 
						+ m_InputList[i] + "', ";
				}

				str += "RecodingState = 1, UpdatingDate = @date, UpdatingPersonID = @personID,UpdatingPerson = @person  where "+m_FieldName[m_PageInt][m_FieldName[m_PageInt].Length-1].ToString()+ " = @index";
				
				comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				comm.Parameters.Add("@personID",SqlDbType.VarChar).Value = m_UserID.ToString();
				comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName.ToString();
			}

			comm.Parameters.Add("@index",SqlDbType.Int).Value = m_InputList[m_InputList.Count-1].ToString();
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script language=javascript>");
			hc.Response.Write("alert('수정이 완료되었습니다.');");
			hc.Response.Write("</script>");

		}


		private void RelationTableUpdate(SqlConnection con,SqlTransaction trans)
		{
			switch(m_PageName)
			{
				case "ItemOrganizationInfo":
					UpdateHistory(con,trans);
					break;
				case "BuyingUnitCodeInfo":
                    UpdateUCI_MTHistory(con,trans);
					break;
				case "OutSideOrderUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "SaleUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "ProcessSequenceInfo":
					UpdatePS_MT(con,trans);
					break;
				case "RealItemOrganizationInfo":
					UpdateRealHistory(con,trans);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 공정정보 수정시 생산창고 삭제후 새로 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void UpdatePS_MT(SqlConnection conn, SqlTransaction tr)
		{
			//기존공정창고삭제
			string str = "Update PS_MT Set RecodingState = '0' Where ItemNum = @num and ProcessSequenceNum = @code";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@num",m_InputList[0].ToString());
			comm.Parameters.Add("@code",m_InputList[1].ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새공정 추가
			str  = "Insert Into PS_MT (ItemNum,ProcessSequenceNum,ProcessCode,[Year],RecodingState) values(@num,@sequence,@code,@year,1)";
			comm.Parameters.Add("@num",m_InputList[0].ToString());
			comm.Parameters.Add("@sequence", int.Parse(m_InputList[1].ToString()));
			comm.Parameters.Add("@code",m_InputList[2].ToString());
			comm.Parameters.Add("@year",DateTime.Now.Year);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}

		


		private void UpdateRowOutSideTable(SqlConnection conn, SqlTransaction tr)
		{
			//외주거래처에 체크되어있으면 외주창고 생성하러간다
			if(m_InputList[1].ToString() == "1")
			{
				//외주거래처에 레코드가 존재하지 않으면 생성
				if(OS_MT_ExistingBusinessNumRecord(conn,tr) == false)
				{
					string strCom = "Select Distinct ItemNum,ProcessSequenceNum,ProcessCode From OS_MT Where RecodingState = 1";

					SqlCommand comm1 = new SqlCommand(strCom,conn);
					comm1.Transaction = tr;
					SqlDataAdapter da = new SqlDataAdapter(comm1);
					DataSet ds = new DataSet();
					da.Fill(ds);

					foreach(DataRow row in ds.Tables[0].Rows)
					{
						strCom = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,[Year],RecodingState) values(@num,@sequence,@code,@Com,@year,1)";
						
						SqlCommand comm = new SqlCommand(strCom,conn);
						comm.Transaction = tr;
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
						comm.Parameters.Add("@sequence",SqlDbType.VarChar).Value = row["ProcessSequenceNum"].ToString();
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = row["ProcessCode"].ToString();
						comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[6].ToString();
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
					}

					
				}
			}
			else
			{
				//외주거래처가 존재하면 비활성화
				if(OS_MT_ExistingBusinessNumRecord(conn,tr) == true)
				{
					string str = "Update OS_MT Set RecodingState=0 where BusinessRegistrationNum = @com";
					SqlCommand comm = new SqlCommand(str,conn);
					comm.Transaction = tr;
					comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[6].ToString();
					comm.ExecuteNonQuery();
				}
			}

		}

		/// <summary>
		/// 거래처정보 수정시 외주창고에 거래처가 존재하는지
		/// 확인하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private bool OS_MT_ExistingBusinessNumRecord(SqlConnection con, SqlTransaction trans)
		{
			string str = "Select count(*) From OS_MT Where RecodingState = 1 and BusinessRegistrationNum = @com and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[6].ToString();
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}
		

		/// <summary>
		/// 삭제메소드
		/// </summary>
		private void DeleteRecord()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{	
				MainTableDelete(conn,tr);		// 메인테이블을 삭제하는 함수
				RelatedTableDelete(conn,tr);	// 관련테이블을 삭제하는 함수

				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('삭제되었습니다!');");
				hc.Response.Write("</script>");

				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
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
		/// 메인테이블을 삭제하는 함수
		/// </summary>
		private void MainTableDelete(SqlConnection con,SqlTransaction trans)
		{		
			string str = "Update " +m_TableName.ToString()+ " set RecodingState = 0 Where "+m_FieldName[m_PageInt][m_FieldName[m_PageInt].Length-1].ToString()+ " = '" + m_InputList[m_InputList.Count-1].ToString()+"'";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 관련테이블을 삭제하는 함수
		/// 요청페이지에 따라 관련테이블들이 다르다
		/// </summary>
		private void RelatedTableDelete(SqlConnection con,SqlTransaction trans)
		{					
			switch(m_PageName)
			{
				case "CompanyInfo":
					DeleteBuyingSaleInfoTable(con,trans);
					DeleteRowOutSideStoreTable(con,trans);//외주창고 삭제
					break;
				case "ItemInfo":
					if(m_InputList[3].ToString() == "원자재")
					{
						DeleteRowMetarialStorehouseTable(con,trans);			// 원자재창고
						
					}
					else if(m_InputList[3].ToString() == "제품" || m_InputList[3].ToString() == "반제품")
					{
						DeleteBusinessStorehouseTable(con,trans);				// 영업창고
						DeleteDeliveryStorehouseTable(con,trans);				// 납품창고
						DeleteProductStorehouse(con,trans);						// 생산창고
						DeleteIOI_MT(con,trans);								// 품목구성
						DeleteProcessSequence(con,trans);						// 공정순서
						DeleteWorkStandard(con,trans);							// 작업표준
					}
					else if(m_InputList[3].ToString() == "상품")
					{
						DeleteBusinessStorehouseTable(con,trans);				// 영업창고
						DeleteDeliveryStorehouseTable(con,trans);				// 납품창고
						DeleteProductStorehouse(con,trans);						// 생산창고
					}
					DeleteUCI_MT(con,trans);									// 단가정보
					DeleteOutSideStorehouseItemTable(con,trans);				// 외주창고
					break;
				case "ItemOrganizationInfo":
					UpdateHistory(con,trans);
					break;
				case "ProcessSequenceInfo":
				{
					DeleteProductStorehouseTable(con,trans);				//생산창고 테이블을 삭제하는 함수 호출
					DeleteOutSideStorehouseProcessTable(con,trans);				//외주창고 테이블을 삭제하는 함수 호출
					DeleteWorkStandardTable(con,trans);					//작업표준 테이블 레코드 삭제

				}
					break;
				case "OutSideOrderUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					DeleteOutSideStorehouseTable(con,trans);				//외주창고 테이블 삭제하는 함수
					break;
				case "SaleUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "BuyingUnitCodeInfo":
					UpdateUCI_MTHistory(con,trans);
					break;
				case "WCInfo":
				{
					DeleteProductionCalendar(con,trans);					// 생산달력 삭제함수
				}
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 품목정보 삭제 품목구성 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteIOI_MT(SqlConnection conn, SqlTransaction tr)
		{

			//품목구성이 삭제되므로 이력원장에 등록한다
			string str = "Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @num or ChildItemNum = @num";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				str = @"Insert into UPIOI_HT (ParentItemNum, ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator, ProcessManagement, SubDivision, SupplyDivision, BOMUnit, BeginDate, EndDate, UpdateReason, UpdatingPerson, UpdatingPersonID, UpdatingDate, ItemOrganizationInfoIndex)
					values(@ParentItemNum, @ChildItemNum, @NeedQuantityNumerator, @NeedQuantityDenominator, @ProcessManagement, @SubDivision, @SupplyDivision, @BOMUnit, @BeginDate, @EndDate, '품목정보삭제로 인한 삭제', @UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @ItemOrganizationInfoIndex)";

				comm.Parameters.Add("@ParentItemNum",dr["ParentItemNum"].ToString());
				comm.Parameters.Add("@ChildItemNum",dr["ChildItemNum"].ToString());
				comm.Parameters.Add("@NeedQuantityNumerator",decimal.Parse(dr["NeedQuantityNumerator"].ToString()));
				comm.Parameters.Add("@NeedQuantityDenominator",decimal.Parse(dr["NeedQuantityDenominator"].ToString()));
				comm.Parameters.Add("@ProcessManagement",bool.Parse(dr["ProcessManagement"].ToString()));
				comm.Parameters.Add("@SubDivision",bool.Parse(dr["SubDivision"].ToString()));
				comm.Parameters.Add("@SupplyDivision",dr["SupplyDivision"].ToString());
				comm.Parameters.Add("@BOMUnit",dr["BOMUnit"].ToString());
				comm.Parameters.Add("@BeginDate",DateTime.Parse(dr["BeginDate"].ToString()).ToShortDateString());
				comm.Parameters.Add("@EndDate",DateTime.Parse(dr["EndDate"].ToString()).ToShortDateString());
				comm.Parameters.Add("@UpdatingPerson",UserName);
				comm.Parameters.Add("@UpdatingPersonID",m_UserID);
				comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@ItemOrganizationInfoIndex",dr["ItemOrganizationInfoIndex"].ToString());
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
			str = "Update IOI_MT Set  RecodingState = 0 where ParentItemNum = @num or ChildItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 품목정보 삭제시 품목구성정보 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteItemOrganizationInfo(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update IOI_MT set RecodingState = 0 Where ParentItemNum = @num or ChildItemNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction  =tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 매입매출테이블의 레코드를 삭제하는 함수
		/// </summary>
		private void DeleteBuyingSaleInfoTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update BSI_MT set RecodingState = 0 Where BusinessRegistrationNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction  =tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[6].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 거래처 삭제시 외주창고레코드 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteRowOutSideStoreTable(SqlConnection conn, SqlTransaction tr)
		{
			//삭제할 거래처명
			string str = "Update OS_MT set RecodingState = 0 Where BusinessRegistrationNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[6].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 원자재 창고 테이블 삭제
		/// </summary>
		private void DeleteRowMetarialStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update RMS_MT set RecodingState = 0 Where ItemNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();

		}

		/// <summary>
		/// 외주창고테이블 레코드 삭제
		/// 원자재
		/// </summary>
		private void DeleteRowOutSideStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string strCom = "Select * From CI_MT Where OutSideOrderCompany = 1 and RecodingState = 1";
			SqlCommand comm1 = new SqlCommand(strCom,conn);
			comm1.Transaction = tr;
			SqlDataAdapter da = new SqlDataAdapter(comm1);
			DataSet ds = new DataSet();
			da.Fill(ds);
            
			string str ="";
			string company = "";
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				company = row["BusinessRegistrationNum"].ToString();

				str = "Update OS_MT set RecodingState = 0 where ItemNum = @num and BusinessRegistrationNum = @Com";
				
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Transaction  = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = company;
				comm.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 영업창고테이블 레코드 삭제
		/// </summary>
		private void DeleteBusinessStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update BS_MT Set  RecodingState = 0 where ItemNum = @num";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 납품창고테이블 레코드 삭제
		/// </summary>
		private void DeleteDeliveryStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update DS_MT Set  RecodingState = 0 where ItemNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 품목정보 삭제시 생산창고 테이블의 레코드 삭제
		/// </summary>
		private void DeleteProductStorehouse(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update PS_MT Set  RecodingState = 0 where ItemNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 품목정보 삭제시 공정순서레코드 삭제
		/// </summary>
		private void DeleteProcessSequence(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update PSI_MT Set  RecodingState = 0 where ItemNum = @num";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction =tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 품목정보 삭제시 작업표준 레코드 삭제
		/// </summary>
		private void DeleteWorkStandard(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update WSI_MT Set  RecodingState = 0 where ItemNum = @num";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 품목정보 삭제시 외주창고 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteOutSideStorehouseItemTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update OS_MT Set  RecodingState = 0 where ItemNum = @num";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 공정순서 삭제시
		/// 생산창고테이블 레코드 삭제
		/// </summary>
		private void DeleteProductStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update PS_MT Set  RecodingState = 0 where ItemNum = @num and ProcessSequenceNum = @sequence and ProcessCode = @code";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@sequence",SqlDbType.VarChar).Value =  m_InputList[1].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[2].ToString();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 공정순서 삭제시
		/// 외주창고 테이블 레코드 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteOutSideStorehouseProcessTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update OS_MT Set  RecodingState = 0 where ItemNum = @num and ProcessSequenceNum = @sequence and ProcessCode = @code";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@sequence",SqlDbType.VarChar).Value =  m_InputList[1].ToString();
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[2].ToString();
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 외주단가 삭제시
		/// 외주창고 테이블 레코드 삭제
		/// </summary>
		private void DeleteOutSideStorehouseTable(SqlConnection conn, SqlTransaction tr)
		{
			//시작공정의 하위품목을 가지고 등록한다.
			//시작공정이 최하위공정이면 바로 아래단계품목들을 출고시킨다
			//이때 바로 아랫단계가 원자재면 외주창고에 레코드를 생성시킨다
			//또한 아랫단계가 반제품인경우 공정을 순서대로 정렬해서 그 공정을 하나씩 꺼내어 외주단가 테이블에
			//동일한 거래처가 아닌것이 존재하면 그 공정을 등록하고 외주단가에 그 공정이 없으면 하나씩 내려가면서
			//공정을 판단해 등록시킨다.


			// 일단 발주원장의 시작공정 아래단계가 어떤 공정인지 알아야 한다
			// 공정순서가 제일 낮은 것이면 바로 아래단계 품목이 나가야 하고
			// 그렇지 않으면 그아래 공정순서 품목이 나가야 한다.
			// 그러므로 아랫단계에 공정이 있는지 없는지 확인하는것이 우선이다

			// 일단 지금 시작공정의 공정순서를 구한다음 그아래 가 있는지 확인한다
			int sequence = 0;

			string str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode = @code and ItemNum = @itemnum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_InputList[3].ToString();				//시작공정코드
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			
			SqlDataReader dr_num = comm.ExecuteReader();
			while(dr_num.Read())
			{
				sequence = int.Parse(dr_num["ProcessSequenceNum"].ToString());
			}
			dr_num.Close();
			comm.Parameters.Clear();
			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();


			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();			//품목번호
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Code = dr2["ProcessCode"].ToString();
			}
			dr2.Close();
			comm.Parameters.Clear();

			

			// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
			// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
			string st_ItemNum = m_InputList[0].ToString();			//품목번호

			

			if(sequence1 != 0)
			{
				str = "Update OS_MT Set  RecodingState = '0' Where  ItemNum= @num and ProcessSequenceNum = @sequence and ProcessCode = @code and BusinessRegistrationNum = @Com";
				comm.CommandText= str;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequence1;
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
				comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
				Stack st = new Stack();	

				string item = "";
				//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
				//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
				str = "SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum WHERE (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
				
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.CommandText= str;
				SqlDataReader dr_Child = comm.ExecuteReader();
				while(dr_Child.Read())
				{
					st.Push(dr_Child["ItemNum"].ToString());
				}
				dr_Child.Close();
				comm.Parameters.Clear();
				
				
				// 스택에 품목이 있는지 확인한다.
				if(st.Count != 0)
				{
					// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
					while(st.Count > 0)
					{	
						item =st.Pop().ToString();
						// 꺼낸품목의 공정순서를 스택1에 넣는다
						Stack st1 = new Stack();					
						int sequencenum=0;

						//꺼낸 품목의 공정순서를 넣어둔다.
						str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
						comm.CommandText = str;
						SqlDataReader dr_Sequence = comm.ExecuteReader();
						while(dr_Sequence.Read())
						{
							//공정순서
							st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
						}
						dr_Sequence.Close();
						comm.Parameters.Clear();

						
						//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
						if(st1.Count == 0)
						{
							//품번,도번,품명 구한다
							string itemdraw=""; string name="";
							str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
							
							comm.CommandText = str;
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
							SqlDataReader dr_Item = comm.ExecuteReader();
							comm.Parameters.Clear();
							while(dr_Item.Read())
							{
								itemdraw = dr_Item["ItemDrawNum"].ToString();
								name = dr_Item["ItemName"].ToString();
							}
							dr_Item.Close();
														

							str = "Update OS_MT Set RecodingState = '0' Where  ItemNum = @num and ProcessCode = '14000000' and BusinessRegistrationNum = @Com";
							
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = name;
							comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						}
						else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
						{
							// 스택1에 있는 공정순서를 하나씩꺼낸다.
							while(st1.Count > 0)
							{
								sequencenum = int.Parse(st1.Pop().ToString());
								str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
								comm.CommandText =str;
								comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
								int aaa = int.Parse(comm.ExecuteScalar().ToString());
								comm.Parameters.Clear();
								
								//공정코드를 구한다
								string processcode="";
								str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
									" FROM      PSI_MT INNER JOIN " +
									" PUC_MT ON ProcessCode = SmallClassificationCode " +
									" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
								comm.CommandText= str;
								comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
								SqlDataReader dr_code = comm.ExecuteReader();
								comm.Parameters.Clear();
								while(dr_code.Read())
								{
									processcode = dr_code["ProcessCode"].ToString();//공정코드
								}
								dr_code.Close();


								// 외주가 아니면 등록
								if(aaa != 0)
								{
									str = "Update OS_MT Set RecodingState = 0 Where ItemNum= @num and ProcessSequenceNum = @sequence and ProcessCode=@code and BusinessRegistrationNum=@Com";
									
									comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
									comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
									comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
									comm.CommandText = str;
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
									break;
								}
								else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
								{
									// 외주단가에서 동일회사 외주공정인지 파악한다.
									str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 등록
									if(find == 0)
									{
										// 데이터셋에 추가하기위한 Row
										str = "Update OS_MT Set RecodingState = '0' Where ItemNum=@num and ProcessSequenceNum=@sequence and ProcessCode = @code and BusinessRegistrationNum = @Com";
										comm.CommandText = str;
										comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
										comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
										comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
										comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = m_InputList[2].ToString();	//거래처
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 외주거래처정보 삭제시 외주창고 삭제
		/// </summary>
		private void DeleteRowOutSideTable(SqlConnection conn, SqlTransaction tr)
		{
			string company = "";
			//string strCom = "Select BusinessRegistrationNum From CI_MT Where RecodingState = 1 and OutSideOrderCompany = 0 and CompanyInfoIndex = @idx";
			string strCom = "Select BusinessRegistrationNum From CI_MT Where RecodingState = 1 and CompanyInfoIndex = @idx";
			SqlCommand comm1 = new SqlCommand(strCom,conn);
			comm1.Transaction =tr;
			comm1.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_InputList[m_InputList.Count-1].ToString());
			SqlDataReader dr = comm1.ExecuteReader();
			while(dr.Read())
			{
				company = dr["BusinessRegistrationNum"].ToString();
			}
			dr.Close();
            
			string str = "Update OS_MT set RecodingState = 0 where BusinessRegistrationNum = @Com";
				
			SqlCommand comm = new SqlCommand(str,conn);	
			comm.Transaction =tr;
			comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = company;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 공정순서 정보 삭제시 작업표준레코드 삭제
		/// </summary>
		private void DeleteWorkStandardTable(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update WSI_MT Set  RecodingState = 0 where ItemNum = @num and ProcessSequenceNum = @sequence";
					
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@sequence",SqlDbType.VarChar).Value = m_InputList[1].ToString();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// WC 정보 삭제시 생산달력 레코드 삭제
		/// </summary>
		private void DeleteProductionCalendar(SqlConnection conn, SqlTransaction tr)
		{
			string str = "Update PCI_MT Set RecodingState = 0 Where WCName = @name";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
			
		}

		/// <summary>
		/// 품목정보 삭제시 단가정보 레코드 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void DeleteUCI_MT(SqlConnection conn, SqlTransaction tr)								// 단가정보
		{
			string str = "Update UCI_MT Set RecodingState = 0 Where ItemNum = @name";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_InputList[0].ToString();
			comm.ExecuteNonQuery();
		}
	}
}

