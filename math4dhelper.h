if


typedef struct Vector5{
    float x;
    float y;
    float z;
    float w;
    float u;    
}Vector5;

typedef struct Matrix5 {
    float m0, m5, m10, m15, m20;
    float m1, m6, m11, m16, m21;
    float m2, m7, m12, m17, m22;
    float m3, m8, m13, m18, m23;
    float m4, m9, m14, m19, m24;
} Matrix5;

typedef struct Camera4D{
    Vector4 position;
    Vector4 target;
    Vector4 up;
    Vector4 over;
    float viewAngle;
}Camera4D;
    
void HyperCubeGen(Vector4 cube[], Vector4 pos, float scale)
{
    cube[0]  = (Vector4){-scale, scale,-scale,-scale};
    cube[1]  = (Vector4){-scale, scale, scale,-scale};
    cube[2]  = (Vector4){ scale, scale, scale,-scale};
    cube[3]  = (Vector4){ scale, scale,-scale,-scale};
    cube[4]  = (Vector4){-scale,-scale,-scale,-scale};
    cube[5]  = (Vector4){-scale,-scale, scale,-scale};
    cube[6]  = (Vector4){ scale,-scale, scale,-scale};
    cube[7]  = (Vector4){ scale,-scale,-scale,-scale};
    cube[8]  = (Vector4){-scale, scale,-scale, scale};
    cube[9]  = (Vector4){-scale, scale, scale, scale};
    cube[10] = (Vector4){ scale, scale, scale, scale};
    cube[11] = (Vector4){ scale, scale,-scale, scale};
    cube[12] = (Vector4){-scale,-scale,-scale, scale};
    cube[13] = (Vector4){-scale,-scale, scale, scale};
    cube[14] = (Vector4){ scale,-scale, scale, scale};
    cube[15] = (Vector4){ scale,-scale,-scale, scale};
    for(int i = 0; i <16;i++)
    {
        cube[i].x += pos.x;
        cube[i].y += pos.y;
        cube[i].z += pos.z;
        cube[i].w += pos.w;
    }
}

Vector4 Vector4Zero()
{ 
    Vector4 result = {0,0,0,0};
    return result;
}
Vector4 Vector4One()
{ 
    Vector4 result = {1,1,1,1};
    return result;
}
Camera CameraFromCamera4D(Camera4D camera4D)
{
    Camera camera = {{camera4D.position.x,camera4D.position.y,camera4D.position.z},
                     {camera4D.target.x,camera4D.target.y,camera4D.target.z},
                     {camera4D.up.x,camera4D.up.y,camera4D.up.z},camera4D.viewAngle,0};
    return camera;
}
