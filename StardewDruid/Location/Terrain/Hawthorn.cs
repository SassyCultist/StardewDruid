﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Location.Terrain
{

    public class Hawthorn : TerrainField
    {

        public Dictionary<Vector2, CommonLeaf> leaves = new();

        public Dictionary<Vector2, CommonLeaf> litter = new();

        public int size;

        public bool littered;

        public int season;

        public Hawthorn(Vector2 Position, int Size = 1)
           : base()
        {

            tilesheet = IconData.tilesheets.hawthorn;

            index = 1;

            position = Position;

            flip = Mod.instance.randomIndex.Next(2) != 0;

            size = Size;

            season = -1;

            fadeout = 0.5f;

            reset();

        }

        public override void reset()
        {

            Vector2 tile = ModUtility.PositionToTile(position);

            where = (int)tile.X % 50;

            baseTiles.Clear();

            switch (size)
            {

                default:
                case 1:

                    for (int y = 1; y <= 2; y++)
                    {

                        for (int x = 4; x < 6; x++)
                        {

                            baseTiles.Add(new Vector2(tile.X + x, tile.Y + (8 - y)));

                        }

                    }

                    layer = (position.Y / 10000) + (0.0064f * 7.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 4 * 64, 6 * 64);

                    Leaves();

                    break;

                case 2:

                    baseTiles.Add(new Vector2(tile.X + 2, tile.Y + 4));

                    layer = (position.Y / 10000) + (0.0064f * 4.5f);

                    bounds = new((int)position.X + 64, (int)position.Y, 3 * 64, 3 * 64);

                    LeavesMedium();

                    break;


            }

            if (littered)
            {

                AddLitter();

            }

            Vector2 corner00 = baseTiles.First() * 64;

            Vector2 corner11 = baseTiles.Last() * 64;

            center = corner00 + (corner11 - corner00) / 2;

            girth = Vector2.Distance(corner00, center);

            clearance = (int)Math.Ceiling(girth / 64);

        }

        public void Leaves()
        {

            leaves.Clear();

            leaves = new()
            {
                // ----------------------
                [new Vector2(128, 16)] = new(CommonLeaf.commonleaves.topleft,0, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(512, 16)] = new(CommonLeaf.commonleaves.topright, 1, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(200, -4)] = new(CommonLeaf.commonleaves.top, 2, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(440, -4)] = new(CommonLeaf.commonleaves.top, 3, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(280, -12)] = new(CommonLeaf.commonleaves.top, 4, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(360, -12)] = new(CommonLeaf.commonleaves.top, 5, 8,CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(24, 72)] = new(CommonLeaf.commonleaves.topleft, 0, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(616, 72)] = new(CommonLeaf.commonleaves.topright, 1, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(88, 68)] = new(CommonLeaf.commonleaves.topleft, 2, 12, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(552, 68)] = new(CommonLeaf.commonleaves.topright, 3, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(160, 60)] = new(CommonLeaf.commonleaves.top, 4, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(480, 60)] = new(CommonLeaf.commonleaves.top, 5, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(232, 52)] = new(CommonLeaf.commonleaves.top, 6, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(408, 52)] = new(CommonLeaf.commonleaves.top, 7, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(320, 40)] = new(CommonLeaf.commonleaves.top, 8, 0,CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-24, 124)] = new(CommonLeaf.commonleaves.topleft, 0, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(664, 124)] = new(CommonLeaf.commonleaves.topright, 1, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(40, 120)] = new(CommonLeaf.commonleaves.topleft, 2, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(600, 120)] = new(CommonLeaf.commonleaves.topright, 3, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(120, 112)] = new(CommonLeaf.commonleaves.topleft, 4, 4, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(520, 112)] = new(CommonLeaf.commonleaves.topright, 5, 4,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(200, 104)] = new(CommonLeaf.commonleaves.top, 6, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(440, 104)] = new(CommonLeaf.commonleaves.top, 7, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(280, 92)] = new(CommonLeaf.commonleaves.top, 8, 0, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(360, 92)] = new(CommonLeaf.commonleaves.top, 9, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-64, 164)] = new(CommonLeaf.commonleaves.topleft, 0, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(704, 164)] = new(CommonLeaf.commonleaves.topright, 1, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(0, 164)] = new(CommonLeaf.commonleaves.topleft, 2, 12, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(640, 164)] = new(CommonLeaf.commonleaves.topright, 3, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(80, 164)] = new(CommonLeaf.commonleaves.topleft, 4, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(560, 164)] = new(CommonLeaf.commonleaves.topright, 5, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(160, 164)] = new(CommonLeaf.commonleaves.mid, 6, 4,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(480, 164)] = new(CommonLeaf.commonleaves.mid, 7, 4,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(240, 160)] = new(CommonLeaf.commonleaves.top, 8, 0, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(400, 160)] = new(CommonLeaf.commonleaves.top, 9, 0, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(320, 156)] = new(CommonLeaf.commonleaves.top, 10, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-80, 188)] = new(CommonLeaf.commonleaves.topleft, 0, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(740, 188)] = new(CommonLeaf.commonleaves.topright, 1, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(0, 196)] = new(CommonLeaf.commonleaves.topleft, 2, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(640, 196)] = new(CommonLeaf.commonleaves.topright, 3, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(88, 208)] = new(CommonLeaf.commonleaves.topleft, 4, 4, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(552, 208)] = new(CommonLeaf.commonleaves.topright, 5, 4,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(184, 220)] = new(CommonLeaf.commonleaves.mid, 6, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(456, 220)] = new(CommonLeaf.commonleaves.mid, 7, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(280, 228)] = new(CommonLeaf.commonleaves.mid, 8, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(360, 228)] = new(CommonLeaf.commonleaves.mid, 9, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-48, 260)] = new(CommonLeaf.commonleaves.bottomleft, 0, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(688, 260)] = new(CommonLeaf.commonleaves.bottomright, 1, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(40, 264)] = new(CommonLeaf.commonleaves.bottomleft, 2, 12, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(600, 264)] = new(CommonLeaf.commonleaves.bottomright, 3, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(136, 272)] = new(CommonLeaf.commonleaves.bottomleft, 4, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(504, 272)] = new(CommonLeaf.commonleaves.bottomright, 5, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(232, 280)] = new(CommonLeaf.commonleaves.mid, 6, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(408, 280)] = new(CommonLeaf.commonleaves.mid, 7, 0,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(320, 288)] = new(CommonLeaf.commonleaves.mid, 8, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-16, 308)] = new(CommonLeaf.commonleaves.bottomleft, 0, 20,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(656, 308)] = new(CommonLeaf.commonleaves.bottomright, 1, 20,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(72, 320)] = new(CommonLeaf.commonleaves.bottomleft, 2, 16, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(568, 320)] = new(CommonLeaf.commonleaves.bottomright, 3, 16,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(168, 332)] = new(CommonLeaf.commonleaves.bottom, 4, 12, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(472, 332)] = new(CommonLeaf.commonleaves.bottom, 5, 12,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(272, 348)] = new(CommonLeaf.commonleaves.mid, 6, 8,CommonLeaf.commonrenders.hawthorn),
                [new Vector2(368, 348)] = new(CommonLeaf.commonleaves.mid, 7, 8,CommonLeaf.commonrenders.hawthorn),

            };

            foreach (KeyValuePair<Vector2, CommonLeaf> leave in leaves)
            {

                leave.Value.where = Math.Abs((int)(leave.Key.X / 64)) % 50;

            }
        }
        
        public void LeavesMedium()
        {

            leaves.Clear();

            leaves = new()
            {
                // ----------------------
                [new Vector2(64, -4)] = new(CommonLeaf.commonleaves.topleft, 0, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(256, -4)] = new(CommonLeaf.commonleaves.topright, 1, 8, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(128, -12)] = new(CommonLeaf.commonleaves.top, 2, 0, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(192, -12)] = new(CommonLeaf.commonleaves.top, 3, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-32, 68)] = new(CommonLeaf.commonleaves.topleft, 0, 16, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(352, 68)] = new(CommonLeaf.commonleaves.topright, 1, 16, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(32, 60)] = new(CommonLeaf.commonleaves.top, 2, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(288, 60)] = new(CommonLeaf.commonleaves.top, 3, 8, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(96, 52)] = new(CommonLeaf.commonleaves.top, 4, 8, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(224, 52)] = new(CommonLeaf.commonleaves.top, 5, 0, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(160, 40)] = new(CommonLeaf.commonleaves.top, 6, 0, CommonLeaf.commonrenders.hawthorn),

                // ----------------------
                [new Vector2(-64, 120)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(384, 120)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(0, 112)] = new(CommonLeaf.commonleaves.bottomleft, 2, 16, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(320, 112)] = new(CommonLeaf.commonleaves.bottomright, 3, 16, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(64, 104)] = new(CommonLeaf.commonleaves.top, 4, 16, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(256, 104)] = new(CommonLeaf.commonleaves.top, 5, 16, CommonLeaf.commonrenders.hawthorn),
                
                [new Vector2(128, 92)] = new(CommonLeaf.commonleaves.top, 6, 16, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(192, 92)] = new(CommonLeaf.commonleaves.top, 7, 16, CommonLeaf.commonrenders.hawthorn),

                // ----------------------

                [new Vector2(32, 156)] = new(CommonLeaf.commonleaves.bottomleft, 0, 24, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(288, 156)] = new(CommonLeaf.commonleaves.bottomright, 1, 24, CommonLeaf.commonrenders.hawthorn),

                [new Vector2(96, 152)] = new(CommonLeaf.commonleaves.bottom, 2, 24, CommonLeaf.commonrenders.hawthorn),
                [new Vector2(224, 152)] = new(CommonLeaf.commonleaves.bottom, 3, 24, CommonLeaf.commonrenders.hawthorn),

                [new Vector2(160, 148)] = new(CommonLeaf.commonleaves.bottom, 4, 24, CommonLeaf.commonrenders.hawthorn),

            };

            foreach (KeyValuePair<Vector2, CommonLeaf> leave in leaves)
            {

                leave.Value.where = Math.Abs((int)(leave.Key.X / 64)) % 50;

            }
        }

        public void AddLitter()
        {

            littered = true;

            litter.Clear();

            switch (size)
            {

                default:
                case 1:

                    double radians = Math.PI / 4;

                    for (int x = 1; x < 5; x++)
                    {

                        double angle = x * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10, 14) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));


                    }

                    for (int x = 0; x < 4; x++)
                    {

                        double angle = x * -1 * radians;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(10, 14) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));

                    }

                    break;

                case 2:
                    /*
                    double radian2 = Math.PI / 4;

                    for (int x = 1; x < 5; x++)
                    {

                        double angle = x * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));


                    }

                    for (int x = 0; x < 4; x++)
                    {

                        double angle = x * -1 * radian2;

                        Vector2 position = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Mod.instance.randomIndex.Next(4, 12) * 16;

                        litter.Add(position, new(CommonLeaf.commonleaves.litter, 0));

                    }
                    */
                    break;
            }

        }

        public override void update(GameLocation location)
        {

            base.update(location);

            wind = 0f;

            windout = 0f;

            if (shake > 0)
            {

                shake--;

                wind = Mod.instance.environment.retrieve(shake, EnvironmentHandle.environmentEffect.trunkShake);

                switch (size)
                {

                    default:
                    case 1:

                        windout = wind * 400;

                        break;

                    case 2:

                        windout = wind * 200;

                        break;

                }

                foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
                {

                    leaf.Value.wind = 0f;

                    leaf.Value.windout = 0f;

                    leaf.Value.wind = Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafShake);

                    leaf.Value.windout = windout + leaf.Value.wind * 80f;

                }

            }
            else
            if (!ruined)
            {

                wind = Mod.instance.environment.retrieve(where, EnvironmentHandle.environmentEffect.trunkRotate);

                switch (size)
                {

                    default:
                    case 1:

                        windout = wind * 400;

                        break;

                    case 2:

                        windout = wind * 200;

                        break;

                }

                foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
                {

                    leaf.Value.wind = 0f;

                    leaf.Value.windout = 0f;

                    leaf.Value.wind = Mod.instance.environment.retrieve(where + leaf.Value.where, EnvironmentHandle.environmentEffect.leafRotate);

                    leaf.Value.windout = leaf.Value.wind * 80f;

                }

            }

        }

        public override void drawFront(SpriteBatch b, GameLocation location)
        {

            if (ruined)
            {

                return;

            }

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {
                return;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            int offset = 0;

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            switch (useSeason)
            {

                case Season.Summer: offset = 160; break;

                case Season.Fall: offset = 320; break;

                case Season.Winter: offset = 480; break;

            }

            Vector2 span = new Vector2(16);

            foreach (KeyValuePair<Vector2, CommonLeaf> leaf in leaves)
            {

                Vector2 place = new(leaf.Value.windout, 0);

                b.Draw(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornleaf],
                    origin + leaf.Key + place,
                    new Rectangle(leaf.Value.source.X + offset, leaf.Value.source.Y, 32, 32),
                    leaf.Value.colour * fade,
                    0 - leaf.Value.wind,
                    span,
                    4f,
                    leaf.Value.flip ? (SpriteEffects)1 : 0,
                    900f
                );

            }

        }

        public override void draw(SpriteBatch b, GameLocation location)
        {

            if (!Utility.isOnScreen(bounds.Center.ToVector2(), 512))
            {
                return;

            }

            Season useSeason = Game1.season;

            if (season != -1)
            {

                useSeason = (Season)season;

            }

            Vector2 origin = new(position.X - Game1.viewport.X, position.Y - Game1.viewport.Y);

            Vector2 place = new Vector2(windout,0);

            Microsoft.Xna.Framework.Color trunkcolour = new(192, 192, 256);

            if (!ruined)
            {

                Vector2 span = new Vector2(16);

                Vector2 lay;

                switch (size)
                {
                    default:
                    case 1:

                        lay = new Vector2(origin.X + 320, origin.Y + 448);

                        break;

                    case 2:

                        lay = new Vector2(origin.X + 160, origin.Y + 288);

                        break;

                }

                if (useSeason == Season.Fall)
                {

                    foreach (KeyValuePair<Vector2, CommonLeaf> leaf in litter)
                    {

                        int leafoffset = 0;

                        switch (useSeason)
                        {

                            case Season.Summer: leafoffset = 160; break;

                            case Season.Fall: leafoffset = 320; break;

                            case Season.Winter: leafoffset = 480; break;

                        }

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornleaf],
                            lay + leaf.Key,
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.White,
                            wind * 2,
                            span,
                            4,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0005f
                        );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornleaf],
                            lay + leaf.Key + new Vector2(2, 8),
                            new Rectangle(leaf.Value.source.X + leafoffset, leaf.Value.source.Y, 32, 32),
                            Color.Black * 0.3f,
                            wind * 2,
                            span,
                            4,
                            leaf.Value.flip ? (SpriteEffects)1 : 0,
                            0.0004f
                        );

                    }
                
                }

            }

            int offset = 0;

            Microsoft.Xna.Framework.Color trunkShadow = Color.White * 0.2f;

            Microsoft.Xna.Framework.Color gladeShade = Color.White * 0.1f;

            Microsoft.Xna.Framework.Color leafShade = Color.White * 0.15f;

            trunkcolour = Color.White;

            if (fade < 1f)
            {

                trunkShadow = Color.White * 0.1f;

                gladeShade = Color.White * 0.05f;

                leafShade = Color.White * 0.075f;

            }

            switch (size)
            {
                default:
                case 1:

                    switch (useSeason)
                    {

                        case Season.Summer: offset = 160; break;

                        case Season.Fall: offset = 320; break;

                        case Season.Winter: offset = 480; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthorn],
                        origin + new Vector2(320, 256) + place,
                        new Rectangle(offset, 0, 160, 128),
                        trunkcolour * fade,
                        wind,
                        new Vector2(80, 64),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );



                    if (Game1.timeOfDay > 2100)
                    {

                        break;

                    }

                    if (!ruined)
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(320, 416) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            0,
                            new Vector2(96, 80),
                            4f,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0384f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(320, 416) + place,
                            new Rectangle(0, 0, 192, 160),
                            leafShade,
                            0,
                            new Vector2(96, 80),
                            4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(320, 448) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );


                    }

                    break;

                case 2:

                    switch (useSeason)
                    {

                        case Season.Summer: offset = 112; break;

                        case Season.Fall: offset = 224; break;

                        case Season.Winter: offset = 336; break;

                    }

                    b.Draw(
                        Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthorn],
                        origin + new Vector2(160, 160) + place, // +64 inwards than normal due to size revision
                        new Rectangle(offset, 128, 112, 80),
                        trunkcolour * fade,
                        wind,
                        new Vector2(56, 40),
                        4,
                        flip ? (SpriteEffects)1 : 0,
                        layer - 0.001f
                        );


                    if (Game1.timeOfDay > 2100)
                    {

                        break;

                    }

                    if (!ruined)
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(160, 308) + place,
                            new Rectangle(192, 0, 192, 160),
                            gladeShade,
                            0,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            layer + 0.0224f
                            );

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(160, 308) + place,
                            new Rectangle(0, 0, 192, 160),
                            leafShade,
                            0,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }
                    else
                    {

                        b.Draw(
                            Mod.instance.iconData.sheetTextures[IconData.tilesheets.hawthornshade],
                            origin + new Vector2(160, 300) + place,
                            new Rectangle(384, 0, 192, 160),
                            trunkShadow,
                            wind,
                            new Vector2(96, 80),
                            2.4f,
                            flip ? (SpriteEffects)1 : 0,
                            0.001f
                            );

                    }

                    break;

            }

        }

    }

}
