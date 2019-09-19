using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine_Sweeper
{
    public partial class MineSweeper : Form
    {
        public MineSweeper()
        {
            InitializeComponent();
        }

        Bitmap bmp;
        Graphics gr;
        Pen pen = new Pen(Color.FromArgb(255, 255, 255, 255), LINE_WIDTH);
        Random rnd;
        Cell[,] cellArray;
        bool check = true;
        List<int> minesIndexList;
        int mineInstalled, mineCount, flagCount = 0, cellPressedCount = 0;
        int mine, numberMinesAroundCell, indexList = 0;
        const int CELL_SIZE = 30, LINE_WIDTH = 1, CELL_SIZE_CORRECTION = 1, AROUND_CELL_NUMBER = 8;
        const int NEWBEE_MINE_COUNT = 10, MEDIUM_MINE_COUNT = 40, EXPERT_MINE_COUNT = 99;
        const int NEWBEE_FORM_WIDTH = 320, NEWBEE_FORM_HEIGHT = 360, MEDIUM_FORM_WIDTH = 530, MEDIUM_FORM_HEIGHT = 570, EXPERT_FORM_WIDTH = 950, EXPERT_FORM_HEIGHT = 570;
        const int NEWBEE_FIELD_SIZE = 271, NEWBEE_ROW_COUNT = 9, MEDIUM_FIELD_SIZE = 481, MEDIUM_ROW_COUNT = 16, EXPERT_FIELD_HEIGHT = 481, EXPERT_FIELD_WIDTH = 901, EXPERT_ROW_COUNT = 16, EXPERT_COLUMN_COUNT = 30;

        public class MyPanel : System.Windows.Forms.Panel
        {
            public MyPanel()
            {
                this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            }
        }

        class Cell
        {
            public int coordX;
            public int coordY;
            public int mine;
            public int numberMinesAroundCell;
            public int flagCountAroundCell; 
            public int numberUndetectedMineAround; 
            public bool cellPressed;
            public bool flag;

            public Cell(int x, int y, int z, int count)
            {
                coordX = x;
                coordY = y;
                mine = z;
                numberMinesAroundCell = count;
                flagCountAroundCell = 0;
                numberUndetectedMineAround = 0;
                cellPressed = false;
                flag = false;
            }

            private void DrawPicture(Graphics gr, Pen pen, Bitmap picture, int cellCoordX, int cellCoordY)
            {
                Bitmap image = picture;
                TextureBrush texture = new TextureBrush(image);
                gr.DrawRectangle(pen, cellCoordX, cellCoordY, CELL_SIZE, CELL_SIZE);
                gr.FillRectangle(texture, cellCoordX + CELL_SIZE_CORRECTION, cellCoordY + CELL_SIZE_CORRECTION, CELL_SIZE - CELL_SIZE_CORRECTION, CELL_SIZE - CELL_SIZE_CORRECTION);
            }

            public void DrawCell(Graphics gr, Pen pen)
            {
                if (cellPressed == false)
                {
                    if(flag == false)
                    {
                        DrawPicture(gr, pen, Properties.Resources.startCell, coordX, coordY);
                    }
                }
                else
                {
                    if (flag == false)
                    {
                        if (mine == 1)
                        {
                            DrawPicture(gr, pen, Properties.Resources.boom, coordX, coordY);
                        }
                        else
                        {
                            switch (numberMinesAroundCell)
                            {
                                case 0:
                                    DrawPicture(gr, pen, Properties.Resources.emptyCell, coordX, coordY);
                                    break;
                                case 1:
                                    DrawPicture(gr, pen, Properties.Resources._1, coordX, coordY);
                                    break;
                                case 2:
                                    DrawPicture(gr, pen, Properties.Resources._2, coordX, coordY);
                                    break;
                                case 3:
                                    DrawPicture(gr, pen, Properties.Resources._3, coordX, coordY);
                                    break;
                                case 4:
                                    DrawPicture(gr, pen, Properties.Resources._4, coordX, coordY);
                                    break;
                                case 5:
                                    DrawPicture(gr, pen, Properties.Resources._5, coordX, coordY);
                                    break;
                                case 6:
                                    DrawPicture(gr, pen, Properties.Resources._6, coordX, coordY);
                                    break;
                                case 7:
                                    DrawPicture(gr, pen, Properties.Resources._7, coordX, coordY);
                                    break;
                                case 8:
                                    DrawPicture(gr, pen, Properties.Resources._8, coordX, coordY);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        DrawPicture(gr, pen, Properties.Resources.flag, coordX, coordY);
                    }
                }
            }
        }

        private void DrawGameField(int formWidth, int formHeight, int panelWidth, int panelHeight, int arrayStringCount, int arrayColumnCount, int count)
        {
            mineLeftLabel.Text = "Мин осталось: " + count;
            flagCount = 0;
            alertTextBox.Visible = false;
            gamefieldPanel.Enabled = true;
            this.Width = formWidth;
            this.Height = formHeight;
            bmp = new Bitmap(panelWidth, panelHeight);
            gamefieldPanel.Size = new Size(panelWidth, panelHeight);
            alertTextBox.Location = new Point((gamefieldPanel.Size.Width / 2 - alertTextBox.Size.Width / 2), (gamefieldPanel.Size.Height / 2 - alertTextBox.Size.Height / 2));
            gamefieldPanel.BackgroundImage = bmp;
            gr = Graphics.FromImage(bmp);
            rnd = new Random();
            cellArray = new Cell[arrayStringCount, arrayColumnCount];
            mineCount = count;
            minesIndexList = new List<int>();
            for (int i = 0; i < mineCount; i++)
            {
                check = true;
                mineInstalled = rnd.Next(0, cellArray.Length);
                if (i >= 1)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (mineInstalled == minesIndexList[j])
                        {
                            check = false;
                            i -= 1;
                            break;
                        }
                    }
                }
                if (check == true)
                {
                    minesIndexList.Add(mineInstalled);
                }
            }
            minesIndexList.Sort();
            indexList = 0;
            for (int i = 0; i < arrayStringCount; i++)
            {
                for (int j = 0; j < arrayColumnCount; j++)
                {
                    numberMinesAroundCell = 0;
                    if ((i * arrayColumnCount + j) == minesIndexList[indexList])
                    {
                        mine = 1;
                        if (indexList != minesIndexList.Count - 1)
                        {
                            indexList++;
                        }
                    }
                    else
                    {
                        mine = 0;
                        if (j < arrayColumnCount - 1)
                        {
                            if (minesIndexList.Contains((i * arrayColumnCount + j + 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i < arrayStringCount && j < arrayColumnCount - 1)
                        {
                            if (minesIndexList.Contains(((i + 1) * arrayColumnCount + j + 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i < arrayStringCount)
                        {
                            if (minesIndexList.Contains(((i + 1) * arrayColumnCount + j)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i < arrayStringCount && j > 0)
                        {
                            if (minesIndexList.Contains(((i + 1) * arrayColumnCount + j - 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (j > 0)
                        {
                            if (minesIndexList.Contains((i * arrayColumnCount + j - 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i > 0 && j > 0)
                        {
                            if (minesIndexList.Contains(((i - 1) * arrayColumnCount + j - 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i > 0)
                        {
                            if (minesIndexList.Contains(((i - 1) * arrayColumnCount + j)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                        if (i > 0 && j < arrayColumnCount - 1)
                        {
                            if (minesIndexList.Contains(((i - 1) * arrayColumnCount + j + 1)) == true)
                            {
                                numberMinesAroundCell++;
                            }
                        }
                    }
                    cellArray[i, j] = new Cell(j * CELL_SIZE, i * CELL_SIZE, mine, numberMinesAroundCell);
                    cellArray[i, j].DrawCell(gr, pen);
                }
            }
        }

        private void MineSweeper_Load(object sender, EventArgs e)
        {
            DrawGameField(NEWBEE_FORM_WIDTH, NEWBEE_FORM_HEIGHT, NEWBEE_FIELD_SIZE, NEWBEE_FIELD_SIZE, NEWBEE_ROW_COUNT, NEWBEE_ROW_COUNT, NEWBEE_MINE_COUNT);
        }

        private void NewbeeMenuItem_Click(object sender, EventArgs e)
        {
            DrawGameField(NEWBEE_FORM_WIDTH, NEWBEE_FORM_HEIGHT, NEWBEE_FIELD_SIZE, NEWBEE_FIELD_SIZE, NEWBEE_ROW_COUNT, NEWBEE_ROW_COUNT, NEWBEE_MINE_COUNT);
        }

        private void MediumMenuItem_Click(object sender, EventArgs e)
        {
            DrawGameField(MEDIUM_FORM_WIDTH, MEDIUM_FORM_HEIGHT, MEDIUM_FIELD_SIZE, MEDIUM_FIELD_SIZE, MEDIUM_ROW_COUNT, MEDIUM_ROW_COUNT, MEDIUM_MINE_COUNT);
        }

        private void ExpertMenuItem_Click(object sender, EventArgs e)
        {
            DrawGameField(EXPERT_FORM_WIDTH, EXPERT_FORM_HEIGHT, EXPERT_FIELD_WIDTH, EXPERT_FIELD_HEIGHT, EXPERT_ROW_COUNT, EXPERT_COLUMN_COUNT, EXPERT_MINE_COUNT);
        }

        private void DrawFieldLines(int panelWidth, int panelHeight, int arrayStringCount, int arrayColumnCount)
        {
            if (gamefieldPanel.Size == new Size(panelWidth, panelHeight))
            {
                bmp = new Bitmap(panelWidth, panelHeight);
                gamefieldPanel.BackgroundImage = bmp;
                gr = Graphics.FromImage(bmp);
                for (int i = 0; i < arrayStringCount; i++)
                {
                    for (int j = 0; j < arrayColumnCount; j++)
                    {
                        cellArray[i, j].DrawCell(gr, pen);
                    }
                }
            }
        }

        private void DrawOpenCells(Cell[,] array, int i, int j, int arrayStringCount, int arrayColumnCount)
        {
            array[i, j].DrawCell(gr, pen);
            if (array[i, j].numberMinesAroundCell == 0)
            {
                for (int k = 0; k < AROUND_CELL_NUMBER; k++)
                {
                    switch (k)
                    {
                        case 0:
                            if (j < arrayColumnCount - 1 && array[i, j + 1].cellPressed == false)
                            {
                                array[i, j + 1].cellPressed = true;
                                array[i, j + 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i, j + 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 1:
                            if (i < arrayStringCount - 1 && j < arrayColumnCount - 1 && array[i + 1, j + 1].cellPressed == false)
                            {
                                array[i + 1, j + 1].cellPressed = true;
                                array[i + 1, j + 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i + 1, j + 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 2:
                            if (i < arrayStringCount - 1 && array[i + 1, j].cellPressed == false)
                            {
                                array[i + 1, j].cellPressed = true;
                                array[i + 1, j].DrawCell(gr, pen);
                                DrawOpenCells(array, i + 1, j, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 3:
                            if (i < arrayStringCount - 1 && j > 0 && array[i + 1, j - 1].cellPressed == false)
                            {
                                array[i + 1, j - 1].cellPressed = true;
                                array[i + 1, j - 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i + 1, j - 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 4:
                            if (j > 0 && array[i, j - 1].cellPressed == false)
                            {
                                array[i, j - 1].cellPressed = true;
                                array[i, j - 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i, j - 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 5:
                            if (i > 0 && j > 0 && array[i - 1, j - 1].cellPressed == false)
                            {
                                array[i - 1, j - 1].cellPressed = true;
                                array[i - 1, j - 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i - 1, j - 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 6:
                            if (i > 0 && array[i - 1, j].cellPressed == false)
                            {
                                array[i - 1, j].cellPressed = true;
                                array[i - 1, j].DrawCell(gr, pen);
                                DrawOpenCells(array, i - 1, j, arrayStringCount, arrayColumnCount);
                            }
                            break;
                        case 7:
                            if (i > 0 && j < arrayColumnCount - 1 && array[i - 1, j + 1].cellPressed == false)
                            {
                                array[i - 1, j + 1].cellPressed = true;
                                array[i - 1, j + 1].DrawCell(gr, pen);
                                DrawOpenCells(array, i - 1, j + 1, arrayStringCount, arrayColumnCount);
                            }
                            break;
                    }
                }
            }
        }

        private void CheckGameWon(int arrayStringCount, int arrayColumnCount, int count)
        {
            cellPressedCount = 0;
            for (int i = 0; i < arrayStringCount; i++)
            {
                for (int j = 0; j < arrayColumnCount; j++)
                {
                    if (cellArray[i, j].cellPressed == true)
                    {
                        cellPressedCount++;
                    }
                }
            }
            if (cellPressedCount == cellArray.Length && flagCount == count)
            {
                alertTextBox.Text = "Вы победили";
                alertTextBox.Visible = true;
                gamefieldPanel.Enabled = false;
            }
            else
            {
                alertTextBox.Text = "Игра окончена";
                alertTextBox.Visible = false;
            }
        }

        private void DrawGame(int panelWidth, int panelHeight, int arrayStringCount, int arrayColumnCount, MouseEventArgs e, int count)
        {
            if (gamefieldPanel.Size == new Size(panelWidth, panelHeight))
            {
                if (e.Button == MouseButtons.Left && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed == false)
                {
                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed = true;
                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].DrawCell(gr, pen);
                    if (cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].mine == 1)
                    {
                        alertTextBox.Visible = true;
                        gamefieldPanel.Enabled = false;
                    }
                    else
                    {
                        CheckGameWon(arrayStringCount, arrayColumnCount, count);
                    }
                    DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE, arrayStringCount, arrayColumnCount);
                }
                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed == false && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == false)
                        {
                            cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed = true;
                            cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag = true;
                            cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].DrawCell(gr, pen);
                            flagCount++;
                            if ((e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell += 1;
                            }
                            if ((e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flagCountAroundCell += 1;
                            }
                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell += 1;
                            }
                            CheckGameWon(arrayStringCount, arrayColumnCount, count);
                        }
                        else
                        {
                            if (cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed == true && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == true)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed = false;
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag = false;
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].DrawCell(gr, pen);
                                flagCount--;
                                if ((e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell -= 1;
                                }
                                if ((e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flagCountAroundCell -= 1;
                                }
                                if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1)
                                {
                                    cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flagCountAroundCell -= 1;
                                }
                            }
                        }
                        mineLeftLabel.Text = "Мин осталось: " + (minesIndexList.Count - flagCount).ToString();
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Middle)
                        {
                            if (cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed == true && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flagCountAroundCell == cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberMinesAroundCell)
                            {
                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround = 0;
                                for (int k = 0; k < AROUND_CELL_NUMBER; k++)
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            if ((e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 1:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 2:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 3:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 4:
                                            if ((e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 5:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 6:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                        case 7:
                                            if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                            {
                                                cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround += 1;
                                            }
                                            break;
                                    }
                                }
                                if (cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE].numberUndetectedMineAround == 0)
                                {
                                    if ((e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE < arrayStringCount - 1 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE + 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE - 1, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE, arrayStringCount, arrayColumnCount);
                                    }
                                    if ((e.Y - e.Y % CELL_SIZE) / CELL_SIZE > 0 && (e.X - e.X % CELL_SIZE) / CELL_SIZE < arrayColumnCount - 1 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].mine == 0 && cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].flag == false)
                                    {
                                        cellArray[(e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1].cellPressed = true;
                                        DrawOpenCells(cellArray, (e.Y - e.Y % CELL_SIZE) / CELL_SIZE - 1, (e.X - e.X % CELL_SIZE) / CELL_SIZE + 1, arrayStringCount, arrayColumnCount);
                                    }
                                }
                            }
                            CheckGameWon(arrayStringCount, arrayColumnCount, count);
                        }
                    }
                }
            }
        }

        private void GamefieldPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (gamefieldPanel.Size == new Size(NEWBEE_FIELD_SIZE, NEWBEE_FIELD_SIZE))
            {
                DrawFieldLines(NEWBEE_FIELD_SIZE, NEWBEE_FIELD_SIZE, NEWBEE_ROW_COUNT, NEWBEE_ROW_COUNT);
                DrawGame(NEWBEE_FIELD_SIZE, NEWBEE_FIELD_SIZE, NEWBEE_ROW_COUNT, NEWBEE_ROW_COUNT, e, NEWBEE_MINE_COUNT);
            }
            if (gamefieldPanel.Size == new Size(MEDIUM_FIELD_SIZE, MEDIUM_FIELD_SIZE))
            {
                DrawFieldLines(MEDIUM_FIELD_SIZE, MEDIUM_FIELD_SIZE, MEDIUM_ROW_COUNT, MEDIUM_ROW_COUNT);
                DrawGame(MEDIUM_FIELD_SIZE, MEDIUM_FIELD_SIZE, MEDIUM_ROW_COUNT, MEDIUM_ROW_COUNT, e, MEDIUM_MINE_COUNT);
            }
            if (gamefieldPanel.Size == new Size(EXPERT_FIELD_WIDTH, EXPERT_FIELD_HEIGHT))
            {
                DrawFieldLines(EXPERT_FIELD_WIDTH, EXPERT_FIELD_HEIGHT, EXPERT_ROW_COUNT, EXPERT_COLUMN_COUNT);
                DrawGame(EXPERT_FIELD_WIDTH, EXPERT_FIELD_HEIGHT, EXPERT_ROW_COUNT, EXPERT_COLUMN_COUNT, e, EXPERT_MINE_COUNT);
            }
        }
    }
}
