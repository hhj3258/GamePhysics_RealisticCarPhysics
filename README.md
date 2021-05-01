# (진행중)게임물리학 자동차 물리 구현 프로젝트

## 개요
- 실제적인 자동차 게임물리 구현   
- 자동차의 게임물리 구현에 포커싱을 맞추기 위해 기본적인 강체의 운동 등의 물리는 Unity의 기본 컴포넌트를 사용   
- 자동차의 엔진, 기어 변속, 브레이크 등의 물리 구현에 초점을 맞춰 진행   
![image](https://user-images.githubusercontent.com/70702088/116791066-d3f83a00-aaf2-11eb-98f5-fe750259602d.png)

## 물리 공식
- 중요한 파트 일부분만 삽입

![image](https://user-images.githubusercontent.com/70702088/116791071-da86b180-aaf2-11eb-8d34-fc55b9c8e4c4.png)
![image](https://user-images.githubusercontent.com/70702088/116791243-e757d500-aaf3-11eb-878a-364ae82dac7b.png)

## ODESolver
상미분방정식을 룽게-쿠타 방정식을 이용해서 풂   
오일러 방정식보다 정확한 룽게-쿠타 방정식을 이용해서 차량의 속도와 이동거리를 구함   
Fourth-Order Runge-Kutta 방정식   
![image](https://user-images.githubusercontent.com/70702088/116791137-4537ed00-aaf3-11eb-8b72-c927c78bb869.png)

```C#
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
```

## 2차원 평면
- WindowForm을 사용해서 2차원 상에서 시뮬레이터 구현   
![image](https://user-images.githubusercontent.com/70702088/116791169-7b756c80-aaf3-11eb-812a-feb12570c991.png)

## 3차원
- Unity로 3차원 상에서 구현   
- 향후 유니티 휠 콜라이더를 제거하고 구현 예정   
![image](https://user-images.githubusercontent.com/70702088/116791227-ce4f2400-aaf3-11eb-8302-b1b7721bee44.png)
