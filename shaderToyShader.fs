#define MAX_MARCH_STEPS 100
#define MAX_MARCH_DIST 100.
#define HIT_DIST .01

vec4 lightPos = vec4(2, 1, 0,4);

vec4 spherePos = vec4(1.,1.,5.,2.5);
vec4 sphere2Pos = vec4(0.,1.,6.,3.1);
vec4 sphere3Pos = vec4(1.,1.,3.,3.2);
vec4 tetPos = vec4(-0.,0.,5.,3.);
vec4 cubePos= vec4(-3.,1.,4.,2.5);

mat4 rotate(float theta)
{
    float c = cos(theta);
    float s = sin(theta);
    return mat4(
           vec4(c,0,-s,0),
           vec4(0,c,0,-s),
           vec4(s,0,c,0),
           vec4(0,s,0,c));
}


float sd4Sphere(vec4 p, float r) 
{
    return length(p)-r;
}

float sd4Cube(vec4 p, vec4 b)
{
    p*= rotate(iTime);
    vec4 q = abs(p) - b;
    return min(max(max(max(q.z,q.w),q.y),q.x),0.) + length(max(q,0.));
}


float map(vec4 pos)
{
    float res = min((sd4Sphere(pos-spherePos,1.0)),sd4Cube(pos-cubePos,vec4(.7)));
         res = min(res, (min(sd4Sphere(pos-sphere2Pos,1.0),sd4Sphere(pos-sphere3Pos,0.5))));
         res = min(res, (sd4Sphere(pos-sphere3Pos,0.5)));
    return res;
}

float rayMarch(vec4 rayO, vec4 rayD)
{
    float distO=0.;
    
    for(int i=0; i<MAX_MARCH_STEPS; i++)
    {
        vec4 p = rayO + rayD*distO;
        float distS = map(p);
        distO += distS;
        if(distO>MAX_MARCH_DIST||distS<HIT_DIST) break;
    }
    return distO;
}

vec4 GetNormal(vec4 p) {
	float d = map(p);
    vec2 e = vec2(.01, 0);
    
    vec4 n = d - vec4(
        map(p-e.xyyy),
        map(p-e.yxyy),
        map(p-e.yyxy),
        map(p-e.yyyx));
    
    return normalize(n);
}

float GetLight(vec4 p) {

    //lightPos.xz += vec2(sin(iTime), cos(iTime))*2.;
    vec4 l = normalize(lightPos-p);
    vec4 n = GetNormal(p);
    
    float dif = clamp(dot(n, l), 0., 1.);
    float d = rayMarch(p+n*HIT_DIST*2., l);
    if(d<length(lightPos-p)) dif *= .1;
    
    return dif;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord-.5*iResolution.xy)/iResolution.y;

    vec3 col = vec3(0);
    float w = 2.7+sin(iTime);
    vec4 ro = vec4(0, 1, -2,w-3.);
    vec4 rd = normalize(vec4(uv.x, uv.y, 1, 0.6));

    float d = rayMarch(ro, rd);
    
    vec4 p = ro + rd * d;
    
    float dif = GetLight(p);
    if(d<42.)dif+=0.2;
    col = vec3(dif);
    
    col = pow(col, vec3(.4545));	// gamma correction
    
    fragColor = vec4(col,1.0);
}
