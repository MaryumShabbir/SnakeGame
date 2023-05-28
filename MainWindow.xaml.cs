using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private readonly Dictionary<GridValue, ImageSource> GridValToImage = new()
        {
            {GridValue.empty, Images.Empty },
            {GridValue.snake, Images.Body },
            {GridValue.food, Images.food }
        };

        public readonly int rows = 15, cols = 15;


        private readonly Image[,] gridImages;
        private Movements GameState;
        private readonly TextBlock ScoreText;


        public MainWindow()
        {
            InitializeComponent();
           ScoreText=new TextBlock();
           gridImages = SetupGrid();
            GameState = new Movements(rows, cols);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            await Gameloop();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (GameState.Game_Over)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left:
                    GameState.ChangeDirections(Directions.left);
                    break;
                case Key.Right:
                    GameState.ChangeDirections(Directions.right);
                    break;
                case Key.Up:
                    GameState.ChangeDirections(Directions.Up);
                    break;
                case Key.Down:
                    GameState.ChangeDirections(Directions.Down);
                    break;
            }
           


        }
        private async Task Gameloop()
        {
            while (!GameState.Game_Over)
            {
                await Task.Delay(100);
                GameState.Move();
                Draw();
            }
        }



        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        private void Draw() {

            DrawGrid();
            ScoreText.Text = $"Score{GameState.Score}";
        }
        private void DrawGrid()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    GridValue gridval = GameState.Grid[r, c];
                    gridImages[r, c].Source = GridValToImage[gridval];
                }
            }
        }
    }
}
       
            
            







