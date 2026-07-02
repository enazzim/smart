using System;

namespace KIT_ERP
{
	enum PageName
	{
		CompanyInfo,
		ItemInfo,
		ItemOrganizationInfo,
		WCInfo,
		ProcessSequenceInfo,
		EquipmentInfo,
		WorkStandardInfo,
		SaleUnitCodeInfo,		//판매단가 
		BuyingUnitCodeInfo,	// 구매단가 
		OutSideOrderUnitCodeInfo,
		UserInfo,
		PublicUseCodeInfo,
		StandardProductionCalendarInfo,
		WCProductionCalendarInfo,
		BusinessPlanInfo,
		ExecutionPlanInfo,
		MonthlyClosingInfo,
		RealItemOrganizationInfo,
		RealProcessSequenceInfo,
		RealWorkStandardInfo,


		ReceiveingRegistration,//수주등록
		ReceiveingbundleRegistration,//수주일괄등록
		ReceiveingPC,//수주현황
		ReceiveingProgressLook,//수주진행보기
		ProductionRequest,//생산의뢰
		ProductionRequestPC,//생산의뢰현황
		ProductionRequestAdd,//생산의뢰추가
		GoodsBuyingRequest,//상품구매의뢰
		GoodsBuyingRequestPC,//상품구매의뢰현황
		GoodsBuyingRequestAdd,//상품구매의뢰추가
		BusinessStorehouseInStorehouse,//영업창고입고
		BusinessStorehouseInStorehousePC,//영업창고입고현황
		GoodsManufactureOutStorehouse,//상품제품출고
		GoodsManufactureOutStorehousePC,//상품제품출고현황
		SaleHistoryRegistration,//매출등록
		SaleHistoryRegistrationPC,//매출현황
		CollectMoneyRegistration,//수금등록
		CollectMoneyRegistrationPC,//수금현황
		UnCollectMoneyLook,//미수금보기
		OtherInOutStorehouse,//기타입출고
		OtherInOutStorehousePC,//기타입출고현황
		StorehouseMoving,//창고이동
		StorehouseMovingPC,//창고이동현황

		ProductionPlan,//생산계획
		ProductionPlanPC,//생산계획현황
		ProductionResultPC,//생산결과현황
		ProductionPlanAdd,//생산계획추가
		RowMetarialRequirementCalculate,//
		RowMetarialRequirementCalculatePC,
		WorkPlanEstablish,
		WCPlanPC,
		WorkDailyReportRegistration,
		WorkDailyReportRegistrationPC,
		RowRowMetariallRequriement,
		RowMetarialRequriementPC,
		RowMetarialRequriementAdd,
		OutsideRequest,
		OutsiderRequestPC,
		
		BuyingOrder,
		BuyingOrderPC,
		BuyingProgressLook,
		BuyingOrderAdd,
		BuyingDelivery,
		BuyingDeliveryPC,
		OutSideOrder,
		OutSideOrderPC,
		OutSideProgressLook,
		OutSideOutStorehouse,
		OutSideOutStorehousePC,
		OutSideDelivery,
		OutSideDeliveryPC,
		PaymentPlanResultRegistraton,
		PaymentPlanResultPC,

		QualityInspectionRegistration,
		QualityInspectPC,
		QualityInspecStatistics,
		IncongruityPresentIndex,
		IncongruityCauseIndex,
		AutonomyInspectPC,
		AutonomyInspectSatistics,
		AutonomyInspectIndex,
		MoneyStandardIndex,
		QuantityStandardIndex,
		LotStandardIndex,
        
		BizPlanResultIndex,
		SaleTatalProfitIndex,
		SaleIndex,
		BuyingIndex,
		ItemsStockIndex,
		StoreHouseStockIndex,
		StockMoneyIndex,
		WorkingRatioIndex,
		NonWorkingRatioIndex,
		ProductivityIndex,

		UserCompanyInput,
		SystemAbilitySet,
		StandardInforbundleRegistration,
		DataBackUpRestoration,
		HistoryAdjustment,

		OrderPC,
		DeliveryRequest,
		OutStorehousePC,
		DeliveryPC,
		StockPC,
		QualityPC,
		QualityIndex,
		품질검사등록 ,
		품질검사현황,
		품질검사통계,
		부적합현상지표,
		부적합원인지표,
		자주검사현황,
		자주검사통계,
		자주검사지표,
		사업계획실적지표,
		매출총이익지표,
		매출지표,
		매입지표,
		품목별재고지표,
		창고별재고지표,
		재고금액지표,
		가동률지표,
		비가동지표,
		생산성지표,
		금액기준지표,
		수량기준지표,
		로트기준지표
	}



	/// <summary>
	/// 요청테이블
	/// </summary>
	enum HistroyName
	{
		RO_HT,		//수주원장
		PR_HT,		//생산의뢰원장
		PP_HT,		//생산계획원장
		MRC_HT,		//자재소요량 원장
		WP_HT,		//작업계획원장
		WDWP_HT,	//WC별 일자별 작업계획원장
		WDR_HT,		//작업일보원장
		QI_HT,		//품질검사원장
		BR_HT,		//구매의뢰원장
		BO_HT,		//구매발주원장
		BDR_HT,		//구매납품의뢰원장
		BD_HT,		//구매납품원장
		OOR_HT,		//외주의뢰원장
		OO_HT,		//외주발주원장
		OOS_HT,		//외주출고원장
		ODR_HT,		//외주납품의뢰원장
		OSD_HT,		//외주납품원장
		ISR_HT,		//입고의뢰원장
		IS_HT,		//입고원장
		OS_HT,		//출고원장
		OIOS_HT,	//기타입출고원장
		SM_HT,		//창고이동원장
		S_HT,		//매출원장
		B_HT,		//매입원장
		P_HT,		//지급원장
		CM_HT,		//수금원장
	
	}



	/// <summary>
	/// 마스터 테이블이름
	/// </summary>
	public class MasterTable
	{
		public static string PublicUseCode = "PUC_MT";
		public static string CompanyInfo = "CI_MT";
		public static string ItemInfo = "II_MT";
		public static string ItemOrganizationInfo = "IOI_MT";
		public static string EquipmentInfo = "EI_MT";
		public static string WCInfo = "WCI_MT";
		public static string ProcessSequenceInfo = "PSI_MT";
		public static string WorkStandardInfo = "WSI_MT";
		public static string UnitCostInfo = "UCI_MT";
		public static string OutSideOutStorehouseItem = "OSOSI_MT";
		public static string UserInfo = "UI_MT";
		public static string ProductionCalendarInfo = "PCI_MT";
		public static string BusinessPlanInfo = "BPI_MT";
		public static string ExecutionPlanInfo = "EPI_MT";
		public static string MonthlyClosingInfo = "MCI_MT";
		public static string BuyingSaleInfo = "BSI_MT";
		public static string RowMetarialStorehouse = "RMS_MT";
		public static string ProductionStorehouse = "PS_MT";
		public static string OutSideStorehouse = "OS_MT";
		public static string BusinessStorehouse = "BS_MT";
		public static string DeliveryStorehouse ="DS_MT";
	}


	/// <summary>
	/// 원장이름
	/// </summary>
	public class HistoryTable
	{
		public static string ReceiveingOrderHistroy = "RO_HT";
		public static string ProductionRequestHistroy = "PR_HT";
		public static string ProductionPlanHistory = "PP_HT";
		public static string MetarialRequirementCalculateHistory = "MRC_HT";
		public static string WorkPlanHistory = "WP_HT";
		public static string WCDailyWorkPlanHistory = "WDWP_HT";
		public static string WorkDailyReportHistory = "WDR_HT";
		public static string QualityInspectionHistory = "QI_HT";
		public static string BuyingRequestHistory = "BR_HT";
		public static string BuyingOrderHistory = "BO_HT";
		
		public static string BuyingDeliveryRequestHistory = "BDR_HT";
		public static string BuyingDeliveryHistory = "BD_HT";
		public static string OutSideOrderRequestHistory = "OOR_HT";
		public static string OutSideOrderHistory ="OO_HT";
		public static string OutSideOutStorehouseHistory = "OOS_HT";
		public static string OutSideDeliveryRequestHistory = "ODR_HT";
		public static string OutSideDeliveryHistory = "OSD_HT";
		public static string InStorehouseRequestHistory ="ISR_HT";
		public static string InStorehouseHistory = "IS_HT";
		public static string OutStorehouseHistory = "OS_HT";

		public static string OtherInOutStorehouseHistory = "OIOS_HT";
		public static string StorehouseMovingHistory = "SM_HT";
		public static string SaleHistory = "S_HT";
		public static string BuyingHistory = "B_HT";
		public static string PaymentHistory ="P_HT";
		public static string CollectMoneyHistory ="CM_HT";

