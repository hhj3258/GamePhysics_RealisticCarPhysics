using System;

public class Car : DragProjectile
{
    private double muR;     // 롤링 마찰계수
    private double omegaE;  // 엔진 회전율(rpm)
    private double redline;
    private double finalDriveRatio;
    private double wheelRadius;
    private int gearNumber;     //  현재 기어 번호
    private int numberOfGears;  //  차량의 총 기어 수
    private string mode;    // 등속, 가속, 제동 등
    private double[] gearRatio;  //  기어 비

    // 생성자
    // DragProjectile 클래스 생성자를 호출하고 Car클래스 변수를 초기화
    public Car(double x, double y, double z, double vx,
               double vy, double vz, double time, double mass,
               double area, double density, double Cd, double redline,
               double finalDriveRatio, double wheelRadius,
               int numberOfGears) : 
                base(x, y, z, vx, vy, vz, time, mass, area, density, Cd)
    {

        //  생성자에 전달된 값으로 변수 초기화
        this.redline = redline;           //  redline rpm
        this.finalDriveRatio = finalDriveRatio;  //  final drive ratio
        this.wheelRadius = wheelRadius;   //  wheel radius
        this.numberOfGears = numberOfGears;   //  number of gears

        //  기어 비 배열 초기화
        //  기어 비 배열 0번째 인덱스는 사용하지 않을 것이기 때문에
        //  gearRatio 배열의 크기를 총 기어 비 +1로 설정하고 0번째 인덱스는 0으로 초기화
        //  모든 기어 비를 일단 1.0으로 초기화
        gearRatio = new double[numberOfGears + 1];
        gearRatio[0] = 0.0;
        for (int i = 1; i < numberOfGears + 1; ++i)
        {
            gearRatio[i] = 1.0;
        }

        //  모든 차량에 동일하게 설정할 변수들
        muR = 0.015;             //  롤링 마찰 계수
        omegaE = 1000.0;         //  engine rpm
        gearNumber = 1;          //  초기 기어
        mode = "accelerating";   //  초기 모드: 가속
    }

    //  변수 각각의 getter와 setter 선언
    public double MuR
    {
        get{ return muR; }
    }

    public double OmegaE
    {
        get { return omegaE; }
        set { omegaE = value; }
    }

    public double Redline
    {
        get { return redline; }
    }

    public double FinalDriveRatio
    {
        get { return finalDriveRatio; }
    }

    public double WheelRadius
    {
        get { return wheelRadius; }
    }

    public int GearNumber
    {
        get { return gearNumber; }
        set { gearNumber = value; }
    }

    public int NumberOfGears
    {
        get { return numberOfGears; }
    }

    public string Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    //  기어 비 배열의 getter와 setter
    public double GetGearRatio()
    {
        return gearRatio[gearNumber];
    }

    public void SetGearRatio(int index, double value)
    {
        gearRatio[index] = value;
    }

    //  기어 변속 메소드
    public void ShiftGear(int shift)
    {
        //  현재 기어가 가장 고단일 때 고단으로 변속 시 변화 없음
        if (shift + this.GearNumber > this.NumberOfGears)
        {
            return;
        }
        //  현재 기어가 가장 저단일 때 저단으로 변속 시 변화 없음
        else if (shift + this.GearNumber < 1)
        {
            return;
        }
        // 정상적인 기어 변속일 때
        // 기어 변경, 엔진 rpm 값 재계산
        else
        {
            double oldGearRatio = GetGearRatio();
            this.GearNumber = this.GearNumber + shift;
            double newGearRatio = GetGearRatio();
            this.OmegaE = this.OmegaE * newGearRatio / oldGearRatio;
        }

        return;
    }

    //  The GetRightHandSide() 메소드는 six first-order projectile ODEs의 오른쪽 항을 반환
    //  q[0] = vx = dxdt
    //  q[1] = x
    //  q[2] = vy = dydt
    //  q[3] = y
    //  q[4] = vz = dzdt
    //  q[5] = z
    public override double[] GetRightHandSide(double s, double[] q,
                                double[] deltaQ, double ds,
                                double qScale)
    {
        double[] dQ = new double[6];
        double[] newQ = new double[6];

        //  종속 변수의 중간 값 계산
        for (int i = 0; i < 6; ++i)
        {
            newQ[i] = q[i] + qScale * deltaQ[i];
        }

        //  torque curve 방정식
        //  엔진 회전율에 따라 세 부분으로 나누어서 정의
        double b, d;
        if (this.OmegaE <= 1000.0)
        {
            b = 0.0;
            d = 220.0;
        }
        else if (this.OmegaE < 4600.0)
        {
            b = 0.025;
            d = 195.0;
        }
        else
        {
            b = -0.032;
            d = 457.2;
        }

        //  속도의 중간값을 나타내기 위한 변수들 선언
        double vx = newQ[0];
        double vy = newQ[2];
        double vz = newQ[4];
        double v = Math.Sqrt(vx * vx + vy * vy + vz * vz) + 1.0e-8;

        //  공기항력 변수 선언 및 계산
        double density = this.Density;
        double area = this.Area;
        double cd = this.Cd;
        double Fd = 0.5 * density * area * cd * v * v;

        //  롤링 마찰 힘 계산
        //  G=-9.81
        double mass = this.Mass;
        double Fr = muR * mass * G;

        //  right-hand sides of the six ODEs 을 계산하면 속도의 중간 값이 나옴
        //  차량의 가속도는 모드(가속, 제동, 등속)에 의해 달라짐.
        //  제동 가속도는 -5.0 m/s^2 로 가정.
        if (String.Equals(mode, "accelerating"))
        {
            double c1 = -Fd / mass;
            double tmp = GetGearRatio() * finalDriveRatio / wheelRadius;
            double c2 = 60.0 * tmp * tmp * b * v / (2.0 * Math.PI * mass);
            double c3 = (tmp * d + Fr) / mass;
            dQ[0] = ds * (c1 + c2 + c3);
        }
        else if (String.Equals(mode, "braking"))
        {
            //  속도가 양수일 때만 브레이크 작동
            if (newQ[0] > 0.1)
            {
                dQ[0] = ds * (-5.0);
            }
            else
            {
                dQ[0] = 0.0;
            }
        }
        else
        {
            dQ[0] = 0.0;
        }

        dQ[1] = ds * newQ[0];
        dQ[2] = 0.0;
        dQ[3] = 0.0;
        dQ[4] = 0.0;
        dQ[5] = 0.0;

        return dQ;
    }
}