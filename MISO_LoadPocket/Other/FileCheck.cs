using System;
namespace MISO_LoadPocket.Other
{
    public class FileCheck
    {
        public static bool Check(string finalLocation){
            var date = DateTime.Now.ToString("MMddyyyy");
            var ASRU = System.IO.File.Exists(finalLocation + "ALL_SOUTH_REGION_UNITS_" + date + ".CSV");
            var LPS = System.IO.File.Exists(finalLocation + "LOAD_POCKET_SUMMARY_" + date + ".CSV");
            var LAA = System.IO.File.Exists(finalLocation + "LOOK_AHEAD_AVAIL_" + date + ".CSV");
            var LAS = System.IO.File.Exists(finalLocation + "LOOK_AHEAD_SUMMARY_" + date + ".CSV");
            var UP = System.IO.File.Exists(finalLocation + "UNIT_PLAN_" + date + ".CSV");
            var check =  ASRU && LPS && LAA && LPS && UP;
            return check;
        }
    }
}
