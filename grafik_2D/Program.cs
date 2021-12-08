using System;
using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(800, 600, "2D_game");
Raylib.SetTargetFPS(60);

int points = 0;
bool pointTaken = false;
float speed = 3f;

Vector2 textPos = new Vector2(225,300);

Texture2D playerImage = Raylib.LoadTexture("kanye.png");
Rectangle playerRect = new Rectangle(10,10,playerImage.width,playerImage.height);
Rectangle treasureRect = new Rectangle(500,500,100,50);
Rectangle pointRect = new Rectangle(200, 500, 16, 16);

Font tale = Raylib.LoadFont("Milonga-Regular.ttf");

string level = "start";

    while(!Raylib.WindowShouldClose())
    {
        if(level == "start" || level == "room2")
        {
            playerRect = readMovement(playerRect,speed);
        }

        if(level == "start")
        {
            if(Raylib.CheckCollisionRecs(playerRect,treasureRect))
            {
                level = "end";
            }
            if(Raylib.CheckCollisionRecs(playerRect, pointRect) && pointTaken == false)
            {
                points = +1;
                pointTaken = true;
            }
            if(playerRect.x > 800)
            {
                level = "room2";
                playerRect.x = 0;
            }
        }
        else if(level == "room2")
        {
            if(playerRect.x < 0)
            {
                level = "start";
                playerRect.x = 800 - playerRect.width;
            }
        }

        Raylib.BeginDrawing();

        if(level == "start")
        {
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawRectangleRec(treasureRect, Color.BLACK);
            if(pointTaken == false)
            {
            Raylib.DrawRectangleRec(pointRect, Color.GOLD);
            }
        }
        else if(level == "end")
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawTextEx(tale, "YOU WIN", textPos, 80, 0, Color.GOLD);
        }

        if(level == "start" || level == "room2")
        {
            Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
            Raylib.DrawText(points.ToString(), 10, 10, 20, Color.BLACK);
        }

        Raylib.EndDrawing();

        static Rectangle readMovement(Rectangle playerRect, float speed)
        {
            if(Raylib.IsKeyDown(KeyboardKey.KEY_W)) playerRect.y -= speed;
            if(Raylib.IsKeyDown(KeyboardKey.KEY_S)) playerRect.y += speed;
            if(Raylib.IsKeyDown(KeyboardKey.KEY_A)) playerRect.x -= speed;
            if(Raylib.IsKeyDown(KeyboardKey.KEY_D)) playerRect.x += speed;

            return playerRect;
        }
    }