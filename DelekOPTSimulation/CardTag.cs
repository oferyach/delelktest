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
    public enum CardTypes
    {
        Credit,
        Attn,
        Manager,
        LAM,
        LAMPrompts,
        VIU,
        NoCredit,
        BadCard,
        OtherMOP
    };
    public partial class Main : Form
    {
        private void CardTag_Click(object sender, EventArgs e)
        {

            if (CardTypes.NoCredit == GetCardType())
            {
                SetState(States.Err_BadCredit);
                return;
            }
            if (CardTypes.BadCard == GetCardType())
            {
                SetState(States.BadCard);
                return;
            }

            if (CardTypes.VIU == GetCardType())
            {
                if (!VIUAttn.Checked)
                {
                    SetState(States.VISCheking);
                    SetShortMessage();
                }
                else
                {
                    action = States.VISFuel;
                    SetState(States.RequestAttn);
                }
                return;
            }

            if (CardTypes.Attn == GetCardType())
            {
                if (state == States.ClockIn || state == States.ClockOut)
                {
                    SetState(States.FSIdle);
                    return;
                }
            }
            
            switch (state)
            {
                case States.SSIdle:
                    if (CardTypes.Credit == GetCardType())
                    {
                        SetState(States.Plate);
                    }
                     
                    else if (CardTypes.LAM == GetCardType())
                        SetState(States.CardCheck);
                    else if (CardTypes.LAMPrompts == GetCardType())
                        SetState(States.PINCode);                   
                    break;
                case States.FSIdle:
                    if (CardTypes.Attn == GetCardType())
                    {
                        if (NeedDrop.Checked)
                            SetState(States.NeedDrop);
                        else
                            if (DropWarn.Checked)
                            {
                                SetState(States.DropWarn);
                                action = States.CashCredit;
                            }
                            else
                            SetState(States.CashCredit);
                    }
                    break;
                case States.CashCredit: //if card was pass just do the card do not need to ask
                case States.AskCard:
                    if (Self.Checked)
                    {
                        if (CardTypes.Credit == GetCardType())
                            SetState(States.ID);
                        else if (CardTypes.LAM == GetCardType())
                            SetState(States.CardCheck);
                        else
                            if (CardTypes.LAMPrompts == GetCardType())
                                SetState(States.PINCode);
                            else
                                SetState(States.BadCard);
                    }
                    else //Full service need Attn card
                    {
                        if (CardTypes.Credit == GetCardType())
                            SetState(States.Plate);
                        else
                        {
                            if (CardTypes.Attn == GetCardType())
                                SetState(action);
                            else
                                SetState(States.BadCard);
                        }
                    }
                    break;
                case States.RequestAttn:
                    if (CardTypes.Attn == GetCardType())
                        SetState(action);
                    else
                        SetState(States.BadCard);
                    break;
                case States.RequestMan:
                    if (CardTypes.Manager == GetCardType())
                        SetState(action);
                    else
                        SetState(States.BadCard);
                    break;
                case States.AirDrange:
                    if (CardTypes.Attn == GetCardType())
                    {
                        action = States.AirDrange;
                        SetState(States.LiftNoz);
                    }
                    else
                        SetState(States.BadCard);
                    break;
                case States.ManApprovalDrop:
                    if (CardTypes.Manager == GetCardType())
                    {
                        SetState(States.DropConfirmation);
                    }
                    else
                        SetState(States.BadCard);
                    break;
                case States.RequestSerailMOP:
                    if (CardTypes.OtherMOP == GetCardType())
                    {
                        SetState(States.LiftNoz);
                    }
                    else
                        SetState(States.BadCard);
                    break;
                
            }
        }

        private CardTypes GetCardType()
        {
            switch (CardType.SelectedIndex)
            {
                case 0: return CardTypes.Credit;
                case 1: return CardTypes.Attn;
                case 2: return CardTypes.LAM;
                case 3: return CardTypes.LAMPrompts;
                case 4: return CardTypes.Manager;
                case 5: return CardTypes.BadCard;
                case 6: return CardTypes.VIU;
                case 7: return CardTypes.OtherMOP;
                default: return CardTypes.BadCard;
            }
        }
    }
}