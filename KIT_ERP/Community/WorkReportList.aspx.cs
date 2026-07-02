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

namespace KIT_ERP.Community
{
	/// <summary>
	/// WorkReportList에 대한 요약 설명입니다.
	/// </summary>
	public class WorkReportList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label lblPageInfo;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		private int iPage = 0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.Cookies["WorkDiary"]["UpdateHits"] = "0";
			

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{

				if(WorkDiary(Session["ID"].ToString().ToLower()))
					Button1.Visible = true;
				else
					Button1.Visible = false;
				
				if(Session["ID"].ToString().Trim().ToLower() == "77sindong")
				{
					DropDownList1.Visible = true;
					DropDownList2.Visible = true;
					Label1.Visible = true;
					Label2.Visible = true;
					Button2.Visible = true;
					HyperLink1.Visible = true;
					HyperLink1.NavigateUrl = "https://docs.google.com/spreadsheets/d/1MOzmTKpHtrjBSexK_Iehlq8oNrJCJ3SDk0pnNFGDaNs/edit#gid=0";
				}
				else
				{
					DropDownList1.Visible = false;
					DropDownList2.Visible = false;
					Label1.Visible = false;
					Label2.Visible = false;
					Button2.Visible = false;
					HyperLink1.Visible = false;
				}

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				string str = "Select Distinct Name, [ID] from UI_MT where RecodingState = 1 and  WorkDiary != '00 그룹' and [ID] != '77sindong' order by Name" ;
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);
				
				DropDownList1.DataSource = ds;
				DropDownList1.DataTextField = ds.Tables[0].Columns[0].ToString();
				DropDownList1.DataValueField = ds.Tables[0].Columns[1].ToString();
				DropDownList1.DataBind();
				DropDownList1.Items.Insert(0, "- 전  체 -") ;
				DropDownList1.Items[0].Value = "";

				DataGrid1.PageSize = 15;
				DataGrid1.VirtualItemCount = GetTotalRecordCount();

				Listing();				
			}
			else
			{
				// CTD_T라는 쿠키값이 존재하면 pageNumber값을 iPage변수에 저장하고
				if(Request.Cookies["WorkDiary"] != null)
				{
					string pageNumber = Request.Cookies["WorkDiary"]["PageNumber"];				
					if(pageNumber != null)
						iPage = int.Parse(pageNumber);
					else
						iPage = 0;
					DataGrid1.CurrentPageIndex = iPage;
				}
			}
		}

		private void Listing()
		{
			
			DataGrid1.DataSource = Search(Session["ID"].ToString().ToLower());
			DataGrid1.DataBind();
			lblPageInfo.Text =  string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
			
			
		}


		// CTD_T 테이블의 총 레코드 수 구하기
		private int RecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			string strSql = "";
			if(Session["ID"].ToString() == "77sindong")
			{
				

				if(DropDownList1.SelectedIndex == 0)
				{
					if(DropDownList2.SelectedIndex == 0)
					{
						strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID is not null";						
					}
					else
					{
						strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID is not null and Approval = @Approval";
						
					}
				}
				else
				{
					if(DropDownList2.SelectedIndex == 0)
					{
						strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID = @RegistrationID ";	
					}
					else
					{
						strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID = @RegistrationID and Approval = @Approval";
					}
				}
			}
			else
			{
				strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID = @RegistrationID";
			}
			SqlCommand Cmd = new SqlCommand(strSql, Con);
			Cmd.Parameters.Add("@RegistrationID", DropDownList1.SelectedItem.Value);
			Cmd.Parameters.Add("@Approval",DropDownList2.SelectedItem.Value);
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
		}

		// CTD_T 테이블의 총 레코드 수 구하기
		private int GetTotalRecordCount()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			string strSql = "";
			if(Session["ID"].ToString() == "77sindong")
				strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID is not null";
			else
				strSql = "SELECT Count(*) FROM WorkReport Where RegistrationID = @id";
			SqlCommand Cmd = new SqlCommand(strSql, Con);
			Cmd.Parameters.Add("@id",Session["ID"].ToString().Trim().ToLower());
			Con.Open();
			int totalCount = (int)Cmd.ExecuteScalar();
			Con.Close();

			return totalCount;
		}


		private bool WorkDiary(string id)
		{
			string work = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "Select WorkDiary From UI_MT where RecodingState = 1 and [ID] = @id";
			Cmd.Parameters.Add("@id",id);
			Con.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				work = dr["WorkDiary"].ToString().Trim();
			}
			dr.Close();
			Con.Close();
			Cmd.Parameters.Clear();

			if(work == "00 그룹")
				return false;
			else
				return true;

		}

		private string ReplaceBR(string s)
		{
			string a =  s.Replace("\n", "<BR>");
			return a.Replace(" ", "&nbsp;");
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
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged_1);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			
			if(Exist())
			{
				
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;

				Cmd.CommandText = @"InSert into WorkReport (ReportDate, approval, RegistrationPerson, RegistrationID)
										values (@ReportDate, @approval, @RegistrationPerson, @RegistrationID)";
				Cmd.Parameters.Add("@ReportDate",DateTime.Now.ToShortDateString());
				Cmd.Parameters.Add("@approval", "미결");
				Cmd.Parameters.Add("@RegistrationPerson", Session["UserName"].ToString().Trim().ToLower());
				Cmd.Parameters.Add("@RegistrationID", Session["ID"].ToString().Trim().ToLower());

				Con.Open();
				Cmd.ExecuteNonQuery();
				Cmd.Parameters.Clear();

				Cmd.CommandText = "Select Max([Index]) From WorkReport";
				int Index   = int.Parse(Cmd.ExecuteScalar().ToString());
				


				string IDAuthority=IDFind(Session["ID"].ToString().Trim().ToLower());

				string str = "";
				string str1="";
				string str2="";
				string str3="";

				switch(IDAuthority)
				{
					case "01 그룹":
						str1 = @" 1. 출장내용

1) 출발시각 :            귀사시각 :              출장사유:

2) 납품시 지적사항

