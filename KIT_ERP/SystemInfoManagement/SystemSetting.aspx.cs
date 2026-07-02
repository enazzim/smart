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
	/// SystemSetting에 대한 요약 설명입니다.
	/// </summary>
	public class SystemSetting : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingPerson;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingMonth;
		protected System.Web.UI.WebControls.DropDownList dl_ClosingYear;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.RadioButton rdoMinusRowMaterialPermissionUsedFalse;
		protected System.Web.UI.WebControls.RadioButton rdoMinusRowMaterialPermissionUsedTrue;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.RadioButton RadioButton3;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.CheckBox CheckBox3;
		protected System.Web.UI.WebControls.CheckBox CheckBox2;
		protected System.Web.UI.WebControls.CheckBox CheckBox4;
		protected System.Web.UI.WebControls.CheckBox CheckBox5;
		protected System.Web.UI.WebControls.CheckBox CheckBox7;
		protected System.Web.UI.WebControls.CheckBox CheckBox8;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.CheckBox CheckBox6;
		protected System.Web.UI.WebControls.CheckBox CheckBox9;
		protected System.Web.UI.WebControls.Label Label2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string strSQL = @"SELECT TOP 1 * FROM SCS_T";
				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					if(bool.Parse(reader["PresentRowMaterialUsed"].ToString()) == true)		//현재고량
						CheckBox1.Checked = true;
					else
						CheckBox1.Checked = false;
					if(bool.Parse(reader["OrderNonInStorehouseUsed"].ToString()) == true)	//발주미입고량
						CheckBox8.Checked = true;
					else
						CheckBox8.Checked = false;
					if(bool.Parse(reader["OrderGapInStorehouseUsed"].ToString()) == true)	//발주간격수량
						CheckBox7.Checked = true;
					else
						CheckBox7.Checked = false;
					if(bool.Parse(reader["MinimumGapUsed"].ToString()) == true)				//최소발주량
						CheckBox6.Checked = true;
					else
						CheckBox6.Checked = false;
					if(bool.Parse(reader["OrderRequestStandbyUsed"].ToString()) == true)	//발주의뢰대기량
						CheckBox9.Checked = true;
					else
						CheckBox9.Checked = false;
					if(bool.Parse(reader["OutOrderUsed"].ToString()) == true)				//외주재고에대한 자가재고 포함 여부
						CheckBox2.Checked = true;
					else
						CheckBox2.Checked = false;
					if(bool.Parse(reader["MinusStockAddUsed"].ToString()) == true)				//마이너스 재고 보상 위한 작업계획 추가 여부
						CheckBox3.Checked = true;
					else
						CheckBox3.Checked = false;
					if(bool.Parse(reader["BundlingUsed"].ToString()) == true)				//동일공정 묶음 여부
						CheckBox4.Checked = true;
					else
						CheckBox4.Checked = false;
					if(bool.Parse(reader["InWorkConsidering"].ToString()) == true)				//외주수량에 대한 자가수량변경여부
						CheckBox5.Checked = true;
					else
						CheckBox5.Checked = false;
					if(bool.Parse(reader["WorkPlan1"].ToString()) == true)				//작업계획수립전략1
							RadioButton1.Checked = true;
					else
							RadioButton1.Checked = false;
					if(bool.Parse(reader["WorkPlan2"].ToString()) == true)				//작업계획수립전략2
						RadioButton2.Checked = true;
					else
						RadioButton2.Checked = false;
					if(bool.Parse(reader["WorkPlan3"].ToString()) == true)				//작업계획수립전략3
						RadioButton3.Checked = true;
					else
						RadioButton3.Checked = false;					
					if(bool.Parse(reader["MinusRowMaterialPermissionUsed"].ToString()) == true)	//마이너스재고 허용여부
						rdoMinusRowMaterialPermissionUsedTrue.Checked = true;
					else
						rdoMinusRowMaterialPermissionUsedFalse.Checked = true;

					
				}
				reader.Close();
				conn.Close();

				SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str = "Select Name, ID From UI_MT Where RecodingState = 1 and BusinessRegistrationNum = @BusinessRegistrationNum";
				SqlCommand comm1 = new SqlCommand(str,conn1);
				comm1.Parameters.Add("@BusinessRegistrationNum","");
				
				SqlDataAdapter da = new SqlDataAdapter(comm1) ;
				DataSet ds= new DataSet() ;
				da.Fill(ds);
				
				dl_ClosingPerson.DataSource = ds;
				dl_ClosingPerson.DataTextField = ds.Tables[0].Columns[0].ToString();
				dl_ClosingPerson.DataValueField = ds.Tables[0].Columns[1].ToString();
				dl_ClosingPerson.DataBind();
				dl_ClosingPerson.Items.Insert(0, "-선택-") ;
				dl_ClosingPerson.Items[0].Value = "";



				

				//dl_ClosingYear.SelectedItem.Value = DateTime.Now.Year.ToString();
				//dl_ClosingMonth.SelectedItem.Value = DateTime.Now.Month.ToString();


				//월 마감 세팅
				MonthClose();
			}
		}

		private void MonthClose()
		{
			string Year = "";
			string Month = "";
			string ID="";
			string PerSon = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select Distinct ClosingYear ,ClosingMonth, UpdatingPerson, UpdatingPersonID From MCI_MT where RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Year = dr["ClosingYear"].ToString();
				Month = dr["ClosingMonth"].ToString();
				
				ID = dr["UpdatingPersonID"].ToString();
				PerSon = dr["UpdatingPerson"].ToString();
			}
			dr.Close();
			conn.Close();

			for(int a = 0; a< dl_ClosingYear.Items.Count ;a++)
			{
				if(Year == dl_ClosingYear.Items[a].Value)
				{
					dl_ClosingYear.SelectedIndex = a;
					break;
				}
				
			}


			for(int a = 0; a< dl_ClosingMonth.Items.Count ;a++)
			{
				if(Month == dl_ClosingMonth.Items[a].Value)
				{
					dl_ClosingMonth.SelectedIndex = a;
					break;
				}
			}

			for(int a = 0; a< dl_ClosingPerson.Items.Count ;a++)
			{
				if(ID == dl_ClosingPerson.Items[a].Value)
				{
					dl_ClosingPerson.SelectedIndex = a;
					break;
				}
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
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged_1);
			this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
			this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			string str = @"Update SCS_T set PresentRowMaterialUsed = @PresentRowMaterialUsed,
									OrderNonInStorehouseUsed = @OrderNonInStorehouseUsed,
									OrderGapInStorehouseUsed = @OrderGapInStorehouseUsed,
									MinimumGapUsed = @MinimumGapUsed,
									OrderRequestStandbyUsed = @OrderRequestStandbyUsed,
									MinusRowMaterialPermissionUsed = @MinusRowMaterialPermissionUsed,
									OutOrderUsed = @OutOrderUsed,
									BundlingUsed = @BundlingUsed,
									WorkPlan1 = @WorkPlan1,
									WorkPlan2 = @WorkPlan2,
									WorkPlan3 = @WorkPlan3,
									InWorkConsidering = @InWorkConsidering,
									MinusStockAddUsed = @MinusStockAddUsed";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@PresentRowMaterialUsed",CheckBox1.Checked);
			comm.Parameters.Add("@OrderNonInStorehouseUsed",CheckBox8.Checked);
			comm.Parameters.Add("@OrderGapInStorehouseUsed",CheckBox7.Checked);
			comm.Parameters.Add("@MinimumGapUsed",CheckBox6.Checked);
			comm.Parameters.Add("@OrderRequestStandbyUsed",CheckBox9.Checked);			
			comm.Parameters.Add("@OutOrderUsed",CheckBox2.Checked);
			comm.Parameters.Add("@BundlingUsed",CheckBox4.Checked);
			comm.Parameters.Add("@WorkPlan1",RadioButton1.Checked);
			comm.Parameters.Add("@WorkPlan2",RadioButton2.Checked);
			comm.Parameters.Add("@WorkPlan3",RadioButton3.Checked);
			comm.Parameters.Add("@InWorkConsidering",CheckBox5.Checked);
			comm.Parameters.Add("@MinusStockAddUsed",CheckBox3.Checked);

			if(rdoMinusRowMaterialPermissionUsedTrue.Checked)
				comm.Parameters.Add("@MinusRowMaterialPermissionUsed", SqlDbType.Bit).Value = true;
			else
				comm.Parameters.Add("@MinusRowMaterialPermissionUsed", SqlDbType.Bit).Value = false;
			


			try
			{
				comm.ExecuteNonQuery();
				Response.Write("<script>alert('세팅되었습니다');</script>");
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('"+ee.Message+"');</script>");
			}
			finally
			{
				conn.Close();
			}
		}

		

		private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(CheckBox1.Checked == true)
			{
				RadioButton1.Checked = false;
				RadioButton1.Enabled  = false;
			}
			else
			{
				CheckBox2.Checked = true;
				CheckBox3.Checked = true;
				CheckBox5.Checked = true;

				CheckBox2.Enabled = false;
				CheckBox3.Enabled = false;
				CheckBox5.Enabled = false;

				RadioButton1.Enabled  = true;
			}

		}

		private void CheckBox2_CheckedChanged(object sender, System.EventArgs e)
		{
			if(CheckBox2.Checked == true)
			{
				CheckBox1.Checked = true;
				CheckBox1.Enabled = false;
			}
			else
			{
				CheckBox1.Enabled = true;
			}

			if(CheckBox1.Checked == true)
			{
				RadioButton1.Checked = false;
				RadioButton1.Enabled  = false;
			}
			else
			{
				RadioButton1.Enabled  = true;
			}
		}

		private void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadioButton2.Checked == true)
			{
				CheckBox1.Checked = true;
				CheckBox1.Enabled = false;
				RadioButton1.Enabled = false;
			}
			else
			{
				CheckBox1.Enabled = true;
			}
			if(RadioButton1.Checked == true)
			{
				CheckBox1.Checked = true;
				CheckBox1.Enabled = false;
				CheckBox2.Checked = true;
				CheckBox2.Enabled = false;
				CheckBox3.Checked = true;
				CheckBox3.Enabled = false;
				CheckBox5.Enabled = false;
				CheckBox5.Checked = false;
			}
			else
			{
				CheckBox1.Enabled = true;
				CheckBox2.Enabled = true;
				CheckBox3.Enabled = true;
				CheckBox5.Enabled = true;
			}

		}

		private void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadioButton1.Checked == true)
			{
				CheckBox1.Checked = false;
				CheckBox1.Enabled = false;
				CheckBox2.Checked = false;
				CheckBox2.Enabled = false;
				CheckBox3.Enabled = false;
				CheckBox3.Checked = false;
				CheckBox5.Enabled = false;
				CheckBox5.Checked = false;
			}
			else
			{
				CheckBox1.Enabled = true;
				CheckBox2.Enabled = true;
				CheckBox3.Enabled = true;
				CheckBox5.Enabled = true;
			}
		}

		private void CheckBox2_CheckedChanged_1(object sender, System.EventArgs e)
		{
			if(CheckBox2.Checked == true)
			{
				CheckBox5.Enabled = true;
			}
			else
			{
				CheckBox5.Checked = false;
				CheckBox5.Enabled = false;
			}
		
		}

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
