using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SMBIOS_SKUNo_Gen
{

    public static class StringData
    {
        const int NO_ANS = -1;
        public const string AHCI_RAID_STR = "RAID or AHCI?";
        public const string NOTE_STR = "Please refer to BIOS release notes";
        public static string DEFAULT_SM_SKU = "0000000000000000";

        public delegate void ModifyByte(string buttonName);

        public static Dictionary<string, ModifyByte> ModifySKU
        {
            get
            {
                return new Dictionary<string, ModifyByte>() 
        {
            {"Q1Modify",Q1ModifyFunction},
            {"Q2Modify",Q2ModifyFunction},
            {"Q3Modify",Q3ModifyFunction},
            {"Q4Modify",Q4ModifyFunction},
            {"Q5Modify",Q5ModifyFunction},
            {"Q6Modify",Q6ModifyFunction},
            {"DoNothing", EmptyFunction}
        };
            }

        }

        public static List<List<Selection>> getButtonData
        {
            get
            {
                return new List<List<Selection>>() { 
                new List<Selection>(){ new Selection(){Label = "Yes", MethodName = "Q1Modify"},new Selection(){Label = "No", MethodName = "Q1Modify"}},//q1
                new List<Selection>(){ new Selection(){Label = "1L", MethodName = "Q2Modify"},
                                       new Selection(){Label = "3L",MethodName = "Q2Modify"},
                                       new Selection(){Label = "2L", MethodName = "Q2Modify"},
                                       new Selection(){Label = "No", MethodName = "Q2Modify"}
                                      },//q2
                new List<Selection>(){ new Selection(){Label = "Yes", MethodName = "Q3Modify"},new Selection(){Label = "No", MethodName = "Q3Modify"}},//q3
                new List<Selection>(){ new Selection(){Label = "Yes", MethodName = "Q4Modify"},new Selection(){Label = "No", MethodName = "Q4Modify"}},//q4
                new List<Selection>(){ new Selection(){Label = "Yes", MethodName = "Q5Modify"},new Selection(){Label = "No", MethodName = "Q5Modify"}},//q5
                new List<Selection>(){ new Selection(){Label = "Yes", MethodName = "Q6Modify"},new Selection(){Label = "No", MethodName = "Q6Modify"}},//q6           
            };
            }
        }

        public static List<string> getQuestionData
        {
            get
            {
                return new List<string>(new string[] { 
                "Q1:Chassis intrusion support?",
                "Q2:1L, 2L or 3L system?",
                "Q3:Card reader installed?",
                "Q4:Optional USB3.0 module installed?",
                "Q5:Optional USB2.0 module installed?",
                "Q6:System fan installed?"          
            });
            }
        }
        public static List<string> getAnsData
        {
            get
            {
                return new List<string>(){
                "FreeDOS/Legacy/AHCI \r\nSKU ID: 0001h \r\nFDOSMBAH.BAT",
                "Win7 64bit/EFI/RAID \r\nSKU ID: 0006h \r\nWIN7GPRD.BAT",
                "Win7 64bit/EFI/AHCI \r\nSKU ID: 0002h \r\nWIN7GPAH.BAT",
                "Win7 64bit/Legacy/RAID \r\nWin7 32bit/Legacy/RAID \r\nSKU ID: 0007h \r\nWIN7MBRD.BAT",
                "Win7 64bit/Legacy/AHCI \r\nWin7 32bit/Legacy/AHCI \r\nSKU ID: 0003h \r\nWIN7MBAH.BAT",
                "Win10/EFI/RAID \r\nSKU ID: 0008h \r\nWINAGPRD.BAT",
                "Win10/EFI/AHCI \r\nSKU ID: 0004h \r\nWINAGPAH.BAT",
                "Win10 Pro/EFI/RAID \r\nSKU ID: 000Bh \r\nWAPRGPRD.BAT",
                "Win10 Pro/EFI/AHCI \r\nSKU ID: 000Ah \r\nWAPRGPAH.BAT",
                "Win7 64bit/Win7 32bit/Legacy/RAID \r\nSKU ID: 0007h \r\nWIN7MBRD.BAT",
                "Win7 64bit/Win7 32bit/Legacy/AHCI \r\nSKU ID: 0003h \r\nWIN7MBAH.BAT",
                "Win10/Win7/EFI/RAID \r\nSKU ID: 0009h \r\nWAW7GPRD.BAT",
                "Win10/Win7/EFI/AHCI \r\nSKU ID: 0005h \r\nWAW7GPAH.BAT",
                "Linpus Linux/EFI/AHCI \r\nSKU ID: 000Ch \r\nLNUXGPAH.BAT"

            };
            }
        }
        public static Dictionary<string, BUTTON_POSIOION> ButtonType
        {
            get
            {
                return new Dictionary<string, BUTTON_POSIOION>()
                {
                    {"LButton",BUTTON_POSIOION.left},
                    {"RButton",BUTTON_POSIOION.right},
                    {"MButton",BUTTON_POSIOION.mid},
                    {"M2Button",BUTTON_POSIOION.mid2},
                    {"UndoButton",BUTTON_POSIOION.undo}
                };
            }
        }

        public static void Q1ModifyFunction(string buttonName)
        {
            if ("Yes" == buttonName)
                DEFAULT_SM_SKU = '1' + DEFAULT_SM_SKU.Substring(1);
            else
                DEFAULT_SM_SKU = '0' + DEFAULT_SM_SKU.Substring(1);
        }

        public static void Q2ModifyFunction(string buttonName)
        {
            switch (buttonName)
            {
                case "1L":
                    DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 1) + "00" + DEFAULT_SM_SKU.Substring(3);
                    break;
                case "2L":
                    DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 1) + "10" + DEFAULT_SM_SKU.Substring(3);
                    break;
                case "3L":
                    DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 1) + "01" + DEFAULT_SM_SKU.Substring(3);
                    break;
                case "No":
                    DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 1) + "00" + DEFAULT_SM_SKU.Substring(3);
                    break;
            }
        }

        public static void Q3ModifyFunction(string buttonName)
        {
            if ("Yes" == buttonName)
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 3) + "1" + DEFAULT_SM_SKU.Substring(4);
            else
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 3) + "0" + DEFAULT_SM_SKU.Substring(4);
        }

        public static void Q4ModifyFunction(string buttonName)
        {
            if ("Yes" == buttonName)
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 4) + "1" + DEFAULT_SM_SKU.Substring(5);
            else
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 4) + "0" + DEFAULT_SM_SKU.Substring(5);
        }

        public static void Q5ModifyFunction(string buttonName)
        {
            if ("Yes" == buttonName)
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 5) + "1" + DEFAULT_SM_SKU.Substring(6);
            else
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 5) + "0" + DEFAULT_SM_SKU.Substring(6);
        }

        public static void Q6ModifyFunction(string buttonName)
        {
            if ("Yes" == buttonName)
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 6) + "1" + DEFAULT_SM_SKU.Substring(7);
            else
                DEFAULT_SM_SKU = DEFAULT_SM_SKU.Substring(0, 6) + "0" + DEFAULT_SM_SKU.Substring(7);      
        }

        public static void EmptyFunction(string buttonName)
        {

        }

        public static void GenBatFile(ItemCollection history)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SKUNoDOS.bat", false))
            {
                file.WriteLine("AMIDEDOS/ SK " + "\"" + DEFAULT_SM_SKU + "\"");            
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SKUNoEFI.nsh", false))
            {
                file.WriteLine("AMIDEEFIx64/ SK " + "\"" + DEFAULT_SM_SKU + "\"");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SKUNoWin.bat", false))
            {
                file.WriteLine("AMIDEWINx64/ SK " + "\"" + DEFAULT_SM_SKU + "\"");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Config.txt", false))
            {
                for (int i = 0; i < history.Count; i += 2)
                    file.WriteLine(history.GetItemAt(i).ToString().Replace("Q" + (i / 2 + 1).ToString() + ":", "").Replace("?", "") + history.GetItemAt(i + 1).ToString().Replace("A:", ": "));
            }
        }

    }
}