		public static string UserCompanyInfo = "UCI_T";
		public static string PageAuthority = "PA_T";
		public static string SystemCapabilitySet = "SCS_T";
		public static string DataBackUpRestoration = "DBUR_T";
	}

	/// <summary>
	/// 마스터테이블 필드이름
	/// </summary>
	public class MasterTableField
	{
		public static string PublicUseCodeIndex = "PublicUseCodeIndex";
		public static string LargeClassificationCode = "LargeClassificationCode";
		public static string LargeClassificationName = "LargeClassificationName";
		public static string SmallClassificationCode = "SmallClassificationCode";
		public static string SmallClassificationName = "SmallClassificationName";
		public static string RecodingState = "RecodingState";
		public static string RegistrationDate = "RegistrationDate";
		public static string RegistrationPerson = "RegistrationPerson";
		public static string RegistrationPersonID = "RegistrationPersonID";
		public static string UpdatingDate = "UpdatingDate";
		public static string UpdatingPerson = "UpdatingPerson";
		public static string UpdatingPersonID = "UpdatingPersonID";

		//		public static string 공용코드번호 = "PublicUseCodeIndex";
		//		public static string 대분류코드 = "LargeClassificationCode";
		//		public static string 대분류명 = "LargeClassificationName";
		//		public static string 소분류코드 = "SmallClassificationCode";
		//		public static string 소분류명 = "SmallClassificationName";
		//		public static string 레코드상태 = "RecodingState";
		//		public static string 등록일 = "RegistrationDate";
		//		public static string 등록자 = "RegistrationPerson";
		//		public static string 수정일 = "UpdatingDate";
		//		public static string 수정자 = "UpdatingPerson";


		public static string CompanyInfoIndex = "CompanyInfoIndex";
		public static string ReceiveingOrderCompany = "ReceiveingOrderCompany";
		public static string OutSideOrderCompany = "OutSideOrderCompany";
		public static string BuyingCompany = "BuyingCompany";
		public static string CostCompany = "CostCompany";
		public static string CompanyName = "CompanyName";
		public static string BusinessRegistrationNum = "BusinessRegistrationNum";
		public static string PresidentName = "PresidentName";
		public static string CorporationRegistrationNum = "CorporationRegistrationNum";
		public static string BusinessCompanyAddress = "BusinessCompanyAddress";
		public static string TaxBillAddress = "TaxBillAddress";
		public static string HomepageAddress = "HomepageAddress";
		public static string BusinessClassification = "BusinessClassification";
		public static string BusinessItem = "BusinessItem";
		public static string CurrentTradeState = "CurrentTradeState";
		public static string SupplementaryValueTax = "SupplementaryValueTax";
		public static string SupplementaryValueTaxRate = "SupplementaryValueTaxRate";
		public static string TelephoneNum = "TelephoneNum";
		public static string FaxNum = "FaxNum";
		public static string TradeClassification1 = "TradeClassification1";
		public static string TradeClassification2 = "TradeClassification2";
		public static string TradeClassification3 = "TradeClassification3";
		public static string SaleStandardDate = "SaleStandardDate";
		public static string SaleStandardBillDate = "SaleStandardBillDate";
		public static string BillApprovalStandard = "BillApprovalStandard";
		public static string FixPeriodCollectMoneyDate1 = "FixPeriodCollectMoneyDate1";
		public static string FixPeriodCollectMoneyDate2 = "FixPeriodCollectMoneyDate2";
		public static string CompanyPersonInCharge = "CompanyPersonInCharge";
		public static string CompanyPersonInChargeEmail = "CompanyPersonInChargeEmail";



		public static string ItemInfoIndex = "ItemInfoIndex";
		public static string ItemNum = "ItemNum";
		public static string ItemDrawNum = "ItemDrawNum";
		public static string ItemName = "ItemName";
		public static string PropertyClassification = "PropertyClassification";
		public static string Unit = "Unit";
		public static string Standard = "Standard";
		public static string Texture = "Texture";
		public static string StockUnit = "StockUnit";
		public static string BOMUnit = "BOMUnit";
		public static string PurchaseUnit = "PurchaseUnit";
		public static string SaleUnit = "SaleUnit";
		public static string IOChackable = "IOChackable";
		public static string StockManagable = "StockManagable";
		public static string CheckDistinction = "CheckDistinction";
		public static string OrderPlan = "OrderPlan";
		public static string ItemType = "ItemType";
		public static string MateralQuality = "MateralQuality";
		public static string ItemState = "ItemState";
		public static string Maker = "Maker";
		public static string ItemClassification1 = "ItemClassification1";
		public static string ItemClassification2 = "ItemClassification2";
		public static string ItemClassification3 = "ItemClassification3";
		public static string ItemClassification4 = "ItemClassification4";
		public static string Standard1 = "Standard1";
		public static string Unit1 = "Unit1";
		public static string Standard2 = "Standard2";
		public static string Unit2 = "Unit2";
		public static string Standard3 = "Standard3";
		public static string Unit3 = "Unit3";
		public static string Standard4 = "Standard4";
		public static string Unit4 = "Unit4";
		public static string ProductWeight = "ProductWeight";
		public static string MaterialWeight = "MaterialWeight";
		public static string SupplyTerm = "SupplyTerm";
		public static string DomesticImportDistinction = "DomesticImportDistinction";
		public static string SafetyStockQuantity = "SafetyStockQuantity";
		public static string OrderIntervalQuantity = "OrderIntervalQuantity";
		public static string MinOrderQuantity = "MinOrderQuantity";
		public static string StiffenDeep = "StiffenDeep";
		public static string ProductHeatTreatmentDescription = "ProductHeatTreatmentDescription";
		public static string MaterialHeatTreatmentDescription = "MaterialHeatTreatmentDescription";
		public static string MaterialHeatTreatmentRequestDegree = "MaterialHeatTreatmentRequestDegree";
		public static string CuttingSpace = "CuttingSpace";
		public static string TariffRate = "TariffRate";
		public static string StandardUnitCost = "StandardUnitCost";
		public static string MainPurchaseCompany = "MainPurchaseCompany";
		public static string MainOutSideOrderCompany = "MainOutSideOrderCompany";
		public static string MainSaleCompany = "MainSaleCompany";
		public static string ChargePerson = "ChargePerson";
		public static string CompleteLeadTime = "CompleteLeadTime";


		public static string ItemOrganizationInfoIndex = "ItemOrganizationInfoIndex";
		public static string ParentItemNum = "ParentItemNum";
		public static string ChildItemNum = "ChildItemNum";
		public static string NeedQuantityNumerator = "NeedQuantityNumerator";
		public static string NeedQuantityDenominator = "NeedQuantityDenominator";
		public static string ProcessManagement = "ProcessManagement";
		public static string SubDivision = "SubDivision";
		public static string SupplyDivision = "SupplyDivision";
		public static string BeginDate = "BeginDate";
		public static string EndDate = "EndDate";
		public static string UpdateReason = "UpdateReason";


		public static string RealItemOrganizationInfoIndex = "RealItemOrganizationInfoIndex";




		public static string EquipmentInfoIndex = "EquipmentInfoIndex";
		public static string EquipmentNum = "EquipmentNum";
		public static string EquipmentName = "EquipmentName";
		public static string EquipmentClassification = "EquipmentClassification";
		public static string Capacity = "Capacity";
		public static string ElectricCapacity = "ElectricCapacity";
		public static string UnitTimeUseCost = "UnitTimeUseCost";
		public static string WCInfoIndex = "WCInfoIndex";
		public static string InjectionStaffNum = "InjectionStaffNum";
		public static string Location = "Location";
		public static string OccupancyArea = "OccupancyArea";
		public static string BuyingDate = "BuyingDate";
		public static string BuyingCost = "BuyingCost";
		public static string InstrumentNum = "InstrumentNum";
		public static string EquipmentState = "EquipmentState";
		public static string CheckDate = "CheckDate";
		public static string ValidPeriod = "ValidPeriod";
		public static string CheckAgency = "CheckAgency";
		public static string ReadyTime = "ReadyTime";
		public static string Cavity = "Cavity";
		public static string ValidityYear = "ValidityYear";
		public static string DesignShot = "DesignShot";
		public static string FirstShot = "FirstShot";
		public static string TotalShot = "TotalShot";
		public static string WorkShot = "WorkShot";
		public static string ManagePeriod1 = "ManagePeriod1";
		public static string ManageDate1 = "ManageDate1";
		public static string ManagePeriod2 = "ManagePeriod2";
		public static string ManageDate2 = "ManageDate2";
		public static string ManagePeriod3 = "ManagePeriod3";
		public static string ManageDate3 = "ManageDate3";
		public static string ManagePeriod4 = "ManagePeriod4";
		public static string ManageDate4 = "ManageDate4";