3) 수정 및 재생내용

4) 현 생산기종

5) 일일생산량

6) 향후 생산계획 확인사항";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str1);
						Cmd.Parameters.Add("@02","2. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@03","");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@07","3. 외근사항\n");	
						Cmd.Parameters.Add("@06","4. 지시사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");

						
						break;
					case "02 그룹":

						str1 = @"1. 업무현황

 총원	명,
 결근	명,
 지각	명,
 조퇴	명, 현재원	명
 신규 입사자 :(   )   주 업무 :
 특근(  )	명
 특근 주작업내용 :    ";
						str2 = @"2. 업무현황

	- 금주출하예상(수출)  :
	- 납기독촉사항\n";

						str3= @"4. 공장최종점검내역
   1) 현장점검현황
	-사무실,현장 내외부 전등 전열기구 차단상태


	-출입문,정문 창문 시건장치 상태


   2) 3정5S 점검현황
	-청소 정리정돈이필요한위치:


";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[06])
										values (@index, @01, @02, @03, @06)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 업무현황");
						Cmd.Parameters.Add("@02","2. 실린더 현황");
						Cmd.Parameters.Add("@03","3. 기타 보고사항 및 건의 사항\n");
						//Cmd.Parameters.Add("@04",str3);
						//Cmd.Parameters.Add("@05","5. 조치사항\n");
						Cmd.Parameters.Add("@06","4. 지시사항\n");
						//Cmd.Parameters.Add("@07","6. 외근사항\n");	
						//Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "03 그룹":
						str1 = @"1. 인원 현황

 총원	명,
 결근	명,
 지각	명,
 조퇴	명, 현재원	명
 신규 입사자 :(   )   주 업무 :
 특근(  )	명
 특근 주작업내용 :    ";
						str2 = @"2. 업무현황
	- 납기독촉사항                      ";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str1);
						Cmd.Parameters.Add("@02", str2);
						Cmd.Parameters.Add("@03", "3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06", "5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "04 그룹":
						str1 = @"1. 매출현황

-----------------------------------------------------
거래처        일 일          월 계           누 계
-----------------------------------------------------
대동공업
수    출
기    타
합    계
";
						str2 = @"2. 업무현황

       - 납기독촉사항";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str1);
						Cmd.Parameters.Add("@02",str2);
						Cmd.Parameters.Add("@03","3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");	
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "05 그룹":
						str1 = @"1. 공문수발 현황

    - 접수 현황
	
	
    - 발송 현황



* 용역인원 체크 현황";
						str2 = @"2. 대동 EDI 공문내용

";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str1);
						Cmd.Parameters.Add("@02", str2);
						Cmd.Parameters.Add("@03", "3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06", "5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "06 그룹":
						str1= @"6. 공장최종점검내역
   1) 현장점검현황
	-사무실,현장 내외부 전등 전열기구 차단상태


	-출입문,정문 창문 시건장치 상태


   2) 3정5S 점검현황
	-청소 정리정돈이필요한위치:


