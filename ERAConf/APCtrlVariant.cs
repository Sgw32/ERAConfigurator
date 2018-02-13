using KBCsv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERAConf
{
    class APCtrlVariant
    {
        public APCtrlVariant() 
        { 
            regCBs = new SortedDictionary<string,ComboBox>();
            varDepParams = new SortedDictionary<String, ERAConf.ParamValue>();
        }
        public List<int> mKeys;
        public SortedDictionary<string, ComboBox> regCBs;
        public SortedDictionary<String, ERAConf.ParamValue> varDepParams;
        public void saveSettings(String fileName)
        {
            try
            {
                using (var streamWriter = new StreamWriter(fileName))
                using (var writer = new CsvWriter(streamWriter))
                {
                    writer.ForceDelimit = true;
                    writer.WriteRecord(variantName, variantPicture);
                    foreach (KeyValuePair<String, ParamValue> val in varDepParams)
                    {
                        writer.WriteRecord(val.Key, val.Value.value, val.Value.description, val.Value.defaultValue);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        public void loadSettings(String fileName)
        {

        }

        public void saveSettings()
        {

        }

        public void transferFromGlobalDParams()
        {
            varDepParams = new SortedDictionary<String, ERAConf.ParamValue>(DependentParameters.Instance.mDepParams);
        }

        public void submitParameters()
        {
            foreach (KeyValuePair<String, ERAConf.ParamValue> val in varDepParams)
            {
                Parameters.Instance.addEditParameter(val.Key, val.Value.value); //Обновление всех параметров
            }
            DependentParameters.Instance.copyDependentParameters(this);
        }

        public String variantName;
        public String variantPicture;
        public void addKeySettings(List<int> keys)
        {
            mKeys = new List<int>(keys);
        }

        public void addComboBox(ComboBox cb)
        {
            regCBs[cb.Text] = cb;
        }

        public void processEntered()
        {

        }

        public void clearComboBoxes()
        {
            foreach (KeyValuePair<string, ComboBox> val in regCBs)
            {
                val.Value.Items.Clear();
            }
        }
        public void addToComboBoxes()
        {
            foreach (KeyValuePair<string, ComboBox> val in regCBs)
            {
                val.Value.Items.Add(val.Key);
            }
        }
    }
}