		//public static string WCInfoIndex = "WCInfoIndex";
		public static string WCName = "WCName";
		public static string MainProcessCode = "MainProcessCode";
		public static string RetentionStaff = "RetentionStaff";
		public static string State = "State";
		public static string CapacityDistinction = "CapacityDistinction";
		public static string OperationTime = "OperationTime";
		public static string Sorting = "Sorting";



		public static string ProcessSequenceInfoIndex = "ProcessSequenceInfoIndex";
		public static string ProcessSequenceNum = "ProcessSequenceNum";
		public static string ProcessCode = "ProcessCode";
		public static string WorkDistinction = "WorkDistinction";
		public static string OutsideOrderRate = "OutsideOrderRate";
		public static string ProgressRate = "ProgressRate";
		public static string LeadTime = "LeadTime";
		public static string EtcText = "EtcText";


		public static string RealProcessSequenceInfoIndex = "RealProcessSequenceInfoIndex";



		public static string WorkStandardInfoIndex = "WorkStandardInfoIndex";
		public static string PriorityOrder = "PriorityOrder";
		public static string MainWorkerID = "MainWorkerID";
		public static string MainWorker = "MainWorker";
		public static string ToolName1 = "ToolName1";
		public static string JigName1 = "JigName1";
		public static string ToolName2 = "ToolName2";
		public static string JigName2 = "JigName2";
		public static string ToolName3 = "ToolName3";
		public static string JigName3 = "JigName3";
		public static string SetupTime = "SetupTime";
		public static string RealProcessingTime = "RealProcessingTime";
		public static string SpaceTime = "SpaceTime";
		public static string StandardTime = "StandardTime";
		public static string WaitTime = "WaitTime";
		public static string LotSize = "LotSize";


		public static string RealWorkStandardInfoIndex = "RealWorkStandardInfoIndex";




		public static string UnitCostInfoIndex = "UnitCostInfoIndex";
		public static string UnitCostDistinction = "UnitCostDistinction";
		public static string BeginProcessCode = "BeginProcessCode";
		public static string EndProcessCode = "EndProcessCode";
		public static string OrderRate = "OrderRate";
		public static string DiscountUnitCost = "DiscountUnitCost";


		




		public static string UserInfoIndex = "UserInfoIndex";
		public static string ID = "ID";
		public static string Password = "Password";
		public static string Name = "Name";
		public static string PostCode = "PostCode";
		public static string Responsibility = "Responsibility";
		public static string IdentificationNumber = "IdentificationNumber";
		public static string Telephone1 = "Telephone1";
		public static string Telephone2 = "Telephone2";
		public static string Address = "Address";
		public static string EnterDate = "EnterDate";
		public static string HomepageCompetence = "HomepageCompetence";
		public static string StandardiInfoCompetence = "StandardiInfoCompetence";
		public static string UserRank = "UserRank";
		public static string Picture = "Picture";
		public static string EMail = "EMail";
		public static string ShotcutUserMenuPage1 = "ShotcutUserMenuPage1";
		public static string ShotcutUserMenuPage2 = "ShotcutUserMenuPage2";
		public static string ShotcutUserMenuPage3 = "ShotcutUserMenuPage3";
		public static string ShotcutUserMenuPage4 = "ShotcutUserMenuPage4";
		public static string ShotcutUserMenuPage5 = "ShotcutUserMenuPage5";
		public static string ShotcutUserMenuPage6 = "ShotcutUserMenuPage6";
		public static string ShotcutUserMenuPage7 = "ShotcutUserMenuPage7";
		public static string ShotcutUserMenuPage8 = "ShotcutUserMenuPage8";
		public static string ShotcutUserMenuPage9 = "ShotcutUserMenuPage9";
		public static string ShotcutUserMenuPage10 = "ShotcutUserMenuPage10";
		public static string ShotcutUserMenuPage11 = "ShotcutUserMenuPage11";
		public static string ShotcutUserMenuPage12 = "ShotcutUserMenuPage12";
		public static string ShotcutUserMenuPage13 = "ShotcutUserMenuPage13";
		public static string ShotcutUserMenuPage14 = "ShotcutUserMenuPage14";
		public static string ShotcutUserMenuPage15 = "ShotcutUserMenuPage15";
		public static string ShotcutUserMenuPage16 = "ShotcutUserMenuPage16";
		public static string ShotcutUserMenuPage17 = "ShotcutUserMenuPage17";
		public static string ShotcutUserMenuPage18 = "ShotcutUserMenuPage18";
		public static string ShotcutUserMenuPage19 = "ShotcutUserMenuPage19";
		public static string ShotcutUserMenuPage20 = "ShotcutUserMenuPage20";
		public static string WorkDiary = "WorkDiary";





		public static string ProductionCalendarInfoIndex = "ProductionCalendarInfoIndex";
		public static string WC_Year = "WC_Year";
		public static string WC_Month = "WC_Month";
		public static string WC_Day = "WC_Day";
		public static string WC_Time = "WC_Time";
		public static string Content = "Content";



		public static string BusinessPlanInfoIndex = "BusinessPlanInfoIndex";
		public static string PlanYear = "PlanYear";
		public static string PlanMonth = "PlanMonth";
		public static string PlanDay = "PlanDay";
		public static string PlanQuantity = "PlanQuantity";
		public static string SaleUnitCost = "SaleUnitCost";
		public static string PlanTotalCost = "PlanTotalCost";



		public static string ExecutionPlanInfoIndex = "ExecutionPlanInfoIndex";
		public static string PlanDate = "PlanDate";
		


		public static string MonthClosingInfoIndex = "MonthClosingInfoIndex";
		public static string AffairDistinction = "AffairDistinction";
		public static string ClosingYear = "ClosingYear";
		public static string ClosingMonth = "ClosingMonth";
		public static string ClosingPerson = "ClosingPerson";
		public static string ClosingPersonID = "ClosingPersonID";



		public static string BuyingSaleInfoIndex = "BuyingSaleInfoIndex";
		public static string SaleDistinction = "SaleDistinction";
		public static string BuyingDistinction = "BuyingDistinction";
		public static string LastYearSaleTransferCost = "LastYearSaleTransferCost";
		public static string LastYearBuyingTransferCost = "LastYearBuyingTransferCost";
		public static string TotalSaleCost1 = "TotalSaleCost1";
		public static string CollectMoney1 = "CollectMoney1";
		public static string UncollectMoney1 = "UncollectMoney1";
		public static string TotalBuyingCost1 = "TotalBuyingCost1";
		public static string PaymentMoney1 = "PaymentMoney1";
		public static string UnPaymentMoney1 = "UnPaymentMoney1";
		public static string TotalSaleCost2 = "TotalSaleCost2";
		public static string CollectMoney2 = "CollectMoney2";
		public static string UncollectMoney2 = "UncollectMoney2";
		public static string TotalBuyingCost2 = "TotalBuyingCost2";
		public static string PaymentMoney2 = "PaymentMoney2";
		public static string UnPaymentMoney2 = "UnPaymentMoney2";
		public static string TotalSaleCost3 = "TotalSaleCost3";
		public static string CollectMoney3 = "CollectMoney3";
		public static string UncollectMoney3 = "UncollectMoney3";
		public static string TotalBuyingCost3 = "TotalBuyingCost3";
		public static string PaymentMoney3 = "PaymentMoney3";
		public static string UnPaymentMoney3 = "UnPaymentMoney3";
		public static string TotalSaleCost4 = "TotalSaleCost4";
		public static string CollectMoney4 = "CollectMoney4";
		public static string UncollectMoney4 = "UncollectMoney4";
		public static string TotalBuyingCost4 = "TotalBuyingCost4";
		public static string PaymentMoney4 = "PaymentMoney4";
		public static string UnPaymentMoney4 = "UnPaymentMoney4";
		public static string TotalSaleCost5 = "TotalSaleCost5";
		public static string CollectMoney5 = "CollectMoney5";
		public static string UncollectMoney5 = "UncollectMoney5";
		public static string TotalBuyingCost5 = "TotalBuyingCost5";
		public static string PaymentMoney5 = "PaymentMoney5";
		public static string UnPaymentMoney5 = "UnPaymentMoney5";
		public static string TotalSaleCost6 = "TotalSaleCost6";
		public static string CollectMoney6 = "CollectMoney6";
		public static string UncollectMoney6 = "UncollectMoney6";
		public static string TotalBuyingCost6 = "TotalBuyingCost6";
		public static string PaymentMoney6 = "PaymentMoney6";
		public static string UnPaymentMoney6 = "UnPaymentMoney6";
		public static string TotalSaleCost7 = "TotalSaleCost7";
		public static string CollectMoney7 = "CollectMoney7";
		public static string UncollectMoney7 = "UncollectMoney7";
		public static string TotalBuyingCost7 = "TotalBuyingCost7";
		public static string PaymentMoney7 = "PaymentMoney7";
		public static string UnPaymentMoney7 = "UnPaymentMoney7";
		public static string TotalSaleCost8 = "TotalSaleCost8";
		public static string CollectMoney8 = "CollectMoney8";
		public static string UncollectMoney8 = "UncollectMoney8";
		public static string TotalBuyingCost8 = "TotalBuyingCost8";
		public static string PaymentMoney8 = "PaymentMoney8";
		public static string UnPaymentMoney8 = "UnPaymentMoney8";
		public static string TotalSaleCost9 = "TotalSaleCost9";
		public static string CollectMoney9 = "CollectMoney9";
		public static string UncollectMoney9 = "UncollectMoney9";
		public static string TotalBuyingCost9 = "TotalBuyingCost9";
		public static string PaymentMoney9 = "PaymentMoney9";
		public static string UnPaymentMoney9 = "UnPaymentMoney9";
		public static string TotalSaleCost10 = "TotalSaleCost10";
		public static string CollectMoney10 = "CollectMoney10";
		public static string UncollectMoney10 = "UncollectMoney10";
		public static string TotalBuyingCost10 = "TotalBuyingCost10";
		public static string PaymentMoney10 = "PaymentMoney10";
		public static string UnPaymentMoney10 = "UnPaymentMoney10";
		public static string TotalSaleCost11 = "TotalSaleCost11";
		public static string CollectMoney11 = "CollectMoney11";
		public static string UncollectMoney11 = "UncollectMoney11";
		public static string TotalBuyingCost11 = "TotalBuyingCost11";
		public static string PaymentMoney11 = "PaymentMoney11";
		public static string UnPaymentMoney11 = "UnPaymentMoney11";
		public static string TotalSaleCost12 = "TotalSaleCost12";
		public static string CollectMoney12 = "CollectMoney12";
		public static string UncollectMoney12 = "UncollectMoney12";
		public static string TotalBuyingCost12 = "TotalBuyingCost12";
		public static string PaymentMoney12 = "PaymentMoney12";
		public static string UnPaymentMoney12 = "UnPaymentMoney12";



