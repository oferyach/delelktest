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


namespace DelekOPTSimulation
{
    public partial class Main : Form
    {
        public void KeyIn(char key)
        {
            //System.Media.SystemSounds.Beep.Play();
            switch (state)
            {
                case States.SSIdle:
                    if (key == 'D' && MultiPump.Checked)
                    {
                        //TBD replace this with menu
                        PumpUsed++;
                        if (PumpUsed > 4)
                            PumpUsed = 1;
                        //update screen
                        SetMultiPump(PumpUsed);
                    }                    
                    if (key == 'B')
                    {
                        SetState(States.LimitVolume);  //TBD any limit
                    }
                    if (key == 'C')
                    {
                        BuildOtherMOPMenu();
                        SetState(States.MenuMode);
                    }
                    if (key == 'A')
                    {
                        SetState(States.Lang);
                    }
                    break;
                case States.LiftNoz:
                    if (key == 'X')
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
                    break;
                case States.FSIdle:
                    if (key == 'C')  //man FS menu
                    {                        
                        BuildAndSetAttnMenu();
                        SetState(States.MenuMode);
                    }
                    if (key == 'A')
                    {
                        SetState(States.LimitMoney);
                    }
                    if (key == 'B')
                    {
                        SetState(States.LimitVolume);
                    }
                    if (key == 'D' && MultiPump.Checked)
                    {
                        //TBD replace this with menu
                        PumpUsed++;
                        if (PumpUsed > 4)
                            PumpUsed = 1;
                        //update screen
                        SetMultiPump(PumpUsed);
                    }
                    break;
                case States.CashCredit:
                    if (key == 'X')
                    {
                        SetState(States.FSIdle);
                    }
                    else if (key == 'B') //cash
                    {
                        bCashPay = true;
                        if (bDrySale || bChangeMOP)
                        {
                            SetState(States.PrintingRecipt);
                        }
                        else
                        {
                            //promot for plate
                            SetState(States.Plate);                            
                        }
                    }
                    else if (key == 'C') //credit
                    {
                        bCashPay = false;
                        SetState(States.AskCard);
                    }
                    break;
                case States.ID:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {                        
                        //check ID validity
                        if (CheckIDNo(indata))
                        {
                            UpdatePrompt("$");  //clear
                            SetState(States.Plate);
                        }
                        else
                        {
                            UpdatePrompt("$");  //clear
                            SetState(States.Err_BadIDNum);
                        }
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.Plate:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {                        
                        UpdatePrompt("$");
                        if (CardTypes.LAMPrompts == GetCardType())
                            SetState(States.Odometer);
                        else if (bCashPay)
                        {
                            if (isNozUp)
                                SetState(States.Fueling);
                            else
                                SetState(States.LiftNoz);
                        }
                        else
                            SetState(States.CardCheck);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.PINCode:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {                        
                        UpdatePrompt("$");
                        SetState(States.Plate);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.Odometer:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {                        
                        UpdatePrompt("$");
                        SetState(States.CardCheck);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.Receipt:  //self service
                    //show short message while receipt is been printed
                    if (key == 'O')  //only if asked for
                        SetState(States.PrintingRecipt);
                    else if (key == 'X')
                        SetState(States.SSIdle);
                    break;
                case States.FSReceipt: //full service can add dry
                    if (key == 'O')
                        SetState(States.PrintingRecipt);
                    else if (key == 'C')  //sale
                    {
                        totalsale = money; //start with fuel 
                        bFuelIncluded = true;
                        SetState(States.DrySale);
                    }
                    else if (key == 'B') //pay in store
                    {
                        SetState(States.FSIdle);
                    }
                    else if (key == 'D' && !bCreditGiven) // change MOP
                    {
                        bChangeMOP = true;
                        SetState(States.CashCredit);
                    }
                    else if (key == 'X')
                        SetState(States.FSIdle);
                            
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
                case States.LimitMoney:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {

                        limit = GetIntValue(indata);
                        limitType = "Money";
                        bLimitDone = true;                        
                        UpdatePrompt("$");
                        SetState(States.AskCard);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.LimitVolume:
                    if (key == 'X')  //cancel
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
                    else if (key == 'O')  //OK
                    {

                        limit = GetIntValue(indata);
                        limitType = "Volume";
                        bLimitDone = true;                        
                        UpdatePrompt("$");
                        SetState(States.AskCard);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.SafeDrop:
                    if (key == 'X')  //cancel
                    {
                        SetState(States.FSIdle);
                    }
                    else if (key == 'O')  //OK
                    {

                        dropAmount = GetIntValue(indata);
                        if (OverDropMoney.Checked)
                        {
                            SetState(States.OverDrop);
                            
                        }
                        else
                        {
                            UpdatePrompt("$");
                            SetState(States.DropConfirmation);
                        }
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.GetProduct:
                    if (key == 'X')  //cancel
                    {
                        SetState(States.DrySale);
                    }
                    else if (key == 'O')  //OK
                    {
                        drycode = GetIntValue(indata);                        
                        UpdatePrompt("$");

                        string itemname = "";
                        double itemprice = 0.0;

                        Config.GetItemByCode(drycode, ref itemname, ref itemprice);
                        //Items item = items.Find(x => x.Code == drycode);

                        if (itemname == "")
                            SetState(States.NoProduct);
                        else
                            SetState(States.GetQunatity);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.GetQunatity:
                    if (key == 'X')  //cancel
                    {
                        SetState(States.DrySale);
                    }
                    else if (key == 'O')  //OK
                    {
                        dryqunatity = GetIntValue(indata);                        
                        UpdatePrompt("$");
                        string itemname = "";
                        double itemprice = 0.0;

                        Config.GetItemByCode(drycode, ref itemname, ref itemprice);
                        //Items item = items.Find(x => x.Code == drycode);
                        drylastsale = dryqunatity * itemprice;
                        SetState(States.DryConfirm);
                    }
                    else
                        HandlePrompt(key,"Int");
                    break;
                case States.DryConfirm:
                    if (key == 'X')
                        SetState(States.DrySale);
                    else if (key == 'O')
                    {
                        totalsale += drylastsale;
                        SetState(States.DrySale);
                    }

                    break;
                    /* using menu
                    case States.FSMenu1:

                        switch (key)
                        {
                            case 'X':
                                SetState(States.FSIdle);
                                break;
                            case 'A':
                                SetState(States.FSMenu2);
                                break;
                            case 'B':
                                action = States.SafeDrop;
                                SetState(States.RequestAttn);
                                break;
                            case 'C':
                                action = States.DrySale;
                                SetState(States.RequestAttn);
                                break;
                            case 'D':
                                SetState(States.PrintingRecipt);
                                break;
                        }
                        break;
                    case States.FSMenu2:
                        switch (key)
                        {
                            case 'X':
                                SetState(States.FSIdle);
                                break;
                            case 'A':
                                SetState(States.FSMenu3);
                                break;
                            case 'B':
                                action = States.TestFuel;
                                SetState(States.RequestMan);
                                break;
                            case 'C':
                                SetState(States.TBD);
                                break;
                            case 'D':
                                SetState(States.ShiftMenu);
                                break;
                        }
                        break;
                    case States.FSMenu3:
                        switch (key)
                        {
                            case 'X':
                                SetState(States.FSIdle);
                                break;
                            case 'A':
                                SetState(States.FSMenu4);
                                break;
                            case 'B':                            
                                SetState(States.TBD);
                                break;
                            case 'C':                            
                                SetState(States.TBD);
                                break;
                            case 'D':
                                SetState(States.TBD);
                                break;
                        }
                        break;
                    case States.FSMenu4:
                        switch (key)
                        {
                            case 'X':
                                SetState(States.FSIdle);
                                break;
                            case 'A':
                                //SetState(States.TBD);
                                break;
                            case 'B':                            
                                //SetState(States.TBD);
                                break;
                            case 'C':                            
                                //SetState(States.TBD);
                                break;
                            case 'D':
                                SetState(States.BlockNoz);
                                break;
                        }
                        break;
                    
                    case States.ShiftMenu:
                        switch (key)
                        {
                            case 'X':
                                SetState(States.FSIdle);
                                break;
                            case 'A':
                                SetState(States.TBD);
                                break;
                            case 'B':
                                action = States.TestFuel;
                                SetState(States.TBD);
                                break;
                            case 'C':
                                action = States.XReport;
                                SetState(States.RequestAttn);
                                break;
                            case 'D':
                                SetState(States.TBD);
                                break;
                        }
                         */
                    break;
                case States.BlockNoz:
                    if (key == 'X')
                        SetState(States.FSIdle);
                    else if (key == 'D')
                    {
                        F95.Enabled = false;
                        SetState(States.Msg_NozIsBlocked);
                    }
                    else if (key == 'C')
                    {
                        SetState(States.Msg_NozIsBlocked);
                        Soler.Enabled = false;
                    }
                    break;
                case States.XReport:
                    if (key == 'X' || key == 'D')
                        SetState(States.FSIdle);
                    break;
                case States.DrySale:
                    switch (key)
                    {
                        case 'C':  //add
                            SetState(States.GetProduct);
                            break;
                        case 'D': //finshe
                            bDrySale = true;
                            if (bFuelIncluded && bCashPay)
                                SetState(States.CashCredit);
                            else
                                SetState(States.FSReceipt);
                            break;
                        case 'X':
                        case 'B': //cancel
                            SetState(States.FSIdle);
                            break;
                    }
                    break;
                case States.Lang:
                    if (key == 'X')
                        SetState(States.SSIdle);
                    break;
                case States.DropConfirmation:
                    if (key == 'X')
                        SetState(States.FSIdle);
                    else if (key == 'O')
                        SetState(States.FSIdle);
                    break;
                case States.AskCard:
                    if (key == 'X')  //cancel
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
                    break;
                case States.RequestAttn:
                case States.RequestMan:
                    if (key == 'X')
                        SetState(States.FSIdle);
                    break;

                case States.MenuMode:
                    MarkMenu(menuindex, false);
                    if (key >= '1' && key <= '8')
                    {
                        //selected by num key
                        int inx = Int16.Parse(key.ToString());
                        if (inx >=1 && inx <= maxmenu)  //in range
                        {
                            menuindex = inx;
                            SetMenuAction();
                        }
                    }
                    if (key == 'O')
                    {
                        //selected
                        SetMenuAction();
                    }
                    if (key == 'X')
                    {
                        menuindex = 0;
                        currentmenu = MenuType.None;
                        ClearMenu();
                        if (Self.Checked)
                        {
                            SetState(States.SSIdle);
                        }
                        else
                        {
                            SetState(States.FSIdle);
                        }
                    }
                    if (key == 'B')
                    {
                        //up / next key
                        menuindex++;
                        if (menuindex>maxmenu)
                        {
                            //move to next page
                            menuindex=1;
                        }
                    }
                    if (key == 'C')
                    {
                        //down / previous key
                        menuindex--;
                        if (menuindex==0)
                        {
                            menuindex = maxmenu;
                        }

                    }
                    MarkMenu(menuindex, true);
                    break;
                    

            }
        }

        private void F1_Click(object sender, EventArgs e)
        {
            KeyIn('A'); //F1
        }

        private void F2_Click(object sender, EventArgs e)
        {
            KeyIn('B');  //F2
        }

        private void F3_Click(object sender, EventArgs e)
        {
            KeyIn('C');  //F3
        }

        private void F4_Click(object sender, EventArgs e)
        {
            KeyIn('D'); //F4
        }

        private void B1_Click(object sender, EventArgs e)
        {
            KeyIn('1');
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            KeyIn('O');
        }

        private void B2_Click(object sender, EventArgs e)
        {
            KeyIn('2');
        }

        private void BX_Click(object sender, EventArgs e)
        {
            KeyIn('X');  // Cancel
        }

        private void B3_Click(object sender, EventArgs e)
        {
            KeyIn('3');
        }

        private void BBack_Click(object sender, EventArgs e)
        {
            KeyIn('K'); //back 
        }

        private void BDOT_Click(object sender, EventArgs e)
        {
            KeyIn('.'); //dot
        }

        private void B4_Click(object sender, EventArgs e)
        {
            KeyIn('4');
        }

        private void B5_Click(object sender, EventArgs e)
        {
            KeyIn('5');
        }

        private void B6_Click(object sender, EventArgs e)
        {
            KeyIn('6');
        }

        private void B7_Click(object sender, EventArgs e)
        {
            KeyIn('7');
        }

        private void B8_Click(object sender, EventArgs e)
        {
            KeyIn('8');
        }

        private void B9_Click(object sender, EventArgs e)
        {
            KeyIn('9');
        }

        private void B0_Click(object sender, EventArgs e)
        {
            KeyIn('0');
        }

        private void BH_Click(object sender, EventArgs e)
        {
            KeyIn('#');
        }
    }
}