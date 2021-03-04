using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestControlPanel;

namespace TestUnrealUDP
{
    public partial class WindowPanel : Form
    {
        private List<int> baccaratPlayersIds = new List<int>();
        private List<int> baccarat2PlayersIds = new List<int>();
        private List<int> roulettePlayersIds = new List<int>();
        private List<int> blackjackPlayersIds = new List<int>();

        private GameType gameType = GameType.None;
        public WindowPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var game = comboBox1.Text;
            var currentGameType = gameType;
            tableLayoutPanel3.Enabled = false;
            tableLayoutPanel5.Enabled = false;
            tableLayoutPanel8.Enabled = false;
            tableLayoutPanel9.Enabled = false;
            tableLayoutPanel10.Enabled = false;
            tableLayoutPanel11.Enabled = false;
            tableLayoutPanel15.Enabled = false;
            var list = new List<int>();
            switch (game)
            {
                case "roulette":
                    gameType = GameType.Roulette;
                    tableLayoutPanel3.Enabled = true;
                    list = roulettePlayersIds;
                    break;
                case "baccarat":
                    gameType = GameType.Baccarat;
                    tableLayoutPanel5.Enabled = true;
                    list = baccaratPlayersIds;
                    if (baccaratPlayersIds.Count > 0)
                    {
                        foreach (var id in baccaratPlayersIds)
                        {
                            comboBox4.Items.Add(id);
                        }
                    }
                    break;
                case "poker":
                    gameType = GameType.Poker;
                    tableLayoutPanel15.Enabled = true;
                    break;
                case "blackjack":
                    gameType = GameType.Blackjack;
                    list = blackjackPlayersIds;
                    tableLayoutPanel8.Enabled = true;
                    tableLayoutPanel9.Enabled = true;
                    tableLayoutPanel10.Enabled = true;
                    tableLayoutPanel11.Enabled = true;
                    tableLayoutPanel15.Enabled = false;
                    if (blackjackPlayersIds.Count > 0)
                    {
                        foreach (var id in blackjackPlayersIds)
                        {
                            comboBox8.Items.Add(id);
                            comboBox11.Items.Add(id);
                            comboBox12.Items.Add(id);
                        }
                    }
                    break;
            }
            if (list.Count > 0 && comboBox3.Items.Count == 0 && comboBox7.Items.Count == 0)
            {
                foreach (var id in list)
                {
                    comboBox3.Items.Add(id);
                    comboBox7.Items.Add(id);
                }
            }

            if (currentGameType != gameType)
                Command.SetGameType(game);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //var name = textBox1.Text;
            //if (string.IsNullOrEmpty(name))
            //{
            //    MessageBox.Show("Name cannot be empty");
            //    return;
            //}
            //var id = (int)numericUpDown4.Value;
            //if (gameType == GameType.Roulette)
            //{
            //    roulettePlayersIds.Add(id);
            //}
            //if (gameType == GameType.Baccarat)
            //{
            //    baccaratPlayersIds.Add(id);
            //    comboBox4.Items.Add(id);
            //    comboBox4.Text = comboBox4.Items[0].ToString();
            //}

            //if (gameType == GameType.Blackjack)
            //{
            //    blackjackPlayersIds.Add(id);
            //    comboBox8.Items.Add(id);
            //    comboBox8.Text = comboBox8.Items[0].ToString();
            //    comboBox11.Items.Add(id);
            //    comboBox11.Text = comboBox11.Items[0].ToString();
            //    comboBox12.Items.Add(id);
            //    comboBox12.Text = comboBox12.Items[0].ToString();
            //}

            //comboBox3.Items.Add(id);
            //comboBox3.Text = comboBox3.Items[0].ToString();

            //comboBox7.Items.Add(id);
            //comboBox7.Text = comboBox7.Items[0].ToString();


            //comboBox13.Items.Add(id);
            //comboBox13.Text = comboBox13.Items[0].ToString();


