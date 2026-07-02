using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// EtcBuyingOrder에 대한 요약 설명입니다.
	/// </summary>
	public class EtcBuyingOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.TextBox txtQuantity;
		protected System.Web.UI.WebControls.TextBox txtTotalCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnReg;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdHistoryIndex;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		// 사용자가 입력항 항목을 DataTable로 만들어 바인딩 될 맴버 DataTable
		DataTable m_dtbl_DataSource;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;

		// 입력하고 결과를 받아올 맴버 DataSet
		DataSet m_Ds = new DataSet();

		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if ( !IsPostBack )
			{
				BindData();
			}

			// true 가 반환되면 등록가능
			if ( !MonthClosing() )
			{
				// 월마감 되었음. - 등록 불가
				Response.Write("<script>var MonthCloseing = \"1\"; </script>");	// 전역변수에 "1" 을 할당
			}
			else
			{
				Response.Write("<script>var MonthCloseing = \"0\"; </script>");	// 전역변수에 "0" 을 할당
			}
		}

		private void BindData()
		{
			// 등록 버튼을 눌렀을 때 입력항목이 유효한지 클라이언트 스크립트 함수를 매핑
			btnReg.Attributes.Add("onclick", "return CheckForm('등록');");

			// 수정 버튼을 눌렀을 때 입력항목이 유효한지 클라이언트 스크립트 함수를 매핑
			btnUpdate.Attributes.Add("onclick", "return CheckForm('수정');");

			// 삭제 버튼을 눌렀을 때 삭제여부 확인
			btnDelete.Attributes.Add("onclick", "return confirm('삭제 하시겠습니까?')");

			//  OnKeyDown 이벤트가 발생하면 숫자만 받도록 하는 자바스크립트 함수 매핑
			txtCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

			// OnKeyUp 이벤트가 발생하면 콤마를 찍는 자바스크립트 함수 매핑
			txtCost.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");

			// OnKeyDown 이벤트가 발생하면 숫자만 받도록 하는 자바스크립트 함수 매핑
			txtQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

			// OnKeyUp 이벤트가 발생하면 자동으로 계산하고 콤마를 찍는 자바스크립트 함수 매핑
			txtQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");


			
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1800'" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			ddlItemClassification1.DataSource = ds_ItemClassification1;
			ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemClassification1.DataBind();

			wcDeliveryDate.NullDateLabel = DateTime.Now.ToShortDateString();
			
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
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		/// <summary>
		/// 맴버변수인 DataTable dtbl_DataSource 객체에 매개변수로 입력된 정보를 추가한다.
		/// </summary>
		/// <param name="strArrValue">입력될 정보</param>
		/// <returns>입력완료된 DataTable</returns>
		private DataTable InputDataTable_Fill(DataTable dt_InputData)
		{
			
			DataRow drow = m_dtbl_DataSource.NewRow();

			drow[0] = dt_InputData.Rows[0][0];		// 품목명
			drow[1] = dt_InputData.Rows[0][1];		// 거래처명
			drow[2] = dt_InputData.Rows[0][2];		// 거래처등록번호
			drow[3] = dt_InputData.Rows[0][3];		// 개별단가
			drow[4] = dt_InputData.Rows[0][4];		// 수량
			drow[5] = dt_InputData.Rows[0][5];		// 총금액
			drow[6] = dt_InputData.Rows[0][6];		// 납기일
			drow[7] = dt_InputData.Rows[0][7];		// 구분
			drow[8] = dt_InputData.Rows[0][8];		// 코드
			drow[9] = dt_InputData.Rows[0][9];		// 인덱스 번호

			m_dtbl_DataSource.Rows.Add(drow);
			return m_dtbl_DataSource;			
		}

		/// <summary>
		/// 맴버변수인 DataTable dtbl_DataSource 객체에 매개변수로 입력된 정보를 수정한다.
		/// </summary>
		/// <param name="strArrValue">수정될 정보</param>
		/// <returns>수정완료된 DataTable</returns>
		private DataTable InputDataTable_Update(DataTable dt_InputData)
		{
			// Select() 메서드로 검색된 결과는 당연히 한 레코드 이므로 [0]번 Row만 수정한다.
			DataRow[] foundRows = m_dtbl_DataSource.Select("인덱스번호 = " + dt_InputData.Rows[0][9]);

			foundRows[0][0] = dt_InputData.Rows[0][0];		// 품목명
			foundRows[0][1] = dt_InputData.Rows[0][1];		// 거래처명
			foundRows[0][2] = dt_InputData.Rows[0][2];		// 거래처등록번호
			foundRows[0][3] = dt_InputData.Rows[0][3];		// 개별단가
			foundRows[0][4] = dt_InputData.Rows[0][4];		// 수량
			foundRows[0][5] = dt_InputData.Rows[0][5];		// 총금액
			foundRows[0][6] = dt_InputData.Rows[0][6];		// 납기일
			foundRows[0][7] = dt_InputData.Rows[0][7];		// 구분
			foundRows[0][8] = dt_InputData.Rows[0][8];		// 코드

			return m_dtbl_DataSource;			
		}




		/// <summary>
		/// 맴버변수인 DataTable dtbl_DataSource 의 객체를 생성하고 미리 정의된 컬럼들을 추가한다.
		/// </summary>
		private void InputDataTable_Create()
		{
			m_dtbl_DataSource = new DataTable();

			DataColumn dcol_ItemName = new DataColumn("품목명", typeof(string));
			DataColumn dcol_CompanyName = new DataColumn("거래처명", typeof(string));
			DataColumn dcol_BusinessRegNum = new DataColumn("거래처등록번호", typeof(string));
			DataColumn dcol_Cost = new DataColumn("개별단가", typeof(string));
			DataColumn dcol_Quantity = new DataColumn("수량", typeof(string));
			DataColumn dcol_TotalCost = new DataColumn("총금액", typeof(string));
			DataColumn dcol_DeliveryDate = new DataColumn("납기요구일", System.Type.GetType("System.DateTime") );
			DataColumn dcol_Reason = new DataColumn("구분", typeof(string));
			DataColumn dcol_ReasonCode = new DataColumn("구분코드", typeof(string));
			DataColumn dcol_IndexNum = new DataColumn("인덱스번호", typeof(int));
			
			m_dtbl_DataSource.Columns.Add( dcol_ItemName );
			m_dtbl_DataSource.Columns.Add( dcol_CompanyName );
			m_dtbl_DataSource.Columns.Add( dcol_BusinessRegNum );
			m_dtbl_DataSource.Columns.Add( dcol_Cost );
			m_dtbl_DataSource.Columns.Add( dcol_Quantity );
			m_dtbl_DataSource.Columns.Add( dcol_TotalCost );
			m_dtbl_DataSource.Columns.Add( dcol_DeliveryDate );
			m_dtbl_DataSource.Columns.Add( dcol_Reason );
			m_dtbl_DataSource.Columns.Add( dcol_ReasonCode );
			m_dtbl_DataSource.Columns.Add( dcol_IndexNum );
		}

		// 물리적인 DB에 입력하면서 입력된 결과를 Select 해온다.
		private void DB_Regist(string[] strArrValue)
		{
			SqlTransaction tran = null;
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrder_Regist", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.CommandType  =CommandType.StoredProcedure;
			
			adap.SelectCommand.Parameters.Add("@itemNum", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = strArrValue[3];
			adap.SelectCommand.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrValue[4];
			adap.SelectCommand.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = strArrValue[5];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(strArrValue[6]);
			adap.SelectCommand.Parameters.Add("@ReasonCode", SqlDbType.VarChar).Value = strArrValue[7];
			adap.SelectCommand.Parameters.Add("@ReasonName", SqlDbType.VarChar).Value = strArrValue[8];
			adap.SelectCommand.Parameters.Add("@RegistrationPersonID", SqlDbType.VarChar).Value = strArrValue[9];

			try
			{
				adap.SelectCommand.Connection.Open();
				tran = adap.SelectCommand.Connection.BeginTransaction();
				adap.SelectCommand.Transaction = tran;

				// 데이터를 입력하고 입력된 결과를 Select 해와서 맴버변수인 m_Ds에 저장함.
				adap.Fill(m_Ds);

				// 정상적으로 입력되어 Select 해오면 1건의 행이 나온다.
				if ( m_Ds.Tables[0].Rows.Count > 0 )
				{
					tran.Commit();
				}
				else
				{
					tran.Rollback();
				}
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				adap.SelectCommand.Connection.Close();
			}
		}

		private void btnReg_Click(object sender, System.EventArgs e)
		{
			if(txtItemName.Text.Length == 0)
			{
				RegisterStartupScript("","<script> alert('품목은 반드시 입력하셔야 합니다!');</script>");
			}
			else if(CSC1.BusinessRegistrationNum == "")
			{
				RegisterStartupScript("","<script> alert('거래처를 명확히 입력하셔야 합니다!');</script>");
			}
			else if(txtCost.Text.Trim() == "" || txtCost.Text.Trim() == "0")
			{
				RegisterStartupScript("","<script> alert('단가은 반드시 입력하셔야 합니다!');</script>");
			}
			else if(txtQuantity.Text.Trim() == "" || txtQuantity.Text.Trim() =="0")
			{
				RegisterStartupScript("","<script> alert('수량은 반드시 입력하셔야 합니다!');</script>");
			}
			else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
			{
				RegisterStartupScript("","<script> alert('사업자번호가 거래처명과 다릅니다!');</script>");
			}
			else
			{
				string[] strArrValue = new string[10];

				strArrValue[0] = txtItemName.Text;
				strArrValue[1] = CSC1.Company;
				strArrValue[2] = CSC1.BusinessRegistrationNum;
				strArrValue[3] = txtCost.Text;
				strArrValue[4] = txtQuantity.Text;
				strArrValue[5] = txtTotalCost.Text;
				strArrValue[6] = wcDeliveryDate.Text;
				strArrValue[7] = ddlItemClassification1.SelectedItem.Value;
				strArrValue[8] = ddlItemClassification1.SelectedItem.Text;
				strArrValue[9] = Session["ID"].ToString();

				this.DB_Regist( strArrValue );

				if ( Session["EtcBO_InputResult"] == null )
				{
					InputDataTable_Create();
					this.m_dtbl_DataSource = InputDataTable_Fill( m_Ds.Tables[0] );

					UltraWebGrid1.DataSource = m_dtbl_DataSource.DefaultView;
					UltraWebGrid1.DataBind();

					Session["EtcBO_InputResult"] = m_dtbl_DataSource;
				}
				else
				{
					this.m_dtbl_DataSource = (DataTable)Session["EtcBO_InputResult"];
					this.m_dtbl_DataSource = InputDataTable_Fill( m_Ds.Tables[0] );

					UltraWebGrid1.DataSource = m_dtbl_DataSource.DefaultView;
					UltraWebGrid1.DataBind();
				}
			}
		}

		private bool Compare(string Company, string BusinessRegistrationNum)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand com = new SqlCommand();
			com.Connection = con;
			con.Open();
			string str = "Select Count(BusinessRegistrationNum) From CI_MT where RecodingState =1 and CompanyName = @Com and BusinessRegistrationNum = @Num";
			com.CommandText = str;
			com.Parameters.Add("@Com",Company);
			com.Parameters.Add("@Num",BusinessRegistrationNum);
			int a = int.Parse(com.ExecuteScalar().ToString());
			com.Parameters.Clear();
			con.Close();

			if(a == 0)
				return false;
			else
				return true;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string strHDIndex = hdHistoryIndex.Value;
			DB_Delete(strHDIndex);
		}


		private void DB_Delete(string strHDIndex)
		{
			SqlTransaction tran = null;
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("DELETE EtcBO_HT where EtcBuyingOrderHistoryIndex = @index", con);
			cmd.Parameters.Add("@index", SqlDbType.Int).Value = strHDIndex;

			try
			{
				con.Open();
				tran = con.BeginTransaction();
				cmd.Transaction = tran;

				cmd.ExecuteNonQuery();
				tran.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				con.Close();	
			}
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if(txtItemName.Text.Length == 0)
			{
				RegisterStartupScript("","<script> alert('품목은 반드시 입력하셔야 합니다!');</script>");
			}
			else if(CSC1.BusinessRegistrationNum == "")
			{
				RegisterStartupScript("","<script> alert('거래처를 명확히 입력하셔야 합니다!');</script>");
			}
			else if(txtCost.Text.Trim() == "" || txtCost.Text.Trim() == "0")
			{
				RegisterStartupScript("","<script> alert('단가은 반드시 입력하셔야 합니다!');</script>");
			}
			else if(txtQuantity.Text.Trim() == "" || txtQuantity.Text.Trim() =="0")
			{
				RegisterStartupScript("","<script> alert('수량은 반드시 입력하셔야 합니다!');</script>");
			}
			else
			{
				string[] strArrValue = new string[10];

				strArrValue[0] = txtItemName.Text;
				strArrValue[1] = CSC1.Company;
				strArrValue[2] = CSC1.BusinessRegistrationNum;
				strArrValue[3] = txtCost.Text;
				strArrValue[4] = txtQuantity.Text;
				strArrValue[5] = txtTotalCost.Text;
				strArrValue[6] = wcDeliveryDate.Text;
				strArrValue[7] = ddlItemClassification1.SelectedItem.Value;
				strArrValue[8] = ddlItemClassification1.SelectedItem.Text;
				strArrValue[9] = hdHistoryIndex.Value;

				this.DB_Update( strArrValue );
				this.m_dtbl_DataSource = (DataTable)Session["EtcBO_InputResult"];
				this.m_dtbl_DataSource = InputDataTable_Update( m_Ds.Tables[0] );

				UltraWebGrid1.DataSource = m_dtbl_DataSource.DefaultView;
				UltraWebGrid1.DataBind();
			}
		}

		// 물리적인 DB에 수정하면서 수정된 결과를 Select 해온다.
		private void DB_Update(string[] strArrValue)
		{
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrder_Update", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.CommandType  =CommandType.StoredProcedure;
			SqlTransaction tran = null;

			adap.SelectCommand.Parameters.Add("@itemNum", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = strArrValue[3];
			adap.SelectCommand.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrValue[4];
			adap.SelectCommand.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = strArrValue[5];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(strArrValue[6]);
			adap.SelectCommand.Parameters.Add("@ReasonCode", SqlDbType.VarChar).Value = strArrValue[7];
			adap.SelectCommand.Parameters.Add("@ReasonName", SqlDbType.VarChar).Value = strArrValue[8];
			adap.SelectCommand.Parameters.Add("@IndexNum", SqlDbType.Int).Value = strArrValue[9];

			try
			{
				adap.SelectCommand.Connection.Open();
				tran = adap.SelectCommand.Connection.BeginTransaction();
				adap.SelectCommand.Transaction = tran;

				// 데이터를 입력하고 입력된 결과를 Select 해와서 맴버변수인 m_Ds에 저장함.
				adap.Fill(m_Ds);

				// 정상적으로 입력되어 Select 해오면 1건의 행이 나온다.
				if ( m_Ds.Tables[0].Rows.Count > 0 )
				{
					tran.Commit();
				}
				else
				{
					tran.Rollback();
				}
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				adap.SelectCommand.Connection.Close();
			}
		}


		/// <summary>
		/// 월마감 여부를 확인하는 메소드
		/// </summary>
		/// <returns></returns>
		public bool MonthClosing()
		{
			int intYear = 0;
			int intMonth = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select [ClosingYear], [ClosingMonth] From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";

			try
			{
				conn.Open();
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					intYear = int.Parse(dr[0].ToString());
					intMonth = int.Parse(dr[1].ToString());
				}
			} 
			catch ( Exception e)
			{
				Response.Write("<script>alert(\"" + e.Message + "\")</script>"); 
			} 
			finally
			{
				conn.Close();
			}
			if(intYear < int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
			{
				return true;
			}
			else if(intYear == int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
			{
				if(intMonth < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
					return true;
				else
					return false;
			}
			else
				return false;
		}

	}
}
