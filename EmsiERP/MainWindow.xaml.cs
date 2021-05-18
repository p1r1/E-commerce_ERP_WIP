using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmsiERP.Windows;
using ERPemsiCore;
using ERPemsiCore.src;
using ERPemsiCore.src.meta;
using MaterialDesignThemes.Wpf;

namespace EmsiERP {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        /// <summary>
        /// when a property changed invoke other ui elements
        /// this.DataContext = this; in initialize
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private double _dockPanelWidth1;
        private double _dockPanelWidth2;
        //private double totalTabWidth = 0;

        private ActionTabViewModal vmd;

        public double DockPanelWidth1 {
            get { return _dockPanelWidth1; }
            set {
                if (_dockPanelWidth1 != value) {
                    _dockPanelWidth1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public double DockPanelWidth2 {
            get { return _dockPanelWidth2; }
            set {
                if (_dockPanelWidth2 != value) {
                    _dockPanelWidth2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindow() {
            this.DataContext = this;
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            setDockPanelMainSize();
            vmd = new ActionTabViewModal();
            // Bind the xaml TabControl to view model tabs
            DockPanelMain.ItemsSource = vmd.Tabs;
            ListViewMenu.SelectedIndex = 0;

        }

        private void Button_CloseApp_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        private void Button_MinimizeApp_Click(object sender, RoutedEventArgs e) {
            if (this.WindowState != WindowState.Minimized) {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void Button_OpenMenu_Click(object sender, RoutedEventArgs e) {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            DockPanelWidth1 = DockPanelMain.Width;
            DockPanelWidth2 = DockPanelMain.Width - 130;
        }

        private void Button_CloseMenu_Click(object sender, RoutedEventArgs e) {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            DockPanelWidth1 = DockPanelMain.Width;
            DockPanelWidth2 = DockPanelMain.Width + 130;
        }
        private void setDockPanelMainSize() {
            DockPanelMain.Height = this.Height - GridTopBar.Height;
            DockPanelMain.Width = this.Width - GridMenu.Width - 1;
        }

        #region old listviewmenu
        //private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        //    //UserControl usc = null;
        //    DockPanelMain.Children.Clear();
        //    StackPanelTopTabs.Children.Clear();
        //    setDockPanelMainSize();

        //    switch (((ListViewItem)((ListView)sender).SelectedItem).Name) {
        //        case "SideButton_Products":
        //            //UserControl_Home usc = new UserControl_Home();
        //            //DockPanelMain.Children.Add(usc);
        //            //UserControl usc = null;
        //            //DockPanelMain.Children.Clear();
        //            setDockPanelMainSize();
        //            UserControl_Products usc = new UserControl_Products();
        //            DockPanelMain.Children.Add(usc);
        //            break;
        //        case "SideButton_Accounts":
        //            UserControl_Accounts usc_account = new UserControl_Accounts();
        //            //usc_account.SelectedAccount = this.accountMain;
        //            DockPanelMain.Children.Add(usc_account);
        //            break;
        //        case "SideButton_Sale":
        //            UserControl_Sales usc_sales = new UserControl_Sales();
        //            //usc_order.SelectedAccount = this.accountMain;
        //            DockPanelMain.Children.Add(usc_sales);
        //            break;
        //        case "SideButton_Purchase":
        //            UserControl_Purchase usc_conversation = new UserControl_Purchase();
        //            //usc_conversation.SelectedAccount = this.accountMain;
        //            DockPanelMain.Children.Add(usc_conversation);
        //            break;
        //        case "SideButton_Users":
        //            UserControl_Users usc_comm = new UserControl_Users();
        //            //usc_comm.SelectedAccount = this.accountMain;
        //            DockPanelMain.Children.Add(usc_comm);
        //            break;
        //        case "SideButton_Settigns":
        //            //UserControl_Communicate usc_comm = new UserControl_Communicate();
        //            //usc_comm.SelectedAccount = this.accountMain;
        //            //DockPanelMain.Children.Add(usc_comm);
        //            break;
        //        default:
        //            break;
        //    }            
        //}
        #endregion
        private void ListViewMenu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            setDockPanelMainSize();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name) {
                case "SideButton_Products":
                    AddNewTab(new UserControl_Products(), "Ürünler");
                    //DockPanelMain.selectab = DockPanelMain.Items.Count;
                    break;
                case "SideButton_Accounts":
                    AddNewTab(new UserControl_Accounts(), "Cari Hesplar");
                    DockPanelMain.SelectedIndex = vmd.Tabs.Count;
                    //usc_account.SelectedAccount = this.accountMain;
                    break;
                case "SideButton_Sale":
                    AddNewTab(new UserControl_Sales(), "Satışlar");
                    //usc_order.SelectedAccount = this.accountMain;
                    break;
                case "SideButton_Purchase":
                    AddNewTab(new UserControl_Purchase(), "Alışlar");
                    //usc_conversation.SelectedAccount = this.accountMain;
                    //DockPanelMain.Children.Add(usc_conversation);
                    break;
                case "SideButton_Users":
                    AddNewTab(new UserControl_Users(), "Kullanıcılar");
                    //usc_comm.SelectedAccount = this.accountMain;
                    //DockPanelMain.Children.Add(usc_comm);
                    break;
                case "SideButton_Settigns":
                    AddNewTab(new UserControl_Settings(), "Ayarlar");
                    //UserControl_Communicate usc_comm = new UserControl_Communicate();
                    //usc_comm.SelectedAccount = this.accountMain;
                    //DockPanelMain.Children.Add(usc_comm);
                    break;
                default:
                    break;
            }
        }
        private void Button_TheAdd_Click(object sender, RoutedEventArgs e) {

        }

        public void AddNewTab(UserControl myUserControl, string header) {            
            //TabItem tabItem = new TabItem();
            //tabItem.Header = "yow0";

            //tabItem.Content = myUserControl;
            //DockPanelMain.Items.Add(tabItem);
            ////tabItem.DataContext = data; //where data is the data that 
            ////comes from the database 
            ////being represented in object form

            ActionTabItem actionTabItem = new ActionTabItem { Content = myUserControl, Header = header };
            // Populate the view model tabs
            vmd.Populate(actionTabItem);
            DockPanelMain.SelectedValue = actionTabItem;
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            // This event will be thrown when on a close image clicked
            vmd.Tabs.RemoveAt(DockPanelMain.SelectedIndex);
        }


    }
}
