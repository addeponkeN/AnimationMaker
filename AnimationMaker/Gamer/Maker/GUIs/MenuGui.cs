using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimationMaker.Gamer.Screener;
using AnimationMaker.Screener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.GameUtility;
using Obo.Gui;
using Obo.Utility;

namespace AnimationMaker.Gamer
{
    class SaveWindow : Sprite
    {
        Label lbName;
        InputBox ibName;

        Button btOk, btClose;

        public bool Saved;
        public bool Active;

        const string ibDefaultText = "Enter filename";

        public string AnimationName;

        public SaveWindow()
        {
            SetSize(240, 160);
            Position = GHelper.Center(Globals.ScreenBox, Size);

            Color = new Color(25, 25, 25);

            lbName = new Label(UtilityContent.debugFont, "Filename:");
            lbName.Position = new Vector2(GHelper.Center(Rectangle, lbName.TextSize).X, Position.Y + 15);

            ibName = new InputBox(ScreenManager.gd, 200, 24);
            ibName.Position = new Vector2(GHelper.Center(Rectangle, ibName.Size).X, Position.Y + 60);
            ibName.Text = ibDefaultText;

            btOk = new Button(ScreenManager.gd, 72, 26, "Ok");
            btOk.Position = new Vector2(GHelper.Center(Rectangle, btOk.Size).X - 48, Position.Y + 100);

            btClose = new Button(ScreenManager.gd, 72, 26, "Close");
            btClose.Position = new Vector2(GHelper.Center(Rectangle, btOk.Size).X + 48, Position.Y + 100);

        }

        public void Update(GameTime gt)
        {
            if(!Active)
                return;

            if(ibName.IsSelected)
                if(ibName.Text == ibDefaultText)
                    ibName.Text = string.Empty;

            ibName.Update(gt);

            if(btOk.IsReleased)
            {
                if(string.IsNullOrEmpty(ibName.Text) || ibName.Text.ToLower() == ibDefaultText.ToLower())
                {
                    ibName.Text = ibDefaultText;
                }
                else
                {
                    AnimationName = ibName.Text;
                    Saved = true;
                    Active = false;
                }
            }

            if(btClose.IsReleased)
            {
                Active = false;
            }


        }

        public override void Draw(SpriteBatch sb)
        {
            if(!Active)
                return;

            base.Draw(sb);

            lbName.Draw(sb);
            ibName.Draw(sb);
            btOk.Draw(sb);
            btClose.Draw(sb);
        }

    }

    class MenuGui : Sprite
    {
        static MenuGui instance = new MenuGui();
        public static MenuGui Instance => instance;

        Button btSavep, btLoadp, btSavea;

        const string projectFileType = ".oamp";
        const string exportFileType = ".oboa";

        const string projectFilter = "OboAnimation Files(*.oamp)|*.oamp";
        const string exportFilter = "OboAnimation Files(*.oboa)|*.oboa";

        const string exportSavePath = "Exports\\";
        const string projectSavePath = "Projects\\";

        public MenuGui()
        {
            Position = Gui.MenuPos;
            Size = Gui.MenuSize;
            Color = new Color(60, 60, 60);

            var gd = ScreenManager.gd;

            btSavep = new Button(gd, new Vector2(142, 32), "Save Project..");
            btSavep.Position = Position + new Vector2(8);
            btSavep.SetColor(new Color(50, 50, 50));

            btLoadp = new Button(gd, new Vector2(142, 32), "Load Project..");
            btLoadp.Position = Position + new Vector2(8) + new Vector2(0, 40);
            btLoadp.SetColor(new Color(50, 50, 50));

            btSavea = new Button(gd, new Vector2(142, 32), "Export Animation..");
            btSavea.Position = Position + new Vector2(8) + new Vector2(0, 90);
            btSavea.SetColor(new Color(50, 50, 50));

            if(!Directory.Exists(exportSavePath))
                Directory.CreateDirectory(exportSavePath);

            if(!Directory.Exists(projectSavePath))
                Directory.CreateDirectory(projectSavePath);

        }

        public void Update(GameTime gt, GameScreen gs)
        {

            if(btSavep.IsReleased)
            {
                SaveProject();
            }

            if(btLoadp.IsReleased)
            {
                LoadProject();
            }

            if(btSavea.IsReleased)
            {
                ExportAnimation();
            }

        }

        public AnimationExportFile CreateExportFile()
        {
            var list = AnimationsListGui.Instance.Labels;
            List<AnimationData> fileFrames = new List<AnimationData>();

            foreach(var lb in list)
            {
                fileFrames.Add(lb.data);
            }

            var file = new AnimationExportFile(fileFrames);

            return file;
        }

        public AnimationProjectFile CreateProjectFile(string name)
        {
            var textures = SpritesheetListGui.Instance.List.Select(x => x.Name).ToList();

            var pf = new AnimationProjectFile(name, CreateExportFile(), textures);

            return pf;
        }

        public void ExportAnimation()
        {
            System.Windows.Forms.SaveFileDialog d = new System.Windows.Forms.SaveFileDialog();
            d.Filter = exportFilter;
            d.Title = "Export Animation";
            d.ShowDialog();

            if(d.FileName != "")
            {
                var file = CreateExportFile();
                JsonHelper.Save<AnimationExportFile>(d.FileName, file);
            }
        }

        public void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog d = new System.Windows.Forms.SaveFileDialog();
            d.InitialDirectory = Path.GetFullPath(projectSavePath);
            d.RestoreDirectory = true;

            d.Filter = projectFilter;
            d.Title = "Save Animation Project";
            d.ShowDialog();

            if(d.FileName != "")
            {
                var path = d.FileName;
                var name = Globals.GetNameFromPath(path);
                var file = CreateProjectFile(name);

                JsonHelper.Save<AnimationProjectFile>(path, file);
            }

        }

        public void LoadProject()
        {
            System.Windows.Forms.OpenFileDialog d = new System.Windows.Forms.OpenFileDialog();

            d.Filter = projectFilter;
            d.Title = "Load Animation Animation";

            if(d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = d.FileName;

                if(File.Exists(path))
                {
                    var file = JsonHelper.Load<AnimationProjectFile>(path);
                    SpritesheetListGui.Instance.LoadExistingFile(file.Textures.ToArray());
                    AnimationsListGui.Instance.LoadFile(file.AnimationFile);
                }

            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            btSavep.Draw(sb);
            btLoadp.Draw(sb);
            btSavea.Draw(sb);
        }

    }
}
