﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PavilionsApp
{
    
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();
            FillCaptcha();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
           if(String.Equals(CaptchaWriteBox.Text, CaptchaBox.Text))
            {
                this.Close();
            } else
            {
                MessageBox.Show("Не верный ввод капчи");
            }
        }

        private void Gen_Click(object sender, RoutedEventArgs e)
        {
            FillCaptcha();
        }

        private void FillCaptcha()
        {
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();

            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            CaptchaBox.Text = pwd;
        }
    }
}
