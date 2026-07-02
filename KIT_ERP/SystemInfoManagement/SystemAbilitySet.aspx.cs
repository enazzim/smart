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
	/// SystemAbilitySet에 대한 요약 설명입니다.
	/// </summary>
	public class SystemAbilitySet : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.CheckBox chkPresentRowMaterialUsed;
		protected System.Web.UI.WebControls.CheckBox chkSafeyRowMaterialUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderNonInStorehouseUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderGapInStorehouseUsed;
		protected System.Web.UI.WebControls.CheckBox chkMinimumGapUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderRequestStandbyUsed;
		protected System.Web.UI.WebControls.RadioButton rdoContinuityWorkProgressTrue;
		protected System.Web.UI.WebControls.RadioButton rdoContinuityWorkProgressFalse;
		protected System.Web.UI.WebControls.RadioButton rdoDailyWorkProgressTrue;
		protected System.Web.UI.WebControls.RadioButton rdoDailyWorkProgressFalse;
		protected System.Web.UI.WebControls.RadioButton rdoMinusRowMaterialPermissionUsedTrue;
		protected System.Web.UI.WebControls.RadioButton rdoMinusRowMaterialPermissionUsedFalse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingMonth;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingPerson;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingYear;

		private static bool checkSCS = false;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				// MainTitle Frame에 해당 페이지 Title출력
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 시스템정보 > 시스템 기능설정';</script>");

				// 현재 설정되어있는 각종 정보를 가져옴
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string strSQL = @"SELECT TOP 1 * FROM SCS_T";
				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					if(bool.Parse(reader["PresentRowMaterialUsed"].ToString()) == true)
						chkPresentRowMaterialUsed.Checked = true;
					else
						chkPresentRowMaterialUsed.Checked = false;
					if(bool.Parse(reader["SafeyRowMaterialUsed"].ToString()) == true)
						chkSafeyRowMaterialUsed.Checked = true;
					else
						chkSafeyRowMaterialUsed.Checked = false;
					if(bool.Parse(reader["OrderNonInStorehouseUsed"].ToString()) == true)
						chkOrderNonInStorehouseUsed.Checked = true;
					else
						chkOrderNonInStorehouseUsed.Checked = false;
					if(bool.Parse(reader["OrderGapInStorehouseUsed"].ToString()) == true)
						chkOrderGapInStorehouseUsed.Checked = true;
					else
						chkOrderGapInStorehouseUsed.Checked = false;
					if(bool.Parse(reader["MinimumGapUsed"].ToString()) == true)
						chkMinimumGapUsed.Checked = true;
					else
						chkMinimumGapUsed.Checked = false;
					if(bool.Parse(reader["OrderRequestStandbyUsed"].ToString()) == true)
						chkOrderRequestStandbyUsed.Checked = true;
					else
						chkOrderRequestStandbyUsed.Checked = false;

					if(bool.Parse(reader["ContinuityWorkProgress"].ToString()) == true)
						rdoContinuityWorkProgressTrue.Checked = true;
					else
						rdoContinuityWorkProgressFalse.Checked = true;
					if(bool.Parse(reader["DailyWorkProgress"].ToString()) == true)
						rdoDailyWorkProgressTrue.Checked = true;
					else
						rdoDailyWorkProgressFalse.Checked = true;
					if(bool.Parse(reader["MinusRowMaterialPermissionUsed"].ToString()) == true)
						rdoMinusRowMaterialPermissionUsedTrue.Checked = true;
					else
						rdoMinusRowMaterialPermissionUsedFalse.Checked = true;

					checkSCS = true;
				}
				reader.Close();
				conn.Close();

				SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str = "Select Name, ID From UI_MT Where RecodingState = 1";
				SqlCommand comm1 = new SqlCommand(str,conn1);
				
				SqlDataAdapter da = new SqlDataAdapter(comm1) ;
				DataSet ds= new DataSet() ;
				da.Fill(ds);
				
				dl_ClosingPerson.DataSource = ds;
				dl_ClosingPerson.DataTextField = ds.Tables[0].Columns[0].ToString();
				dl_ClosingPerson.DataValueField = ds.Tables[0].Columns[1].ToString();
				dl_ClosingPerson.DataBind();
				dl_ClosingPerson.Items.Insert(0, "-선택-") ;
				dl_ClosingPerson.Items[0].Value = "";



				

				dl_ClosingYear.SelectedItem.Value = DateTime.Now.Year.ToString();
				dl_ClosingMonth.SelectedItem.Value = DateTime.Now.Month.ToString();
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
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion




		// TODO : 설정버튼 클릭시
		private void Button2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			string strSQL = "";
			string message = "";
			
			if(!checkSCS)
			{
				strSQL = @"INSERT INTO SCS_T (PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, OrderGapInStorehouseUsed,
							MinimumGapUsed, OrderRequestStandbyUsed, ContinuityWorkProgress, DailyWorkProgress, MinusRowMaterialPermissionUsed)
							VALUES (@PresentRowMaterialUsed, @SafeyRowMaterialUsed, @OrderNonInStorehouseUsed, @OrderGapInStorehouseUsed,
							@MinimumGapUsed, @OrderRequestStandbyUsed, @ContinuityWorkProgress, @DailyWorkProgress, @MinusRowMaterialPermissionUsed)";

				message = "등록하였습니다.";
				checkSCS = true;
				
			}
			else
			{
				strSQL = @"UPDATE SCS_T SET PresentRowMaterialUsed = @PresentRowMaterialUsed, SafeyRowMaterialUsed = @SafeyRowMaterialUsed,
							OrderNonInStorehouseUsed = @OrderNonInStorehouseUsed, OrderGapInStorehouseUsed = @OrderGapInStorehouseUsed,
							MinimumGapUsed = @MinimumGapUsed, OrderRequestStandbyUsed = @OrderRequestStandbyUsed,
							ContinuityWorkProgress = @ContinuityWorkProgress, DailyWorkProgress = @DailyWorkProgress,
							MinusRowMaterialPermissionUsed = @MinusRowMaterialPermissionUsed";

				message = "수정하였습니다.";
			}
			SqlCommand comm = new SqlCommand(strSQL, conn);
			comm.Parameters.Add("@PresentRowMaterialUsed", SqlDbType.Bit).Value = chkPresentRowMaterialUsed.Checked;
			comm.Parameters.Add("@SafeyRowMaterialUsed", SqlDbType.Bit).Value = chkSafeyRowMaterialUsed.Checked;
			comm.Parameters.Add("@OrderNonInStorehouseUsed", SqlDbType.Bit).Value = chkOrderNonInStorehouseUsed.Checked;
			comm.Parameters.Add("@OrderGapInStorehouseUsed", SqlDbType.Bit).Value = chkOrderGapInStorehouseUsed.Checked;
			comm.Parameters.Add("@MinimumGapUsed", SqlDbType.Bit).Value = chkMinimumGapUsed.Checked;
			comm.Parameters.Add("@OrderRequestStandbyUsed", SqlDbType.Bit).Value = chkOrderRequestStandbyUsed.Checked;
			if(rdoContinuityWorkProgressTrue.Checked)
				comm.Parameters.Add("@ContinuityWorkProgress", SqlDbType.Bit).Value = true;
			else
				comm.Parameters.Add("@ContinuityWorkProgress", SqlDbType.Bit).Value = false;
			if(rdoDailyWorkProgressTrue.Checked)
				comm.Parameters.Add("@DailyWorkProgress", SqlDbType.Bit).Value = true;
			else
				comm.Parameters.Add("@DailyWorkProgress", SqlDbType.Bit).Value = false;
			if(rdoMinusRowMaterialPermissionUsedTrue.Checked)
				comm.Parameters.Add("@MinusRowMaterialPermissionUsed", SqlDbType.Bit).Value = true;
			else
				comm.Parameters.Add("@MinusRowMaterialPermissionUsed", SqlDbType.Bit).Value = false;

            comm.ExecuteNonQuery();

			
			conn.Close();
			Response.Write("<script>alert('" + message.ToString() + "');</script>");
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;			
			comm.Transaction = tr;
			try
			{
				if(dl_ClosingYear.SelectedIndex == 0)
				{
					throw new Exception("년도를 선택하세요!");
				}
				else if(dl_ClosingMonth.SelectedIndex==0)
				{
					throw new Exception("월을 선택하세요!");
				}
				else if(dl_ClosingPerson.SelectedIndex == 0)
				{
					throw new Exception("마감지시자를 선택하세요!");
				}
				else
				{
					string str = @"Update MCI_MT Set ClosingYear = @year, ClosingMonth = @mon, ClosingPerson = @person, ClosingPersonID= @id, UpdatingPerson = @updateperson, UpdatingPersonID=@upid, UpdatingDate = @date";
				
					comm.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(dl_ClosingYear.SelectedItem.Value);
					comm.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(dl_ClosingMonth.SelectedItem.Value);
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = dl_ClosingPerson.SelectedItem.Text;
					comm.Parameters.Add("@id",SqlDbType.VarChar).Value = dl_ClosingPerson.SelectedItem.Value;
					comm.Parameters.Add("@updateperson",SqlDbType.VarChar).Value = Session["UserName"].ToString();
					comm.Parameters.Add("@upid",SqlDbType.VarChar).Value = Session["ID"].ToString();
					comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

					comm.CommandText= str;
					comm.ExecuteNonQuery();
					tr.Commit();
					Response.Write("<script>alert('월마감을 수정하였습니다!');</script>");
				}
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('" + ee.Message + "');</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}

		}
	}
}
