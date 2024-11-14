# CrossRoad3D
 1. InputSystem 구현
    CharacterManager: 싱글톤을 구현하여 하나의 인스턴스만 존재하게 하였고 중복 인스턴스를 파괴하여 오류를 방지하였습니다.
    Player: InputSystem 실행을 명령하는 스크립트입니다.
    PlayerController: 실제로 실행하는 스크립트로 앞, 뒤, 좌, 우 움직임과 점프를 구현하였습니다.
문제점: 캐릭터가 앞, 뒤를 반대로 움직이는 문제
       캐릭터가 점프를 실행하지 않는 문제
