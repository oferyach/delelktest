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
        public void BuildAndSetAttnMenu()
        {
            //the menu is set - TBD according to actual action
            SetMenu("מכירות", 1);
            SetMenu("קבלה אחרונה", 2);
            SetMenu("הפקדה לכספת", 3);           
            SetMenu("ניהול משמרות", 4);
            SetMenu("התעלם מדלקן", 5);
            SetMenu("אישור דלקן חריג", 6);
            SetMenu("החזרות", 7);
            SetMenu("הדפס הפקדה אחרונה", 8);
            menuindex = 1;
            maxmenu = 8;
            menupage = 1;

            currentmenu = MenuType.Attendant;

            MarkMenu(menuindex, true);
        }

        public void BuildAndSetShiftMenu()
        {

            SetMenu("בדיקה", 1);
            SetMenu("דוח X", 2);
            SetMenu("חסימת פיה",3);
            SetMenu("סגירת משמרת", 4);
            SetMenu("שעון כניסה", 5);
            SetMenu("שעון יציאה", 6);
            
            menuindex = 1;
            maxmenu = 6;
            menupage = 1;

            currentmenu = MenuType.ShiftMng;

            MarkMenu(menuindex, true);
        }

        public void BuildOtherMOPMenu()
        {

            SetMenu("TMC", 1);
            SetMenu("סלב", 2);
            SetMenu("פנגו", 3);
            
            menuindex = 1;
            maxmenu = 3;
            menupage = 1;

            currentmenu = MenuType.OtherMOP;

            MarkMenu(menuindex, true);
        }

        public void SetMenuAction()
        {
            MarkMenu(menuindex, false);
            ClearMenu();
            switch (currentmenu)
            {
                case MenuType.Attendant:
                    switch (menuindex)
                    {
                        case 1:
                            action = States.DrySale;
                            SetState(States.RequestAttn);
                            break;
                        case 2:
                            SetState(States.PrintingRecipt);
                            break;
                        case 3:
                            action = States.SafeDrop;
                            SetState(States.RequestAttn);
                            break;
                        case 4:
                            
                            BuildAndSetShiftMenu();
                            SetState(States.MenuMode);
                            break;
                        case 8:
                            SetState(States.PrintingRecipt);
                            break;
                    }
                break;

                case MenuType.ShiftMng:
                    switch (menuindex)
                    {
                        case 1:
                            action = States.TestFuel;
                            SetState(States.RequestMan);
                            break;
                        case 2:
                            if (CommErrTR.Checked)
                            {
                                backState = States.FSIdle;
                                SetState(States.Err_CommXReport);
                            }
                            else
                            {
                                action = States.XReport;
                                SetState(States.RequestAttn);
                            }
                            break;
                        case 3:
                            SetState(States.BlockNoz);
                            break;
                        case 4: //close shift
                            if (OpenTrans.Checked)
                            {
                                backState = States.FSIdle;
                                SetState(States.Err_OpenTrans);
                            }
                            else
                            if (FuelingNow.Checked)
                            {
                                backState = States.FSIdle;
                                SetState(States.Err_BusyPumps);
                            }
                            else
                                if (CommErrTR.Checked)
                                {
                                    backState = States.FSIdle;
                                    SetState(States.Err_CommShift);
                                }
                            else
                            {
                                backState = States.FSIdle;
                                SetState(States.CloseingShift);
                            }
                            break;
                        case 5: //clock in / out
                            SetState(States.ClockIn);
                            break;
                        case 6:
                            SetState(States.ClockOut);
                            break;
                    }
                break;                   
                case MenuType.OtherMOP:
                    action = States.OtherMOP;
                    SetState(States.RequestSerailMOP);
                break;
                        
            }
        }

    }
}
