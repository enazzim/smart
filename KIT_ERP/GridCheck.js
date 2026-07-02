<!--
// Grid내의 CheckBox의 전체체크 여부 확인 변수
checkState = true;
// Grid의 HeaderClick했을때 발생
function ColumnHeaderClickHandler(gridName, columnId, button)
{
	
	var grid = igtbl_getGridById(gridName);
	if(checkState)
   	{
		for(var i=0; i<grid.Rows.length; i++)
		{
			var row = grid.Rows.getRow(i);
			row.getCellFromKey("chk").setValue(true);
		}
		checkState = false;
	}
	else
	{
		for(var i=0; i<grid.Rows.length; i++)
		{
			var row = grid.Rows.getRow(i);
			row.getCellFromKey("chk").setValue(false);
		}
		checkState = true;
	}
}
//-->