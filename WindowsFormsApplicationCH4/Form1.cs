using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplicationCH4
{
    public partial class MyNote : Form
    {
        public MyNote()
        {
            InitializeComponent();
        }

        private void MyNote_Load(object sender, EventArgs e)
        {

        }

        //==開啟檔案==
        //點選'檔案/開啟'選項後,會出現一個'開啟檔案交談窗(openFileDialog1)',其中只會出現副檔名為txt的純文字檔案<因為在Filter屬性設為*.txt所產生的效果>
        private void 開啟OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)                //判斷是否為純文字檔
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.Default);   //純文字檔為true ==> 
                                                                                                //讀入檔案:使用檔案(File)功能讀取所有的文字(ReadAllText)...
                                                                                                //從openFileDialog1.FileName(選取的)檔案中讀取,而且需使用系統預設(Default)的文字編碼(Encoding)方式, 
                                                                                                //並將讀取到的文字放到textBox1的Text屬性中.
            }
        }


        //==儲存==
        private void 儲存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName == "")                                                     //先判斷openFileDialog1.FileName是否為空, 如果是=>代表使用者並未開啟舊檔,這時就必須執行與另存新檔一樣的程式碼
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)            //判斷是否為純文字檔
                {
                    File.WriteAllText(saveFileDialog1.FileName, textBox1.Text, Encoding.Default);           //純文字檔為true ==> 強制使用者選用新的檔名來進行存檔
                }
            }


            else                                                                                    //再判斷openFileDialog1.FileName是否為空, 如果否=>必須以openFileDialog1.FileName的檔名,存回到原來的檔案                                                     
            {
                File.WriteAllText(openFileDialog1.FileName, textBox1.Text, Encoding.Default);
            }
        }


        //==另存新檔==
        private void 另存新檔AToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)            //判斷是否為純文字檔
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text, Encoding.Default);           //純文字檔為true ==> 強制使用者選用新的檔名來進行存檔
            }
        }


        //==新增==
        private void 新增NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            textBox1.Clear();
        }


        //==結束==
        private void 結束XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //==剪下==
        private void 剪下TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }


        //==複製==
        private void 複製CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }


        //==貼上==
        private void 貼上PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }


        //==全選==
        private void 全選AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }


        //==搜尋取代==
            //開啟'搜尋取代'面板
        private void 搜尋取代_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }
            //關閉'搜尋取代'面板
            private void button3_Click(object sender, EventArgs e)
            {
                panel1.Visible = false;
            }

        //"搜尋"功能
        private void button1_Click(object sender, EventArgs e)
        {
            int P;                                                                              //宣告 整數變數P , 用來存放"被搜尋到字串的"起始位置

            if (textBox1.SelectionLength > 0)                                                   //第一個判斷式: 先判斷目前的textBox1.Text中,有沒有被選取的文字 ( SelectionLength > 0 )
            {
                P = textBox1.Text.IndexOf(textBox2.Text, textBox1.SelectionStart + 1);              //有的話,表示先前已經找到 '目標字串'了, 所以接著必須搜尋"下一個"'目標字串'
            }
            else                                                                                //如果沒有被選取到任何文字( SelectionStart == 0 )
            {
                P = textBox1.Text.IndexOf(textBox2.Text, textBox1.SelectionStart );                 //表示應該是剛開始搜尋, 那就直接用目前的位置( SelectionStart )開始搜尋
            }

            if (P < 0)                                                                          //第二個判斷式: 依據回傳值 P(也就是目標字串的索引位置)作出回應: 
            {
                MessageBox.Show("未發現搜尋字串!!");                                                 //如果找不到搜尋字串時 (P = -1), 就顯示 MessageBox的內容
            }
            else                                                                                //如果找到了搜尋字串,  
            {
                textBox1.SelectionStart = P;                                                        //先將 選取資料範圍的起點(SelectionStart)設定為P
                textBox1.SelectionLength = textBox2.TextLength;                                     //再將 目標字串(textBox2.Text)的長度 設為 所要選取資料的長度(SelectionLength)
                textBox1.Select();                                                                  //最後執行 選取動作(Select) , 然後就可以看到搜尋到的字串被反白了
            }

        }

        //"取代"功能
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = textBox3.Text;                                              //將 textBox1<主視窗>中被選取的文字(SelectedText)用textBox3的內容(Text)取代
        }
        //**************************************************************************************防呆: 加入 只有'想搜尋的字串'才可以取代 的功能


        //拖曳物件的功能[可拖曳視窗]
        int mdx, mdy;                                                       //設定全域變數 mdx, mdy
        private void panel1_MouseDown(object sender, MouseEventArgs e)          //滑鼠按下(準備開始拖曳)的時候,紀錄起點 X與Y 座標  [而 e 是 事件副程式夾帶的參數集合, 每個'事件副程式'都會有這個顯示為e的集合]
        {
            mdx = e.X;                                                          //e.X 表示滑鼠的 X座標
            mdy = e.Y;                                                          //e.Y 表示滑鼠的 Y座標

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)             //先判斷滑鼠左鍵是否按下 [e.Botton指的是滑鼠按鍵, MouseButtons.Left指的是滑鼠左鍵]==>如果滑鼠左鍵是按住的,那麼就該移動物件了
            {                                                                   //事實上只要在程式中改變物件(panel)的Left(X方向)與Top(Y方向)屬性即可
                panel1.Left += e.X - mdx;                                       //每次的 X移動量 是 [終點的e.X 減去 起點的 mdx];
                panel1.Top  += e.Y - mdy;                                       //每次的 Y移動量 是 [終點的e.Y 減去 起點的 mdy];
            }
        }


        //==字型==
        private void 字型CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新加入滑鼠控制項
            FontDialog fontDialog1 = new FontDialog();
            if (fontDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                // RichTextBox.SelectionFont取得目前選擇的文字，並且將FontDialog所設定的字型結果套入
                richTextBox1.SelectionFont = fontDialog1.Font;
            }//滑鼠控制項結束


            fontDialog1.ShowDialog();                                       //先呼叫開啟對話方塊(ShowDialog),使用者選擇(或取消選擇)後將選定的字型與顏色賦予textBox1的字型與顏色即可
            textBox1.Font = fontDialog1.Font;
        }
        //**************************************************************************************防呆: 加入 只有'選擇到的字串'才可以改字型 的功能


        //==顏色==
        private void 顏色OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.ForeColor = colorDialog1.Color;                        //Fore是'前景'的意思,也就是 文字的顏色, 與它相對的就是BackColor(背景顏色)
            //richTextBox1.SelectionColor = colorDialog1.Color; 

        }
        //**************************************************************************************防呆: 加入 只有'選擇到的字串'才可以改顏色 的功能


        //==列印==
        private void 列印PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();                                      //先呼叫開啟列印對話方塊(ShowDialog)
        }

        //==預覽列印==
        private void 預覽列印VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();                               //先呼叫開啟預覽列印對話方塊(ShowDialog)
        }









    }
}
