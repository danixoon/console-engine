namespace ConsoleEngine.Editor
{
    partial class GameEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sceneObjectList = new System.Windows.Forms.ListBox();
            this.gameObjectY = new System.Windows.Forms.Label();
            this.gameObjectX = new System.Windows.Forms.Label();
            this.selObjY = new System.Windows.Forms.TextBox();
            this.selObjX = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.objectInfo = new System.Windows.Forms.Panel();
            this.swapObjectDownButton = new System.Windows.Forms.Button();
            this.swapObjectUpButton = new System.Windows.Forms.Button();
            this.componentsListView = new System.Windows.Forms.ListView();
            this.componentVarHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.componentValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectComponentsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allComponentsComboBox = new System.Windows.Forms.ComboBox();
            this.addComponentToObjectButton = new System.Windows.Forms.Button();
            this.objectNameTextBox = new System.Windows.Forms.TextBox();
            this.debugInfo = new System.Windows.Forms.StatusStrip();
            this.gameFpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.editorFpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateFormTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createEmpySceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.объектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ресурсыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перезагрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.editorScene = new System.Windows.Forms.Panel();
            this.openSceneDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveSceneDialog = new System.Windows.Forms.SaveFileDialog();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.objectInfo.SuspendLayout();
            this.objectComponentsMenuStrip.SuspendLayout();
            this.debugInfo.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sceneObjectList
            // 
            this.sceneObjectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneObjectList.FormattingEnabled = true;
            this.sceneObjectList.Items.AddRange(new object[] {
            "котик1",
            "котик2",
            "дельфин",
            "неешьменя"});
            this.sceneObjectList.Location = new System.Drawing.Point(0, 0);
            this.sceneObjectList.Name = "sceneObjectList";
            this.sceneObjectList.Size = new System.Drawing.Size(349, 302);
            this.sceneObjectList.TabIndex = 1;
            this.sceneObjectList.SelectedIndexChanged += new System.EventHandler(this.sceneObjectList_SelectedIndexChanged);
            this.sceneObjectList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sceneObjectList_KeyDown);
            // 
            // gameObjectY
            // 
            this.gameObjectY.AutoSize = true;
            this.gameObjectY.Location = new System.Drawing.Point(90, 32);
            this.gameObjectY.Name = "gameObjectY";
            this.gameObjectY.Size = new System.Drawing.Size(14, 13);
            this.gameObjectY.TabIndex = 5;
            this.gameObjectY.Text = "Y";
            // 
            // gameObjectX
            // 
            this.gameObjectX.AutoSize = true;
            this.gameObjectX.Location = new System.Drawing.Point(1, 32);
            this.gameObjectX.Name = "gameObjectX";
            this.gameObjectX.Size = new System.Drawing.Size(14, 13);
            this.gameObjectX.TabIndex = 5;
            this.gameObjectX.Text = "X";
            // 
            // selObjY
            // 
            this.selObjY.Location = new System.Drawing.Point(110, 29);
            this.selObjY.Name = "selObjY";
            this.selObjY.Size = new System.Drawing.Size(65, 20);
            this.selObjY.TabIndex = 4;
            this.selObjY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selObjY_KeyDown);
            // 
            // selObjX
            // 
            this.selObjX.Location = new System.Drawing.Point(19, 29);
            this.selObjX.Name = "selObjX";
            this.selObjX.Size = new System.Drawing.Size(65, 20);
            this.selObjX.TabIndex = 4;
            this.selObjX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selObjX_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sceneObjectList);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.objectInfo);
            this.splitContainer1.Size = new System.Drawing.Size(992, 302);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 302);
            this.panel2.TabIndex = 2;
            // 
            // objectInfo
            // 
            this.objectInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectInfo.Controls.Add(this.tagTextBox);
            this.objectInfo.Controls.Add(this.swapObjectDownButton);
            this.objectInfo.Controls.Add(this.swapObjectUpButton);
            this.objectInfo.Controls.Add(this.componentsListView);
            this.objectInfo.Controls.Add(this.allComponentsComboBox);
            this.objectInfo.Controls.Add(this.addComponentToObjectButton);
            this.objectInfo.Controls.Add(this.objectNameTextBox);
            this.objectInfo.Controls.Add(this.gameObjectY);
            this.objectInfo.Controls.Add(this.selObjY);
            this.objectInfo.Controls.Add(this.selObjX);
            this.objectInfo.Controls.Add(this.gameObjectX);
            this.objectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectInfo.Location = new System.Drawing.Point(0, 0);
            this.objectInfo.Name = "objectInfo";
            this.objectInfo.Size = new System.Drawing.Size(639, 302);
            this.objectInfo.TabIndex = 6;
            // 
            // swapObjectDownButton
            // 
            this.swapObjectDownButton.Location = new System.Drawing.Point(315, 29);
            this.swapObjectDownButton.Name = "swapObjectDownButton";
            this.swapObjectDownButton.Size = new System.Drawing.Size(75, 21);
            this.swapObjectDownButton.TabIndex = 10;
            this.swapObjectDownButton.Text = "Вниз";
            this.swapObjectDownButton.UseVisualStyleBackColor = true;
            this.swapObjectDownButton.Click += new System.EventHandler(this.swapObjectDownButton_Click);
            // 
            // swapObjectUpButton
            // 
            this.swapObjectUpButton.Location = new System.Drawing.Point(315, 3);
            this.swapObjectUpButton.Name = "swapObjectUpButton";
            this.swapObjectUpButton.Size = new System.Drawing.Size(75, 21);
            this.swapObjectUpButton.TabIndex = 10;
            this.swapObjectUpButton.Text = "Вверх";
            this.swapObjectUpButton.UseVisualStyleBackColor = true;
            this.swapObjectUpButton.Click += new System.EventHandler(this.swapObjectUpButton_Click);
            // 
            // componentsListView
            // 
            this.componentsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.componentsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.componentVarHeader,
            this.componentValueHeader});
            this.componentsListView.ContextMenuStrip = this.objectComponentsMenuStrip;
            this.componentsListView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.componentsListView.LabelEdit = true;
            this.componentsListView.Location = new System.Drawing.Point(-1, 55);
            this.componentsListView.Name = "componentsListView";
            this.componentsListView.Size = new System.Drawing.Size(639, 246);
            this.componentsListView.TabIndex = 7;
            this.componentsListView.UseCompatibleStateImageBehavior = false;
            this.componentsListView.View = System.Windows.Forms.View.Details;
            this.componentsListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.componentsListView_AfterLabelEdit);
            this.componentsListView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.componentsListView_BeforeLabelEdit);
            // 
            // componentVarHeader
            // 
            this.componentVarHeader.Text = "Параметр";
            this.componentVarHeader.Width = 131;
            // 
            // componentValueHeader
            // 
            this.componentValueHeader.Text = "Значение:";
            this.componentValueHeader.Width = 415;
            // 
            // objectComponentsMenuStrip
            // 
            this.objectComponentsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem});
            this.objectComponentsMenuStrip.Name = "objectComponentsMenuStrip";
            this.objectComponentsMenuStrip.Size = new System.Drawing.Size(119, 26);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // allComponentsComboBox
            // 
            this.allComponentsComboBox.FormattingEnabled = true;
            this.allComponentsComboBox.Location = new System.Drawing.Point(181, 3);
            this.allComponentsComboBox.Name = "allComponentsComboBox";
            this.allComponentsComboBox.Size = new System.Drawing.Size(128, 21);
            this.allComponentsComboBox.TabIndex = 9;
            // 
            // addComponentToObjectButton
            // 
            this.addComponentToObjectButton.Location = new System.Drawing.Point(181, 29);
            this.addComponentToObjectButton.Name = "addComponentToObjectButton";
            this.addComponentToObjectButton.Size = new System.Drawing.Size(128, 21);
            this.addComponentToObjectButton.TabIndex = 8;
            this.addComponentToObjectButton.Text = "Добавить компонент";
            this.addComponentToObjectButton.UseVisualStyleBackColor = true;
            this.addComponentToObjectButton.Click += new System.EventHandler(this.addComponentToObjectButton_Click);
            // 
            // objectNameTextBox
            // 
            this.objectNameTextBox.Location = new System.Drawing.Point(19, 3);
            this.objectNameTextBox.Name = "objectNameTextBox";
            this.objectNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.objectNameTextBox.TabIndex = 6;
            this.objectNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectNameTextBox_KeyDown);
            // 
            // debugInfo
            // 
            this.debugInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameFpsLabel,
            this.editorFpsLabel});
            this.debugInfo.Location = new System.Drawing.Point(0, 694);
            this.debugInfo.Name = "debugInfo";
            this.debugInfo.Size = new System.Drawing.Size(992, 22);
            this.debugInfo.TabIndex = 6;
            this.debugInfo.Text = "debugInfo";
            // 
            // gameFpsLabel
            // 
            this.gameFpsLabel.Name = "gameFpsLabel";
            this.gameFpsLabel.Size = new System.Drawing.Size(56, 17);
            this.gameFpsLabel.Text = "GameFps";
            // 
            // editorFpsLabel
            // 
            this.editorFpsLabel.Name = "editorFpsLabel";
            this.editorFpsLabel.Size = new System.Drawing.Size(56, 17);
            this.editorFpsLabel.Text = "EditorFps";
            // 
            // updateFormTimer
            // 
            this.updateFormTimer.Enabled = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.объектToolStripMenuItem,
            this.ресурсыToolStripMenuItem,
            this.pauseMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(992, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSceneToolStripMenuItem,
            this.saveSceneToolStripMenuItem,
            this.createEmpySceneToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openSceneToolStripMenuItem
            // 
            this.openSceneToolStripMenuItem.Name = "openSceneToolStripMenuItem";
            this.openSceneToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openSceneToolStripMenuItem.Text = "Открыть сцену";
            this.openSceneToolStripMenuItem.Click += new System.EventHandler(this.openSceneToolStripMenuItem_Click);
            // 
            // saveSceneToolStripMenuItem
            // 
            this.saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            this.saveSceneToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveSceneToolStripMenuItem.Text = "Сохранить сцену";
            this.saveSceneToolStripMenuItem.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // createEmpySceneToolStripMenuItem
            // 
            this.createEmpySceneToolStripMenuItem.Name = "createEmpySceneToolStripMenuItem";
            this.createEmpySceneToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createEmpySceneToolStripMenuItem.Text = "Создать пустую сцену";
            this.createEmpySceneToolStripMenuItem.Click += new System.EventHandler(this.createEmpySceneToolStripMenuItem_Click);
            // 
            // объектToolStripMenuItem
            // 
            this.объектToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createObjToolStripMenuItem,
            this.openObjToolStripMenuItem,
            this.saveObjToolStripMenuItem});
            this.объектToolStripMenuItem.Name = "объектToolStripMenuItem";
            this.объектToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.объектToolStripMenuItem.Text = "Объект";
            // 
            // createObjToolStripMenuItem
            // 
            this.createObjToolStripMenuItem.Name = "createObjToolStripMenuItem";
            this.createObjToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.createObjToolStripMenuItem.Text = "Создать";
            this.createObjToolStripMenuItem.Click += new System.EventHandler(this.createObjToolStripMenuItem_Click);
            // 
            // openObjToolStripMenuItem
            // 
            this.openObjToolStripMenuItem.Name = "openObjToolStripMenuItem";
            this.openObjToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openObjToolStripMenuItem.Text = "Добавить";
            this.openObjToolStripMenuItem.Click += new System.EventHandler(this.addObjToolStripMenuItem_Click);
            // 
            // saveObjToolStripMenuItem
            // 
            this.saveObjToolStripMenuItem.Name = "saveObjToolStripMenuItem";
            this.saveObjToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveObjToolStripMenuItem.Text = "Сохранить";
            this.saveObjToolStripMenuItem.Click += new System.EventHandler(this.saveObjToolStripMenuItem_Click);
            // 
            // ресурсыToolStripMenuItem
            // 
            this.ресурсыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.перезагрузитьToolStripMenuItem});
            this.ресурсыToolStripMenuItem.Name = "ресурсыToolStripMenuItem";
            this.ресурсыToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.ресурсыToolStripMenuItem.Text = "Ресурсы";
            // 
            // перезагрузитьToolStripMenuItem
            // 
            this.перезагрузитьToolStripMenuItem.Name = "перезагрузитьToolStripMenuItem";
            this.перезагрузитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.перезагрузитьToolStripMenuItem.Text = "Перезагрузить";
            this.перезагрузитьToolStripMenuItem.Click += new System.EventHandler(this.reloadResourcesToolStripMenuItem_Click);
            // 
            // pauseMenuItem
            // 
            this.pauseMenuItem.Name = "pauseMenuItem";
            this.pauseMenuItem.Size = new System.Drawing.Size(90, 20);
            this.pauseMenuItem.Text = "Возобновить";
            this.pauseMenuItem.Click += new System.EventHandler(this.pauseMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.editorScene);
            this.splitContainer2.Size = new System.Drawing.Size(992, 670);
            this.splitContainer2.SplitterDistance = 302;
            this.splitContainer2.TabIndex = 8;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // editorScene
            // 
            this.editorScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorScene.Location = new System.Drawing.Point(0, 0);
            this.editorScene.Name = "editorScene";
            this.editorScene.Size = new System.Drawing.Size(992, 364);
            this.editorScene.TabIndex = 0;
            this.editorScene.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorScene_MouseDown);
            this.editorScene.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorScene_MouseMove);
            this.editorScene.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editorScene_MouseUp);
            // 
            // openSceneDialog
            // 
            this.openSceneDialog.FileName = "scene.scn";
            // 
            // tagTextBox
            // 
            this.tagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTextBox.Location = new System.Drawing.Point(396, 4);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(238, 20);
            this.tagTextBox.TabIndex = 11;
            this.tagTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tagTextBox_KeyDown);
            // 
            // GameEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 716);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.debugInfo);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(659, 519);
            this.Name = "GameEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameEditorForm_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.GameEditorForm_ResizeEnd);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.objectInfo.ResumeLayout(false);
            this.objectInfo.PerformLayout();
            this.objectComponentsMenuStrip.ResumeLayout(false);
            this.debugInfo.ResumeLayout(false);
            this.debugInfo.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox sceneObjectList;
        private System.Windows.Forms.Label gameObjectX;
        private System.Windows.Forms.TextBox selObjY;
        private System.Windows.Forms.TextBox selObjX;
        private System.Windows.Forms.Label gameObjectY;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip debugInfo;
        private System.Windows.Forms.ToolStripStatusLabel gameFpsLabel;
        private System.Windows.Forms.Panel objectInfo;
        private System.Windows.Forms.Timer updateFormTimer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSceneToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.OpenFileDialog openSceneDialog;
        private System.Windows.Forms.SaveFileDialog saveSceneDialog;
        private System.Windows.Forms.Panel editorScene;
        private System.Windows.Forms.ToolStripMenuItem объектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createObjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openObjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveObjToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel editorFpsLabel;
        private System.Windows.Forms.ToolStripMenuItem createEmpySceneToolStripMenuItem;
        private System.Windows.Forms.TextBox objectNameTextBox;
        private System.Windows.Forms.ListView componentsListView;
        private System.Windows.Forms.ColumnHeader componentVarHeader;
        private System.Windows.Forms.Button addComponentToObjectButton;
        private System.Windows.Forms.ComboBox allComponentsComboBox;
        private System.Windows.Forms.ContextMenuStrip objectComponentsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader componentValueHeader;
        private System.Windows.Forms.ToolStripMenuItem ресурсыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перезагрузитьToolStripMenuItem;
        private System.Windows.Forms.Button swapObjectDownButton;
        private System.Windows.Forms.Button swapObjectUpButton;
        private System.Windows.Forms.ToolStripMenuItem pauseMenuItem;
        private System.Windows.Forms.TextBox tagTextBox;
    }
}