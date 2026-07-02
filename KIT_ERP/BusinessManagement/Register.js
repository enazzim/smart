// 콤마제거 함수
function RemoveComma(Ovalue)
{
	var array = new String(Ovalue).split(",");
	var Rvalue = new String();
	for ( var i = 0 ; i < array.length ; i++)
	{
		Rvalue += array[i];
	}
	return Rvalue;
}


function OnKeyUp_Currency(obj)
{
	sFieldValue = new String(obj.value);
	sFieldValue = sFieldValue.replace(/,/gi,"");
	if ( IsNumericKey() )		
	{
		SetEventPass();
		return true;
	}
	obj.value = MakeCommaSeparate(sFieldValue);
}

function OnBlur_Cur(obj)	
{
	if (ValidateCurrency(obj.value) == false) 
	{
		obj.value = "";
    	return false;
	}
}

function ValidateCurrency( a_sValue ) 
{	
	var s_FieldValue = new String(a_sValue);
	
	if (isNaN(parseFloat(s_FieldValue)))
		return false;
		
	return true;
}

// OnKeyUp_Currency 에서 호출할 소수점 찍기
function MakeCommaSeparate(a_Value)	
{
	var fl = "";
	var nPointPos = 0;    
	
	if(isNaN(a_Value)) 	
	{ 
    	event.returnValue = false;
    	this.value = "";
    	return "";
	}
	
	if(a_Value == 0) return a_Value;
	
	if(a_Value < 0) 
	{ 
		a_Value=a_Value*(-1);
		fl = "-";
	}
	else if(a_Value == 0)	
	{
    	// 처음 입력값이 0부터 시작할때 이것을 제거한다.
    	a_Value = a_Value*1; 
	}
	
	var a_Value = new String(a_Value);
	var temp = "";
	var sRemain = "";
	var co = 3;
	
	nPointPos = a_Value.indexOf(".");

	if( nPointPos == -1 )  
	{
    	num_len = a_Value.length;
	}
	else
	{
    	// "." 가 포함되어 있을 경우에 재계산
    	if( parseInt(a_Value.substr(0,nPointPos)) == 0 )	
    	{
    		a_Value = "0" + a_Value.substr(nPointPos);
    		nPointPos = a_Value.indexOf(".");
    	}
    	num_len = nPointPos;
    	sRemain = a_Value.substr(nPointPos);
	}
	
	while (num_len>0)	
	{
		num_len = num_len - co;
		
		if(num_len<0)	
		{
        	co=num_len+co;
        	num_len=0;
		}
		temp = "," + a_Value.substr(num_len,co) + temp;
	}
	return fl + temp.substr(1) + sRemain;
}

function IsNumericKey()  
{
	if (
		event.keyCode == 8 ||
		event.keyCode == 9 ||
		event.keyCode == 35 ||
		event.keyCode == 36 ||
		event.keyCode == 37 ||
		event.keyCode == 39 ||
		event.keyCode == 38 ||
		event.keyCode == 40 ||
		event.keyCode == 46 ||
		event.keyCode == 189 ||
		event.keyCode == 109 ||
		event.keyCode == 110 ||		// "."
		event.keyCode == 190			// 키패드 "."
		)
    	return true;
	else
    	return false;
}
					
function SetEventPass() 
{
		event.cancelBubble = false;
		event.returnValue = true;
}

function SetEventCancel() 
{
		event.cancelBubble = true;
		event.returnValue = false;
}
