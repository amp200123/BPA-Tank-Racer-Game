﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PanzerDash
{
    /// <summary>
    /// Game state for a sound options screen
    /// </summary>
    public class SoundScreen : Screen
    {
        private Texture2D logo;
        private Texture2D sfxLogo, musicLogo;

        private Texture2D backButton, backButtonDefault, backButtonSelected;
        private Texture2D musicBar, sfxBar;
        private Texture2D soundBar, soundBarSelected;
        private Texture2D soundPickerMusic, soundPickerFX;
        private Texture2D soundBarBackground;

        private KeyboardState oldState;

        private int selectedButton;

        private float musicVolume;
        private float sfxVolume;

        public SoundScreen(ContentManager content, EventHandler screenEvent)
            : base(screenEvent)
        {
            musicVolume = Game1.musicVolume;
            sfxVolume = Game1.effectVolume;

            logo = content.Load<Texture2D>("Sound");
            soundBarBackground = content.Load<Texture2D>("SoundBarBackground");

            sfxLogo = content.Load<Texture2D>("SFX");
            musicLogo = content.Load<Texture2D>("Music");

            backButtonDefault = content.Load<Texture2D>("Back");
            soundBar = content.Load<Texture2D>("SoundBar");

            backButtonSelected = content.Load<Texture2D>("Back-Selected");
            soundBarSelected = content.Load<Texture2D>("SoundBar-Selected");

            soundPickerFX = content.Load<Texture2D>("SoundPicker");
            soundPickerMusic = content.Load<Texture2D>("SoundPicker");

            musicBar = soundBarSelected;
            sfxBar = soundBar;
            backButton = backButtonDefault;
        }

        public override void Update(GameTime gametime)
        {
            KeyboardState newState = Keyboard.GetState();

            //Keyboard logic
            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                Game1.selectFX.Play();

                //Change selected button

                if (selectedButton == 0)
                    musicBar = soundBar;
                else if (selectedButton == 1)
                    sfxBar = soundBar;
                else if (selectedButton == 2)
                    backButton = backButtonDefault;

                selectedButton++;
                selectedButton %= 3;

                if (selectedButton == 0)
                    musicBar = soundBarSelected;
                else if (selectedButton == 1)
                    sfxBar = soundBarSelected;
                else if (selectedButton == 2)
                    backButton = backButtonSelected;
            }
            else if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                Game1.selectFX.Play();

                //Change selected button

                if (selectedButton == 0)
                    musicBar = soundBar;
                else if (selectedButton == 1)
                    sfxBar = soundBar;
                else if (selectedButton == 2)
                    backButton = backButtonDefault;

                if (selectedButton == 0)
                    selectedButton = 2;
                else selectedButton--;

                if (selectedButton == 0)
                    musicBar = soundBarSelected;
                else if (selectedButton == 1)
                    sfxBar = soundBarSelected;
                else if (selectedButton == 2)
                    backButton = backButtonSelected;
            }

            if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                if (selectedButton == 0)
                {
                    if (musicVolume <= 1.0f)
                    {
                        Game1.selectFX.Play();
                        musicVolume += 0.1f;
                    }
                }
                else if (selectedButton == 1)
                {
                    if (sfxVolume <= 1.0f)
                    {
                        Game1.selectFX.Play();
                        sfxVolume += 0.1f;
                    }
                }
            }
            else if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                if (selectedButton == 0)
                {
                    if (musicVolume >= 0.1f)
                    {
                        Game1.selectFX.Play();
                        musicVolume -= 0.1f;
                    }
                }
                else if (selectedButton == 1)
                {
                    if (sfxVolume >= 0.1f)
                    {
                        Game1.selectFX.Play();
                        sfxVolume -= 0.1f;
                    }
                }
            }

            if (newState.IsKeyDown(Keys.Enter) && selectedButton == 2)
                screenEvent.Invoke(this, new EventArgs());

            //Edit game volume
            Game1.musicVolume = (float)Math.Round(musicVolume, 1);
            Game1.effectVolume = (float)Math.Round(sfxVolume, 1);

            oldState = newState;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            //Draw logos
            spritebatch.Draw(logo, new Vector2(Game1.WindowWidth / 2, 25), new Rectangle(0, 0, logo.Width, logo.Height),
                Color.White, 0, new Vector2(logo.Width / 2, logo.Height / 2), 1.3f, SpriteEffects.None, 1f);
            spritebatch.Draw(musicLogo, new Vector2(Game1.WindowWidth / 2, 150), new Rectangle(0, 0, musicLogo.Width, musicLogo.Height),
                Color.White, 0, new Vector2(musicLogo.Width / 2, musicLogo.Height / 2), 1, SpriteEffects.None, 1f);
            spritebatch.Draw(sfxLogo, new Vector2(Game1.WindowWidth / 2, 250), new Rectangle(0, 0, sfxLogo.Width, sfxLogo.Height),
                Color.White, 0, new Vector2(sfxLogo.Width / 2, sfxLogo.Height / 2), 1, SpriteEffects.None, 1f);

            //Draw bar backgrounds
            spritebatch.Draw(soundBarBackground, new Vector2(Game1.WindowWidth / 2, 200), new Rectangle(0, 0, soundBarBackground.Width, soundBarBackground.Height),
                Color.White, 0, new Vector2(soundBarBackground.Width / 2, soundBarBackground.Height / 2), 1, SpriteEffects.None, 1f);
            spritebatch.Draw(soundBarBackground, new Vector2(Game1.WindowWidth / 2, 300), new Rectangle(0, 0, soundBarBackground.Width, soundBarBackground.Height),
                Color.White, 0, new Vector2(soundBarBackground.Width / 2, soundBarBackground.Height / 2), 1, SpriteEffects.None, 1f);

            //Draw bars
            spritebatch.Draw(musicBar, new Vector2(Game1.WindowWidth / 2, 200), new Rectangle(0, 0, musicBar.Width, musicBar.Height),
                Color.White, 0, new Vector2(musicBar.Width / 2, musicBar.Height / 2), 1, SpriteEffects.None, 1f);
            spritebatch.Draw(sfxBar, new Vector2(Game1.WindowWidth / 2, 300), new Rectangle(0, 0, sfxBar.Width, sfxBar.Height),
                Color.White, 0, new Vector2(sfxBar.Width / 2, sfxBar.Height / 2), 1, SpriteEffects.None, 1f);

            //Draw sound pickers
            spritebatch.Draw(soundPickerMusic, new Vector2((Game1.WindowWidth / 2 + musicBar.Width / 2) - (musicBar.Width * musicVolume), 200),
                new Rectangle(0, 0, soundPickerMusic.Width, soundPickerMusic.Height), Color.White, 0,
                new Vector2(soundPickerMusic.Width / 2, soundPickerMusic.Height / 2), 1, SpriteEffects.None, 1f);
            spritebatch.Draw(soundPickerFX, new Vector2((Game1.WindowWidth / 2 + sfxBar.Width / 2) - (sfxBar.Width * sfxVolume), 300),
                new Rectangle(0, 0, soundPickerFX.Width, soundPickerFX.Height), Color.White, 0,
                new Vector2(soundPickerFX.Width / 2, soundPickerFX.Height / 2), 1, SpriteEffects.None, 1f);

            //Draw back button
            spritebatch.Draw(backButton, new Vector2(Game1.WindowWidth / 2, 400), new Rectangle(0, 0,
                backButton.Width, backButton.Height), Color.White, 0, new Vector2(backButton.Width / 2, backButton.Height / 2),
                1, SpriteEffects.None, 1f);
        }
    }
}
