﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tibialyzer.Structures;

namespace Tibialyzer {
    public partial class ManaBar : BaseHUD {
        private bool displayText;
        private bool centerText;
        private bool reverseBar;

        public ManaBar() {
            InitializeComponent();
            
            BackColor = StyleManager.BlendTransparencyKey;
            TransparencyKey = StyleManager.BlendTransparencyKey;

            displayText = SettingsManager.getSettingBool(GetHUD() + "DisplayText");
            centerText = SettingsManager.getSettingBool(GetHUD() + "CenterText");
            reverseBar = SettingsManager.getSettingBool(GetHUD() + "ReverseProgressBar");
            double opacity = SettingsManager.getSettingDouble(GetHUD() + "Opacity");
            opacity = Math.Min(1, Math.Max(0, opacity));
            this.Opacity = opacity;

            MemoryReader.RegisterManaChanged(this, (o, e) => RefreshHUD(e));
            ProcessManager.RegisterTibiaVisibilityChanged(this, (o, e) => UpdateVisibility(e));
        }

        public override void LoadHUD() {
            double fontSize = SettingsManager.getSettingDouble(GetHUD() + "FontSize");
            fontSize = fontSize < 0 ? 20 : fontSize;
            this.manaBarLabel.Font = new System.Drawing.Font("Verdana", (float)fontSize, System.Drawing.FontStyle.Bold);

            this.manaBarLabel.reverse = reverseBar;
            this.manaBarLabel.centerText = centerText;

            this.RefreshHUD(MemoryReader.mana, MemoryReader.maxMana);
        }

        private void RefreshHUD(long value, long max) {
            double percentage = ((double) value) / ((double) max);
            if (displayText) {
                manaBarLabel.Text = String.Format("{0}/{1}", value, max);
            } else {
                manaBarLabel.Text = "";
            }

            manaBarLabel.percentage = percentage;
            manaBarLabel.Size = this.Size;
            manaBarLabel.BackColor = StyleManager.ManaColor;
        }

        private void RefreshHUD(PlayerMana playerMp) {
            long mana = playerMp.Mana;
            long maxMana = playerMp.MaxMana;

            if (maxMana == 0) {
                mana = 1;
                maxMana = 1;
            }

            try
            {
                this.Invoke((MethodInvoker)delegate {
                    RefreshHUD(mana, maxMana);
                });
            }
            catch
            {
            }
        }

        public override string GetHUD() {
            return "ManaBar";
        }
    }
}
