using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace DelekOPTSimulation
{
    public enum States
    {
        SSIdle,
        FSIdle,
        FSMenu1,
        FSMenu2,
        ShiftMenu,
        FSMenu3,
        FSMenu4,
        CardCheck,
        ID,
        Plate,
        LiftNoz,
        Fueling,
        Receipt,
        FSReceipt,
        VISCheking,
        VISFuel,
        SafeDrop,
        AskCard,
        LimitVolume,
        LimitMoney,
        DrySale,
        GetProduct,
        GetQunatity,
        PrintingRecipt,
        NoService,
        CashCredit,
        Lang,
        BadCard,
        RequestAttn,
        RequestMan,
        DropConfirmation,
        DryConfirm,
        NoProduct,
        TestFuel,
        XReport,
        ClockIn,
        ClockOut,
        Odometer,
        PINCode,
        BlockNoz,
        Err_WrongProduct,
        Err_NozBlocked,
        Err_BadIDNum,
        Msg_NozIsBlocked,
        ReturnToIdle,
        MenuMode,
        RequestSerailMOP,
        OtherMOP,
        NeedDrop,
        DropWarn,
        OverDrop,
        CloseingShift,
        Err_OpenTrans,
        Err_BusyPumps,
        Err_CommShift,
        Err_CommXReport,
        Err_BadCredit,
        AirDrange,
        ManApprovalDrop,
        Refund,
        RefundStore,
        RefundInvoice,
        RefundCash,
        RefundCredit,
        RefundSelectMOP,
        Err_RefundStore,
        Err_RefundInvoice,
        Err_RefundNotAllowed,

        TBD
    };

    
   

    public partial class Main : Form
    {
        [ComVisible(true)]
        public class ScriptManager
        {
            // Variable to store the form of type Form1.
            private Main mForm;

            // Constructor.
            public ScriptManager(Main form)
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

      

        //dry items sale
        
        public double totalsale = 0.0;
        public int dryqunatity = 1;
        public int drycode = 0;
        public double drylastsale = 0.0;
        public bool bDrySale = false;

        public States state;
        //string indata = "";
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer pumpTimer = new System.Windows.Forms.Timer();
        //pump data
        static double volume = 0000.00;
        static double money = 0000.00;
        static double price = 6.18;   //95 full service
        static double pricefull = 6.18;
        static double priceself = 5.99; //95 self service
        static bool isNozUp = false;
        
        //limits
        public bool bLimitDone = false;
        public int limit = 0;
        public string limitType = "Money";

        //pumpused
        public int PumpUsed = 1;

        public string indata = "";
        public string PromptType = "Short";
        public States action;
        public int dropAmount;
        public bool bChangeMOP = false;
        public bool bCreditGiven = false;
        

        public bool bCashPay = true;
        public bool bFuelIncluded = false;

        States backState = States.ReturnToIdle;  //the one to retunr after error
        Invenco invenco = null;

        OrPT orpt = null;

        Config conf = null;

        public Main()
        {
            InitializeComponent();

            FileVersion.Text = "Version:" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString(); 

            //load configuration
            conf = new Config();
                        
            state = States.SSIdle;
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            // Sets the timer interval to 3 seconds.
            myTimer.Interval = 2000;

            pumpTimer.Tick += new EventHandler(PumpTimerEventProcessor);
            // Sets the timer interval to 0.5 seconds.
            pumpTimer.Interval = 500;
            pumpTimer.Start(); //run allways

            CardType.SelectedIndex = 0;

            /*add some items
            Items item = new Items();
            item.Code = 1;
            item.Name = "מים";
            item.price = 12.00;
            items.Add(item);
            item = new Items();
            item.Code = 2;
            item.Name ="שמן";
            item.price = 23.50;
            items.Add(item);

            //add some itemscat
            ItemsCat itemcat = new ItemsCat();
            itemcat.Name = "שמנים";
            //build items list
            item = new Items();
            item.Code = 1;
            item.Name = "מים";
            item.price = 12.00;
            items.Add(item);
             */ 


            //open the OPT screen
            
            invenco = new Invenco(this);

            SetMultiPump(PumpUsed);

            invenco.Show();



            //do not show ORPT
            orpt = new OrPT(this);
            //orpt.Show();


        }

        bool CheckIDNo(String strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (Validation.Checked == false)
                return true;
            
            if (strID == null)
                return false;

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            switch (state)
            {
                case States.VISCheking:                
                    if (isNozUp)
                        SetState(States.Fueling);
                    else
                        SetState(States.LiftNoz);
                    break;
                case States.CardCheck:
                    if (BadCredit.Checked)
                    {
                        SetState(States.Err_BadCredit);
                        SetShortMessage();
                        return;
                    }
                    if (bDrySale || bChangeMOP)
                    {
                        SetState(States.PrintingRecipt);
                    }
                    else
                    if (action != States.TBD)
                    {
                        SetState(action);
                    }
                    else
                    {
                        if (isNozUp)
                            SetState(States.Fueling);
                        else
                            SetState(States.LiftNoz);
                    }
                    break;
                case States.PrintingRecipt:
                    if (Self.Checked)
                    {
                        SetState(States.SSIdle);
                    }
                    else 
                    {
                        SetState(States.FSIdle);
                    }
                    break;
                case States.BadCard:
                    if (Self.Checked)
                    {
                        SetState(States.SSIdle);
                    }
                    else
                    {
                        SetState(States.FSIdle);
                    }
                    break;
                case States.NeedDrop:
                    SetState(States.FSIdle);
                    break;
                case States.OverDrop:
                    SetState(States.FSIdle);
                    break;
                case States.DropWarn:
                    SetState(action);
                    break;
                case States.Err_BadIDNum:
                    SetState(States.ID);                    
                    break;
                case States.NoProduct:
                    SetState(States.DrySale);
                    break;
                case States.TBD:
                    SetState(States.FSMenu1);
                    break;
                case States.Err_WrongProduct:
                case States.Err_NozBlocked:
                case States.Msg_NozIsBlocked:
                case States.Err_OpenTrans:
                case States.CloseingShift:
                case States.Err_BusyPumps:
                case States.Err_CommXReport:
                case States.Err_CommShift:
                case States.Err_RefundNotAllowed:
                case States.Err_RefundInvoice:
                case States.Err_RefundStore:
                    if (backState == States.ReturnToIdle)
                    {
                        if (Self.Checked)
                        {
                            SetState(States.SSIdle);
                        }
                        else
                        {
                            SetState(States.FSIdle);
                        }
                    }
                    else
                        SetState(backState);
                    break;
                

            }
        }

        


        private void PumpTimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if ((state == States.Fueling || state == States.TestFuel) && isNozUp)
            {
                volume += 1.5;
                money = volume * price;
                if (bLimitDone)
                {
                    if (limitType == "Volume")
                    {
                        if (volume > limit)
                        {
                            volume = limit;
                            money = volume * price;
                        }
                    }
                    else
                    {
                        if (money > limit)
                        {
                            money = limit;
                            volume = money / price;
                        }
                    }
                }
                Volume.Text = volume.ToString("0000.00");
                Money.Text = money.ToString("0000.00");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public void SetLines(string l1, string l2, string l3, string l4, byte marked)
        {
            invenco.SetLines(l1, l2, l3, l4, marked);
        }

        

        public void SetMultiPump(int Pump)
        {
            invenco.SetMultiPump(Pump);
        }

        


        private void SetOPTState(States state)
        {
            orpt.SetOPTState(state);
            invenco.SetOPTState(state);
        }
        

        public void SetShortMessage()
        {
            Console.Beep(1440, 300);
            Console.Beep(1440, 300);
            myTimer.Interval = 1500; //1.5 seconds
            myTimer.Start();
        }

        public void SetState(States newstate)
        {
            string image="";
            string l1 = "";
            string l2 = "";
            string l3 = "";
            string l4 = "";
            HtmlElement a;
            //SetLines(" ", " ", " ", " ",0);
            //a = invenco.OPTScreen.Document.GetElementById("M1");
            //a.Style = "visibility:hidden";
            UpdatePrompt("");
            backState = States.ReturnToIdle;
            switch (newstate)
            {
                case States.SSIdle:
                    state = States.SSIdle;
                   // image = "SSIdleLang.jpg";
                    bLimitDone = false;
                    bDrySale = false;
                    bChangeMOP = false;
                    bCreditGiven = false;
                    totalsale = 0;
                    action = States.TBD;
                    bCashPay = false; //always with credit for Self service
                    break;
                case States.FSIdle:
                    state = States.FSIdle;
                    //image = "FSIdle.jpg";
                    //clear limits
                    bLimitDone = false;
                    bDrySale = false;
                    bChangeMOP = false;
                    bCreditGiven = false;
                    bFuelIncluded = false;
                    totalsale = 0;
                    action = States.TBD;
                    //example of calling Javascript with params
                    //object[] o = new object[1];
                    //o[0]="My application is here";
                    //OPTScreen.Document.InvokeScript("ShowMessage", o);
                    break;
                case States.AskCard:
                    state = States.AskCard;
                    image = "AskCard.jpg";
                    bCreditGiven = true;
                    if (bLimitDone)
                    {
                        if (limitType == "Money")
                            l1 = " הוגבל ל" + limit.ToString(" 0.00 ") + "שקלים ";
                        else
                            l1 += " הוגבל ל" + limit.ToString(" 0.00 ") + "ליטרים ";
                    }
                    else
                        l1 = "";
                    invenco.SetLines("", "", "", l1, 0);
                    break;
                case States.ID:
                    state = States.ID;
                    image = "ID.jpg";
                    PromptType = "Long";
                    break;
                case States.Plate:
                    state = States.Plate;
                    image = "Plate.jpg";
                    PromptType = "Long";
                    break;
                case States.CardCheck:
                    state = States.CardCheck;
                    SetShortMessage();
                    break;
                case States.VISCheking:
                    state = States.VISCheking;
                    image = "VISChecking.jpg";
                    
                    break;
                case States.BlockNoz:
                    state = States.BlockNoz;
                    image = "BlockNoz.jpg";
                    break;
                case States.Fueling:
                    state = States.Fueling;
                    
                    //These lines to play the movie
                    //a = OPTScreen.Document.GetElementById("M1");
                    //a.Style = "visibility:visible";
                    //OPTScreen.Document.InvokeScript("StartPlayer", null);
                   
                    if (Self.Checked)
                    {
                        image = "AD.jpg"; ;
                    }
                    else
                    {
                        image = "FSAD.jpg";
                    }
                    
                    break;
                case States.TestFuel:
                    state = States.TestFuel;
                    image = "TestFuel.jpg";
                    break;
                case States.LimitMoney:
                    image = "LimitMoney.jpg";
                    state = States.LimitMoney;
                    PromptType = "Short";
                    break;
                case States.LimitVolume:
                    image = "LimitVolume.jpg";
                    state = States.LimitVolume;
                    PromptType = "Short";
                    break;
                case States.Receipt:
                    state = States.Receipt;
                    image = "Receipt.jpg";
                    invenco.OPTScreen.Document.InvokeScript("StopPlayer", null);                    
                    break;
                case States.FSReceipt:
                    state = States.FSReceipt;
                    image = "FSReceipt.jpg";
                    invenco.OPTScreen.Document.InvokeScript("StopPlayer", null);
                    break;
                case States.LiftNoz:
                    state = States.LiftNoz;
                    image = "LiftNoz.jpg";
                    if (bLimitDone)
                    {
                        if (limitType == "Money")
                            l1 = " הוגבל ל" + limit.ToString(" 0.00 ") + "שקלים ";
                        else
                            l1 += " הוגבל ל" + limit.ToString(" 0.00 ") + "ליטרים ";
                    }
                    else
                        l1 = "";
                    invenco.SetLines(l1,""," ", " ", 0);
                    break;
                case States.FSMenu1:
                    state = States.FSMenu1;
                    image = "FMenu1.jpg";  //"MenuAttn.jpg";
                    maxmenu = 6;
                    menuindex = 1;
                    MarkMenu(menuindex,true);
                    break;
                case States.FSMenu2:
                    state = States.FSMenu2;
                    image = "FSMenu2.jpg";
                    break;
                case States.FSMenu3:
                    state = States.FSMenu3;
                    image = "FSMenu3.jpg";
                    break;
                case States.FSMenu4:
                    state = States.FSMenu4;
                    image = "FSMenu4.jpg";
                    break;
                case States.ShiftMenu:
                    state = States.ShiftMenu;
                    image = "SHiftMenu.jpg";
                    break;
                case States.SafeDrop:
                    state = States.SafeDrop;
                    image = "SafeDrop.jpg";
                    break;
                case States.DrySale:

                    StartMenu(MenuType.DryListLevel1,true);
                    SetState(States.MenuMode);                    
                    /*
                    state = States.DrySale;
                    image = "DrySale.jpg";
                    l1 =  " סכום כולל " + totalsale.ToString("0.00");
                    SetLines("", "",l1, "", 0);
                     */
                    break;
                case States.GetProduct:
                    state = States.GetProduct;
                    image = "ProductCode.jpg";
                    break;
                case States.GetQunatity:
                    state = States.GetQunatity;
                    image = "ProductQuntity.jpg";
                    break;
                case States.PrintingRecipt:
                    state = States.PrintingRecipt;
                    image = "PrintingReceipt.jpg";
                    myTimer.Interval = 2500; //2.5 seconds
                    myTimer.Start();
                    break;
                case States.TBD:
                    state = States.TBD;
                    image = "TBD.jpg";
                    myTimer.Interval = 1500; //1.5 seconds
                    myTimer.Start();
                    break;
                case States.Err_WrongProduct:
                    state = States.Err_WrongProduct;
                    image = "Err_WrongProduct.jpg";
                    SetShortMessage();
                    break;
                case States.Err_NozBlocked:
                    state = States.Err_NozBlocked;
                    image = "Err_NozBlocked.jpg";
                    SetShortMessage();
                    break;
                case States.Msg_NozIsBlocked:
                    state = States.Msg_NozIsBlocked;
                    image = "Msg_NozIsBlocked.jpg";
                    SetShortMessage();
                    break;
                case States.NoService:
                    state = States.NoService;
                    image = "NoService.jpg";
                    break;
                case States.CashCredit:
                    state = States.CashCredit;
                    image = "CashCredit.jpg";
                    break;
                case States.Lang:
                    state = States.Lang;
                    image = "Lang.jpg";
                    break;
                case States.BadCard:
                    state = States.BadCard;
                    image = "BadCard.jpg";
                    SetShortMessage();
                    break;
                case States.Err_BadIDNum:
                    state = States.Err_BadIDNum;
                    image = "Err_BadIdNum.jpg";
                    SetShortMessage();
                    break;
                case States.NoProduct:
                    state = States.NoProduct;
                    image = "NoProduct.jpg";
                    SetShortMessage();
                    break;
                case States.RequestAttn:
                    state = States.RequestAttn;
                    image = "RequestAttn.jpg";
                    break;
                case States.RequestMan:
                    state = States.RequestMan;
                    image = "RequestMan.jpg";
                    break;

                case States.XReport:
                    state = States.XReport;
                    image = "XReport.jpg";
                    l1 = " מזומן " + 1550.ToString(" 0.00 ");
                    l2 = " אשראי " + 450.ToString("0.00");
                   
                    invenco.SetLines("מוכרן רמי יולזרי",l1,l2, " ", 0);

                    break;
                case States.ClockIn:
                    state = States.ClockIn;
                    image = "ClockIn.jpg";
                    break;
                case States.ClockOut:
                    state = States.ClockOut;
                    image = "ClockOut";
                    break;
                case States.Odometer:
                    state = States.Odometer;
                    image = "Odometer.jpg";
                    break;
                case States.PINCode:
                    state = States.PINCode;
                    image = "PINCode.jpg";
                    break;
                case States.DropConfirmation:
                    state = States.DropConfirmation;
                    image = "DropConfirmation.jpg";
                    
                    l1 = " הפקדה" + dropAmount.ToString(" 0.00 ");
                   
                    SetLines("מוכרן רמי יולזרי",l1," ", " ", 0);
                    break;
                case States.DryConfirm:
                    state = States.DryConfirm;
                    image = "DryConfirm.jpg";

                    string itemname = "";
                    double itemprice = 0.0;

                    Config.GetItemByCode(drycode, ref itemname, ref itemprice);

                    //Items item = items.Find(x => x.Code == drycode);
                                      

                    l1 = itemname + " כמות " + dryqunatity.ToString("0") + "  ";
                    //l2 = " כמות " + dryqunatity.ToString("0");
                    l2 = " מחיר ליחידה " + itemprice.ToString("0.00");
                    l3 = " מחיר כולל " + (itemprice * dryqunatity).ToString("0.00");
                   
                    SetLines(l1,l2,l3,"", 0);

                    break;
                case States.MenuMode:
                    state = States.MenuMode;
                    image = "MenuMode.jpg";                    
                    break;

                case States.VISFuel:
                    SetState(States.VISCheking);
                    myTimer.Interval = 2000;
                    myTimer.Start();
                    break;
                case States.NeedDrop:
                    state = States.NeedDrop;
                    SetShortMessage();
                    break;
                case States.OverDrop:
                    state = States.OverDrop;
                    SetShortMessage();
                    break;
                case States.DropWarn:
                    state = States.DropWarn;
                    SetShortMessage();
                    break;
                case States.Err_OpenTrans:
                    state = States.Err_OpenTrans;
                    SetShortMessage();
                    break;
                case States.Err_BusyPumps:
                    state = States.Err_BusyPumps;
                    SetShortMessage();
                    break;
                case States.Err_CommShift:
                    state = States.Err_CommShift;
                    SetShortMessage();
                    break;
                case States.Err_CommXReport:
                    state = States.Err_CommXReport; // h
                    SetShortMessage();
                    break;
                case States.CloseingShift:
                    state = States.CloseingShift;
                    SetShortMessage();
                    break;
                case States.AirDrange:
                    state = States.AirDrange;
                    break;
                case States.Err_BadCredit:
                    state = States.Err_BadCredit;
                    break;
                case States.ManApprovalDrop:
                    state = States.ManApprovalDrop;
                    break;
                case States.RequestSerailMOP:
                    state = States.RequestSerailMOP;
                    break;
                case States.RefundStore:
                    state = States.RefundStore;
                    break;
                case States.RefundInvoice:
                    state = States.RefundInvoice;
                    break;
                case States.Refund:
                    state = States.Refund;
                    break;
                case States.RefundCash:
                    state = States.RefundCash;
                    break;
                case States.RefundCredit:
                    state = States.RefundCredit;
                    break;
                case States.RefundSelectMOP:
                    state = States.RefundSelectMOP;
                    break;
                case States.Err_RefundStore:
                    state = States.Err_RefundStore;
                    SetShortMessage();
                    break;
                case States.Err_RefundInvoice:
                    state = States.Err_RefundInvoice;
                    SetShortMessage();
                    break;
                case States.Err_RefundNotAllowed:
                    state = States.Err_RefundNotAllowed;
                    SetShortMessage();
                    break;


            }

            try
            { 
                //update picture 
                //a = invenco.OPTScreen.Document.GetElementById("Screen");
                //a.SetAttribute("src", image);
                SetOPTState(state);
            }
            catch (Exception exp)
            {

            }
        
        }

        private void UpdatePrompt(string prompt)
        {
            invenco.UpdatePrompt(prompt);
            orpt.UpdatePrompt(prompt);
            return;
             
        }



        public void HandlePrompt(char key,string type)
        {
            if (key == 'A' || key == 'B' || key == 'C' || key == 'D')
                return; //no function keys

            if (key == '.' && type == "Int")
                return; //do not accept . if integer

            if (key == 'K' && indata.Length > 0)  //back
            {
                indata = indata.Substring(0, indata.Length - 1);
                UpdatePrompt(indata);
            }
            else if (key == '.')
            {
                if (!indata.Contains("."))
                {
                    indata += key;
                    UpdatePrompt(indata);
                }
            }
            else if (indata.Length < 16)
            {
                indata += key;
                UpdatePrompt(indata);
            }
        }
        
        

                


        private double GetDoubleValue(string indata)
        {
            try
            {
                return double.Parse(indata);
            }
            catch
            {
                return 0.0;
            }
        }

        private int GetIntValue(string indata)
        {
            try
            {
                return int.Parse(indata);
            }
            catch
            {
                return 0;
            }
        }

        

       

        public void Self_CheckedChanged(object sender, EventArgs e)
        {
            //reset all and set mode
            ClearMenu();
            price = priceself;
            PPV.Text = price.ToString("0.00");
            totalsale = 0.0;
            UpdatePrompt("");
            SetState(States.SSIdle);
            F95.Enabled = true;
            Soler.Enabled = true;
        }

        private void Full_CheckedChanged(object sender, EventArgs e)
        {
            //reset all and set mode
            price = pricefull;
            PPV.Text = price.ToString("0.00");
            UpdatePrompt("");
            totalsale = 0.0;
            SetState(States.FSIdle);
            F95.Enabled = true;
            Soler.Enabled = true;
        }

        private void NoService_CheckedChanged(object sender, EventArgs e)
        {
            PPV.Text = "";
            ClearMenu();
            UpdatePrompt("");
            SetState(States.NoService);
        }

       

       

        private void Noz_Click(object sender, EventArgs e)
        {
            //change noz state
            if (isNozUp)
            {
                isNozUp = false;
                Noz.Image = DelekOPTSimulation.Properties.Resources.PicNozzDown;
                if (action == States.AirDrange) //just print ticket
                {
                    SetState(States.Receipt);
                    return;
                }
                if (state == States.Fueling || state == States.TestFuel)
                {
                    if (Self.Checked)
                    {
                        SetState(States.Receipt);
                    }
                    else
                    {
                        SetState(States.FSReceipt);                     
                    }
                }
            }
            else //nozzle was lifted
            {
                isNozUp = true;
                Noz.Image = DelekOPTSimulation.Properties.Resources.LogNozzUp;
                if (state == States.LiftNoz)
                { 
                    if (action == States.AirDrange)
                    {
                        SetState(States.Fueling);
                        return;
                    }
                    //check if selected nozzel is correct
                    if (CardTypes.LAM == GetCardType() && F95.Checked)
                    {
                        SetState(States.Err_WrongProduct);
                        backState = States.LiftNoz;
                    }
                    else if ((!F95.Enabled && F95.Checked) || (!Pump.Enabled && Soler.Checked))
                    {
                        SetState(States.Err_NozBlocked);
                        backState = States.LiftNoz;
                    }
                    else
                    {
                        volume = 0.0;
                        Volume.Text = volume.ToString("0000.00");
                        SetState(States.Fueling);
                    }
                }
            }
            
        }

        private void MultiPump_CheckedChanged(object sender, EventArgs e)
        {
            if (MultiPump.Checked)
                SetMultiPump(PumpUsed);
            else
                SetMultiPump(0);

            SetState(state); //just to refresh
        }

        

        

        
    }
}
