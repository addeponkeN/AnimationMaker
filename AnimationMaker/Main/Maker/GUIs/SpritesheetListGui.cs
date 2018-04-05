using AnimationMaker.Gamer;
using AnimationMaker.Screener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.GameUtility;
using Obo.Gui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{

    public class SpritesheetListGui : Sprite
    {

        static SpritesheetListGui instance = new SpritesheetListGui();
        public static SpritesheetListGui Instance => instance;

        public List<Spritesheet> List = new List<Spritesheet>();

        public Button btAdd;

        public bool IsAddingSheet;

        public SpritesheetListGui()
        {
            Texture = GameContent.box;
            Color = new Color(60, 60, 60);
            Position = Gui.ListPos;
            Size = Gui.ListSize;

            btAdd = new Button(ScreenManager.gd, new Vector2(120, 32), "Add Sheet");
            btAdd.Position = new Vector2(Position.X + 4, Position.Y + 4);
            btAdd.SetColor(new Color(50, 50, 50));
        }

        public void LoadExistingFile(params string[] files)
        {
            try
            {
                for(int i = 0; i < files.Length; i++)
                {
                    var name = files[i];
                    var texture = GameContent.LoadRawTexture("Spritesheets/" + name);
                    var sheet = new Spritesheet(texture);
                    sheet.Name = name;
                    List.Add(sheet);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            SetPositions();
        }

        public void Add(params string[] filePaths)
        {
            try
            {
                for(int i = 0; i < filePaths.Length; i++)
                {
                    string path = filePaths[i];
                    string fileName = GetFileNameFromPath(path);

                    //  replace new with old if names are same
                    for(int j = 0; j < List.Count; j++)
                    {
                        if(List[j].Name == fileName)
                        {
                            List.RemoveAt(j);
                            break;
                        }
                    }

                    CopyFileToContentFolder(path);

                    var texture = GameContent.LoadRawTexture("Spritesheets/" + fileName);
                    var sheet = new Spritesheet(texture);
                    sheet.Name = fileName;
                    List.Add(sheet);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            SetPositions();

        }

        void SetPositions()
        {
            int i = 0;
            foreach(var s in List)
            {
                s.Position = new Vector2(Position.X + 16, Position.Y + 16 + btAdd.Size.Y + (i * s.Size.Y) + (i * 8));
                i++;
            }
        }

        string GetFileNameFromPath(string path)
        {
            var fileName = path.Split('\\').Last();
            return fileName;
        }

        void CopyFileToContentFolder(string path)
        {
            //await Task.Run(() => 
            //{
            if(!Directory.Exists("Spritesheets"))
                Directory.CreateDirectory("Spritesheets");

            var fileName = GetFileNameFromPath(path);

            //  TODO: overwrite prompt
            if(File.Exists("Spritesheets/" + fileName))
                File.Delete("Spritesheets/" + fileName);

            File.Copy(path, "Spritesheets/" + fileName);
            // });
        }


        public void Update()
        {
            Position = Gui.ListPos;
            Size = Gui.ListSize;

            if(btAdd.IsClicked)
            {
                IsAddingSheet = true;

                //  temp:
                //Add(@"X:\Dev\playerSheet.png");
                //--


                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.InitialDirectory = @"X:\Dev\";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|jpg (*.jpg;*.jpeg)|*.jpg;*.jpeg|png (*.png)|*.png|gif (*.gif)|*.gif";
                ofd.FilterIndex = 1;
                ofd.Multiselect = true;

                if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var files = ofd.FileNames;
                    Add(files);
                    IsAddingSheet = false;
                }

            }

            if(Input.LeftClick && Rectangle.Contains(Input.MousePos))
                foreach(var s in List)
                {
                    if(s.Rectangle.Contains(Input.MousePos))
                    {
                        SetSelectedSheet(s);
                        break;
                    }
                }

        }

        void SetSelectedSheet(Spritesheet sheet)
        {
            SelectorGui.Instance.SelectedSheet = sheet;
            FramesGui.Instance.SelectedSheet = sheet;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            btAdd.Draw(sb);

            foreach(var s in List)
            {
                s.Draw(sb);
            }

        }

    }

}
