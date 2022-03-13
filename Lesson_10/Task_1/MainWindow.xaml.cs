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
    /// в окне присутствует TabControl, который отображает чаты.
    /// каждая вкладка - это чат с отдельным пользователем, содержит ListBox с сообщениями.
    /// в каждом чате отображаются как входящие, так и исходящие сообщения.
    /// справа TextBox для набора сообщения на отправку и кнопка для отправки сообщения.
    /// сообщение отправляется пользователю, чья вкладка выбрана в момент отправки.
    /// при запуске происходит импорт сохраненной истории сообщений.
    /// при закрытии окна происходит сохранение всех чатов.
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramService bot;
        
        public MainWindow()
        {
            InitializeComponent();
            bot  = new TelegramService(this);
            
            ///Отрисовываем историю сообщений, если она есть
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
                    ///Проверка сообщений на входящее/исходящее и отрисовка шаблонов
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
                    ///Заголовок вкладки - это имя пользователя. Свойство Name вкладки содержит ID пользователя
                    tabControl.Items.Add(new TabItem
                    {
                        Header = user.Name,
                       
                        Name = $"tabItem{user.ID}",
                        Content = listMessage,

                    });
                    ///отслеживание изменения коллекции сообщений
                    user.Messages.CollectionChanged += Messages_CollectionChanged;

                }
                //foreach (TabItem tab in tabControl.Items)
                //{
                //    ListBox lb = (ListBox)tab.Content;
                //    lb.SelectionChanged += listBox_SelectionChanged;
                //}
            }                
            ///отслеживание изменений коллекции пользователей
            bot.users.CollectionChanged += Users_CollectionChanged;          
            
        }  
        
        //void listBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        //{
        
        //}

        void Users_CollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            ///создаем экземпляр пользователя из последнего изменения
            List<User> newusers = e.NewItems.Cast<User>().ToList();
            User user = newusers.First();
                       
            ListBox listMessage = new ListBox()
            {
                Name = $"listBox{user.ID}",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                                
            };
            
            ///проверяем есть ли вкладки в Контроле. Если нет, то после создания выбираем ее.
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
            ///Создаем экземпляр сообщения из последнего изменения
            List<Message> newMessage = e.NewItems.Cast<Message>().ToList();
            Message message = newMessage.First();

            TabItem tabItem = new TabItem();
            ListBox listBox = new ListBox();

            ///Проверяем не исходящее ли это сообщение
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

            ///определяем вкладку, соответствующую пользователю, приславшему сообщение
            ///добавляем сообщение в ListBox этой вкладки
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

            ///Если вкладка не выбрана, то сигнализируем, что пришло сообщение
            if (tabItem.IsSelected != true)
            {
                tabItem.FontWeight = FontWeights.Bold;
                tabItem.Foreground = Brushes.Red;
            }
        }

        /// <summary>
        /// действие по нажатию кнопки отправить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {        
            string text = textBox.Text;

            ///при отсутсвии текста ничего не проиходит
            if (text == "")
            {
                return;
            }

            ///проверка нет ли попытки отправить сообщение, не имея адресата
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

        /// <summary>
        /// обработка события выбора вкладки для удаления сигнализации о новом сообщении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TabItem tab = (TabItem)tabControl.SelectedItem;
            tab.FontWeight = FontWeights.Normal;
            tab.Foreground = Brushes.Black;
        }   

        /// <summary>
        /// обработка события закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bot.SaveMessageHistory();
        }

       
    }
    
}
