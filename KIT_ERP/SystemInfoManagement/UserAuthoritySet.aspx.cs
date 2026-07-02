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
using System.Data.SqlClient;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// UserAuthoritySet에 대한 요약 설명입니다.
	/// </summary>
	public class UserAuthoritySet : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label searchTitle;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			Search();
		}

		private void Search()
		{
			string Module = DropDownList1.SelectedItem.Value.Trim();

			string str = "";
			if(Module == "")
			{
				str = @"Select [Index], Title as 페이지명, Competence00 as [00],
				Competence01 as [01],Competence02 as [02],Competence03 as [03],Competence04 as [04],
				Competence05 as [05],Competence06 as [06],Competence07 as [07],Competence08 as [08],
				Competence09 as [09],Competence10 as [10],Competence11 as [11],Competence12 as [12],
				Competence13 as [13],Competence14 as [14],Competence15 as [15],Competence16 as [16],
				Competence17 as [17],Competence18 as [18],Competence19 as [19],Competence20 as [20] 
				From CT_T order by Num";
			}
			else
			{
				str = @"Select [Index], Title as 페이지명, Competence00 as [00],
				Competence01 as [01],Competence02 as [02],Competence03 as [03],Competence04 as [04],
				Competence05 as [05],Competence06 as [06],Competence07 as [07],Competence08 as [08],
				Competence09 as [09],Competence10 as [10],Competence11 as [11],Competence12 as [12],
				Competence13 as [13],Competence14 as [14],Competence15 as [15],Competence16 as [16],
				Competence17 as [17],Competence18 as [18],Competence19 as [19],Competence20 as [20] 
				From CT_T Where [Group] = @module order by Num";
			}
//			string str = @"Select Num, Title as 페이지명, Competence00 as [00],
//				Competence01 as [01],Competence02 as [02],Competence03 as [03],Competence04 as [04],
//				Competence05 as [05],Competence06 as [06],Competence07 as [07],Competence08 as [08],
//				Competence09 as [09],Competence10 as [10],Competence11 as [11],Competence12 as [12],
//				Competence13 as [13],Competence14 as [14],Competence15 as [15],Competence16 as [16],
//				Competence17 as [17],Competence18 as [18],Competence19 as [19],Competence20 as [20] 
//				From CT_T Where [Group] Like @module order by Num";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			SqlCommand comm = new SqlCommand(str,conn);

			comm.Parameters.Add("@module",SqlDbType.VarChar).Value = Module;

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();



			UltraWebGrid1.Width = 800;
			UltraWebGrid1.Columns[0].Hidden = true;
			UltraWebGrid1.Columns[0].Width = 0;
			UltraWebGrid1.Columns[1].Width = 82;
			
			for(int a = 2; a < 23 ; a++)
			{
				UltraWebGrid1.Columns[a].Width = 34;
			}
		}

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			
			for(int a = 0; a < UltraWebGrid1.Rows.Count; a++)
			{
				string strSQL = @"Update CT_T Set Competence00 = @a00, 
				Competence01 = @a01, Competence02 = @a02, Competence03 = @a03, Competence04 = @a04, 
				Competence05 = @a05, Competence06 = @a06, Competence07 = @a07, Competence08 = @a08,	
				Competence09 = @a09, Competence10 = @a10, Competence11 = @a11, Competence12 = @a12,	
				Competence13 = @a13, Competence14 = @a14, Competence15 = @a15, Competence16 = @a16,	
				Competence17 = @a17, Competence18 = @a18, Competence19 = @a19, Competence20 = @a20  
				where [Index] = @index";
								
				SqlCommand comm = new SqlCommand(strSQL,conn);
				

				
				comm.Parameters.Add("@a00",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("00").Value));
				comm.Parameters.Add("@a01",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("01").Value));
				comm.Parameters.Add("@a02",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("02").Value));
				comm.Parameters.Add("@a03",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("03").Value));
				comm.Parameters.Add("@a04",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("04").Value));
				comm.Parameters.Add("@a05",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("05").Value));
				comm.Parameters.Add("@a06",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("06").Value));
				comm.Parameters.Add("@a07",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("07").Value));
				comm.Parameters.Add("@a08",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("08").Value));
				comm.Parameters.Add("@a09",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("09").Value));
				comm.Parameters.Add("@a10",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("10").Value));
				comm.Parameters.Add("@a11",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("11").Value));
				comm.Parameters.Add("@a12",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("12").Value));
				comm.Parameters.Add("@a13",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("13").Value));
				comm.Parameters.Add("@a14",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("14").Value));
				comm.Parameters.Add("@a15",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("15").Value));
				comm.Parameters.Add("@a16",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("16").Value));
				comm.Parameters.Add("@a17",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("17").Value));
				comm.Parameters.Add("@a18",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("18").Value));
				comm.Parameters.Add("@a19",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("19").Value));
				comm.Parameters.Add("@a20",Convert.ToBoolean(UltraWebGrid1.Rows[a].Cells.FromKey("20").Value));
				
				comm.Parameters.Add("@Index",int.Parse(UltraWebGrid1.Rows[a].Cells.FromKey("Index").Value.ToString()));
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
			}

			Search();
		}
	}
}
