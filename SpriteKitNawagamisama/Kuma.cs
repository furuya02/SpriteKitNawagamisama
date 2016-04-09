using System;
using System.Collections;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;

namespace SpriteKitNawagamisama
{

	public class Kuma
	{
		private SKSpriteNode _node;
		private SKTexture[] _textures;
		private const int ImageMax = 10;
		private int _pattern = 0;
		private int _count = 0;
		private Random _random = new Random(Environment.TickCount);
		private nfloat _locationX;
		private nfloat _locationY;
		private nfloat _width;

		public int Score { get; set; }
		public CGPoint Center{
			get{
				return new CGPoint(_locationX + _width / 2, _locationY + _width / 2);
			}
		}

		enum Status
		{
			Try,
			Die,
			Sleep,
		};
		private Status _status = Status.Sleep;

		public Kuma(SKScene sceen, CGPoint location, CGSize size)
		{
			_textures = new SKTexture[ImageMax];
			for (var i = 0; i < ImageMax; i++)
			{
				_textures[i] = SKTexture.FromImageNamed(String.Format("Image{0}", i));
			}

			_node = SKSpriteNode.FromTexture((SKTexture)_textures[0]);
			_node.AnchorPoint = new CGPoint(0, 0);
			_node.Position = location;
			_node.Size = size;
			_node.ZPosition = 0;
			sceen.AddChild(_node);

			_locationX = location.X;
			_locationY = location.Y;
			_width = size.Width;

		}

		private bool IsHit(CGPoint point)
		{
			if (_locationX <= point.X && point.X <= _locationX + _width)
			{
				if (_locationY <= point.Y && point.Y <= _locationY + _width)
				{
					return true;
				}
			}
			return false;
		}

		// 死んだ場合は、true
		// 影響なかった場合は、false
		public bool Attack(CGPoint point)
		{
			if (!IsHit(point))
			{
				return false;
			}

			if (_status == Status.Try)
			{
				//死んだ時のパターン番号
				var dieNo = 6;//パターン1～４の場合
				if (_pattern == 5)
				{ //　パターン5の場合は ７，８，９のどれか
					dieNo = _random.Next(3) + 7;
				}
				switch (dieNo)
				{
					case 6:
						Score += 100;
						break;
					case 7:
						Score += 10;
						break;
					case 8:
						Score += 10;
						break;
					case 9:
						Score += 20;
						break;
				}
				_node.Texture = _textures[dieNo];
				_count = 3;
				_status = Status.Die;
				return true;

			}
			return false;
		}

		//タイマーイベント
		public void Interval()
		{
			_count--;
			if (_count > 0)
			{
				return;
			}
			if (_status == Status.Sleep)
			{
				_status = Status.Try;
				_count = 3; //出現時間

				if (0 == _random.Next(5))
				{
					//パターン１～４は、まれに出現
					_pattern = _random.Next(4) + 1;
				}
				else {
					//パターン5は、通常出現
					_pattern = 5;
				}
				_node.Texture = _textures[_pattern];

			}
			else if (_status == Status.Die || _status == Status.Try)
			{

				if (_status == Status.Try)
				{
					//死ななかった場合
					//Score -= 50; //縄神様の勝ち
				}

				_status = Status.Sleep;
				_count = _random.Next(10);
				_node.Texture = _textures[0];
			}
		}
	}

}

