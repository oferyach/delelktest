using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DelekOPTSimulation
{
    public partial class OrPT : Form
    {
        Main main = null;
        public OrPT(Main _main)
        {
            InitializeComponent();
            main = _main;
        }

        private void SetLines(string l1, string l2, string l3, string l4)
        {
            Line1.Text = l1;
            Line2.Text = l2;
            Line3.Text = l3;
            Line4.Text = l4;
        }



        static string PromptType = "Short";

        public void SetOPTState(States state)
        {
            string image = "";
            string l1 = "";
            string l2 = "";
            string l3 = "";
            string l4 = "";
            HtmlElement a;
            SetLines(" ", " ", " ", " ");
            switch (state)
            {
                case States.SSIdle:
                    SetLines("ברוכים הבאים", "העבר כרטיס", "הקש F3 להגבלת סכום", "English");
                    break;
                case States.FSIdle:
                    SetLines("העבר כרטיס", "F3 הגבלה", "F2 תפריט", "");
                    //clear limits
                    //example of calling Javascript with params
                    //object[] o = new object[1];
                    //o[0]="My application is here";
                    //OPTScreen.Document.InvokeScript("ShowMessage", o);
                    break;
                case States.AskCard:
                    if (main.bLimitDone)
                    {
                        if (main.limitType == "Money")
                            l1 = " הוגבל ל" + main.limit.ToString(" 0.00 ") + "שקלים ";
                        else
                            l1 += " הוגבל ל" + main.limit.ToString(" 0.00 ") + "ליטרים ";
                    }
                    else
                        l1 = "";
                    SetLines("העבר כרטיס", "", "", l1);
                    break;
                case States.ID:
                    image = "ID.jpg";
                    SetLines("הכנס מספר תעודת זהות", "הקש אישור להמשך", "", "");
                    PromptType = "Long";
                    break;
                case States.Plate:
                    image = "Plate.jpg";
                    SetLines("הקש מספר רכב", "הקש אישור להמשך", "", "");
                    PromptType = "Long";
                    break;
                case States.CardCheck:
                    image = "CardCheck.jpg";
                    SetLines("כרטיס בבדיקה", "", "", "");
                    break;
                case States.VISCheking:
                    image = "VISChecking.jpg";
                    SetLines("התקן דלקן בבדיקה", "", "", "");
                    break;
                case States.BlockNoz:

                    image = "BlockNoz.jpg";
                    SetLines("פיה חסומה", "", "", "");
                    break;
                case States.Fueling:

                    //These lines to play the movie
                    //a = OPTScreen.Document.GetElementById("M1");
                    //a.Style = "visibility:visible";
                    //OPTScreen.Document.InvokeScript("StartPlayer", null);

                    if (main.Self.Checked)
                    {
                        image = "AD.jpg";
                        SetLines("דלק זה לא רק דלק", "", "", "");
                    }
                    else
                    {
                        image = "FSAD.jpg";
                        SetLines("יש להציע מבצעים", "", "", "");
                    }

                    break;
                case States.TestFuel:

                    image = "TestFuel.jpg";
                    SetLines("כיול משאבה", "", "", "");
                    break;
                case States.LimitMoney:
                    image = "LimitMoney.jpg";
                    SetLines("הכנס הגבלת כסף", "", "", "");
                    PromptType = "Short";
                    break;
                case States.LimitVolume:
                    image = "LimitVolume.jpg";
                    SetLines("הכנס הגבלת ליטרים", "", "", "");
                    PromptType = "Short";
                    break;
                case States.Receipt:
                    SetLines("מדפיס קבלה", "", "", "");
                    image = "Receipt.jpg";
                    break;
                case States.FSReceipt:
                    SetLines("לסיום הקש אישור", "F3 מכירה", "", "");
                    image = "FSReceipt.jpg";
                    break;
                case States.LiftNoz:

                    image = "LiftNoz.jpg";
                    if (main.bLimitDone)
                    {
                        if (main.limitType == "Money")
                            l1 = " הוגבל ל" + main.limit.ToString(" 0.00 ") + "שקלים ";
                        else
                            l1 += " הוגבל ל" + main.limit.ToString(" 0.00 ") + "ליטרים ";
                    }
                    else
                        l1 = "";
                    SetLines(l1, "הרם ידית והחל לתדלק", " ", " ");
                    break;
                case States.FSMenu1:

                    image = "FMenu1.jpg";  //"MenuAttn.jpg";
                    main.maxmenu = 6;
                    main.menuindex = 1;
                    //MarkMenu(main.menuindex, true);
                    SetLines("F1 עוד", "F2 הפקדה", "F3 מכירה", "F4 קבלה");
                    break;
                case States.FSMenu2:
                    image = "FSMenu2.jpg";
                    SetLines("F1 עוד", "F2 כיול משאבות", "F3 התעלם מדלקן", "F4 ניהול משמרות");
                    break;
                case States.FSMenu3:
                    image = "FSMenu3.jpg";
                    SetLines("F1 עוד", "F2 החזרות", "F3 תשלום עיסקה", "F4 אישור דלקן חריג");
                    break;
                case States.FSMenu4:
                    image = "FSMenu4.jpg";
                    SetLines("", "", "", "F4 חסימת פיה");
                    break;
                case States.ShiftMenu:
                    image = "SHiftMenu.jpg";
                    SetLines("F1 שעון יציאה", "F2 שעון כניסה", "F3 דוח X", "F4 סוף יום");
                    break;
                case States.SafeDrop:
                    image = "SafeDrop.jpg";
                    SetLines("הכנס סכום להפקדה", "הקש אישור להמשך", "", "");
                    break;
                case States.DrySale:
                    image = "DrySale.jpg";
                    l1 = " סכום כולל " + main.totalsale.ToString("0.00");
                    SetLines("הכנס סכום להפקדה", "הקש אישור להמשך", l1, "");
                    break;
                case States.GetProduct:
                    image = "ProductCode.jpg";
                    SetLines("הכנס קוד מוצר", "הקש אישור להמשך", "", "");
                    break;
                case States.GetQunatity:
                    image = "ProductQuntity.jpg";
                    SetLines("הכנס כמות", "הקש אישור להמשך", "", "");
                    break;
                case States.PrintingRecipt:
                    image = "PrintingReceipt.jpg";
                    SetLines("מדפיס קבלה", "", "", "");
                    break;
                case States.TBD:
                    image = "TBD.jpg";
                    SetLines("פעולה לא נתמכת", "", "", "");
                    break;
                case States.Err_WrongProduct:
                    image = "Err_WrongProduct.jpg";
                    SetLines("סוג דלק נבחר", "לא מורשה", "", "");
                    break;
                case States.Err_NozBlocked:
                    image = "Err_NozBlocked.jpg";
                    SetLines("פיה חסומה לתדלוק", "", "", "");
                    break;
                case States.Err_BadIDNum:
                    image = "Err_BadIDNum.jpg";
                    SetLines("שגיאה במספר זהות", "", "", "");
                    break;
                case States.Msg_NozIsBlocked:
                    image = "Msg_NozIsBlocked.jpg";
                    SetLines("הפיה נחסמה", "", "", "");
                    break;
                case States.NoService:
                    image = "NoService.jpg";
                    SetLines("אין שרות", "פנה לעמדה אחרת", "", "");
                    break;
                case States.CashCredit:
                    image = "CashCredit.jpg";
                    SetLines("בחר אמצעי תשלום", "F2 מזומן", "F3 אשראי", "");
                    break;
                case States.Lang:
                    image = "Lang.jpg";
                    SetLines("F1 English", "F2 Francais", "", "");
                    break;
                case States.BadCard:
                    image = "BadCard.jpg";
                    SetLines("כרטיס לא מאושר", "", "", "");
                    break;
                case States.NoProduct:
                    image = "NoProduct.jpg";
                    SetLines("לא נמצא מוצר", "בקוד זה", "", "");
                    break;
                case States.RequestAttn:
                    image = "RequestAttn.jpg";
                    SetLines("העבר כרטיס מוכרן", "", "", "");
                    break;
                case States.RequestMan:
                    image = "RequestMan.jpg";
                    SetLines("העבר כרטיס מנהל", "", "", "");
                    break;

                case States.XReport:
                    image = "XReport.jpg";
                    l1 = " מזומן " + 1550.ToString(" 0.00 ");
                    l2 = " אשראי " + 450.ToString("0.00");

                    SetLines("מוכרן רמי יולזרי", l1, l2, " ");

                    break;
                case States.ClockIn:
                    image = "ClockIn.jpg";
                    SetLines("", "", "", "");
                    break;
                case States.ClockOut:
                    image = "ClockOut";
                    SetLines("", "", "", "");
                    break;
                case States.Odometer:
                    image = "Odometer.jpg";
                    SetLines("הכנס מד מרחק", "הקש אישור להמשך", "", "");
                    break;
                case States.PINCode:
                    image = "PINCode.jpg";
                    SetLines("הכנס קוד סודי", "הקש אישור להמשך", "", "");
                    break;
                case States.DropConfirmation:
                    image = "DropConfirmation.jpg";

                    l1 = " הפקדה" + main.dropAmount.ToString(" 0.00 ");

                    SetLines("מוכרן רמי יולזרי", l1, " ", " ");
                    break;
                case States.DryConfirm:
                    image = "DryConfirm.jpg";

                    string itemname = "";
                    double itemprice = 0.0;

                    Config.GetItemByCode(main.drycode, ref itemname, ref itemprice);

                    //Items item = main.items.Find(x => x.Code == main.drycode);


                    l1 = itemname + " כמות " + main.dryqunatity.ToString("0") + "  ";
                    //l2 = " כמות " + dryqunatity.ToString("0");
                    l2 = " מחיר ליחידה " + itemprice.ToString("0.00");
                    l3 = " מחיר כולל " + (itemprice * main.dryqunatity).ToString("0.00");

                    SetLines(l1, l2, l3, "");

                    break;

            }

            try
            {
                
            }
            catch (Exception exp)
            {

            }

        }

        //public string indata = "";
        //static string PromptType = "Short";
        

        private string GetSpaces(string promot)
        {
            string s="";
            for (int i = 0; i < 18 - promot.Length; i++)
                s += " ";

            return s;
        }

        public void UpdatePrompt(string prompt)
        {
            if (prompt == "$")
                main.indata = "";

            if (PromptType == "Short")
                Line3.Text = GetSpaces(prompt) + prompt;
            else
                Line3.Text = GetSpaces(prompt) + prompt;
        }

        

        private void F2_Click(object sender, EventArgs e)
        {
            main.KeyIn('B');
        }

        List<States> F1isF1 = new List<States> { States.SSIdle,States.FSIdle };
        List<States> F4isF4 = new List<States> { States.SSIdle, States.FSIdle };

        List<States> Prompts = new List<States> {States.ID, States.Plate, States.PINCode, States.Odometer, States.LimitMoney,
                                                 States.LimitVolume, States.GetProduct, States.GetQunatity, States.SafeDrop};

        private void F1_Click(object sender, EventArgs e)
        {
            //if (F1isF1.Find(x => x==main.state)==main.state)
            //    main.KeyIn('A');
            //else
                main.KeyIn('X');
        }

        private void F3_Click(object sender, EventArgs e)
        {
            main.KeyIn('C');
        }

        private void F4_Click(object sender, EventArgs e)
        {
            //if (F4isF4.Find(x => x == main.state) == main.state)
            //    main.KeyIn('D');
            //else
                main.KeyIn('O');
        }
        private void B1_Click(object sender, EventArgs e)
        {
            if (Prompts.Find(x => x == main.state) == main.state)
                main.KeyIn('1');
            else
                main.KeyIn('A');
        }

        private void B2_Click(object sender, EventArgs e)
        {
            if (Prompts.Find(x => x == main.state) == main.state)
                main.KeyIn('2');
            else
                main.KeyIn('B');
        }

        private void B3_Click(object sender, EventArgs e)
        {
            if (Prompts.Find(x => x == main.state) == main.state)
                main.KeyIn('3');
            else
                main.KeyIn('C');
        }

        private void B4_Click(object sender, EventArgs e)
        {
            if (Prompts.Find(x => x == main.state) == main.state)
                main.KeyIn('4');
            else
                main.KeyIn('D');
        }

        private void BDOT_Click(object sender, EventArgs e)
        {
            main.KeyIn('.');
        }

        private void B5_Click(object sender, EventArgs e)
        {
            main.KeyIn('5');
        }

        private void B6_Click(object sender, EventArgs e)
        {
            main.KeyIn('6');
        }

        private void B7_Click(object sender, EventArgs e)
        {
            main.KeyIn('7');
        }

        private void B8_Click(object sender, EventArgs e)
        {
            main.KeyIn('8');
        }

        private void B9_Click(object sender, EventArgs e)
        {
            main.KeyIn('9');
        }

        private void B0_Click(object sender, EventArgs e)
        {
            main.KeyIn('0');
        }

        private void Bback_Click(object sender, EventArgs e)
        {
            main.KeyIn('K');
        }

        
    }
}
