using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Engine
{
    public class CWLogging : Singleton<CWLogging>
    {
        const bool DebugOn = true;
        const bool WarningOn = true;
        const bool ErrorOn = true;
        private Actuator actuator;

        // Encapsulate actual logging.
        private class Actuator
        {
            public void printLog(String msg)
            {
                Debug.Log(msg);
            }
        }

        public CWLogging()
        {
            actuator = new Actuator();
        }

        public void LogDebug( String msg )
        {
            if (!DebugOn)
                return;
            actuator.printLog("DEBUG: " + msg);
        }
        public void LogWarning(String msg )
        {
            if (!WarningOn)
                return;
            actuator.printLog("WARNING: " + msg);
        }

        public void LogError( String msg )
        {
            if (!ErrorOn)
                return;
            actuator.printLog("ERROR: " + msg);
        } 
    }
}
