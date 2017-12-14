using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using ConsoleEngine.Scenes;
using ConsoleEngine.Components;

namespace ConsoleEngine.Editor
{
    public partial class GameEditorForm : Form
    {
        #region Form
        public static bool editorFocused;
        public static int editorFps = 0;
        public static int gameFps = 0;
        public GameEditorForm()
        {
            InitializeComponent();
            Activated += (object o, EventArgs e) => { editorFocused = true; };
            Deactivate += (object o, EventArgs e) => { editorFocused = false; };
            UpdateObjects();
            editorScene.MouseWheel += (object o, MouseEventArgs e) => { GameEditorScene.MouseWheel(e.Delta); };
            updateFormTimer.Tick += (object o, EventArgs e) =>
            {
                if (editorScene.Focused)
                {
                    ChooseObject(GameEditorScene.choosedIndex);
                    UpdatePosition();
                }
                UpdateFps();
                if (needUpdateObjectComponents)
                {
                    UpdateGameObjectComponentsList();
                    labelText = "";
                    needUpdateObjectComponents = false;
                }
                if (needInitScene)
                {
                    GameEditorScene.Init(editorScene);
                    needInitScene = false;
                }
            };
            UpdateComponentsList();
            needInitScene = true;
            Core.GameEngine.gameState = Core.GameEngine.GameState.editorPause;
        }
        bool needInitScene = false;
        private void GameEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameEditorScene.Stop();
        }
        #endregion
        #region Updates
        private bool needUpdateObjectComponents = false;
        private void UpdateFps()
        {
            gameFpsLabel.Text = "Game fps: " + gameFps.ToString();
            editorFpsLabel.Text = "Editor fps: " + editorFps.ToString();
        }
        private void UpdateObjects()
        {
            sceneObjectList.Items.Clear();
            List<GameObject> objects = SceneManager.currentScene.gameObjects;
            foreach (GameObject obj in objects)
            {
                sceneObjectList.Items.Add(obj);
            }
            objectInfo.Visible = false;
        }
        private void UpdatePosition()
        {
            if (sceneObjectList.SelectedItem != null)
            {
                selObjX.Text = ((GameObject)sceneObjectList.SelectedItem).position.x.ToString();
                selObjY.Text = ((GameObject)sceneObjectList.SelectedItem).position.y.ToString();
            }
        }
        private void UpdateComponentsList()
        {
            var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(GameComponent).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();

            foreach (object comp in listOfBs)
            {
                string name = comp.ToString();
                string _name = "";
                for (int i = name.Length - 1; i >= 0; i--)
                {
                    if (name[i] == '.')
                    {
                        break;
                    }
                    else
                    {
                        _name += name[i];
                    }
                }
                name = "";
                for (int i = _name.Length - 1; i >= 0; i--)
                {
                    name += _name[i];
                }
                if (typeof(GameComponent).Name != name)
                {
                    allComponentsComboBox.Items.Add(name);
                }
            }
        }
        private void UpdateGameObjectComponentsList()
        {
            if (sceneObjectList.SelectedItem != null)
            {
                componentsListView.Groups.Clear();
                componentsListView.Items.Clear();
                GameObject selectedObject = (GameObject)sceneObjectList.SelectedItem;
                foreach (GameComponent comp in selectedObject.components)
                {
                    string name = comp.ToString();
                    ListViewGroup g = componentsListView.Groups.Add(name, comp.ToString());
                    List<PropertyInfo> result = comp.GetType().GetProperties().Where(p => p.IsDefined(typeof(EditableProperty), false)).ToList();
                    ListViewItem item = componentsListView.Items.Add(new ListViewItem(name, g));
                    foreach (PropertyInfo inf in result)
                    {
                        item = componentsListView.Items.Add(new ListViewItem(inf.ToString(), g));
                        if (inf.GetValue(comp) != null)
                        {
                            item.SubItems.Add(inf.GetValue(comp).ToString());
                        }
                    }
                }
            }
        }
        #endregion
        #region Scene
        public int selObj;
        public void ChooseObject(int index)
        {
            if (index != -1)
            {
                sceneObjectList.SelectedIndex = index;
            }
        }
        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            GameEditorScene.OnResizeEnd();
        }
        private void editorScene_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GameEditorScene.mouseDown = true;
            }
            editorScene.Select();
            editorScene.Focus();
            GameEditorScene.MouseDown(e);

        }
        private void editorScene_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GameEditorScene.mouseDown = false;
            }
            GameEditorScene.MouseUp(e);
        }
        private void editorScene_MouseMove(object sender, MouseEventArgs e)
        {
            GameEditorScene.MouseMove(e);
        }
        private void GameEditorForm_ResizeEnd(object sender, EventArgs e)
        {
            GameEditorScene.OnResizeEnd();
        }
        #endregion
        #region GameObjects
        private void sceneObjectList_SelectedIndexChanged(object sender, EventArgs e) //При выборе игрового объекта из списка
        {
            if (sceneObjectList.SelectedItem != null)
            {
                UpdatePosition();
                objectNameTextBox.Text = sceneObjectList.SelectedItem.ToString();
                tagTextBox.Text = ((GameObject)sceneObjectList.SelectedItem).tag;
                if (!objectInfo.Visible)
                {
                    objectInfo.Visible = true;
                }
                UpdateGameObjectComponentsList();
            }
        }
        private void selObjX_KeyDown(object sender, KeyEventArgs e)
        {
            if (selObjX.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    editorScene.Select();
                    editorScene.Focus();
                    float.TryParse(selObjX.Text, out ((GameObject)sceneObjectList.SelectedItem).position.x);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    selObjY.Select();
                    selObjY.Focus();
                }
            }
        }
        private void selObjY_KeyDown(object sender, KeyEventArgs e)
        {
            if (selObjY.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    editorScene.Select();
                    editorScene.Focus();
                    float.TryParse(selObjY.Text, out ((GameObject)sceneObjectList.SelectedItem).position.y);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    selObjX.Select();
                    selObjX.Focus();
                }
            }
        }
        private void objectNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (objectNameTextBox.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    editorScene.Select();
                    editorScene.Focus();
                    int selectedIndex = sceneObjectList.SelectedIndex;
                    ((GameObject)sceneObjectList.SelectedItem).name = objectNameTextBox.Text;
                    UpdateObjects();
                    sceneObjectList.SelectedIndex = selectedIndex;
                }
            }
        }
        private void sceneObjectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (sceneObjectList.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                SceneManager.currentScene.RemoveGameObject(sceneObjectList.SelectedItem.ToString());
                sceneObjectList.Items.Remove(sceneObjectList.SelectedItem);
                objectInfo.Visible = false;
            }
        }
        private void addComponentToObjectButton_Click(object sender, EventArgs e)
        {
            AddComponentToSelectedGameObject();
            UpdateGameObjectComponentsList();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (componentsListView.SelectedItems.Count != 0)
            {
                string name = componentsListView.SelectedItems[0].Text;
                Type ct = Type.GetType("ConsoleEngine.Components." + name);
                if (ct != null)
                {
                    GameObject obj = ((GameObject)sceneObjectList.SelectedItem);
                    Type type = obj.GetType();
                    MethodInfo info = type.GetMethod("RemoveComponent");
                    info = info.MakeGenericMethod(ct);
                    info.Invoke(obj, null);
                    UpdateGameObjectComponentsList();
                }
            }
        }
        private void componentsListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string type = "";
            string name = "";
            for (int i = 0; i < labelText.Length; i++)
            {
                if (labelText[i] == ' ')
                {
                    name = labelText.Remove(0, i + 1);
                    type = labelText.Remove(i, labelText.Length - i);
                    break;
                }
            }
            SetComponentProperty(componentsListView.SelectedItems[0].Group.Name, name, type, e.Label);
            needUpdateObjectComponents = true;
        }
        string labelText = "";
        private void componentsListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (labelText == "")
            {
                labelText = componentsListView.SelectedItems[0].Text;
            }
        }
        private void swapObjectUpButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = sceneObjectList.SelectedIndex;
            if (selectedIndex <= 0)
            {
                return;
            }
            GameObject obj = sceneObjectList.SelectedItem as GameObject;
            GameObject downObj = sceneObjectList.Items[selectedIndex - 1] as GameObject;
            sceneObjectList.Items[selectedIndex - 1] = obj;
            sceneObjectList.Items[selectedIndex] = downObj;
            SceneManager.currentScene.gameObjects[selectedIndex - 1] = obj;
            SceneManager.currentScene.gameObjects[selectedIndex] = downObj;
            sceneObjectList.SelectedIndex = selectedIndex - 1;
        }
        private void swapObjectDownButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = sceneObjectList.SelectedIndex;
            if (selectedIndex >= sceneObjectList.Items.Count - 1)
            {
                return;
            }
            GameObject obj = sceneObjectList.SelectedItem as GameObject;
            GameObject upObj = sceneObjectList.Items[selectedIndex + 1] as GameObject;
            sceneObjectList.Items[selectedIndex + 1] = obj;
            sceneObjectList.Items[selectedIndex] = upObj;
            SceneManager.currentScene.gameObjects[selectedIndex + 1] = obj;
            SceneManager.currentScene.gameObjects[selectedIndex] = upObj;
            sceneObjectList.SelectedIndex = selectedIndex + 1;
        }
        private void tagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (tagTextBox.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ((GameObject)sceneObjectList.SelectedItem).tag = tagTextBox.Text;
                    int selectedIndex = sceneObjectList.SelectedIndex;
                    UpdateObjects();
                    sceneObjectList.SelectedIndex = selectedIndex;
                }
            }
        }
        private void SetComponentProperty(string componentName, string propertyName, string propertyType, string value)
        {
            GameObject obj = ((GameObject)sceneObjectList.SelectedItem);
            Type type = obj.GetType();
            MethodInfo info = type.GetMethod("GetComponent");
            Type ct = Type.GetType("ConsoleEngine.Components." + componentName);
            info = info.MakeGenericMethod(ct);
            GameComponent component = info.Invoke(obj, null) as GameComponent;

            List<PropertyInfo> result = component.GetType().GetProperties().Where(p => p.IsDefined(typeof(EditableProperty), false)).ToList();
            foreach (PropertyInfo inf in result)
            {
                if (inf.Name == propertyName)
                {
                    try
                    {
                        if (inf.PropertyType.IsEnum)
                        {
                            object o = Enum.Parse(inf.PropertyType, value);
                            inf.SetValue(component, o, null);
                        }
                        else
                        {
                            if (propertyType.Contains("System."))
                            {
                                propertyType = propertyType.Remove(0, 7);
                            }
                            Type t = Type.GetType("System." + propertyType);
                            object o = Convert.ChangeType(value, t);
                            inf.SetValue(component, o, null);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    return;
                }
            }
        }
        private void AddComponentToSelectedGameObject()
        {
            if (allComponentsComboBox.SelectedItem != null)
            {

                GameObject obj = ((GameObject)sceneObjectList.SelectedItem);
                Type type = obj.GetType();
                MethodInfo info = type.GetMethod("AddComponent");
                Type t = Type.GetType("ConsoleEngine.Components." + allComponentsComboBox.SelectedItem.ToString());
                info = info.MakeGenericMethod(t);
                info.Invoke(obj, null);
            }
        }
        #endregion
        #region Menu
        private void openSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSceneDialog.Filter = "Файл сцены|*.scn";
            openSceneDialog.Title = "Выберите файл сцены";
            if (openSceneDialog.ShowDialog() == DialogResult.OK)
            {
                SceneManager.LoadScene(openSceneDialog.FileName);
                UpdateObjects();
            }
            openSceneDialog.Dispose();
        }
        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSceneDialog.Filter = "Файл сцены (*.scn)|*.scn|Все файлы (*.*)|*.*";
            if (saveSceneDialog.ShowDialog() == DialogResult.OK)
            {
                SceneManager.SaveScene(saveSceneDialog.FileName);
            }
        }
        private void createObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject obj = SceneManager.currentScene.AddGameObject(new GameObject("gameObject" + SceneManager.currentScene.gameObjects.Count, new Vector2(0, 0), new List<Components.GameComponent>()));
            sceneObjectList.Items.Add(obj);
        }
        private void saveObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sceneObjectList.SelectedItem != null)
            {
                saveSceneDialog.Filter = "Файл префаба (*.prefab)|*.prefab|Все файлы (*.*)|*.*";
                if (saveSceneDialog.ShowDialog() == DialogResult.OK)
                {
                    Assets.AssetManager.SavePrefab((GameObject)sceneObjectList.SelectedItem, saveSceneDialog.FileName);
                }
            }
        }
        private void createEmpySceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneManager.CreateNewScene();
            objectInfo.Visible = false;
            UpdateObjects();
        }
        private void addObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSceneDialog.Filter = "Файл префаба|*.prefab";
            openSceneDialog.Title = "Выберите файл префаба";
            if (openSceneDialog.ShowDialog() == DialogResult.OK)
            {
                SceneManager.currentScene.AddGameObject(Assets.AssetManager.LoadPrefab(openSceneDialog.FileName));
                UpdateObjects();
            }
        }
        private void pauseMenuItem_Click(object sender, EventArgs e)
        {
            if (Core.GameEngine.gameState == Core.GameEngine.GameState.active)
            {
                Core.GameEngine.gameState = Core.GameEngine.GameState.editorPause;
                pauseMenuItem.Text = "Возобновить";
            }
            else if (Core.GameEngine.gameState == Core.GameEngine.GameState.editorPause)
            {
                Core.GameEngine.gameState = Core.GameEngine.GameState.active;
                pauseMenuItem.Text = "Пауза";
            }
        }
        private void reloadResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assets.AssetManager.LoadAssetsByPath(Core.GameEngine.GetDirectoryPath() + Core.GameEngine.GAME_SPRITES_PATH);
        }
        #endregion
    }
}
