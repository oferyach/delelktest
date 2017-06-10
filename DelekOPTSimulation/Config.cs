using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace DelekOPTSimulation
{
    public class DeviceData
    {
        public string cardnum;
        public double limit;
        public bool bEnabled;
        public string plate;
        public string type;
        public string prod;
    }



    
    public class Config
    {
        //config data
        public bool bSimulator;
        public bool bLogPoll = true;
        public string IP;
        public int port;
        public int Addr = 0x32;
        public double PPV1 = 1.0;
        public double PPV2 = 1.0;
        public string Product1 = "Prod1";
        public string Product2 = "Prod1";
        public string Units = "Units";
        public double flowrate = 60.0;
        public string LimitText = "Limit";
        public string PlateText = "Plate";
        public string LimitPerVolText = "Limit/Tnx";
        public string LimitPerMoneyText = "Money/Tnx";
        public int HOAuthDelay = 2000;
        public int PumpAuthDelay = 1500;


        public class Item
        {
            public int Code;
            public string Name;
            public double price;
        }

        public class ItemsCat
        {
            public string Name;
            public List<Item> items = new List<Item>();
        }

        static public List<Item> items = new List<Item>();
        static public List<ItemsCat> itemscat = new List<ItemsCat>();   

        public Config()
        {
            //read config file
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("Config/config.xml");

                if (xDoc != null)
                {
                    /*
                    string sim = xDoc.SelectSingleNode("Configuration/General/Simulator").InnerXml.ToString();
                    bSimulator = (sim == "true") ? true : false;
                    string poll = xDoc.SelectSingleNode("Configuration/General/LogPoll").InnerXml.ToString();
                    bLogPoll = (poll == "true") ? true : false;
                    //WGT data
                    IP = xDoc.SelectSingleNode("Configuration/General/WGT/IP").InnerXml.ToString();
                    port = Int32.Parse(xDoc.SelectSingleNode("Configuration/General/WGT/Port").InnerXml.ToString());
                    Addr = Convert.ToInt32(xDoc.SelectSingleNode("Configuration/General/WGT/Addr").InnerXml.ToString(), 16);
                    PPV1 = Double.Parse(xDoc.SelectSingleNode("Configuration/General/PPV1").InnerXml.ToString());
                    PPV2 = Double.Parse(xDoc.SelectSingleNode("Configuration/General/PPV2").InnerXml.ToString());
                    flowrate = Double.Parse(xDoc.SelectSingleNode("Configuration/General/FlowRate").InnerXml.ToString());
                    PumpAuthDelay = Int32.Parse(xDoc.SelectSingleNode("Configuration/General/PumpAuthDelay").InnerXml.ToString());
                    HOAuthDelay = Int32.Parse(xDoc.SelectSingleNode("Configuration/General/HOAuthDelay").InnerXml.ToString());
                    Product1 = xDoc.SelectSingleNode("Configuration/General/Prod1").InnerXml.ToString();
                    Product2 = xDoc.SelectSingleNode("Configuration/General/Prod2").InnerXml.ToString();
                    Units = xDoc.SelectSingleNode("Configuration/General/Units").InnerXml.ToString();

                    LimitText = xDoc.SelectSingleNode("Configuration/General/LimitText").InnerXml.ToString();
                    PlateText = xDoc.SelectSingleNode("Configuration/General/PlateText").InnerXml.ToString();
                    LimitPerVolText = xDoc.SelectSingleNode("Configuration/General/LimitPerVolText").InnerXml.ToString();
                    LimitPerMoneyText = xDoc.SelectSingleNode("Configuration/General/LimitPerMoneyText").InnerXml.ToString();
                    */

                    
                    
                    // Get items cat
                    //XmlNode node = xDoc.SelectSingleNode("Configuration/Devices").FirstChild.ChildNodes;
                    //xDoc.SelectSingleNode("Configuration/ItemsCat").ChildNodes.Item(0).SelectSingleNode("Items").SelectSingleNode("Item")
                    XmlNodeList node = xDoc.SelectNodes("Configuration/ItemsCat/Cat");
                    foreach (XmlNode node3 in node)
                    {                        
                        ItemsCat ic = new ItemsCat();
                        ic.items = new List<Item>();
                        ic.Name = node3.SelectSingleNode("Name").InnerText;

                        XmlNodeList cat = node3.SelectNodes("Items/Item");

                        foreach (XmlNode am in cat)
                        {

                            Item item = new Item();                            
                            item.Code = int.Parse(am.SelectSingleNode("Code").InnerText);
                            item.Name = am.SelectSingleNode("Name").InnerText;
                            item.price = double.Parse(am.SelectSingleNode("Price").InnerText);                                
                            ic.items.Add(item);                            
                        }

                        itemscat.Add(ic);
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading configuration file. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        
        static public void GetItemByCode(int Code,ref string Name, ref double itemprice)
        {
            Name = "";
            itemprice = 0.0;
                

            foreach (ItemsCat ic in itemscat)
            {
                Item item = ic.items.Find(x => x.Code == Code);
                if (item != null)
                {
                    Name = item.Name;
                    itemprice = item.price;
                    return;
                }
            }
            return;
        }
    }
}

