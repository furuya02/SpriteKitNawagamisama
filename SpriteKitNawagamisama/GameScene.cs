using System;
using System.Collections;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace SpriteKitNawagamisama
{
	public class GameScene : SKScene
	{


		private Random _random = new Random(Environment.TickCount);
		private CGSize _effectSize;
		private CGSize _kumaSize;
		private ArrayList _kumaList;
		private int _updateCounter = 0;
		private SKLabelNode _scoreLabel;

		protected GameScene(IntPtr handle) : base(handle)
		{

		}


		public override void DidMoveToView(SKView view)
		{
			this.BackgroundColor = UIColor.Black;
			AddChild(new SKLabelNode("Chalkduster")
			{
				Text = "NAWAGAMISAMA",
				FontSize = 40,
				Position = new CGPoint(Frame.Width / 2, Frame.Height-50)
			});


			var w = View.Frame.Width / 3;
			_kumaSize = new CGSize(w, w);
			_effectSize = new CGSize(w+100,w+100);

			_kumaList = new ArrayList();
			for (var y = 0; y < 3; y++)
			{
				for (var x = 0; x < 3; x++)
				{
					_kumaList.Add(new Kuma(this, new CGPoint(x * w, y * w + 150), _kumaSize));
				}
			}

			_scoreLabel= new SKLabelNode("Chalkduster")
			{
				FontSize = 50,
				Position = new CGPoint(Frame.Width / 2, 50)
			};
			AddChild(_scoreLabel);

		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches)
			{
				var location = ((UITouch)touch).LocationInNode(this);

				foreach (Kuma kuma in _kumaList)
				{
					if (kuma.Attack(location))
					{
						var wing = new AnimationNode("wing", 5, 6, 0.02, _effectSize);
						wing.Action(this,kuma.Center);
						break;
					}
					else {
						var wrap = new AnimationNode("wrap", 2, 13, 0.02, _effectSize);
						wrap.Action(this, location);
					}
				}
			}
		}

		public override void Update(double currentTime)
		{
			if (_updateCounter > 5)
			{
				var score = 0;
				foreach (Kuma kuma in _kumaList)
				{
					kuma.Interval();
					score += kuma.Score;
				}
				_scoreLabel.Text = String.Format("{0}", score);
				_updateCounter = 0;
			}
			_updateCounter++;
		}
	}
}

