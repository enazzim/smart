namespace KIT_ERP.Community.CompanyControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		CompanyControl에 대한 요약 설명입니다.
	/// </summary>
	public class CompanyControl : System.Web.UI.UserControl
	{

		protected System.Web.UI.WebControls.TextBox txtCompanyName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCompanyNameOpen;
		protected string strPost = "false";
		protected string strBusinessRegistrationNum = "";
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtBusinessRegistrationNum;
		protected string strUnitCostDistinction = "";
		protected string strItemNum = "";


		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			this.AttributesBind();
		}

		#region Web Form 디자이너에서 생성한 코드
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		디자이너 지원에 필요한 메서드입니다. 이 메서드의 내용을
		///		코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		/// <summary>
		///  팝업창을 오픈할때 필요한 정보를 세팅
		/// </summary>
		private void AttributesBind()
		{
			//btnCompanyNameOpen.Attributes.Add("onclick", "Popupopen('" + txtCompanyName.ClientID + "', 'CompanyName')");
			//'" + propertyClassification + "', '" + strBusinessRegistrationNum + "', '" + strUnitCostDistinction + "' )");
			btnCompanyNameOpen.Attributes.Add("onclick", "Popupopen('" + txtCompanyName.ClientID + "', 'CompanyName','" + strUnitCostDistinction + "','" + strItemNum + "' )");
		}

		/// <summary>
		/// 거래처명을 설정하거나 반환합니다.
		/// </summary>
		public string Company
		{
			get
			{
				return txtCompanyName.Text;
			}
			set
			{
				txtCompanyName.Text = value;
			}
		}

		/// <summary>
		/// 거래처명을 설정하거나 반환합니다.
		/// </summary>
		public string BusinessRegistrationNum
		{
			get
			{
				return txtBusinessRegistrationNum.Value;
			}
			set
			{
				txtBusinessRegistrationNum.Value = value;
			}
		}

		/// <summary>
		/// 현재 페이지에서 품목을 검색한후 JS 포스트백 함수(DoPost())를 호출해야 하는지의 여부를 결정합니다. - 기본값은 false
		/// </summary>
		public bool IsDoPost1
		{
			set 
			{
				if ( value )
				{
					this.strPost = "true";
				}
				else
				{
					this.strPost = "false";
				}
			}
		}

		/// <summary>
		/// 품목번호를 설정하거나 반환합니다.
		/// </summary>
		public string ItemNum
		{
			set
			{
				this.strItemNum = value;
			}		
		}

		/// <summary>
		/// 입력된 단가구분의 품목만 팝업창에 가져온다.(판매단가, 외주단가, 구매단가...등)
		/// </summary>
		public string UnitCostDistinction
		{
			set
			{
				this.strUnitCostDistinction = value;
				AttributesBind();
			}
		}
	}
}
