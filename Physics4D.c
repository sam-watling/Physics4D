#include "raylib.h"
#include "math4dhelper.h"
#include <stdio.h>

typedef struct Camera4D{
    Vector4 position;
    Vector4 target;
    Vector4 up;
    Vector4 over;
    float viewAngle;
}Camera4D;
    

Camera CameraFromCamera4D(Camera4D camera4D)
{
    Camera camera = {{camera4D.position.x,camera4D.position.y,camera4D.position.z},
                     {camera4D.target.x,camera4D.target.y,camera4D.target.z},
                     {camera4D.up.x,camera4D.up.y,camera4D.up.z},camera4D.viewAngle,0};
    return camera;
}

int main(void)
{
    
    // Initialization
    //--------------------------------------------------------------------------------------
    const int screenWidth = 1200;
    const int screenHeight = 900;
        

    InitWindow(screenWidth, screenHeight, "Sphere Collision");

    // Define 4d camera
    Camera4D camera4D = { {10.0f,10.0f,10.0f,10.0f},{0.0f,0.0f,0.0f,0.0f},{0.0f,1.0f,0.0f,0.0f},{0.0f,0.0f,0.0f,1.0f},45.0f};
    // Define the camera to look into our 3d world
    Camera camera = CameraFromCamera4D(camera4D);
    
    
    // create tesseract, 3d object in 4d space
    
    Vector4 testTesseract[16];
    HyperCubeGen(testTesseract, Vector4Zero(),1.0f);
    
    
    
    // Main loop
    while (!WindowShouldClose())    
    {
        // Update
        //----------------------------------------------------------------------------------
        
        
        //----------------------------------------------------------------------------------

        // Draw
        //----------------------------------------------------------------------------------
        BeginDrawing();

            ClearBackground(RAYWHITE);

            BeginMode3D(camera);            
            
            
            
            EndMode3D();

            DrawFPS(10, 10);

        EndDrawing();
        //----------------------------------------------------------------------------------
    }

    // De-Initialization
    //--------------------------------------------------------------------------------------
    CloseWindow();        // Close window
    //--------------------------------------------------------------------------------------

    return 0;
    
}
    
    