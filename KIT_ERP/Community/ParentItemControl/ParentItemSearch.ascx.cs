namespace KIT_ERP.Community.ParentItemControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		ItemSearch에 대한 요약 설명입니다.
	/// </summary>
	public class ParentItemSearch : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnItemDrawNumOpen;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnItemNumOpen;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnItemNameOpen;
		protected bool 상품 = false, 제품 = false, 팬텀 = false, 반제품 = false, 원자재 = false;
		protected string strPost = "false";
		protected string strBusinessRegistrationNum = "";
		protected System.Web.UI.WebControls.TextBox txtItemNum;
		protected System.Web.UI.WebControls.TextBox txtItemDrawNum;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		public System.Web.UI.WebControls.Label lbItemNum;
		public System.Web.UI.WebControls.Label lbItemDrawNum;
		public System.Web.UI.WebControls.Label lbItemName;
		protected string strUnitCostDistinction = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
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
			string propertyClassification = 상품 + ";" + 제품 + ";" + 팬텀 + ";" + 반제품 + ";" + 원자재;

			btnItemNumOpen.Attributes.Add
				("onclick", "openPopup('" + txtItemNum.ClientID + "', 'ItemNum', '" + propertyClassification + "', '" + strBusinessRegistrationNum + "', '" + strUnitCostDistinction + "' )");

			btnItemDrawNumOpen.Attributes.Add
				("onclick", "openPopup('" + txtItemDrawNum.ClientID + "', 'ItemDrawNum', '" + propertyClassification + "', '" + strBusinessRegistrationNum + "', '" + strUnitCostDistinction + "' )");

			btnItemNameOpen.Attributes.Add
				("onclick", "openPopup('" + txtItemName.ClientID + "', 'ItemName', '" + propertyClassification + "', '" + strBusinessRegistrationNum + "', '" + strUnitCostDistinction + "' )");
		}

		/// <summary>
		/// 품목번호를 설정하거나 반환합니다.
		/// </summary>
		public string ItemNum
		{
			get
			{
				return txtItemNum.Text;
			}
			set
			{
				txtItemNum.Text = value;
			}
		}

		/// <summary>
		/// 도면번호를 설정하거나 반환합니다.
		/// </summary>
		public string ItemDrawNum
		{
			get
			{
				return txtItemDrawNum.Text;
			}
			set
			{
				txtItemDrawNum.Text = value;
			}
		}

		/// <summary>
		/// 품목명을 설정하거나 반환합니다.
		/// </summary>
		public string ItemName
		{
			get
			{
				return txtItemName.Text;
			}
			set
			{
				txtItemName.Text = value;
			}
		}

		//품목번호레이블 텍스트 변경
		public string lbNum
		{
			set
			{
				lbItemNum.Text = value;
			}
		}
		public string lbDraw
		{
			set
			{
				lbItemDrawNum.Text = value;
			}
		}
		public string lbName
		{
			set
			{
				lbItemName.Text = value;
			}
		}

		/// <summary>
		/// 품목검색시 자산분류가 상품을 검색할 것 인지를 결정합니다. - 기본값은 false
		/// </summary>
		public bool Commodity
		{
			set
			{
				this.상품 = value;
			}
		}

		/// <summary>
		/// 품목검색시 자산분류가 제품을 검색할 것 인지를 결정합니다. - 기본값은 false
		/// </summary>
		public bool Products
		{
			set
			{
				this.제품 = value;
			}
		}

		/// <summary>
		/// 품목검색시 자산분류가 팬텀을 검색할 것 인지를 결정합니다. - 기본값은 false
		/// </summary>
		public bool Phantom
		{
			set
			{
				this.팬텀 = value;
			}
		}

		/// <summary>
		/// 품목검색시 자산분류가 반제품을 검색할 것 인지를 결정합니다. - 기본값은 false
		/// </summary>
		public bool HalfFinishedProducts
		{
			set
			{
				this.반제품 = value;
			}
		}

		/// <summary>
		/// 품목검색시 자산분류가 원자재를 검색할 것 인지를 결정합니다. - 기본값은 false
		/// </summary>
		public bool  RawMaterials
		{
			set
			{
				this.원자재 = value;
			}
		}

		/// <summary>
		/// ItemSearchControl 유저컨트롤의 모든 텍스트 박스를 초기화 시킵니다.
		/// </summary>
		public void ClearTextBox()
		{
			txtItemName.Text = "";
			txtItemDrawNum.Text = "";
			txtItemNum.Text = "";
		}


		/// <summary>
		/// 현재 페이지에서 품목을 검색한후 JS 포스트백 함수(DoPost())를 호출해야 하는지의 여부를 결정합니다. - 기본값은 false
		/// </summary>
		public bool IsDoPost
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
		/// 해당하는 거래처의 품목만 팝업창을 통해 보여준다. - 단가 테이블(UCI_MT)에서 가져옴
		/// </summary>
		public string BusinessRegistrationNum
		{
			set
			{
				this.strBusinessRegistrationNum = value;
				AttributesBind();
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
