using KBCsv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERAConf
{
    class APCtrlWorker
    {
        private static APCtrlWorker instance;
        public SortedDictionary<int, APCtrl> mSystems;
        public Control.ControlCollection fCtl;
        public SortedDictionary<string, string> taggedEdits;
        public int curSysNum;
        private APCtrlWorker() 
        { 
            mSystems = new SortedDictionary<int,APCtrl>();
            taggedEdits = new SortedDictionary<string, string>();
            curSysNum = 0;
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
            curSysNum = sysnum;
            //DependentParameters.Instance.clearParams();
        }
        private ArrayList FindInternal(string key, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
        {
            if ((controlsToLookIn == null) || (foundControls == null))
            {
                return null;
            }
            try
            {
                for (int i = 0; i < controlsToLookIn.Count; i++)
                {
                    if ((controlsToLookIn[i] != null) && (controlsToLookIn[i].Name == key))
                    {
                        foundControls.Add(controlsToLookIn[i]);
                    }
                }
                if (!searchAllChildren)
                {
                    return foundControls;
                }
                for (int j = 0; j < controlsToLookIn.Count; j++)
                {
                    if (((controlsToLookIn[j] != null) && (controlsToLookIn[j].Controls != null)) && (controlsToLookIn[j].Controls.Count > 0))
                    {
                        foundControls = this.FindInternal(key, searchAllChildren, controlsToLookIn[j].Controls, foundControls);
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
            return foundControls;
        }
        
        public void transferValuesToTextBoxes()
        {
            foreach (KeyValuePair<string, ERAConf.ParamValue> val in Parameters.Instance.parameters)
            {
                if (taggedEdits.ContainsKey(val.Key))
                {
                    String controlName = taggedEdits[val.Key];
                    ArrayList alcb = new ArrayList();
                    alcb = FindInternal(controlName, true, fCtl, alcb);
                    if (alcb.Count > 0)
                    {
                        TextBox cb = alcb[0] as TextBox;
                        cb.Text = val.Value.value;
                    }
                }
            }
                
            foreach (KeyValuePair<string, ERAConf.ParamValue> val in DependentParameters.Instance.mDepParams)
            {
                if (taggedEdits.ContainsKey(val.Key))
                {
                    String controlName = taggedEdits[val.Key];
                    ArrayList alcb = new ArrayList();
                    alcb = FindInternal(controlName, true, fCtl, alcb);
                    if (alcb.Count > 0)
                    {
                        TextBox cb = alcb[0] as TextBox;
                        cb.Text = val.Value.value;
                    }
                }
            }
        }
        public void loadACTLTables()
        {
            foreach (KeyValuePair<int, APCtrl> val in mSystems)
            {
                int systype = val.Key;
                APCtrl system = val.Value;
                string tableFilename = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                + "/base/" + system.ctrlName + "_table.csv";
                if (File.Exists(tableFilename))
                {
                    //Найдена таблица вариантов
                    using (var streamReader = new StreamReader(tableFilename))
                    using (var reader = new CsvReader(streamReader))
                    {
                        var dataRecord = reader.ReadDataRecord();
                        if (dataRecord[0]==system.ctrlName)
                        {
                            while (reader.HasMoreRecords)
                            {
                                dataRecord = reader.ReadDataRecord(); //прочитаем вариант
                                string varname = dataRecord[0]; //общее название
                                string variantFilename = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                                        + "/base/" + system.ctrlName + "_" + varname + ".csv";
                                //проверим, есть ли описание варианта:
                                using (var streamReader2 = new StreamReader(variantFilename))
                                using (var reader2 = new CsvReader(streamReader2))
                                {
                                    var dataRecord2 = reader2.ReadDataRecord();
                                    if (dataRecord2[0] == varname) //тот ли вариант?
                                    {
                                        //Тот, надо добавлять.
                                        DependentParameters.Instance.clearParams();
                                        APCtrlVariant variant = new APCtrlVariant();
                                        variant.variantName = varname;
                                        variant.variantPicture = dataRecord2[1];
                                        for (int i = 1; i != dataRecord.Count(); i=i+2)
                                        {
                                            String controlValue = dataRecord[i];
                                            String controlName = dataRecord[i + 1];
                                            ArrayList alcb = new ArrayList();
                                            alcb = FindInternal(controlName, true, fCtl, alcb);
                                            ComboBox cb = alcb[0] as ComboBox;
                                            cb.Text=controlValue;
                                            variant.addComboBox(cb);
                                        }
                                        while (reader2.HasMoreRecords)
                                        {
                                            
                                            dataRecord2 = reader2.ReadDataRecord();
                                            String parameterName = dataRecord2[0];
                                            String value = dataRecord2[1];
                                            String description = dataRecord2[2];
                                            String defaultValue = dataRecord2[3];
                                            ParamValue pval = new ParamValue();
                                            pval.defaultValue = defaultValue;
                                            pval.description = description;
                                            pval.value = value;
                                            DependentParameters.Instance.addDependentParameter(parameterName, pval);
                                        }
                                        variant.transferFromGlobalDParams();
                                        system.addVariantWithoutSave(variant.variantName, variant); //add or replace
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