		public static string LastYearTransferQuantity = "LastYearTransferQuantity";
		public static string LastYearTransferCost = "LastYearTransferCost";
		public static string InStorehouseQuantity1 = "InStorehouseQuantity1";
		public static string InStorehouseCost1 = "InStorehouseCost1";
		public static string OutStorehouseQuantity1 = "OutStorehouseQuantity1";
		public static string OutStorehouseCost1 = "OutStorehouseCost1";
		public static string StockQuantity1 = "StockQuantity1";
		public static string StockCost1 = "StockCost1";
		public static string InStorehouseQuantity2 = "InStorehouseQuantity2";
		public static string InStorehouseCost2 = "InStorehouseCost2";
		public static string OutStorehouseQuantity2 = "OutStorehouseQuantity2";
		public static string OutStorehouseCost2 = "OutStorehouseCost2";
		public static string StockQuantity2 = "StockQuantity2";
		public static string StockCost2 = "StockCost2";
		public static string InStorehouseQuantity3 = "InStorehouseQuantity3";
		public static string InStorehouseCost3 = "InStorehouseCost3";
		public static string OutStorehouseQuantity3 = "OutStorehouseQuantity3";
		public static string OutStorehouseCost3 = "OutStorehouseCost3";
		public static string StockQuantity3 = "StockQuantity3";
		public static string StockCost3 = "StockCost3";
		public static string InStorehouseQuantity4 = "InStorehouseQuantity4";
		public static string InStorehouseCost4 = "InStorehouseCost4";
		public static string OutStorehouseQuantity4 = "OutStorehouseQuantity4";
		public static string OutStorehouseCost4 = "OutStorehouseCost4";
		public static string StockQuantity4 = "StockQuantity4";
		public static string StockCost4 = "StockCost4";
		public static string InStorehouseQuantity5 = "InStorehouseQuantity5";
		public static string InStorehouseCost5 = "InStorehouseCost5";
		public static string OutStorehouseQuantity5 = "OutStorehouseQuantity5";
		public static string OutStorehouseCost5 = "OutStorehouseCost5";
		public static string StockQuantity5 = "StockQuantity5";
		public static string StockCost5 = "StockCost5";
		public static string InStorehouseQuantity6 = "InStorehouseQuantity6";
		public static string InStorehouseCost6 = "InStorehouseCost6";
		public static string OutStorehouseQuantity6 = "OutStorehouseQuantity6";
		public static string OutStorehouseCost6 = "OutStorehouseCost6";
		public static string StockQuantity6 = "StockQuantity6";
		public static string StockCost6 = "StockCost6";
		public static string InStorehouseQuantity7 = "InStorehouseQuantity7";
		public static string InStorehouseCost7 = "InStorehouseCost7";
		public static string OutStorehouseQuantity7 = "OutStorehouseQuantity7";
		public static string OutStorehouseCost7 = "OutStorehouseCost7";
		public static string StockQuantity7 = "StockQuantity7";
		public static string StockCost7 = "StockCost7";
		public static string InStorehouseQuantity8 = "InStorehouseQuantity8";
		public static string InStorehouseCost8 = "InStorehouseCost8";
		public static string OutStorehouseQuantity8 = "OutStorehouseQuantity8";
		public static string OutStorehouseCost8 = "OutStorehouseCost8";
		public static string StockQuantity8 = "StockQuantity8";
		public static string StockCost8 = "StockCost8";
		public static string InStorehouseQuantity9 = "InStorehouseQuantity9";
		public static string InStorehouseCost9 = "InStorehouseCost9";
		public static string OutStorehouseQuantity9 = "OutStorehouseQuantity9";
		public static string OutStorehouseCost9 = "OutStorehouseCost9";
		public static string StockQuantity9 = "StockQuantity9";
		public static string StockCost9 = "StockCost9";
		public static string InStorehouseQuantity10 = "InStorehouseQuantity10";
		public static string InStorehouseCost10 = "InStorehouseCost10";
		public static string OutStorehouseQuantity10 = "OutStorehouseQuantity10";
		public static string OutStorehouseCost10 = "OutStorehouseCost10";
		public static string StockQuantity10 = "StockQuantity10";
		public static string StockCost10 = "StockCost10";
		public static string InStorehouseQuantity11 = "InStorehouseQuantity11";
		public static string InStorehouseCost11 = "InStorehouseCost11";
		public static string OutStorehouseQuantity11 = "OutStorehouseQuantity11";
		public static string OutStorehouseCost11 = "OutStorehouseCost11";
		public static string StockQuantity11 = "StockQuantity11";
		public static string StockCost11 = "StockCost11";
		public static string InStorehouseQuantity12 = "InStorehouseQuantity12";
		public static string InStorehouseCost12 = "InStorehouseCost12";
		public static string OutStorehouseQuantity12 = "OutStorehouseQuantity12";
		public static string OutStorehouseCost12 = "OutStorehouseCost12";
		public static string StockQuantity12 = "StockQuantity12";
		public static string StockCost12 = "StockCost12";



		public static string ProductionStorehouseIndex = "ProductionStorehouseIndex";


		
		public static string OutSideStorehouseIndex = "OutSideStorehouseIndex";




		public static string BusinessStorehouseSequenceNum = "BusinessStorehouseSequenceNum";
		public static string BusinessStorehouseNum = "BusinessStorehouseNum";


		public static string DeliveryStorehouseIndex = "DeliveryStorehouseIndex";
	}


	/// <summary>
	/// 원장필드이름
	/// </summary>
	public class HistoryTableField
	{
		//수주원장
		public static string CompanyName = "CompanyName";
		public static string BusinessRegistrationNum = "BusinessRegistrationNum";
		public static string ItemNum = "ItemNum";
		public static string ItemDrawNum = "ItemDrawNum";
		public static string ItemName = "ItemName";
		public static string PropertyClassification = "PropertyClassification";
		public static string ProductionRequestDivision = "ProductionRequestDivision";	//생산의뢰여부
		public static string ReceivingOrderDate = "ReceivingOrderDate";
		public static string ApplyUnitCost = "ApplyUnitCost";
		public static string DeliveryRequestQuantity1 = "DeliveryRequestQuantity1";
		public static string DeliveryRequestDate1 = "DeliveryRequestDate1";
		public static string DeliveryRequestQuantity2 = "DeliveryRequestQuantity2";
		public static string DeliveryRequestDate2 = "DeliveryRequestDate2";
		public static string DeliveryRequestQuantity3 = "DeliveryRequestQuantity3";
		public static string DeliveryRequestDate3 = "DeliveryRequestDate3";
		public static string DeliveryRequestQuantity4 = "DeliveryRequestQuantity4";
		public static string DeliveryRequestDate4 = "DeliveryRequestDate4";
		public static string DeliveryRequestQuantity5 = "DeliveryRequestQuantity5";
		public static string DeliveryRequestDate5 = "DeliveryRequestDate5";
		public static string TotalReceiveingOrderQuantity = "TotalReceiveingOrderQuantity";
		public static string TotalCost = "TotalCost";
		public static string OutStorehouseQuantity = "OutStorehouseQuantity";
		public static string SuitabilityQuantity = "SuitabilityQuantity";
		public static string UnInspectionQuantity = "UnInspectionQuantity";
		public static string RemainderQuantity = "RemainderQuantity";
		public static string OrderNum = "OrderNum";
		public static string DeliveryPlace = "DeliveryPlace";
		public static string VolumNum = "VolumNum";
		public static string ProgressCondition = "ProgressCondition";
		public static string RegistrationPerson = "RegistrationPerson";
		public static string RegistrationPersonID = "RegistrationPersonID";
		public static string RegistrationDate = "RegistrationDate";
		public static string UpdatingPerson = "UpdatingPerson";
		public static string UpdatingPersonID = "UpdatingPersonID";
		public static string UpdatingDate = "UpdatingDate";
		public static string ReceivingOrderHistoryIndex = "ReceivingOrderHistoryIndex";


