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

namespace KIT_ERP.Homepage
{
	/// <summary>
	/// Image에 대한 요약 설명입니다.
	/// </summary>
	public class Image : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton2;
		protected System.Web.UI.WebControls.ImageButton ImageButton3;
		protected System.Web.UI.WebControls.ImageButton ImageButton4;
		protected System.Web.UI.WebControls.ImageButton ImageButton5;
		protected System.Web.UI.WebControls.ImageButton ImageButton6;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
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
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.ImageButton2.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton2_Click);
			this.ImageButton3.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton3_Click);
			this.ImageButton4.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton4_Click);
			this.ImageButton5.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton5_Click);
			this.ImageButton6.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton6_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//회사소개
		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'CompanyIntroduce.aspx';</script>");
		}

		//홈으로
		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'Contents.aspx';</script>");
		}

		//사업제품개요
		private void ImageButton3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'BusinessProduct.aspx';</script>");
		}

		//자유게시판
		private void ImageButton4_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'FreeBoard.aspx';</script>");
		}

		//자료실
		private void ImageButton5_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'PDS.aspx';</script>");	
		}

		//방명록
		private void ImageButton6_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'GuestBook.aspx';</script>");
		}

	}
}
