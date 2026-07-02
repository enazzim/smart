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
using System.Configuration;

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WorkPlanAdd에 대한 요약 설명입니다.
	/// </summary>
	public class WorkPlanAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList ddlProcessName;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit wneWorkQuantity;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit wneLeadTime;
		protected System.Web.UI.WebControls.DropDownList ddlWCName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcWorkDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.HtmlControls.HtmlInputText txtItemState;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btAdd;
		protected KIT_ERP.ProductionManagement.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		


		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품
			ItemSearchControl1.IsDoPost = true;
			
			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
				string str = "Select WCName, WCInfoIndex From WCI_MT Where RecodingState = 1 order by WCName";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				ddlWCName.DataSource = ds;
				ddlWCName.DataTextField = ds.Tables[0].Columns[0].ToString();
				ddlWCName.DataValueField = ds.Tables[0].Columns[1].ToString();
				ddlWCName.DataBind();
				ddlWCName.Items.Insert(0, "-선택-") ;
				ddlWCName.Items[0].Value = "";

				wdcWorkDate.NullDateLabel = DateTime.Today.ToShortDateString();
				wdcDeliveryDate.NullDateLabel = DateTime.Today.ToShortDateString();
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
			this.Dropdownlist2.SelectedIndexChanged += new System.EventHandler(this.Dropdownlist2_SelectedIndexChanged);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.LinkButton2.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			//string str = @"select SmallClassificationName, PSI_MT.ProcessCode From PSI_MT inner join PUC_MT on PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode where PSI_MT.RecodingState = 1 and ItemNum = @num order by SmallClassificationName  asc";
			string str = @"select PUC_MT.SmallClassificationName, PSI_MT.ProcessCode, PUC_MT_1.SmallClassificationName as ItemState
						From PSI_MT inner join PUC_MT on PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode 
						inner join II_MT on PSI_MT.ItemNum = II_MT.ItemNum
						left join PUC_MT PUC_MT_1 on II_MT.ItemState = PUC_MT_1.SmallClassificationCode 
						where PSI_MT.RecodingState = 1 and II_MT.RecodingState =1 and PUC_MT.RecodingState = 1 and PUC_MT_1.RecodingState = 1 and PSI_MT.ItemNum = @num order by PSI_MT.ProcessSequenceNum  asc";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",ItemSearchControl1.ItemNum.ToString());
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			
			this.ddlProcessName.DataSource = ds; 
			this.ddlProcessName.DataTextField =  ds.Tables[0].Columns[0].ToString();
			this.ddlProcessName.DataValueField =  ds.Tables[0].Columns[1].ToString();
			this.ddlProcessName.DataBind();
			this.ddlProcessName.Items.Insert(0,"-- 선택--");
			this.ddlProcessName.Items[0].Value = "";
			txtItemState.Value = ds.Tables[0].Rows[0]["ItemState"].ToString();
			conn.Close();
			
		}

		/// <summary>
		/// 추가버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{
			if (Validate(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName))
			{
				if(ddlProcessName.SelectedItem.Value == null || ddlProcessName.SelectedItem.Value == "")
				{
					RegisterClientScriptBlock("","<script>alert('공정을 선택하세요!');</script>");
				}
				else if((ddlWCName.SelectedItem.Value == null || ddlWCName.SelectedItem.Value == "")&&(Dropdownlist2.SelectedItem.Value != "외주"))
				{
					RegisterClientScriptBlock("","<script>alert('작업장을 선택하세요!');</script>");
				}
				else if(wneWorkQuantity.Text == "0")
				{
					RegisterClientScriptBlock("","<script>alert('작업량을 입력하세요!');</script>");
				}
				else if(Dropdownlist2.SelectedItem.Value == "외주" && wneLeadTime.Text == "0")
				{
					RegisterClientScriptBlock("","<script>alert('리드타임을 입력하세요!');</script>");
				}
				else if(wdcWorkDate.Text.Trim() == "")
				{
					RegisterClientScriptBlock("","<script>alert('작업일을 입력하세요!');</script>");
				}
				else if(wdcDeliveryDate.Text.Trim() == "")
				{
					RegisterClientScriptBlock("","<script>alert('납기일을 입력하세요!');</script>");
				}
				else
				{
					//공정순서 입력
					int SequenceNum = FindSequence(ItemSearchControl1.ItemNum,ddlProcessName.SelectedItem.Value);

					if(SequenceNum != 0)
					{
                			
						SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
						conn.Open();
						SqlTransaction tr = conn.BeginTransaction();						

						
						string str = @"Insert into WDWP_HT (ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, PreProcessName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, DeliveryDate, WorkCompleteDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate) 
							Values(@ItemNum, @ItemDrawNum, @ItemName, @ProcessSequenceNum, @ProcessCode, @ProcessName, @PreProcessName, @WCName, @WorkDistinction, @WorkPlanQuantity, '0', @OrderLeadTime, @WorkDate, @DeliveryDate, @WorkCompleteDate, '대기', @RegistrationPerson, @RegistrationPersonID, @RegistrationDate)";
						SqlCommand comm = new SqlCommand(str,conn);
						comm.Transaction = tr;
						comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.ToString());
						comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.ToString());
						comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.ToString());
						comm.Parameters.Add("@ProcessSequenceNum",SequenceNum);
						comm.Parameters.Add("@ProcessCode",ddlProcessName.SelectedItem.Value);
						comm.Parameters.Add("@ProcessName",ddlProcessName.SelectedItem.Text);

						comm.Parameters.Add("@PreProcessName",StoreManage(ItemSearchControl1.ItemNum.ToString()));


						if(ddlWCName.SelectedIndex == 0)
							comm.Parameters.Add("@WCName",Convert.DBNull);
						else
							comm.Parameters.Add("@WCName",ddlWCName.SelectedItem.Text);

						comm.Parameters.Add("@WorkDistinction",Dropdownlist2.SelectedItem.Value);
						comm.Parameters.Add("@WorkPlanQuantity",decimal.Parse(wneWorkQuantity.Text));
						comm.Parameters.Add("@OrderLeadTime",int.Parse(wneLeadTime.Text));
						comm.Parameters.Add("@WorkDate",DateTime.Parse(wdcWorkDate.Text).ToShortDateString());
						comm.Parameters.Add("@DeliveryDate",DateTime.Parse(wdcDeliveryDate.Text).ToShortDateString());
						comm.Parameters.Add("@WorkCompleteDate",DateTime.Parse(wdcDeliveryDate.Text).ToShortDateString());
						comm.Parameters.Add("@RegistrationPerson",Session["UserName"].ToString());
						comm.Parameters.Add("@RegistrationPersonID",Session["ID"].ToString());
						comm.Parameters.Add("@RegistrationDate",DateTime.Now.ToShortDateString());

						
						try
						{
							if(Dropdownlist2.SelectedItem.Value == "자가")
							{
								//작업표준에 해당공정이 존재해야함.
								if(!WSIExist(ItemSearchControl1.ItemNum.ToString(),SequenceNum))
								{
									throw new Exception("해당 공정이 작업표준에 존재하지 않습니다!");
								}
							}
							else
							{
								//외주는 외주단가를 볼 수 없다. 해당 외주가 반드시 외주단가에
								//존재하는지 알수 없기때문이다.
							}

							comm.ExecuteNonQuery();
							tr.Commit();

							RegisterClientScriptBlock("","<script>alert('추가했습니다!');</script>");
						}
						catch(Exception ee)
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert('"+ee.Message+"');");
							Response.Write("</script>");
							tr.Rollback();
						}
						finally
						{
							conn.Close();
						}

						


					}
					else
					{
						RegisterClientScriptBlock("","<script>alert('해당 공정이 없습니다!');</script>");
					}
				}
			}

		}



		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private string StoreManage(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);			
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();				
			}
			dr.Close();	
			comm.Parameters.Clear();
			conn.Close();

			return store;
		}

		private bool WSIExist(string item, int seq)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			string str = "Select Count(*) From WSI_MT where ItemNum = @Item and ProcessSequenceNum = @SequenceNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@Item",item);
			comm.Parameters.Add("@SequenceNum",seq);
			int count = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			if(count == 0)
				return false;
			else
				return true;
			
		}

		private bool Validate(string item, string draw, string name)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;
			int a = 0;

			try
			{
				string str = @"Select ItemNum From II_MT Where RecodingState = 1 and ItemNum = @num";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemDrawNum From II_MT Where RecodingState = 1 and ItemDrawNum = @draw";
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				if(ds1.Tables[0].Rows.Count == 0 )
					throw new Exception("도면번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemName = @name";
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
				comm.CommandText = str;
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);

				if(ds2.Tables[0].Rows.Count == 0 )
					throw new Exception("품목명을 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemNum = @num and ItemDrawNum = @draw and ItemName = @name";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;

				comm.CommandText = str;
				SqlDataAdapter da3 = new SqlDataAdapter(comm);
				DataSet ds3 = new DataSet();
				da3.Fill(ds3);

				if(ds3.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호, 도면번호, 품목명을 다시 확인하세요.");

				tr.Commit();
			}
			catch(Exception ee)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
				tr.Rollback();
				a = 1;
			}
			finally
			{
				conn.Close();
			}
			if(a == 0)
				return true;
			else
				return false;
		}

		private void Dropdownlist2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(Dropdownlist2.SelectedItem.Value == "자가")
			{
				wneLeadTime.Value = "0";
				wneLeadTime.Enabled = false;
			}
			else
				wneLeadTime.Enabled = true;
		}

		/// <summary>
		/// 공정순서 찾기
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Process"></param>
		/// <returns></returns>
		private int FindSequence(string ItemNum, string Process)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			int num= 0;
			string str = "Select ProcessSequenceNum From PSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",Process);

			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				num = int.Parse(dr["ProcessSequenceNum"].ToString());
			conn.Close();

			return num;
		}

		/// <summary>
		/// 초기화버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button2_Click(object sender, System.EventArgs e)
		{

			ddlProcessName.SelectedIndex = -1;
			ddlWCName.SelectedIndex = -1;
			wdcDeliveryDate.Value = "";
			wdcWorkDate.Value = "";
			Dropdownlist2.SelectedIndex = -1;
			wneWorkQuantity.Text = "0";
			wneLeadTime.Text = "0";
			wneLeadTime.Enabled = false;
			txtItemState.Value = "";

			ItemSearchControl1.ClearTextBox();
		}

		
	}
}
