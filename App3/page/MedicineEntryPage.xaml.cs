﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavPageSample.notification;
using System.Data.SQLite;

namespace NavPageSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineEntryPage : ContentPage
    {
        public MedicineEntryPage()
        {
            InitializeComponent();

            // 2021/12/28 吉澤追加分 ここから

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
                DisplayAlert("Alert", "TestPage.NotificationReceived", "ok");
            };
            DisplayAlert("Alert", "TestPage.InitializeComponent", "ok");
            // 2021/12/28 吉澤追加分 ここまで
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            //入力してもらい、それをDBに登録後、SQLで通知に使う朝食の時間などを取り出す
            //食前食後かで加算減算をする。その結果を通知を送るメソッドのとこに入れたい

            SQLiteConnection con = new SQLiteConnection("Data Source=R03TEAM04.db;Version=3;");
            //接続を開く
            con.Open();

            if (!string.IsNullOrWhiteSpace(Medicine_Name_Entry.Text))
            {
                await App.Database.SaveMedicineAsync(new Medicine
                {
                    Medicine_name = Medicine_Name_Entry.Text,
                    Url = Url_Entry.Text,
                    timing = Timing_Entry.Text,
                });

                await App.Database.SaveUserAsync(new User
                {
                    Jikantai = Jikantai_Entry.Text
                });
            }


        }

        // 2021/12/28 吉澤追加分 ここから

        INotificationManager notificationManager;

        int notificationNumber = 0;


       
        //ボタン押すと10秒後に通知がくるやつ
        private void oneButtonClicked(object sender, EventArgs e)
        {

            notificationNumber++;
            string title = $"通知しますた #{notificationNumber}";
            string message = $"お薬飲もう {notificationNumber} ね!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddMinutes(1));
            var msg = new Label()
            {
                Text = $"Schedule Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);
        }

        private void twoButtonClicked(object sender, EventArgs e)
        {

            notificationNumber++;
            string title = $"通知しますた #{notificationNumber}";
            string message = $"お薬飲もう {notificationNumber} ね!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddMinutes(2));
            var msg = new Label()
            {
                Text = $"Schedule Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);
        }

        private void threeeButtonClicked(object sender, EventArgs e)
        {

            notificationNumber++;
            string title = $"通知しますた #{notificationNumber}";
            string message = $"お薬飲もう {notificationNumber} ね!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddMinutes(3));
            var msg = new Label()
            {
                Text = $"Schedule Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);
        }

        private void fourButtonClicked(object sender, EventArgs e)
        {

            notificationNumber++;
            string title = $"通知しますた #{notificationNumber}";
            string message = $"お薬飲もう {notificationNumber} ね!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddMinutes(4));
            var msg = new Label()
            {
                Text = $"Schedule Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }

        // 2021/12/28 吉澤追加分 ここまで

    }
}

/* //ボタン押すと即時通知されるやつ
        private void OnNotifyButtonClicked(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message);
            var msg = new Label()
            {
                Text = $"Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);

        }
*/