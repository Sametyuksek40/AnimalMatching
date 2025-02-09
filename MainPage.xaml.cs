namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

#pragma warning disable CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. 'Gerekli' değiştiricisini ekleyin veya null atanabilir olarak bildirmeyi göz önünde bulundurun.
        public MainPage()
#pragma warning restore CS8618 // Null atanamaz alan, oluşturucudan çıkış yaparken null olmayan bir değer içermelidir. 'Gerekli' değiştiricisini ekleyin veya null atanabilir olarak bildirmeyi göz önünde bulundurun.
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {

            AnimalButtons.IsVisible = true; // bu hayvan animeleri butonlarımızı görünür kılıyor
                                            // bu tuşa basıldığında bu fonksiyonun neler yapmasını istediğimizi iletiyoruz

            PlayAgainButton.IsVisible=false; //play again tuşu görünmez kılınıyor,oyun aktifken

            List<string> animalEmoji = ["🐘","🐘","🦁","🦁","🐳","🐳"
                ,"🦘","🦘","🐪","🐪","🦔","🦔","🐴","🐴","🦅 ","🦅 " ];


             //animal buttons içindeki  tüm Button nesnelerini döngüye alır 
            // yani animals button içindeki her bir buton için aşağıdaki işlemler tekrarlanır

            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index=Random.Shared.Next(animalEmoji.Count); //animalEmoji listesinin indexi arasından rastgele bir değer seçer 
                string nextEmoji=animalEmoji[index];
                button.Text = nextEmoji;// rastgele bir hayvan emojisi ile göürünür
                animalEmoji.RemoveAt(index);// bu satır animalEmoji listesinden rastgele seçilen emojiyi listeden kaldırır
                //bu aynı emojinin birden fazla değere atanmasını engeller

            }
            // zamanlayıcımız her 0,1 saniyede bir TimerTick adlı bir yöntemi çalıştırılmasına sebep olur
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1),ZamanSayac);


        }
        int ondaBirGecenZaman = 0;
        private bool ZamanSayac()
        {

            if (!this.IsLoaded)
            {
                return false;
            }
            ondaBirGecenZaman++;
            TimeElapsed.Text = "Geçen Süre : " + (ondaBirGecenZaman / 10F).ToString("0.0s");
            if (PlayAgainButton.IsVisible)
            {
                ondaBirGecenZaman = 0;
                return false;
            }

            return true;
        }

        Button lastClicked;
        bool findingMatch=false;
        int matchesFound;
        private void Button_Clicked(object sender, EventArgs e)
        {

            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text)&&(findingMatch==false))
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                { 
                    //olası bir hata engellenmiş oldu

                    if ((buttonClicked!=lastClicked)&&(buttonClicked.Text==lastClicked.Text)&&!String.IsNullOrWhiteSpace(buttonClicked.Text))
                    {

                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }
                    lastClicked.BackgroundColor=Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch=false;

                } 

            }

            if (matchesFound==8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }





        }

        
    }

}
