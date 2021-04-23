using System;

public abstract class ODE
{
    //  클래스에서 사용할 변수들 정의
    private int numEqns;  //  number of equations to solve
    private double[] q;   //  종속 변수의 배열
    private double s;     //  독립 변수

    //  Constructor
    public ODE(int numEqns)
    {
        this.numEqns = numEqns;
        q = new double[numEqns];
    }

    //getter 및 setter
    public int NumEqns
    {
        get { return numEqns; }
    }

    public double S
    {
        get { return s; }
        set { s = value; }
    }

    //  q 배열의 특정 인덱스 getter
    public double GetQ(int index)
    {
        return q[index];
    }
    //  q 배열의 특정 인덱스 setter
    public void SetQ(double value, int index)
    {
        q[index] = value;
        return;
    }
    //  q 배열의 getter
    public double[] GetAllQ()
    {
        return q;
    }

    
    // ODE 방정식 풀이를 위한 오른쪽 항들에 관한 추상 메소드 선언
    // 하위 클래스에서 이 메소드에 관한 구현을 따로 해줄 것임
    public abstract double[] GetRightHandSide(double s,
        double[] q, double[] deltaQ, double ds, double qScale);
}

