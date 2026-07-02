using System;
using System.Configuration;
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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// UserInfo에 대한 요약 설명입니다.
	/// </summary>
	public class UserInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label lb_IDChoice;
		protected System.Web.UI.WebControls.Label lb_ID;
		protected System.Web.UI.WebControls.Label lb_Password;
		protected System.Web.UI.WebControls.Label lb_Name;
		protected System.Web.UI.WebControls.Label lb_Post;
		protected System.Web.UI.WebControls.Label lb_IdentificationNumber;
		protected System.Web.UI.WebControls.Label lb_Company;
		protected System.Web.UI.WebControls.Label lb_Telephone1;
		protected System.Web.UI.WebControls.Label lb_Telephone2;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_IDChoice;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox tb_ID;
		protected System.Web.UI.WebControls.Button bt_Post1;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.TextBox tb_Name;
		protected System.Web.UI.WebControls.Image im_Picture;
		protected System.Web.UI.WebControls.TextBox tb_Password;
		protected System.Web.UI.WebControls.TextBox tb_IdentificationNumber;
		protected System.Web.UI.WebControls.DropDownList dl_Post;
		protected System.Web.UI.WebControls.TextBox tb_Responsibility;
		protected System.Web.UI.WebControls.TextBox tb_Telephone1;
		protected System.Web.UI.WebControls.TextBox tb_Telephone2;
		protected System.Web.UI.WebControls.TextBox tb_Address;
		protected System.Web.UI.WebControls.DropDownList dl_HomepageCompetence;
		protected System.Web.UI.WebControls.DropDownList dl_StandardiInfoCompetence;
		protected System.Web.UI.WebControls.DropDownList dl_UserRank;
		protected System.Web.UI.HtmlControls.HtmlInputFile Picture;
		protected System.Web.UI.WebControls.TextBox tb_EMail;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label lb_Picture;
		protected System.Web.UI.WebControls.DropDownList dl_Company;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_User;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit wdt_EnterDate;
		protected System.Web.UI.WebControls.DropDownList ddlWorkDiary;
		private string File_Name="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 사용자정보';</script>");

				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

