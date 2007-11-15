/*
 * Created by SharpDevelop.
 * User: laurabeth
 * Date: 11/30/2006
 * Time: 10:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using System.Text;
using System.Xml;

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

namespace Spreadsheets
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public class SpreadsheetsSampleApp : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox listAddTextBox;
        private System.Windows.Forms.Button listAddSubmitButton;
        private System.Windows.Forms.TabPage cellsFeedTabPage;
        private System.Windows.Forms.ListView cellsListView;
        private System.Windows.Forms.TextBox listOrderByTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox listUpdateTextBox;
        private System.Windows.Forms.Label listQueryQueryLabel;
        private System.Windows.Forms.TabPage updateCellsTabPage;
        private System.Windows.Forms.TabControl cellsActionTabContol;
        private System.Windows.Forms.TextBox cellUpdateColumnTextBox;
        private System.Windows.Forms.Label listDeleteEntryLabel;
        private System.Windows.Forms.ListView worksheetListView;
        private System.Windows.Forms.Button cellUpdateSubmitButton;
        private System.Windows.Forms.TabPage listInsertTabPage;
        private System.Windows.Forms.TabControl listActionTabControl;
        private System.Windows.Forms.TabPage listDeleteTabPage;
        private System.Windows.Forms.TabPage loginTabPage;
        private System.Windows.Forms.CheckBox listQueryReverseCheckBox;
        private System.Windows.Forms.Button listDeleteSubmitButton;
        private System.Windows.Forms.ComboBox listEntryComboBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label cellUpdateDataLabel;
        private System.Windows.Forms.Label listEntryLabel;
        private System.Windows.Forms.ListView listListView;
        private System.Windows.Forms.TabPage spreadsheetListTabPage;
        private System.Windows.Forms.Button listQuerySubmitButton;
        private System.Windows.Forms.Label listAddExampleLabel;
        private System.Windows.Forms.TextBox listDeleteEntryTextBox;
        private System.Windows.Forms.Label cellUpdateRowLabel;
        private System.Windows.Forms.TextBox cellUpdateDataTextBox;
        private System.Windows.Forms.ListView spreadsheetListView;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox listQueryQueryTextBox;
        private System.Windows.Forms.TabPage listUpdateTabPage;
        private System.Windows.Forms.Label listQueryOrderByLabel;
        private System.Windows.Forms.TabPage worksheetListTabPage;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TabPage listQueryTabPage;
        private System.Windows.Forms.Button listUpdateSubmitButton;
        private System.Windows.Forms.Label cellUpdateColumnLabel;
        private System.Windows.Forms.TabPage listFeedTabPage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TextBox cellUpdateRowTextBox;
        private SpreadsheetsService service;
        private System.Collections.Hashtable editUriTable;

        public SpreadsheetsSampleApp()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            service = new SpreadsheetsService("Spreadsheet-GData-Sample-App");

            editUriTable = new System.Collections.Hashtable();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new SpreadsheetsSampleApp());
        }

#region Windows Forms Designer generated code
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.cellUpdateRowTextBox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.listFeedTabPage = new System.Windows.Forms.TabPage();
            this.cellUpdateColumnLabel = new System.Windows.Forms.Label();
            this.listUpdateSubmitButton = new System.Windows.Forms.Button();
            this.listQueryTabPage = new System.Windows.Forms.TabPage();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.worksheetListTabPage = new System.Windows.Forms.TabPage();
            this.listQueryOrderByLabel = new System.Windows.Forms.Label();
            this.listUpdateTabPage = new System.Windows.Forms.TabPage();
            this.listQueryQueryTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.spreadsheetListView = new System.Windows.Forms.ListView();
            this.cellUpdateDataTextBox = new System.Windows.Forms.TextBox();
            this.cellUpdateRowLabel = new System.Windows.Forms.Label();
            this.listDeleteEntryTextBox = new System.Windows.Forms.TextBox();
            this.listAddExampleLabel = new System.Windows.Forms.Label();
            this.listQuerySubmitButton = new System.Windows.Forms.Button();
            this.spreadsheetListTabPage = new System.Windows.Forms.TabPage();
            this.listListView = new System.Windows.Forms.ListView();
            this.listEntryLabel = new System.Windows.Forms.Label();
            this.cellUpdateDataLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.listEntryComboBox = new System.Windows.Forms.ComboBox();
            this.listDeleteSubmitButton = new System.Windows.Forms.Button();
            this.listQueryReverseCheckBox = new System.Windows.Forms.CheckBox();
            this.loginTabPage = new System.Windows.Forms.TabPage();
            this.listDeleteTabPage = new System.Windows.Forms.TabPage();
            this.listActionTabControl = new System.Windows.Forms.TabControl();
            this.listInsertTabPage = new System.Windows.Forms.TabPage();
            this.cellUpdateSubmitButton = new System.Windows.Forms.Button();
            this.worksheetListView = new System.Windows.Forms.ListView();
            this.listDeleteEntryLabel = new System.Windows.Forms.Label();
            this.cellUpdateColumnTextBox = new System.Windows.Forms.TextBox();
            this.cellsActionTabContol = new System.Windows.Forms.TabControl();
            this.updateCellsTabPage = new System.Windows.Forms.TabPage();
            this.listQueryQueryLabel = new System.Windows.Forms.Label();
            this.listUpdateTextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.listOrderByTextBox = new System.Windows.Forms.TextBox();
            this.cellsListView = new System.Windows.Forms.ListView();
            this.cellsFeedTabPage = new System.Windows.Forms.TabPage();
            this.listAddSubmitButton = new System.Windows.Forms.Button();
            this.listAddTextBox = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.listFeedTabPage.SuspendLayout();
            this.listQueryTabPage.SuspendLayout();
            this.worksheetListTabPage.SuspendLayout();
            this.listUpdateTabPage.SuspendLayout();
            this.spreadsheetListTabPage.SuspendLayout();
            this.loginTabPage.SuspendLayout();
            this.listDeleteTabPage.SuspendLayout();
            this.listActionTabControl.SuspendLayout();
            this.listInsertTabPage.SuspendLayout();
            this.cellsActionTabContol.SuspendLayout();
            this.updateCellsTabPage.SuspendLayout();
            this.cellsFeedTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cellUpdateRowTextBox
            // 
            this.cellUpdateRowTextBox.Location = new System.Drawing.Point(48, 8);
            this.cellUpdateRowTextBox.Name = "cellUpdateRowTextBox";
            this.cellUpdateRowTextBox.Size = new System.Drawing.Size(48, 20);
            this.cellUpdateRowTextBox.TabIndex = 2;
            this.cellUpdateRowTextBox.Text = "";
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.loginTabPage);
            this.tabControl.Controls.Add(this.spreadsheetListTabPage);
            this.tabControl.Controls.Add(this.worksheetListTabPage);
            this.tabControl.Controls.Add(this.cellsFeedTabPage);
            this.tabControl.Controls.Add(this.listFeedTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 550);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // listFeedTabPage
            // 
            this.listFeedTabPage.Controls.Add(this.listActionTabControl);
            this.listFeedTabPage.Controls.Add(this.listListView);
            this.listFeedTabPage.Location = new System.Drawing.Point(4, 25);
            this.listFeedTabPage.Name = "listFeedTabPage";
            this.listFeedTabPage.Size = new System.Drawing.Size(792, 521);
            this.listFeedTabPage.TabIndex = 5;
            this.listFeedTabPage.Text = "List Feed";
            // 
            // cellUpdateColumnLabel
            // 
            this.cellUpdateColumnLabel.Location = new System.Drawing.Point(104, 8);
            this.cellUpdateColumnLabel.Name = "cellUpdateColumnLabel";
            this.cellUpdateColumnLabel.Size = new System.Drawing.Size(48, 23);
            this.cellUpdateColumnLabel.TabIndex = 3;
            this.cellUpdateColumnLabel.Text = "Column";
            this.cellUpdateColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listUpdateSubmitButton
            // 
            this.listUpdateSubmitButton.Location = new System.Drawing.Point(688, 104);
            this.listUpdateSubmitButton.Name = "listUpdateSubmitButton";
            this.listUpdateSubmitButton.TabIndex = 2;
            this.listUpdateSubmitButton.Text = "Submit";
            this.listUpdateSubmitButton.Click += new System.EventHandler(this.OnClickSubmitButton);
            // 
            // listQueryTabPage
            // 
            this.listQueryTabPage.Controls.Add(this.listQuerySubmitButton);
            this.listQueryTabPage.Controls.Add(this.listQueryQueryTextBox);
            this.listQueryTabPage.Controls.Add(this.listQueryQueryLabel);
            this.listQueryTabPage.Controls.Add(this.listQueryReverseCheckBox);
            this.listQueryTabPage.Controls.Add(this.listOrderByTextBox);
            this.listQueryTabPage.Controls.Add(this.listQueryOrderByLabel);
            this.listQueryTabPage.Location = new System.Drawing.Point(4, 22);
            this.listQueryTabPage.Name = "listQueryTabPage";
            this.listQueryTabPage.Size = new System.Drawing.Size(776, 134);
            this.listQueryTabPage.TabIndex = 0;
            this.listQueryTabPage.Text = "Query";
            // 
            // passwordLabel
            // 
            this.passwordLabel.Location = new System.Drawing.Point(24, 64);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(72, 24);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password";
            // 
            // worksheetListTabPage
            // 
            this.worksheetListTabPage.Controls.Add(this.worksheetListView);
            this.worksheetListTabPage.Location = new System.Drawing.Point(4, 25);
            this.worksheetListTabPage.Name = "worksheetListTabPage";
            this.worksheetListTabPage.Size = new System.Drawing.Size(792, 521);
            this.worksheetListTabPage.TabIndex = 2;
            this.worksheetListTabPage.Text = "Select Worksheet";
            // 
            // listQueryOrderByLabel
            // 
            this.listQueryOrderByLabel.Location = new System.Drawing.Point(8, 8);
            this.listQueryOrderByLabel.Name = "listQueryOrderByLabel";
            this.listQueryOrderByLabel.TabIndex = 0;
            this.listQueryOrderByLabel.Text = "orderby=column:";
            this.listQueryOrderByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listUpdateTabPage
            // 
            this.listUpdateTabPage.Controls.Add(this.listEntryComboBox);
            this.listUpdateTabPage.Controls.Add(this.listEntryLabel);
            this.listUpdateTabPage.Controls.Add(this.listUpdateSubmitButton);
            this.listUpdateTabPage.Controls.Add(this.listUpdateTextBox);
            this.listUpdateTabPage.Location = new System.Drawing.Point(4, 22);
            this.listUpdateTabPage.Name = "listUpdateTabPage";
            this.listUpdateTabPage.Size = new System.Drawing.Size(776, 134);
            this.listUpdateTabPage.TabIndex = 2;
            this.listUpdateTabPage.Text = "Update";
            // 
            // listQueryQueryTextBox
            // 
            this.listQueryQueryTextBox.Location = new System.Drawing.Point(112, 32);
            this.listQueryQueryTextBox.Name = "listQueryQueryTextBox";
            this.listQueryQueryTextBox.Size = new System.Drawing.Size(328, 20);
            this.listQueryQueryTextBox.TabIndex = 4;
            this.listQueryQueryTextBox.Text = "";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(96, 32);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(168, 20);
            this.usernameTextBox.TabIndex = 1;
            this.usernameTextBox.Text = "";
            // 
            // spreadsheetListView
            // 
            this.spreadsheetListView.Location = new System.Drawing.Point(8, 8);
            this.spreadsheetListView.MultiSelect = false;
            this.spreadsheetListView.Name = "spreadsheetListView";
            this.spreadsheetListView.Size = new System.Drawing.Size(776, 504);
            this.spreadsheetListView.TabIndex = 0;
            this.spreadsheetListView.View = System.Windows.Forms.View.List;
            // 
            // cellUpdateDataTextBox
            // 
            this.cellUpdateDataTextBox.Location = new System.Drawing.Point(48, 40);
            this.cellUpdateDataTextBox.Name = "cellUpdateDataTextBox";
            this.cellUpdateDataTextBox.Size = new System.Drawing.Size(160, 20);
            this.cellUpdateDataTextBox.TabIndex = 6;
            this.cellUpdateDataTextBox.Text = "";
            // 
            // cellUpdateRowLabel
            // 
            this.cellUpdateRowLabel.Location = new System.Drawing.Point(8, 8);
            this.cellUpdateRowLabel.Name = "cellUpdateRowLabel";
            this.cellUpdateRowLabel.Size = new System.Drawing.Size(40, 23);
            this.cellUpdateRowLabel.TabIndex = 1;
            this.cellUpdateRowLabel.Text = "Row";
            this.cellUpdateRowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listDeleteEntryTextBox
            // 
            this.listDeleteEntryTextBox.Location = new System.Drawing.Point(56, 8);
            this.listDeleteEntryTextBox.Name = "listDeleteEntryTextBox";
            this.listDeleteEntryTextBox.TabIndex = 1;
            this.listDeleteEntryTextBox.Text = "";
            // 
            // listAddExampleLabel
            // 
            this.listAddExampleLabel.Location = new System.Drawing.Point(8, 8);
            this.listAddExampleLabel.Name = "listAddExampleLabel";
            this.listAddExampleLabel.Size = new System.Drawing.Size(760, 23);
            this.listAddExampleLabel.TabIndex = 1;
            this.listAddExampleLabel.Text = "Example:";
            // 
            // listQuerySubmitButton
            // 
            this.listQuerySubmitButton.Location = new System.Drawing.Point(688, 104);
            this.listQuerySubmitButton.Name = "listQuerySubmitButton";
            this.listQuerySubmitButton.TabIndex = 5;
            this.listQuerySubmitButton.Text = "Submit";
            this.listQuerySubmitButton.Click += new System.EventHandler(this.OnClickSubmitButton);
            // 
            // spreadsheetListTabPage
            // 
            this.spreadsheetListTabPage.Controls.Add(this.spreadsheetListView);
            this.spreadsheetListTabPage.Location = new System.Drawing.Point(4, 25);
            this.spreadsheetListTabPage.Name = "spreadsheetListTabPage";
            this.spreadsheetListTabPage.Size = new System.Drawing.Size(792, 521);
            this.spreadsheetListTabPage.TabIndex = 1;
            this.spreadsheetListTabPage.Text = "Select Spreadsheet";
            // 
            // listListView
            // 
            this.listListView.GridLines = true;
            this.listListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listListView.Location = new System.Drawing.Point(8, 8);
            this.listListView.MultiSelect = false;
            this.listListView.Name = "listListView";
            this.listListView.Size = new System.Drawing.Size(776, 344);
            this.listListView.TabIndex = 0;
            this.listListView.View = System.Windows.Forms.View.Details;
            // 
            // listEntryLabel
            // 
            this.listEntryLabel.Location = new System.Drawing.Point(8, 8);
            this.listEntryLabel.Name = "listEntryLabel";
            this.listEntryLabel.Size = new System.Drawing.Size(40, 23);
            this.listEntryLabel.TabIndex = 3;
            this.listEntryLabel.Text = "Entry:";
            this.listEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cellUpdateDataLabel
            // 
            this.cellUpdateDataLabel.Location = new System.Drawing.Point(8, 40);
            this.cellUpdateDataLabel.Name = "cellUpdateDataLabel";
            this.cellUpdateDataLabel.Size = new System.Drawing.Size(40, 23);
            this.cellUpdateDataLabel.TabIndex = 5;
            this.cellUpdateDataLabel.Text = "Data";
            this.cellUpdateDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(96, 64);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(168, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.Text = "";
            // 
            // listEntryComboBox
            // 
            this.listEntryComboBox.Location = new System.Drawing.Point(48, 8);
            this.listEntryComboBox.Name = "listEntryComboBox";
            this.listEntryComboBox.Size = new System.Drawing.Size(72, 21);
            this.listEntryComboBox.TabIndex = 4;
            this.listEntryComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSelectedUpdateListEntryChanged);
            // 
            // listDeleteSubmitButton
            // 
            this.listDeleteSubmitButton.Location = new System.Drawing.Point(688, 104);
            this.listDeleteSubmitButton.Name = "listDeleteSubmitButton";
            this.listDeleteSubmitButton.TabIndex = 2;
            this.listDeleteSubmitButton.Text = "Submit";
            this.listDeleteSubmitButton.Click += new System.EventHandler(this.OnClickSubmitButton);
            // 
            // listQueryReverseCheckBox
            // 
            this.listQueryReverseCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.listQueryReverseCheckBox.Location = new System.Drawing.Point(368, 8);
            this.listQueryReverseCheckBox.Name = "listQueryReverseCheckBox";
            this.listQueryReverseCheckBox.Size = new System.Drawing.Size(72, 24);
            this.listQueryReverseCheckBox.TabIndex = 2;
            this.listQueryReverseCheckBox.Text = "reverse?";
            // 
            // loginTabPage
            // 
            this.loginTabPage.Controls.Add(this.passwordTextBox);
            this.loginTabPage.Controls.Add(this.passwordLabel);
            this.loginTabPage.Controls.Add(this.usernameTextBox);
            this.loginTabPage.Controls.Add(this.usernameLabel);
            this.loginTabPage.Location = new System.Drawing.Point(4, 25);
            this.loginTabPage.Name = "loginTabPage";
            this.loginTabPage.Size = new System.Drawing.Size(792, 521);
            this.loginTabPage.TabIndex = 0;
            this.loginTabPage.Text = "Login";
            // 
            // listDeleteTabPage
            // 
            this.listDeleteTabPage.Controls.Add(this.listDeleteSubmitButton);
            this.listDeleteTabPage.Controls.Add(this.listDeleteEntryTextBox);
            this.listDeleteTabPage.Controls.Add(this.listDeleteEntryLabel);
            this.listDeleteTabPage.Location = new System.Drawing.Point(4, 22);
            this.listDeleteTabPage.Name = "listDeleteTabPage";
            this.listDeleteTabPage.Size = new System.Drawing.Size(776, 134);
            this.listDeleteTabPage.TabIndex = 3;
            this.listDeleteTabPage.Text = "Delete";
            // 
            // listActionTabControl
            // 
            this.listActionTabControl.Controls.Add(this.listQueryTabPage);
            this.listActionTabControl.Controls.Add(this.listUpdateTabPage);
            this.listActionTabControl.Controls.Add(this.listInsertTabPage);
            this.listActionTabControl.Controls.Add(this.listDeleteTabPage);
            this.listActionTabControl.Location = new System.Drawing.Point(8, 360);
            this.listActionTabControl.Name = "listActionTabControl";
            this.listActionTabControl.SelectedIndex = 0;
            this.listActionTabControl.Size = new System.Drawing.Size(784, 160);
            this.listActionTabControl.TabIndex = 1;
            // 
            // listInsertTabPage
            // 
            this.listInsertTabPage.Controls.Add(this.listAddTextBox);
            this.listInsertTabPage.Controls.Add(this.listAddExampleLabel);
            this.listInsertTabPage.Controls.Add(this.listAddSubmitButton);
            this.listInsertTabPage.Location = new System.Drawing.Point(4, 22);
            this.listInsertTabPage.Name = "listInsertTabPage";
            this.listInsertTabPage.Size = new System.Drawing.Size(776, 134);
            this.listInsertTabPage.TabIndex = 1;
            this.listInsertTabPage.Text = "Add";
            // 
            // cellUpdateSubmitButton
            // 
            this.cellUpdateSubmitButton.Location = new System.Drawing.Point(688, 104);
            this.cellUpdateSubmitButton.Name = "cellUpdateSubmitButton";
            this.cellUpdateSubmitButton.TabIndex = 0;
            this.cellUpdateSubmitButton.Text = "Submit";
            this.cellUpdateSubmitButton.Click += new System.EventHandler(this.OnClickSubmitButton);
            // 
            // worksheetListView
            // 
            this.worksheetListView.Location = new System.Drawing.Point(8, 8);
            this.worksheetListView.MultiSelect = false;
            this.worksheetListView.Name = "worksheetListView";
            this.worksheetListView.Size = new System.Drawing.Size(776, 504);
            this.worksheetListView.TabIndex = 0;
            this.worksheetListView.View = System.Windows.Forms.View.List;
            // 
            // listDeleteEntryLabel
            // 
            this.listDeleteEntryLabel.Location = new System.Drawing.Point(8, 8);
            this.listDeleteEntryLabel.Name = "listDeleteEntryLabel";
            this.listDeleteEntryLabel.Size = new System.Drawing.Size(48, 23);
            this.listDeleteEntryLabel.TabIndex = 0;
            this.listDeleteEntryLabel.Text = "Entry #";
            this.listDeleteEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cellUpdateColumnTextBox
            // 
            this.cellUpdateColumnTextBox.Location = new System.Drawing.Point(152, 8);
            this.cellUpdateColumnTextBox.Name = "cellUpdateColumnTextBox";
            this.cellUpdateColumnTextBox.Size = new System.Drawing.Size(56, 20);
            this.cellUpdateColumnTextBox.TabIndex = 4;
            this.cellUpdateColumnTextBox.Text = "";
            // 
            // cellsActionTabContol
            // 
            this.cellsActionTabContol.Controls.Add(this.updateCellsTabPage);
            this.cellsActionTabContol.Location = new System.Drawing.Point(8, 360);
            this.cellsActionTabContol.Multiline = true;
            this.cellsActionTabContol.Name = "cellsActionTabContol";
            this.cellsActionTabContol.SelectedIndex = 0;
            this.cellsActionTabContol.Size = new System.Drawing.Size(784, 160);
            this.cellsActionTabContol.TabIndex = 3;
            // 
            // updateCellsTabPage
            // 
            this.updateCellsTabPage.Controls.Add(this.cellUpdateDataTextBox);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateDataLabel);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateColumnTextBox);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateColumnLabel);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateRowTextBox);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateRowLabel);
            this.updateCellsTabPage.Controls.Add(this.cellUpdateSubmitButton);
            this.updateCellsTabPage.Location = new System.Drawing.Point(4, 22);
            this.updateCellsTabPage.Name = "updateCellsTabPage";
            this.updateCellsTabPage.Size = new System.Drawing.Size(776, 134);
            this.updateCellsTabPage.TabIndex = 0;
            this.updateCellsTabPage.Text = "Update";
            // 
            // listQueryQueryLabel
            // 
            this.listQueryQueryLabel.Location = new System.Drawing.Point(8, 32);
            this.listQueryQueryLabel.Name = "listQueryQueryLabel";
            this.listQueryQueryLabel.TabIndex = 3;
            this.listQueryQueryLabel.Text = "sq=";
            this.listQueryQueryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listUpdateTextBox
            // 
            this.listUpdateTextBox.Location = new System.Drawing.Point(128, 8);
            this.listUpdateTextBox.Name = "listUpdateTextBox";
            this.listUpdateTextBox.Size = new System.Drawing.Size(640, 20);
            this.listUpdateTextBox.TabIndex = 1;
            this.listUpdateTextBox.Text = "";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(24, 32);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(72, 23);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // listOrderByTextBox
            // 
            this.listOrderByTextBox.Location = new System.Drawing.Point(112, 8);
            this.listOrderByTextBox.Name = "listOrderByTextBox";
            this.listOrderByTextBox.Size = new System.Drawing.Size(248, 20);
            this.listOrderByTextBox.TabIndex = 1;
            this.listOrderByTextBox.Text = "";
            // 
            // cellsListView
            // 
            this.cellsListView.AutoArrange = false;
            this.cellsListView.GridLines = true;
            this.cellsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.cellsListView.Location = new System.Drawing.Point(8, 8);
            this.cellsListView.MultiSelect = false;
            this.cellsListView.Name = "cellsListView";
            this.cellsListView.Size = new System.Drawing.Size(776, 344);
            this.cellsListView.TabIndex = 2;
            this.cellsListView.View = System.Windows.Forms.View.Details;
            // 
            // cellsFeedTabPage
            // 
            this.cellsFeedTabPage.Controls.Add(this.cellsListView);
            this.cellsFeedTabPage.Controls.Add(this.cellsActionTabContol);
            this.cellsFeedTabPage.Location = new System.Drawing.Point(4, 25);
            this.cellsFeedTabPage.Name = "cellsFeedTabPage";
            this.cellsFeedTabPage.Size = new System.Drawing.Size(792, 521);
            this.cellsFeedTabPage.TabIndex = 4;
            this.cellsFeedTabPage.Text = "Cells Feed";
            // 
            // listAddSubmitButton
            // 
            this.listAddSubmitButton.Location = new System.Drawing.Point(688, 104);
            this.listAddSubmitButton.Name = "listAddSubmitButton";
            this.listAddSubmitButton.TabIndex = 0;
            this.listAddSubmitButton.Text = "Submit";
            this.listAddSubmitButton.Click += new System.EventHandler(this.OnClickSubmitButton);
            // 
            // listAddTextBox
            // 
            this.listAddTextBox.Location = new System.Drawing.Point(8, 32);
            this.listAddTextBox.Name = "listAddTextBox";
            this.listAddTextBox.Size = new System.Drawing.Size(760, 20);
            this.listAddTextBox.TabIndex = 2;
            this.listAddTextBox.Text = "";
            // 
            // SpreadsheetsSampleApp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.tabControl);
            this.Name = "SpreadsheetsSampleApp";
            this.Text = "Spreadsheets Sample App";
            this.tabControl.ResumeLayout(false);
            this.listFeedTabPage.ResumeLayout(false);
            this.listQueryTabPage.ResumeLayout(false);
            this.worksheetListTabPage.ResumeLayout(false);
            this.listUpdateTabPage.ResumeLayout(false);
            this.spreadsheetListTabPage.ResumeLayout(false);
            this.loginTabPage.ResumeLayout(false);
            this.listDeleteTabPage.ResumeLayout(false);
            this.listActionTabControl.ResumeLayout(false);
            this.listInsertTabPage.ResumeLayout(false);
            this.cellsActionTabContol.ResumeLayout(false);
            this.updateCellsTabPage.ResumeLayout(false);
            this.cellsFeedTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
        }
#endregion

        /// <summary>
        /// Handler for when the Spreadsheets tab becomes the active tab 
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        void OnEnterSelectSpreadsheetTab(object sender, System.EventArgs e)
        {
            service.setUserCredentials(this.usernameTextBox.Text, this.passwordTextBox.Text);

            SpreadsheetQuery query = new SpreadsheetQuery();
            setSpreadsheetListView(service.Query(query));
        }

        /// <summary>
        /// Sets the list view on the Spreadsheets page with the titles of the
        /// spreadsheets in the feed
        /// </summary>
        /// <param name="feed">The spreadsheets feed</param>
        void setSpreadsheetListView(SpreadsheetFeed feed) 
        {
            // Clear out the old list
            this.spreadsheetListView.Items.Clear();

            AtomEntryCollection entries = feed.Entries;
            for (int i = 0; i < entries.Count; i++)
            {
                // Get the worksheets feed URI
                AtomLink worksheetsLink = entries[i].Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, 
                                                                       AtomLink.ATOM_TYPE);

                // Create an item that will display the title and hide the worksheets feed URI
                ListViewItem item = new ListViewItem(new string[2]{entries[i].Title.Text, worksheetsLink.HRef.Content});

                this.spreadsheetListView.Items.Add(item);
            }
        }

        /// <summary>
        /// Handler for when the Worksheets page becomes active.  Depends on 
        /// the spreadsheet being selected on the Spreadsheets page.
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        void OnEnterSelectWorksheetTab(object sender, System.EventArgs e)
        {
			if (spreadsheetListView.SelectedItems.Count > 0)
			{
				// Get the worksheet feed from the selected entry
				WorksheetQuery query = new WorksheetQuery(spreadsheetListView.SelectedItems[0].SubItems[1].Text);
				SetWorksheetListView(service.Query(query));
			}
        }

        /// <summary>
        /// Sets the list view on the Worksheets page with the titles of the worksheets,
        /// and the row and column counts for demonstration of these extension elements
        /// </summary>
        /// <param name="feed">The worksheets feed for the spreadsheet selected on the Spreadsheets page</param>
        void SetWorksheetListView(WorksheetFeed feed) 
        {
            this.worksheetListView.Items.Clear();

            AtomEntryCollection entries = feed.Entries;
            for (int i = 0; i < entries.Count; i++)
            {
                // Get the cells feed URI
                AtomLink cellsLink = entries[i].Links.FindService(GDataSpreadsheetsNameTable.CellRel, AtomLink.ATOM_TYPE);

                // Get the list feed URI
                AtomLink listLink = entries[i].Links.FindService(GDataSpreadsheetsNameTable.ListRel, AtomLink.ATOM_TYPE);

                // Create an item that will display the title and hide the cells and list feed URIs
                ListViewItem item = new ListViewItem(new string[3]{ entries[i].Title.Text + " (Rows:"
                                                     + ((WorksheetEntry)entries[i]).RowCount.Count
                                                     + ", Cols:" + ((WorksheetEntry)entries[i]).ColCount.Count + ")",
                                                     cellsLink.HRef.Content, listLink.HRef.Content});

                this.worksheetListView.Items.Add(item);
            }
        }

        /// <summary>
        /// Handler for whent eh Cells page becomes active
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        void OnEnterCellsFeedTab(object sender, System.EventArgs e) 
        {
			if (worksheetListView.SelectedItems.Count > 0) 
			{
				CellQuery query = new CellQuery(worksheetListView.SelectedItems[0].SubItems[1].Text);
				SetCellListView(service.Query(query));
			}
        }

        /// <summary>
        /// Sets the list view on the Cells tab
        /// </summary>
        /// <param name="feed">The feed providing the data</param>
        void SetCellListView(CellFeed feed) 
        {
            // Clear out all the old information
            this.cellsListView.Items.Clear();
            this.cellsListView.Columns.Clear();
            this.editUriTable.Clear();

            AtomEntryCollection entries = feed.Entries;

            // Add in the column headers, as many as the column count asks
            // The number of rows, we only care to go as far as the data goes
            this.cellsListView.Columns.Add("", 20, HorizontalAlignment.Left);
            for (int i=1; i < feed.ColCount.Count; i++)
            {
                this.cellsListView.Columns.Add(i.ToString(), 80, HorizontalAlignment.Center);
            }

            int currentRow = 1;
            int currentCol = 1;

            ListViewItem item = new ListViewItem();
            item.Text = 1.ToString();

            for (int i=0; i < entries.Count; i++)
            {
                CellEntry entry = entries[i] as CellEntry;
                if (entry != null)
                {

                    // Add the current row, since we are starting 
                    // a new row in data from the feed
                    while (entry.Cell.Row > currentRow)
                    {
                        this.cellsListView.Items.Add(item);
                        item = new ListViewItem();
                        item.Text = (currentRow + 1).ToString();
                        currentRow++;
                        currentCol = 1;
                    }

                    // Add blank entries where there is no data for the column
                    while (entry.Cell.Column > currentCol)
                    {
                        item.SubItems.Add("");
                        currentCol++;
                    }

                    // Add the current data entry
                    item.SubItems.Add(entry.Cell.Value);
                    this.editUriTable.Add("R" + currentRow + "C" + currentCol,
                                          entry);
                    currentCol++;
                }
            }
        }

        /// <summary>
        /// On entering the List tab page we need to load a few things
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        void OnEnterListFeedTab(object sender, System.EventArgs e) 
        {
			if (worksheetListView.SelectedItems.Count > 0) 
			{
				ListQuery query = new ListQuery(worksheetListView.SelectedItems[0].SubItems[2].Text);

				SetListListView(service.Query(query));

				SetListEntryComboBox();

				this.listAddExampleLabel.Text = "Example: " + ToCommaSeparatedString((ListEntry)this.editUriTable["1"]);
			}
        }

        /// <summary>
        /// Sets the list view on the List tab
        /// </summary>
        /// <param name="feed">The feed providing the data</param>
        void SetListListView(ListFeed feed) 
        {
            this.listListView.Items.Clear();
            this.listListView.Columns.Clear();
            this.editUriTable.Clear();

            this.listListView.Columns.Add("", 80, HorizontalAlignment.Center);

            AtomEntryCollection entries = feed.Entries;

            for (int i=0; i < entries.Count; i++)
            {
                ListEntry entry = entries[i] as ListEntry;

                ListViewItem item = new ListViewItem();
                item.Text = (i+1).ToString();

                if (entry != null)
                {
                    ListEntry.CustomElementCollection elements = entry.Elements;
                    for (int j=0; j < elements.Count; j++)
                    {
                        item.SubItems.Add(elements[j].Value);

                        if (listListView.Columns.Count - 2 < j)
                        {
                            listListView.Columns.Add(elements[j].LocalName, 80, HorizontalAlignment.Center);
                        }
                    }


                    listListView.Items.Add(item);
                    this.editUriTable.Add(item.Text, entry);
                }
            }
        }

        /// <summary>
        /// Sets the entry numbers in the combo box for selecting
        /// </summary>
        void SetListEntryComboBox()
        {
            this.listEntryComboBox.Items.Clear();

            for (int i=0; i < this.listListView.Items.Count; i++)
            {
                this.listEntryComboBox.Items.Add("" + (i+1));
            }
        }

        /// <summary>
        /// The selected index for the tab control has changed.
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        void OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.tabControl.SelectedTab.Equals(this.spreadsheetListTabPage))
            {
                OnEnterSelectSpreadsheetTab(sender, e);
            }
            else if (this.tabControl.SelectedTab.Equals(this.worksheetListTabPage))
            {
                OnEnterSelectWorksheetTab(sender, e);
            }
            else if (this.tabControl.SelectedTab.Equals(this.cellsFeedTabPage))
            {
                OnEnterCellsFeedTab(sender, e);
            }
            else if (this.tabControl.SelectedTab.Equals(this.listFeedTabPage))
            {
                OnEnterListFeedTab(sender, e);
            }
        }

        /// <summary>
        /// Handler for click events to the various Submit buttons.
        /// </summary>
        /// <param name="sender">A submit button.</param>
        /// <param name="e">Ignored</param>
        void OnClickSubmitButton(object sender, System.EventArgs e)
        {
            if (this.listQuerySubmitButton.Equals(sender))
            {
                DoListQuery();
            }
            else if (this.listAddSubmitButton.Equals(sender))
            {
                DoListInsert();
            }
            else if (this.listUpdateSubmitButton.Equals(sender))
            {
                DoListUpdate();
            }
            else if (this.listDeleteSubmitButton.Equals(sender))
            {
                DoListDelete();
            }
            else if (this.cellUpdateSubmitButton.Equals(sender))
            {
                string index = "R" + this.cellUpdateRowTextBox.Text 
                               + "C" + this.cellUpdateColumnTextBox.Text;

                if (this.editUriTable.ContainsKey(index))
                {
                    if (this.cellUpdateDataTextBox.Text == null
                        || this.cellUpdateDataTextBox.Text.Length <= 0)
                    {
                        DoCellDelete(index);
                    }
                    else
                    {
                        DoCellUpdate(index);
                    }
                }
                else
                {
                    if (this.cellUpdateDataTextBox.Text != null
                        && this.cellUpdateDataTextBox.Text.Length > 0)
                    {
                        DoCellInsert();
                    }
                }
            }

            if (this.tabControl.SelectedTab.Equals(this.listFeedTabPage)
                && !this.listQuerySubmitButton.Equals(sender))
            {
                // Refresh
                this.OnEnterListFeedTab(sender, e);
            }
            else if (this.tabControl.SelectedTab.Equals(this.cellsFeedTabPage))
            {
                this.OnEnterCellsFeedTab(sender, e);
            }

        }

        /// <summary>
        /// Converts the gsx: tags to a comma seperated list for parsing
        /// Warning: Not robust for handling formulas
        /// </summary>
        /// <param name="entry">The list entry</param>
        /// <returns>The gsx: tags as a comma separated list</returns>
        private String ToCommaSeparatedString(ListEntry entry)
        {
            if (entry != null) 
            {
                String commaSeparated = "";
   
                ListEntry.CustomElementCollection elements = entry.Elements;
                for (int i=0; i < elements.Count; i++)
                {
                    commaSeparated += elements[i].LocalName + "=" + elements[i].Value + ",";
                }

                return commaSeparated.Substring(0, commaSeparated.Length - 1);
            }
            return ""; 
        }
#region Exercise Actions
        private void DoListQuery()
        {
            // Set up the query
            ListQuery query = new ListQuery(this.worksheetListView.SelectedItems[0].SubItems[2].Text);
            query.OrderByColumn = this.listOrderByTextBox.Text;
            query.Reverse = this.listQueryReverseCheckBox.Checked;
            query.SpreadsheetQuery = this.listQueryQueryTextBox.Text;

            // Set the view with the new feed
            this.SetListListView(service.Query(query));
        }

        private void DoListInsert()
        {
            String[] commaSplit = this.listAddTextBox.Text.Split(',');
            ListEntry entry = new ListEntry();

            for (int i=0; i < commaSplit.Length; i++)
            {
                String[] pair = commaSplit[i].Split('=');
                ListEntry.Custom custom = new ListEntry.Custom();
                custom.LocalName = pair[0];
                custom.Value = pair[1];
                entry.Elements.Add(custom);
            }

            AtomEntry retEntry = service.Insert(new Uri(this.worksheetListView.SelectedItems[0].SubItems[2].Text), entry);
        }

        private void DoListUpdate()
        {
            String[] commaSplit = this.listUpdateTextBox.Text.Split(',');

            ListEntry entry 
            = (ListEntry)editUriTable[this.listEntryComboBox.Text];

            for (int i=0; i < commaSplit.Length; i++)
            {
                String[] pair = commaSplit[i].Split('=');

                // Make the new replacement custom
                ListEntry.Custom custom = new ListEntry.Custom();
                custom.LocalName = pair[0];

                if (pair.Length == 2)
                {
                    custom.Value = pair[1];
                }
                else
                {
                    custom.Value = null;
                    entry.Elements.Remove(custom);
                    continue;
                }

                // Set the replacement custom
                int index = entry.Elements.IndexOf(custom);
                entry.Elements[index] = custom;
            }

            AtomEntry retEntry = service.Update(entry);
        }

        private void DoListDelete()
        {
            service.Delete((AtomEntry)(this.editUriTable[this.listDeleteEntryTextBox.Text]));
        }

        private void DoCellInsert()
        {
            // If there is data to insert then do so
            CellEntry entry = new CellEntry();
            CellEntry.CellElement cell = new CellEntry.CellElement();
            cell.InputValue = this.cellUpdateDataTextBox.Text;
            cell.Row = UInt32.Parse(this.cellUpdateRowTextBox.Text);
            cell.Column = UInt32.Parse(this.cellUpdateColumnTextBox.Text);
            entry.Cell = cell;

            service.Insert(new Uri(this.worksheetListView.SelectedItems[0].SubItems[1].Text),
                           entry);
        }

        private void DoCellUpdate(string index)
        {
            CellEntry entry = (CellEntry)this.editUriTable[index];
            entry.Cell.Value = null;
            entry.Cell.NumericValue = null;
            entry.Cell.InputValue = this.cellUpdateDataTextBox.Text;

            service.Update(entry);
        }

        private void DoCellDelete(string index)
        {
            CellEntry entry = (CellEntry)this.editUriTable[index];

            // If the data is empty, then delete the cell
            service.Delete(entry);
        }
#endregion

        /// <summary>
        /// When the selection in the combo box on the List Update section
        /// changes, then the contents of the text box will change for easier input
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void OnSelectedUpdateListEntryChanged(object sender, System.EventArgs e)
        {
            this.listUpdateTextBox.Text 
            = ToCommaSeparatedString((ListEntry)editUriTable["" + (this.listEntryComboBox.SelectedIndex+1)]);
        }

    }
}
