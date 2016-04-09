﻿using System;

using SpriteKit;
using UIKit;

namespace SpriteKitNawagamisama
{
	public partial class GameViewController : UIViewController
	{
		protected GameViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Configure the view.
			var skView = (SKView)View;
			skView.ShowsFPS = true;
			skView.ShowsNodeCount = true;
			/* Sprite Kit applies additional optimizations to improve rendering performance */
			skView.IgnoresSiblingOrder = true;

			// Create and configure the scene.
			var scene = SKNode.FromFile<GameScene>("GameScene");
			//scene.ScaleMode = SKSceneScaleMode.AspectFill;
			scene.ScaleMode = SKSceneScaleMode.ResizeFill;

			// Present the scene.
			skView.PresentScene(scene);
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? UIInterfaceOrientationMask.AllButUpsideDown : UIInterfaceOrientationMask.All;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override bool PrefersStatusBarHidden()
		{
			return true;
		}
	}
}

