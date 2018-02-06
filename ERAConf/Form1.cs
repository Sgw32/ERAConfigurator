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
            listBox1.Items.Clear();
            SortedDictionary<String, ParamValue> baseP = Parameters.Instance.baseParameters;
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
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            Parameters.Instance.readParameters();
            updateListBox();
            makeACTLS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Parameters.Instance.removeBaseParameter(listBox1.SelectedValue.ToString());
            updateListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String description = textBox4.Text;
            String defaultValue = textBox3.Text;
            String name = textBox2.Text;
            Parameters.Instance.addBaseParameter(name, defaultValue, description);
            updateListBox();
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
            String name = listBox1.SelectedItem.ToString();
            ParamValue val = Parameters.Instance.baseParameters[listBox1.SelectedItem.ToString()];
            String description = val.description;
            String defaultValue = val.defaultValue;
            textBox4.Text = description;
            textBox3.Text = defaultValue;
            textBox2.Text = name;
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
            
            ss.addVariant(variant.variantName, variant); //add or replace
            redrawVariants(sysnum);
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

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text))
            {
                pictureBox1.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + textBox8.Text);
            }
        }
    }
}
