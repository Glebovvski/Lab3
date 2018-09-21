using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_SherbashinWebForms
{
    public partial class Form1 : Form
    {

        const float q = 1700;
        const float Vg = 3800;
        const float m0 = 200000;
        const float Re = 6371000;
        float Me = 5.96f * (float)Math.Pow(10, 24);
        float KG = 6.67f * (float)Math.Pow(10, -11);
        float[] dt = { 0.1f, 0.2f, 1, 5, 20 };

        float F = 0;
        float Fprev;
        float P = 0;
        float Pprev = 0;
        float m = m0;
        float mprev = m0;
        float r = 0;
        float rprev;
        float x = 0;
        float xprev = 0;
        float a = 0;
        float aprev = 0;
        float V = 0;
        float Vprev = 0;

        float d;

        float GetF()
        {
            F = Vg * q - Pprev;
            Fprev = F;
            return F;
        }

        float GetP()
        {
            P = KG * ((Me * mprev) / (float)Math.Pow(rprev, 2));
            Pprev = P;
            return P;
        }

        float GetV()
        {
            V = Vprev + (F / mprev) * d;
            Vprev = V;
            return V;
        }

        float GetX()
        {
            x = xprev + V * d;
            xprev = x;
            return x;
        }

        float GetR()
        {
            r = Re + x;
            rprev = r;
            return r;
        }

        float GetM()
        {
            m = mprev - q * d;
            mprev = m;
            return m;
        }

        float GetA()
        {
            a = F / m;
            aprev = a;
            return a;
        }

        void UpdateValues()
        {
            GetM();
            GetX();
            GetR();
            GetP();
            GetF();
            GetA();
            GetV();
        }
        
        void Reset()
        {
            F = 0;
            Fprev = 0;
            P = 0;
            Pprev = 0;
            m = m0;
            mprev = m0;
            r = 0;
            rprev = 0;
            x = 0;
            xprev = 0;
            a = 0;
            aprev = 0;
            V = 0;
            Vprev = 0;
        }

        public Form1()
        {
            InitializeComponent();
            dtForm.SelectedIndex = 0;
            grid.Columns.Add("i", "i c");
            grid.Columns.Add("a", "a м/с2");
            grid.Columns.Add("v", "v м/с");
            grid.Columns.Add("x", "x м");
            grid.AutoResizeColumns();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        void GetResults()
        {
            grid.Rows.Clear();
            for (float i = 0; i <= 60; i += d)
            {
                UpdateValues();
                grid.Rows.Add(i, a, V, x);
            }
        }

        private void dtForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            d = dt[dtForm.SelectedIndex];
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetResults();
        }
    }
}
