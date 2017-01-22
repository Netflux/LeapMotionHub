using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Leap;
using WebSocketSharp.Server;

namespace LeapMotionHub
{
	/**
	 * Utility application that serializes Leap Motion frames and sends it to all connected clients.
	 */
	public class App : ApplicationContext
	{
		private NotifyIcon mTrayIcon;
		private Boolean mIsActive = true;

		private Stopwatch mStopwatch = new Stopwatch();
		private long mTimestamp;
		private const int UPDATE_TIME = 1000 / 120;

		private Controller mController = new Controller();

		private WebSocketServer mWebSocketServer = new WebSocketServer("ws://localhost:4567");

		public App()
		{
			// Setup System Tray Icon/Context Menu
			mTrayIcon = new NotifyIcon()
			{
				Icon = SystemIcons.Application,
				ContextMenu = new ContextMenu(new MenuItem[] {
					new MenuItem("Disable", ToggleActive),
					new MenuItem("-"),
					new MenuItem("Exit", Exit)
				}),
				Visible = true
			};
			mTrayIcon.Text = "Leap Motion Hub is on";

			// Setup the Stopwatch for update rate limiting
			mStopwatch.Start();
			mTimestamp = 0;

			// Setup Leap Motion Controller and frame event handler
			mController.EventContext = WindowsFormsSynchronizationContext.Current;
			mController.FrameReady += newFrameHandler;

			// Setup and start the Web Socket Server
			mWebSocketServer.AddWebSocketService<AppWebSocketBehavior>("/");
			mWebSocketServer.Start();
		}

		private void newFrameHandler(object sender, FrameEventArgs eventArgs)
		{
			if (mIsActive && mWebSocketServer.IsListening && mStopwatch.ElapsedMilliseconds > mTimestamp)
			{
				Frame frame = eventArgs.frame;
				byte[] bytes = Serializer.serializeFrame(frame);
				//string xml = Serializer.Serialize<Frame>(frame);

				mTimestamp = mStopwatch.ElapsedMilliseconds + UPDATE_TIME;

				// Serialize and pass Leap Motion frame to all clients
				mWebSocketServer.WebSocketServices["/"].Sessions.Broadcast(bytes);
			}
		}

		private void ToggleActive(object sender, EventArgs eventArgs)
		{
			// Swap the active state and enable/disable the Web Socket Server
			mIsActive = !mIsActive;

			if (mIsActive)
			{
				if (!mWebSocketServer.IsListening)
				{
					mWebSocketServer.AddWebSocketService<AppWebSocketBehavior>("/");
					mWebSocketServer.Start();
				}

				mTrayIcon.Text = "Leap Motion Hub is on";
				mTrayIcon.ContextMenu.MenuItems[0].Text = "Disable";
			}
			else
			{
				if (mWebSocketServer.IsListening)
				{
					mWebSocketServer.Stop();
				}

				mTrayIcon.Text = "Leap Motion Hub is off";
				mTrayIcon.ContextMenu.MenuItems[0].Text = "Enable";
			}
		}
		
		private void Exit(object sender, EventArgs eventArgs)
		{
			mTrayIcon.Visible = false;

			Application.Exit();
		}
	}

	/**
	 * Non-abstract version of the WebSocketBehavior class.
	 */
	public class AppWebSocketBehavior : WebSocketBehavior { }
}
