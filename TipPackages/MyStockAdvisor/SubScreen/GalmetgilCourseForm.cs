using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MyStockAdvisor
{
    public partial class GalmetgilCourseForm : MetroForm // MetroSetForm
    {
        public GalmetgilCourseForm()
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
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            /**
                http://apis.data.go.kr/6260000/BusanGalmaetGilService/getGalmaetGilInfo
                ?serviceKey=qzyS5rrq0D4aHe%2B8lcYGp0LCf4hA0XrMG%2BbCDrg2TJMrBhTu82nhMSw71WQsM4CUj1EJc%2B3%2Fo%2BT9KOEZFJihPw%3D%3D
                &pageNo=1
                &numOfRows=10
                &resultType=json&
             */
            StringBuilder str = new StringBuilder();
            str.Append("http://apis.data.go.kr/6260000/BusanGalmaetGilService/getGalmaetGilInfo");
            str.Append("?serviceKey=qzyS5rrq0D4aHe%2B8lcYGp0LCf4hA0XrMG%2BbCDrg2TJMrBhTu82nhMSw71WQsM4CUj1EJc%2B3%2Fo%2BT9KOEZFJihPw%3D%3D"); //인증키
            str.Append("&pageNo=1");//페이지 수
            str.Append("&numOfRows=10");//읽어올 데이터 수
            str.Append("&resultType=json");//결과타입 json
            
            string result = wc.DownloadString(str.ToString());
            JObject obj = JObject.Parse(result);
            //dynamic json = JsonConvert.DeserializeObject(result);
            var info = obj.SelectToken("getGalmaetGilInfo");
            //var items = obj.SelectToken("item");
            var items = JArray.Parse(obj.SelectToken("getGalmaetGilInfo.item").ToString());

            foreach (var item in items)
            {
                // TODO
                System.Diagnostics.Debug.WriteLine(item);
            }
            DgvStocks.Rows.Clear();

            //foreach (XmlNode item in items)
            //{
            //    DgvStocks.Rows.Add(item["isin"].InnerText, item["issuDt"].InnerText, 
            //        item["korSecnNm"].InnerText, item["secnKacdNm"].InnerText, item["shotnIsin"].InnerText);
            //}

            DgvStocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }
}
