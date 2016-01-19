using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMBIOS_SKUNo_Gen
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        const int QUEST_SIZE = 6;
        int QuestionScore;
        List<Question> questions = new List<Question>();
        List<string> ansLabels;
        Stack<int> undoStack = new Stack<int>();
        List<List<Selection>> buttonsLabels;
        public MainWindow()
        {
            InitializeComponent();

            buttonsLabels = StringData.getButtonData;
            ansLabels = StringData.getAnsData;

            List<string> questionLabels = StringData.getQuestionData;
      
            for (int i = 0; i < QUEST_SIZE; i++)      
                questions.Add(new Question(i, questionLabels[i], buttonsLabels[i]));
           
            this.QuestionScore = 0;
            this.QLabel.Content = questions[this.QuestionScore].QString;
            this.LButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.left].Label;
            this.RButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.right].Label;
            //this.MButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.mid].Label;
            this.MButton.Visibility = Visibility.Hidden;
            this.M2Button.Visibility = Visibility.Hidden;
            this.UndoButton.IsEnabled = false;
        }

        private void ButtonCheck(BUTTON_POSIOION pos, string ansString)
        {
            if (BUTTON_POSIOION.undo == pos)
            {
                if (undoStack.Count > 0)
                {
                    this.QuestionScore -= undoStack.Pop();//回歸問題tree的位置
                    for (int i = 0; i < 2; i++)//把Q跟A移除掉
                        this.QuestionHistroyList.Items.RemoveAt(this.QuestionHistroyList.Items.Count - 1);
                    this.UndoButton.IsEnabled = undoStack.Count > 0;//防呆 stack的size要大於0才能點
                    this.LButton.Visibility = Visibility.Visible;
                    this.RButton.Visibility = Visibility.Visible;
                }
            }
            else//點下左邊中間右邊的button之後
            {
                int score = 1;
                StringData.ModifySKU[questions[this.QuestionScore].ButtonsLabel[(int)pos].MethodName](ansString);
                undoStack.Push(score);//執行過的放到stack做紀錄
                this.QuestionHistroyList.Items.Add(this.QLabel.Content);//印出問題
                this.QuestionHistroyList.Items.Add("A:" + ansString);
                this.UndoButton.IsEnabled = true;
                this.QuestionScore += score;
                if (6 == this.QuestionScore)//代表到了結尾
                {
                    this.LButton.Visibility = Visibility.Hidden;
                    this.RButton.Visibility = Visibility.Hidden;
                    //MessageBox.Show(ansLabels[questions[this.QuestionScore].ButtonsLabel[(int)pos].AnsID], "Information");
                    MessageBox.Show("Generating files:\r\n  SKUNoDOS.bat\r\n  SKUNoEFI.nsh\r\n  SKUNoWin.bat\r\n  Config.txt\r\n","Notify");
                    StringData.GenBatFile(this.QuestionHistroyList.Items);
                    return;
                }
                
                

            }
            this.LButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.left].Label;
            this.RButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.right].Label;

            if (questions[this.QuestionScore].ButtonsLabel.Count > 2)
            {
                this.MButton.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.mid].Label;
                this.MButton.Visibility = Visibility.Visible;
                this.M2Button.Content = questions[this.QuestionScore].ButtonsLabel[(int)BUTTON_POSIOION.mid + 1].Label;
                this.M2Button.Visibility = Visibility.Visible;
            }
            else
            {
                this.MButton.Visibility = Visibility.Hidden;
                this.M2Button.Visibility = Visibility.Hidden;

            }
            this.QLabel.Content = questions[this.QuestionScore].QString;
            this.NoteLabel.Content = this.QLabel.Content.ToString().Contains(StringData.AHCI_RAID_STR) ? StringData.NOTE_STR : "";
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonCheck(StringData.ButtonType[((Button)sender).Name], ((Button)sender).Content.ToString());
        }
    }

    public class Selection
    {
        public string Label { get; set; }
        public string MethodName { get; set; }
    }


    public class Question
    {
        public Question(int id, string qString, List<Selection> buttonsLabel)
        {
            this.id = id;
            this.qString = qString;
            this.buttonsLabel = buttonsLabel;
        }
        public int ID { get { return this.id; } }
        public string QString { get { return this.qString; } }
        public List<Selection> ButtonsLabel { get { return this.buttonsLabel; } }

        private int id;
        private string qString;
        private List<Selection> buttonsLabel;
    }
    public enum BUTTON_POSIOION
    {
        left,
        right,
        mid,
        mid2,
        undo
    }
}
