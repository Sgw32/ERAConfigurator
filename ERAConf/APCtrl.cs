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
            value.saveSettings(System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                + "/base/" + ctrlName + "_" + value.variantName + ".csv");
            saveVariantTable();
        }

        public void addVariantWithoutSave(String param, ERAConf.APCtrlVariant value)
        {
            variants[param] = value;
        }

        public void addVariant(int sysnum,String param, ERAConf.APCtrlVariant value)
        {
            variants[param] = value;
            value.saveSettings(System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                + "/base/" + ctrlName + "_" + value.variantName + ".csv");
        }

        public void saveVariantTable()
        {
            using (var streamWriter = new StreamWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                + "/base/" + ctrlName + "_table.csv")) 
            using (var writer = new CsvWriter(streamWriter))
            {
                writer.ForceDelimit = true;
                writer.WriteRecord(ctrlName);
                foreach (KeyValuePair<string, ERAConf.APCtrlVariant> val in variants)
                {
                    List<string> vars = new List<string>();
                    vars.Add(val.Key);
                    foreach (KeyValuePair<string, ComboBox> cb in val.Value.regCBs)
                    {
                        vars.Add(cb.Key);
                        vars.Add(cb.Value.Name);
                    }
                    
                    writer.WriteRecord(vars.ToArray());
                }
            }
        }

        public bool hasVariant(String name)
        {
            return variants.ContainsKey(name);
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
