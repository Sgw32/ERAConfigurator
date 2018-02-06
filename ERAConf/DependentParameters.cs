using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAConf
{
    class DependentParameters
    {
        private static DependentParameters instance;

        private DependentParameters()
        {
            mDepParams = new SortedDictionary<string,ParamValue>();
        }
        public static DependentParameters Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new DependentParameters();
                }
                return instance;
            }
        }

        public SortedDictionary<String, ERAConf.ParamValue> mDepParams;
        public void addDependentParameter(String param, ERAConf.ParamValue value)
        {
            mDepParams[param] = value;
        }
        public void clearParams()
        {
            mDepParams.Clear();
        }
    }
}
