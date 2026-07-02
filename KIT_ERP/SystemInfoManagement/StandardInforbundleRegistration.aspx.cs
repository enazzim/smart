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

using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// StandardInforbundleRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class StandardInforbundleRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Image Image7;
		protected System.Web.UI.WebControls.Image Image8;
		protected System.Web.UI.WebControls.Image Image9;
		protected System.Web.UI.WebControls.Image Image10;
		protected System.Web.UI.WebControls.Image Image11;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.HyperLink HyperLink2;
		protected System.Web.UI.WebControls.HyperLink HyperLink3;
		protected System.Web.UI.WebControls.HyperLink HyperLink4;
		protected System.Web.UI.WebControls.HyperLink HyperLink5;
		protected System.Web.UI.WebControls.HyperLink HyperLink6;
		protected System.Web.UI.WebControls.HyperLink HyperLink7;
		protected System.Web.UI.WebControls.HyperLink HyperLink8;
		protected System.Web.UI.WebControls.HyperLink HyperLink9;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		private DataSet dsExcel = new DataSet();
		private int count = 0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 시스템정보 > 기준정보 일괄입력';</script>");
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// TODO : 정리버튼 클릭시
		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(File1.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(File1.PostedFile.FileName) + System.IO.Path.GetFileName(File1.PostedFile.FileName);
			
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls" || FileName.ToString().Substring(FileName.ToString().Length - 3) == "XLS")
				{

					string upLoadFile = Path.GetTempFileName();
					File1.PostedFile.SaveAs(upLoadFile);

					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
			
					con.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con);
						
					adapter.Fill(dsExcel);
					con.Close();

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();
					SqlTransaction tr = conn.BeginTransaction();

					try
					{

						switch(DropDownList1.SelectedItem.Text)
						{
							case "거래처 정보"	:
								CompanyInfo(conn,tr);
								break;
							case "품목 정보"	:
								ItemInfo(conn,tr);
								break;
							case "품목 구성 정보"	:
								ItemOrganizationInfo(conn,tr);
								break;
							case "작업장 정보"		:
								WCInfo(conn,tr);
								break;
							case "공정순서 정보"	:
								ProcessSequenceInfo(conn,tr);
								break;
							case "설비 정보"	:
								EquipmentInfo(conn,tr);
								break;
							case "작업표준 정보"	:
								WorkStandardInfo(conn,tr);
								break;
							case "단가 정보"	:
								UnitCodeInfo(conn,tr);
								break;
							case "사용자 정보"	:
								UserInfo(conn,tr);
								break;
							case "진품목구성정보"	:
								RealItemOrganizationInfo(conn,tr);
								break;
							case "진공정정보"	:
								RealProcessSequenceInfo(conn,tr);
								break;
							case "진작업표준정보"	:
								RealWorkStandardInfo(conn,tr);
								break;
						}
						Response.Write("<script language=javascript>");
						Response.Write("alert('등록되었습니다.');");
						Response.Write("</script>");
						tr.Commit();
					}
					catch(Exception ee)
					{
						Response.Write("<script language=javascript>");
						Response.Write("alert('"+count+"번째자료 "+ee.Message+"');");
						Response.Write("</script>");

						tr.Rollback();
					}
					finally
					{
						File.Delete(upLoadFile);
						conn.Close();
					}
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}

		private void CompanyInfo(SqlConnection con, SqlTransaction trans)
		{
			
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
                arrayList = new ArrayList();
				
				if(row["수주거래처"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["수주거래처"].ToString()));
				
				if(row["외주거래처"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["외주거래처"].ToString()));
				
				if(row["구매거래처"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["구매거래처"].ToString()));
				
				if(row["비용거래처"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["비용거래처"].ToString()));
				
				arrayList.Add(row["거래처명"].ToString().Trim());
				arrayList.Add(row["대표자명"].ToString().Trim());
				arrayList.Add(row["사업자등록번호"].ToString().Trim());
				arrayList.Add(row["법인등록번호"].ToString().Trim());
				arrayList.Add(row["사업장주소"].ToString().Trim());
				arrayList.Add(row["세금계산주소"].ToString().Trim());
				arrayList.Add(row["홈페이지주소"].ToString().Trim());
				arrayList.Add(row["업태"].ToString().Trim());
				arrayList.Add(row["종목"].ToString().Trim());
				
				if(row["거래상태"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["거래상태"].ToString().Trim());

				if(row["부가세처리"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["부가세처리"].ToString().Trim());
				
				if(row["부가세율"].ToString().Trim() == "")
					arrayList.Add("10");
				else
					arrayList.Add(decimal.Parse(row["부가세율"].ToString().Trim()));
				arrayList.Add(row["전화번호"].ToString().Trim());
				arrayList.Add(row["Fax"].ToString().Trim());
				arrayList.Add(row["거래처분류1"].ToString().Trim());
				arrayList.Add(row["거래처분류2"].ToString().Trim());
				arrayList.Add(row["거래처분류3"].ToString().Trim());
				arrayList.Add(row["판매기준일"].ToString().Trim());
				arrayList.Add(row["판매기준어음일"].ToString().Trim());
				arrayList.Add(row["어음결재기준"].ToString().Trim());
				arrayList.Add(row["정기수금일1"].ToString().Trim());
				arrayList.Add(row["정기수금일2"].ToString().Trim());
				arrayList.Add(row["거래처담당자"].ToString().Trim());
				arrayList.Add(row["담당자이메일"].ToString().Trim());

				Upload upload = new Upload("CompanyInfo", "Registration", "CI_MT", (int)PageName.CompanyInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				
				count++;
			}
		}

		private void ItemInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["도면번호"].ToString().Trim());
				arrayList.Add(row["품목명"].ToString().Trim());
				arrayList.Add(row["자산분류"].ToString().Trim());
				arrayList.Add(row["단위"].ToString().Trim());
				arrayList.Add(row["규격"].ToString().Trim());
				
				if(row["과세여부"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["과세여부"].ToString().Trim());

				if(row["부가세율"].ToString().Trim() == "")
					arrayList.Add(10);
				else
					arrayList.Add(float.Parse(row["부가세율"].ToString().Trim()));
				arrayList.Add(row["재고단위"].ToString().Trim());
				arrayList.Add(row["BOM단위"].ToString().Trim());
				arrayList.Add(row["구매단위"].ToString().Trim());
				arrayList.Add(row["판매단위"].ToString().Trim());
				

				if(row["자산분류"].ToString().Trim() =="부자재")
					arrayList.Add(0);
				else
				{
					if(row["자재산출여부"].ToString().Trim() == "")
						arrayList.Add(1);
					else
						arrayList.Add(row["자재산출여부"].ToString().Trim());
				}
				
				if(row["재고관리여부"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(int.Parse(row["재고관리여부"].ToString().Trim()));
				
				if(row["자산분류"].ToString().Trim() == "부자재")
					arrayList.Add(0);
				else
				{
					if(row["검사구분"].ToString().Trim() == "")
						arrayList.Add(1);
					else
						arrayList.Add(int.Parse(row["검사구분"].ToString().Trim()));
				}

				if(row["발주방침"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(int.Parse(row["발주방침"].ToString().Trim()));

				arrayList.Add(row["품목타입"].ToString().Trim());
				arrayList.Add(row["재질"].ToString().Trim());
				
				if(row["품목상태"].ToString().Trim() == "")
					arrayList.Add("06300010");//양산
				else
					arrayList.Add(row["품목상태"].ToString().Trim());

				arrayList.Add(row["메이커"].ToString().Trim());
				arrayList.Add(row["품목분류1"].ToString().Trim());
				arrayList.Add(row["품목분류2"].ToString().Trim());
				arrayList.Add(row["품목분류3"].ToString().Trim());
				arrayList.Add(row["품목분류4"].ToString().Trim());
				arrayList.Add(row["규격1"].ToString().Trim());
				arrayList.Add(row["단위1"].ToString().Trim());
				arrayList.Add(row["규격2"].ToString().Trim());
				arrayList.Add(row["단위2"].ToString().Trim());
				arrayList.Add(row["규격3"].ToString().Trim());
				arrayList.Add(row["단위3"].ToString().Trim());
				arrayList.Add(row["규격4"].ToString().Trim());
				if(row["단위4"].ToString().Trim() == "")
					arrayList.Add("예");
				else
					arrayList.Add(row["단위4"].ToString().Trim());

				arrayList.Add(row["제품중량"].ToString().Trim());
				arrayList.Add(row["소재중량"].ToString().Trim());
				arrayList.Add(row["조달기간"].ToString().Trim());
				if(row["완성리드타임"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["완성리드타임"].ToString().Trim()));
				
				if(row["내외자구분"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(int.Parse(row["내외자구분"].ToString().Trim()));

				if(row["안전재고량"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["안전재고량"].ToString().Trim()));

				if(row["발주간격수량"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["발주간격수량"].ToString().Trim()));
				
				if(row["최소발주량"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["최소발주량"].ToString().Trim()));
				arrayList.Add(row["경화깊이"].ToString().Trim());
				arrayList.Add(row["제품열처리사양"].ToString().Trim());
				arrayList.Add(row["소재열처리사양"].ToString().Trim());
				arrayList.Add(row["소재열처리정도"].ToString().Trim());
				arrayList.Add(row["절단여유"].ToString().Trim());
				
				if(row["관세율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["관세율"].ToString().Trim()));
				
				if(row["기준단가"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["기준단가"].ToString().Trim()));
				arrayList.Add(row["주구입처"].ToString().Trim());
				arrayList.Add(row["주외주처"].ToString().Trim());
				arrayList.Add(row["주판매처"].ToString().Trim());
				arrayList.Add(row["담당자"].ToString().Trim());

				Upload upload = new Upload("ItemInfo", "Registration", "II_MT", (int)PageName.ItemInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);

				count++;
			}
		}

		private void ItemOrganizationInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["모품목번호"].ToString().Trim());
				arrayList.Add(row["자품목번호"].ToString().Trim());

				if(row["소요량분자"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["소요량분자"].ToString().Trim()));

				if(row["소요량분모"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["소요량분모"].ToString().Trim()));
				
				if(row["공정관리"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["공정관리"].ToString());
				if(row["하위구분"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(row["하위구분"].ToString().Trim());
				arrayList.Add(row["조달구분"].ToString().Trim());
				arrayList.Add(row["BOM단위"].ToString().Trim());

				if(row["시작일"].ToString().Trim() == "")
					arrayList.Add("2000-01-01");
				else
					arrayList.Add(row["시작일"].ToString().Substring(0,10).Trim());
				if(row["종료일"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
                    arrayList.Add(row["종료일"].ToString().Substring(0,10).Trim());
				
				Upload upload = new Upload("ItemOrganizationInfo", "Registration", "IOI_MT", (int)PageName.ItemOrganizationInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void WCInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["WC명"].ToString().Trim());
				arrayList.Add(row["대표공정"].ToString().Trim());
				
				if(row["보유인원"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["보유인원"].ToString().Trim());
				
				if(row["현상태"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["현상태"].ToString().Trim());
				
				if(row["Capa구분"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(row["Capa구분"].ToString().Trim());

				if(row["가동시간"].ToString().Trim() == "")
					arrayList.Add(480);
				else
					arrayList.Add(decimal.Parse(row["가동시간"].ToString().Trim()));
				
				if(row["전력용량"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["전력용량"].ToString().Trim()));
				
				if(row["단위시간당사용료"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["단위시간당사용료"].ToString().Trim()));
				
				Upload upload = new Upload("WCInfo", "Registration", "WCI_MT", (int)PageName.WCInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void ProcessSequenceInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["공정순서번호"].ToString().Trim());
				arrayList.Add(row["공정코드명"].ToString().Trim());
				arrayList.Add(row["작업구분"].ToString().Trim());
				arrayList.Add(row["WC명"].ToString().Trim());
				
				if(row["외주발주비율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["외주발주비율"].ToString().Trim()));
				
				if(row["진척비율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["진척비율"].ToString().Trim()));
				
				if(row["리드타임"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["리드타임"].ToString().Trim()));

				arrayList.Add(row["기타"].ToString().Trim());
				
				Upload upload = new Upload("ProcessSequenceInfo", "Registration", "PSI_MT", (int)PageName.ProcessSequenceInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void EquipmentInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["설비번호"].ToString().Trim());
				arrayList.Add(row["설비명"].ToString().Trim());
				arrayList.Add(row["설비분류"].ToString().Trim());
				arrayList.Add(row["규격"].ToString().Trim());
				
				if(row["Capacity"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["Capacity"].ToString().Trim()));
				
				if(row["전력량"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["전력량"].ToString().Trim()));
				
				if(row["시간당사용료"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["시간당사용료"].ToString().Trim()));
				arrayList.Add(row["단위"].ToString().Trim());
				arrayList.Add(row["WC명"].ToString().Trim());
				
				if(row["투입원"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["투입원"].ToString().Trim()));
				arrayList.Add(row["위치"].ToString());
				
				if(row["차지면적"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["차지면적"].ToString().Trim()));
				
				if(row["구입일"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["구입일"].ToString()).ToShortDateString().Trim());
				
				
				if(row["구입가격"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(row["구입가격"].ToString().Trim());
				
				arrayList.Add(row["기기고유번호"].ToString().Trim());

				if(row["설비상태"].ToString().Trim() == "")
					arrayList.Add("가동중");
				else
					arrayList.Add(row["설비상태"].ToString().Trim());
				if(row["검교정일"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["검교정일"].ToString()).ToShortDateString().Trim());
				if(row["유효기간"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["유효기간"].ToString()).ToShortDateString().Trim());
				arrayList.Add(row["검교정기관"].ToString());
				
				if(row["가동준비시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["가동준비시간"].ToString().Trim()));
				
				if(row["Cavity"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["Cavity"].ToString().Trim()));

				if(row["내용년수"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["내용년수"].ToString().Trim()));
				
				if(row["설계샷"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["설계샷"].ToString().Trim()));
				
				if(row["초기샷"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["초기샷"].ToString().Trim()));
				if(row["누계샷"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["누계샷"].ToString().Trim()));
                
				if(row["작업샷"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["작업샷"].ToString().Trim()));

				arrayList.Add(row["관리주기1"].ToString());
				if(row["관리일1"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["관리일1"].ToString()).ToShortDateString().Trim());
				arrayList.Add(row["관리주기2"].ToString());
				if(row["관리일2"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["관리일2"].ToString()).ToShortDateString().Trim());
				arrayList.Add(row["관리주기3"].ToString());
				if(row["관리일3"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["관리일3"].ToString()).ToShortDateString().Trim());
				arrayList.Add(row["관리주기4"].ToString());
				if(row["관리일4"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["관리일4"].ToString()).ToShortDateString().Trim());

				Upload upload = new Upload("EquipmentInfo", "Registration", "EI_MT", (int)PageName.EquipmentInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void WorkStandardInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["공정순서번호"].ToString().Trim());
				arrayList.Add(row["공정코드명"].ToString().Trim());
				arrayList.Add(row["WC명"].ToString().Trim());
				if(row["우선순위"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["우선순위"].ToString().Trim()));
				arrayList.Add(row["주작업자ID"].ToString().Trim());
				arrayList.Add(row["주작업자"].ToString().Trim());
				arrayList.Add(row["사용공구1"].ToString().Trim());
				arrayList.Add(row["사용치구1"].ToString().Trim());
				arrayList.Add(row["사용공구2"].ToString().Trim());
				arrayList.Add(row["사용치구2"].ToString().Trim());
				arrayList.Add(row["사용공구3"].ToString().Trim());
				arrayList.Add(row["사용치구3"].ToString().Trim());
				
				if(row["셋업타임"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["셋업타임"].ToString().Trim()));
				
				if(row["실가공시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["실가공시간"].ToString().Trim()));
				
				if(row["여유시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["여유시간"].ToString().Trim()));
				
				if(row["표준시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["표준시간"].ToString().Trim()));
				
				if(row["대기시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["대기시간"].ToString().Trim()));
				
				if(row["LotSize"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["LotSize"].ToString().Trim()));
				
				if(row["Cavity"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["Cavity"].ToString().Trim()));
				
				Upload upload = new Upload("WorkStandardInfo", "Registration", "WSI_MT", (int)PageName.WorkStandardInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void UnitCodeInfo(SqlConnection con, SqlTransaction trans)
		{
			Upload upload;
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["단가구분"].ToString().Trim());
				arrayList.Add(row["거래처번호"].ToString().Trim());

				switch(Property(row["품목번호"].ToString().Trim(),con,trans))
				{
					case "상품":
						arrayList.Add("14009999");
						arrayList.Add("14009999");
						break;
					case "원자재":
						arrayList.Add("14000000");
						arrayList.Add("14000000");
						break;
					default :
						arrayList.Add(row["시작공정"].ToString().Trim());
						arrayList.Add(row["종료공정"].ToString().Trim());
						break;
				}
				
				if(row["발주비율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["발주비율"].ToString().Trim()));
				
				if(row["기준단가"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["기준단가"].ToString().Trim()));
				
				if(row["할인단가"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["할인단가"].ToString().Trim()));
				
				if(row["적용시작일"].ToString().Trim() == "")
					arrayList.Add("1900-01-01");
				else
                    arrayList.Add(DateTime.Parse(row["적용시작일"].ToString()).ToShortDateString().Trim());

				if(row["적용종료일"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(DateTime.Parse(row["적용종료일"].ToString()).ToShortDateString().Trim());
				
				switch(row["단가구분"].ToString().Trim())
				{
					case "외주단가" :
						upload = new Upload("OutSideOrderUnitCodeInfo", "Registration", "UCI_MT", (int)PageName.OutSideOrderUnitCodeInfo,Session["ID"].ToString(), arrayList,"일괄입력");
						upload.Register(con,trans);
						break;
					case "판매단가"	:
						upload = new Upload("SaleUnitCodeInfo", "Registration", "UCI_MT", (int)PageName.SaleUnitCodeInfo,Session["ID"].ToString(), arrayList,"일괄입력");
						upload.Register(con,trans);
						break;
					case "구매단가" :
						upload = new Upload("BuyingUnitCodeInfo", "Registration", "UCI_MT", (int)PageName.BuyingUnitCodeInfo,Session["ID"].ToString(), arrayList,"일괄입력");
						upload.Register(con,trans);
						break;
				}
				count++;
			}
		}

		private void UserInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["아이디"].ToString().Trim());
				arrayList.Add(row["패스워드"].ToString().Trim());
				arrayList.Add(row["이름"].ToString().Trim());
				arrayList.Add(row["부서"].ToString().Trim());
				arrayList.Add(row["직책"].ToString().Trim());
				arrayList.Add(row["주민등록번호"].ToString().Trim());
				arrayList.Add(row["연락처1"].ToString().Trim());
				arrayList.Add(row["연락처2"].ToString().Trim());
				arrayList.Add(row["주소"].ToString().Trim());
				if(row["입사일"].ToString().Trim() == "")
					arrayList.Add("1900-01-01");
				else
					arrayList.Add(row["입사일"].ToString().Substring(0,10).Trim());
				arrayList.Add(row["홈페이지권한여부"].ToString().Trim());
				arrayList.Add(row["기준정보등록권한여부"].ToString().Trim());
				arrayList.Add(row["사용자등급"].ToString().Trim());
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add(row["거래처"].ToString().Trim());
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				arrayList.Add("");
				
				Upload upload = new Upload("UserInfo", "Registration", "UI_MT", (int)PageName.UserInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}


		private void RealItemOrganizationInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["모품목번호"].ToString().Trim());
				arrayList.Add(row["자품목번호"].ToString().Trim());

				if(row["소요량분자"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["소요량분자"].ToString().Trim()));

				if(row["소요량분모"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(decimal.Parse(row["소요량분모"].ToString().Trim()));
				
				if(row["공정관리"].ToString().Trim() == "")
					arrayList.Add(1);
				else
					arrayList.Add(row["공정관리"].ToString());
				if(row["하위구분"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(row["하위구분"].ToString().Trim());
				arrayList.Add(row["조달구분"].ToString().Trim());
				arrayList.Add(row["BOM단위"].ToString().Trim());
				arrayList.Add(row["시작일"].ToString().Substring(0,10).Trim());
				if(row["종료일"].ToString().Trim() == "")
					arrayList.Add("2076-06-06");
				else
					arrayList.Add(row["종료일"].ToString().Substring(0,10).Trim());
				
				Upload upload = new Upload("RealItemOrganizationInfo", "Registration", "RIOI_MT", (int)PageName.RealItemOrganizationInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}

		private void RealProcessSequenceInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["공정순서번호"].ToString().Trim());
				arrayList.Add(row["공정코드명"].ToString().Trim());
				arrayList.Add(row["작업구분"].ToString().Trim());
				arrayList.Add(row["WC명"].ToString().Trim());
				
				if(row["외주발주비율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["외주발주비율"].ToString().Trim()));
				
				if(row["진척비율"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["진척비율"].ToString().Trim()));
				
				if(row["리드타임"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["리드타임"].ToString().Trim()));

				arrayList.Add(row["기타"].ToString().Trim());
				
				Upload upload = new Upload("RealProcessSequenceInfo", "Registration", "RPSI_MT", (int)PageName.RealProcessSequenceInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}


		private void RealWorkStandardInfo(SqlConnection con, SqlTransaction trans)
		{
			ArrayList arrayList;
			foreach(DataRow row in dsExcel.Tables[0].Rows)
			{
				arrayList = new ArrayList();
				arrayList.Add(row["품목번호"].ToString().Trim());
				arrayList.Add(row["공정순서번호"].ToString().Trim());
				arrayList.Add(row["공정코드명"].ToString().Trim());
				arrayList.Add(row["WC명"].ToString().Trim());
				if(row["우선순위"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(int.Parse(row["우선순위"].ToString().Trim()));
				arrayList.Add(row["주작업자ID"].ToString().Trim());
				arrayList.Add(row["주작업자"].ToString().Trim());
				arrayList.Add(row["사용공구1"].ToString().Trim());
				arrayList.Add(row["사용치구1"].ToString().Trim());
				arrayList.Add(row["사용공구2"].ToString().Trim());
				arrayList.Add(row["사용치구2"].ToString().Trim());
				arrayList.Add(row["사용공구3"].ToString().Trim());
				arrayList.Add(row["사용치구3"].ToString().Trim());
				
				if(row["셋업타임"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["셋업타임"].ToString().Trim()));
				
				if(row["실가공시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["실가공시간"].ToString().Trim()));
				
				if(row["여유시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["여유시간"].ToString().Trim()));
				
				if(row["표준시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["표준시간"].ToString().Trim()));
				
				if(row["대기시간"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["대기시간"].ToString().Trim()));
				
				if(row["LotSize"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["LotSize"].ToString().Trim()));
				
				if(row["Cavity"].ToString().Trim() == "")
					arrayList.Add(0);
				else
					arrayList.Add(decimal.Parse(row["Cavity"].ToString().Trim()));
				
				Upload upload = new Upload("RealWorkStandardInfo", "Registration", "RWSI_MT", (int)PageName.RealWorkStandardInfo,Session["ID"].ToString(), arrayList,"일괄입력");
				upload.Register(con,trans);
				count++;
			}
		}


		private string Property(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string Classification = "";
			string str_cost = "Select PropertyClassification From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				Classification = dr["PropertyClassification"].ToString();
			dr.Close();

			if(Classification == "")
				throw new Exception("품목정보에 품목 "+ItemNum+"의 자산분류가 존재하지 않습니다!");
			
			return Classification;
		}
	}
}
