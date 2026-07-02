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
//using Microsoft.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebNavigator;

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// Bom1에 대한 요약 설명입니다.
	/// </summary>
	public class Bom1 : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebNavigator.UltraWebTree UltraWebTree1;

		static Stack st = new Stack(); 
		static Stack st1 = new Stack(); 
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			string ItemNum = Request.QueryString["ItemNum"];	
			Decimal need = 1;
			
			
			Node node = new Node();
			node = Node_Add(need, ItemNum, node);
			UltraWebTree1.Nodes.Add(node);
		
		}




		private Node Node_Add(Decimal need , string txt, Node Collection)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			Decimal cou = need;
			//매개변수로 넘어온 모품목번호로 품목명을 찾아 노드의 텍스트에 입력.
			//string str_Name = "Select ItemName,ItemNum from II_MT where ItemNum = @num and RecodingState = 1 order by ItemNum desc";
			string str_Name = "Select ItemName,ItemNum from II_MT where ItemNum = @num and RecodingState = 1 order by ItemNum";
			SqlCommand com_Name = new SqlCommand(str_Name,conn);
			SqlDataAdapter ad_Name = new SqlDataAdapter(com_Name);
			ad_Name.SelectCommand.Parameters.Add("@num",SqlDbType.VarChar).Value = txt;
			DataSet ds_Name = new DataSet();
			ad_Name.Fill(ds_Name);
			if(ds_Name.Tables[0].Rows.Count > 0)
			{
				Collection.Text = ds_Name.Tables[0].Rows[0]["ItemName"].ToString() + "(" + ds_Name.Tables[0].Rows[0]["ItemNum"].ToString() + ")(" + cou + ")";
			}
			
			
			//넘어온 텍스트 즉...모품목번호를 가지고 자품번호가(하위노드)이 있는지 검색함.
			string str_sql = "Select ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator from IOI_MT where RecodingState = 1 and ParentItemNum = @num Order by ChildItemNum desc";
			SqlCommand comread = new SqlCommand(str_sql,conn);
			comread.Parameters.Add("@num",SqlDbType.VarChar).Value = txt;
			SqlDataReader reader = comread.ExecuteReader();
			


			//자품번호(하위노드)가 있을 시...
			if(reader.FieldCount != 0)
			{
				//count는 검색한후 검색된 레코드의 개수를 가지는 변수.
				int count = 0;
				while(reader.Read())
				{
					//검색된 자품번호(하위노드)들을 스택에 넣음(Push)함.
					st.Push(reader["ChildItemNum"].ToString());
					cou = need * (Decimal.Parse(reader["NeedQuantityNumerator"].ToString())/Decimal.Parse(reader["NeedQuantityDenominator"].ToString()));
			
					st1.Push(cou.ToString());
					
					cou = need;
					count++;
				}
				reader.Close();
				conn.Close();

				//하위노드로 만들어질 것들의 갯수만큼 반복하여 노드를 만듦.
				string nodename;
				for(int i=0;i < count ;i++ )
				{
					Node node = new Node();
					//하위노드를 만들기 위해 넘겨줄 품목번호를 스택에서 꺼냄.
					nodename = st.Pop().ToString();
					cou = Decimal.Parse(st1.Pop().ToString());
					//다시 새 노드를 만들어 재귀적호출을 함.
					Collection.Nodes.Add(Node_Add(cou, nodename,node));
					
				}
			}
			else
			{
				conn.Close();
				//만약 하위노드가 없을경우...
				string nodename;
				if(st.Count > 0)
				{
					Node node = new Node();
					//스택에서 다음 자품명을 가지고 현재 매개변수로 넘어온 트리노드 객체를 재귀적 호출.
					nodename = st.Pop().ToString();
					cou = Decimal.Parse(st1.Pop().ToString());
					Collection.Nodes.Add(Node_Add(cou, nodename, node));
				}
			}
								
			return Collection;
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

		private void Button1_Click(object sender, System.EventArgs e)
		{			
			Response.Write("<script language=javascript>");
			Response.Write("window.close()");
			Response.Write("</script>");
		}
	}
}
