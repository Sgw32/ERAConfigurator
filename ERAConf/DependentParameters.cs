using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERAConf
{
    class DependentParameters
    {
        private static DependentParameters instance;
        public List<ComboBox> updateCombos;
        private DependentParameters()
        {
            mDepParams = new SortedDictionary<String,ParamValue>();
            updateCombos = new List<ComboBox>();
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
            refreshComboBoxes();
        }
        public void refreshComboBoxes()
        {
            foreach (ComboBox cb in updateCombos)
            {
                cb.Items.Clear();
            }
            foreach (ComboBox cb in updateCombos)
            {
                foreach (KeyValuePair<String, ERAConf.ParamValue> kp in mDepParams)
                {
                    cb.Items.Add(kp.Key);
                    cb.SelectedIndex = 0;
                }
                
            }
        }
        public void copyDependentParameters(APCtrlVariant variant)
        {
            mDepParams = new SortedDictionary<String, ERAConf.ParamValue>(variant.varDepParams);
            refreshComboBoxes();
        }
        public void clearParams()
        {
            mDepParams.Clear();
        }
    }
}