		//생산의뢰원장
		public static string ProductionRequestSourceCode = "ProductionRequestSourceCode";
		public static string ProductionRequestSource = "ProductionRequestSource";
		public static string ProductionRequestQuantity = "ProductionRequestQuantity";
		public static string RequestQuantity1 = "RequestQuantity1";
		public static string RequestDate1 = "RequestDate1";
		public static string RequestQuantity2 = "RequestQuantity2";
		public static string RequestDate2 = "RequestDate2";
		public static string RequestQuantity3 = "RequestQuantity3";
		public static string RequestDate3 = "RequestDate3";
		public static string RequestQuantity4 = "RequestQuantity4";
		public static string RequestDate4 = "RequestDate4";
		public static string RequestQuantity5 = "RequestQuantity5";
		public static string RequestDate5 = "RequestDate5";
		public static string ProductionRequestHistoryIndex = "ProductionRequestHistoryIndex";


		//품질검사원장
		public static string QualityInspectionCompleteDate = "QualityInspectionCompleteDate";
		public static string RequestQuantity = "RequestQuantity";
		public static string UnSuitabilityCost = "UnSuitabilityCost";
		public static string BeginProcessSequenceNum = "BeginProcessSequenceNum";
		public static string EndProcessSequenceNum = "EndProcessSequenceNum";
		public static string BeginProcessName = "BeginProcessName";
		public static string EndProcessName = "EndProcessName";
		public static string InspectionDecisionCode = "InspectionDecisionCode";
		public static string InspectionDecisionMeaning = "InspectionDecisionMeaning";
		public static string UnSuitabilityCauseCode = "UnSuitabilityCauseCode";
		public static string UnSuitabilityCauseMeaning = "UnSuitabilityCauseMeaning";
		public static string UnSuitabilityStatusCode = "UnSuitabilityStatusCode";
		public static string UnSuitabilityStatusMeaning = "UnSuitabilityStatusMeaning";
		public static string UnSuitabilityDetailMeaning = "UnSuitabilityDetailMeaning";
		public static string Investigator = "Investigator";
		public static string InvestigatorID = "InvestigatorID";
		public static string QualityInspectionHistoryIndex = "QualityInspectionHistoryIndex";
		public static string HistoryIndex1 = "HistoryIndex1";
		public static string HistorySection1 = "HistorySection1";
		public static string HistoryIndex2 = "HistoryIndex2";
		public static string HistorySection2 = "HistorySection2";



		// 기타입출고원장
		public static string StorehouseName = "StorehouseName"; 
		public static string BusinessStorehouseNum = "BusinessStorehouseNum";
		public static string InOutStorehouseDistinction = "InOutStorehouseDistinction";
		public static string InOutStorehouseQuantity = "InOutStorehouseQuantity";
		public static string InOutStorehouseReasonCode = "InOutStorehouseReasonCode";
		public static string InOutStorehouseReason = "InOutStorehouseReason";
		public static string InOutStorehouseDetailReason = "InOutStorehouseDetailReason";
		public static string OtherInOutStorehouseHistoryIndex = "OtherInOutStorehouseHistoryIndex";


		//입고원장
		public static string InStorehouseQuantity = "InStorehouseQuantity";
		public static string InStorehouseHistoryIndex = "InStorehouseHistoryIndex";


		//출고원장
		public static string UnSuitabilityCause = "UnSuitabilityCause";
		public static string UnSuitabilityStatus = "UnSuitabilityStatus";
		public static string InspectionDecision = "InspectionDecision";
		public static string ItemizeAccountNum = "ItemizeAccountNum";
		public static string OutStorehouseHistoryIndex = "OutStorehouseHistoryIndex";



		//매출원장
		public static string SupplementaryValueTaxRate = "SupplementaryValueTaxRate";
		public static string BillNum = "BillNum";
		public static string SupplementaryValueTaxFloatationDate = "SupplementaryValueTaxFloatationDate";
		public static string SaleHistoryIndex = "SaleHistoryIndex";



		//매입원장
		public static string BuyingQuantity = "BuyingQuantity";
		public static string BuyingHistoryIndex = "BuyingHistoryIndex";



		//지급원장
		public static string PaymentDate = "PaymentDate";
		public static string ItemPaymentCost = "ItemPaymentCost";
		public static string SupplementaryValueTaxPaymentCost = "SupplementaryValueTaxPaymentCost";
		public static string DecisionMethodCode = "DecisionMethodCode";
		public static string DecisionMethod = "DecisionMethod";
		public static string ProofData = "ProofData";
		public static string BillNum1 = "BillNum1";
		public static string BillPaymentDate1 = "BillPaymentDate1";
		public static string BankCode1 = "BankCode1";
		public static string BankName1= "BankName1";
		public static string BillNum2 = "BillNum2";
		public static string BillPaymentDate2 = "BillPaymentDate2";
		public static string BankCode2 = "BankCode2";
		public static string BankName2 = "BankName2";
		public static string BillNum3 = "BillNum3";
		public static string BillPaymentDate3 = "BillPaymentDate3";
		public static string BankCode3 = "BankCode3";
		public static string BankName3 = "BankName3";
		public static string PaymentHistoryIndex = "PaymentHistoryIndex";


		//수금원장
		public static string CollectMoneyDate = "CollectMoneyDate";
		public static string ItemCollectMoneyCost = "ItemCollectMoneyCost";
		public static string SupplementaryValueTaxCollectMoneyCost= "SupplementaryValueTaxCollectMoneyCost";
		public static string CollectMoneyHistoryIndex= "CollectMoneyHistoryIndex";


		//창고이동원장
		public static string OriginalStorehouseName = "OriginalStorehouseName";
		public static string MovingStorehoseName = "MovingStorehoseName";
		public static string BusinessStorehouseNum1 = "";//원영업창고번호
		public static string BusinessStorehouseNum2 = "";//이동영업창고번호
		public static string MovingQuantity = "MovingQuantity";
		public static string StorehouseMovingHistoryIndex = "StorehouseMovingHistoryIndex";



		//생산계획원장
		public static string ProductionPlanHistorySourceCode = "ProductionPlanHistorySourceCode";
		public static string ProductionPlanHistorySource = "ProductionPlanHistorySource";
		public static string ProductionPlanQuantity = "ProductionPlanQuantity";
		public static string ProductionBeginDate = "ProductionBeginDate";
		public static string RowMetarialCalculation = "RowMetarialCalculation";
		public static string ProductionPlanHistoryIndex = "ProductionPlanHistoryIndex";



		//자재소요원장
		public static string DeliveryOrderRequestQuantity = "DeliveryOrderRequestQuantity";
		public static string RowMaterialQuantityReflection = "RowMaterialQuantityReflection";
		public static string OrderNonInStorehouseReflection = "OrderNonInStorehouseReflection";
		public static string OrderRequestReadyQuantityReflection = "OrderRequestReadyQuantityReflection";
		public static string SafyRowMetiralQuantityReflection = "SafyRowMetiralQuantityReflection";
		public static string MinimumOrderQuantityReflection = "MinimumOrderQuantityReflection";
		public static string OrderGapQuantityReflection = "OrderGapQuantityReflection";
		public static string MaterialRequirementCalculateHistoryIndex = "MaterialRequirementCalculateHistoryIndex";


		//작업계획원장
		public static string ProcessCode = "ProcessCode";
		public static string ProcessName = "ProcessName";
		public static string ProcessSequenceNum = "ProcessSequenceNum";
		public static string WorkPlanQuantity = "WorkPlanQuantity";
		public static string WorkDistinction = "WorkDistinction";
		public static string ProductName = "ProductName";
		public static string ProductDrawNum = "ProductDrawNum";
		public static string ProductItemNum = "ProductItemNum";
		public static string ParentName = "ParentName";
		public static string ParentDrawNum ="ParentDrawNum";
		public static string ParentItemNum = "ParentItemNum";
		public static string UnSuitabilityQuantity = "UnSuitabilityQuantity";
		public static string WorkPlanHistoryIndex = "WorkPlanHistoryIndex";



