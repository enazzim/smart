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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// LoaderState에 대한 요약 설명입니다.
	/// </summary>
	public class LoaderState : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.Button btComSearch;
		protected System.Web.UI.WebControls.TextBox txtDeliveryQuantity;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
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
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.Label Label52;
		protected System.Web.UI.WebControls.Label Label53;
		protected System.Web.UI.WebControls.Label Label54;
		protected System.Web.UI.WebControls.Label Label55;
		protected System.Web.UI.WebControls.Label Label56;
		protected System.Web.UI.WebControls.Label Label57;
		protected System.Web.UI.WebControls.Label Label58;
		protected System.Web.UI.WebControls.Label Label59;
		protected System.Web.UI.WebControls.Label Label60;
		protected System.Web.UI.WebControls.Label Label61;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label76;
		protected System.Web.UI.WebControls.Label Label77;
		protected System.Web.UI.WebControls.Label Label78;
		protected System.Web.UI.WebControls.Label Label79;
		protected System.Web.UI.WebControls.Label Label80;
		protected System.Web.UI.WebControls.Label Label81;
		protected System.Web.UI.WebControls.Label Label82;
		protected System.Web.UI.WebControls.Label Label83;
		protected System.Web.UI.WebControls.Label Label84;
		protected System.Web.UI.WebControls.Label Label85;
		protected System.Web.UI.WebControls.Label Label86;
		protected System.Web.UI.WebControls.Label Label87;
		protected System.Web.UI.WebControls.Label Label88;
		protected System.Web.UI.WebControls.Label Label89;
		protected System.Web.UI.WebControls.Label Label90;
		protected System.Web.UI.WebControls.Label Label91;
		protected System.Web.UI.WebControls.Label Label92;
		protected System.Web.UI.WebControls.Label Label93;
		protected System.Web.UI.WebControls.Label Label94;
		protected System.Web.UI.WebControls.Label Label95;
		protected System.Web.UI.WebControls.Label Label96;
		protected System.Web.UI.WebControls.Label Label97;
		protected System.Web.UI.WebControls.Label Label98;
		protected System.Web.UI.WebControls.Label Label99;
		protected System.Web.UI.WebControls.Label Label100;
		protected System.Web.UI.WebControls.Label Label101;
		protected System.Web.UI.WebControls.Label Label102;
		protected System.Web.UI.WebControls.Label Label103;
		protected System.Web.UI.WebControls.Label Label104;
		protected System.Web.UI.WebControls.Label Label105;
		protected System.Web.UI.WebControls.Label Label106;
		protected System.Web.UI.WebControls.Label Label107;
		protected System.Web.UI.WebControls.Label Label108;
		protected System.Web.UI.WebControls.Label Label109;
		protected System.Web.UI.WebControls.Label Label110;
		protected System.Web.UI.WebControls.Label Label111;
		protected System.Web.UI.WebControls.Label Label112;
		protected System.Web.UI.WebControls.Label Label113;
		protected System.Web.UI.WebControls.Label Label114;
		protected System.Web.UI.WebControls.Label Label115;
		protected System.Web.UI.WebControls.Label Label116;
		protected System.Web.UI.WebControls.Label Label117;
		protected System.Web.UI.WebControls.Label Label118;
		protected System.Web.UI.WebControls.Label Label119;
		protected System.Web.UI.WebControls.Label Label120;
		protected System.Web.UI.WebControls.Label Label121;
		protected System.Web.UI.WebControls.Label Label122;
		protected System.Web.UI.WebControls.Label Label123;
		protected System.Web.UI.WebControls.Label Label124;
		protected System.Web.UI.WebControls.Label Label125;
		protected System.Web.UI.WebControls.Label Label126;
		protected System.Web.UI.WebControls.Label Label127;
		protected System.Web.UI.WebControls.Label Label128;
		protected System.Web.UI.WebControls.Label Label129;
		protected System.Web.UI.WebControls.Label Label130;
		protected System.Web.UI.WebControls.Label Label131;
		protected System.Web.UI.WebControls.Label Label132;
		protected System.Web.UI.WebControls.Label Label133;
		protected System.Web.UI.WebControls.Label Label134;
		protected System.Web.UI.WebControls.Label Label135;
		protected System.Web.UI.WebControls.Label Label136;
		protected System.Web.UI.WebControls.Label Label137;
		protected System.Web.UI.WebControls.Label Label138;
		protected System.Web.UI.WebControls.Label Label139;
		protected System.Web.UI.WebControls.Label Label140;
		protected System.Web.UI.WebControls.Label Label141;
		protected System.Web.UI.WebControls.Label Label142;
		protected System.Web.UI.WebControls.Label Label143;
		protected System.Web.UI.WebControls.Label Label144;
		protected System.Web.UI.WebControls.Label Label145;
		protected System.Web.UI.WebControls.Label Label146;
		protected System.Web.UI.WebControls.Label Label147;
		protected System.Web.UI.WebControls.Label Label148;
		protected System.Web.UI.WebControls.Label Label149;
		protected System.Web.UI.WebControls.Label Label150;
		protected System.Web.UI.WebControls.Label Label151;
		protected System.Web.UI.WebControls.Label Label152;
		protected System.Web.UI.WebControls.Label Label153;
		protected System.Web.UI.WebControls.Label Label154;
		protected System.Web.UI.WebControls.Label Label155;
		protected System.Web.UI.WebControls.Label Label156;
		protected System.Web.UI.WebControls.Label Label157;
		protected System.Web.UI.WebControls.Label Label158;
		protected System.Web.UI.WebControls.Label Label159;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Assay;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Delevery;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcAssayDate;
		protected System.Web.UI.WebControls.TextBox txtAssayQuantity;
		protected System.Web.UI.WebControls.Label Label160;
		protected bool boolAssay;
		protected bool boolDelevery;

		private string Month;
		private string Day;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				SearchReset();
				//boolAssay =true;
				//boolDelevery = true;
				boolAssay = true;
				boolDelevery = true;
			}
		}


		private void SearchReset()
		{

			for(int a = 0; a<=ddlYear.Items.Count; a++) 
			{
				if(ddlYear.Items[a].Value == DateTime.Now.Year.ToString())
				{
					ddlYear.SelectedIndex = a;
					break;
				}					
			}

			ddlMon.SelectedIndex = DateTime.Now.Month-1; 
   
			Delevery.Value = "0";
			Assay.Value = "0";
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
			this.btComSearch.Click += new System.EventHandler(this.btComSearch_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void AssayPrintList(string total, string day)
		{
			
			switch(day)
			{
				case "1":
					Label12.Text = total;				
					Label13.Text = total;
					Label14.Text = total;
					Label15.Text = total;
					Label16.Text = total;
					Label17.Text = total;
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "2":
					Label13.Text = total;
					Label14.Text = total;
					Label15.Text = total;
					Label16.Text = total;
					Label17.Text = total;
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case"3":
					Label14.Text = total;
					Label15.Text = total;
					Label16.Text = total;
					Label17.Text = total;
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "4":					
					Label15.Text = total;
					Label16.Text = total;
					Label17.Text = total;
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "5":
					Label16.Text = total;
					Label17.Text = total;
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "7":
					Label18.Text = total;
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "8":
					Label19.Text = total;
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "9":
					Label20.Text = total;

					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "10":
					Label51.Text = total;
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				
				case "11":
					Label52.Text = total;
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				
				case "12":
					Label53.Text = total;
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "13":
					Label54.Text = total;
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;


				case "14":
					Label55.Text = total;
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "15":
					Label56.Text = total;
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "16":
					Label57.Text = total;
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "17":
					Label58.Text = total;
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "18":
					Label59.Text = total;			
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "19":
					Label60.Text = total;

					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "20":
					Label91.Text = total;
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "21":
					Label92.Text = total;
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "22":
					Label93.Text = total;
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "23":
					Label94.Text = total;
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "24":
					Label95.Text = total;
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "25":
					Label96.Text = total;
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "26":
					Label97.Text = total;
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "27":
					Label98.Text = total;
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "28":
					Label99.Text = total;
					Label100.Text = total;
				
					Label131.Text = total;
					break;
				case "29":
					Label100.Text = total;
				
					Label131.Text = total;
					break;

				case "30":
					Label131.Text = total;
					break;
			}
		}




		private void SendPrintList(string total,string day)
		{
			switch(day)
			{
				case "1":
					Label32.Text = total;
					Label33.Text = total;
					Label34.Text = total;
					Label35.Text = total;
					Label36.Text = total;
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "2":
					Label33.Text = total;
					Label34.Text = total;
					Label35.Text = total;
					Label36.Text = total;
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "3":
					Label34.Text = total;
					Label35.Text = total;
					Label36.Text = total;
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "4":
					Label35.Text = total;
					Label36.Text = total;
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "5":
					Label36.Text = total;
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "6":
					Label37.Text = total;
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "7":
					Label38.Text = total;
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "8":
					Label39.Text = total;
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "9":
					Label40.Text = total;

					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "10":
					Label71.Text = total;
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "11":
					Label72.Text = total;
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "12":
					Label73.Text = total;
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "13":
					Label74.Text = total;
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "14":
					Label75.Text = total;
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "15":
					Label76.Text = total;
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "16":
					Label77.Text = total;
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "17":
					Label78.Text = total;
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "18":
					Label79.Text = total;			
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "19":
					Label80.Text = total;
					
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "20":
					Label111.Text = total;
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "21":
					Label112.Text = total;
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "22":
					Label113.Text = total;
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "23":
					Label114.Text = total;
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "24":
					Label115.Text = total;
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "25":
					Label116.Text = total;
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "26":
					Label117.Text = total;
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "27":
					Label118.Text = total;
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "28":
					Label119.Text = total;
					Label120.Text = total;
				
					Label151.Text = total;
					break;

				case "29":
					Label120.Text = total;
				
					Label151.Text = total;
					break;
				case "30":
					Label151.Text = total;
					break;
			}
		}


		private void btComSearch_Click(object sender, System.EventArgs e)
		{
			search();			
		}


		private void totalClear()
		{			
				Label1.Text = Label2.Text = Label3.Text = Label4.Text = Label5.Text =  Label6.Text =  Label7.Text =  Label8.Text =   Label9.Text =  Label10.Text = "";
				Label11.Text = Label12.Text = Label13.Text = Label14.Text = Label15.Text =  Label16.Text =  Label17.Text =  Label18.Text =   Label19.Text =  Label20.Text = "";
				Label21.Text = Label22.Text = Label23.Text = Label24.Text = Label25.Text =  Label26.Text =  Label27.Text =  Label28.Text =   Label29.Text =  Label30.Text = "";
				Label31.Text = Label32.Text = Label33.Text = Label34.Text = Label35.Text =  Label36.Text =  Label37.Text =  Label38.Text =   Label39.Text =  Label40.Text = "";
				Label41.Text = Label42.Text = Label43.Text = Label44.Text = Label45.Text =  Label46.Text =  Label47.Text =  Label48.Text =   Label49.Text =  Label50.Text = "";
				Label51.Text = Label52.Text = Label53.Text = Label54.Text = Label55.Text =  Label56.Text =  Label57.Text =  Label58.Text =   Label59.Text =  Label60.Text = "";
				Label61.Text = Label62.Text = Label63.Text = Label64.Text = Label65.Text =  Label66.Text =  Label67.Text =  Label68.Text =   Label69.Text =  Label70.Text = "";
				Label71.Text = Label72.Text = Label73.Text = Label74.Text = Label75.Text =  Label76.Text =  Label77.Text =  Label78.Text =   Label79.Text =  Label80.Text = "";
				Label81.Text = Label82.Text = Label83.Text = Label84.Text = Label85.Text =  Label86.Text =  Label87.Text =  Label88.Text =   Label89.Text =  Label90.Text = "";
				Label91.Text = Label92.Text = Label93.Text = Label94.Text = Label95.Text =  Label96.Text =  Label97.Text =  Label98.Text =   Label99.Text =  Label100.Text = "";
				Label101.Text = Label102.Text = Label103.Text = Label104.Text = Label105.Text =  Label106.Text =  Label107.Text =  Label108.Text =   Label109.Text =  Label110.Text = "";
				Label111.Text = Label112.Text = Label113.Text = Label114.Text = Label115.Text =  Label116.Text =  Label117.Text =  Label118.Text =   Label119.Text =  Label120.Text = "";
				Label121.Text = Label122.Text = Label123.Text = Label124.Text = Label125.Text =  Label126.Text =  Label127.Text =  Label128.Text =   Label129.Text =  Label130.Text = "";
				Label131.Text = Label132.Text = Label133.Text = Label134.Text = Label135.Text =  Label136.Text =  Label137.Text =  Label138.Text =   Label139.Text =  Label140.Text = "";
				Label141.Text = Label142.Text = Label143.Text = Label144.Text = Label145.Text =  Label146.Text =  Label147.Text =  Label148.Text =   Label149.Text =  Label150.Text = "";
				Label151.Text = Label152.Text = Label153.Text = Label154.Text = Label155.Text =  Label156.Text =  Label157.Text =  Label158.Text =   Label159.Text =  Label160.Text = "";
		}

		private void search()
		{

			totalClear();

			Month = ddlYear.SelectedItem.Value;
			Day = ddlMon.SelectedItem.Value;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			boolAssay = true;
			boolDelevery = true;

			string str1 = @"select isnull(Max(AssayCumulativeQuantity),0) as Assay From LS_HT where [Year] = @year and [Month] = @month";
			SqlCommand comm = new SqlCommand(str1,conn);
			
			comm.Parameters.Add("@year",SqlDbType.Int).Value = ddlYear.SelectedItem.Value;
			comm.Parameters.Add("@month",SqlDbType.Int).Value = ddlMon.SelectedItem.Value;


			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				Assay.Value = dr1["Assay"].ToString().Trim();				
			}
			dr1.Close();


			comm.Parameters.Clear();



			string str2 = @"select isnull(Max(DeleveryCumulativeQuantity),0) as Delevery From LS_HT where [Year] = @year and [Month] = @month";
			SqlCommand comm2 = new SqlCommand(str2,conn);
			
			comm2.Parameters.Add("@year",SqlDbType.Int).Value = ddlYear.SelectedItem.Value;
			comm2.Parameters.Add("@month",SqlDbType.Int).Value = ddlMon.SelectedItem.Value;


			SqlDataReader dr2 = comm2.ExecuteReader();
			while(dr2.Read())
			{
				Delevery.Value = dr2["Delevery"].ToString().Trim();
			}
			dr2.Close();


			comm2.Parameters.Clear();
			
			string str = @"select [Division], [Year], [Month], [Day],[AssayQuantity], [AssayCumulativeQuantity], [DeleveryQuantity], [DeleveryCumulativeQuantity] From LS_HT where [Year] = @year and [Month] = @month order by [RegisterDate] asc";
			SqlCommand Cmd = new SqlCommand(str, conn);
			
			Cmd.Parameters.Add("@year",SqlDbType.Int).Value = ddlYear.SelectedItem.Value;
			Cmd.Parameters.Add("@month",SqlDbType.Int).Value = ddlMon.SelectedItem.Value;
			
			//조립이면 조립에 누적하고
			//출하면 출하에 누적해야 함		
			
			SqlDataReader dr = Cmd.ExecuteReader();
			while(dr.Read())
			{
				switch(dr["Day"].ToString().Trim())
				{
					case "1":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label1.Text = dr["AssayQuantity"].ToString().Trim();
							Label11.Text = dr["AssayCumulativeQuantity"].ToString().Trim();
							if(int.Parse(Assay.Value.Trim()) == int.Parse(dr["AssayCumulativeQuantity"].ToString().Trim()))
								AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "1");
							else
							{
								int a = int.Parse(dr["AssayCumulativeQuantity"].ToString().Trim()) + int.Parse(Assay.Value.Trim());
								AssayPrintList(a.ToString().Trim(), "1");
							}
							boolAssay =true;
						}
						else
						{
							Label21.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label31.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();
							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "1");
							boolDelevery = true;
						}

						break;
					case "2":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label2.Text = dr["AssayQuantity"].ToString().Trim();
							Label12.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "2");
							boolAssay =true;
						}
						else
						{
							Label22.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label32.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "2");
							boolDelevery = true;
						}

						break;
					case "3":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label3.Text = dr["AssayQuantity"].ToString().Trim();
							Label13.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "3");
							boolAssay =true;
						}

						else
						{
							Label23.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label33.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "3");
							boolDelevery = true;
						}
						break;
					case "4":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label4.Text = dr["AssayQuantity"].ToString().Trim();
							Label14.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "4");
							boolAssay =true;
						}
						else
						{
							Label24.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label34.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "4");
							boolDelevery = true;
						}
						break;
					case "5":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label5.Text = dr["AssayQuantity"].ToString().Trim();
							Label15.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "5");
							boolAssay =true;
						}
						else
						{
							Label25.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label35.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "5");
							boolDelevery = true;
						}
						break;
					case "6":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label6.Text = dr["AssayQuantity"].ToString().Trim();
							Label16.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "6");
							boolAssay =true;
						}
						else
						{
							Label26.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label36.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "6");
							boolDelevery = true;
						}
						break;
					case "7":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label7.Text = dr["AssayQuantity"].ToString().Trim();
							Label17.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "7");
							boolAssay =true;
						}
						else
						{
							Label27.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label37.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "7");
							boolDelevery = true;
						}
						break;
					case "8":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label8.Text = dr["AssayQuantity"].ToString().Trim();
							Label18.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "8");
							boolAssay =true;
						}
						else
						{
							Label28.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label38.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "8");
							boolDelevery = true;
						}
						break;
					case "9":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label9.Text = dr["AssayQuantity"].ToString().Trim();
							Label19.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "9");
							boolAssay =true;
						}

						else
						{
							Label29.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label39.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "9");
							boolDelevery = true;
						}

						break;
					case "10":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label10.Text = dr["AssayQuantity"].ToString().Trim();
							Label20.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();						

							AssayPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "10");
							boolAssay =true;
						}
						else
						{
							Label30.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label40.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "10");
							boolDelevery = true;
						}
						break;
					case "11":	
						if(dr["Division"].ToString().Trim() == "True")
						{			
							Label41.Text = dr["AssayQuantity"].ToString().Trim();
							Label51.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "11");
							boolAssay =true;
						}
						else
						{
							Label61.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label71.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "11");
							boolDelevery = true;
						}
						break;
					case "12":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label42.Text = dr["AssayQuantity"].ToString().Trim();
							Label52.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "12");
							boolAssay =true;
						}
						else
						{
							Label62.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label72.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "12");
							boolDelevery = true;
						}
						break;
					case "13":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label43.Text = dr["AssayQuantity"].ToString().Trim();
							Label53.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "13");
							boolAssay =true;
						}
						else
						{
							Label63.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label73.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "13");
							boolDelevery = true;
						}
						break;
					case "14":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label44.Text = dr["AssayQuantity"].ToString().Trim();
							Label54.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "14");
							boolAssay =true;
						}
						else
						{
							Label64.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label74.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "14");
							boolDelevery = true;
						}
						break;
					case "15":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label45.Text = dr["AssayQuantity"].ToString().Trim();
							Label55.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "15");
						}
						else
						{
							Label65.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label75.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "15");
							boolDelevery = true;
						}
						break;
					case "16":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label46.Text = dr["AssayQuantity"].ToString().Trim();
							Label56.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "16");
							boolAssay =true;
						}
						else
						{
							Label66.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label76.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "16");
							boolDelevery = true;
						}
						break;
					case "17":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label47.Text = dr["AssayQuantity"].ToString().Trim();
							Label57.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "17");
							boolAssay =true;
						}
						else
						{
							Label67.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label77.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "17");
							boolDelevery = true;
						}
						break;
					case "18":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label48.Text = dr["AssayQuantity"].ToString().Trim();
							Label58.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "18");
							boolAssay =true;
						}
						else
						{
							Label68.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label78.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "18");
							boolDelevery = true;
						}
						break;
					case "19":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label49.Text = dr["AssayQuantity"].ToString().Trim();
							Label59.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "19");
							boolAssay =true;
						}
						else
						{
							Label69.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label79.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "19");
							boolDelevery = true;
						}
						break;
					case "20":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label50.Text = dr["AssayQuantity"].ToString().Trim();
							Label60.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "20");
							boolAssay =true;
						}
						else
						{
							Label70.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label80.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "20");
							boolDelevery = true;
						}
						break;
					case "21":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label81.Text = dr["AssayQuantity"].ToString().Trim();
							Label91.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "21");
							boolAssay =true;
						}
						else
						{
							Label101.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label111.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "21");
							boolDelevery = true;
						}
						break;
					case "22":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label82.Text = dr["AssayQuantity"].ToString().Trim();
							Label92.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "22");
							boolAssay =true;
						}
						else
						{
							Label102.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label112.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "22");
							boolDelevery = true;
						}
						break;
					case "23":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label83.Text = dr["AssayQuantity"].ToString().Trim();
							Label93.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "23");
							boolAssay =true;
						}
						else
						{
							Label103.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label113.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "23");
							boolDelevery = true;
						}
						break;
					case "24":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label84.Text = dr["AssayQuantity"].ToString().Trim();
							Label94.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "24");
							boolAssay =true;
						}
						else
						{
							Label104.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label114.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "24");
							boolDelevery = true;
						}
						break;
					case "25":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label85.Text = dr["AssayQuantity"].ToString().Trim();
							Label95.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "25");
							boolAssay =true;
						}
						else
						{
							Label105.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label115.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "25");
							boolDelevery = true;
						}
						break;
					case "26":
						if(dr["Division"].ToString().Trim() == "True")
						{				
							Label86.Text = dr["AssayQuantity"].ToString().Trim();
							Label96.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "26");
							boolAssay =true;
						}

						Label106.Text = dr["DeleveryQuantity"].ToString().Trim();
						Label116.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

						SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "26");
						boolDelevery = true;

						break;
					case "27":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label87.Text = dr["AssayQuantity"].ToString().Trim();
							Label97.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "27");
							boolAssay =true;
						}
						else
						{
							Label107.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label117.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "27");
							boolDelevery = true;
						}
						break;
					case "28":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label88.Text = dr["AssayQuantity"].ToString().Trim();
							Label98.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "28");
							boolAssay =true;
						}
						else
						{
							Label108.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label118.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "28");
							boolDelevery = true;
						}
						break;
					case "29":
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label89.Text = dr["AssayQuantity"].ToString().Trim();
							Label99.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "29");
							boolAssay =true;
						}
						else
						{
							Label109.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label119.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "29");
							boolDelevery = true;
						}
						break;
					case "30":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label90.Text = dr["AssayQuantity"].ToString().Trim();
							Label100.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "30");
							boolAssay =true;
						}
						else
						{
							Label110.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label120.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "30");
							boolDelevery = true;
						}
						break;
					case "31":	
						if(dr["Division"].ToString().Trim() == "True")
						{
							Label121.Text = dr["AssayQuantity"].ToString().Trim();
							Label131.Text = dr["AssayCumulativeQuantity"].ToString().Trim();

							AssayPrintList(dr["AssayCumulativeQuantity"].ToString().Trim(), "31");
							boolAssay =true;
						}
						else
						{
							Label141.Text = dr["DeleveryQuantity"].ToString().Trim();
							Label151.Text = dr["DeleveryCumulativeQuantity"].ToString().Trim();

							SendPrintList(dr["DeleveryCumulativeQuantity"].ToString().Trim(), "31");
							boolDelevery = true;
						}
						break;

				}

			}
			dr.Close();

			Cmd.Parameters.Clear();


			conn.Close();
		}

		/// <summary>
		/// 출하 등록
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRegistration_Click(object sender, System.EventArgs e)
		{
			if(wdcDeliveryDate.Text.Trim() == "" || txtDeliveryQuantity.Text.Trim() == "" || txtDeliveryQuantity.Text.Trim() == "0")
			{
				RegisterStartupScript("","<script>alert('출하날짜와 출하수량을 확인하세요!');</script>");
			}
			else
			{
				boolDelevery = true;
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string str = @"Insert  into LS_HT ([Division], [Year], [Month], [Day], [DeleveryQuantity], [DeleveryCumulativeQuantity], [RegisterDate] ) 
						values (@division, @year, @month, @day,@deleveryQuantity, @deleveryCumulativeQuantity, @date) ";
				SqlCommand comm = new SqlCommand(str,conn);

				comm.Parameters.Add("@division",SqlDbType.Bit).Value = 0;
				comm.Parameters.Add("@year",SqlDbType.Int).Value = wdcDeliveryDate.Text.Substring(0,4).Trim();
				comm.Parameters.Add("@month",SqlDbType.Int).Value = wdcDeliveryDate.Text.Substring(5,2).Trim();
				comm.Parameters.Add("@day",SqlDbType.Int).Value = wdcDeliveryDate.Text.Substring(8,2).Trim();
				comm.Parameters.Add("@deleveryQuantity",SqlDbType.Decimal).Value = int.Parse(txtDeliveryQuantity.Text.Trim());
				comm.Parameters.Add("@deleveryCumulativeQuantity",SqlDbType.Decimal).Value = int.Parse(txtDeliveryQuantity.Text.Trim()) + int.Parse(Delevery.Value.Trim());
				//RegisterDate
				comm.Parameters.Add("@date",SqlDbType.DateTime).Value = wdcDeliveryDate.Text.Trim();
				comm.ExecuteNonQuery();

				Delevery.Value = int.Parse(txtDeliveryQuantity.Text.Trim()) + int.Parse(Delevery.Value.Trim()).ToString().Trim();
				

				search();	
			}

		
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(wdcAssayDate.Text.Trim() =="" || txtAssayQuantity.Text.Trim() == "" || txtAssayQuantity.Text.Trim() == "0")
			{
				RegisterStartupScript("","<script>alert('조립일자와 조립수량을 확인하세요!');</script>");
			}
			else
			{
				boolAssay = true;
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string str = @"Insert  into LS_HT ([Division], [Year], [Month], [Day],[AssayQuantity], [AssayCumulativeQuantity], [RegisterDate]) 
							values (@division, @year, @month, @day,@AssayQuantity, @AssayCumulativeQuantity, @date) ";
				SqlCommand comm = new SqlCommand(str,conn);

				comm.Parameters.Add("@division",SqlDbType.Bit).Value = 1;
				comm.Parameters.Add("@year",SqlDbType.Int).Value = wdcAssayDate.Text.Substring(0,4).Trim();
				comm.Parameters.Add("@month",SqlDbType.Int).Value = wdcAssayDate.Text.Substring(5,2).Trim();
				comm.Parameters.Add("@day",SqlDbType.Int).Value = wdcAssayDate.Text.Substring(8,2).Trim();
				comm.Parameters.Add("@AssayQuantity",SqlDbType.Decimal).Value = int.Parse(txtAssayQuantity.Text.Trim());
				comm.Parameters.Add("@AssayCumulativeQuantity",SqlDbType.Decimal).Value = int.Parse(txtAssayQuantity.Text.Trim()) + int.Parse(Assay.Value.Trim());
				comm.Parameters.Add("@date",SqlDbType.DateTime).Value = wdcAssayDate.Text.Trim();
				comm.ExecuteNonQuery();

				Assay.Value = int.Parse(txtAssayQuantity.Text.Trim()) + int.Parse(Assay.Value.Trim()).ToString().Trim();

				search();	
			}
		
		}
	}
}
