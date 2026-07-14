using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HomeStayDorm.UI.Common
{
    public static class UiHelper
    {
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Success = Color.FromArgb(22, 101, 52);
        public static readonly Color Text = Color.FromArgb(30, 41, 59);
        public static readonly Color Muted = Color.FromArgb(71, 85, 105);
        public static readonly Color Surface = Color.FromArgb(246, 248, 251);

        public static Button Button(string text, Color? backColor = null)
        {
            Button button = new Button
            {
                Text = text,
                AutoSize = true,
                BackColor = backColor ?? Color.White,
                ForeColor = backColor.HasValue ? Color.White : Text,
                FlatStyle = FlatStyle.Flat,
                Padding = new Padding(12, 4, 12, 4),
                Margin = new Padding(6),
                MinimumSize = new Size(96, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                UseCompatibleTextRendering = true
            };
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.FlatAppearance.BorderSize = backColor.HasValue ? 0 : 1;
            return button;
        }

        public static DataGridView Grid()
        {
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                MultiSelect = false,
                EnableHeadersVisualStyles = false
            };
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            return grid;
        }

        public static TableLayoutPanel FieldsPanel(int rows, int labelWidth = 145)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = rows,
                Padding = new Padding(8)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, labelWidth));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < rows; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            }

            return panel;
        }

        public static TableLayoutPanel Footer(Label statusLabel, Control actions)
        {
            TableLayoutPanel footer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 6, 0, 0)
            };
            footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            footer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            footer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            statusLabel.Dock = DockStyle.Fill;
            statusLabel.AutoSize = false;
            statusLabel.MinimumSize = new Size(0, 42);
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            actions.Dock = DockStyle.Right;
            actions.AutoSize = true;

            footer.Controls.Add(statusLabel, 0, 0);
            footer.Controls.Add(actions, 1, 0);
            return footer;
        }

        public static Panel ScrollHost(TableLayoutPanel content)
        {
            Panel host = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Surface
            };

            content.Dock = DockStyle.Top;
            content.AutoSize = true;
            content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            content.AutoScroll = false;
            host.Controls.Add(content);
            return host;
        }

        public static void UseNaturalRows(TableLayoutPanel panel)
        {
            panel.RowStyles.Clear();
            for (int i = 0; i < panel.RowCount; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        public static void ReserveHeight(Control control, int height)
        {
            control.MinimumSize = new Size(0, height);
            control.Height = height;
        }

        public static TableLayoutPanel InlineAction(Control input, Button button, int buttonWidth = 120)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = Padding.Empty
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, buttonWidth));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            input.Dock = DockStyle.Fill;
            button.Dock = DockStyle.Fill;
            button.Margin = new Padding(6, 0, 0, 0);
            panel.Controls.Add(input, 0, 0);
            panel.Controls.Add(button, 1, 0);
            return panel;
        }

        public static void AddField(TableLayoutPanel panel, string labelText, Control control, int row)
        {
            Label label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Muted
            };
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(4, 5, 0, 5);
            panel.Controls.Add(label, 0, row);
            panel.Controls.Add(control, 1, row);
        }

        public static void ConfigureCombo(ComboBox combo, params object[] items)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Items.Clear();
            combo.Items.AddRange(items);
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        public static void ConfigureComboFromData(ComboBox combo, DataTable data, string columnName, params object[] fallbackItems)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.DataSource = null;
            combo.Items.Clear();

            HashSet<string> added = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            void AddItem(object? value)
            {
                string text = Convert.ToString(value)?.Trim() ?? string.Empty;
                if (text.Length == 0 || !added.Add(text))
                {
                    return;
                }

                combo.Items.Add(text);
            }

            if (data.Columns.Contains(columnName))
            {
                foreach (DataRow row in data.Rows)
                {
                    AddItem(row[columnName]);
                }
            }

            foreach (object item in fallbackItems)
            {
                AddItem(item);
            }

            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        public static int? CurrentInt(DataGridView grid, string columnName)
        {
            if (grid.CurrentRow == null || !grid.Columns.Contains(columnName))
            {
                return null;
            }

            object? value = grid.CurrentRow.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? null : System.Convert.ToInt32(value);
        }
    }
}
