<!--
//Grid의 출력된 날짜를 수정창(RowEditTemplate)의 WebDateChooser로 바인딩 시켜줌
function uwgPP_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
{
	var row = igtbl_getRowById(rowId);
	
	//if((row.getCellFromKey("ProductionPlanHistorySourceCode").getValue() != "03200020" && row.getCellFromKey("ProgressCondition").getValue() == "대기") && (row.getCellFromKey("ProductionPlanHistorySource").getValue() != "정상") && (row.getCellFromKey("RowMaterialCalculation").getValue() != "산출"))
	if((row.getCellFromKey("RowMaterialCalculation").getValue() != "산출") && row.getCellFromKey("ProgressCondition").getValue() == "대기")
	{
		var numeric = igedit_getById("uwgPP_HT__ctl0_wnePlanQuantity", "igtxtuwgPP_HT__ctl0_wnePlanQuantity");
		var date = igedit_getById("uwgPP_HT__ctl0_wdeStartDate", "igtxtuwgPP_HT__ctl0_wdeStartDate");
		date.setValue(row.getCellFromKey("ProductionBeginDate").getValue());
		numeric.setValue(row.getCellFromKey("ProductionPlanQuantity").getValue());
	}
	else
	{
		alert("수정할 수 없습니다.!");
		return true;
	}
}

//수정창(RowEditTemplate)의 WebDateChooser의 의해 선택된 날짜를 Grid의 해당 Row의 해당 Column의 값을 변경
function uwgPP_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
{
	if(event.srcElement.id == "igtbl_reOkBtn")
	{
		var row = igtbl_getRowById(rowId);
		var numeric = igedit_getById("uwgPP_HT__ctl0_wnePlanQuantity", "igtxtuwgPP_HT__ctl0_wnePlanQuantity");
		var date = igedit_getById("uwgPP_HT__ctl0_wdeStartDate", "igtxtuwgPP_HT__ctl0_wdeStartDate");
		
		row.getCellFromKey("ProductionPlanQuantity").setValue(numeric.getValue());		
		row.getCellFromKey("ProductionBeginDate").setValue(date.getValue());
		document.Form1.ProductionPlanHistoryIndex.value = rowId;
	}
}
//-->