            //Command.AddPlayer(id, name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var id = comboBox3.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                var bet = numericUpDown2.Value;
                var hand = (int)numericUpDown13.Value;
                Command.SetPlaceBet(idPlayer, bet, hand);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var id = comboBox7.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                Command.RemovePlayer(idPlayer);
            }
            else
            {
                return;
            }
            if (gameType == GameType.Roulette)
            {
                roulettePlayersIds.Remove(idPlayer);
            }
            if (gameType == GameType.Baccarat)
            {
                baccaratPlayersIds.Remove(idPlayer);
                comboBox4.Items.Remove(id);
                if (comboBox4.Items.Count > 0)
                    comboBox4.Text = comboBox4.Items[0].ToString();
            }

            if (gameType == GameType.Blackjack)
            {
                blackjackPlayersIds.Remove(idPlayer);
                comboBox8.Items.Remove(idPlayer);
                if (comboBox8.Items.Count > 0)
                    comboBox8.Text = comboBox8.Items[0].ToString();
                comboBox11.Items.Remove(idPlayer);
                if (comboBox11.Items.Count > 0)
                    comboBox11.Text = comboBox11.Items[0].ToString();
                comboBox12.Items.Remove(idPlayer);
                if (comboBox12.Items.Count > 0)
                    comboBox12.Text = comboBox12.Items[0].ToString();
            }

            comboBox13.Items.Remove(id);
            if (comboBox13.Items.Count > 0)
                comboBox13.Text = comboBox13.Items[0].ToString();

            comboBox3.Items.Remove(idPlayer);
            if (comboBox3.Items.Count > 0)
                comboBox3.Text = comboBox3.Items[0].ToString();

            comboBox7.Items.Remove(idPlayer);
            if (comboBox7.Items.Count > 0)
                comboBox7.Text = comboBox7.Items[0].ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var status = comboBox2.Text;
            if (!string.IsNullOrEmpty(status))
            {
                Command.SetStatus(status);
            }
            else
            {
                MessageBox.Show("Status cannot be empty");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Command.ResetGame();
            if (gameType == GameType.Baccarat)
            {
                baccaratPlayersIds.Clear();
                comboBox4.Items.Clear();
            }
            if (gameType == GameType.Baccarat2)
            { baccarat2PlayersIds.Clear(); }
            if (gameType == GameType.Roulette)
            { roulettePlayersIds.Clear(); }
            if (gameType == GameType.Blackjack)
            { blackjackPlayersIds.Clear(); }

            comboBox3.Items.Clear();
            comboBox7.Items.Clear();
            button1_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var id = comboBox4.Text;
            var number = (int)numericUpDown3.Value;
            if (int.TryParse(id, out var idPlayer))
            {
                var card = comboBox5.Text;
                Command.SetPlayerCard(idPlayer, card, number);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var card = comboBox6.Text;
            var number = (int)numericUpDown5.Value;
            Command.SetDealerCard(card, number);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var number = (int)numericUpDown1.Value;
            Command.ThrowTheBall(number);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //var id = comboBox8.Text;
            //if (int.TryParse(id, out var idPlayer))
            //{
            //    var card = comboBox9.Text;
            //    var number = (int)numericUpDown6.Value;
            //    bool isFaceUp = checkBox1.Checked;
            //    var hand = (int)numericUpDown11.Value;
            //    Command.SetPlayerCard(idPlayer, card, number, isFaceUp, hand);
            //}
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //var card = comboBox10.Text;
            //var number = (int)numericUpDown7.Value;
            //bool isFaceUp = checkBox2.Checked;

            //Command.SetDealerCard(card, number, isFaceUp);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var id = comboBox11.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                Command.Split(idPlayer);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var id = comboBox12.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                var bet = numericUpDown8.Value;
                var hand = (int)numericUpDown12.Value;
                Command.Insurance(idPlayer, bet, hand);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var id = comboBox13.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                var number = (int)numericUpDown9.Value;
                Command.PlayerFlipCard(idPlayer, number);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var number = (int)numericUpDown10.Value;
            Command.DealerFlipCard(number);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var id = comboBox15.Text;
            if (int.TryParse(id, out var idPlayer))
            {
                var card = comboBox16.Text;
                var number = (int)numericUpDown15.Value;
                Command.SetPlayerCard(idPlayer, card, number);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var card = comboBox14.Text;
            var number = (int)numericUpDown14.Value;
            Command.SetDealerCard(card, number);
        }
    }

    enum GameType
    {
        Roulette,
        Baccarat,
        Baccarat2,
        Blackjack,
        Poker,
        None
    }
}
