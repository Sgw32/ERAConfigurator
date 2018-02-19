using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using GlobalizedWizard;
using System.Windows.Documents;
using System.IO;
using KBCsv;

namespace ERAConf
{
    public partial class Form1 : Form
    {
        private bool writeLock;
        public Form1()
        {
            InitializeComponent();
        }

        private void визуализироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* var newWindow = new GlobalizedWizard.ApplicationMainWindow();
            newWindow.Show();*/
            
        }

        private void updateListBox()
        {
            //listBox1.Items.Clear();
            /*SortedDictionary<String, ParamValue> baseP = Parameters.Instance.baseParameters;
            if (baseP.Count>0)
            {
                foreach (KeyValuePair<String, ParamValue> val in baseP)
                {
                    listBox1.Items.Add(val.Key);
                }
            }
            foreach (String l in listBox1.Items)
            {
                listBox2.Items.Add(l);
            }*/
        }

        private void updateEditListBox()
        {
            listBox2.Items.Clear();
            SortedDictionary<String, ParamValue> baseP = Parameters.Instance.parameters;
            if (baseP.Count > 0)
            {
                foreach (KeyValuePair<String, ParamValue> val in baseP)
                {
                    listBox2.Items.Add(val.Key);
                }
            }
        }


        private void импортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Parameters.Instance.loadBaseFromFile(openFileDialog1.FileName);
                updateListBox();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    Parameters.Instance.loadEditFromFile(openFileDialog1.FileName);
                    updateEditListBox();
                    APCtrlWorker.Instance.transferValuesToTextBoxes();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Parameters.Instance.saveEditFromFile(saveFileDialog1.FileName);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void makeACTLS()
        {
            APCtrlWorker.Instance.makeNewAPCtrl();
            APCtrlWorker.Instance.curACTL.ctrlName = "sensorSetup";
            APCtrlWorker.Instance.curACTL.systemType = 0;
            APCtrlWorker.Instance.saveCurAPCtrlToSystem();

            APCtrlWorker.Instance.makeNewAPCtrl();
            APCtrlWorker.Instance.curACTL.ctrlName = "dvsSetup";
            APCtrlWorker.Instance.curACTL.systemType = 1;
            APCtrlWorker.Instance.saveCurAPCtrlToSystem();

            APCtrlWorker.Instance.makeNewAPCtrl();
            APCtrlWorker.Instance.curACTL.ctrlName = "motoSetup";
            APCtrlWorker.Instance.curACTL.systemType = 2;
            APCtrlWorker.Instance.saveCurAPCtrlToSystem();
        }

        private void transferToEditTextBoxes()
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            writeLock = true;
            //listBox1.Items.Clear();
            listBox2.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            Parameters.Instance.regCBs[comboBox1.Name] = comboBox1;
            Parameters.Instance.regCBs[comboBox2.Name] = comboBox2;
            Parameters.Instance.regCBs[comboBox3.Name] = comboBox3;
            Parameters.Instance.regCBs[comboBox4.Name] = comboBox4;
            Parameters.Instance.regCBs[comboBox5.Name] = comboBox5;
            Parameters.Instance.regCBs[comboBox6.Name] = comboBox6;
            Parameters.Instance.regCBs[comboBox7.Name] = comboBox7;

            DependentParameters.Instance.updateCombos.Add(comboBox7);
            DependentParameters.Instance.updateCombos.Add(comboBox9);
            //Parameters.Instance.readParameters(); 
            updateListBox();
            makeACTLS();
            APCtrlWorker.Instance.fCtl = this.Controls;
            loadVariants();
            APCtrlWorker.Instance.taggedEdits["BATT_AMP_OFFSET"] = "textBox11";
            APCtrlWorker.Instance.taggedEdits["BATTH_SNHT_RAT"] = "textBox15";
            APCtrlWorker.Instance.taggedEdits["BATT_VOLT_MULT"] = "textBox14";
            APCtrlWorker.Instance.taggedEdits["BATT_AMP_PERVOLT"] = "textBox13";
            APCtrlWorker.Instance.taggedEdits["BATT_AMP_OFFSET"] = "textBox11";
            APCtrlWorker.Instance.taggedEdits["Q_M_SV_SCALE"] = "textBox28";
            APCtrlWorker.Instance.taggedEdits["Q_M_SV_LIMIT"] = "textBox32";
            APCtrlWorker.Instance.taggedEdits["Q_M_ELEVON_POWER"] = "textBox20";
            APCtrlWorker.Instance.taggedEdits["Q_M_PITCH_SPEED"] = "textBox21";
            APCtrlWorker.Instance.taggedEdits["Q_M_THST_HOVER"] = "textBox22";
            APCtrlWorker.Instance.taggedEdits["Q_M_SPOOL_TIME"] = "textBox23";
            APCtrlWorker.Instance.taggedEdits["Q_M_THR_MAX"] = "textBox18";
            APCtrlWorker.Instance.taggedEdits["Q_M_SPIN_MAX"] = "textBox17";
            APCtrlWorker.Instance.taggedEdits["Q_M_SPIN_MIN"] = "textBox16";

