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
	/// WorkPageMove에 대한 요약 설명입니다.
	/// </summary>
	public class WorkPageMove : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				string Num = Request["Index"].Trim().ToString();
				if(IDFind(Num) == "01 그룹")
					Response.Redirect("WorkDiaryWrite01.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "02 그룹")
					Response.Redirect("WorkDiaryWrite02.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "03 그룹")
					Response.Redirect("WorkDiaryWrite03.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "04 그룹")
					Response.Redirect("WorkDiaryWrite04.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "05 그룹")
					Response.Redirect("WorkDiaryWrite05.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "06 그룹")
					Response.Redirect("WorkDiaryWrite06.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "07 그룹")
					Response.Redirect("WorkDiaryWrite07.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "08 그룹")
					Response.Redirect("WorkDiaryWrite08.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "09 그룹")
					Response.Redirect("WorkDiaryWrite09.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "10 그룹")
					Response.Redirect("WorkDiaryWrite10.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "11 그룹")
					Response.Redirect("WorkDiaryWrite11.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "12 그룹")
					Response.Redirect("WorkDiaryWrite12.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "13 그룹")
					Response.Redirect("WorkDiaryWrite13.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "14 그룹")
					Response.Redirect("WorkDiaryWrite14.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "15 그룹")
					Response.Redirect("WorkDiaryWrite15.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				else if(IDFind(Num) == "16 그룹")
					Response.Redirect("WorkDiaryWrite16.aspx?Num="+Num+"&id="+Session["ID"].ToString().Trim()+"");
				//Authority();
			}
			
		}

		private string IDFind(string Number)
		{
			string WorkDiary = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"Select RegistrationID From WorkReport Where [Index]= @index";
			Cmd.Parameters.Add("@index",Number);
			SqlDataReader dr = Cmd.ExecuteReader();

			string ID = "";
			while(dr.Read())
			{
				ID = dr["RegistrationID"].ToString();
			}
			dr.Close();
			Cmd.Parameters.Clear();

			Cmd.CommandText = "Select WorkDiary From UI_MT where RecodingState = 1 and [ID] = @id";
			Cmd.Parameters.Add("@id",ID);
			SqlDataReader dr1 = Cmd.ExecuteReader();
			while(dr1.Read())
			{
				WorkDiary = dr1["WorkDiary"].ToString();
			}
			dr1.Close();
			Con.Close();
			Cmd.Parameters.Clear();

			return WorkDiary;

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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