		//WC별 일자별작업계획원장
		public static string WCInfoIndex = "WCInfoIndex";
		public static string WCName = "WCName";
		public static string WorkDate = "WorkDate";
		public static string WorkCompletionQuantity ="WorkCompletionQuantity";
		public static string WorkBeginTime = "WorkBeginTime";
		public static string WorkEndTime = "WorkEndTime";
		public static string OrderLeadTime = "OrderLeadTime";
		public static string WCDailyWorkPlanHistoryIndex ="WCDailyWorkPlanHistoryIndex";


		//작업일보원장
		public static string WorkDailyReportHistoryIndex = "WorkDailyReportHistoryIndex";
		public static string RemainQuantity = "RemainQuantity";
		public static string UnSuittabilityQuantity = "UnSuittabilityQuantity";
		public static string Worker ="Worker";
		public static string WorkerID ="WorkerID";
		public static string NonWorkTime1 = "NonWorkTime1";
		public static string NonWorkTimeCode1 = "NonWorkTimeCode1";
		public static string NonWorkTimeReason1 = "NonWorkTimeReason1";
		public static string NonWorkTime2 = "NonWorkTime2";
		public static string NonWorkTimeCode2 = "NonWorkTimeCode2";
		public static string NonWorkTimeReason2 = "NonWorkTimeReason2";
		public static string NonWorkTime3 = "NonWorkTime3";
		public static string NonWorkTimeCode3 = "NonWorkTimeCode3";
		public static string NonWorkTimeReason3 = "NonWorkTimeReason3";
		public static string NonSuittabilityQuantitySituation = "NonSuittabilityQuantitySituation";
		public static string NonSuittabilityFactor = "NonSuittabilityFactor";
		public static string DetailContents = "DetailContents";
		public static string UseTool1 = "UseTool1";
		public static string UseJig1 = "UseJig1";
		public static string UseTool2 = "UseTool2";
		public static string UseJig2 = "UseJig2";
		public static string UseTool3 = "UseTool3";
		public static string UseJig3 = "UseJig3";



		//사용회사정보
		public static string PresidentName = "PresidentName";
		public static string CorporationRegistrationNum = "CorporationRegistrationNum";
		public static string BusinessCompanyAddress = "BusinessCompanyAddress";
		public static string BusinessBillAddress = "BusinessBillAddress";
		public static string TaxBillAddress = "TaxBillAddress";
		public static string BusinessClassification = "BusinessClassification";
		public static string BusinessItem = "BusinessItem";


		//시스템 기능세팅
		public static string SystemCapabilitySetIndex = "SystemCapabilitySetIndex";
		public static string PresentRowMetrialUsed = "PresentRowMetrialUsed";
		public static string SafeyRowMetrialUsed = "SafeyRowMetrialUsed";
		public static string OrderNonInStorehouseUsed = "OrderNonInStorehouseUsed";
		public static string OrderGapInStorehouseUsed = "OrderGapInStorehouseUsed";
		public static string MinimumGapUsed = "MinimumGapUsed";
		public static string OrderRequestStandbyUsed = "OrderRequestStandbyUsed";
		public static string ContinuityWorkProgress = "ContinuityWorkProgress";
		public static string DailyWorkProgress ="DailyWorkProgress";
		public static string MinusRowMetrialPermissionUsed = "MinusRowMetrialPermissionUsed";



		//데이터백업복구
		public static string DataBackUpRestorationIndex = "DataBackUpRestorationIndex";
		public static string Date = "Date";
		public static string User = "User";
		public static string FileName = "FileName";


		//페이지권한 설정
		//		public static string PageAuthorityIndex = "PageAuthorityIndex";
		//		public static string Grade = "Grade";
		//		public static string CompanyInfo = "CompanyInfo";
		//		public static string ItemInfo = "ItemInfo";
		//		public static string ItemOrganizationInfo = "ItemOrganizationInfo";
		//		public static string EquipmentInfo = "EquipmentInfo";
		//		public static string WCInfo = "WCInfo";
		//		public static string ProcessSequence = "ProcessSequence";
		//		public static string WorkStandardInfo = "WorkStandardInfo";
		//		public static string SaleUnitCodeInfo = "SaleUnitCodeInfo";
		//		public static string OutSideOrderUnitCodeInfo = "OutSideOrderUnitCodeInfo";
		//		public static string BuyingUnitCodeInfo = "BuyingUnitCodeInfo";
		//		public static string UserInfo = "UserInfo";
		//		public static string PublicUseCode = "PublicUseCode";
		//		public static string StandardProductionCalendarInfo = "StandardProductionCalendarInfo";
		//		public static string WCProductionCalendarInfo = "WCProductionCalendarInfo";
		//		public static string BusinessPlanInfo = "BusinessPlanInfo";
		//		public static string ExecutionPlanInfo = "ExecutionPlanInfo";
		//		public static string MonthlyClosingInfo = "MonthlyClosingInfo";
		//		public static string ReceiveingRegistration = "ReceiveingRegistration";
		//		public static string ReceiveingbundleRegistration = "ReceiveingbundleRegistration";
		//		public static string ReceiveingPC = "ReceiveingPC";
		//		public static string ReceiveingProgressLook = "ReceiveingProgressLook";
		//		public static string ProductionRequest = "ProductionRequest";
		//		public static string ProductionRequestPC = "ProductionRequestPC";
		//		public static string ProductionRequestAdd = "ProductionRequestAdd";
		//		public static string GoodsBuyingRequest = "GoodsBuyingRequest";
		//		public static string GoodsBuyingRequestPC = "GoodsBuyingRequestPC";
		//		public static string GoodsBuyingRequestAdd = "GoodsBuyingRequestAdd";
		//		public static string BusinessStorehouseInStorehouse = "BusinessStorehouseInStorehouse";
		//		public static string BusinessStorehouseInStorehousePC = "BusinessStorehouseInStorehousePC";
		//		public static string GoodsManufactureOutStorehouse = "GoodsManufactureOutStorehouse";
		//		public static string GoodsManufactureOutStorehousePC = "GoodsManufactureOutStorehousePC";
		//		public static string SaleHistoryRegistration = "SaleHistoryRegistration";
		//		public static string SaleHistoryRegistrationPC = "SaleHistoryRegistrationPC";
		//		public static string CollectMoneyRegistration = "CollectMoneyRegistration";
		//		public static string CollectMoneyRegistrationPC = "CollectMoneyRegistrationPC";
		//		public static string UnCollectMoneyLook = "UnCollectMoneyLook";
		//		public static string StorehouseMoving = "StorehouseMoving";
		//		public static string StorehouseMovingPC = "StorehouseMovingPC";
		//		public static string OtherInOutStorehouse = "OtherInOutStorehouse";
		//		public static string OtherInOutStorehousePC = "OtherInOutStorehousePC";
		//		public static string ProductionPlan = "ProductionPlan";
		//		public static string ProductionPlanPC = "ProductionPlanPC";
		//		public static string ProductionResultPC = "ProductionResultPC";
		//		public static string ProductionPlanAdd = "ProductionPlanAdd";
		public static string P05_RowMetarialRequirementCalculate = "P05_RowMetarialRequirementCalculate";
		public static string P06_RowMetarialRequirementCalculatePC = "P06_RowMetarialRequirementCalculatePC";
		//		public static string WorkPlanEstablish = "WorkPlanEstablish";
		//		public static string WCPlanPC = "WCPlanPC";
		//		public static string WorkDailyReportRegistration = "WorkDailyReportRegistration";
		//		public static string WorkDailyReportRegistrationPC = "WorkDailyReportRegistrationPC";
		//		public static string RowMetarialRequriement = "RowMetarialRequriement";
		//		public static string RowMetarialRequriementPC = "RowMetarialRequriementPC";
		//		public static string RowMetarialRequriementAdd = "RowMetarialRequriementAdd";
		//		public static string OutsideRequest = "OutsideRequest";
		//		public static string OutsideRequestPC = "OutsideRequestPC";
		//		public static string BuyingOrder = "BuyingOrder";
		//		public static string BuyingOrderPC = "BuyingOrderPC";
		//		public static string BuyingProgressLook = "BuyingProgressLook";
		//		public static string BuyingOrderAdd = "BuyingOrderAdd";
		//		public static string BuyingDelievery = "BuyingDelievery";
		//		public static string BuyingDelieveryPC = "BuyingDelieveryPC";
		//		public static string OutSideOrder = "OutSideOrder";
		//		public static string OutSideOrderPC = "OutSideOrderPC";
		//		public static string OutSideProgressLook = "OutSideProgressLook";
		//		public static string OutSideOutStorehouse = "OutSideOutStorehouse";
		//		public static string OutSideOutStorehousePC = "OutSideOutStorehousePC";
		//		public static string OutSideDelievery = "OutSideDelievery";
		//		public static string OutSideDelieveryPC = "OutSideDelieveryPC";
		//		public static string PaymentPlanResultRegistraton = "PaymentPlanResultRegistraton";
		//		public static string PaymentPlanResultRegistratonPC = "PaymentPlanResultRegistratonPC";
		//		public static string QualityInspectionRegistration = "QualityInspectionRegistration";
		//		public static string QualityInspectionPC = "QualityInspectionPC";
		//		public static string QualityInspectionStatistics = "QualityInspectionStatistics";
		//		public static string IncongruityPresentIndex = "IncongruityPresentIndex";
		//		public static string IncongruityCauseIndex = "IncongruityCauseIndex";
		//		public static string AutonomyInspectPC = "AutonomyInspectPC";
		//		public static string AutonomyInspectStatistics = "AutonomyInspectStatistics";
		//		public static string AutonomyInspectIndex = "AutonomyInspectIndex";
		//		public static string MoneyStandardIndex = "MoneyStandardIndex";
		//		public static string QuantityStandardIndex = "QuantityStandardIndex";
		//		public static string LotStandardIndex = "LotStandardIndex";
		//		public static string BizPlanResultIndex = "BizPlanResultIndex";
		//		public static string SaleTatalProfitIndex = "SaleTatalProfitIndex";
		//		public static string SaleIndex = "SaleIndex";
		//		public static string BuyingIndex = "BuyingIndex";
		//		public static string ItemsStockIndex = "ItemsStockIndex";
		//		public static string StoreHouseStockIndex = "StoreHouseStockIndex";
		//		public static string StockMoneyIndex = "StockMoneyIndex";
		//		public static string WorkingRatioIndex = "WorkingRatioIndex";
		//		public static string NonWorkingRatioIndex = "NonWorkingRatioIndex";
		//		public static string ProductivityIndex = "ProductivityIndex";
		//		public static string AutonomyInspectSatistics = "AutonomyInspectSatistics";
		//		public static string MoneyStandardIndex = "MoneyStandardIndex";
		//		public static string QuantityStandardIndex = "QuantityStandardIndex";
		//		public static string LotStandardIndex = "LotStandardIndex";
		//		public static string UserCompanyInput = "UserCompanyInput";
		//		public static string SystemAbilitySet = "SystemAbilitySet";
		//		public static string StandardInforbundleRegistration = "StandardInforbundleRegistration";
		//		public static string DataBackUpRestoration = "DataBackUpRestoration";
		//		public static string HistoryAdjustment = "HistoryAdjustment";
		//		public static string OrderPC = "OrderPC";
		//		public static string DeliveryRequestPC = "DeliveryRequestPC";
		//		public static string OutStorehousePC = "OutStorehousePC";
		//		public static string DeliveryPC = "DeliveryPC";
		//		public static string StockPC = "StockPC";
		//		public static string QualityPC = "QualityPC";
		//		public static string QualityIndex = "QualityIndex";
		//		public static string Notice = "Notice";
		//		public static string NoRegistration = "NoRegistration";
		//		public static string NoAdjustment = "NoAdjustment";
		//		public static string FreeBoard = "FreeBoard";
		//		public static string FreetRegistration = "FreetRegistration";
		//		public static string FreeAdjustment = "FreeAdjustment";
		//		public static string GeneralData = "GeneralData";
		//		public static string GnRegistration = "GnRegistration";
		//		public static string GnAdjustment = "GnAdjustment";
		//		public static string TechnologyData = "TechnologyData";
		//		public static string TnRegistration = "TnRegistration";
		//		public static string TnAdjustment = "TnAdjustment";
		//		public static string CompanyStandard = "CompanyStandard";
		//		public static string CSRegistration = "CSRegistration";
		//		public static string CSAdjustment = "CSAdjustment";
		//		public static string RelationSite = "RelationSite";
		//		public static string Main = "Main";
		//		public static string Explanation = "Explanation";