            APCtrlWorker.Instance.taggedEdits["BATTH_TEMP_CHK"] = "textBox26";
            APCtrlWorker.Instance.taggedEdits["BATTH_CLR_MAX"] = "textBox31";
            APCtrlWorker.Instance.taggedEdits["BATTH_CHG_MAX"] = "textBox27";
            APCtrlWorker.Instance.taggedEdits["BATTH_TEMP_MAX"] = "textBox25";
            APCtrlWorker.Instance.taggedEdits["BATTH_TEMP_MIN"] = "textBox24";
            APCtrlWorker.Instance.taggedEdits["BATTH_THR_MIN"] = "textBox29";
            APCtrlWorker.Instance.taggedEdits["BATTH_THR_MAX"] = "textBox30";
            APCtrlWorker.Instance.transferValuesToTextBoxes();
            writeLock = false;
            Parameters.Instance.loadEditFromFile("base.param");
            updateEditListBox();
            APCtrlWorker.Instance.transferValuesToTextBoxes();
        }

        private void loadVariants()
        {
            APCtrlWorker.Instance.loadACTLTables();
            redrawVariants(0);
            redrawVariants(1);
            redrawVariants(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          //  Parameters.Instance.removeBaseParameter(listBox1.SelectedValue.ToString());
          //  updateListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*String description = textBox4.Text;
            String defaultValue = textBox3.Text;
            String name = textBox2.Text;
            Parameters.Instance.addBaseParameter(name, defaultValue, description);
            updateListBox();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parameters.Instance.writeParameters();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*String name = listBox1.SelectedItem.ToString();
            ParamValue val = Parameters.Instance.baseParameters[listBox1.SelectedItem.ToString()];
            String description = val.description;
            String defaultValue = val.defaultValue;
            textBox4.Text = description;
            textBox3.Text = defaultValue;
            textBox2.Text = name;*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String value = textBox1.Text;
            String name = listBox2.SelectedItem.ToString();
            Parameters.Instance.addEditParameter(name, value);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*ParamValue val = new ParamValue();
            val.value=textBox6.Text;
            APCtrlWorker.Instance.curACTL.addDependentParameter(comboBox8.Text, val);*/
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            APCtrlWorker.Instance.selectSystem(0);  //sensorSetup
        }
        public void redrawVariants(int sysnum)
        {
            APCtrlWorker.Instance.selectSystem(sysnum); //sensorSetup
            APCtrl ss = APCtrlWorker.Instance.curACTL;
            String varText = comboBox1.Text;
            ss.updateControls();
        }
        public void addVariant(int sysnum,String variantName)
        {
            APCtrlWorker.Instance.selectSystem(sysnum); //sensorSetup
            APCtrl ss = APCtrlWorker.Instance.curACTL;
            APCtrlVariant variant = new APCtrlVariant();
            switch (sysnum)
            {
                case 0:
                    variant.addComboBox(comboBox1);
                    variant.variantPicture = textBox8.Text;
                    break;
                case 1:
                    variant.addComboBox(comboBox2);
                    variant.addComboBox(comboBox3);
                    variant.variantPicture = textBox8.Text;
                    break;
                case 2:
                    variant.addComboBox(comboBox4);
                    variant.addComboBox(comboBox5);
                    variant.addComboBox(comboBox6);
                    variant.addComboBox(comboBox10);
                    variant.variantPicture = textBox10.Text;
                    break;
                default:
                    break;
            }
            variant.variantName = variantName;
            variant.transferFromGlobalDParams();
            variant.submitParameters();
            ss.addVariant(variant.variantName, variant); //add or replace
            redrawVariants(sysnum);
        }

        void chooseVariant(int sysnum,String variantName)
        {
            APCtrlWorker.Instance.selectSystem(sysnum); //sensorSetup
            APCtrl ss = APCtrlWorker.Instance.curACTL;
            if (ss.hasVariant(variantName))
            {
                APCtrlVariant variant = ss.getVariantByName(variantName);
                variant.submitParameters();
                switch (sysnum)
                {
                    case 0:
                        textBox8.Text = variant.variantPicture;
                        break;
                    case 1:
                        textBox8.Text = variant.variantPicture;
                        break;
                    case 2:
                        textBox10.Text = variant.variantPicture;
                        break;
                    default:
                        break;
                }
                APCtrlWorker.Instance.transferValuesToTextBoxes();
            }
            updateEditListBox();
            //ss.updateControls();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            addVariant(0, comboBox1.Text);
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            APCtrlWorker.Instance.selectSystem(1);  //sensorSetup
        }

        private void button13_Click(object sender, EventArgs e)
        {
            addVariant(1, comboBox2.Text + comboBox3.Text);
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            APCtrlWorker.Instance.selectSystem(1);  //sensorSetup
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ParamValue val = new ParamValue();
            val.value = textBox9.Text;
            val.description = textBox6.Text;
            DependentParameters.Instance.addDependentParameter(comboBox9.Text, val);
            textBox9.Text = "";
            textBox6.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tmpText = comboBox1.Text;
            chooseVariant(0, tmpText);
            textBox7.Text = "";
            textBox12.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ParamValue val = new ParamValue();
            val.value = textBox7.Text;
            val.description = textBox12.Text;
            DependentParameters.Instance.addDependentParameter(comboBox7.Text,val);
            textBox7.Text = "";
            textBox12.Text = "";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseVariant(1, comboBox2.Text + comboBox3.Text);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text))
            {
                if (APCtrlWorker.Instance.curSysNum==0)
                    pictureBox1.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text);
                else
                    pictureBox2.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text);
            }
            else
            {
                if (APCtrlWorker.Instance.curSysNum == 0)
                    pictureBox1.Image = null;
                else
                    pictureBox2.Image = null;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            addVariant(2, comboBox4.Text + comboBox5.Text + comboBox6.Text + comboBox10.Text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text))
            {
                pictureBox3.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text);
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox7.Text = DependentParameters.Instance.mDepParams[comboBox7.Text].value;
            textBox12.Text = DependentParameters.Instance.mDepParams[comboBox7.Text].description;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseVariant(2, comboBox4.Text + comboBox5.Text + comboBox6.Text + comboBox10.Text);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseVariant(2, comboBox4.Text + comboBox5.Text + comboBox6.Text + comboBox10.Text);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseVariant(2, comboBox4.Text + comboBox5.Text + comboBox6.Text + comboBox10.Text);
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseVariant(2, comboBox4.Text + comboBox5.Text + comboBox6.Text + comboBox10.Text);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = listBox2.Text;
            String name = listBox2.Text;
            String value = Parameters.Instance.parameters[name].value;
            textBox1.Text = value;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DependentParameters.Instance.clearParams();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox9.Text = DependentParameters.Instance.mDepParams[comboBox9.Text].value;
            textBox6.Text = DependentParameters.Instance.mDepParams[comboBox9.Text].description;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            changeTrackBar(trackBar1, 0, 1);
        }

        private void changeTrackBar(TrackBar bar,float offset,float gain)
        {
            ParamValue val = new ParamValue();
            val.value = ((float)bar.Value*gain+offset).ToString();
            val.description = "";
            DependentParameters.Instance.addDependentParameter(bar.Tag.ToString(), val);
            textBox9.Text = "";
            textBox6.Text = "";
        }

        private void changeTextBox(TextBox bar)
        {
            if (!writeLock)
            {
                ParamValue val = new ParamValue();
                val.value = bar.Text;
                val.description = "";
                DependentParameters.Instance.addDependentParameter(bar.Tag.ToString(), val);
                textBox9.Text = "";
                textBox6.Text = "";
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            changeTrackBar(trackBar2, 0, 1);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            changeTrackBar(trackBar3, 0, 1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ParamValue val = new ParamValue();
            val.value = checkBox1.Checked ? "1" : "0";
            val.description = textBox6.Text;
            DependentParameters.Instance.addDependentParameter(checkBox1.Tag.ToString(), val);
            textBox9.Text = "";
            textBox6.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DependentParameters.Instance.clearParams();
            DependentParameters.Instance.refreshComboBoxes();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox11);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox13);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox14);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox15);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox27);
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox26);
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox25);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox24);
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox31);
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox30);
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox29);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            changeTextBox(textBox19);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            Parameters.Instance.loadEditFromFile("base.param");
            updateEditListBox();
            APCtrlWorker.Instance.transferValuesToTextBoxes();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem_Click(this, e);
        }
    }
}
