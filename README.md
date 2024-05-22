# 숲의마녀:마력의탐색 프로젝트

## 개발인원 및 기간
2024.04.20 ~ (1인 개발)

## 게임소개
Unity 3D로 개발한 탑뷰 형식의 모바일 게임입니다.
자신이 원하는 타일로 이동하며 몬스터를 쓰러트리는 것이 목표입니다.
몬스터는 탄막 형식의 게임이고 난이도는 게임을 진행할수록 쉬워질 것입니다.

## 게임플레이
### [메인화면]
![Screenshot_20240522-040255](https://github.com/GyuHawn/ForestWitch-MagicSearch/assets/125939517/de85bfda-1677-4423-85be-83586bbb23bf)
* 스토리, 옵션, 능력, 모험 레벨 등 게임에 필요한 설정을 합니다.

### [캐릭터 선택]
![Screenshot_20240522-040310](https://github.com/GyuHawn/ForestWitch-MagicSearch/assets/125939517/177d5c55-6c79-4449-86bd-4239d1d66fb1)
* 1명의 캐릭터와 2개의 대포를 선택하여 원하는 플레이를 진행합니다.

### [타일 맵]
![Screenshot_20240522-040323](https://github.com/GyuHawn/ForestWitch-MagicSearch/assets/125939517/439302ee-716c-4623-9f19-e44d75a7631d)
* 원하는 타일을 선택하여 자신이 원하는 빌드를 선택할 수 있습니다.

### [몬스터 맵]
![Screenshot_20240522-040336](https://github.com/GyuHawn/ForestWitch-MagicSearch/assets/125939517/ad38f6dd-9fdf-4598-8069-158bde36dedb)
* 탄막 형식으로 진행되며 총알을 주워 대포를 이용하여 몬스터를 공격합니다.

## 사용기술
### [Unity]
* Uinity 2022.3.14f1 버전 사용
### [C#]
### [AI]
* Chat Cpt - 이미지 생성 및 코드 해결에 사용
* Suno - BGM 생성
* RunWay - 이미지 -> 동영상 생성

## 에러사항
* 타일 맵과 몬스터 맵간의 필요/불필요한 오브젝트 관리를 스테이지 전환시 UI를 추가하여 자연스럽게 해결
* 원하는 느낌의 에셋을 찾기 어려워 게임이 약간 부자연스러워지는 문제 발생
* 대부분의 UI 이미지를 AI를 이용하여 생성했으나, 생성할 때마다 그림체가 조금씩 달라져 어색함이 발생
* 구글 플레이 기능을 사용했지만, 대부분의 자료가 몇 년 전 버전의 정보였고 최신 버전의 정보가 적어 자료 검색 및 AI를 이용하여 최대한 오류를 해결
