﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp
{
    public partial class AdminLayout
    {
        bool MenuOpened { get; set; }

        void DrawerToggle()
        {
            MenuOpened = !MenuOpened;
        }
    }
}