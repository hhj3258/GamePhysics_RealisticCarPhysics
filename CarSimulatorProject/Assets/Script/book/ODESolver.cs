using System;

public class ODESolver
{
    //  Fourth-order Runge-Kutta ODE solver.  
    public static void RungeKutta4(ODE ode, double ds)
    {

        //  방정식 풀이를 위한 변수들 정의
        int j;
        int numEqns = ode.NumEqns;
        double s;
        double[] q;
        double[] dq1 = new double[numEqns];
        double[] dq2 = new double[numEqns];
        double[] dq3 = new double[numEqns];
        double[] dq4 = new double[numEqns];

        // 현재 종속 변수와 독립 변수를 찾음
        s = ode.S;  //독립변수
        q = ode.GetAllQ();  // 종속 변수

        // 4개의 Runge-Kutta 단계를 계산합니다.
        // getRightHandSide 메서드의 반환 값은 각 4단계에 대한 델타-q 값의 배열입니다.
        // 0.5, 1.0 등은 가중치를 나타내고 있음
        dq1 = ode.GetRightHandSide(s, q, q, ds, 0.0);
        dq2 = ode.GetRightHandSide(s + 0.5 * ds, q, dq1, ds, 0.5);
        dq3 = ode.GetRightHandSide(s + 0.5 * ds, q, dq2, ds, 0.5);
        dq4 = ode.GetRightHandSide(s + ds, q, dq3, ds, 1.0);

        // 새 종속 변수 위치에서 종속 변수 및 독립 변수 값을 업데이트하고
        // 값을 ODE 객체 배열에 저장합니다.
        ode.S = s + ds;

        for (j = 0; j < numEqns; ++j)
        {
            q[j] = q[j] + (dq1[j] + 2.0 * dq2[j] + 2.0 * dq3[j] + dq4[j]) / 6.0;
            ode.SetQ(q[j], j);
        }

        return;
    }
}

