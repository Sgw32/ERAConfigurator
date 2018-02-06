using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAConf
{
    class APCtrl
    {
        public APCtrl() 
        {
            variants = new SortedDictionary<string, ERAConf.APCtrlVariant>();
        }
        public SortedDictionary<String, ERAConf.APCtrlVariant> variants;
        
        public String ctrlName;
        public int systemType;

        public void addVariant(String param, ERAConf.APCtrlVariant value)
        {
            variants[param] = value;
        }

        public APCtrlVariant getVariantByName(String param)
        {
            return variants[param];
        }

        public void updateControls()
        {
            foreach (KeyValuePair<string, ERAConf.APCtrlVariant> val in variants)
            {
                val.Value.clearComboBoxes();
            }
            foreach (KeyValuePair<string, ERAConf.APCtrlVariant> val in variants)
            {
                val.Value.addToComboBoxes();
            }
        }
    }
}
