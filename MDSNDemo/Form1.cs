using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;

namespace MDSNDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AtualizaButton_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();
            string pagina = wc.DownloadString("https://social.msdn.microsoft.com/forums/pt-BR/home?sort=lastpostdesc");


            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(pagina);
            dataGridView1.Rows.Clear();

            string id = string.Empty;
            string link = string.Empty;
            string titulo = string.Empty;
            string postagem = string.Empty;
            string exibicao = string.Empty; 
            string resposta = string.Empty;
            

            foreach( HtmlNode node in htmlDocument.GetElementbyId("threadList").ChildNodes)
            {
                if (node.Attributes.Count > 0)
                {
                    id = node.Attributes["data-threadid"].Value;
                    link = "https://social.msdn.microsoft.com/forums/pt-BR/" + id;
                    titulo = node.Descendants().First(x => x.Id.Equals("threadTitle_" + id)).InnerText;
                    postagem = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("lastpost")).InnerText.Replace("\n", "").Replace("  ", ""));
                    exibicao = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("viewcount")).InnerText);
                    resposta = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("replycount")).InnerText);

                    if(!string.IsNullOrEmpty(titulo))                   
                        dataGridView1.Rows.Add(titulo, postagem, exibicao, resposta, link);                 
                }
            }
        }

       // private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
       //     if (e.ColumnIndex == )
       //         Process.Start(new ProcessStartInfo(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
       // }
    }
}