";
						str2 = @"5. 주요현안 사항
    1공장인원현황;총원    명                        2공장인원현황;총원     명
                 결근     명                                      결근     명
                 지각     명                                      지각     명
                 조퇴     명                                      조퇴     명
                 신입사원 주요업무                                신입사원 주요업무

                 특근     명                                      특근     명

";


						
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[08],[09],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@08,@09,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 출장자 현황\n");
						Cmd.Parameters.Add("@02","2. 거래처 협의사항 및 방문자\n");
						Cmd.Parameters.Add("@03","3. 설비이상 사항\n");
						Cmd.Parameters.Add("@04","4. 개발 관련 사항,  납품독촉사항\n");
						//Cmd.Parameters.Add("@05","5. 주요현안 사항\n");
						Cmd.Parameters.Add("@05", str2);
						Cmd.Parameters.Add("@06","9. 지시사항\n");
						Cmd.Parameters.Add("@07","8. 외근사항\n");
						Cmd.Parameters.Add("@08",str1);
						Cmd.Parameters.Add("@09","7. 조치사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						
						break;
					case "07 그룹":
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 고객으로부터 품질문제를 통보 받은 사항(유,무선등 제반 연락사항)\n");
						//Cmd.Parameters.Add("@02","2. 고객에 납품 한 후 품질문제로 반송 된 사항\n");
						Cmd.Parameters.Add("@02","2. 개발 업무 현황\n");
						Cmd.Parameters.Add("@03","3. 외주품 입고검사시 품질문제로 불합격 또는 반송 조치한 사항\n");
						Cmd.Parameters.Add("@04","4. 사내 양산 및 개발품 제작시 일일 불량 Check List\n");
						Cmd.Parameters.Add("@05","5. 기타 보고사항 및 건의 사항 \n");
						Cmd.Parameters.Add("@06","7. 지시사항\n");
						Cmd.Parameters.Add("@07","6. 외근사항\n");	
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "08 그룹":
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 자재관리 관련 사항(구매/외주)\n");
						Cmd.Parameters.Add("@02","2. 생산관리 관련 사항\n");
						Cmd.Parameters.Add("@03","3. 전산 관련 사항\n");
						Cmd.Parameters.Add("@04","4. 기타 현안 사항\n");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","6. 지시사항\n");
						Cmd.Parameters.Add("@07","5. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "09 그룹":
						str1 = @"2. 외국인숙소점검 사항
1)전등,전열기구차단상태


2)출입문시건상태

";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 주요현안사항\n");
						Cmd.Parameters.Add("@02",str1);
						Cmd.Parameters.Add("@03","");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","4. 지시사항\n");
						Cmd.Parameters.Add("@07","3. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "10 그룹":
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 개발업무 관련사항\n");
						Cmd.Parameters.Add("@02","2. 개정업무 관련사항\n");
						Cmd.Parameters.Add("@03","3. 기타 보고사항 및 건의사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "11 그룹":
						str1 = @"2. 부적합 내용

----------------------------------------------------------------------------------------------------------------
품 번                    품 명               부적합수량                                           원 인                              조치사항
----------------------------------------------------------------------------------------------------------------
";

						str2=@"4. 공장최종점검내역
   1) 현장점검현황
	-사무실,현장 내외부 전등 전열기구 차단상태


	-출입문,정문 창문 시건장치 상태


   2) 3정5S 점검현황
	-청소 정리정돈이필요한위치:


