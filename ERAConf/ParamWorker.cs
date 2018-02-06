using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAConf
{
    class ParamWorker
    {
        private static ParamWorker instance;

        private ParamWorker() { }

        public static ParamWorker Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ParamWorker();
                }
                return instance;
            }
        }


    }
}
