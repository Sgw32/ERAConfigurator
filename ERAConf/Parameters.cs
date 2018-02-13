using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using KBCsv;
using System.Windows.Forms;

namespace ERAConf
{
    class ParamValue
    {
        public ParamValue() { }

        public String defaultValue = "";
        public String value = "";
        public String description = "";
    }
    class Parameters
    {
        private static Parameters instance;
        public SortedDictionary<String, ParamValue> parameters;
        public SortedDictionary<String, ParamValue> baseParameters;
        public SortedDictionary<String, ComboBox> regCBs;
        private Parameters() 
        {
            parameters = new SortedDictionary<String, ParamValue>();
            baseParameters = new SortedDictionary<String, ParamValue>();
            regCBs = new SortedDictionary<String, ComboBox>();
        }

        public static Parameters Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new Parameters();
                }
                return instance;
            }
        }

        public void removeBaseParameter(String name)
        {
            baseParameters.Remove(name);
        }
        public void addBaseParameter(String name,String defaultValue)
        {
            ParamValue val = new ParamValue();
            val.defaultValue = defaultValue;
            baseParameters.Add(name, val);
        }
        public void addBaseParameter(String name, String defaultValue, String description)
        {
            ParamValue val = new ParamValue();
            val.defaultValue = defaultValue;
            val.description = description;
            baseParameters[name]= val;
        }

        public void writeParameters()
        {
            try 
            {
                using (var streamWriter = new StreamWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/param_base.csv"))
                using (var writer = new CsvWriter(streamWriter))
                {
                    writer.ForceDelimit = true;
                    writer.WriteRecord("name", "description", "defaultValue");

                    foreach (KeyValuePair<String, ParamValue> val in baseParameters)
                    {
                        writer.WriteRecord(val.Key, val.Value.description, val.Value.defaultValue);
                    }
                }
            }
            catch(Exception e)
            {

            }
           
        }
        public void readParameters()
        {
            try
            {
                using (var streamReader = new StreamReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"/param_base.csv"))
                using (var reader = new CsvReader(streamReader))
                {
                    var dataRecord = reader.ReadDataRecord();
                    while (reader.HasMoreRecords)
                    {
                        dataRecord = reader.ReadDataRecord();
                        ParamValue val = new ParamValue();
                        val.defaultValue = dataRecord[2];
                        val.value = dataRecord[2];
                        val.description = dataRecord[1];
                        baseParameters[dataRecord[0]] = val;
                        parameters[dataRecord[0]] = val;
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
        public void loadBaseFromFile(String file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    // Read the stream to a string, and write the string to the console.
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        //Remove comment
                        int cmt = line.IndexOf('#');
                        if (cmt > -1)
                        {
                            line.Remove(cmt);
                        }
                        int zap = line.IndexOf(',');
                        if (zap > -1)
                        {
                            //Process parameter
                            String name = line.Substring(0, zap);
                            String defValue = line.Substring(zap+1);
                            addBaseParameter(name, defValue);
                        }
                    }
                    
                    
                }
            }
            catch (Exception e)
            {

            }
        }

        public void addEditParameter(string name, string value)
        {
            ParamValue val = new ParamValue();
            val.value = value;
            parameters[name]=val;
        }

        public void loadEditFromFile(String file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    // Read the stream to a string, and write the string to the console.
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        //Remove comment

                        int cmt = line.IndexOf('#');
                        if (cmt > -1)
                        {
                            //line.Remove(cmt);
                            cmt = line.IndexOf("DATA:");
                            if (cmt > -1)
                            {
                                line = line.Remove(0, cmt+5);
                                cmt = line.IndexOf(':');
                                if (cmt > -1)
                                {
                                    //line.Remove(0, line.Length - cmt);
                                    String name = line.Substring(0, cmt);
                                    String defValue = line.Substring(cmt+1);
                                    int selectId = 0;
                                    int.TryParse(defValue, out selectId);
                                    if (regCBs.ContainsKey(name))
                                    {
                                        regCBs[name].SelectedIndex = selectId;
                                    }
                                }
                            }
                        }

                        int zap = line.IndexOf(',');
                        if (zap > -1)
                        {
                            //Process parameter
                            String name = line.Substring(0, zap);
                            String defValue = line.Substring(zap + 1);
                            addEditParameter(name, defValue);
                        }
                    }


                }
            }
            catch (Exception e)
            {

            }
        }

        public void saveEditFromFile(String file)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file))
                {
                    foreach (KeyValuePair<String, ComboBox> val in regCBs)
                    {
                        sr.WriteLine("#DATA:" + val.Value.Name + ":" + val.Value.SelectedIndex.ToString());
                    }
                    foreach (KeyValuePair<String, ParamValue> val in parameters)
                    {
                        sr.WriteLine(val.Key + "," + val.Value.value);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
