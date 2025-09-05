# Cooking_Knight
스파르타 유니티 11기 10주차 유니티 심화 팀과제

#### [[ 기획서 ]](https://www.notion.so/Cooking-Knight-25ec9d875db7809bbb4ffbaed6638234?source=copy_link)
#### [[ 발표자료 ]](https://www.canva.com/design/DAGx0EZTgGE/ePFWl_wrXAHOOhebuEVZlg/edit?utm_content=DAGx0EZTgGE&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)

---

## 목차

#### 1. 프로젝트 개요
#### 2. 주요 기능
#### 3. 게임 실행 화면
#### 4. 에셋 및 라이센스

---

## 프로젝트 개요
### 1. 프로젝트 소개
- 몬스터를 사냥하고 전리품을 획득해 맛있는 요리를 만들어 판매하는 게임
### 2. 장르
- 2D 플랫포머 액션 아케이드
### 3. 플랫폼
- PC
### 4. 개발 목적
- 기획, 개발 팀의 협업 업무 경험
- 개발 일정을 고려한 개발 범위 설정과 업무 분할
- 코어 루프, 핵심 시스템 구축
### 5. 개발 기간 및 범위
- 개발 기간
  - 25.09.01 ~ 25.09.05
- 개발 범위
  1. 플레이어 조작
  2. 코어 루프 시스템 (사냥, 요리, 음식 판매)
  3. 사운드 및 에셋
### 6. 사용 기술
- Unity / C#
### 7. 참여 인원
- __31 조__ : 기획
  - 고승진
  - 박기훈
- __13 조__ : 개발
  - 이찬희
  - 진영아
---
## 주요 기능
### 1. 조작
| 기능 | 방법 |
| :-: | :-: |
| 이동 | 키보드 A / D |
| 공격 | 마우스 좌클릭 |
| 점프 | Space |
| 아래 점프 | 키보드 S + Space |
| 상호작용 | 가까이 다가가 활성화 된 버튼 클릭 |
### 2. 파밍 스테이지
- 몬스터를 사냥하고 전리품을 획득할 수 있습니다.
- 몬스터는 다양한 행동을 하고, 플레이어를 공격할 수 있습니다.
### 3. 거점 스테이지
- 전리품을 이용해 음식을 요리하고, 판매할 수 있습니다.
- 거점에 머물면 체력이 자동으로 회복됩니다.
### 4. 요리
- 취사장에서 가진 전리품을 이용해 음식을 만듭니다.
- 희귀한 전리품으로 요리를 할 경우 더 비싼 음식을 만들 수 있습니다.
### 5. 판매
- 음식을 판매하고 골드를 획득합니다.
---
## 게임 실행 화면 (추후 gif 삽입 예정)
---
## 시스템 아키텍처 및 디자인 패턴
1. 캐릭터 상태 관리
- 선택 패턴: 상태(State) 패턴
- 선택 이유: 캐릭터의 다양한 상태(예: Idle, Walk, Attack, Death)에 따른 행동과 상태 전환을 객체화하여 관리합니다. 이는 각 상태별 로직을 분리함으로써 코드를 더 읽기 쉽고 유지보수하기 쉽게 만듭니다.

2. 캐릭터 기능 구현
- 선택 패턴: 컴포넌트 기반 설계(Component-Based Design)
- 선택 이유: 유니티(Unity)의 특성을 최대한 활용하기 위해 캐릭터의 기능을 Condition(상태), Movement(이동), Attack(공격)과 같은 독립적인 컴포넌트로 분리했습니다. 이를 통해 기능의 모듈화와 재사용성을 높여 개발 효율성을 향상시켰습니다.

3. 몬스터 생성 관리
- 선택 패턴: 오브젝트 풀링(Object Pooling)
- 선택 이유: 몬스터처럼 자주 생성되고 파괴되는 오브젝트의 경우, 매번 Instantiate와 Destroy를 사용하면 런타임 성능이 저하될 수 있습니다. 오브젝트 풀링을 사용해 미리 오브젝트를 생성하고 재활용함으로써 메모리 할당 및 해제 비용을 절감하여 게임의 최적화에 기여합니다.

4. UI 관리
- 선택 패턴: UI 프레임워크(UI Framework)
- 선택 이유: 게임의 많은 UI에서 공통적으로 사용되는 기능(예: UI 활성화/비활성화, 데이터 바인딩)과 작동 방식을 하나의 통합된 프레임워크로 구축했습니다. 이를 통해 일관성 있는 UI 개발 환경을 조성하고, 반복적인 작업을 줄여 생산성을 높였습니다.
---
## 에셋 및 사운드 라이센스 (assets, sounds lisence)
- 에셋 라이센스 (assets lisence)
1. knight
https://rgsdev.itch.io/pixel-art-animated-knight-character-pack-rgsdev
2. chicken
https://kittensoverboard.itch.io/pixel-companion-chicken
3. portal
https://pixelnauta.itch.io/pixel-dimensional-portal-32x32
4. restaurant
https://pixelmateai.itch.io/market-stalls
5. item
https://ghostpixxells.itch.io/pixelfood
https://astra-lissa.itch.io/pixel-food-icons
6. background
https://bongseng.itch.io/parallax-forest-desert-sky-moon
7. 2D Casual UI
https://assetstore.unity.com/packages/package/82080
- 사운드 라이센스 (sounds lisence)
1. background, player-hurt, player-jump
https://brackeysgames.itch.io/brackeys-platformer-bundle
2. player-walk
https://leohpaz.itch.io/rpg-essentials-sfx-free
3. player-attack
https://leohpaz.itch.io/minifantasy-dungeon-sfx-pack
4. UI click
https://cyrex-studios.itch.io/universal-ui-soundpack
5. chicken-hurt, kitchen, portal, restaurant
https://soundeffect-lab.info/sound/battle/
6. player-death
https://dillonbecker.itch.io/sdap
