using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using Microsoft.Win32;
using System.Configuration;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// UserCompanyInput에 대한 요약 설명입니다.
	/// </summary>
	public class UserCompanyInput : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.TextBox txtCompanyName;
		protected System.Web.UI.WebControls.TextBox txtPresidentName;
		protected System.Web.UI.WebControls.TextBox txtBusinessCompanyNum;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.TextBox txtCorporationRegistrationNum;
		protected System.Web.UI.WebControls.TextBox txtBusinessClassification;
		protected System.Web.UI.WebControls.TextBox txtBusinessItem;
		protected System.Web.UI.WebControls.TextBox txtBusinessCompanyAddress;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtTaxBillAddress;
		protected System.Web.UI.WebControls.TextBox txtBusinessBillAddress;
		protected System.Web.UI.WebControls.TextBox txtPost2;
		protected System.Web.UI.WebControls.TextBox txtPost5;
		protected System.Web.UI.WebControls.TextBox txtPost6;
		protected System.Web.UI.WebControls.TextBox txtPost4;
		protected System.Web.UI.WebControls.TextBox txtPost3;
		protected System.Web.UI.WebControls.Button btnPost1;
		protected System.Web.UI.WebControls.Button btnPost3;
		protected System.Web.UI.WebControls.Button btnPost2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtPost1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;

		//private static bool checkBusinessCompanyNum = false;
		//private bool checkBusinessCompanyNum = false;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				//Session.Abandon();
				//Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				// MainTitle Frame에 해당 페이지 Title출력
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 시스템정보 > 사용회사 정보입력';</script>");

				btnPost1.Attributes.Add("onclick", "Popup('PopupWindows/PostSearch.aspx?id=Post1', window, '330', '260');");
				btnPost2.Attributes.Add("onclick", "Popup('PopupWindows/PostSearch.aspx?id=Post2', window, '330', '260');");
				btnPost3.Attributes.Add("onclick", "Popup('PopupWindows/PostSearch.aspx?id=Post3', window, '330', '260');");
				
				// 현재 설정되어있는 회사 정보를 가져옴
//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string strSQL = @"SELECT TOP 1 * FROM UCI_T";
				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					txtCompanyName.Text = reader["CompanyName"].ToString();
					txtPresidentName.Text = reader["PresidentName"].ToString();
					txtBusinessCompanyNum.Text = reader["BusinessCompanyNum"].ToString();
					txtCorporationRegistrationNum.Text = reader["CorporationRegistrationNum"].ToString();
					txtBusinessClassification.Text = reader["BusinessClassification"].ToString();
					txtBusinessItem.Text = reader["BusinessItem"].ToString();
					txtPost1.Text = reader["BusinessCompanyAddress"].ToString().Substring(0,3);
					txtPost2.Text = reader["BusinessCompanyAddress"].ToString().Substring(3,3);
					txtBusinessCompanyAddress.Text = reader["BusinessCompanyAddress"].ToString().Substring(6);
					txtPost3.Text = reader["BusinessBillAddress"].ToString().Substring(0,3);
					txtPost4.Text = reader["BusinessBillAddress"].ToString().Substring(3,3);
					txtBusinessBillAddress.Text = reader["BusinessBillAddress"].ToString().Substring(6);
					txtPost5.Text = reader["TaxBillAddress"].ToString().Substring(0,3);
					txtPost6.Text = reader["TaxBillAddress"].ToString().Substring(3,3);
					txtTaxBillAddress.Text = reader["TaxBillAddress"].ToString().Substring(6);
					//checkBusinessCompanyNum = true;
				}
				reader.Close();
				conn.Close();
			}
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
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if((txtCompanyName.Text != "") && (txtBusinessCompanyNum.Text != "") && (txtPresidentName.Text != "")
				&& (txtBusinessClassification.Text != "") && (txtBusinessItem.Text != "") && (txtPost1.Text != "")
				&& (txtPost2.Text != "") && (txtBusinessCompanyAddress.Text != "") && (txtPost3.Text != "")
				&& (txtPost4.Text != "") && (txtBusinessBillAddress.Text != "") && (txtPost5.Text != "")
				&& (txtPost6.Text != "") && (txtTaxBillAddress.Text != ""))
			{
//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string strSQL = "";
				string message = "";

				string str = "Select Count(*) From UCI_T";
				SqlCommand comm1 = new SqlCommand(str,conn);
				int count = int.Parse(comm1.ExecuteScalar().ToString());
				

				if(count == 0)
				{
					strSQL = @"INSERT INTO UCI_T (CompanyName, PresidentName, BusinessCompanyNum, CorporationRegistrationNum,
							BusinessClassification, BusinessItem, BusinessCompanyAddress, BusinessBillAddress, TaxBillAddress)
							VALUES (@CompanyName, @PresidentName, @BusinessCompanyNum, @CorporationRegistrationNum,
							@BusinessClassification, @BusinessItem, @BusinessCompanyAddress, @BusinessBillAddress, @TaxBillAddress)";
				
					message = "등록되었습니다.";
				
					//checkBusinessCompanyNum = true;
				}
				else
				{
					strSQL = @"UPDATE UCI_T SET CompanyName = @CompanyName, PresidentName = @PresidentName, BusinessCompanyNum = @BusinessCompanyNum,
							CorporationRegistrationNum = @CorporationRegistrationNum, BusinessClassification = @BusinessClassification,
							BusinessItem = @BusinessItem, BusinessCompanyAddress = @BusinessCompanyAddress,
							BusinessBillAddress = @BusinessBillAddress, TaxBillAddress = @TaxBillAddress";

					message = "수정되었습니다.";
				}

				SqlCommand comm = new SqlCommand(strSQL, conn);
				comm.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = txtCompanyName.Text;
				comm.Parameters.Add("@PresidentName", SqlDbType.VarChar).Value = txtPresidentName.Text;
				comm.Parameters.Add("@BusinessCompanyNum", SqlDbType.VarChar).Value = txtBusinessCompanyNum.Text;
				comm.Parameters.Add("@CorporationRegistrationNum", SqlDbType.VarChar).Value = txtCorporationRegistrationNum.Text;
				comm.Parameters.Add("@BusinessClassification", SqlDbType.VarChar).Value = txtBusinessClassification.Text;
				comm.Parameters.Add("@BusinessItem", SqlDbType.VarChar).Value = txtBusinessItem.Text;
				comm.Parameters.Add("@BusinessCompanyAddress", SqlDbType.VarChar).Value = txtPost1.Text + txtPost2.Text + txtBusinessCompanyAddress.Text;
				comm.Parameters.Add("@BusinessBillAddress", SqlDbType.VarChar).Value = txtPost3.Text + txtPost4.Text + txtBusinessBillAddress.Text;
				comm.Parameters.Add("@TaxBillAddress", SqlDbType.VarChar).Value = txtPost5.Text + txtPost6.Text + txtTaxBillAddress.Text;
				comm.ExecuteNonQuery();
				conn.Close();
				Response.Write("<script>alert('" + message.ToString() + "');</script>");
			}
			else
			{
				Response.Write("<script>alert('필수 입력항목 중 입력되지 않은 항목이 있습니다.');</script>");
			}
		}
	}
}
