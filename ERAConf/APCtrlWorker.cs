using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAConf
{
    class APCtrlWorker
    {
        private static APCtrlWorker instance;
        public SortedDictionary<int, APCtrl> mSystems;
        private APCtrlWorker() 
        { 
            mSystems = new SortedDictionary<int,APCtrl>();
        }

        public APCtrl curACTL;
        public static APCtrlWorker Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new APCtrlWorker();
                }
                return instance;
            }
        }

        public void makeNewAPCtrl()
        {
            curACTL = new APCtrl();
        }
        public void saveCurAPCtrlToSystem()
        {
            mSystems[curACTL.systemType] = curACTL;
        }
        public void selectSystem(int sysnum)
        {
            curACTL = mSystems[sysnum];
            DependentParameters.Instance.clearParams();
        }
    }
}
