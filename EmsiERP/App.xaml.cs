using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmsiERP {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        /// <summary>
        /// Entry Point Override
        /// </summary>
        /// <param name="e">StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            // here you take control
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = new System.Globalization.CultureInfo("en-US");

            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

        }
    }
}
