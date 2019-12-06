using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Components;
using ConsoleEngine.Core;
using ConsoleEngine.Input;
using ConsoleEngine.Scenes;
using ConsoleEngine.ConsoleGraphics;
using ConsoleEngine.Assets;

namespace ConsoleEngine.Components
{
    class Pong : GameComponent
    {
        public GameObject ball;
        public TextShape shape;
        [EditableProperty] public int platformSize { get; set; } = 1;
        [EditableProperty] public float movingSpeed { get; set; } = 1;
        [EditableProperty] public bool isLeftPlayer { get; set; } = true;
        public int middlePong = 2;

        public override void OnCreate()
        {
            ball = SceneManager.currentScene.FindGameObject("ball");
            shape = gameObject.GetComponent<TextShape>();
            if (isLeftPlayer)
            {
                gameObject.position = new Vector2(0, GraphicsManager.height / 2);
            }
            else
            {
                gameObject.position = new Vector2(GraphicsManager.width - 1, GraphicsManager.height / 2);
            }
        }
        public override void Update()
        {
            if (isLeftPlayer)
            {
                bool s = InputManager.GetKeyPress(Keys.S), w = InputManager.GetKeyPress(Keys.W);
                if (gameObject.position.y > 1)
                {
                    if (w)
                    {
                        gameObject.position.y -= movingSpeed;
                    }
                }
                if (gameObject.position.y + platformSize < GraphicsManager.height - 1)
                {
                    if (s)
                    {
                        gameObject.position.y += movingSpeed;
                    }
                }
            }
            else
            {
                bool down = InputManager.GetKeyPress(Keys.Down), up = InputManager.GetKeyPress(Keys.Up);
                if (gameObject.position.y > 1)
                {
                    if (up)
                    {
                        gameObject.position.y -= movingSpeed;
                    }
                }
                if (gameObject.position.y + platformSize < GraphicsManager.height - 1)
                {
                    if (down)
                    {
                        gameObject.position.y += movingSpeed;
                    }
                }
            }
        }
    }
    class Ball : GameComponent
    {
        [EditableProperty] public int width { get; set; } = 2;
        [EditableProperty] public int height { get; set; } = 2;
        [EditableProperty] public float dx { get; set; } = 0;
        [EditableProperty] public float dy { get; set; } = 0;
        [EditableProperty] public float maxSpeed { get; set; } = 2;
        public int step { get; set; }
        private Pong leftPlatform;
        private Pong rightPlatform;
        private TextShape shape;
        private PongScore score;
        public override void OnCreate()
        {
            leftPlatform = SceneManager.currentScene.FindGameObject("leftPlatform").GetComponent<Pong>();
            rightPlatform = SceneManager.currentScene.FindGameObject("rightPlatform").GetComponent<Pong>();
            score = SceneManager.currentScene.FindGameObject("score").GetComponent<PongScore>();
            shape = gameObject.GetComponent<TextShape>();
   //         SetPosition();
        }
        public override void Update()
        {
            float x = gameObject.position.x;
            float y = gameObject.position.y;
            if (y + dy <= 1 || y + dy + height > GraphicsManager.height + 1)
            {
                dy = -dy;
            }
            if (x + dx <= 0)
            {
                if (y > leftPlatform.gameObject.position.y - height && y < leftPlatform.gameObject.position.y + leftPlatform.platformSize + height)
                {
                    dx = -dx;
                    int center = (int)Math.Round(leftPlatform.gameObject.position.y + leftPlatform.platformSize / 2);
                    dy = (y - center) * 0.1f;
                    shape.color = leftPlatform.shape.color;
                    score.scoreShape.color = leftPlatform.shape.color;
                    if (Math.Abs(dx) <= maxSpeed)
                    {
                        dx *= 1.2f;
                    }
                }
                else
                {
                    score.score++;
                    SetPosition();
                }
            }
            else if (x + dx + width >= GraphicsManager.width)
            {
                if (y > rightPlatform.gameObject.position.y - height && y < rightPlatform.gameObject.position.y + rightPlatform.platformSize + height)
                {
                    dx = -dx;
                    int center = (int)Math.Round(rightPlatform.gameObject.position.y + rightPlatform.platformSize / 2);
                    dy = (y - center) * 0.1f;
                    shape.color = rightPlatform.shape.color;
                    score.scoreShape.color = rightPlatform.shape.color;
                    if (Math.Abs(dx) <= maxSpeed)
                    {
                        dx *= 1.2f;
                    }
                }
                else
                {
                    score.score++;
                    SetPosition();
                }
            }
            gameObject.position = new Vector2(gameObject.position.x + dx, gameObject.position.y + dy);
        }
        private void SetPosition()
        {
            gameObject.position = new Vector2(GraphicsManager.width / 2, GraphicsManager.height / 2);
            Random rnd = new Random(Environment.TickCount);
            float[] _dx = new float[] { -0.5f, 0.5f };
            dy = rnd.Next(-1, 1) * 0.5f;
            dx = _dx[rnd.Next(0, 2)];
        }
    }
    class PongScore : GameComponent
    {
        [EditableProperty]
        public int score
        {
            get { return _score; }
            set
            {
                _score = value; SetShapeNumber();
            }
        }
        [EditableProperty] public int maxScore { get; set; } = 5;
        private int _score = 0;
        public TextShape scoreShape;
        public override void OnCreate()
        {
            gameObject.position = new Vector2(GraphicsManager.width / 2, GraphicsManager.height / 2);
            if (gameObject.GetComponent<TextShape>() == null)
            {
                scoreShape = gameObject.AddComponent<TextShape>();
                SetShapeNumber();
            }
            else
            {
                scoreShape = gameObject.GetComponent<TextShape>();
            }
        }
        private void SetShapeNumber()
        {
            if (scoreShape != null)
            {
                if (score > maxScore)
                {
                    score = 0;
                }
                scoreShape.shapeName = "text_" + score;
            }
        }
    }
    class PongController : GameComponent
    {
        public bool active;
        public override void OnCreate()
        {
            StartPongGame();
        }
        private void StartPongGame()
        {

        }
    }
}
