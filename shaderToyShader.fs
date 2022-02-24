#define MAX_MARCH_STEPS 100
#define MAX_MARCH_DIST 100.
#define HIT_DIST .01

vec4 lightPos = vec4(2, 1, 0,4);

vec4 spherePos = vec4(1.,1.,5.,2.5);
vec4 sphere2Pos = vec4(1.,1.,10.,3.1);
vec4 sphere3Pos = vec4(1.,1.,3.,3.2);
vec4 cubePos= vec4(-3.,1.,4.,2.5);

float sd4Sphere(vec4 p, float r) 
{
    return length(p)-r;
}

float sd4Cube(vec4 p, vec4 b)
{
    vec4 q = abs(p) - b;
    return min(max(max(max(q.z,q.w),q.y),q.x),0.) + length(max(q,0.));
}

vec2 opU( vec2 d1, vec2 d2 )
{
    return (d1.x<d2.x) ? d1 : d2;
}

vec2 map(vec4 pos)
{
    vec2 res = opU(vec2(sd4Sphere(pos-spherePos,1.0),1.0),vec2(sd4Cube(pos-cubePos,vec4(.7)),1.0));
         res = opU(res, vec2(min(sd4Sphere(pos-sphere2Pos,1.0),sd4Sphere(pos-sphere3Pos,0.5)),1.0));
         res = opU(res, vec2(sd4Sphere(pos-sphere3Pos,0.5),1.0));
    return res;
}

float rayMarch(vec4 rayO, vec4 rayD)
{
    float distO=0.;
    
    for(int i=0; i<MAX_MARCH_STEPS; i++)
    {
        vec4 p = rayO + rayD*distO;
        float distS = map(p).x;
        distO += distS;
        if(distO>MAX_MARCH_DIST||distS<HIT_DIST) break;
    }
    return distO;
}

vec4 GetNormal(vec4 p) {
	float d = map(p).x;
    vec2 e = vec2(.01, 0);
    
    vec4 n = d - vec4(
        map(p-e.xyyy).x,
        map(p-e.yxyy).x,
        map(p-e.yyxy).x,
        map(p-e.yyyx).x);
    
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
    float w = 3.+sin(iTime);
    vec4 ro = vec4(0, 1, 0,w);
    vec4 rd = normalize(vec4(uv.x, uv.y, 1, 0));

    float d = rayMarch(ro, rd);
    
    vec4 p = ro + rd * d;
    
    float dif = GetLight(p);
    col = vec3(dif);
    
    col = pow(col, vec3(.4545));	// gamma correction
    
    fragColor = vec4(col,1.0);
}