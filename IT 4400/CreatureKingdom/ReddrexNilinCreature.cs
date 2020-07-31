using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CreatureKingdom
{
    class ReddrexNilinCreature : Creature
    {
        // 0-left 1-right
        private enum Direction
        {
            left,
            right
        }

        private Direction direction = Direction.right;
        private Image frogImage;
        private DispatcherTimer dispatcherTimer;
        private readonly string imageFilePath = @"ReddrexNilin\{0}.png";
        public ReddrexNilinCreature(Canvas kingdom, Dispatcher dispatcher, int waitTime = 100) : base(kingdom, dispatcher, waitTime)
        {
            
        }

        public override void Place(double x = 100, double y = 200)
        {
            BitmapImage frog = Image();
            this.frogImage = new Image()
            {
                Width = frog.Width / 10,
                Height = frog.Height / 10,
                Name = "Frog",
                Source = frog
            };

            this.kingdom.Children.Add(this.frogImage);
            base.Place(x, y);
            Canvas.SetTop(this.frogImage, this.Y);
            Canvas.SetLeft(this.frogImage, this.X);

            this.dispatcherTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(this.WaitTime), DispatcherPriority.Background, new EventHandler(
                (sender, e) =>
                {
                    if (!this.Paused)
                    {
                        // update creature movement
                        this.CreatureMovement();
                        Canvas.SetTop(this.frogImage, this.Y);
                        Canvas.SetLeft(this.frogImage, this.X);
                    }
                }),
                this.dispatcher);
        }

        public override void Shutdown()
        {
            this.dispatcherTimer.Stop();
            this.kingdom.Children.Remove(this.frogImage);
            base.Shutdown();
        }

        private void CreatureMovement()
        {

            if(this.kingdom.ActualWidth < (this.x + this.frogImage.Width))
            {
                this.direction = Direction.left;
                this.frogImage.Source = this.Image();
            }
            else if (this.x <= 0)
            {
                this.direction = Direction.right;
                this.frogImage.Source = this.Image();
            }

            switch (this.direction)
            {
                case Direction.left:
                    this.x -= 1;
                    break;
                case Direction.right:
                    this.x += 1;
                    break;
            }


        }

        private BitmapImage Image()
        {
            string filePath = string.Format(imageFilePath, this.direction.ToString());
            BitmapImage frog = this.LoadBitmap(filePath, 840);
            return frog;

        }
    }
}
