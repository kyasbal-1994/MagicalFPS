﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMF;
using MMF.Controls.Forms;
using MMF.DeviceManager;
using MMF.Grid;
using MMF.Matricies.Camera.CameraMotion;
using MMF.Model.PMX;
using MMF.Oculus;
using MMF.Utility;
using OculusForMikuMikuFlex;
using SlimDX;

namespace MagicalFPS
{
    public partial class MainWindow :D2DSupportedRenderForm
    {
        private readonly GameContext _gameContext;
        public static Size MainWindowSize=new Size(640,480);

        private OculusDisplayRenderer oculusRenderer;

        private OculusDeviceManager oculusDeviceManager;
        private PMXModel mona;

        public MainWindow(GameContext gameContext)
        {
            _gameContext = gameContext;
            InitializeComponent();
            Size = MainWindowSize;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ScreenContext.CameraMotionProvider=new BasicCameraControllerMotionProvider(this,this);
            //とりあえずテストでもなーの読み込み
            BasicGrid grid = new BasicGrid();
            grid.Load(RenderContext);
            ScreenContext.WorldSpace.AddResource(grid);
            mona = PMXModel.OpenLoad("mona-.pmx", RenderContext);
            mona.Visibility = true;
            mona.Transformer.Position = new Vector3(0, 0, 1);
            ScreenContext.WorldSpace.AddResource(mona);
            //oculusDeviceManager=new OculusDeviceManager(RenderContext);
            //oculusRenderer=new OculusDisplayRenderer(RenderContext,WorldSpace);
        }

        public override void Render()
        {
            base.Render();
            if (_gameContext.handOperationChecker != null) mona.Transformer.Position += Vector3.Normalize(new Vector3(_gameContext.handOperationChecker.getMovementVector(), 0));
            //oculusRenderer.Render();
        }

        //protected override void RenderSprite()
        //{

        //}
    }
}
