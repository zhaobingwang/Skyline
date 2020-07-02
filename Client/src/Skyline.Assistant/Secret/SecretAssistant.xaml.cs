using MaterialDesignThemes.Wpf;
using Skyline.Assistant.Data;
using Skyline.Assistant.Entities;
using Skyline.Assistant.Share.Dialogs;
using Skyline.Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Skyline.Assistant.Secret
{
    /// <summary>
    /// Interaction logic for SecretAssistant.xaml
    /// </summary>
    public partial class SecretAssistant : Window
    {
        AssistantDbContext dbContext;
        List<SecretViewModel> secrets;
        public SecretAssistant()
        {
            InitializeComponent();


            dbContext = new AssistantDbContext();
            secrets = new List<SecretViewModel>();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var entities = dbContext.Secrets.ToList();
            foreach (var entity in entities)
            {
                secrets.Add(new SecretViewModel
                {
                    Id = entity.ID,
                    Name = entity.AppName,
                    Password = entity.Password,
                    CreateTime = entity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifyTime = entity.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            dgSecret.ItemsSource = secrets;
        }

        private void dgSecret_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private string beforeValue = string.Empty;
        private void dgSecret_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private async void dgSecret_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var currentColumn = e.Row.Item as SecretViewModel;
            MessageBoxResult messageBoxResult = MessageBox.Show("是否保存修改内容", "警告", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var entity = dbContext.Secrets.FirstOrDefault(x => x.ID == currentColumn.Id);
                entity.Password = currentColumn.Password;
                entity.AppName = currentColumn.Name;

                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = dbContext.SaveChanges();
                MessageBox.Show(result.ToString());

            }
        }
    }
}
