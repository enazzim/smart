-- MariaDB 11.4 Refactored DDL (Normalized and Unified PK Index);

CREATE DATABASE IF NOT EXISTS `kit_erp` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `kit_erp`;

CREATE TABLE IF NOT EXISTS `Phantom` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `DeliveryOrderRequestQuantity` DECIMAL(19,4) NULL,
  `RowMaterialQuantityReflection` TINYINT(1) NULL,
  `OrderNonInStorehouseReflection` TINYINT(1) NULL,
  `OrderRequestReadyQuantityReflection` TINYINT(1) NULL,
  `SafyRowMaterialQuantityReflection` TINYINT(1) NULL,
  `MinimumOrderQuantityReflection` TINYINT(1) NULL,
  `OrderGapQuantityReflection` TINYINT(1) NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SCS_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `OrderNonInStorehouseUsed` VARCHAR(255) NULL,
  `OutOrderUsed` VARCHAR(255) NULL,
  `WorkPlan1` VARCHAR(255) NULL,
  `WorkPlan3` VARCHAR(255) NULL,
  `SafeyRowMaterialUsed` VARCHAR(255) NULL,
  `MinimumGapUsed` VARCHAR(255) NULL,
  `MinusRowMaterialPermissionUsed` VARCHAR(255) NULL,
  `PresentRowMaterialUsed` VARCHAR(255) NULL,
  `BundlingUsed` VARCHAR(255) NULL,
  `InWorkConsidering` VARCHAR(255) NULL,
  `MinusStockAddUsed` VARCHAR(255) NULL,
  `DailyWorkProgress` VARCHAR(255) NULL,
  `ContinuityWorkProgress` VARCHAR(255) NULL,
  `OrderRequestStandbyUsed` VARCHAR(255) NULL,
  `WorkPlan2` VARCHAR(255) NULL,
  `OrderGapInStorehouseUsed` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UPRIOI_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ParentItemIndex` INT NULL,
  `RealItemOrganizationInfoIndex` INT NULL,
  `NeedQuantityDenominator` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `SupplyDivision` VARCHAR(255) NULL,
  `BeginDate` DATETIME NULL,
  `EndDate` DATETIME NULL,
  `UpdateReason` DATETIME NULL,
  `ProcessManagement` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ChildItemIndex` INT NULL,
  `BOMUnit` VARCHAR(255) NULL,
  `SubDivision` VARCHAR(255) NULL,
  `NeedQuantityNumerator` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SFB_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Hits` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `HBP_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `Distinction` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Category` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CT_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Competence08` VARCHAR(255) NULL,
  `Competence19` VARCHAR(255) NULL,
  `Competence07` VARCHAR(255) NULL,
  `Competence12` VARCHAR(255) NULL,
  `Competence05` VARCHAR(255) NULL,
  `Competence18` VARCHAR(255) NULL,
  `Competence16` VARCHAR(255) NULL,
  `Competence06` VARCHAR(255) NULL,
  `Competence00` VARCHAR(255) NULL,
  `Competence02` VARCHAR(255) NULL,
  `Competence03` VARCHAR(255) NULL,
  `Competence14` VARCHAR(255) NULL,
  `Competence13` VARCHAR(255) NULL,
  `Competence04` VARCHAR(255) NULL,
  `Competence17` VARCHAR(255) NULL,
  `Competence11` VARCHAR(255) NULL,
  `Competence10` VARCHAR(255) NULL,
  `Competence01` VARCHAR(255) NULL,
  `Competence20` VARCHAR(255) NULL,
  `Competence09` VARCHAR(255) NULL,
  `Competence15` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OOS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `OrderRate` DECIMAL(19,4) NULL,
  `ProgressRate` DECIMAL(19,4) NULL,
  `ProcessSequenceNum` INT NULL,
  `ProcessIndex` INT NULL,
  `CompanyIndex` INT NULL,
  `ThistimeOutStorehouseQuantity` DECIMAL(19,4) NULL,
  `OutStorehouseDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `ThisOrderQuantity` DECIMAL(19,4) NULL,
  `ThisDeliveryDate` DATETIME NULL,
  `LotNum` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RO_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `CompanyIndex` INT NULL,
  `PropertyClassification` VARCHAR(255) NULL,
  `ProductionRequestDivision` TINYINT(1) NULL,
  `ReceivingOrderDate` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `DeliveryRequestQuantity1` DECIMAL(19,4) NULL,
  `DeliveryRequestDate1` DATETIME NULL,
  `DeliveryRequestQuantity2` DECIMAL(19,4) NULL,
  `DeliveryRequestDate2` DATETIME NULL,
  `DeliveryRequestQuantity3` DECIMAL(19,4) NULL,
  `DeliveryRequestDate3` DATETIME NULL,
  `DeliveryRequestQuantity4` DECIMAL(19,4) NULL,
  `DeliveryRequestDate4` DATETIME NULL,
  `DeliveryRequestQuantity5` DECIMAL(19,4) NULL,
  `DeliveryRequestDate5` DATETIME NULL,
  `TotalReceiveingOrderQuantity` DECIMAL(19,4) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity` DECIMAL(19,4) NULL,
  `SuitabilityQuantity` DECIMAL(19,4) NULL,
  `UnInspectionQuantity` DECIMAL(19,4) NULL,
  `RemainderQuantity` DECIMAL(19,4) NULL,
  `OrderNum` VARCHAR(255) NULL,
  `DeliveryPlace` VARCHAR(255) NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkRedundantItemLastResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BackUp_Table` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `RegisterDate` DATETIME NULL,
  `Filename` VARCHAR(255) NULL,
  `Register` VARCHAR(255) NULL,
  `BackUpReason` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `LotLedger` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `LotNumber` VARCHAR(255) NULL,
  `P1` VARCHAR(255) NULL,
  `P2` VARCHAR(255) NULL,
  `RawMaterial` TINYINT(1) NULL,
  `Active` TINYINT(1) NULL,
  `Remark` TINYINT(1) NULL,
  `RegisterDate` DATETIME NULL,
  `Registerer` VARCHAR(255) NULL,
  `IsDelete` TINYINT(1) NULL,
  `DeleteDate` DATETIME NULL,
  `Deleter` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SM_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `BusinessStorehouseNum1` VARCHAR(255) NULL,
  `MovingQuantity` DECIMAL(19,4) NULL,
  `MovingStorehoseName` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `BusinessStorehouseNum2` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `OriginalStorehouseName` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WDR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `EtcNum1` VARCHAR(255) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `UseJig3` VARCHAR(255) NULL,
  `EtcNum2` VARCHAR(255) NULL,
  `NonWorkTime3` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `WorkerID` VARCHAR(255) NULL,
  `NonWorkTimeReason1` DATETIME NULL,
  `Worker` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `NonWorkTimeCode2` DATETIME NULL,
  `ProcessIndex` INT NULL,
  `UnSuitabilityStatusMeaning` VARCHAR(255) NULL,
  `UnSuitabilityCost` DECIMAL(19,4) NULL,
  `SuitabilityQuantity` DECIMAL(19,4) NULL,
  `UpdatingDate` DATETIME NULL,
  `UseTool1` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ProductName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `UnSuitabilityStatusCode` VARCHAR(255) NULL,
  `UnSuitabilityDetailMeaning` VARCHAR(255) NULL,
  `UseTool3` VARCHAR(255) NULL,
  `UnSuitabilityQuantity` DECIMAL(19,4) NULL,
  `NonWorkTime1` DATETIME NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `WorkEndTime` DATETIME NULL,
  `NonWorkTimeCode1` DATETIME NULL,
  `UseJig1` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UseTool2` VARCHAR(255) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `WCInfoIndex` INT NULL,
  `NonWorkTimeCode3` DATETIME NULL,
  `UnSuitabilityCauseMeaning` VARCHAR(255) NULL,
  `InspectionDecisionCode` VARCHAR(255) NULL,
  `NonWorkTimeReason3` DATETIME NULL,
  `NonWorkTime2` DATETIME NULL,
  `RemainQuantity` DECIMAL(19,4) NULL,
  `NonWorkTimeReason2` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `LotNum` VARCHAR(255) NULL,
  `UseJig2` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `WorkBeginTime` DATETIME NULL,
  `UnSuitabilityCauseCode` VARCHAR(255) NULL,
  `InspectionDecision` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `ThisWorkCompletionQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `II_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ItemDrawNum` VARCHAR(255) NULL,
  `ItemName` VARCHAR(255) NULL,
  `ChargePerson` VARCHAR(255) NULL,
  `MaterialWeight` VARCHAR(255) NULL,
  `MainOutSideOrderCompany` VARCHAR(255) NULL,
  `ItemClassification3` VARCHAR(255) NULL,
  `Standard1` VARCHAR(255) NULL,
  `OrderPlan` VARCHAR(255) NULL,
  `Unit4` VARCHAR(255) NULL,
  `MainPurchaseCompany` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `Unit` VARCHAR(255) NULL,
  `SupplyTerm` VARCHAR(255) NULL,
  `BOMUnit` VARCHAR(255) NULL,
  `ItemType` VARCHAR(255) NULL,
  `Texture` VARCHAR(255) NULL,
  `IOChackable` VARCHAR(255) NULL,
  `StockManagable` VARCHAR(255) NULL,
  `MateralQuality` VARCHAR(255) NULL,
  `OrderIntervalQuantity` DECIMAL(19,4) NULL,
  `MinOrderQuantity` DECIMAL(19,4) NULL,
  `StockUnit` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `ProductWeight` VARCHAR(255) NULL,
  `Standard3` VARCHAR(255) NULL,
  `MainSaleCompany` VARCHAR(255) NULL,
  `CheckDistinction` VARCHAR(255) NULL,
  `ProductHeatTreatmentDescription` VARCHAR(255) NULL,
  `CompleteLeadTime` VARCHAR(255) NULL,
  `ItemClassification4` VARCHAR(255) NULL,
  `Standard2` VARCHAR(255) NULL,
  `ItemClassification1` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `Standard` VARCHAR(255) NULL,
  `StandardUnitCost` DECIMAL(19,4) NULL,
  `Unit2` VARCHAR(255) NULL,
  `SupplementaryValueTaxRate` DECIMAL(19,4) NULL,
  `Unit1` VARCHAR(255) NULL,
  `ItemClassification2` VARCHAR(255) NULL,
  `MaterialHeatTreatmentDescription` VARCHAR(255) NULL,
  `SafetyStockQuantity` DECIMAL(19,4) NULL,
  `TariffRate` DECIMAL(19,4) NULL,
  `PurchaseUnit` VARCHAR(255) NULL,
  `CuttingSpace` VARCHAR(255) NULL,
  `StiffenDeep` VARCHAR(255) NULL,
  `MaterialHeatTreatmentRequestDegree` VARCHAR(255) NULL,
  `ItemState` TINYINT(1) NULL,
  `Standard4` VARCHAR(255) NULL,
  `Unit3` VARCHAR(255) NULL,
  `Maker` VARCHAR(255) NULL,
  `SaleUnit` VARCHAR(255) NULL,
  `DomesticImportDistinction` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ItemInfo` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBRawRedundantItemPlanResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `HFB_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Hits` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WorkReport` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Equipment` VARCHAR(255) NULL,
  `Customer` VARCHAR(255) NULL,
  `ReportDate` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `approval` VARCHAR(255) NULL,
  `QC` VARCHAR(255) NULL,
  `Approval` VARCHAR(255) NULL,
  `BusinessTripPerson` VARCHAR(255) NULL,
  `MainPanding` VARCHAR(255) NULL,
  `Indication` VARCHAR(255) NULL,
  `RegistrationID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SBD_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UpdatingPerson` VARCHAR(255) NULL,
  `SubBuyingOrderHistoryIndex` INT NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `DeliveryQuantity` DECIMAL(19,4) NULL,
  `UpdatingDate` DATETIME NULL,
  `CompanyIndex` INT NULL,
  `RegistrationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `Year` VARCHAR(255) NULL,
  `Month` VARCHAR(255) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `StandardUnitCost` DECIMAL(19,4) NULL,
  `DeliveryDate` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BD_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `DeliveryQuantity` DECIMAL(19,4) NULL,
  `BuyingDeliveryRequestHistoryIndex` INT NULL,
  `UpdatingDate` DATETIME NULL,
  `CompanyIndex` INT NULL,
  `NowStockQuantity` DECIMAL(19,4) NULL,
  `RegistrationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `Year` VARCHAR(255) NULL,
  `Month` VARCHAR(255) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `StandardUnitCost` DECIMAL(19,4) NULL,
  `DeliveryDate` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `LotNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BPC_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `CalendarDate` DATE NOT NULL,
  `WorkTime` DECIMAL(4,2) NOT NULL DEFAULT 8.00,
  `Content` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  UNIQUE KEY `UQ_BPC_T_Date` (`CalendarDate`, `RecodingState`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OIOS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `InOutStorehouseQuantity` DECIMAL(19,4) NULL,
  `InOutStorehouseDistinction` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `InOutStorehouseDetailReason` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `InOutStorehouseReasonCode` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationDate` DATETIME NULL,
  `InOutStorehouseReason` VARCHAR(255) NULL,
  `BusinessStorehouseNum` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `StorehouseName` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `InOutDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PP_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProductionPlanHistorySourceCode` VARCHAR(255) NULL,
  `HistorySection` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `ProductionBeginDate` DATETIME NULL,
  `RegistrationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `HistoryIndex` INT NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `ProductionCompleteQuantity` DECIMAL(19,4) NULL,
  `RowMaterialCalculation` VARCHAR(255) NULL,
  `ProductionPlanQuantity` DECIMAL(19,4) NULL,
  `ProductionPlanHistorySource` VARCHAR(255) NULL,
  `MaterialRequirementVolumNum` VARCHAR(255) NULL,
  `DeliveryDate` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `VolumNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Development` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Product` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WorkReportID` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `08` VARCHAR(255) NULL,
  `06` VARCHAR(255) NULL,
  `07` VARCHAR(255) NULL,
  `09` VARCHAR(255) NULL,
  `02` VARCHAR(255) NULL,
  `01` VARCHAR(255) NULL,
  `05` VARCHAR(255) NULL,
  `10` VARCHAR(255) NULL,
  `04` VARCHAR(255) NULL,
  `03` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkRedundantResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkRedundantItemPlanResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CM_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `BillPaymentDate1` DATETIME NULL,
  `BillNum1` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ItemPaymentCost` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `SupplementaryValueTaxPaymentCost` DECIMAL(19,4) NULL,
  `UpdatingDate` DATETIME NULL,
  `CompanyIndex` INT NULL,
  `BankName2` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `BillPaymentDate3` DATETIME NULL,
  `BankCode3` VARCHAR(255) NULL,
  `BillNum3` VARCHAR(255) NULL,
  `CollectMoneyDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `BankName3` VARCHAR(255) NULL,
  `BankCode1` VARCHAR(255) NULL,
  `DecisionMethodCode` VARCHAR(255) NULL,
  `BillNum2` VARCHAR(255) NULL,
  `BillPaymentDate2` DATETIME NULL,
  `DecisionMethod` VARCHAR(255) NULL,
  `BankName1` VARCHAR(255) NULL,
  `BankCode2` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WCPC_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `WCIndex` INT NOT NULL,
  `CalendarDate` DATE NOT NULL,
  `WorkTime` DECIMAL(4,2) NOT NULL,
  `Content` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  UNIQUE KEY `UQ_WCPC_T_WC_Date` (`WCIndex`, `CalendarDate`, `RecodingState`),
  INDEX `IDX_WCPC_T_WC` (`WCIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UPIOI_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ParentItemIndex` INT NULL,
  `ItemOrganizationInfoIndex` INT NULL,
  `NeedQuantityDenominator` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `SupplyDivision` VARCHAR(255) NULL,
  `BeginDate` DATETIME NULL,
  `EndDate` DATETIME NULL,
  `UpdateReason` DATETIME NULL,
  `ProcessManagement` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ChildItemIndex` INT NULL,
  `BOMUnit` VARCHAR(255) NULL,
  `SubDivision` VARCHAR(255) NULL,
  `NeedQuantityNumerator` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `StandbyTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `PreProcessName` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `ProductName` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `OrderLeadTime` DECIMAL(19,4) NULL,
  `WorkDate` DATETIME NULL,
  `Processing` TINYINT(1) NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `WorkPlanHistoryIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CFB_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Hits` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RowMaterial` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `TotalRowMaterialQuantityReflection` DECIMAL(19,4) NULL,
  `RowMaterialQuantityReflection` DECIMAL(19,4) NULL,
  `OrderNonInStorehouseReflection` DECIMAL(19,4) NULL,
  `OrderRequestReadyQuantityReflection` DECIMAL(19,4) NULL,
  `FactRowMaterialQuantityReflection` DECIMAL(19,4) NULL,
  `SafyRowMaterialQuantityReflection` DECIMAL(19,4) NULL,
  `MinimumOrderQuantityReflection` DECIMAL(19,4) NULL,
  `OrderGapQuantityReflection` DECIMAL(19,4) NULL,
  `DeliveryOrderRequestQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SD_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WorkTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `PreProcessName` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `ProductName` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `OrderLeadTime` DECIMAL(19,4) NULL,
  `WorkDate` DATETIME NULL,
  `WorkEndDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `WorkPlanHistoryIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Loader` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CL_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Classification` VARCHAR(255) NULL,
  `Link` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `SiteName` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBPreWorkPlan` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkCompleteDate` DATETIME NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `WorkDate` DATETIME NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `DeliveryDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CWD_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `WorkDateTitle` VARCHAR(255) NULL,
  `Id` VARCHAR(255) NULL,
  `Name` VARCHAR(255) NULL,
  `MorningWork` VARCHAR(255) NULL,
  `AfternoonWork` VARCHAR(255) NULL,
  `NightWork` VARCHAR(255) NULL,
  `TomorrowWork` VARCHAR(255) NULL,
  `Listing` TINYINT(1) NULL,
  `Closing` VARCHAR(255) NULL,
  `NoWriter` INT NULL,
  `RegistrationDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OO_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `UnitCostDistinction` VARCHAR(255) NULL,
  `BeginProcessIndex` INT NULL,
  `BeginProcess` VARCHAR(255) NULL,
  `EndProcessIndex` INT NULL,
  `EndProcess` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `OrderRate` DECIMAL(19,4) NULL,
  `FirstDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FirstDeliveryDemandDate` DATETIME NULL,
  `SecondDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `SecondDeliveryDemandDate` DATETIME NULL,
  `ThirdDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `ThirdDeliveryDemandDate` DATETIME NULL,
  `FourthDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FourthDeliveryDemandDate` DATETIME NULL,
  `FifthDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FifthDeliveryDemandDate` DATETIME NULL,
  `OrderQuantity` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `RemainQuantity` DECIMAL(19,4) NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `InStoreWaitingQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NULL,
  `ItemIndex` INT NULL,
  `LastYearTransferQuantity` DECIMAL(19,4) NULL,
  `Year` VARCHAR(255) NULL,
  `LastYearTransferCost` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TempTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `ItemIndex` INT NULL,
  `Lavel` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `S_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `SupplementaryValueTaxRate` DECIMAL(19,4) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `OutStorehouseHistoryIndex` INT NULL,
  `SuitabilityQuantity` DECIMAL(19,4) NULL,
  `SaleDate` DATETIME NULL,
  `OutStorehouseQuantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `OrderNum` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RIOI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ParentItemIndex` INT NULL,
  `NeedQuantityDenominator` DECIMAL(19,4) NULL,
  `SupplyDivision` VARCHAR(255) NULL,
  `BeginDate` DATETIME NULL,
  `EndDate` DATETIME NULL,
  `ProcessManagement` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NULL,
  `ChildItemIndex` INT NULL,
  `RegistrationDate` DATETIME NULL,
  `BOMUnit` VARCHAR(255) NULL,
  `SubDivision` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `NeedQuantityNumerator` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `B_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `TotalCost` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `HistorySection` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `BuyingQuantity` DECIMAL(19,4) NULL,
  `ItemIndex` INT NULL,
  `HistoryIndex` INT NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SH_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `Date` DATETIME NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `Quantity` DECIMAL(19,4) NULL,
  `HistoryDivision` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `Division` VARCHAR(255) NULL,
  `HistoryIndex` INT NULL,
  `StoreName` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CN_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ODR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `DeliveryRequestQuantity` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `BeginProcessName` VARCHAR(255) NULL,
  `EndProcessSequenceNum` VARCHAR(255) NULL,
  `EndProcessName` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `BeginProcessIndex` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `BeginProcessSequenceNum` VARCHAR(255) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `EndProcessIndex` INT NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `IS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `BusinessStorehouseNum` VARCHAR(255) NULL,
  `InStorehouseQuantity` DECIMAL(19,4) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `InStorehouseRequestHistoryIndex` INT NULL,
  `InStoreDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `CompanyName` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `PresidentName` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ItemGroupErr_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `CompanyIndex` INT NULL,
  `ReceivingOrderDate` DATETIME NULL,
  `DeliveryRequestDate` DATETIME NULL,
  `TotalReceiveingOrderQuantity` DECIMAL(19,4) NULL,
  `ErrRegion` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ProgressTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `PreProcessName` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `ProductName` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `OrderLeadTime` DECIMAL(19,4) NULL,
  `WorkDate` DATETIME NULL,
  `WorkEndDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `WorkPlanHistoryIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ST_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `StoreNum` VARCHAR(255) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `ThrowingQuanity` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `ThrowingDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PCL_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ClaimStatusMeaning` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `ReceiptDate` DATETIME NULL,
  `ClaimQuantity` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `ClaimStatusCode` VARCHAR(255) NULL,
  `ClaimCauseCode` VARCHAR(255) NULL,
  `ClaimCost` DECIMAL(19,4) NULL,
  `ClaimCauseMeaning` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CCG_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Category` VARCHAR(255) NULL,
  `Classify` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Institute` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ISR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `InstorehouseRequestQuantity` DECIMAL(19,4) NULL,
  `InStorehouseQuantity` DECIMAL(19,4) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `InStoreDate` DATETIME NULL,
  `HistorySection` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `HistoryIndex` INT NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RPSI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NOT NULL,
  `ProcessIndex` INT NOT NULL,
  `ProcessSequence` INT NOT NULL,
  `WorkDivision` VARCHAR(100) NOT NULL,
  `WCIndex` INT NULL,
  `OutsourceLeadTime` INT NULL,
  `OutsourceRate` DECIMAL(5,2) NULL,
  `EtcText` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_RPSI_MT_Item` (`ItemIndex`),
  INDEX `IDX_RPSI_MT_Process` (`ProcessIndex`),
  INDEX `IDX_RPSI_MT_WC` (`WCIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `HD_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Category` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SBO_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `TotalCost` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `DeliveryQuantity` DECIMAL(19,4) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `DeliveryRemainQuantity` DECIMAL(19,4) NULL,
  `ItemIndex` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `DeliveryDate` DATETIME NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Materials` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `InStorehouseCost12` DECIMAL(19,4) NULL,
  `InStorehouseCost6` DECIMAL(19,4) NULL,
  `StockCost1` DECIMAL(19,4) NULL,
  `StockCost8` DECIMAL(19,4) NULL,
  `StockQuantity1` DECIMAL(19,4) NULL,
  `StockQuantity11` DECIMAL(19,4) NULL,
  `ProcessIndex` INT NULL,
  `OutStorehouseCost5` DECIMAL(19,4) NULL,
  `OutStorehouseCost1` DECIMAL(19,4) NULL,
  `InStorehouseCost8` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity3` DECIMAL(19,4) NULL,
  `StockCost3` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity12` DECIMAL(19,4) NULL,
  `InStorehouseQuantity6` DECIMAL(19,4) NULL,
  `InStorehouseQuantity10` DECIMAL(19,4) NULL,
  `StockQuantity10` DECIMAL(19,4) NULL,
  `BusinessStorehouseNum` VARCHAR(255) NULL,
  `OutStorehouseQuantity8` DECIMAL(19,4) NULL,
  `StockQuantity2` DECIMAL(19,4) NULL,
  `StockQuantity6` DECIMAL(19,4) NULL,
  `OutStorehouseCost4` DECIMAL(19,4) NULL,
  `StockCost10` DECIMAL(19,4) NULL,
  `RecodingState` TINYINT(1) NULL,
  `OutStorehouseCost2` DECIMAL(19,4) NULL,
  `OutStorehouseCost6` DECIMAL(19,4) NULL,
  `StockQuantity7` DECIMAL(19,4) NULL,
  `InStorehouseCost5` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity5` DECIMAL(19,4) NULL,
  `StockQuantity9` DECIMAL(19,4) NULL,
  `OutStorehouseCost11` DECIMAL(19,4) NULL,
  `InStorehouseQuantity4` DECIMAL(19,4) NULL,
  `InStorehouseCost1` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity4` DECIMAL(19,4) NULL,
  `InStorehouseQuantity2` DECIMAL(19,4) NULL,
  `StockCost9` DECIMAL(19,4) NULL,
  `ItemIndex` INT NULL,
  `InStorehouseQuantity8` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity6` DECIMAL(19,4) NULL,
  `StockCost12` DECIMAL(19,4) NULL,
  `InStorehouseQuantity11` DECIMAL(19,4) NULL,
  `StockQuantity12` DECIMAL(19,4) NULL,
  `StockQuantity5` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity9` DECIMAL(19,4) NULL,
  `StockCost2` DECIMAL(19,4) NULL,
  `InStorehouseCost9` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity10` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity7` DECIMAL(19,4) NULL,
  `OutStorehouseCost9` DECIMAL(19,4) NULL,
  `StockQuantity8` DECIMAL(19,4) NULL,
  `Year` VARCHAR(255) NULL,
  `StockQuantity4` DECIMAL(19,4) NULL,
  `StockCost6` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity1` DECIMAL(19,4) NULL,
  `OutStorehouseCost10` DECIMAL(19,4) NULL,
  `InStorehouseCost4` DECIMAL(19,4) NULL,
  `InStorehouseCost3` DECIMAL(19,4) NULL,
  `StockCost7` DECIMAL(19,4) NULL,
  `InStorehouseQuantity12` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity2` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity11` DECIMAL(19,4) NULL,
  `StockCost11` DECIMAL(19,4) NULL,
  `InStorehouseQuantity1` DECIMAL(19,4) NULL,
  `InStorehouseCost2` DECIMAL(19,4) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `InStorehouseQuantity5` DECIMAL(19,4) NULL,
  `InStorehouseQuantity7` DECIMAL(19,4) NULL,
  `InStorehouseCost11` DECIMAL(19,4) NULL,
  `OutStorehouseCost3` DECIMAL(19,4) NULL,
  `InStorehouseCost10` DECIMAL(19,4) NULL,
  `StockCost4` DECIMAL(19,4) NULL,
  `LastYearTransferQuantity` DECIMAL(19,4) NULL,
  `OutStorehouseCost8` DECIMAL(19,4) NULL,
  `LastYearTransferCost` DECIMAL(19,4) NULL,
  `OutStorehouseCost7` DECIMAL(19,4) NULL,
  `InStorehouseCost7` DECIMAL(19,4) NULL,
  `InStorehouseQuantity3` DECIMAL(19,4) NULL,
  `StockQuantity3` DECIMAL(19,4) NULL,
  `StockCost5` DECIMAL(19,4) NULL,
  `OutStorehouseCost12` DECIMAL(19,4) NULL,
  `InStorehouseQuantity9` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `UnInspectionQuantity` DECIMAL(19,4) NULL,
  `SuitabilityQuantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `OrderNum` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `UnSuitabilityQuantity` DECIMAL(19,4) NULL,
  `OutStorehouseQuantity` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `BusinessStorehouseNum` VARCHAR(255) NULL,
  `OutStoreDate` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `LotNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BDR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `TotalCost` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `DeliveryRequestQuantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RecodingState` TINYINT(1) NULL,
  `ItemIndex` INT NULL,
  `Year` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_WorkDailyBoard` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ECL_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `ReceiptDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `EtcClaimRegion` VARCHAR(255) NULL,
  `ClaimCost` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TempGroupViewPP_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `PropertyClassification` VARCHAR(255) NULL,
  `Type` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `OrderRemainQuantity` DECIMAL(19,4) NULL,
  `PlanRemainQuantity` DECIMAL(19,4) NULL,
  `InsufficiencyQuantity` DECIMAL(19,4) NULL,
  `StockQuantity` DECIMAL(19,4) NULL,
  `ThisPlanQuantity` DECIMAL(19,4) NULL,
  `NeedQuantity` DECIMAL(19,4) NULL,
  `ProductionCompleteDate` DATETIME NULL,
  `ProductionBeginDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `EPI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `PlanDate` DATETIME NULL,
  `State` TINYINT(1) NULL,
  `PlanQuantity` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `PlanTotalCost` DECIMAL(19,4) NULL,
  `RecodingState` TINYINT(1) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `SaleUnitCost` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProductionRequestSource` VARCHAR(255) NULL,
  `RequestDate3` DATETIME NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RequestDate2` DATETIME NULL,
  `PropertyClassification` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RequestQuantity2` DECIMAL(19,4) NULL,
  `RequestQuantity1` DECIMAL(19,4) NULL,
  `RegistrationDate` DATETIME NULL,
  `ProductionRequestSourceCode` VARCHAR(255) NULL,
  `RequestDate5` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RequestQuantity3` DECIMAL(19,4) NULL,
  `RequestQuantity5` DECIMAL(19,4) NULL,
  `RequestDate1` DATETIME NULL,
  `RequestQuantity4` DECIMAL(19,4) NULL,
  `RequestDate4` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `VolumNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `ProductionRequestQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `DS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NULL,
  `ItemIndex` INT NULL,
  `LastYearTransferQuantity` DECIMAL(19,4) NULL,
  `Year` VARCHAR(255) NULL,
  `LastYearTransferCost` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SM_Table` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `SM_StockClassify` VARCHAR(255) NULL,
  `SM_ItemNumber` VARCHAR(255) NULL,
  `SM_StockLocation` VARCHAR(255) NULL,
  `SM_ProcessName` VARCHAR(255) NULL,
  `SM_CarryOverQuantity1` DOUBLE NULL,
  `SM_CarryOverCost1` DOUBLE NULL,
  `SM_StorageQuantity1` DOUBLE NULL,
  `SM_StorageCost1` DOUBLE NULL,
  `SM_DeliveryQuantity1` DOUBLE NULL,
  `SM_DeliveryCost1` DOUBLE NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PA_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `H06_QualityPC` VARCHAR(255) NULL,
  `Q02_IncongruityPresentIndex` INT NULL,
  `M05_OtherInOutStorehouse` VARCHAR(255) NULL,
  `C01_Notice` VARCHAR(255) NULL,
  `O10_OutSideDelivery` VARCHAR(255) NULL,
  `Q07_MoneyStandardIndex` INT NULL,
  `C05_CompanyStandard` VARCHAR(255) NULL,
  `Q09_LotStandardIndex` INT NULL,
  `P02_ProductionPlanPC` VARCHAR(255) NULL,
  `S15_SaleHistoryRegistration` VARCHAR(255) NULL,
  `O11_OutSideDeliveryPC` VARCHAR(255) NULL,
  `Q08_QuantityStandardIndex` INT NULL,
  `S09_GoodsBuyingRequestPC` VARCHAR(255) NULL,
  `P05_RowMaterialRequirementCalculatePC` VARCHAR(255) NULL,
  `Q12_StockMoneyIndex` INT NULL,
  `H05_StockPC` VARCHAR(255) NULL,
  `O14_QualityInspectionRegistration` VARCHAR(255) NULL,
  `H04_OutStorehousePC` VARCHAR(255) NULL,
  `S13_GoodsManufactureOutStorehouse` VARCHAR(255) NULL,
  `B03_ItemOrganizationInfo` VARCHAR(255) NULL,
  `C02_FreeBoard` VARCHAR(255) NULL,
  `P14_OutsideRequestPC` VARCHAR(255) NULL,
  `S06_ProductionRequestPC` VARCHAR(255) NULL,
  `B02_ItemInfo` VARCHAR(255) NULL,
  `O08_OutSideOutStorehouse` VARCHAR(255) NULL,
  `C06_link` VARCHAR(255) NULL,
  `P07_RowMaterialRequriementPC` VARCHAR(255) NULL,
  `U06_HistoryAdjustment` VARCHAR(255) NULL,
  `M06_OtherInOutStorehousePC` VARCHAR(255) NULL,
  `O05_BuyingDeliveryPC` VARCHAR(255) NULL,
  `P08_RowMaterialRequriementAdd` VARCHAR(255) NULL,
  `P09_WorkPlan` VARCHAR(255) NULL,
  `P01_ProductionPlan` VARCHAR(255) NULL,
  `B05_ProcessSequenceInfo` VARCHAR(255) NULL,
  `Q06_AutonomyInspectIndex` INT NULL,
  `O09_OutSideOutStorehousePC` VARCHAR(255) NULL,
  `M09_SaleIndex` INT NULL,
  `P10_WCPlanPC` VARCHAR(255) NULL,
  `U05_DataBackUpRestoration` VARCHAR(255) NULL,
  `O07_OutSideOrderPC` VARCHAR(255) NULL,
  `O03_BuyingOrderAdd` VARCHAR(255) NULL,
  `S18_CollectMoneyRegistrationPC` DECIMAL(19,4) NULL,
  `S03_ReceiveingPC` VARCHAR(255) NULL,
  `H02_DeliveryRequestPC` VARCHAR(255) NULL,
  `U04_UserAuthoritySet` VARCHAR(255) NULL,
  `O15_QualityInspectionPC` VARCHAR(255) NULL,
  `Q03_IncongruityCauseIndex` INT NULL,
  `S22_StorehouseMoving` VARCHAR(255) NULL,
  `S08_GoodsBuyingRequest` VARCHAR(255) NULL,
  `Q13_WorkingRatioIndex` INT NULL,
  `P12_WorkDailyReportRegistrationPC` VARCHAR(255) NULL,
  `S05_ProductionRequest` VARCHAR(255) NULL,
  `S23_StorehouseMovingPC` VARCHAR(255) NULL,
  `B06_EquipmentInfo` VARCHAR(255) NULL,
  `S02_ReceiveingbundleRegistration` VARCHAR(255) NULL,
  `O13_PaymentPlanResultRegistrationPC` VARCHAR(255) NULL,
  `S04_ReceiveingProgressLook` VARCHAR(255) NULL,
  `S19_UnCollectMoneyLook` DECIMAL(19,4) NULL,
  `Q11_StoreHouseStockIndex` INT NULL,
  `P06_RowMaterialRequriement` VARCHAR(255) NULL,
  `Grade` VARCHAR(255) NULL,
  `H09_SubFreeBoard` VARCHAR(255) NULL,
  `Q01_QualityInspectionStatistics` VARCHAR(255) NULL,
  `P04_RowMaterialRequirementCalculate` VARCHAR(255) NULL,
  `H10_SubData` VARCHAR(255) NULL,
  `C04_TechnologyData` VARCHAR(255) NULL,
  `M07_BizPlanResultIndex` INT NULL,
  `P13_OutsideRequest` VARCHAR(255) NULL,
  `H07_QualityIndex` INT NULL,
  `P11_WorkDailyReportRegistration` VARCHAR(255) NULL,
  `P03_ProductionPlanAdd` VARCHAR(255) NULL,
  `S07_ProductionRequestAdd` VARCHAR(255) NULL,
  `S14_GoodsManufactureOutStorehousePC` VARCHAR(255) NULL,
  `B09_OutSideOrderUnitCodeInfo` VARCHAR(255) NULL,
  `S10_GoodsBuyingRequestAdd` VARCHAR(255) NULL,
  `H03_DeliveryPC` VARCHAR(255) NULL,
  `M10_BuyingIndex` INT NULL,
  `H01_OrderPC` VARCHAR(255) NULL,
  `M02_BusinessPlanInfoPC` VARCHAR(255) NULL,
  `M08_SaleTatalProfitIndex` INT NULL,
  `O12_PaymentPlanResultRegistration` VARCHAR(255) NULL,
  `U02_StandardInforbundleRegistration` VARCHAR(255) NULL,
  `U03_SystemAbilitySet` VARCHAR(255) NULL,
  `S11_BusinessStorehouseInStorehouse` VARCHAR(255) NULL,
  `S16_SaleHistoryRegistrationPC` VARCHAR(255) NULL,
  `O01_BuyingOrder` VARCHAR(255) NULL,
  `Q15_ProductivityIndex` INT NULL,
  `B10_BuyingUnitCodeInfo` VARCHAR(255) NULL,
  `B07_WorkStandardInfo` VARCHAR(255) NULL,
  `Q14_NonWorkingRatioIndex` INT NULL,
  `B04_WCInfo` VARCHAR(255) NULL,
  `C03_GeneralData` VARCHAR(255) NULL,
  `U01_UserCompanyInput` VARCHAR(255) NULL,
  `Q05_AutonomyInspectStatistics` VARCHAR(255) NULL,
  `S17_CollectMoneyRegistration` DECIMAL(19,4) NULL,
  `S20_ClaimRegistration` VARCHAR(255) NULL,
  `M04_ExecutionPlanInfoPC` VARCHAR(255) NULL,
  `C07_WorkDiary` VARCHAR(255) NULL,
  `Q10_ItemsStockIndex` INT NULL,
  `O02_BuyingOrderPC` VARCHAR(255) NULL,
  `M03_ExecutionPlanInfo` VARCHAR(255) NULL,
  `B11_StandardProductionCalendarInfo` VARCHAR(255) NULL,
  `Q04_AutonomyInspectPC` VARCHAR(255) NULL,
  `S12_BusinessStorehouseInStorehousePC` VARCHAR(255) NULL,
  `B12_WCProductionCalendarInfo` VARCHAR(255) NULL,
  `O06_OutSideOrder` VARCHAR(255) NULL,
  `H08_SubNotice` VARCHAR(255) NULL,
  `B14_PublicUseCode` VARCHAR(255) NULL,
  `B08_SaleUnitCodeInfo` VARCHAR(255) NULL,
  `S21_ClaimRegistrationPC` VARCHAR(255) NULL,
  `Explanation` VARCHAR(255) NULL,
  `M01_BusinessPlanInfo` VARCHAR(255) NULL,
  `S01_ReceiveingRegistration` VARCHAR(255) NULL,
  `O04_BuyingDelivery` VARCHAR(255) NULL,
  `B13_UserInfo` VARCHAR(255) NULL,
  `B01_CompanyInfo` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `MCI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ClosingPerson` VARCHAR(255) NULL,
  `ClosingYear` VARCHAR(255) NULL,
  `ClosingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ClosingMonth` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Pipe` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CL_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ClaimStatusMeaning` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `ReceiptDate` DATETIME NULL,
  `ClaimQuantity` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `ClaimStatusCode` VARCHAR(255) NULL,
  `ClaimCauseCode` VARCHAR(255) NULL,
  `ClaimCost` DECIMAL(19,4) NULL,
  `ClaimCauseMeaning` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkPlanning` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Resion` VARCHAR(255) NULL,
  `Date` DATETIME NULL,
  `UserName` VARCHAR(255) NULL,
  `UserID` VARCHAR(255) NULL,
  `Division` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `LS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Day` VARCHAR(255) NULL,
  `RegisterDate` DATETIME NULL,
  `DeleveryQuantity` DECIMAL(19,4) NULL,
  `DeleveryCumulativeQuantity` DECIMAL(19,4) NULL,
  `AssayCumulativeQuantity` DECIMAL(19,4) NULL,
  `Division` VARCHAR(255) NULL,
  `AssayQuantity` DECIMAL(19,4) NULL,
  `Year` VARCHAR(255) NULL,
  `Month` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `AS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `Year` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BOS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `UpdatePersonID` DATETIME NULL,
  `UnitCost` DECIMAL(19,4) NULL,
  `Date` DATETIME NULL,
  `UpdateHistoryReason` DATETIME NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `Quantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `UpdatePerson` DATETIME NULL,
  `HistoryDivision` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `UpDatePerson` DATETIME NULL,
  `HistoryIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WDWP_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `PreProcessName` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `ProductName` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `OrderLeadTime` DECIMAL(19,4) NULL,
  `WorkDate` DATETIME NULL,
  `WorkDenotationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `WorkPlanHistoryIndex` INT NULL,
  `WorkCompleteDate` DATETIME NULL,
  `DeliveryDate` DATETIME NULL,
  `EtcText` VARCHAR(255) NULL,
  `VolumNum` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `AS_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `InstoreQuantity` DECIMAL(19,4) NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `InstoreDate` DATETIME NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `ReasonCode` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `ReasonName` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `StoreName` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `SN_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `P_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `BillPaymentDate1` DATETIME NULL,
  `BillNum1` VARCHAR(255) NULL,
  `PaymentDate` DATETIME NULL,
  `ItemPaymentCost` DECIMAL(19,4) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `SupplementaryValueTaxPaymentCost` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `BankName2` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `BillPaymentDate3` DATETIME NULL,
  `BankCode3` VARCHAR(255) NULL,
  `BillNum3` VARCHAR(255) NULL,
  `ProofData` VARCHAR(255) NULL,
  `BankName3` VARCHAR(255) NULL,
  `BankCode1` VARCHAR(255) NULL,
  `DecisionMethodCode` VARCHAR(255) NULL,
  `BillNum2` VARCHAR(255) NULL,
  `BillPaymentDate2` DATETIME NULL,
  `DecisionMethod` VARCHAR(255) NULL,
  `BankName1` VARCHAR(255) NULL,
  `BankCode2` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `QI_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Recognition` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `UnSuitabilityStatusMeaning` VARCHAR(255) NULL,
  `UnSuitabilityCost` DECIMAL(19,4) NULL,
  `SuitabilityQuantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `EndProcessSequenceNum` VARCHAR(255) NULL,
  `BeginProcessName` VARCHAR(255) NULL,
  `EndProcessName` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `NowStockQuantity` DECIMAL(19,4) NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `UnSuitabilityDetailMeaning` VARCHAR(255) NULL,
  `Year` VARCHAR(255) NULL,
  `QualityInspectionCompleteDate` DATETIME NULL,
  `HistorySection1` VARCHAR(255) NULL,
  `BeginProcessSequenceNum` VARCHAR(255) NULL,
  `UnSuitabilityQuantity` DECIMAL(19,4) NULL,
  `Month` VARCHAR(255) NULL,
  `InspectionDecisionMeaning` VARCHAR(255) NULL,
  `HistoryIndex2` VARCHAR(255) NULL,
  `InvestigatorID` VARCHAR(255) NULL,
  `StandardUnitCost` DECIMAL(19,4) NULL,
  `HistoryIndex1` VARCHAR(255) NULL,
  `UnSuitabilityCauseMeaning` VARCHAR(255) NULL,
  `InspectionDecisionCode` VARCHAR(255) NULL,
  `RequestQuantity` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `Investigator` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `LotNum` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `UnSuitabilityCauseCode` VARCHAR(255) NULL,
  `UnSuitabilityStatusCode` VARCHAR(255) NULL,
  `HistorySection2` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WSI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NOT NULL,
  `ProcessIndex` INT NOT NULL,
  `WCIndex` INT NOT NULL,
  `EquipmentIndex` INT NULL,
  `Priority` INT NOT NULL DEFAULT 1,
  `MainWorkerIndex` INT NULL,
  `ToolName` VARCHAR(255) NULL,
  `SetupTime` INT NOT NULL DEFAULT 0,
  `StandardTime` INT NOT NULL DEFAULT 0,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_WSI_MT_Item` (`ItemIndex`),
  INDEX `IDX_WSI_MT_Process` (`ProcessIndex`),
  INDEX `IDX_WSI_MT_WC` (`WCIndex`),
  INDEX `IDX_WSI_MT_Equipment` (`EquipmentIndex`),
  INDEX `IDX_WSI_MT_Worker` (`MainWorkerIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UCI_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `CompanyName` VARCHAR(255) NULL,
  `BusinessCompanyNum` VARCHAR(255) NULL,
  `BusinessItem` VARCHAR(255) NULL,
  `BusinessClassification` VARCHAR(255) NULL,
  `BusinessBillAddress` VARCHAR(255) NULL,
  `PresidentName` VARCHAR(255) NULL,
  `TaxBillAddress` VARCHAR(255) NULL,
  `BusinessCompanyAddress` VARCHAR(255) NULL,
  `CorporationRegistrationNum` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OSD_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `DeliveryQuantity` DECIMAL(19,4) NULL,
  `CompanyIndex` INT NULL,
  `RegistrationDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `Year` VARCHAR(255) NULL,
  `Month` VARCHAR(255) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `StandardUnitCost` DECIMAL(19,4) NULL,
  `DeliveryDate` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `LotNum` VARCHAR(255) NULL,
  `OutSideDeliveryRequestHistoryIndex` INT NULL,
  `ItemIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Temp_zip` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Temp_add` VARCHAR(255) NULL,
  `Temp_index` INT NULL,
  `Temp_zip` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `IOI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ParentItemIndex` INT NULL,
  `NeedQuantityDenominator` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `SupplyDivision` VARCHAR(255) NULL,
  `BeginDate` DATETIME NULL,
  `EndDate` DATETIME NULL,
  `UpdatingDate` DATETIME NULL,
  `ProcessManagement` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NULL,
  `ChildItemIndex` INT NULL,
  `RegistrationDate` DATETIME NULL,
  `BOMUnit` VARCHAR(255) NULL,
  `SubDivision` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `NeedQuantityNumerator` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ZIPCODE` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ZIPCODE` VARCHAR(255) NULL,
  `SIDO` VARCHAR(255) NULL,
  `GUGUN` VARCHAR(255) NULL,
  `DONG` VARCHAR(255) NULL,
  `BUNJI` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `E_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `CreateItemNum` VARCHAR(255) NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `ExhaustQuantity` DECIMAL(19,4) NULL,
  `WorkerID` VARCHAR(255) NULL,
  `WorkerName` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `ExhaustDate` DATETIME NULL,
  `StartTime` DATETIME NULL,
  `ExhaustItemNum` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `RegistrationDate` DATETIME NULL,
  `CreateQuantity` DECIMAL(19,4) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `CreateItemDrawNum` VARCHAR(255) NULL,
  `ExhaustItemName` VARCHAR(255) NULL,
  `CreateItemName` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `Use` VARCHAR(255) NULL,
  `ExhaustItemDrawNum` VARCHAR(255) NULL,
  `EndTime` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkRedundant` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UCI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UnitCostDivision` VARCHAR(100) NOT NULL,
  `ItemIndex` INT NOT NULL,
  `CompanyIndex` INT NOT NULL,
  `BeginProcessIndex` INT NULL,
  `EndProcessIndex` INT NULL,
  `StandardUnitCost` DECIMAL(19,4) NOT NULL DEFAULT 0.0000,
  `DiscountUnitCost` DECIMAL(19,4) NOT NULL DEFAULT 0.0000,
  `OrderRate` DECIMAL(5,2) NULL,
  `BeginDate` DATETIME NOT NULL,
  `EndDate` DATETIME NULL,
  `UpdateReason` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_UCI_MT_Item` (`ItemIndex`),
  INDEX `IDX_UCI_MT_Company` (`CompanyIndex`),
  INDEX `IDX_UCI_MT_BeginProc` (`BeginProcessIndex`),
  INDEX `IDX_UCI_MT_EndProc` (`EndProcessIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BO_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `CompanyIndex` INT NULL,
  `FirstDeliveryDemandQuantity` DOUBLE NULL,
  `FirstDeliveryDemandDate` DATETIME NULL,
  `SecondDeliveryDemandQuantity` DOUBLE NULL,
  `SecondDeliveryDemandDate` DATETIME NULL,
  `ThirdDeliveryDemandQuantity` DOUBLE NULL,
  `ThirdDeliveryDemandDate` DATETIME NULL,
  `FourthDeliveryDemandQuantity` DOUBLE NULL,
  `FourthDeliveryDemandDate` DATETIME NULL,
  `FifthDeliveryDemandQuantity` DOUBLE NULL,
  `FifthDeliveryDemandDate` DATETIME NULL,
  `OrderQuantity` DOUBLE NULL,
  `ApplyUnitCost` DOUBLE NULL,
  `TotalCost` DOUBLE NULL,
  `OrderRate` DOUBLE NULL,
  `RemainQuantity` DOUBLE NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `StoreQuantity` DECIMAL(19,4) NULL,
  `InStoreWaitingQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WDWPTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `ProcessIndex` INT NULL,
  `PreProcessName` VARCHAR(255) NULL,
  `ProductItemIndex` INT NULL,
  `ProductDrawNum` VARCHAR(255) NULL,
  `ProductName` VARCHAR(255) NULL,
  `ParentItemIndex` INT NULL,
  `ParentDrawNum` VARCHAR(255) NULL,
  `ParentName` VARCHAR(255) NULL,
  `WCName` VARCHAR(255) NULL,
  `WorkDistinction` VARCHAR(255) NULL,
  `WorkPlanQuantity` DECIMAL(19,4) NULL,
  `WorkCompletionQuantity` DECIMAL(19,4) NULL,
  `OrderLeadTime` DECIMAL(19,4) NULL,
  `WorkDate` DATETIME NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `WorkPlanHistoryIndex` INT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `PropertyClassification` VARCHAR(255) NULL,
  `BuyingRequestSourceCode` VARCHAR(255) NULL,
  `BuyingRequestSource` VARCHAR(255) NULL,
  `FirstDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FirstDeliveryDemandDate` DATETIME NULL,
  `SecondDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `SecondDeliveryDemandDate` DATETIME NULL,
  `ThirdDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `ThirdDeliveryDemandDate` DATETIME NULL,
  `FourthDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FourthDeliveryDemandDate` DATETIME NULL,
  `FifthDeliveryDemandQuantity` DECIMAL(19,4) NULL,
  `FifthDeliveryDemandDate` DATETIME NULL,
  `OrderQuantity` DECIMAL(19,4) NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `TotalCost` DECIMAL(19,4) NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `HistoryIndex` INT NULL,
  `HistorySection` VARCHAR(255) NULL,
  `RequestPost` VARCHAR(255) NULL,
  `RequestPostCode` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CCS_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Category` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WP_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RMS_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ProcessIndex` INT NULL,
  `ProcessSequenceNum` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NULL,
  `ItemIndex` INT NULL,
  `LastYearTransferQuantity` DECIMAL(19,4) NULL,
  `Year` VARCHAR(255) NULL,
  `LastYearTransferCost` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `HG_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Hits` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_Notice` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CTD_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Category` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ItemGroup_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemGroup` VARCHAR(255) NULL,
  `ReceivingOrderDate` DATETIME NULL,
  `DeliveryDate` DATETIME NULL,
  `Quantity` DECIMAL(19,4) NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBWorkRedundantSum` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Relation_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ReadDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ListErr` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `MRC_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `DeliveryOrderRequestQuantity` DECIMAL(19,4) NULL,
  `RowMaterialQuantityReflection` TINYINT(1) NULL,
  `OrderNonInStorehouseReflection` TINYINT(1) NULL,
  `OrderRequestReadyQuantityReflection` TINYINT(1) NULL,
  `SafyRowMaterialQuantityReflection` TINYINT(1) NULL,
  `MinimumOrderQuantityReflection` TINYINT(1) NULL,
  `OrderGapQuantityReflection` TINYINT(1) NULL,
  `VolumNum` INT NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;



CREATE TABLE IF NOT EXISTS `TBRemainderTable` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `PS_MTStock` VARCHAR(255) NULL,
  `RO_HTStock` VARCHAR(255) NULL,
  `PR_HTStock` VARCHAR(255) NULL,
  `OO_HTStock` VARCHAR(255) NULL,
  `WDWP_HTStock` VARCHAR(255) NULL,
  `ItemIndex` INT NULL,
  `RemainderQuantity` DECIMAL(19,4) NULL,
  `DS_MTStock` VARCHAR(255) NULL,
  `BS_MTStock` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `T_PresidentNotice` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `OOR_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `UnitCostDistinction` VARCHAR(255) NULL,
  `BeginProcessIndex` INT NULL,
  `BeginProcess` VARCHAR(255) NULL,
  `EndProcessIndex` INT NULL,
  `EndProcess` VARCHAR(255) NULL,
  `ApplyUnitCost` DOUBLE NULL,
  `FirstDeliveryDemandQuantity` DOUBLE NULL,
  `FirstDeliveryDemandDate` DATETIME NULL,
  `SecondDeliveryDemandQuantity` DOUBLE NULL,
  `SecondDeliveryDemandDate` DATETIME NULL,
  `ThirdDeliveryDemandQuantity` DOUBLE NULL,
  `ThirdDeliveryDemandDate` DATETIME NULL,
  `FourthDeliveryDemandQuantity` DOUBLE NULL,
  `FourthDeliveryDemandDate` DATETIME NULL,
  `FifthDeliveryDemandQuantity` DOUBLE NULL,
  `FifthDeliveryDemandDate` DATETIME NULL,
  `TotalOutSideOrderRequestQuantity` DOUBLE NULL,
  `TotalCost` DOUBLE NULL,
  `ProgressCondition` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingPersonID` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  `VolumNum` VARCHAR(255) NULL,
  `OrderQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BSI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `BuyingDistinction` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `RecodingState` TINYINT(1) NULL,
  `SaleDistinction` VARCHAR(255) NULL,
  `Year` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `PSI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NOT NULL,
  `ProcessIndex` INT NOT NULL,
  `ProcessSequence` INT NOT NULL,
  `WorkDivision` VARCHAR(100) NOT NULL,
  `WCIndex` INT NULL,
  `OutsourceLeadTime` INT NULL,
  `OutsourceRate` DECIMAL(5,2) NULL,
  `EtcText` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_PSI_MT_Item` (`ItemIndex`),
  INDEX `IDX_PSI_MT_Process` (`ProcessIndex`),
  INDEX `IDX_PSI_MT_WC` (`WCIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `CGD_T` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `FileSize` VARCHAR(255) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `Title` VARCHAR(255) NULL,
  `Contents` VARCHAR(255) NULL,
  `Category` VARCHAR(255) NULL,
  `Hits` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBRawRedundantResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `RowMaterialQuantity` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `ProcessSequenceNum` INT NULL,
  `ProductionPlanQuantity` DECIMAL(19,4) NULL,
  `TotalWorkCompletionQuantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `BPI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `PlanDay` VARCHAR(255) NULL,
  `PlanMonth` VARCHAR(255) NULL,
  `CompanyIndex` INT NULL,
  `PlanQuantity` DECIMAL(19,4) NULL,
  `RegistrationPerson` VARCHAR(255) NULL,
  `PlanYear` VARCHAR(255) NULL,
  `PlanTotalCost` DECIMAL(19,4) NULL,
  `RecodingState` TINYINT(1) NULL,
  `RegistrationPersonID` VARCHAR(255) NULL,
  `RegistrationDate` DATETIME NULL,
  `ItemIndex` INT NULL,
  `SaleUnitCost` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBStockStore` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ROErr_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `ItemIndex` INT NULL,
  `CompanyIndex` INT NULL,
  `ReceivingOrderDate` DATETIME NULL,
  `ApplyUnitCost` DECIMAL(19,4) NULL,
  `DeliveryRequestDate` DATETIME NULL,
  `TotalReceiveingOrderQuantity` DECIMAL(19,4) NULL,
  `OrderNum` VARCHAR(255) NULL,
  `DeliveryPlace` VARCHAR(255) NULL,
  `ErrorReason` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBOutOrderRedundant` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `TBRawRedundantItemLastResult` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `Quantity` DECIMAL(19,4) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `WorkDailyBoard` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `content` VARCHAR(255) NULL,
  `FileType` VARCHAR(255) NULL,
  `FileName` VARCHAR(255) NULL,
  `transdate` DATETIME NULL,
  `FileSize` VARCHAR(255) NULL,
  `Title` VARCHAR(255) NULL,
  `AppendFile` VARCHAR(255) NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UserID` VARCHAR(100) NOT NULL,
  `Password` VARCHAR(255) NOT NULL,
  `UserName` VARCHAR(100) NOT NULL,
  `Contact` VARCHAR(100) NULL,
  `WorkDiaryGroupIndex` INT NULL,
  `AuthorityIndex` INT NOT NULL,
  `Email` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  UNIQUE KEY `UQ_UI_MT_UserID` (`UserID`, `RecodingState`),
  INDEX `IDX_UI_MT_DiaryGroup` (`WorkDiaryGroupIndex`),
  INDEX `IDX_UI_MT_Authority` (`AuthorityIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `UPUCI_HT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `UnitCostInfoIndex` INT NOT NULL,
  `UnitCostDivision` VARCHAR(100) NOT NULL,
  `ItemIndex` INT NOT NULL,
  `CompanyIndex` INT NOT NULL,
  `BeginProcessIndex` INT NULL,
  `EndProcessIndex` INT NULL,
  `StandardUnitCost` DECIMAL(19,4) NOT NULL DEFAULT 0.0000,
  `DiscountUnitCost` DECIMAL(19,4) NOT NULL DEFAULT 0.0000,
  `OrderRate` DECIMAL(5,2) NULL,
  `BeginDate` DATETIME NOT NULL,
  `EndDate` DATETIME NULL,
  `UpdateReason` VARCHAR(255) NULL,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_UPUCI_HT_Master` (`UnitCostInfoIndex`),
  INDEX `IDX_UPUCI_HT_Item` (`ItemIndex`),
  INDEX `IDX_UPUCI_HT_Company` (`CompanyIndex`),
  INDEX `IDX_UPUCI_HT_BeginProc` (`BeginProcessIndex`),
  INDEX `IDX_UPUCI_HT_EndProc` (`EndProcessIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `EI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `EquipmentNum` VARCHAR(100) NOT NULL,
  `EquipmentName` VARCHAR(255) NOT NULL,
  `EquipmentCategoryIndex` INT NOT NULL,
  `WCIndex` INT NULL,
  `DesignShot` INT NOT NULL DEFAULT 0,
  `InitialShot` INT NOT NULL DEFAULT 0,
  `AccumulatedShot` INT NOT NULL DEFAULT 0,
  `WorkShot` INT NOT NULL DEFAULT 0,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_EI_MT_Category` (`EquipmentCategoryIndex`),
  INDEX `IDX_EI_MT_WC` (`WCIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `REI_MT` (
  `Index` INT NOT NULL AUTO_INCREMENT,
  `EquipmentNum` VARCHAR(100) NOT NULL,
  `EquipmentName` VARCHAR(255) NOT NULL,
  `EquipmentCategoryIndex` INT NOT NULL,
  `WCIndex` INT NULL,
  `DesignShot` INT NOT NULL DEFAULT 0,
  `InitialShot` INT NOT NULL DEFAULT 0,
  `AccumulatedShot` INT NOT NULL DEFAULT 0,
  `WorkShot` INT NOT NULL DEFAULT 0,
  `RecodingState` TINYINT(1) NOT NULL DEFAULT 1,
  `RegistrationPerson` VARCHAR(255) NOT NULL,
  `RegistrationDate` DATETIME NOT NULL,
  `UpdatingPerson` VARCHAR(255) NULL,
  `UpdatingDate` DATETIME NULL,
  PRIMARY KEY (`Index`),
  INDEX `IDX_REI_MT_Category` (`EquipmentCategoryIndex`),
  INDEX `IDX_REI_MT_WC` (`WCIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
