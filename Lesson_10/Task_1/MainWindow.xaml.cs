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
            bot.users.CollectionChanged += Users_CollectionChanged;
            bot.user.Messages.CollectionChanged += Messages_CollectionChanged;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (FileStream fs = new FileStream("chat.xml", FileMode.OpenOrCreate))
            {
                bot.users = xmlSerializer.Deserialize(fs) as ObservableCollection<User>;
            }
            tabControl.ItemsSource = bot.users;
            
        }  
        
        void Users_CollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            
            ListBox listMessage = new ListBox()
            {
                Name = $"messageList{e.NewStartingIndex+1}",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                ItemTemplate = (DataTemplate)TryFindResource("InputMessage"),
                ItemsSource = bot.user.Messages
                
            };
            if (tabControl.Items.Count == 0)
            {
                tabControl.Items.Add(new TabItem
                {
                    Header = bot.user.Name,
                    
                    Name = $"_{bot.user.ID}",
                    Content = listMessage,
                    IsSelected = true
                });
            }
            else
            {

                tabControl.Items.Add(new TabItem
                {
                    Header = bot.user.Name,                    
                    Name = $"_{bot.user.ID}",
                    Content = listMessage,
                    //FontWeight = FontWeights.Bold,
                    //Foreground = Brushes.Red
                });
            }
            
        }

        void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            textBlock.Text = "jbhmkhghjhg";
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            
            string text = textBox.Text;
            
            TabItem tab = (TabItem)tabControl.SelectedItem;
            Message message = new Message(DateTime.Now, "Me", text);
            ListBox listMessage = (ListBox)tab.Content;
            long id = Convert.ToInt64(tab.Name.TrimStart('_'));
            bot.SendMessage(text, id);

            foreach (User u in bot.users)
            {
                if (u.ID == id)
                {                    
                    listMessage.ItemTemplate = (DataTemplate)TryFindResource("OutputMessage");
                    u.Messages.Add(message);
                    //listMessage.ItemTemplate = (DataTemplate)TryFindResource("InputMessage");
                }
            }
            
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            //using (JsonWriter jr = new JsonWriter())
            //{
            //    JsonSerializer.Serialize<ObservableCollection<User>>(fs, bot.users);
            //}
        }
    }
    
}
