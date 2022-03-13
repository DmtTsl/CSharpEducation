using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Newtonsoft.Json;

namespace Lesson_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramService bot;
        
        public MainWindow()
        {
            InitializeComponent();
            bot  = new TelegramService(this);
            
            if (bot.users.Count > 0)
            {
                
                foreach (User user in bot.users)
                {
                    ListBox listMessage = new ListBox()
                    {
                        Name = $"listBox{user.ID}",
                        Margin = new Thickness(5, 5, 5, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        
                    };
                    foreach (Message message in user.Messages)
                    {
                        if (message.ID == 0)
                        {
                            
                            listMessage.Items.Add(new ListBoxItem
                            {                                
                                Template = (ControlTemplate)TryFindResource("Outgoing"),
                                DataContext = message
                            });
                        }
                        else
                        {
                            listMessage.Items.Add(new ListBoxItem
                            {                                
                                Template = (ControlTemplate)TryFindResource("Income"),
                                DataContext = message
                            });
                        }    
                        
                    }
                    tabControl.Items.Add(new TabItem
                    {
                        Header = user.Name,
                       
                        Name = $"tabItem{user.ID}",
                        Content = listMessage,

                    });
                    user.Messages.CollectionChanged += Messages_CollectionChanged;

                }
                //foreach (TabItem tab in tabControl.Items)
                //{
                //    ListBox lb = (ListBox)tab.Content;
                //    lb.SelectionChanged += listBox_SelectionChanged;
                //}
            }                
            
            bot.users.CollectionChanged += Users_CollectionChanged;          
            
        }  
        
        //void listBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        //{
        
        //}

        void Users_CollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            List<User> newusers = e.NewItems.Cast<User>().ToList();
            User user = newusers.First();
                       
            ListBox listMessage = new ListBox()
            {
                Name = $"listBox{user.ID}",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                                
            };
                        
            if (tabControl.Items.Count == 0)
            {
                tabControl.Items.Add(new TabItem
                {
                    Header = user.Name,
                    
                    Name = $"tabItem{user.ID}",
                    Content = listMessage,
                    IsSelected = true
                });
            }
            else
            {
                tabControl.Items.Add(new TabItem
                {
                    Header = user.Name,                    
                    Name = $"tabItem{user.ID}",
                    Content = listMessage,
                   
                });
            }

            bot.user.Messages.CollectionChanged += Messages_CollectionChanged;
        }

        void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            List<Message> newMessage = e.NewItems.Cast<Message>().ToList();
            Message message = newMessage.First();
            TabItem tabItem = new TabItem();
            ListBox listBox = new ListBox();

            if (message.ID == 0)
            {
                tabItem = tabControl.SelectedItem as TabItem;
                listBox = tabItem.Content as ListBox;
                listBox.Items.Add(new ListBoxItem
                {
                    Template = (ControlTemplate)TryFindResource("Outgoing"),
                    DataContext = message
                });
                return;
            }

            foreach (TabItem tab in tabControl.Items)
            {
                if (Convert.ToInt64(tab.Name.Substring(7)) == message.ID)
                {
                    tabItem = tab;
                }
            }

            listBox = tabItem.Content as ListBox;
            listBox.Items.Add(new ListBoxItem
            {
                Template = (ControlTemplate)TryFindResource("Income"),
                DataContext = message
            });

            if (tabItem.IsSelected != true)
            {
                tabItem.FontWeight = FontWeights.Bold;
                tabItem.Foreground = Brushes.Red;
            }
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {        
            string text = textBox.Text;

            if (text == "")
            {
                return;
            }

            if (tabControl.Items.Count != 0)
            {               
                textBox.Text = "";
                TabItem tab = (TabItem)tabControl.SelectedItem;
                Message message = new Message(DateTime.Now, "Me", text);
                
                long id = Convert.ToInt64(tab.Name.Substring(7));
               
                bot.SendMessage(text, id);

                foreach (User u in bot.users)
                {
                    if (u.ID == id)
                    {
                        u.Messages.Add(message);
                    }
                }
            }
            else MessageBox.Show("Не выбрано ни одного чата");               
        }


        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = (TabItem)tabControl.SelectedItem;
            tab.FontWeight = FontWeights.Normal;
            tab.Foreground = Brushes.Black;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bot.SaveMessageHistory();
        }

       
    }
    
}
