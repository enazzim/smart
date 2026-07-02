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

namespace KIT_ERP.Community.Popup
{
	/// <summary>
	/// MonthlyClosingInfo에 대한 요약 설명입니다.
	/// </summary>
	public class MonthlyClosingInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
		string CGeneralDataIndex;
		string CTechnologyDataIndex;
		string CCompanyStandardIndex;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CGeneralDataIndex = Request.QueryString["CGeneralDataIndex"];
			CTechnologyDataIndex = Request.QueryString["CTechnologyDataIndex"];
			CCompanyStandardIndex = Request.QueryString["CCompanyStandardIndex"];

			if(!IsPostBack)
			{
				Label1.Text = "<월마감 정보>";
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Con.Open();
				Cmd.CommandText = "SELECT MonthClosingInfoIndex, AffairDistinction, ClosingYear, ClosingMonth, ClosingPersonID, ClosingPerson FROM MCI_MT Where RecodingState = 1 ORDER BY ClosingYear DESC, ClosingMonth DESC";

				SqlDataAdapter adp = new SqlDataAdapter(Cmd);
				DataSet ds = new DataSet();

				adp.Fill(ds);
				DataGrid1.DataSource = ds.Tables[0].DefaultView;
				DataGrid1.DataBind();
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		// 창닫기 버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.close();</script>");
		}



		// 등록버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			bool count = false;    // 체크된 항목이 없을경우 사용하기 위해...
			
			foreach(DataGridItem item in DataGrid1.Items)
			{
				CheckBox cb = ((CheckBox)item.FindControl("CheckBox1"));
				if(cb.Checked)
				{
					count = true;
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					Con.Open();
					SqlCommand Cmd = new SqlCommand();
					SqlTransaction trans = Con.BeginTransaction();
					Cmd.Connection = Con;
					Cmd.Transaction = trans;
					
					try
					{
						if(CGeneralDataIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('월마감정보', "+ item.Cells[1].Text +", 'CGD_T', "+ CGeneralDataIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../GnAdjustment.aspx?CGeneralDataIndex=" +CGeneralDataIndex+ "'; self.close();  </script>");
						}
						else if(CTechnologyDataIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('월마감정보', "+ item.Cells[1].Text +", 'CTD_T', "+ CTechnologyDataIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../TnAdjustment.aspx?CTechnologyDataIndex=" +CTechnologyDataIndex+ "'; self.close();  </script>");
						}
						else
						{
							Cmd.CommandText = "INSERT CR_T VALUES('월마감정보', "+ item.Cells[1].Text +", 'CCS_T', "+ CCompanyStandardIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../CSAdjustment.aspx?CCompanyStandardIndex=" +CCompanyStandardIndex+ "'; self.close();  </script>");
						}
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
					}
				}				
			}
			
			// 체크된 항목이 없을경우...
			if(!count)
				Response.Write("<script>alert('선택된 내용이 없습니다.');</script>");
		}




		// 마우스 오버시 색깔...
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}
	}
}