		//페이지권한 설정
		public static string PageAuthorityIndex = "PageAuthorityIndex";
		public static string Grade ="Grade";			
		public static string B01_CompanyInfo ="B01_CompanyInfo";				
		public static string B02_ItemInfo ="B02_ItemInfo";				
		public static string B03_ItemOrganizationInfo ="B03_ItemOrganizationInfo";			
		public static string B04_WCInfo ="B04_WCInfo";				
		public static string B05_ProcessSequence ="B05_ProcessSequence";			
		public static string B06_EquipmentInfo ="B06_EquipmentInfo";				
		public static string B07_WorkStandardInfo ="B07_WorkStandardInfo";			
		public static string B08_SaleUnitCodeInfo ="B08_SaleUnitCodeInfo";			
		public static string B09_OutSideOrderUnitCodeInfo ="B09_OutSideOrderUnitCodeInfo";		
		public static string B10_BuyingUnitCodeInfo ="B10_BuyingUnitCodeInfo";			
		public static string B11_UserInfo ="B11_UserInfo";				
		public static string B12_PublicUseCode ="B12_PublicUseCode";				
		public static string B13_StandardProductionCalendarInfo ="B13_StandardProductionCalendarInfo";	
		public static string B14_WCProductionCalendarInf0="B14_WCProductionCalendarInf0";	
		public static string B15_BusinessPlanInfo ="B15_BusinessPlanInfo";			
		public static string B16_ExecutionPlanInfo ="B16_ExecutionPlanInfo";			
		public static string B17_MonthlyClosingInfo ="B17_MonthlyClosingInfo";			
		public static string S01_ReceiveingRegistration	 ="S01_ReceiveingRegistration";		
		public static string S02_ReceiveingbundleRegistration ="S02_ReceiveingbundleRegistration";		
		public static string S03_ReceiveingPC ="S03_ReceiveingPC";				
		public static string S04_ReceiveingProgressLook ="S04_ReceiveingProgressLook";	
		public static string S05_ProductionRequest ="S05_ProductionRequest";	
		public static string S06_ProductionRequestPC ="S06_ProductionRequestPC";		
		public static string S07_ProductionRequestAdd ="S07_ProductionRequestAdd";	
		public static string S08_GoodsBuyingRequest ="S08_GoodsBuyingRequest";	
		public static string S09_GoodsBuyingRequestPC ="S09_GoodsBuyingRequestPC";	
		public static string S10_GoodsBuyingRequestAdd ="S10_GoodsBuyingRequestAdd";	
		public static string S11_BusinessStorehouseInStorehouse ="S11_BusinessStorehouseInStorehouse";	
		public static string S12_BusinessStorehouseInStorehousePC ="S12_BusinessStorehouseInStorehousePC";	
		public static string S13_GoodsManufactureOutStorehouse ="S13_GoodsManufactureOutStorehouse";	
		public static string S14_GoodsManufactureOutStorehousePC ="S14_GoodsManufactureOutStorehousePC";	
		public static string S15_SaleHistoryRegistration ="S15_SaleHistoryRegistration";	
		public static string S16_SaleHistoryRegistrationPC ="S16_SaleHistoryRegistrationPC";	
		public static string S17_CollectMoneyRegistration ="S17_CollectMoneyRegistration";	
		public static string S18_CollectMoneyRegistrationPC ="S18_CollectMoneyRegistrationPC";	
		public static string S19_UnCollectMoneyLook ="S19_UnCollectMoneyLook";	
		public static string S20_StorehouseMoving ="S20_StorehouseMoving";	
		public static string S21_StorehouseMovingPC ="S21_StorehouseMovingPC";	
		public static string S22_OtherInOutStorehouse ="S22_OtherInOutStorehouse";	
		public static string S23_OtherInOutStorehousePC ="S23_OtherInOutStorehousePC";	
		public static string P01_ProductionPlan ="P01_ProductionPlan";	
		public static string P02_ProductionPlanPC ="P02_ProductionPlanPC";	
		public static string P03_ProductionResultPC ="P03_ProductionResultPC";	
		public static string P04_ProductionPlanAdd ="P04_ProductionPlanAdd";	
		public static string P05_RowMaterialRequirementCalculat ="P05_RowMaterialRequirementCalculat";	
		public static string P06_RowMaterialRequirementCalculatPC ="P06_RowMaterialRequirementCalculatPC";	
		public static string P07_WorkPlanEstablish ="P07_WorkPlanEstablish";	
		public static string P08_WCPlanPC ="P08_WCPlanPC";	
		public static string P09_WorkDailyReportRegistration ="P09_WorkDailyReportRegistration";	
		public static string P10_WorkDailyReportRegistrationPC ="P10_WorkDailyReportRegistrationPC";	
		public static string P11_RowMaterialRequriement ="P11_RowMaterialRequriement";	
		public static string P12_RowMaterialRequriementPC ="P12_RowMaterialRequriementPC";	
		public static string P13_RowMaterialRequriementAdd ="P13_RowMaterialRequriementAdd";	
		public static string P14_OutsideRequest ="P14_OutsideRequest";	
		public static string P15_OutsideRequestPC ="P15_OutsideRequestPC";	
		public static string O01_BuyingOrder ="O01_BuyingOrder";	
		public static string O02_BuyingOrderPC ="O02_BuyingOrderPC";	
		public static string O03_BuyingProgressLook ="O03_BuyingProgressLook";	
		public static string O04_BuyingOrderAdd ="O04_BuyingOrderAdd";	
		public static string O05_BuyingDelievery ="O05_BuyingDelievery";	
		public static string O06_BuyingDelieveryPC ="O06_BuyingDelieveryPC";	
		public static string O07_OutSideOrder ="O07_OutSideOrder";	
		public static string O08_OutSideOrderPC ="O08_OutSideOrderPC";	
		public static string O09_OutSideProgressLook ="O09_OutSideProgressLook";	
		public static string O10_OutSideOutStorehouse ="O10_OutSideOutStorehouse";	
		public static string O11_OutSideOutStorehousePC ="O11_OutSideOutStorehousePC";	
		public static string O12_OutSideDelievery ="O12_OutSideDelievery";	
		public static string O13_OutSideDelieveryPC ="O13_OutSideDelieveryPC";	
		public static string O14_PaymentPlanResultRegistraton ="O14_PaymentPlanResultRegistraton";	
		public static string O15_PaymentPlanResultRegistratonPC ="O15_PaymentPlanResultRegistratonPC";	
		public static string Q01_QualityInspectionRegistration ="Q01_QualityInspectionRegistration";	
		public static string Q02_QualityInspectionPC ="Q02_QualityInspectionPC";	
		public static string Q03_QualityInspectionStatistics ="Q03_QualityInspectionStatistics";		
		public static string Q04_IncongruityPresentIndex ="Q04_IncongruityPresentIndex";	
		public static string Q05_IncongruityCauseIndex ="Q05_IncongruityCauseIndex";	
		public static string Q06_AutonomyInspectPC ="Q06_AutonomyInspectPC";	
		public static string Q07_AutonomyInspectStatistics ="Q07_AutonomyInspectStatistics";	
		public static string Q08_AutonomyInspectIndex ="Q08_AutonomyInspectIndex";	
		public static string Q09_MoneyStandardIndex ="Q09_MoneyStandardIndex";	
		public static string Q10_QuantityStandardIndex ="Q10_QuantityStandardIndex";	
		public static string Q11_LotStandardIndex ="Q11_LotStandardIndex";	
		public static string M01_BizPlanResultIndex ="M01_BizPlanResultIndex";		
		public static string M02_SaleTatalProfitIndex ="M02_SaleTatalProfitIndex";	
		public static string M03_SaleIndex ="M03_SaleIndex";	
		public static string M04_BuyingIndex ="M04_BuyingIndex";			
		public static string M05_ItemsStockIndex ="M05_ItemsStockIndex";		
		public static string M06_StoreHouseStockIndex ="M06_StoreHouseStockIndex";		
		public static string M07_StockMoneyIndex ="M07_StockMoneyIndex";	
		public static string M08_WorkingRatioIndex ="M08_WorkingRatioIndex";	
		public static string M09_NonWorkingRatioIndex ="M09_NonWorkingRatioIndex";	
		public static string M10_ProductivityIndex ="M10_ProductivityIndex";	
		public static string M11_AutonomyInspectSatistics ="M11_AutonomyInspectSatistics";	
		public static string M12_MoneyStandardIndex ="M12_MoneyStandardIndex";	
		public static string M13_QuantityStandardIndex ="M13_QuantityStandardIndex";	
		public static string M14_LotStandardIndex ="M14_LotStandardIndex";	
		public static string U01_UserCompanyInput ="U01_UserCompanyInput";	
		public static string U02_SystemAbilitySet ="U02_SystemAbilitySet";	
		public static string U03_StandardInforbundleRegistration ="U03_StandardInforbundleRegistration";	
		public static string U04_DataBackUpRestoration ="U04_DataBackUpRestoration";	
		public static string U05_HistoryAdjustment ="U05_HistoryAdjustment";	
		public static string H01_OrderPC ="H01_OrderPC";	
		public static string H02_DeliveryRequestPC ="H02_DeliveryRequestPC";	
		public static string H03_OutStorehousePC ="H03_OutStorehousePC";	
		public static string H04_DeliveryPC	 ="H04_DeliveryPC";	
		public static string H05_StockPC ="H05_StockPC";	
		public static string H06_QualityPC ="H06_QualityPC";	
		public static string H07_QualityIndex ="H07_QualityIndex";			
		public static string H08_SubNotice ="H08_SubNotice";	
		public static string H09_SubFreeBoard ="H09_SubFreeBoard";		
		public static string H10_SubData ="H10_SubData";		
		public static string C01_CNotice ="C01_CNotice";		
		public static string C02_CFreeBoard ="C02_CFreeBoard";	
		public static string C03_CGeneralData ="C03_CGeneralData";		
		public static string C04_CTechnologyData ="C04_CTechnologyData";	
		public static string C05_CCompanyStandard ="C05_CCompanyStandard";	
		public static string C06_Clink ="C06_Clink";	
		public static string C07_WorkDiary = "C07_WorkDiary";
		public static string Explanation = "Explanation";

			