//				string str = "Select Grade From PA_T order by Grade";
//				SqlCommand comm =  new SqlCommand(str,conn);
//				SqlDataAdapter da = new SqlDataAdapter(comm) ;
//				DataSet ds = new DataSet() ;
//				da.Fill(ds);
//
//
//				dl_UserRank.DataSource = ds;
//				dl_UserRank.DataTextField = ds.Tables[0].Columns[0].ToString();
//				dl_UserRank.DataBind();



			
				page_Load();
			}
			conn.Close();
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
			this.wc_IDChoice.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wc_IDChoice_SelectedRowChanged);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Post1.Click += new System.EventHandler(this.bt_Post1_Click);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.UltraWebGrid1.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.UltraWebGrid1_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			string str = "SELECT ID, Name, UserInfoIndex FROM UI_MT where RecodingState = 1 Order by ID";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			wc_IDChoice.DataSource = ds;
			wc_IDChoice.DataTextField = ds.Tables[0].Columns[0].ToString();
			wc_IDChoice.DataValueField = ds.Tables[0].Columns[2].ToString();
			wc_IDChoice.DataBind();


			//	*************************************************
			//	**  부서 드롭다운리스트... 데이타바인딩...     **  
			//	*************************************************
			//코드분류표에서 대분류명이 부서인 소분류명을 가지고 옴.
			string str_Post = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '부서'" ;
			SqlCommand comm_Post = new SqlCommand(str_Post,conn);
			SqlDataAdapter da_Post = new SqlDataAdapter(comm_Post) ;
			DataSet ds_Post = new DataSet() ;
			da_Post.Fill(ds_Post);
				
			dl_Post.DataSource = ds_Post;
			dl_Post.DataTextField = ds_Post.Tables[0].Columns[0].ToString();
			dl_Post.DataValueField = ds_Post.Tables[0].Columns[1].ToString();
			dl_Post.DataBind();
			dl_Post.Items.Insert(0, "-선택-") ;
			dl_Post.Items[0].Value = "";


			//	*************************************************
			//	**  거래처 드롭다운리스트... 데이타바인딩...   **  
			//	*************************************************
			//코드분류표에서 대분류명이 부서인 소분류명을 가지고 옴.
			string str_Com = "Select CompanyName, BusinessRegistrationNum from CI_MT where RecodingState = 1 and (OutSideOrderCompany = '1' or BuyingCompany = '1')" ;
			SqlCommand comm_Com = new SqlCommand(str_Com,conn);
			SqlDataAdapter da_Com = new SqlDataAdapter(comm_Com) ;
			DataSet ds_Com = new DataSet() ;
			da_Com.Fill(ds_Com);
				
			dl_Company.DataSource = ds_Com;
			dl_Company.DataTextField = ds_Com.Tables[0].Columns[0].ToString();
			dl_Company.DataValueField = ds_Com.Tables[0].Columns[1].ToString();
			dl_Company.DataBind();
			dl_Company.Items.Insert(0, "-선택-") ;
			dl_Company.Items[0].Value = "";


			conn.Close();
			
		}

		/// <summary>
		/// 등록버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{

			if(Name())
			{

				ArrayList arr = new ArrayList();

				arr.Add(tb_ID.Text);
				arr.Add(tb_Password.Text);
				arr.Add(tb_Name.Text);
				arr.Add(dl_Post.SelectedItem.Value);
				arr.Add(tb_Responsibility.Text);
				arr.Add(tb_IdentificationNumber.Text);
				arr.Add(tb_Telephone1.Text);
				arr.Add(tb_Telephone2.Text);
				arr.Add(tb_Address.Text);
				//arr.Add(wdc_EnterDate.Text.Trim());
				if(wdt_EnterDate.Text.Trim() == "")
                    arr.Add(Convert.DBNull.ToString());
				else
					arr.Add(wdt_EnterDate.Text.Trim());
				
				arr.Add(dl_HomepageCompetence.SelectedItem.Value);
				arr.Add(dl_StandardiInfoCompetence.SelectedItem.Value);
				arr.Add(dl_UserRank.SelectedItem.Value);
				if(Picture.Value.ToString() == "")
					File_Name = Convert.DBNull.ToString();
				arr.Add(File_Name.ToString());
				arr.Add(tb_EMail.Text);
				arr.Add(dl_Company.SelectedItem.Value);
				arr.Add(ddlWorkDiary.SelectedItem.Value);
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("UserInfo","Registration","UI_MT",(int)PageName.UserInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				if(File_Name.ToString() != "")
					Picture.PostedFile.SaveAs(Request.PhysicalApplicationPath.ToString() + "\\Images\\Pic\\" + File_Name);

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;

				data_bind();
			}

		}

		private void data_bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			string str = "SELECT ID, Name, UserInfoIndex FROM UI_MT where RecodingState = 1 Order by ID";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			wc_IDChoice.DataSource = ds;
			wc_IDChoice.DataTextField = ds.Tables[0].Columns[0].ToString();
			wc_IDChoice.DataValueField = ds.Tables[0].Columns[2].ToString();
			wc_IDChoice.DataBind();

			conn.Close();
		}


        

		private void wc_IDChoice_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			if(Convert.ToString(wc_IDChoice.DataValue) != "")
			{
			
				lb_Index.Text = Convert.ToString(wc_IDChoice.DataValue);
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str = "Select * From UI_MT Where RecodingState = 1 and UserInfoIndex = @index ";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@index",SqlDbType.Int).Value = lb_Index.Text;

				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				//admin은 삭제불가능 하도록 하기위해 저장
				hd_User.Value = ds.Tables[0].Rows[0]["ID"].ToString();

				tb_ID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
				
				tb_Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();


				for (int i=0 ; i < dl_Post.Items.Count ; i++)
				{
					if( dl_Post.Items[i].Value == ds.Tables[0].Rows[0]["PostCode"].ToString())
					{
						dl_Post.SelectedIndex = i;				
					}
				}
				
						


				tb_Responsibility.Text = ds.Tables[0].Rows[0]["Responsibility"].ToString();
				tb_IdentificationNumber.Text = ds.Tables[0].Rows[0]["IdentificationNumber"].ToString();
				tb_Telephone1.Text = ds.Tables[0].Rows[0]["Telephone1"].ToString();
				tb_Telephone2.Text = ds.Tables[0].Rows[0]["Telephone2"].ToString();
				tb_IdentificationNumber.Text = ds.Tables[0].Rows[0]["IdentificationNumber"].ToString();
				tb_Address.Text = ds.Tables[0].Rows[0]["Address"].ToString();
				//wdc_EnterDate.Value = ds.Tables[0].Rows[0]["EnterDate"].ToString();
				if(ds.Tables[0].Rows[0]["EnterDate"].ToString().Substring(0,4) == "1900")
					wdt_EnterDate.Value = "";
				else
					wdt_EnterDate.Value = ds.Tables[0].Rows[0]["EnterDate"].ToString();
				tb_EMail.Text = ds.Tables[0].Rows[0]["EMail"].ToString();


				string HomepageCompetence = "1";
				string StandardiInfoCompetence = "1";
                
				if(ds.Tables[0].Rows[0]["HomepageCompetence"].ToString() == "False")
					HomepageCompetence = "0";
				if(ds.Tables[0].Rows[0]["StandardiInfoCompetence"].ToString() == "False")
					StandardiInfoCompetence = "0";

				for (int i=0 ; i < dl_HomepageCompetence.Items.Count ; i++)
				{	
					if( dl_HomepageCompetence.Items[i].Value == HomepageCompetence)
					{
						dl_HomepageCompetence.SelectedIndex = i;				
					}

				}	

				for (int i=0 ; i < dl_StandardiInfoCompetence.Items.Count ; i++)
				{
					if( dl_StandardiInfoCompetence.Items[i].Value == StandardiInfoCompetence)
					{
						dl_StandardiInfoCompetence.SelectedIndex = i;				
					}
				}				
				for (int i=0 ; i < dl_UserRank.Items.Count ; i++)
				{
					if( dl_UserRank.Items[i].Value == ds.Tables[0].Rows[0]["UserRank"].ToString())
					{
						dl_UserRank.SelectedIndex = i;				
					}
				}	

				for (int i=0 ; i < dl_Company.Items.Count ; i++)
				{
					if( dl_Company.Items[i].Value == ds.Tables[0].Rows[0]["BusinessRegistrationNum"].ToString())
					{
						dl_Company.SelectedIndex = i;				
					}
				}				
				
				for (int i=0 ; i < ddlWorkDiary.Items.Count ; i++)
				{
					if( ddlWorkDiary.Items[i].Value == ds.Tables[0].Rows[0]["WorkDiary"].ToString())
					{
						ddlWorkDiary.SelectedIndex = i;				
					}
				}


				Pass(ds.Tables[0].Rows[0]["Password"].ToString());


				//이미지를 화면에 보여주는 부분
				if(ds.Tables[0].Rows[0]["Picture"].ToString() != "")
				{
					string pictureName = "../Images/Pic/" + ds.Tables[0].Rows[0]["Picture"].ToString();
					im_Picture.ImageUrl = pictureName.ToString();
				}

				bt_Delete.Enabled = true;
				bt_Update.Enabled = true;
				bt_Registration.Enabled = false;
			}
		}

		private void Pass(string pass)
		{
			string script;
			script  = "<script>";
			script += "		document.all['" + tb_Password.ClientID + "'].value = '"+ pass + "';";
			script += "</script>";
			
			this.RegisterStartupScript("password", script);
		}


		private bool Name()
		{
			
			//파일의 타입을 가져와서 Split 함수로 분리함.
			string[] content_type = Picture.PostedFile.ContentType.Split('/');

			//파일의 확장자명을 가져옴
			string[] file_type = System.IO.Path.GetFileName(Picture.PostedFile.FileName).Split('.');

			if(Picture.Value.ToString() != "")
			{


				//가져온 파일의 타입이 이미지파일일때 실행
				if(content_type[0].ToString() == "image")
				{
					//업로드될 파일의 이름을 ID로 주고 확장자명은 파일의 확장자명 그대로 줌.
					//이렇게 함으로서 파일의 중복을 막을 수 있음.
					File_Name = tb_ID.Text.ToString() + "." + file_type[1];
				
					//이미지 보기
					im_Picture.ImageUrl = Picture.Value.ToString();
					return true;
				}
				else
				{
					RegisterStartupScript("","<script>alert('등록할 사진이 이미지 파일이 아닙니다.');</script>");
					return false;
				}
			}
			else
			{
				File_Name = "";
				return true;
			}

		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(Name())
			{
				if(hd_User.Value != tb_ID.Text)
				{
					RegisterStartupScript("","<script>alert('관리자 아이디는 수정이 불가능 합니다!');</script>");
				}
				else
				{
					ArrayList arr = new ArrayList();

					arr.Add(tb_ID.Text);
					arr.Add(tb_Password.Text);
					arr.Add(tb_Name.Text);
					arr.Add(dl_Post.SelectedItem.Value);
					arr.Add(tb_Responsibility.Text);
					arr.Add(tb_IdentificationNumber.Text);
					arr.Add(tb_Telephone1.Text);
					arr.Add(tb_Telephone2.Text);
					arr.Add(tb_Address.Text);
					//arr.Add(wdc_EnterDate.Text.Trim());
					if(wdt_EnterDate.Text.Trim() == "")
						arr.Add(Convert.DBNull.ToString());
					else
						arr.Add(wdt_EnterDate.Text.Trim());
				
					arr.Add(dl_HomepageCompetence.SelectedItem.Value);
					arr.Add(dl_StandardiInfoCompetence.SelectedItem.Value);
					arr.Add(dl_UserRank.SelectedItem.Value);
					if(Picture.Value.ToString() == "")
						File_Name = Convert.DBNull.ToString();
					arr.Add(File_Name.ToString());
					arr.Add(tb_EMail.Text);
					arr.Add(dl_Company.SelectedItem.Value);
					arr.Add(ddlWorkDiary.SelectedItem.Value);
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add("");
					arr.Add(lb_Index.Text);

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD("UserInfo","Update","UI_MT",(int)PageName.UserInfo,Session["ID"].ToString(),arr,"낱개입력");
					rud.ActivateRUD();

					bt_Delete.Enabled = false;
					bt_Update.Enabled = false;
					bt_Registration.Enabled = true;
				}

				if(File_Name.ToString() != "")
					Picture.PostedFile.SaveAs(Request.PhysicalApplicationPath.ToString() + "\\Images\\Pic\\" + File_Name);

				data_bind();
			}
		}

		private void bt_ID_Click(object sender, System.EventArgs e)
		{
			string pass = tb_Password.Text;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select Count(*) From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = tb_ID.Text;
			int exist = int.Parse(comm.ExecuteScalar().ToString());

			if(exist == 0)
			{
				RegisterStartupScript("","<script>alert('사용할수 있는 ID입니다');</script>");
			}
			else
			{
				RegisterStartupScript("","<script>alert('동일한 ID가 있습니다');</script>");
			}

			Pass(pass);



		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(hd_User.Value == "admin")
			{
				RegisterStartupScript("","<script>alert('관리자는 삭제가 불가능 합니다!');</script>");
			}
			else
			{
				ArrayList arr = new ArrayList();

				arr.Add(tb_ID.Text);
				arr.Add(tb_Password.Text);
				arr.Add(tb_Name.Text);
				arr.Add(dl_Post.SelectedItem.Value);
				arr.Add(tb_Responsibility.Text);
				arr.Add(tb_IdentificationNumber.Text);
				arr.Add(tb_Telephone1.Text);
				arr.Add(tb_Telephone2.Text);
				arr.Add(tb_Address.Text);
				//arr.Add(wdc_EnterDate.Text.Trim());
				if(wdt_EnterDate.Text.Trim() == "")
					arr.Add(Convert.DBNull.ToString());
				else
					arr.Add(wdt_EnterDate.Text.Trim());
				
				arr.Add(dl_HomepageCompetence.SelectedItem.Value);
				arr.Add(dl_StandardiInfoCompetence.SelectedItem.Value);
				arr.Add(dl_UserRank.SelectedItem.Value);
				if(Picture.Value.ToString() == "")
					File_Name = Convert.DBNull.ToString();
				arr.Add(File_Name.ToString());
				arr.Add(tb_EMail.Text);
				arr.Add(dl_Company.SelectedItem.Value);
				arr.Add(ddlWorkDiary.SelectedItem.Value);
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add("");
				arr.Add(lb_Index.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("UserInfo","Delete","UI_MT",(int)PageName.UserInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				Clear();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
			}

			data_bind();
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			Clear();

			bt_Delete.Enabled = false;
			bt_Update.Enabled = false;
			bt_Registration.Enabled = true;

		}

		private void Clear()
		{
			tb_ID.Text = "";
			tb_Password.Text = "";
			tb_Name.Text = "";
			dl_Post.SelectedIndex = 0;
			tb_Responsibility.Text = "";
			tb_IdentificationNumber.Text = "";
			tb_Telephone1.Text = "";
			tb_Telephone2.Text ="";
			tb_Address.Text="";
			//wdc_EnterDate.Value = "";
			wdt_EnterDate.Text= "";
				

			dl_HomepageCompetence.SelectedIndex = 0;
			dl_StandardiInfoCompetence.SelectedIndex = 0;
			dl_UserRank.SelectedIndex = 0;
			//Picture.Value = "";
			im_Picture.ImageUrl = "";
			tb_EMail.Text = "";
			dl_Company.SelectedIndex = 0;
			lb_Index.Text = "0";
			lb_Picture.Text = "";
			wc_IDChoice.DataValue = "";
			ddlWorkDiary.SelectedIndex = 0;
		}

       
		/// <summary>
		/// 주소찾기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Post1_Click(object sender, System.EventArgs e)
		{
			string text = "tb_Address";
			
			RegisterStartupScript("","<script>window.open('./Popup/PostSearch.aspx?formID=UserInfo&text="+text+"','PostSearch'," + "'width=300" + "," + "height=300px');</script>");
		}

		/// <summary>
		/// 참고보기 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Text == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "사용자정보";
				int m_RecodeIdx = int.Parse(lb_Index.Text);

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				UltraWebGrid1.DataSource = rf.reference();
				UltraWebGrid1.DataBind();
			}		
		}

		private void UltraWebGrid1_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/User.aspx','User'," + "'width=830" + "," + "height=500px');</script>");
		}
	}
}
