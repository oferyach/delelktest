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
        //menu
        public int menuindex = 1;
        public int maxmenu = 6;
        public int selectmenu = 1;
        public int menupage = 1;
        public int maxpage = 1;
        public enum MenuType
        {
            None,
            Attendant,
            ShiftMng,
            DryList,
            OtherMOP,
            TBD
        };

        public MenuType currentmenu = MenuType.None;
        public void MarkMenu(int index, bool bMark)
        {
            int i;

            i = index % 9;
            invenco.MarkMenu(index, bMark);
        }

        public void SetMenu(string text, int index)
        {
            int i;
            i = index % 9;
            invenco.SetMenu(text, index);
        }

        public void ClearMenu()
        {
            
            invenco.ClearMenu();
            
        }

        
        public void StartMenu(MenuType menu,Boolean bStart)
        {
            currentmenu = menu;

            switch (currentmenu)
            {
                case MenuType.Attendant:
                    if (bStart) 
                        menupage = 1;
                    maxpage = 2;
                    BuildAndSetAttnMenu();
                    break;
                case MenuType.ShiftMng:
                    if (bStart)
                        menupage = 1;
                    maxpage = 1;
                    BuildAndSetShiftMenu();
                    break;
                case MenuType.OtherMOP:
                    if (bStart)
                        menupage = 1;
                    maxpage = 1;
                    BuildOtherMOPMenu();
                    break;
            };
        }

        public void BuildAndSetAttnMenu()
        {
            //the menu is set - TBD according to actual action
            if (menupage == 1)
            {
                SetMenu("מכירות", 1);
                SetMenu("קבלה אחרונה", 2);
                SetMenu("הפקדה לכספת", 3);
                SetMenu("ניהול משמרות", 4);
                SetMenu("התעלם מדלקן", 5);
                SetMenu("אישור דלקן חריג", 6);
                SetMenu("החזרות", 7);
                SetMenu("הדפס הפקדה אחרונה", 8);
                maxmenu = 8;
            }
            if (menupage == 2)
            {
                SetMenu("ניקוז אויר", 1);
                SetMenu("Page 2/2", 2);
                maxmenu = 2;
            }
            menuindex = 1;
            


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
                    switch (menupage)
                    {
                        case 1:
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
                                case 5: //ignore VIS
                                    break;
                                case 6: //VIS excpetion
                                    break;
                                case 7: //returns
                                    break;
                                case 8:
                                    SetState(States.PrintingRecipt);
                                    break;
                            }
                            break;
                        case 2:
                            switch (menuindex)
                            {
                                case 1:
                                    action = States.AirDrange;
                                    SetState(States.AirDrange);
                                    break;
                                case 2:
                                    SetState(States.PrintingRecipt);
                                    break;
                            }
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
