using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace LibVLCDetachIssue
{
    public partial class MainWindow : Window
    {
        private LibVLC _libVLC;
        private MediaPlayer _mp;

        string videoURL;

        public MainWindow()
        {
            InitializeComponent();
            Thread.Sleep(2000);
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            string VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64")).FullName;
            Core.Initialize(VlcLibDirectory);

            videoURL = "http://v16m-default.akamaized.net/38fcbe678227c1e9bc0f7a8c3d2ff2e3/5e485de1/video/tos/useast2a/tos-useast2a-ve-0068c001/7fb5975ac9e04e8caaf25a80f34b85d9/?a=0&br=2162&bt=1081&cr=3&cs=0&dr=0&ds=3&er=&l=2020021515080301011024413908D6F462&lr=&qs=0&rc=M2h2Zndvbm91czMzNzczM0ApOzo1NDNkODxoNzQ2PGY7OWdrczRqcnM0bjBfLS02MTZzczJiYmEvXjJgLjVhYS8xNTA6Yw%3D%3D&vl=&vr=";
            Console.WriteLine(videoURL);

            _libVLC = new LibVLC("--verbose=2");
            _mp = new MediaPlayer(_libVLC);
            videoView.Loaded += (sender, e) => videoView.MediaPlayer = _mp;
            _mp.Play(new Media(_libVLC, videoURL, FromType.FromLocation));
            _mp.Volume = 30;
        }
    }
}