";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 주요 작업 내용\n");
						Cmd.Parameters.Add("@02",str1);
						Cmd.Parameters.Add("@03","3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04",str2);
						Cmd.Parameters.Add("@05","5. 조치사항\n");
						Cmd.Parameters.Add("@06","7. 지시사항\n");
						Cmd.Parameters.Add("@07","6. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "12 그룹":
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 수출관련 업무\n");
						Cmd.Parameters.Add("@02","2. 통합메일 수신\n");
						Cmd.Parameters.Add("@03", "3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06", "5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");	
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "13 그룹":
						str1 = @"2. 부적합 내용

----------------------------------------------------------------------------------------------------------------
품 번                    품 명               부적합수량                                           원 인                              조치사항
----------------------------------------------------------------------------------------------------------------
";
						str2 = @"2. 차량 운행 현황

----------------------------------------------------------------------------------------------------------------
차량번호           출발시각                도착시각                         운행사유                           목적지                  경유지
----------------------------------------------------------------------------------------------------------------
";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 주요 작업 내용\n");
						Cmd.Parameters.Add("@02",str1);
						Cmd.Parameters.Add("@03",str2);
						Cmd.Parameters.Add("@04","4. 기타보고 및 건사항\n");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","6. 지시사항\n");
						Cmd.Parameters.Add("@07","5. 외근사항\n");	
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;
					case "14 그룹":
						str1 = @" 1. 레이져 작업 내역

1) 주간작업 : 







2) 야간작업 : ";
						str2 = @"2. 레이저불량내역

-------------------------------------------------------------------------------------------------------------
      구분                                          수량                              상태
-------------------------------------------------------------------------------------------------------------
  절단면
  홀
  레이저불량(재생)
  레이저불량(폐기)

";
						str3 = @"3. 레이져 절단기 체크LIST
  1) 아마다 절단기
       ① 빔 자바라 상태 점검 :
       ② 현재 출력 상태(kW) :
       ③ 이상소음 현상 :
       ④ 가공시간(일일/전체누적) :
  2) 바이스트로닉 절단기
       ① 빔 자바라 상태 점검 :
       ② 현재 출력 상태(kW) :
       ③ 이상소음 현상 :
       ④ 가공시간(일일/전체누적) :
  3) 플라즈마 절단기
       ① 집진기 가동 상태 :
       ② 빔 출력 상태 :
       ③ 가스/에어배관/냉각수 상태 :
       ④ 가공시간(일일/전체누적) :  
 
  ※ 기타 보고사항 



";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str1);
						Cmd.Parameters.Add("@02",str2);
						Cmd.Parameters.Add("@03",str3);
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06","5. 지시사항\n");
						Cmd.Parameters.Add("@07","4. 외근사항\n");	
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;						
					case "15 그룹":
						str2 = @"2. 일일 체크LIST

    1) 전기
       ① 1공장 분전반 차단기 파손 및 트립 유무:
       ② 2공장 분전반 차단기 파손 및 트립 유무:

    2) 콤프레셔
       ① 1공장(50hp) 이상소음 유무(가동시간):
       ② 2공장(75hp) 이상소음 유무(가동시간):
       ③ 2공장(50hp) 이상소음 유무(가동시간):

    3) 도장라인
       ① 파손 및 청소 상태:
       ③ 컨베어체인 오일 자동주유 여부:

    4) 쇼트기 파손 및 청소 상태:

    5) 실린더 세척기 파손, 누유 및 청소 상태:

    6) 호이스트 파손, 단락 및 작동 여부(1공장/2공장):




";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01","1. 생산관리 관련 현황\n");
						Cmd.Parameters.Add("@02",str2);
						Cmd.Parameters.Add("@03", "3. 기타 보고사항\n");
						Cmd.Parameters.Add("@04","");
						Cmd.Parameters.Add("@05","");
						Cmd.Parameters.Add("@06", "4. 지시사항\n");
						Cmd.Parameters.Add("@07","");	
						Cmd.Parameters.Add("@10","");	
						break;
					case "16 그룹":
						str = @"1. 주요 작업 내용
    o 3RD Function 밸브 검수(납품업체 TEST 유무 확인) : ";

						str1 = @"2. 부적합 내용

----------------------------------------------------------------------------------------------------------------
품 번                    품 명               부적합수량                                           원 인                              조치사항
----------------------------------------------------------------------------------------------------------------
";

						str2=@"4. 공장최종점검내역
   1) 현장점검현황
	-사무실,현장 내외부 전등 전열기구 차단상태


	-출입문,정문 창문 시건장치 상태


   2) 3정5S 점검현황
	-청소 정리정돈이필요한위치:


