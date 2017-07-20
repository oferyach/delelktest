using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using DelekOPTSimulation;

namespace DelekOPTSimulation
{
    public partial class Invenco : Form
    {
        [ComVisible(true)]
        public class MyScriptManager
        {
            // Variable to store the form of type Form1.
            private Invenco mForm;

            // Constructor.
            public MyScriptManager(Invenco form)
            {
                // Save the form so it can be referenced later.
                mForm = form;
            }

            // This method can also be called from JavaScript.
            public void AnotherMethod(string message)
            {
                MessageBox.Show(message);
            }
        }

        Main main = null;

        public Invenco(Main _main)
        {
            InitializeComponent();
            OPTScreen.ObjectForScripting = new MyScriptManager(this);
            string curDir = Directory.GetCurrentDirectory();
            var url = new Uri(String.Format("file:///{0}/{1}", curDir, "Images/OPTScreen.html"));
            OPTScreen.Navigate(url);
            main = _main;
        }

        private void OPTScreen_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                main.Self_CheckedChanged(null, null);
                OPTScreen.Document.InvokeScript("StopPlayer", null);
                main.SetState(States.SSIdle);
            }
            catch
            {

            }
        }

        private void F1_Click(object sender, EventArgs e)
        {
            main.KeyIn('A'); //F1
        }

        private void F2_Click(object sender, EventArgs e)
        {
            main.KeyIn('B');  //F2
        }

        private void F3_Click(object sender, EventArgs e)
        {
            main.KeyIn('C');  //F3
        }

        private void F4_Click(object sender, EventArgs e)
        {
            main.KeyIn('D'); //F4
        }

        private void B1_Click(object sender, EventArgs e)
        {
            main.KeyIn('1');
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            main.KeyIn('O');
        }

        private void B2_Click(object sender, EventArgs e)
        {
            main.KeyIn('2');
        }

        private void BX_Click(object sender, EventArgs e)
        {
            main.KeyIn('X');  // Cancel
        }

        private void B3_Click(object sender, EventArgs e)
        {
            main.KeyIn('3');
        }

        private void BBack_Click(object sender, EventArgs e)
        {
            main.KeyIn('K'); //back 
        }

        private void BDOT_Click(object sender, EventArgs e)
        {
            main.KeyIn('.'); //dot
        }

        private void B4_Click(object sender, EventArgs e)
        {
            main.KeyIn('4');
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

        private void BH_Click(object sender, EventArgs e)
        {
            main.KeyIn('#');
        }


        public void SetLines(string l1, string l2, string l3, string l4, byte marked)
        {
            HtmlElement a;
            a = OPTScreen.Document.GetElementById("Line1");
            a.InnerHtml = l1;
            if (marked == 1)
                a.SetAttribute("className", "sansserifm");
            else
                a.SetAttribute("className", "sansserif");
            a = OPTScreen.Document.GetElementById("Line2");
            a.InnerHtml = l2;
            if (marked == 2)
                a.SetAttribute("className", "sansserifm");
            else
                a.SetAttribute("className", "sansserif");
            a = OPTScreen.Document.GetElementById("Line3");
            a.InnerHtml = l3;
            if (marked == 3)
                a.SetAttribute("className", "sansserifm");
            else
                a.SetAttribute("className", "sansserif");
            a = OPTScreen.Document.GetElementById("Line4");
            a.InnerHtml = l4;
            if (marked == 4)
                a.SetAttribute("className", "sansserifm");
            else
                a.SetAttribute("className", "sansserif");
        }

        public void SetMenu(string text, int index)
        {
            HtmlElement a = null;

            string mm = "Menu" + index.ToString("0");

            a = OPTScreen.Document.GetElementById(mm);
            if (a != null)
            {
                if (text !="")
                    a.InnerHtml = index.ToString() + " " + text;
                else
                    a.InnerHtml = text;
            }
        }

        
        public void MarkMenu(int index, bool bMark)
        {
            HtmlElement a = null;

            string mm = "Menu" + index.ToString("0");

            
            a = OPTScreen.Document.GetElementById(mm);
            if (a != null)
                if (bMark)
                    a.SetAttribute("className", "sansserifm");
                else
                    a.SetAttribute("className", "sansserif");
        }

        public void ClearMenu()
        {
            for (byte i = 1; i <= 8; i++)
            {
                MarkMenu(i, false);
                SetMenu("", i);
            }
            
        }

        
        public void UpdatePrompt(string prompt)
        {
            HtmlElement a;

            if (prompt == "$")
                main.indata = "";

            if (main.PromptType == "Short")
                a = OPTScreen.Document.GetElementById("SPrompt");
            else
                a = OPTScreen.Document.GetElementById("Prompt");
            a.InnerHtml = prompt;
        }


        public void SetMultiPump(int Pump)
        {
            HtmlElement a = null;

            string mm = "MultiPump";

            a = OPTScreen.Document.GetElementById(mm);
            if (a != null)
            {
                if (Pump == 0)
                    a.InnerHtml = "";
                else
                    a.InnerHtml = " משאבה" + " "+ Pump.ToString();
            }
                    
        }

        public void SetOPTState(States state)
        {
            string image = "";
            string l1 = "";
            string l2 = "";
            string l3 = "";
            string l4 = "";
            HtmlElement a;
            SetLines(" ", " ", " ", " ", 0);
            a = OPTScreen.Document.GetElementById("M1");
            a.Style = "visibility:hidden";
            UpdatePrompt("");
            switch (state)
            {
                case States.SSIdle:
                    if (!main.MultiPump.Checked)
                        image = "SSIdleSinglePump.jpg";
                    else
                        image = "SSIdleMultiPump.jpg";
                    break;
                case States.FSIdle:
                    //select image based on MultiPump
                    if (!main.MultiPump.Checked)                
                        image = "FSIdle.jpg";
                    else
                        image = "FSIdleMP.jpg";
                    //clear limits
                    //example of calling Javascript with params
                    //object[] o = new object[1];
                    //o[0]="My application is here";
                    //OPTScreen.Document.InvokeScript("ShowMessage", o);
                    break;
                case States.AskCard:
                    image = "AskCard.jpg";
                    if (main.bLimitDone)
                    {
                        if (main.limitType == "Money")
                            l1 = " הוגבל ל" + main.limit.ToString(" 0.00 ") + "שקלים ";
                        else
                            l1 += " הוגבל ל" + main.limit.ToString(" 0.00 ") + "ליטרים ";
                    }
                    else
                        l1 = "";
                    SetLines("", "", "", l1, 0);
                    break;
                case States.ID:
                    image = "ID.jpg";
                    
                    break;
                case States.Plate:
                    image = "Plate.jpg";
                    break;
                case States.CardCheck:
                    image = "CardCheck.jpg";
                    break;
                case States.VISCheking:
                    image = "VISChecking.jpg";
                    break;
                case States.BlockNoz:
                    
                    image = "BlockNoz.jpg";
                    break;
                case States.Fueling:

                    //These lines to play the movie
                    //a = OPTScreen.Document.GetElementById("M1");
                    //a.Style = "visibility:visible";
                    //OPTScreen.Document.InvokeScript("StartPlayer", null);

                    if (main.Self.Checked)
                    {
                        image = "AD.jpg"; ;
                    }
                    else
                    {
                        image = "FSAD.jpg";
                    }

                    break;
                case States.TestFuel:
                    
                    image = "TestFuel.jpg";
                    break;
                case States.LimitMoney:
                    image = "LimitMoney.jpg";
                    
                    break;
                case States.LimitVolume:
                    image = "LimitVolume.jpg";
                    
                    break;
                case States.Receipt:
                    
                    image = "Receipt.jpg";
                    OPTScreen.Document.InvokeScript("StopPlayer", null);
                    break;
                case States.FSReceipt:
                    if (!main.bCreditGiven)
                        image = "FSReceipt.jpg";
                    else
                        image = "FSReceiptNoChangeMOP.jpg";
                    OPTScreen.Document.InvokeScript("StopPlayer", null);
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
                    SetLines(l1, "", " ", " ", 0);
                    break;
                case States.FSMenu1:
                    
                    image = "FMenu1.jpg";  //"MenuAttn.jpg";
                    main.maxmenu = 6;
                    main.menuindex = 1;
                    MarkMenu(main.menuindex, true);
                    break;
                case States.FSMenu2:
                    image = "FSMenu2.jpg";
                    break;
                case States.FSMenu3:
                    image = "FSMenu3.jpg";
                    break;
                case States.FSMenu4:
                    image = "FSMenu4.jpg";
                    break;
                case States.ShiftMenu:
                    image = "SHiftMenu.jpg";
                    break;
                case States.SafeDrop:
                    image = "SafeDrop.jpg";
                    break;
                case States.DrySale:
                    image = "DrySale.jpg";
                    l1 = " סכום כולל " + main.totalsale.ToString("0.00");
                    SetLines("", "", l1, "", 0);
                    break;
                case States.GetProduct:
                    image = "ProductCode.jpg";
                    break;
                case States.GetQunatity:
                    image = "ProductQuntity.jpg";
                    break;
                case States.PrintingRecipt:
                    image = "PrintingReceipt.jpg";
                    break;
                case States.TBD:
                    image = "TBD.jpg";
                    break;
                case States.Err_WrongProduct:
                    image = "Err_WrongProduct.jpg";
                    break;
                case States.Err_NozBlocked:
                    image = "Err_NozBlocked.jpg";
                    break;
                case States.Err_BadIDNum:
                    image = "Err_BadIDNum.jpg";
                    break;
                case States.Err_OpenTrans:
                    image = "OpenTransError.jpg";
                    break;
                case States.CloseingShift:
                    image = "ClosingShift.jpg";
                    break;
                case States.Err_BusyPumps:
                    image = "Err_FuelInProgress.jpg";
                    break;
                case States.Err_CommShift:
                    image = "CommErrCloseShift.jpg";
                    break;
                case States.Err_CommXReport:
                    image = "CommErroXreport.jpg";
                    break;
                case States.Msg_NozIsBlocked:
                    image = "Msg_NozIsBlocked.jpg";
                    break;
                case States.NoService:
                    image = "NoService.jpg";
                    break;
                case States.CashCredit:
                    image = "CashCredit.jpg";
                    break;
                case States.Lang:
                    image = "Lang.jpg";
                    break;
                case States.BadCard:
                    image = "BadCard.jpg";
                    break;
                case States.NoProduct:
                    image = "NoProduct.jpg";
                    break;
                case States.RequestAttn:
                    image = "RequestAttn.jpg";
                    break;
                case States.RequestMan:
                    image = "RequestMan.jpg";
                    break;

                case States.XReport:
                    image = "XReport.jpg";
                    l1 = " מזומן " + 1550.ToString(" 0.00 ");
                    l2 = " אשראי " + 450.ToString("0.00");

                    SetLines("מוכרן רמי יולזרי", l1, l2, " ", 0);

                    break;
                case States.ClockIn:
                    image = "ClockIn.jpg";
                    break;
                case States.ClockOut:
                    image = "ClockOut.jpg";
                    break;
                case States.Odometer:
                    image = "Odometer.jpg";
                    break;
                case States.PINCode:
                    image = "PINCode.jpg";
                    break;
                case States.DropConfirmation:
                    image = "DropConfirmation.jpg";

                    l1 = " הפקדה" + main.dropAmount.ToString(" 0.00 ");

                    SetLines("מוכרן רמי יולזרי", l1, " ", " ", 0);
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

                    SetLines(l1, l2, l3, "", 0);

                    break;

                case States.MenuMode:
                    if (main.currentmenu == Main.MenuType.DryListLevel1 || main.currentmenu == Main.MenuType.DryListLevel2)
                    {
                        image = "DrySaleMenu.png";
                    }
                    else
                        image = "MenuMode.jpg";
                    break;
                case States.NeedDrop:
                    image = "SafeDropCantFuel.png";
                    break;
                case States.OverDrop:
                    image = "DropError.jpg";
                    break;
                case States.DropWarn:
                    image = "SafeDropAlert.png";
                    break;
                case States.AirDrange:
                    image = "AirDrange.jpg";
                    break;
                case States.Err_BadCredit:
                    image = "CreditError.jpg";
                    break;
                case States.ManApprovalDrop:
                    image = "RequestMan.jpg";
                    break;
                case States.RequestSerailMOP:
                    image = "OtherMOP.jpg";
                    break;
                case States.RefundStore:
                    image = "RefundStoreCode.png";
                    break;
                case States.RefundInvoice:
                    image = "RefundInvoiceNo.png";
                    break;
                case States.Refund:
                    image = "RefundSelectItem.png";
                    break;
                case States.RefundCash:
                    image = "RefundCash.png";
                    break;
                case States.RefundCredit:
                    image = "RefundCredit.png";
                    break;
                case States.RefundSelectMOP:
                    image = "RefundSelectMOP.png";
                    break;
                case States.Err_RefundStore:
                    image = "RefundNoStore.png";
                    break;
                case States.Err_RefundInvoice:
                    image = "RefundNoInvoice.png";
                    break;
                case States.Err_RefundNotAllowed:
                    image = "RefundNoAllowed.png";
                    break;

            }

            try
            {
                //update picture 
                a = OPTScreen.Document.GetElementById("Screen");
                a.SetAttribute("src", image);
                main.CurrentImage.Text = image;            
            }
            catch (Exception exp)
            {

            }

        }


    }
}
