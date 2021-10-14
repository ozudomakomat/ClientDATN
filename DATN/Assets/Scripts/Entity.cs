
public class Entity
{
}

public class CDBT{
    public int id;
    public string name;
    public int mac;
    public float cdcn;
    public float cdck;
    public int mddh;
    public float fcubs;
    public float fcuaci;
}
public class CDT
{
    public int id;
    public string name;
    public int rsn;
    public int mddh;
    public int cdcn;
    public int cdcntd;
    public int cdcntdd;
}
public class DataNoiBo
{
    public float N;
    public float Mx;
    public float My;
    public int a;
    public int Cy;
    public int Cx;
    public int L;

    public DataNoiBo(float n, float mx, float my, int a, int cx, int cy, int l)
    {
        N = n;
        Mx = mx;
        My = my;
        this.a = a;
        Cy = cy;
        Cx = cx;
        L = l;
    }

    
}

public class History {
    public string udid;
    public int id;
    public int idBeTong;
    public int idThep;
    public float n;
    public float mx;
    public float my;
    public int a;
    public int b;
    public int h;
    public int l;
    public float ast;
    public float muy;
    public string date;
}
public class BoTriThep
{
    public int soluong;
    public int phi;

    public BoTriThep(int soluong, int phi)
    {
        this.soluong = soluong;
        this.phi = phi;
    }
}