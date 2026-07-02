using System;
using System.Web.UI.WebControls;
using System.Web;

namespace KIT_ERP.Common
{
	/// <summary>
	/// 마우스 우측버튼 클릭시 나타나는 Popup Context 메뉴를 나타낸다.
	/// </summary>
	public class RegisterRButtonDownScript : System.Web.UI.Page
	{
		public RegisterRButtonDownScript()
		{
		}

		public void Activate()
		{
			string scriptString = @"<SCRIPT Language=""JavaScript"">
									function RButtonDown() 
									{ 
										if ((event.button == 2) || (event.button == 3)) 
										{
											var vTop = event.clientY;
											var vLeft = event.clientX;
											vTop += 200;						
											vLeft += 220;
											window.showModalDialog(""/KIT_ERP/Common/PopUp_ContextMenu.aspx"",""dddd"",""dialogHeight: 400px; dialogWidth: 250px; help: no; resizable: no; status: no; dialogLeft:"" + vLeft +""px; dialogTop:"" + vTop + ""px; "");
										} 
									} 
									document.onmousedown=RButtonDown; 
								</SCRIPT>";

			HttpContext hc = HttpContext.Current;
			hc.Response.Write(scriptString);
		}
	}
}