		//구매의뢰원장
		public static string BuyingRequestHistoryIndex = "BuyingRequestHistoryIndex";
		public static string BuyingRequestSourceCode = "BuyingRequestSourceCode";
		public static string BuyingRequestSource = "BuyingRequestSource";
		public static string FirstDeliveryDemandQuantity = "FirstDeliveryDemandQuantity";
		public static string FirstDeliveryDemandDate = "FirstDeliveryDemandDate";
		public static string SecondDeliveryDemandQuantity = "SecondDeliveryDemandQuantity";
		public static string SecondDeliveryDemandDate = "SecondDeliveryDemandDate";
		public static string ThirdDeliveryDemandQuantity = "ThirdDeliveryDemandQuantity";
		public static string ThirdDeliveryDemandDate = "ThirdDeliveryDemandDate";
		public static string FourthDeliveryDemandQuantity = "FourthDeliveryDemandQuantity";
		public static string FourthDeliveryDemandDate = "FourthDeliveryDemandDate";
		public static string FifthDeliveryDemandQuantity = "FifthDeliveryDemandQuantity";
		public static string FifthDeliveryDemandDate = "FifthDeliveryDemandDate";
		public static string OrderQuantity = "OrderQuantity";
		public static string HistoryIndex = "HistoryIndex";
		public static string HistorySection = "HistorySection";



		//구매발주원장
		public static string BuyingOrderHistoryIndex = "BuyingOrderHistoryIndex";
		public static string StandardUnitCost = "StandardUnitCost";
		public static string OrderRate = "OrderRate";

		//구매납품의뢰원장
		public static string BuyingDeliveryRequestHistoryIndex = "BuyingDeliveryRequestHistoryIndex";
		public static string DeliveryRequestQuantity = "DeliveryRequestQuantity";



		//구매납품원장
		public static string BuyingDeliveryHistoryIndex = "BuyingDeliveryHistoryIndex";
		public static string DeliveryQuantity = "DeliveryQuantity";


		//외주의뢰원장
		public static string OutSideOrderRequestHistoryIndex = "OutSideOrderRequestHistoryIndex";
		public static string UnitCostDistinction = "UnitCostDistinction";
		public static string BeginProcessCode = "BeginProcessCode";
		public static string BeginProcess = "BeginProcess";
		public static string EndProcessCode = "EndProcessCode";		
		public static string EndProcess = "EndProcess";
		public static string TotalOutSideOrderRequestQuantity = "TotalOutSideOrderRequestQuantity";


		//외주발주원장
		public static string OutSideOrderHistoryIndex = "OutSideOrderHistoryIndex";



		//외주출고원장
		public static string OutSideOutStorehouseHistoryIndex = "OutSideOutStorehouseHistoryIndex";
		public static string ProgressRate = "ProgressRate";
		public static string ThistimeOutStorehouseQuantity = "ThistimeOutStorehouseQuantity";
		public static string OutStorehouseDate = "OutStorehouseDate";


		//외주납품의뢰원장
		public static string OutSideDeliveryRequestHistoryIndex = "OutSideDeliveryRequestHistoryIndex";


		//외주납품원장
		public static string OutSideDeliveryHistoryIndex = "OutSideDeliveryHistoryIndex";

		
		//입고의뢰원장
		public static string InStorehouseRequestHistoryIndex = "InStorehouseRequestHistoryIndex";
		public static string InStorehouseRequestQuantity = "InStorehouseRequestQuantity";

	}

}
