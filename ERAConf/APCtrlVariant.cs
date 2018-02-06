using System;
using System.Collections.Generic;
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
        }
        public List<int> mKeys;
        public SortedDictionary<string, ComboBox> regCBs;
        public void saveSettings(String fileName)
        {

        }
        public void loadSettings(String fileName)
        {

        }

        public void saveSettings()
        {

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
