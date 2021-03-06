﻿using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views.Social
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharedPlanUserPage : ContentPage
    {
        public SharedPlanUserPage()
        {
            InitializeComponent();
        }

        public SharedPlanUserPage(User user)
        {
            InitializeComponent();
            NameLabel.Text = user.UserName;
            if (user.Gender.Equals("M"))
            {
                genderLabel.Text = ResxFile.lbl_gender_male;
            } else
            {
                genderLabel.Text = ResxFile.lbl_female;
            }
            heightLabel.Text = user.Height.ToString();
            weightLabel.Text = user.Weight.ToString();
        }
    }
}