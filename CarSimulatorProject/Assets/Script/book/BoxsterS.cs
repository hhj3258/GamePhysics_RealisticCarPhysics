using System;

public class BoxsterS : Car
{
    //  BoxsterS 생성자는 Car 생성자를 호출한 후에 BoxterS의 기어 비를 설정함.
    //  박스터S의 스펙
    //  mass = 1393.0 kg (with 70 kg driver)
    //  area = 1.94 m^2 
    //  Cd = 0.31
    //  redline = 7200 rpm
    //  finalDriveRatio = 3.44
    //  wheelRadius = 0.3186
    //  numberOfGears = 6;

    public BoxsterS(double x, double y, double z, double vx, double vy, double vz, double time, double density) :
               base(x, y, z, vx, vy, vz, time, 1393.0, 1.94, density, 0.31, 7200.0, 3.44, 0.3186, 6)
    {

        //  Set the gear ratios.
        SetGearRatio(1, 3.82);
        SetGearRatio(2, 2.20);
        SetGearRatio(3, 1.52);
        SetGearRatio(4, 1.22);
        SetGearRatio(5, 1.02);
        SetGearRatio(6, 0.84);
    }
}

