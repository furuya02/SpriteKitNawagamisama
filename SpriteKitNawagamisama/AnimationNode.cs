using System;
using System.Collections;
using CoreGraphics;
using Foundation;
using SpriteKit;
using UIKit;


namespace SpriteKitNawagamisama
{
	public class AnimationNode
	{
		public SKSpriteNode node;
		private SKTexture[] textures;
		private SKAction action;

		// テクスチャから初期化する
		// w 横の画像数
		// h 縦の画像数
		public AnimationNode(String name, int w, int h, double sec,CGSize size)
		{
			textures = new SKTexture[w * h];
			var texture = SKTexture.FromImageNamed(name);
			var c = 0;
			for (var y = h - 1; y >= 0; y--)
			{
				for (var x = 0; x < w; x++)
				{
					textures[c++] = SKTexture.FromRectangle(new CGRect(x / (float)w, y / (float)h, 1 / (float)w, 1 / (float)h), texture);
				}
			}
			node = SKSpriteNode.FromTexture((SKTexture)textures[0]);
			node.Size = size;
			node.ZPosition = 1;
			node.Position = new CGPoint(-100, -100);
			action = SKAction.AnimateWithTextures((SKTexture[])textures, sec);
		}

		public void Action(SKScene sceen,CGPoint location)
		{
			node.Position = location;
			sceen.AddChild(node);
			// 繰り返しの場合
			//SKAction* forever = [SKAction repeatActionForever: walk];
			//[walker runAction:forever];
			node.RunAction(action, () =>
			{
				node.RemoveFromParent();
			});
		}
	}
}
