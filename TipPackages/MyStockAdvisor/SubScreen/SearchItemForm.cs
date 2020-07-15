using MetroFramework.Forms;
using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace MyStockAdvisor
{
    public partial class SearchItemForm : MetroForm // MetroSetForm
    {
        public SearchItemForm()
        {
            InitializeComponent();
        }

        private void MtlBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;


            MainForm main = new MainForm();
            main.Location = this.Location;
            main.ShowDialog();

            this.Close();
        }

        private void SearchItemForm_Load(object sender, EventArgs e)
        {
            DgvStocks.Font = new Font(@"NanumGothic", 9, FontStyle.Regular);
        }

        /// <summary>
        /// 검색버튼 클릭시 처리할 것
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            WebClient wc = null;
            XmlDocument doc = null;

            wc = new WebClient() { Encoding = Encoding.UTF8 };
            doc = new XmlDocument();

            StringBuilder str = new StringBuilder();
            str.Append("http://api.seibro.or.kr/openapi/service/StockSvc/getStkIsinByNmN1");
            str.Append("?serviceKey=qzyS5rrq0D4aHe%2B8lcYGp0LCf4hA0XrMG%2BbCDrg2TJMrBhTu82nhMSw71WQsM4CUj1EJc%2B3%2Fo%2BT9KOEZFJihPw%3D%3D"); //인증키
            str.Append("&secnNm=" + TxtSearchItem.Text);//종목명
            str.Append("&pageNo=1");//페이지 수
            str.Append("&numOfRows=200");//읽어올 데이터 수
            str.Append("&martTpcd=11");//주식시장종류 : 11은 유가증권시장
            
            string xml = wc.DownloadString(str.ToString());
            doc.LoadXml(xml);

            XmlElement root = doc.DocumentElement;
            XmlNodeList items = doc.GetElementsByTagName("item");

            DgvStocks.Rows.Clear();

            foreach (XmlNode item in items)
            {
                DgvStocks.Rows.Add(item["isin"].InnerText, item["issuDt"].InnerText, 
                    item["korSecnNm"].InnerText, item["secnKacdNm"].InnerText, item["shotnIsin"].InnerText);
            }

            DgvStocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }
}