";
						Cmd.CommandText = @"InSert into WorkReportID ([Index],[01], [02],[03],[04],[05], [06],[07],[10])
										values (@index, @01, @02, @03, @04,@05, @06,@07,@10)";
						Cmd.Parameters.Add("@index",Index);
						Cmd.Parameters.Add("@01",str);
						Cmd.Parameters.Add("@02",str1);
						Cmd.Parameters.Add("@03","3. 기타 보고사항 및 건의 사항\n");
						Cmd.Parameters.Add("@04",str2);
						Cmd.Parameters.Add("@05","5. 조치사항\n");
						Cmd.Parameters.Add("@06","7. 지시사항\n");
						Cmd.Parameters.Add("@07","6. 외근사항\n");
						Cmd.Parameters.Add("@10","* 로더 공정별 확인\n");	
						break;


				}
				
				Cmd.ExecuteNonQuery();
				Cmd.Parameters.Clear();


				if(IDAuthority == "01 그룹")
			        Response.Redirect("WorkDiaryWrite01.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "02 그룹")
			        Response.Redirect("WorkDiaryWrite02.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "03 그룹")
			        Response.Redirect("WorkDiaryWrite03.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "04 그룹")
			        Response.Redirect("WorkDiaryWrite04.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "05 그룹")
			        Response.Redirect("WorkDiaryWrite05.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "06 그룹")
			        Response.Redirect("WorkDiaryWrite06.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "07 그룹")
			        Response.Redirect("WorkDiaryWrite07.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "08 그룹")
			        Response.Redirect("WorkDiaryWrite08.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "09 그룹")
			        Response.Redirect("WorkDiaryWrite09.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "10 그룹")
					Response.Redirect("WorkDiaryWrite10.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "11 그룹")
					Response.Redirect("WorkDiaryWrite11.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "12 그룹")
					Response.Redirect("WorkDiaryWrite12.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "13 그룹")
					Response.Redirect("WorkDiaryWrite13.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "14 그룹")
					Response.Redirect("WorkDiaryWrite14.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "15 그룹")
					Response.Redirect("WorkDiaryWrite15.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
				else if(IDAuthority == "16 그룹")
					Response.Redirect("WorkDiaryWrite16.aspx?Num="+Index.ToString().Trim()+"&id="+Session["ID"].ToString().Trim().ToLower()+"");
			}
			else
			{
				Response.Write("<script>alert('이미 등록되어 있습니다!')</script>");
			}
		}


		private string IDFind(string id)
		{
			string WorkDiary = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "Select WorkDiary From UI_MT where RecodingState = 1 and [ID] = @id";
			Cmd.Parameters.Add("@id",id);
			Con.Open();
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				WorkDiary = dr["WorkDiary"].ToString();
			}
			Con.Close();
			
			return WorkDiary;
		}
		private bool Exist()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "Select count(ReportDate) From WorkReport where ReportDate = @ReportDate and RegistrationID = @id";
			Cmd.Parameters.Add("@ReportDate",DateTime.Now.ToShortDateString());
			Cmd.Parameters.Add("@id",Session["ID"].ToString().Trim().ToLower());
			Con.Open();
			int count = int.Parse(Cmd.ExecuteScalar().ToString());
			Con.Close();
			if(count == 1)
				return false;
			else
				return true;



		}
		private DataSet Search(string id)
		{
			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			if(id.Trim().ToLower() == "77sindong")
			{
				if(DropDownList1.SelectedIndex == 0)
				{
					if(DropDownList2.SelectedIndex == 0)
					{
						Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate, Approval,RegistrationPerson, RegistrationID, [Index] From WorkReport Where RegistrationID is not null and [Index] Not in (Select Top "+iPage*15+" [Index] From WorkReport  order by RegistrationDate desc, [Index] desc) order by RegistrationDate desc, [Index] desc";
					}
					else
					{
						Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate, Approval,RegistrationPerson, RegistrationID, [Index] From WorkReport Where RegistrationID is not null and Approval = @Approval and [Index] Not in (Select Top "+iPage*15+" [Index] From WorkReport order by RegistrationDate desc, [Index] desc) order by RegistrationDate desc, [Index] desc";	
						Cmd.Parameters.Add("@Approval",DropDownList2.SelectedItem.Value);
					}
				}
				else
				{
					if(DropDownList2.SelectedIndex == 0)
					{
						Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate, Approval,RegistrationPerson, RegistrationID, [Index] From WorkReport Where RegistrationID = @RegistrationID and [Index] Not in (Select Top "+iPage*15+" [Index] From WorkReport Where RegistrationID = @RegistrationID order by RegistrationDate desc, [Index] desc) order by RegistrationDate desc, [Index] desc";
						Cmd.Parameters.Add("@RegistrationID",DropDownList1.SelectedItem.Value);
					}
					else
					{
						Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate, Approval,RegistrationPerson, RegistrationID, [Index] From WorkReport Where RegistrationID = @RegistrationID and Approval = @Approval and [Index] Not in (Select Top "+iPage*15+" [Index] From WorkReport Where RegistrationID = @RegistrationID order by RegistrationDate desc, [Index] desc) order by RegistrationDate desc, [Index] desc";
						Cmd.Parameters.Add("@Approval",DropDownList2.SelectedItem.Value);
						Cmd.Parameters.Add("@RegistrationID", DropDownList1.SelectedItem.Value);
					}
				}
			}
			else
			{
				Cmd.CommandText = "Select Top 15 Convert(varchar, ReportDate, 111)ReportDate, Approval,RegistrationPerson, RegistrationID, [Index] From WorkReport Where RegistrationID = @id and [Index] Not in (Select Top "+iPage*15+" [Index] From WorkReport Where RegistrationID = @id order by RegistrationDate desc, [Index] desc) order by RegistrationDate desc, [Index] desc";
				Cmd.Parameters.Add("@id",id);
			}
			
			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			adp.Fill(ds);

			return ds;

			
			
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			DataGrid1.DataSource = Search(Session["ID"].ToString().Trim().ToLower());
			DataGrid1.DataBind();
		}

		private void btnWrite_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkDailyWrite.aspx");
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// 마우스오버시 색깔...
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string strDate = ((DataRowView)e.Item.DataItem)["ReportDate"].ToString();
				DateTime orginDate = DateTime.Parse(strDate);
				
				TimeSpan gap = DateTime.Now - orginDate;
				if(gap.TotalMinutes < 1440)
				{
					Literal l = new Literal();
					l.Text = " <img src=../images/new.gif>";
					e.Item.Cells[0].Controls.Add(l);
				}

				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#F0F0F0'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
			

		}

		private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			TableCell summaryCell;

			
			if ( e.Item.ItemType == ListItemType.Footer )
			{
				int cellCountNum = e.Item.Cells.Count;

				for ( int i = 1 ; i < cellCountNum ;i++ )
				{
					e.Item.Cells.RemoveAt(0); //footer의 칼럼을 1개로 합치기
				} 

				((DataGrid)sender).ShowFooter = false;
				summaryCell = e.Item.Cells[0];

				if( ((DataGrid)sender).Items.Count == 0)
				{
					summaryCell.Text = "등록된 글이 없습니다!.";
					((DataGrid)sender).ShowFooter = true;
				} 

				summaryCell.ColumnSpan = cellCountNum;
 
				//좌우 정렬을 지정한다. 
				summaryCell.HorizontalAlign = HorizontalAlign.Center;
			}
		}

		private void DataGrid1_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			iPage = e.NewPageIndex;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			
			// 페이지번호 클릭시 현재 선택한 페이지번호를 쿠키에 저장
			Response.Cookies["WorkDiary"]["PageNumber"] = iPage.ToString();
			Listing();
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			iPage = 0;
			DataGrid1.CurrentPageIndex = 0;

			DataGrid1.PageSize = 15;
			DataGrid1.VirtualItemCount = RecordCount();

			DataGrid1.DataSource = Search(Session["ID"].ToString().ToLower());
			DataGrid1.DataBind();
			lblPageInfo.Text =  string.Format("( 현재 {0}페이지 / 전체 {1}페이지 )", iPage+1, DataGrid1.PageCount);
		}

		
	}
}
